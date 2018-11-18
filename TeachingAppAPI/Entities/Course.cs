using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
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
        public DateTime? DateCreated { get; set; }
        public int? CourseDuration { get; set; }
        public string CourseThumbnailUrl { get; set; }
        public string Subtitle { get; set; }
        public string CourseDescription { get; set; }

        public CourseStatus CourseStatus { get; set; }
        public ICollection<Enrolment> Enrolment { get; set; }
        public ICollection<Topic> Topic { get; set; }
    }
}
