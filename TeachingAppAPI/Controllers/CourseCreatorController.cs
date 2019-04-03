using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingAppAPI.Data;
using TeachingAppAPI.Entities;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Models;
using TeachingAppAPI.Services;

namespace TeachingAppAPI.Controllers
{
    /**
     * All CRUD opperations required to create a Course are handled in this controller,
     * that includes any actions required to create, update and delete topics/lessons
     */

    [Produces("application/json")]
    [Route("api/creator")]
    public class CourseCreatorController : Controller
    {
        private TestDB_Phase2Context _context;
        private IFileUploadService _uploadService;

        public CourseCreatorController(TestDB_Phase2Context context, IFileUploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }

        // GET Request Methods

        [Authorize(Policy = "RegisteredUser")]
        [HttpGet("get-courses")]
        public IActionResult GetCourses()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var param = new SqlParameter("@CreatorAppUserId", Convert.ToInt32(userIdClaim));
            var courses = _context.Course.FromSql($"select * from Course where CreatorAppUserId = @CreatorAppUserId", param).ToList();

            var courseListToReturn = Mapper.Map<IEnumerable<CoursesListDto>>(courses);
            return Ok(courseListToReturn);
        }


        // Method to display list of all topics for a given course
        [HttpGet("get-topics/{id}")]
        public IActionResult GetTopics(int id)
        {
            var param = new SqlParameter("@CourseId", id);
            var query = _context.Topic.FromSql($"select * from Topic where CourseId = @CourseId order by TopicOrder asc", param).ToList();
            var topics = Mapper.Map<IEnumerable<TopicsListDto>>(query);
            return Ok(topics);
        }


        // Get all required information about the selected topic
        [HttpGet("get-topic-by-id/{id}")]
        public IActionResult GetTopicById(int id)
        {
            Console.WriteLine("called");
            var topic = _context.Topic.Include(t => t.Lesson).AsEnumerable().FirstOrDefault(t => t.TopicId == id); //LINQ statement to get topic and all its lessons
            Console.WriteLine(topic);
            var topicToReturn = Mapper.Map<TopicDto>(topic);
            return Ok(topicToReturn);
        }
        // POST Request Methods

        /**
         * Method used to create course
         * Called when a user first creates and enters a name for their new course
         * This method also creates a default topic and lesson
         */
        [Authorize(Policy = "RegisteredUser")]
        [HttpPost("new-course")]
        public IActionResult CreateNewCourse([FromBody]Course course)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            Console.WriteLine("uid: " + userIdClaim);
            course.CourseStatusId = 1; //TODO: change this to 0 if course is in process of being created and has not been submitted for review
            Topic topic = new Topic() { CourseId = course.CourseId, TopicName = "Example Topic", TopicDesc = "Example Description", TopicOrder = 1 };
            Lesson defaultLesson = new Lesson() { TopicId = course.CourseId, LessonName = "Default Lesson", LessonOrder = 1, VideoFileName = "No File Selected" };
            topic.Lesson.Add(defaultLesson);
            course.CreatorAppUserId = Convert.ToInt32(userIdClaim); // TODO: check if the user exists first
            course.Topic.Add(topic);
            _context.Course.Add(course); //TODO: create a CourseForCreationDto
            _context.SaveChanges();
            return Ok(course); //TODO: return CourseDto
        }


        // Method used to create a new Topic
        [HttpPost("new-topic")]
        [HttpPost]
        public IActionResult CreateNewTopic([FromBody]Topic newTopic)// TODO: this should have the course id the topic is being created for in the url and should be checking that topic exists
        {
            try
            {
                Console.WriteLine("called createTopic()");
                _context.Add(newTopic);
                _context.SaveChanges();
                TopicsListDto createdTopic = new TopicsListDto();

                createdTopic.TopicId = newTopic.TopicId;
                createdTopic.TopicName = newTopic.TopicName;
                createdTopic.TopicOrder = newTopic.TopicOrder;

                return Ok(createdTopic);
            }
            catch (CustomException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        // Method used to create a new Lesson
        [HttpPost("new-lesson")]
        [HttpPost]
        public IActionResult CreateNewLesson([FromBody]LessonForCreationDto lesson)
        {
            Console.WriteLine("CreateNewLesson Called");
            if (lesson == null)
            {
                return BadRequest();
            }

            var lessonEntity = Mapper.Map<Lesson>(lesson);
            lessonEntity.VideoFileName = "No File Selected";
            _context.Add(lessonEntity);
            _context.SaveChanges();

            var lessonToReturn = Mapper.Map<LessonDto>(lessonEntity);
            return CreatedAtRoute("GetLessonsForTopic", new { id = lesson.TopicId }, lessonToReturn);
        }

        // UPDATE Request Methods

        // Method uset to update a Topics Name & Description
        [HttpPut("update-topic/{id}")]
        public void UpdateTopic(int id, [FromBody]Topic topic)
        {
            var topicFromDatabase = _context.Topic.FirstOrDefault(t => t.TopicId == id);

            topicFromDatabase.TopicName = topic.TopicName;
            topicFromDatabase.TopicDesc = topic.TopicDesc;
            _context.Topic.Update(topicFromDatabase);
            _context.SaveChanges();

        }


        // Method uset to update the name of a lesson
        [HttpPut("update-lesson-name/{id}")]
        public IActionResult UpdateLessonName(int id, [FromBody] LessonForCreationDto lesson)
        {
            Console.WriteLine("called put lesson: " + id);
            if (lesson == null)
            {
                Console.WriteLine("fuck");
                return BadRequest();
            }

            var lessonFromDB = _context.Lesson.FirstOrDefault(l => l.LessonId == id);

            var ls = Mapper.Map<LessonDto>(lessonFromDB);

            lessonFromDB.LessonName = lesson.LessonName;
            _context.Lesson.Update(lessonFromDB);
            _context.SaveChanges();
            Console.WriteLine("should have updated");
            return NoContent();
        }


        // Method to update the video for a lesson
        [HttpPut("update-video/{id}")]
        public async Task<IActionResult> UpdateLessonVideo(int id, [FromBody] LessonForCreationDto lesson)
        {
            Console.WriteLine("called put lesson: " + id);
            if (lesson == null)
            {
                Console.WriteLine("fuck");
                return BadRequest();
            }

            var lessonFromDB = _context.Lesson.FirstOrDefault(l => l.LessonId == id);

            // Checks to see if the lesson has already got a video set in the db
            // If it does it first deletes the vdeo in the S# bucket then creates the new updated one
            if (lessonFromDB.LessonVideoUrl != null)
            {
                Console.WriteLine("Lesson video already exists in database... updating old video");
                _uploadService.DeleteOldImageFromBucket(lessonFromDB.S3videoFileName);
                var res = await _uploadService.UploadImage(lesson.LessonVideoUrl);
                lessonFromDB.LessonVideoUrl = res[0] + res[1];
                lessonFromDB.S3videoFileName = res[1];
            }
            else
            {
                Console.WriteLine("No video for this lesson foun in database... creating new requested video");
                var res = await _uploadService.UploadImage(lesson.LessonVideoUrl);
                lessonFromDB.LessonVideoUrl = res[0] + res[1];
                lessonFromDB.S3videoFileName = res[1];
            }

            lessonFromDB.VideoFileName = lesson.VideoFileName;
            _context.Lesson.Update(lessonFromDB);
            _context.SaveChanges();
            Console.WriteLine("should have updated");
            return NoContent();
        }
        // DELETE Request Methods

        [HttpDelete("delete-topic/{id}")]
        public IActionResult DeleteTopic(int id)
        {
            var topic = _context.Topic.FirstOrDefault(t => t.TopicId == id);
            var param = new SqlParameter("@TopicId", id);
            var lessons = _context.Lesson.FromSql($"select * from Lesson where TopicId = @TopicId", param).ToList();

            foreach(Lesson l in lessons)
            {
                if(l.S3videoFileName != null)
                {
                    _uploadService.DeleteOldImageFromBucket(l.S3videoFileName);
                }
            }

            _context.Lesson.RemoveRange(lessons);
            _context.Topic.Remove(topic);
            _context.SaveChanges();
            return NoContent();
        }
    }
}