using System;
using System.Collections.Generic;
using System.Linq;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Models;
using TeachingAppAPI.Data;


namespace TeachingAppAPI.Services
{
    public class CourseService : ICourseService
    {

        private TestDB_Phase2Context _context;

        public CourseService(TestDB_Phase2Context context)
        {
            _context = context;
        }

        public Course CreateCourse(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.CourseName))
            {
                throw new CustomException("Course name is required");
            }

            if (DoesCourseAlreadyExist(course.CourseName))
            {
                throw new CustomException("Course name already exists");
            }
            else
            {
                _context.Course.Add(course);
                _context.SaveChanges();
                Console.WriteLine("created course");
                return course;
            }

        }

        private Boolean DoesCourseAlreadyExist(string coursename)
        {
            if (_context.Course.Any(x => x.CourseName == coursename))
            {
                return true;
            }
            return false;
        }


    }
}
