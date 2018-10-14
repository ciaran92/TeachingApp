using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingAppAPI.Data;
using TeachingAppAPI.Models;

namespace TeachingAppAPI.Controllers
{
    [Route("api/[controller]")]
    public class TopicsController : Controller
    {
        private TestDB_Phase2Context _context;

        public TopicsController(TestDB_Phase2Context context)
        {
            _context = context;
        }


        // Select all Topics:
        // Returns json array of Topic objects 
        [HttpGet]
        public IActionResult GetAllTopics()
        {
            var topics = _context.Topic.FromSql("select * from Topic").ToArray();
            var length = topics.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    topicId = topics.ElementAt(i).TopicId,
                    courseId = topics.ElementAt(i).CourseId,
                    topicName = topics.ElementAt(i).TopicName,
                    topicDesc = topics.ElementAt(i).TopicDesc
                };

            }

            return Ok(returnArray);

        }

        
        // Select a Topic:
        // Returns json array of Topic objects 
        [HttpGet("/topic/{id}", Name = "Topic")]
        public IActionResult GetTopic(int id)
        {
            var topics = _context.Topic.FromSql($"select * from Topic where TopicId = {id}").ToArray();
            var length = topics.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    topicId = topics.ElementAt(i).TopicId,
                    courseId = topics.ElementAt(i).CourseId,
                    topicName = topics.ElementAt(i).TopicName,
                    topicDesc = topics.ElementAt(i).TopicDesc
                };

            }

            return Ok(returnArray);

        }


        // Select all Topics assoicated with a Course:
        // Returns json array of Topic objects 
        [HttpGet("/topics/{id}", Name = "Course_Topics")]
        public IActionResult GetCourseTopics(int id)
        {
            var topics = _context.Topic.FromSql($"select * from Topic where CourseId = {id}").ToArray();
            var length = topics.Length;
            var returnArray = new object[length];

            for (int i = 0; i < length; i++)
            {
                returnArray[i] = new
                {
                    topicId = topics.ElementAt(i).TopicId,
                    courseId = topics.ElementAt(i).CourseId,
                    topicName = topics.ElementAt(i).TopicName,
                    topicDesc = topics.ElementAt(i).TopicDesc
                };

            }

            return Ok(returnArray);

        }


        // Update a Course Topic:
        // 
        [HttpPut("update-topic")]
        public void UpdateTopic([FromBody]Topic topic)
        {

            var courseTopic = _context.Topic.FromSql($"select * from Topic where CourseID = {topic.CourseId} and TopicId = {topic.TopicId}").ToArray();
            //var courses = _context.Course.Where(x => x.CourseId == id);

            //courseTopic.ElementAt(0).TopicName = ;
            _context.SaveChanges();


        }

        // DELETE a Course:
        // Only works if there are no topics, etc., accociated with the course
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var courses = _context.Course.FromSql($"delete from Course where CourseId = {id}").ToArray();
        }




    }
}