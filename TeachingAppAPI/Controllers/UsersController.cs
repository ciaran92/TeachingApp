using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TeachingAppAPI.Data;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Models;
using TeachingAppAPI.Services;

namespace TeachingAppAPI.Controllers
{

    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private TestDB_Phase2Context context;
        //private Example mailServer;
        private readonly AppSettings appSettings;
        private EmailSender emailSender;
        private IUserService _userService;

        public UsersController(TestDB_Phase2Context context, IOptions<AppSettings> appSettings, IOptions<EmailSender> emailSender, IUserService userService)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
            this.emailSender = emailSender.Value;
            _userService = userService;
            //this.mailServer = mailServer;
        }
        // GET api/values
        [HttpGet]
        public IActionResult GetUsers()
        {
            var blogs = context.AppUser.FromSql("select * from AppUser").ToArray();
            return Ok(blogs);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("confirm-account")]
        public IActionResult ConfirmAccount([FromBody]AppUser user)
        {
            var generatedCode = context.AppUser.FromSql($"select * from AppUser where AppUserId = {user.AppUserId}").ToArray();
            Console.WriteLine("the actual code is: " + generatedCode.ElementAt(0).VerificationCode);
            Console.WriteLine("the AppUser code is: " + user.VerificationCode);
            Console.WriteLine(user.VerificationCode == generatedCode.ElementAt(0).VerificationCode);
            // if entered code == code for user in database continue
            if (user.VerificationCode == generatedCode.ElementAt(0).VerificationCode)
            {
                //var isAccVerified = true;
                //user.AccountVerified = true;
                var param = new SqlParameter("@AppUserId", user.AppUserId);
                context.Database.ExecuteSqlCommand($"verifyAccount @AppUserId", param);
                //context.AppUser.FromSql($"verifyAccount @AppUserId", param);
                //context.Update(user);
                context.SaveChanges();
                return Ok(new
                {
                    id = generatedCode.ElementAt(0).AppUserId,
                    userName = generatedCode.ElementAt(0).UserName,
                    email = generatedCode.ElementAt(0).Email,
                    firstName = generatedCode.ElementAt(0).FirstName,
                    lastName = generatedCode.ElementAt(0).LastName
                });
            }
            else
            {
                return BadRequest("piss off wrong code m9");
            }
            // generate token
        }

        // POST api/AppUser
        [HttpPost]
        public IActionResult CreateNewUser([FromBody]AppUser newUser)
        {
            try
            {
                Console.WriteLine("called createUser()");
                var user = _userService.CreateUser(newUser, newUser.UserPassword);
                emailSender.SendEmailAsync(appSettings.MailKey, user.Email, "Your verifiaction code", "Your verification code is " + user.VerificationCode);
                Console.WriteLine("sent mail");
                return Ok(new
                {
                    id = user.AppUserId,
                    userName = user.UserName,
                    email = user.Email,
                    firstName = user.FirstName,
                    lastName = user.LastName
                }); //really strange... id, UserName etc have to be lower case
            }
            catch (CustomException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        
        public static string GenerateCode(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string code = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return code;
        }

        /**
         * Checks to see if user exists.
         * returns true if user exists.
         **/
        public bool UserExists(string UserName)
        {
            if(!context.AppUser.Any(x => x.UserName == UserName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EmailTaken(string emailAddress)
        {
            if(context.AppUser.Any(x => x.Email == emailAddress))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost("token")]
        public IActionResult Token([FromBody]AppUser userInput)
        {
            var user = context.AppUser.SingleOrDefault(x => x.UserName == userInput.UserName); // understand more on this line
            if(userInput == null || user == null)
            {
                return BadRequest("UserName does not exist!");
            }
            if(userInput.UserPassword != user.UserPassword)
            {
                return BadRequest("Incorroect password");
            }
            if(user.AccountVerified == false)
            {
                return Ok(new
                {
                    id = user.AppUserId,
                    userName = user.UserName,
                    email = user.Email,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    verified = user.AccountVerified
                }); //really strange... id, UserName etc have to be lower case
            }
            //var checkCreds = context.AppUser.FromSql($"select * from AppUser where UserName = {inputModel.UserName}").ToArray();
            if (IsUserAccountVerified(userInput.UserName))
            {
                Console.WriteLine((context.AppUser.Where(x => x.UserName == userInput.UserName).SingleOrDefault()).ToString());
                Console.WriteLine("helooooo");
                var SecretKey = Encoding.ASCII.GetBytes(appSettings.Secret);
                var key = new SymmetricSecurityKey(SecretKey);
                var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userInput.UserName)
                };
                var token = new JwtSecurityToken
                        (
                        issuer: "http://localhost:52459",
                        audience: "http://localhost:52459",
                        expires: DateTime.Now.AddMinutes(1),
                        claims: claims,
                        signingCredentials: signInCred
                        );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                //Example.SendMail();
                return Ok(new
                { token = tokenString,
                    id = user.AppUserId,
                    userName = user.UserName,
                    email = user.Email,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    verified = user.AccountVerified
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        public string TokenGenerator(bool accountVerified)
        {
            var SecretKey = Encoding.ASCII.GetBytes(appSettings.Secret);
            var key = new SymmetricSecurityKey(SecretKey);
            var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var claims = new List<Claim>
                {
                    new Claim("UserAccountVerified", accountVerified.ToString())
                };
            var token = new JwtSecurityToken
                        (
                        issuer: "http://localhost:52459",
                        audience: "http://localhost:52459",
                        expires: DateTime.Now.AddMinutes(1),
                        claims: claims,
                        signingCredentials: signInCred
                        );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsUserAccountVerified(string UserName)
        {
            var userDetails = context.AppUser.FromSql($"select * from AppUser where UserName = {UserName}").ToArray();

            if(userDetails.ElementAt(0).AccountVerified == true)
            {
                return true;
            }
            return false;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
