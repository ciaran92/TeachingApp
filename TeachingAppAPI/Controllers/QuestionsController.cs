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
    public class QuestionsController : ControllerBase
    {
        /* This Contoller contains the following:
       - GetQuestion(): Selects particular question
       - GetAllQuestions(): selects all available question ??? remove later ???
       - GetQuizQuestion: gets all questions for a particular quiz
       - CreateNewQuestion(): creates a new question
       - UpdateQuestion(): updates an existing question
       - Delete(): deletes a question where a question has no associated answers, quizinstanceanswer, etc.

       */


        private TestDB_Phase2Context _context;
        private IQuestionService _questionService;

        public QuestionsController(TestDB_Phase2Context context, IQuestionService questionService)
        {
            _context = context;
            _questionService = questionService;
        }

        //// GET api/values
        //[HttpGet]
        //public IActionResult GetQuestions()
        //{

        //    return null;

        //}


        // Select all Questions:
        // Returns json array of Question objects 
        [HttpGet]
        public IActionResult GetAllQuestions()
        {
            var questions = _context.Question.FromSql("select * from Question").ToArray();
            var length = questions.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    questionId = questions.ElementAt(i).QuestionId,
                    quizId = questions.ElementAt(i).QuizId,
                    questionText = questions.ElementAt(i).QuestionText
                };

            }

            return Ok(returnArray);

        }


        // Select a Question:
        // Returns json array of one Question objects 
        [HttpGet("{id}")]
        public IActionResult GetQuestion(int id)
        {
            var question = _context.Question.FromSql($"select * from Question where QuestionId = {id}").ToArray();
            var length = question.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    questionId = question.ElementAt(i).QuestionId,
                    quizId = question.ElementAt(i).QuizId,
                    questionText = question.ElementAt(i).QuestionText
                };

            }

            return Ok(returnArray);

        }


        // Select all Questions associated with a Quiz:
        // Returns json array of Question objects 
        [HttpGet("/quiz-questions/{id}", Name = "Quiz-questions")]
        public IActionResult GetQuizQuestions(int id)
        {
            var questions = _context.Question.FromSql($"select * from Question where QuizId = {id}").ToArray();
            var length = questions.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    questionId = questions.ElementAt(i).QuestionId,
                    quizId = questions.ElementAt(i).QuizId,
                    questionText = questions.ElementAt(i).QuestionText
                };

            }

            return Ok(returnArray);

        }


        // POST api/Question
        [HttpPost]
        public IActionResult CreateNewQuestion([FromBody]Question newQuestion)
        {
            try
            {
                Console.WriteLine("called createQuestion()");
                var question = _questionService.CreateQuestion(newQuestion);

                return Ok(new
                {
                    questionId = question.QuestionId,
                    quizId = question.QuizId,
                    questionText = question.QuestionText
                });
            }
            catch (CustomException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        // Update a Question:
        [HttpPut("update-question")]
        public void UpdateQuestion([FromBody]Question updateQuestion)
        {

            var question = _context.Question.FromSql($"select * from Question where QuestionId = {updateQuestion.QuestionId}").ToArray();

            question.ElementAt(0).QuizId = updateQuestion.QuizId; //?????????
            question.ElementAt(0).QuestionText = updateQuestion.QuestionText;

            _context.SaveChanges();

        }





        // DELETE a Question:
        // Only works if there are no answers, etc., accociated with the Question
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var topic = _context.Topic.FromSql($"delete from Question where QuestionId = {id}").ToArray();
        }






    }
}
