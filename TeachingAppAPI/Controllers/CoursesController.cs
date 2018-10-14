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
    public class CoursesController : ControllerBase
    {
        private TestDB_Phase2Context _context;
        private ICourseService _courseService;


        public CoursesController(TestDB_Phase2Context context, ICourseService courseService) 
        {
            _context = context;
            _courseService = courseService;
        }

        

        [HttpGet("{id}")]
        public IActionResult GetCourse(int id)
        {
            // Calling the selectCourse Stored Procedure:
            var param = new SqlParameter("@CourseId", id);
            var courses = _context.Course.FromSql($"selectCourse @CourseId", param).ToArray();

            // Alternative :
            //var courses = _context.Course.FromSql($"select * from Course where CourseId = {id}").ToArray();
            
            if(courses.Length <= 0)
            {
                return BadRequest("Fuck off");
            }

            //Console.WriteLine("course: " + courses);
            var length = courses.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    courseId = courses.ElementAt(i).CourseId,
                    courseName = courses.ElementAt(i).CourseName,
                    courseStatusId = courses.ElementAt(i).CourseStatusId,
                    courseDateTimeStart = courses.ElementAt(i).CourseDateTimeStart,
                    courseDuration = courses.ElementAt(i).CourseDuration,
                    courseDesc = courses.ElementAt(i).CourseDesc
                };

            }

            return Ok(returnArray);
               
        }



        // Select all Courses:
        // Returns json array of Course objects 
        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = _context.Course.FromSql("select * from Course").ToArray();
            var length = courses.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    courseId = courses.ElementAt(i).CourseId,
                    courseName = courses.ElementAt(i).CourseName,
                    courseStatusId = courses.ElementAt(i).CourseStatusId,
                    courseDateTimeStart = courses.ElementAt(i).CourseDateTimeStart,
                    courseDuration = courses.ElementAt(i).CourseDuration,
                    courseDesc = courses.ElementAt(i).CourseDesc
                };

            }

            return Ok(returnArray);

        }



        // POST api/AppUser
        [HttpPost]
        public IActionResult CreateNewCourse([FromBody]Course newCourse)
        {
            try
            {
                Console.WriteLine("called createCourse()");
                var course = _courseService.CreateCourse(newCourse);

                return Ok(new
                {
                    courseId = course.CourseId,
                    courseName = course.CourseName,
                    courseStatusId = course.CourseStatusId,
                    courseDateTimeStart = course.CourseDateTimeStart,
                    courseDuration = course.CourseDuration,
                    courseDesc = course.CourseDesc,
                    courseStatus = course.CourseStatus
                });
            }
            catch (CustomException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }



        // Update a Course:
        // 
        [HttpPut("{id}")]
        public void UpdateCourse([FromBody]Course updateCourse)
        {

            var course = _context.Course.FromSql($"select * from Course where CourseId = {updateCourse.CourseId}").ToArray();

            course.ElementAt(0).CourseName = updateCourse.CourseName;
            course.ElementAt(0).CourseStatusId = updateCourse.CourseStatusId;
            course.ElementAt(0).CourseDateTimeStart = updateCourse.CourseDateTimeStart;
            course.ElementAt(0).CourseDuration = updateCourse.CourseDuration;
            course.ElementAt(0).CourseDesc = updateCourse.CourseDesc;

            _context.SaveChanges();

        }


        // DELETE a Course - not really useful at this point. Stored procedure required:
        // Only works if there are no topics, etc., accociated with the course
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var courses = _context.Course.FromSql($"delete from Course where CourseId = {id}").ToArray();
        }







    }
}