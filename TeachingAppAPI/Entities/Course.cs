using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class Course
    {
        public Course()
        {
            CourseTrailerVideo = new HashSet<CourseTrailerVideo>();
            Enrolment = new HashSet<Enrolment>();
            Thumbnail = new HashSet<Thumbnail>();
            Topic = new HashSet<Topic>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int? CourseStatusId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Subtitle { get; set; }
        public string CourseDescription { get; set; }
        public int? CreatorAppUserId { get; set; }

        public CourseStatus CourseStatus { get; set; }
        public AppUser CreatorAppUser { get; set; }
        public ICollection<CourseTrailerVideo> CourseTrailerVideo { get; set; }
        public ICollection<Enrolment> Enrolment { get; set; }
        public ICollection<Thumbnail> Thumbnail { get; set; }
        public ICollection<Topic> Topic { get; set; }
    }
}
