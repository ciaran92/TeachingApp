using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingAppAPI.Models;

namespace TeachingAppAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private TestDBContext context;

        public UsersController(TestDBContext context)
        {
            this.context = context;
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
