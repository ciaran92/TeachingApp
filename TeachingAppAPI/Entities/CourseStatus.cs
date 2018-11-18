using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class CourseStatus
    {
        public CourseStatus()
        {
            Course = new HashSet<Course>();
        }

        public int CourseStatusId { get; set; }
        public string CourseStatusDesc { get; set; }

        public ICollection<Course> Course { get; set; }
    }
}
