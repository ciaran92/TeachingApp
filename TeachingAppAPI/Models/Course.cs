using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class Course
    {
        public Course()
        {
            Enrolment = new HashSet<Enrolment>();
            Topic = new HashSet<Topic>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int? CourseStatusId { get; set; }
        public DateTime? CourseDateTimeStart { get; set; }
        public int? CourseDuration { get; set; }
        public string CourseDesc { get; set; }

        public CourseStatus CourseStatus { get; set; }
        public ICollection<Enrolment> Enrolment { get; set; }
        public ICollection<Topic> Topic { get; set; }
    }
}
