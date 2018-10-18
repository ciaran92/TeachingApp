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

namespace TeachingAppAPI.Services
{
    [Route("api/[controller]")]
    public class EnrolmentController : ControllerBase
    {
        /* This Contoller contains the following:
        - GetEnrolment(): Selects particular enrolment
        - GetEnrolments(): selects all enrolments fot a course
        - CreateNewEnrolment(): creates a new enrolment
        - UpdateEnrolment(): updates an existing enrolment
        - Delete(): deletes an enrolment (where an enrolment has no associated Quizzes, etc.)
        - GetUserEnrolments(): selects all courses where a user is enrolled
        */

        private TestDB_Phase2Context _context;
        private IEnrolmentService _enrolmentService;

        public EnrolmentController(TestDB_Phase2Context context, IEnrolmentService enrolmentService)
        {
            _context = context;
            _enrolmentService = enrolmentService;
        }


        //[HttpGet("{id}")]
        //public IActionResult GetEnrolment(int id)
        //{
        //    // Calling the selectEnrolment Stored Procedure:
        //    var param = new SqlParameter("@EnrolmentId", id);
        //    var enrolment = _context.Enrolment.FromSql($"selectEnrolment @EnrolmentId", param).ToArray();

        //    if (enrolment.Length <= 0)
        //    {
        //        return BadRequest("There is no such enrolment.");
        //    }

           
        //    var length = enrolment.Length;
        //    var returnArray = new object[length];

        //    for (int i = 0; i < length; i++)
        //    {
        //        returnArray[i] = new
        //        {
        //            enrolmentID = enrolment.ElementAt(i).EnrolmentId,
        //            appUserId = enrolment.ElementAt(i).AppUserId,
        //            courseId = enrolment.ElementAt(i).CourseId,
        //            enrolmentDateTimeStart = enrolment.ElementAt(i).EnrolmentDateTimeStart
        //        };

        //    }

        //    return Ok(returnArray);

        //}


        // Select all Enrolments for a Course: 
        [HttpGet("{id}")]
        public IActionResult GetCourseEnrolments(int id)
        {
            var enrolment = _context.Enrolment.FromSql($"select * from Enrolment where CourseId = {id}").ToArray();

            if (enrolment.Length <= 0)
            {
                return BadRequest("There are no enrolments in this course.");
            }


            var length = enrolment.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    enrolmentID = enrolment.ElementAt(i).EnrolmentId,
                    appUserId = enrolment.ElementAt(i).AppUserId,
                    courseId = enrolment.ElementAt(i).CourseId,
                    enrolmentDateTimeStart = enrolment.ElementAt(i).EnrolmentDateTimeStart
                };

            }

            return Ok(returnArray);

        }



        // POST api/Course
        [HttpPost]
        public IActionResult CreateNewEnrolment([FromBody]Enrolment newEnrolment)
        {
            try
            {
                Console.WriteLine("called createEnrolment()");
                var enrolment = _enrolmentService.CreateEnrolment(newEnrolment);

                return Ok(new
                {
                    appUserID = enrolment.AppUserId,
                    courseID = enrolment.CourseId,
                    enrolment = enrolment.EnrolmentDateTimeStart
                });
            }
            catch (CustomException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }



        // DELETE an Enrollment - not really useful at this point. Stored procedure required:
        // Only works if there are no quiz instances, etc., associated with the course
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var courses = _context.Course.FromSql($"delete from Enrolment where CourseId = {id}").ToArray();
        }

    }
}