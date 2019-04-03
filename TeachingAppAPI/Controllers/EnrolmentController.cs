using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingAppAPI.Data;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        [Authorize(Policy = "RegisteredUser")]
        [HttpPost]
        public IActionResult CreateNewEnrolment([FromBody]Enrolment newEnrolment)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int userIdClaim = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);

            Console.WriteLine("EnrolmentId: " + newEnrolment.EnrolmentId);
            if (_context.Enrolment.Any(x => x.EnrolmentId == newEnrolment.EnrolmentId))
            {
               BadRequest();
            }
            newEnrolment.AppUserId = userIdClaim;
            newEnrolment.EnrolmentDateTimeStart = DateTime.UtcNow;
            _context.Enrolment.Add(newEnrolment);
            _context.SaveChanges();
            return Ok();
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