using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingAppAPI.Data;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Entities;
using TeachingAppAPI.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using Amazon.S3;
using Amazon.S3.Model;
using TeachingAppAPI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TeachingAppAPI.Controllers
{
    /* This Contoller contains the following:
    - GetCourse(): Selects particular course
    - GetCourses(): selects all available courses
    - CreateNewCourse(): creates a new course
    - UpdateCourse(): updates an existing course
    - Delete(): deletes a course (where a course has no associated topics, etc.)

    - GetUserCourses(): selects all available courses ?? GetUserEnrolments(): selects all courses where a user is enrolled

         
    */

    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private TestDB_Phase2Context _context;
        private ICourseService _courseService;
        private IHostingEnvironment _hostingEnvironment;

        public CoursesController(TestDB_Phase2Context context, ICourseService courseService, IHostingEnvironment hostingEnvironment) 
        {
            _context = context;
            _courseService = courseService;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet("{id}")]
        public IActionResult GetCourse(int id)
        {
            // Calling the selectCourse Stored Procedure:
            var param = new SqlParameter("@CourseId", id);

            var courses = _context.Course.FromSql($"select * from Course where CourseId = @CourseId", param).ToArray();
            var topics = _context.Topic.FromSql($"select * from Topic where CourseId = @CourseId", param).ToArray();

            // Alternative :
            //var courses = _context.Course.FromSql($"select * from Course where CourseId = {id}").ToArray();
            
            if(courses.Length <= 0)
            {
                return BadRequest("No course exists.");
            }

            //Console.WriteLine("course: " + courses);
            var length = courses.Length;
            var returnArray = new object[length];
            var topicsArray = new object[topics.Length];

            for(int i = 0; i < topics.Length; i++)
            {
                topicsArray[i] = new
                {
                    topicId = topics.ElementAt(i).TopicId,
                    topicName = topics.ElementAt(i).TopicName
                };
            }

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    courseId = courses.ElementAt(i).CourseId,
                    courseName = courses.ElementAt(i).CourseName,
                    subtitle = courses.ElementAt(i).Subtitle,
                    topics = topicsArray,
                    courseStatusId = courses.ElementAt(i).CourseStatusId,
                    dateCreated = courses.ElementAt(i).DateCreated,
                    courseDuration = courses.ElementAt(i).CourseDuration,
                    courseDesc = courses.ElementAt(i).CourseDescription
                };

            }

            return Ok(returnArray);
               
        }



        // Select all Courses (no matter what their status:
        // Returns CoursesListDto object 

        [Authorize(Policy = "RegisteredUser")]
        [HttpGet]
        public IActionResult GetCourses()
        {
            try
            {
                var query = _context.Course.ToList();
                var courses = new List<CoursesListDto>();

                foreach (var course in query)
                {
                    courses.Add(new CoursesListDto()
                    {
                        CourseId = course.CourseId,
                        CourseName = course.CourseName,
                        CourseThumbnailUrl = course.CourseThumbnailUrl
                    });
                }
                return Ok(courses);
            }
            catch(Exception ex)
            {
                return Ok();
            }
            

            
        }

        [Authorize(Policy = "RegisteredUser")]
        [HttpGet("my-courses")]
        public IActionResult GetCoursesEnrolledIn()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            var userId = new SqlParameter("@UserId", userIdClaim);
            var myCourses = _context.EnrolledCoursesLists.FromSql($"ViewEnrolledList @UserId", userId).ToList();

            var length = myCourses.Count;
            var returnArray = new object[length];
            for(int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    courseId = myCourses.ElementAt(i).CourseId,
                    courseName = myCourses.ElementAt(i).CourseName
                };
            }
            return Ok(returnArray);
        }

        

        [HttpPost("add-topic")]
        public IActionResult CreateNewTopic([FromBody]Topic topic)
        {
            _context.Topic.Add(topic);
            _context.SaveChanges();
            return Ok(topic);
        }

        /*[HttpPost("uploadNGX"), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadNGXImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.APSoutheast2);
                byte[] fileBytes = new byte[file.Length];
                file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

                var fileName = Guid.NewGuid() + file.FileName;

                PutObjectResponse response = null;

                using (var stream = new MemoryStream(fileBytes))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucket,
                        Key = fileName,
                        InputStream = stream,
                        ContentType = file.ContentType,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    response = await client.PutObjectAsync(request);
                }
                string urls = "https://s3-ap-southeast-2.amazonaws.com/scrotums/" + fileName;
                return Ok(new
                {
                    url = urls
                });
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            
            return null;
        }*/

        [HttpPost("new-course")]
        public IActionResult CreateNewCourse([FromBody]Course course)
        {
            course.CourseStatusId = 1; //TODO: change this to 0 if course is in process of being created and has not been submitted for review
            
            _context.Course.Add(course);
            
            _context.SaveChanges();
            CreateDefaultTopic(course.CourseId);
            return Ok(course);
        }

        // Method to create default topic whenever a new course is created
        public IActionResult CreateDefaultTopic([FromQuery] int id)
        {
            Topic topic = new Topic() { CourseId = id, TopicName = "Example Topic", TopicDesc = "Example Description", TopicOrder = 1 };
            _context.Topic.Add(topic);
            _context.SaveChanges();
            CreateDefaultLesson(topic.TopicId);
            return null;
        }

        // Method to create default topic whenever a new course is created
        public IActionResult CreateDefaultLesson([FromQuery] int id)
        {
            Lesson defaultLesson = new Lesson() { TopicId = id, LessonName = "Default Lesson", LessonOrder = 1, VideoFileName = "No File Selected" };
            _context.Lesson.Add(defaultLesson);
            _context.SaveChanges();
            return null;
        }


        

        /*[HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.APSoutheast2);

                byte[] fileBytes = new byte[file.Length];
                file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

                var fileName = Guid.NewGuid() + file.FileName;

                PutObjectResponse response = null;

                using (var stream = new MemoryStream(fileBytes))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucket,
                        Key = fileName,
                        InputStream = stream,
                        ContentType = file.ContentType,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    response = await client.PutObjectAsync(request);
                }

                Course course = new Course
                {
                    CourseName = Request.Form.ElementAt(0).Value,
                    Subtitle = Request.Form.ElementAt(1).Value,
                    CourseDescription = Request.Form.ElementAt(2).Value,
                    CourseThumbnailUrl = fileName
                };

                _context.Course.Add(course);
                _context.SaveChanges();
                
                    /*string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine("gs://teachingapp-509be.appspot.com/images/", fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }
        }*/




    }
}