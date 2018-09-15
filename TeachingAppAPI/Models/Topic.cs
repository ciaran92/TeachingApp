using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class Topic
    {
        public Topic()
        {
            Questions = new HashSet<Questions>();
        }

        public int TopicId { get; set; }
        public int? CourseId { get; set; }
        public string TopicName { get; set; }

        public Course Course { get; set; }
        public ICollection<Questions> Questions { get; set; }
    }
}
