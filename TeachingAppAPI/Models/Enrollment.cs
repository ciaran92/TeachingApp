using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class Enrollment
    {
        public int EnrolmentId { get; set; }
        public int? UserId { get; set; }
        public int? CourseId { get; set; }

        public Course Course { get; set; }
        public Users User { get; set; }
    }
}
