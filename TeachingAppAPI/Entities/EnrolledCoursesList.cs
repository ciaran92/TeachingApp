using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class EnrolledCoursesList
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public Course Course { get; set; }
    }
}
