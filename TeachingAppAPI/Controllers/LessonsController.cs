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
        private static string accessKey = "AKIAJXGSGKXO5IK4YDIA";
        private static String accessSecret = "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";
        private static String bucket = "scrotums/videos";

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

        // POST api/Lesson
        [HttpPost]
        public IActionResult CreateNewLesson([FromBody]LessonForCreationDto lesson)
        {
            Console.WriteLine("CreateNewLesson Called");
            if(lesson == null)
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


        /*
         * 
         */
        [HttpPut("update-name/{id}")]
        public IActionResult UpdateLessonName(int id, [FromBody] LessonForCreationDto lesson)
        {
            Console.WriteLine("called put lesson: " + id);
            if(lesson == null)
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


        /*
         *  Method used to update/create the video for the lesson
         */
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
                DeleteOldImageFromBucket(lessonFromDB.S3VideoFileName);
                var res = await UploadImage(lesson.LessonVideoUrl);
                lessonFromDB.LessonVideoUrl = res[0] + res[1];
                lessonFromDB.S3VideoFileName = res[1];
            }
            else
            {
                Console.WriteLine("No video for this lesson foun in database... creating new requested video");
                var res = await UploadImage(lesson.LessonVideoUrl);
                lessonFromDB.LessonVideoUrl = res[0] + res[1];
                lessonFromDB.S3VideoFileName = res[1];
            }

            lessonFromDB.VideoFileName = lesson.VideoFileName;
            _context.Lesson.Update(lessonFromDB);
            _context.SaveChanges();
            Console.WriteLine("should have updated");
            return NoContent();
        }

        public async Task<string[]> UploadImage(String base64str)
        {
            
            var fileName = "";
            try
            {
                byte[] bytes = Convert.FromBase64String(base64str);
                var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.APSoutheast2);
                fileName = Guid.NewGuid().ToString();
                PutObjectResponse response = null;

                using (var stream = new MemoryStream(bytes))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucket,
                        Key = fileName,
                        InputStream = stream,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    response = await client.PutObjectAsync(request);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ha" + ex);
            }
            Console.WriteLine("path: " + fileName);
            string[] returnStr = { "https://s3-ap-southeast-2.amazonaws.com/scrotums/", fileName };
            return returnStr;

        }


        /**
         * Method to delete old Image in S3 bucket
         * @param filename: name of the file to be deleted
         */ 
        public async void DeleteOldImageFromBucket(string filename)
        {
            Console.WriteLine("file to delete: " + filename);
            DeleteObjectResponse response = null;
            var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.APSoutheast2);
            DeleteObjectRequest request = new DeleteObjectRequest();
            request.BucketName = bucket;
            request.Key = filename;
            response = await client.DeleteObjectAsync(request);
            Console.WriteLine("File deleted");
        }

    }
}