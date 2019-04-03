using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingAppAPI.Data;
using TeachingAppAPI.Entities;
using TeachingAppAPI.Models;

namespace TeachingAppAPI.Controllers
{

    [Route("api/[controller]")]
    public class LessonsController : Controller
    {
        private TestDB_Phase2Context _context;
        

        public LessonsController(TestDB_Phase2Context context)
        {
            _context = context;
        }

        [HttpGet("{id}", Name = "GetLessonsForTopic")]
        public IActionResult GetLessons(int id)
        {
            var param = new SqlParameter("@TopicId", id);
            var lessons = _context.Lesson.FromSql($"select * from Lesson where TopicId = @TopicId order by LessonOrder asc", param).ToList();
            var lessonsList = Mapper.Map<IEnumerable<LessonDto>>(lessons);
            return Ok(lessonsList);
        }

        

        


        

    }
}