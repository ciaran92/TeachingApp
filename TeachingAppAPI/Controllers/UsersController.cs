using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TeachingAppAPI.Data;
using TeachingAppAPI.Entities;
using TeachingAppAPI.Helpers;
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
        private IDataProtector _protector;
        private ITokenGenerator _tokenGenerator;

        public UsersController(TestDB_Phase2Context context, IOptions<AppSettings> appSettings, IOptions<EmailSender> emailSender, IUserService userService, ITokenGenerator tokenGenerator, IDataProtectionProvider provider)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
            this.emailSender = emailSender.Value;
            _userService = userService;
            _tokenGenerator = tokenGenerator;
            _protector = provider.CreateProtector("Contoso.UsersController.v1");
            //this.mailServer = mailServer;
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
                CreateToken(user);
                return Ok();
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
                var SecretKey = Encoding.ASCII.GetBytes(appSettings.Secret);
                var key = new SymmetricSecurityKey(SecretKey);
                var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
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
                return Ok(new
                {
                    token = tokenString,
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

        [HttpPost("token")]
        public IActionResult CreateToken([FromBody]AppUser userInput)
        {
            var user = context.AppUser.SingleOrDefault(x => x.UserName == userInput.UserName);
            Console.WriteLine("user: " + user);
            if(user != null)
            {
                if (_userService.Authenticate(user, userInput.UserName, userInput.UserPassword))
                {
                    // create refresh token here
                    RefreshToken refreshToken = _tokenGenerator.CreateRefreshToken(user.AppUserId);
                    // call create access token method here

                    string accessToken = _tokenGenerator.CreateAccessToken(user);
                    // TODO: create new user model to return
                    return Ok(new
                    {
                        token = accessToken,
                        refreshToken = refreshToken.Token,
                        id = user.AppUserId,
                        userName = user.UserName
                    });
                }
                else
                {
                    return BadRequest("Incorrect username or password");
                }
            }
            else
            {
                return BadRequest("User does not exist");
            }       
        }

        [HttpPost("token/refresh")]
        public IActionResult RefreshToken([FromBody] RefreshToken refreshToken)
        {
            var refreshTokenFromDatabase = context.RefreshToken.Include(x => x.AppUser).SingleOrDefault(i => i.Token == refreshToken.Token);
            Console.WriteLine("DateTime.Now: " + DateTime.Now);
            if (refreshTokenFromDatabase == null)
            {
                return BadRequest();
            }
            if (refreshTokenFromDatabase.ExpiresUtc < DateTime.Now)
            {
                Console.WriteLine("DateTime.Now: " + DateTime.Now);
                Console.WriteLine("refreshTokenFromDatabase.ExpiresUtc: " + refreshTokenFromDatabase.ExpiresUtc);
                return Unauthorized();
            }
            AppUser user = refreshTokenFromDatabase.AppUser;
            string accessToken = _tokenGenerator.CreateAccessToken(user);


            return Ok(new
            {
                token = accessToken,
                refreshToken = refreshTokenFromDatabase.Token,
                id = refreshTokenFromDatabase.AppUser.AppUserId
            });
        

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
