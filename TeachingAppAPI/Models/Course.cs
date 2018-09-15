using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class Course
    {
        public Course()
        {
            Enrollment = new HashSet<Enrollment>();
            Topic = new HashSet<Topic>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public ICollection<Enrollment> Enrollment { get; set; }
        public ICollection<Topic> Topic { get; set; }
    }
}
