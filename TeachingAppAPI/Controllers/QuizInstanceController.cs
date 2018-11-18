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
    public class QuizInstanceController : ControllerBase
    {
        /* This Controller contains the following:
        - GetQuizInstance(): Selects particular topic
        - GetAllEnrolmentQuizInstances: gets all QuizInstances for a particular Enrolment
        - CreateNewQuizInstance(): creates a new QuizInstance
        - UpdateQuizInstance(): updates an existing QuizInstance
        - Delete(): deletes a QuizInstance (where a QuizInstance has no associated QuizInstanceAnswers, etc.)
 
        */


        private TestDB_Phase2Context _context;
        private IQuizInstanceService _quizInstanceService;

        public QuizInstanceController(TestDB_Phase2Context context, IQuizInstanceService quizInstanceService)
        {
            _context = context;
            _quizInstanceService = quizInstanceService;
        }



        //Select all Quiz Questions and answers
        // Returns json array of test1 objects 
        [HttpGet("{id}")]
        public IActionResult GetQuizQuestionsAndAnswers3(int id)
        {
            // Calling the selectQuizInstanceQuestionAnswers Stored Procedure:
            var param = new SqlParameter("@QuizInstanceID", id);
            var questionsAndAnswers = _context.Test1.FromSql($"selectQuizInstanceQuestionAnswers @QuizInstanceID", param).ToList();
            //Console.WriteLine("reponse array length: " + questionsAndAnswers.Length); 
            var length = questionsAndAnswers.Count;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                //Console.WriteLine("reponse: "+ i + " " + questionsAndAnswers.ElementAt(i).QuestionText);
                returnArray[i] = new
                {
                    enrolmentId = questionsAndAnswers.ElementAt(i).EnrolmentId, // remove late, not required in response?
                    quizInstanceId = questionsAndAnswers.ElementAt(i).QuizInstanceId, 
                    quizId = questionsAndAnswers.ElementAt(i).QuizId, // remove late, not required in response?
                    questionId = questionsAndAnswers.ElementAt(i).QuestionId, 
                    questionText = questionsAndAnswers.ElementAt(i).QuestionText,
                    answerID = questionsAndAnswers.ElementAt(i).AnswerId,
                    answerText = questionsAndAnswers.ElementAt(i).AnswerText,
                    answerTypeID = questionsAndAnswers.ElementAt(i).AnswerTypeId, // remove late, not required in response?
                    answerTypeDesc = questionsAndAnswers.ElementAt(i).AnswerType_Desc

                };

            }

            return Ok(returnArray);

        }


        



        // ?????????? Remove ??????????
        // Select all QuizInstances: 
        // Returns json array of QuizInstance objects 
        //[HttpGet]
        //public IActionResult GetAllQuizInstances()
        //{
        //    var quizInstances = _context.QuizInstance.FromSql("select * from QuizInstance").ToArray();
        //    var length = quizInstances.Length;
        //    var returnArray = new object[length];

        //    for (int i = 0; i < length; i++)
        //    {
        //        returnArray[i] = new
        //        {
        //            quizInstanceId = quizInstances.ElementAt(i).QuizInstanceId,
        //            enrolmentId = quizInstances.ElementAt(i).EnrolmentId,
        //            quizId = quizInstances.ElementAt(i).QuizId,
        //            quizUserStatusId = quizInstances.ElementAt(i).QuizUserStatusId,
        //            quizDateTimeStart = quizInstances.ElementAt(i).QuizDateTimeStart
        //        };

        //    }

        //    return Ok(returnArray);

        //}


        // Select a QuizInstance:
        // Returns json array of QuizInstance objects 
        //[HttpGet("{id}")]
        //public IActionResult GetQuizInstance(int id)
        //{
        //    var quizInstance = _context.QuizInstance.FromSql($"select * from QuizInstance where EnrolmentId = {id}").ToArray();
        //    var length = quizInstance.Length;
        //    var returnArray = new object[length];

        //    for (int i = 0; i < length; i++)
        //    {
        //        returnArray[i] = new
        //        {
        //            quizInstanceId = quizInstance.ElementAt(i).QuizInstanceId,
        //            enrolmentId = quizInstance.ElementAt(i).EnrolmentId,
        //            quizId = quizInstance.ElementAt(i).QuizId,
        //            quizUserStatusId = quizInstance.ElementAt(i).QuizUserStatusId,
        //            quizDateTimeStart = quizInstance.ElementAt(i).QuizDateTimeStart
        //        };

        //    }

        //    return Ok(returnArray);

        //}


        // Select all QuizInstances associated with an Enrolment:
        // Returns json array of QuizInstance objects 
        // [HttpGet("/enrolment-quizInstances/{id}", Name = "Enrolment-quizInstances")]
        [HttpGet("enrolment-quizInstances/{id}")]
        public IActionResult GetEnrolmentQuizInstances(int id)
        {
            var quizInstances = _context.QuizInstance.FromSql($"select * from QuizInstance where EnrolmentId = {id}").ToArray();
            var length = quizInstances.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    quizInstanceId = quizInstances.ElementAt(i).QuizInstanceId,
                    enrolmentId = quizInstances.ElementAt(i).EnrolmentId,
                    quizId = quizInstances.ElementAt(i).QuizId,
                    quizUserStatusId = quizInstances.ElementAt(i).QuizUserStatusId,
                    quizDateTimeStart = quizInstances.ElementAt(i).QuizDateTimeStart
                };

            }

            return Ok(returnArray);

        }


        // POST api/QuizInstance
        [HttpPost]
        public IActionResult CreateNewQuizInstance([FromBody]QuizInstance newQuizInstance)
        {
            try
            {
                Console.WriteLine("called createQuizInstance()");
                var quizInstance = _quizInstanceService.CreateQuizInstance(newQuizInstance);

                return Ok(new
                {
                    enrolmentId = quizInstance.EnrolmentId,
                    quizId = quizInstance.QuizId,
                    quizUserStatusId = quizInstance.QuizUserStatusId,
                    quizDateTimeStart = quizInstance.QuizDateTimeStart
                });
            }
            catch (CustomException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        // Update a QuizInstance:
        [HttpPut("update-quizinstance")]
        public void updateQuizInstance([FromBody]QuizInstance updateQuizInstance)
        {

            var quizInstance = _context.QuizInstance.FromSql($"select * from QuizInstance where QuizInstanceId = {updateQuizInstance.QuizInstanceId}").ToArray();
            
            quizInstance.ElementAt(0).QuizId = updateQuizInstance.QuizId;
            quizInstance.ElementAt(0).QuizUserStatusId = updateQuizInstance.QuizUserStatusId;
            quizInstance.ElementAt(0).QuizDateTimeStart = updateQuizInstance.QuizDateTimeStart;

            _context.SaveChanges();

        }





        // DELETE a QuizInstance:
        // Only works if there are no QuizInstanceAnswers, etc., accociated with the ToQuizInstancepic
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var quizInstance = _context.QuizInstance.FromSql($"delete from QuizInstance where QuizInstanceId = {id}").ToArray();
        }
    }
}