using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class Enrolment
    {
        public Enrolment()
        {
            QuizInstance = new HashSet<QuizInstance>();
        }

        public int EnrolmentId { get; set; }
        public int? AppUserId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? EnrolmentDateTimeStart { get; set; }

        public AppUser AppUser { get; set; }
        public Course Course { get; set; }
        public ICollection<QuizInstance> QuizInstance { get; set; }
    }
}
