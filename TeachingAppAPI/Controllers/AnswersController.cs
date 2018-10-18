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
    public class AnswersController : Controller
    {
        /* This Contoller contains the following:
       - GetAnswer(): Selects particular question
       - GetAllAnswers(): selects all available answers ??? remove later ???
       - GetQuestionAnswer: gets all answers for a particular question
       - CreateNewQuestion(): creates a new answer
       - UpdateAnswer(): updates an existing answer
       - Delete(): deletes an answer where a question has no associated quizinstanceanswers.

        To do:
        - GetCorrectQuestionAnswer: gets all correct answers for a particular question
        - GetIncorrectQuestionAnswer: gets all incorrect answers for a particular question
       */


        private TestDB_Phase2Context _context;
        private IAnswerService _answerService;

        public AnswersController(TestDB_Phase2Context context, IAnswerService answerService)
        {
            _context = context;
            _answerService = answerService;
        }


        // ???? Remove later ??????????
        // Select all Answers:
        // Returns json array of Answer objects 
        [HttpGet]
        public IActionResult GetAllAnswers()
        {
            var answers = _context.Answer.FromSql("select * from Answer").ToArray();
            var length = answers.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    answerID = answers.ElementAt(i).AnswerId,
                    questionId = answers.ElementAt(i).QuestionId,
                    answerTypeId = answers.ElementAt(i).AnswerTypeId,
                    answerText = answers.ElementAt(i).AnswerText
                };

            }

            return Ok(returnArray);

        }


        // Select an Answer:
        // Returns json array of one Answer objects 
        [HttpGet("{id}")]
        public IActionResult GetAnswer(int id)
        {
            var answer = _context.Answer.FromSql($"select * from Answers where AnswerId = {id}").ToArray();
            var length = answer.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    answerID = answer.ElementAt(i).AnswerId,
                    questionId = answer.ElementAt(i).QuestionId,
                    answerTypeId = answer.ElementAt(i).AnswerTypeId,
                    answerText = answer.ElementAt(i).AnswerText
                };

            }

            return Ok(returnArray);

        }


        // Select all Answers associated with a Question:
        // Returns json array of Answer objects 
        [HttpGet("/question-answers/{id}", Name = "Question-answers")]
        public IActionResult GetQuestionAnswers(int id)
        {
            var answers = _context.Answer.FromSql($"select * from Answer where QuestionId = {id}").ToArray();
            var length = answers.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    answerID = answers.ElementAt(i).AnswerId,
                    questionId = answers.ElementAt(i).QuestionId,
                    answerTypeId = answers.ElementAt(i).AnswerTypeId,
                    answerText = answers.ElementAt(i).AnswerText
                };

            }

            return Ok(returnArray);

        }


        // POST api/Answer
        [HttpPost]
        public IActionResult CreateNewAnswer([FromBody]Answer newAnswer)
        {
            try
            {
                Console.WriteLine("called createAnswer()");
                var answer = _answerService.CreateAnswer(newAnswer);

                return Ok(new
                {
                    questionId = answer.QuestionId,
                    answerTypeId = answer.AnswerTypeId,
                    answerText = answer.AnswerText

                });
            }
            catch (CustomException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        // Update an Answer:
        [HttpPut("update-answer")]
        public void UpdateAnswer([FromBody]Answer updateAnswer)
        {

            var answer = _context.Answer.FromSql($"select * from Answer where AnswerId = {updateAnswer.AnswerId}").ToArray();

            answer.ElementAt(0).QuestionId = updateAnswer.QuestionId; //???? Should we be able to change this?
            answer.ElementAt(0).AnswerTypeId = updateAnswer.AnswerTypeId;
            answer.ElementAt(0).AnswerText = updateAnswer.AnswerText;

            _context.SaveChanges();

        }


        // DELETE an Answer:
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var topic = _context.Topic.FromSql($"delete from Answer where AnswerId = {id}").ToArray();
        }
    }
}