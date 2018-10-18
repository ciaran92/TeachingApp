using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingAppAPI.Data;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Models;
using TeachingAppAPI.Services;

namespace TeachingAppAPI.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        /* This Controller contains the following:
        - GetQuiz(): Selects particular quiz
        - GetAllQuizzes(): selects all available quizzes ???
        - GetTopicQuizzes: gets all quizzes for a particular topic
        - CreateNewQuiz(): creates a new quiz
        - UpdateQuiz(): updates an existing quiz
        - Delete(): deletes a quiz where a quiz has no associated answers, quizinstance, etc.
 
        */

        private TestDB_Phase2Context _context;
        private IQuizService _quizService;

        public QuizController(TestDB_Phase2Context context, IQuizService quizService)
        {
            _context = context;
            _quizService = quizService;
        }

        // Select all Quizzes:
        // Returns json array of Quiz objects 
        [HttpGet]
        public IActionResult GetAllQuizzes()
        {
            var quizzes = _context.Quiz.FromSql("select * from Quiz").ToArray();
            var length = quizzes.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    topicId = quizzes.ElementAt(i).TopicId,
                    quizTypeId = quizzes.ElementAt(i).QuizTypeId,
                    quizDesc = quizzes.ElementAt(i).QuizDesc
                };

            }

            return Ok(returnArray);

        }


        // Select a Quiz:
        // Returns json array of one Quiz objects 
        [HttpGet("{id}")]
        public IActionResult GetQuiz(int id)
        {
            var quiz = _context.Quiz.FromSql($"select * from Quiz where QuizId = {id}").ToArray();
            var length = quiz.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    topicId = quiz.ElementAt(i).TopicId,
                    quizTypeId = quiz.ElementAt(i).QuizTypeId,
                    quizDesc = quiz.ElementAt(i).QuizDesc
                };

            }

            return Ok(returnArray);

        }


        // Select all Quizzs assoicated with a Topic:
        // Returns json array of Quiz objects 
        [HttpGet("/quizzes/{id}", Name = "Topic-quizzes")]
        public IActionResult GetTopicQuizzes(int id)
        {
            var quizzes = _context.Quiz.FromSql($"select * from Quiz where TopicId = {id}").ToArray();
            var length = quizzes.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    topicId = quizzes.ElementAt(i).TopicId,
                    quizTypeId = quizzes.ElementAt(i).QuizTypeId,
                    quizDesc = quizzes.ElementAt(i).QuizDesc
                };

            }

            return Ok(returnArray);

        }


        // POST api/Quiz
        [HttpPost]
        public IActionResult CreateNewQuiz([FromBody]Quiz newQuiz)
        {
            try
            {
                Console.WriteLine("called createQuiz()");
                var quiz = _quizService.CreateQuiz(newQuiz);

                return Ok(new
                {
                    topicId = quiz.TopicId,
                    quizTypeId = quiz.QuizTypeId,
                    quizDesc = quiz.QuizDesc
                });
            }
            catch (CustomException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        // Update a Quiz:
        [HttpPut("update-quiz")]
        public void UpdateTopic([FromBody]Quiz updateQuiz)
        {

            var quiz = _context.Quiz.FromSql($"select * from Quiz where QuizId = {updateQuiz.QuizId}").ToArray();

            quiz.ElementAt(0).TopicId = updateQuiz.TopicId;
            quiz.ElementAt(0).QuizTypeId = updateQuiz.QuizTypeId;
            quiz.ElementAt(0).QuizDesc = updateQuiz.QuizDesc;

            _context.SaveChanges();

        }





        // DELETE a Quiz:
        // Only works if there are no answers, etc., accociated with the Quiz
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var topic = _context.Topic.FromSql($"delete from Toipc where TopicId = {id}").ToArray();
        }



    }
}