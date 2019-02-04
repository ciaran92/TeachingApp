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
                UserLogin(user);
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
                var SecretKey = Encoding.UTF8.GetBytes(appSettings.Secret);
                var key = new SymmetricSecurityKey(SecretKey);
                var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                var token = new JwtSecurityToken
                        (
                        issuer: "http://localhost:52459",
                        audience: "http://localhost:52459",
                        expires: DateTime.Now.AddSeconds(10),
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

        // Method used to Log In
        [HttpPost("token")]
        public IActionResult UserLogin([FromBody]AppUser userInput)
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


        /**
         * Method used to Refresh and give new AccessToken to client.
         * Method is called from client whenever a 401 unauthorized error is given, the client will then attempt to get a new accessToken by
         * calling this method parsing its Expired access token and current refresh token. If tokens are valid and refresh token has not expired
         * a new access token will be sent back to client
         * https://www.blinkingcaret.com/2018/05/30/refresh-tokens-in-asp-net-core-web-api/
         */
        [AllowAnonymous]
        [HttpPost("token/refresh")]
        public IActionResult RefreshToken([FromBody]dynamic tokens)
        {
            string parsedRefreshToken = Convert.ToString(tokens.RefreshToken);
            string parsedAccessToken = Convert.ToString(tokens.AccessToken);

            if(parsedRefreshToken.Length <= 0 || parsedAccessToken.Length <= 0)
            {
                Console.WriteLine("empty negros");
                return Unauthorized();
            }
            Console.WriteLine("refresh token parsed: " + parsedRefreshToken);
            var principal = GetPrincipalFromExpiredToken(parsedAccessToken);
            int userId = Convert.ToInt32(principal.Identity.Name);

            var refreshTokenFromDB = _tokenGenerator.GetRefreshTokenFromDB(userId);

            if (refreshTokenFromDB == null)
            {
                return BadRequest();
            }

            // If refresh token has expired return unauthorized
            if (refreshTokenFromDB.ExpiresUtc < DateTime.Now)
            {
                Console.WriteLine("DateTime.Now: " + DateTime.Now);
                Console.WriteLine("refreshTokenFromDatabase.ExpiresUtc: " + refreshTokenFromDB.ExpiresUtc);
                return Unauthorized();
            }

            // If token from db is different to the token parsed then return unathorized
            if(refreshTokenFromDB.Token != parsedRefreshToken)
            {
                return Unauthorized();
            }
            
            // If RefreshToken and old AccessToken are both valid, generate new Access token and send it back to Client
            var user = context.AppUser.FirstOrDefault(x => x.AppUserId == userId);
            var newAccessToken = _tokenGenerator.CreateAccessToken(user);

            return Ok(new { token = newAccessToken });
        }


        private ClaimsPrincipal GetPrincipalFromExpiredToken(String token)
        {
            var SecretKey = Encoding.UTF8.GetBytes(appSettings.Secret);
            var key = new SymmetricSecurityKey(SecretKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        
    }
}
