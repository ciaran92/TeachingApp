using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingAppAPI.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeachingAppAPI.Controllers
{
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private TestDBContext context;        

        public QuestionsController(TestDBContext context)
        {
            this.context = context;
        }
        // GET api/values
        [HttpGet]
        public IActionResult GetQuestions()
        {
            var blogs = context.Questions.FromSql("EXEC randomTenQuestionQuiz").ToArray();
            var dog = new object[blogs.Length];

            for(int i = 0; i < blogs.Length; i++)
            {
                dog[i] = new
                {
                    QuestionId = blogs.ElementAt(i).QuestionId,
                    Question = blogs.ElementAt(i).Question,
                    Answer = blogs.ElementAt(i).Answer,
                    Options = new object[4] 
                    {
                        blogs.ElementAt(i).Option1,
                        blogs.ElementAt(i).Option2,
                        blogs.ElementAt(i).Option3,
                        blogs.ElementAt(i).Option4
                    }
                };
            }
            //return Ok(blogs.ElementAt(1).Option1);
            
                //return Ok(dog);
            

            return Ok(dog);
            
        }


    }
}
