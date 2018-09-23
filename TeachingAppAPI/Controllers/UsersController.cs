using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TeachingAppAPI.Data;
using TeachingAppAPI.Models;

namespace TeachingAppAPI.Controllers
{
    
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private TestDBContext context;
        private readonly AppSettings appSettings;

        public UsersController(TestDBContext context, IOptions<AppSettings> appSettings)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
        }
        // GET api/values
        [HttpGet]
        public IActionResult GetUsers()
        {
            var blogs = context.Users.FromSql("select * from users").ToArray();
            return Ok(blogs);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Users value)
        {
            context.Users.Add(value);
            context.SaveChanges();
            return new ObjectResult("User added");
        }

        [HttpPost("token")]
        public IActionResult Token([FromBody]Users userInput)
        {
            var user = context.Users.SingleOrDefault(x => x.Username == userInput.Username); // understand more on this line
            if(userInput == null || user == null)
            {
                return BadRequest("Password or Username incorrect!");
            }

            //var checkCreds = context.Users.FromSql($"select * from Users where Username = {inputModel.Username}").ToArray();
            if (userInput.Username == user.Username && userInput.UserPassword == user.UserPassword)
            {
                Console.WriteLine((context.Users.Where(x => x.Username == userInput.Username).SingleOrDefault()).ToString());
                Console.WriteLine("helooooo");
                var SecretKey = Encoding.ASCII.GetBytes(appSettings.Secret);
                var key = new SymmetricSecurityKey(SecretKey);
                var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userInput.Username)
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
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
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
