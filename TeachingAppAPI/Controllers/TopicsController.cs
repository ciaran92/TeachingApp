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
    public class TopicsController : Controller
    {
        /* This Contoller contains the following:
        - GetTopic(): Selects particular topic
        - GetAllTopics(): selects all available topics
        - GetCourseTopics: gets all topics for a particular course
        - CreateNewTopic(): creates a new coursetopic
        - UpdateTopic(): updates an existing topic
        - Delete(): deletes a topic (where a topic has no associated quizzes, etc.)
 
        */


        private TestDB_Phase2Context _context;
        private ITopicService _topicService;

        public TopicsController(TestDB_Phase2Context context, ITopicService topicService)
        {
            _context = context;
            _topicService = topicService;
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
        [HttpGet("{id}")]
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
        [HttpGet("/course-topics/{id}", Name = "Course-topics")]
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


        // POST api/Topic
        [HttpPost]
        public IActionResult CreateNewTopic([FromBody]Topic newTopic)
        {
            try
            {
                Console.WriteLine("called createTopic()");
                var topic = _topicService.CreateTopic(newTopic);

                return Ok(new
                {
                    courseId = topic.CourseId,
                    topicName = topic.TopicName,
                    topicDesc = topic.TopicDesc
                });
            }
            catch (CustomException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        // Update a Topic:
        [HttpPut("update-topic")]
        public void UpdateTopic([FromBody]Topic updateTopic)
        {

            var topic = _context.Topic.FromSql($"select * from Topic where TopicId = {updateTopic.TopicId}").ToArray();

            topic.ElementAt(0).TopicName = updateTopic.TopicName;
            topic.ElementAt(0).CourseId = updateTopic.CourseId;
            topic.ElementAt(0).TopicDesc = updateTopic.TopicDesc;

            _context.SaveChanges();

        }





        // DELETE a Topic:
        // Only works if there are no topics, etc., accociated with the Topic
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var topic = _context.Topic.FromSql($"delete from Topic where TopicId = {id}").ToArray();
        }

    }
}