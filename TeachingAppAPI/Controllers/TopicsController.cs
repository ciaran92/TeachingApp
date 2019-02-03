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
using TeachingAppAPI.Models;
using AutoMapper;

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

        [HttpDelete("{id}")]
        public IActionResult DeleteTopic(int id)
        {
            var topic = _context.Topic.FirstOrDefault(t => t.TopicId == id);

            _context.Topic.Remove(topic);
            _context.SaveChanges();
            return NoContent();
        }


        // Update a Topic:
        [HttpPut("{id}")]
        public void UpdateTopic(int id, [FromBody]Topic topic)
        {
            var topicFromDatabase = _context.Topic.FirstOrDefault(t => t.TopicId == id);

            topicFromDatabase.TopicName = topic.TopicName;
            topicFromDatabase.TopicDesc = topic.TopicDesc;
            _context.Topic.Update(topicFromDatabase);
            _context.SaveChanges();

        }

        [HttpGet("get-topic-by-id/{id}")]
        public IActionResult GetTopicById(int id)
        {
            Console.WriteLine("called");
            var topic = _context.Topic.Include(t => t.Lesson).AsEnumerable().FirstOrDefault(t => t.TopicId == id); //LINQ statement to get topic and all its lessons
            Console.WriteLine(topic);
            var topicToReturn = Mapper.Map<TopicDto>(topic);
            return Ok(topicToReturn);
        }

        // Method to display list of all topics for a given course
        [HttpGet("get-topics/{id}")]
        public IActionResult GetTopics(int id)
        {
            var param = new SqlParameter("@CourseId", id);
            var query = _context.Topic.FromSql($"select * from Topic where CourseId = @CourseId order by TopicOrder asc", param).ToArray();
            var topics = Mapper.Map<IEnumerable<TopicsListDto>>(query);
            return Ok(topics);

        }

    }
}