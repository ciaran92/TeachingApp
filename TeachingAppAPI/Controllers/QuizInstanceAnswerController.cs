using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingAppAPI.Data;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Entities;
using TeachingAppAPI.Services;

namespace TeachingAppAPI.Controllers
{
    [Route("api/[controller]")]
    public class QuizInstanceAnswerController : Controller
    {
        /* This Controller contains the following:
        - GetQuizInstanceAnswer(): Selects particular quizInstanceAnswer
        - GetAllQuizInstanceAnswers(): selects all available quizInstanceAnswer ???
        - GetQuizInstanceAnswers(): gets all quizInstanceAnswer for a particular QuizInstance
        - CreateNewQuizInstanceAnswer(): creates a new quizInstanceAnswer
        - UpdateQuizInstanceAnswer(): updates an existing quizInstanceAnswer
        - Delete(): deletes a quizInstanceAnswer.
 
        */

        private TestDB_Phase2Context _context;
        private IQuizInstanceAnswerService _quizInstanceAnswerService;

        public QuizInstanceAnswerController(TestDB_Phase2Context context, IQuizInstanceAnswerService quizInstanceAnswerService)
        {
            _context = context;
            _quizInstanceAnswerService = quizInstanceAnswerService;
        }

        //?????? remove later ????????
        // Select all QuizInstanceAnswers:
        // Returns json array of QuizInstanceAnswer objects 
        [HttpGet]
        public IActionResult GetAllQuizInstanceAnswers()
        {
            var quizInstanceAnswers = _context.QuizInstanceAnswer.FromSql("select * from QuizInstanceAnswer").ToArray();
            var length = quizInstanceAnswers.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    quizInstanceAnswerId = quizInstanceAnswers.ElementAt(i).QuizInstanceAnswerId,
                    quizInstanceId = quizInstanceAnswers.ElementAt(i).QuizInstanceId,
                    questionId = quizInstanceAnswers.ElementAt(i).QuestionId,
                    answerId = quizInstanceAnswers.ElementAt(i).AnswerId,
                    appUserAnswerId = quizInstanceAnswers.ElementAt(i).AppUserAnswer,
                    appUserAnswerDateTime = quizInstanceAnswers.ElementAt(i).AppUserAnswerDateTime
                };

            }

            return Ok(returnArray);

        }


        // Select a QuizInstanceAnswer:
        // Returns json array of one QuizInstanceAnswer objects 
        [HttpGet("{id}")]
        public IActionResult GetQuizInstanceAnswer(int id)
        {
            var quizInstanceAnswer = _context.QuizInstanceAnswer.FromSql($"select * from QuizInstanceAnswer where QuizInstanceAnswerId = {id}").ToArray();
            var length = quizInstanceAnswer.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    quizInstanceAnswerId = quizInstanceAnswer.ElementAt(i).QuizInstanceAnswerId,
                    quizInstanceId = quizInstanceAnswer.ElementAt(i).QuizInstanceId,
                    questionId = quizInstanceAnswer.ElementAt(i).QuestionId,
                    answerId = quizInstanceAnswer.ElementAt(i).AnswerId,
                    appUserAnswerId = quizInstanceAnswer.ElementAt(i).AppUserAnswer,
                    appUserAnswerDateTime = quizInstanceAnswer.ElementAt(i).AppUserAnswerDateTime
                };

            }

            return Ok(returnArray);

        }


        // Select all QuizInstanceAnswers assoicated with a QuizInstance:
        // Returns json array of QuizInstanceAnswer objects 
        [HttpGet("/quizinstance-answers/{id}", Name = "quizinstance-answers")]
        public IActionResult GetQuizInstanceAnswers(int id)
        {
            var quizInstanceAnswers = _context.QuizInstanceAnswer.FromSql($"select * from QuizInstanceAnswer where QuizInstanceId = {id}").ToArray();
            var length = quizInstanceAnswers.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    quizInstanceAnswerId = quizInstanceAnswers.ElementAt(i).QuizInstanceAnswerId,
                    quizInstanceId = quizInstanceAnswers.ElementAt(i).QuizInstanceId,
                    questionId = quizInstanceAnswers.ElementAt(i).QuestionId,
                    answerId = quizInstanceAnswers.ElementAt(i).AnswerId,
                    appUserAnswerId = quizInstanceAnswers.ElementAt(i).AppUserAnswer,
                    appUserAnswerDateTime = quizInstanceAnswers.ElementAt(i).AppUserAnswerDateTime
                };

            }

            return Ok(returnArray);

        }


        // POST api/QuizInstanceAnswer
        [HttpPost]
        public IActionResult CreateNewQuizInstanceAnswer([FromBody]QuizInstanceAnswer newQuizInstanceAnswer)
        {
            try
            {
                //newQuizInstanceAnswer.AppUserAnswerDateTime = DateTime.UtcNow;
                //newQuizInstanceAnswer.AppUserAnswer = "P";
                var quizInstanceAnswer = _quizInstanceAnswerService.CreateQuizInstanceAnswer(newQuizInstanceAnswer);

                Console.WriteLine("called createQuizInstanceAnswer(3)" + quizInstanceAnswer.AnswerId);
                return Ok(new
                {
                    quizInstanceAnswerId = quizInstanceAnswer.QuizInstanceAnswerId,
                    quizInstanceId = quizInstanceAnswer.QuizInstanceId,
                    questionId = quizInstanceAnswer.QuestionId,
                    answerId = quizInstanceAnswer.AnswerId,
                    appUserAnswerId = quizInstanceAnswer.AppUserAnswer,
                    appUserAnswerDateTime = quizInstanceAnswer.AppUserAnswerDateTime
                    
                });
            }
            catch (CustomException e)
            {
                return BadRequest();
            }
        }


        // Update a QuizInstanceAnswer:
        [HttpPut("update-quizinstanceanswer")]
        public void UpdateQuizInstanceAnswer([FromBody]QuizInstanceAnswer updateQuizInstanceAnswer)
        {

            var quizInstanceAnswer = _context.QuizInstanceAnswer.FromSql($"select * from QuizInstanceAnswer where QuizInstanceAnswerId = {updateQuizInstanceAnswer.QuizInstanceAnswerId}").ToArray();

            quizInstanceAnswer.ElementAt(0).QuizInstanceId = updateQuizInstanceAnswer.QuizInstanceId;
            quizInstanceAnswer.ElementAt(0).QuestionId = updateQuizInstanceAnswer.QuestionId;
            quizInstanceAnswer.ElementAt(0).AnswerId = updateQuizInstanceAnswer.AnswerId;
            quizInstanceAnswer.ElementAt(0).AppUserAnswer = updateQuizInstanceAnswer.AppUserAnswer;
            quizInstanceAnswer.ElementAt(0).AppUserAnswerDateTime = updateQuizInstanceAnswer.AppUserAnswerDateTime;

            _context.SaveChanges();

        }


        // DELETE a QuizInstanceAnswer:
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var quizInstanceAnswer = _context.QuizInstanceAnswer.FromSql($"delete from QuizInstanceAnswer where QuizInstanceAnswerId = {id}").ToArray();
        }
    }
}