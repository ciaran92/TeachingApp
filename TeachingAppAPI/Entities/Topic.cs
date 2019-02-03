using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class Topic
    {
        public Topic()
        {
            Lesson = new HashSet<Lesson>();
            Quiz = new HashSet<Quiz>();
        }

        public int TopicId { get; set; }
        public int? CourseId { get; set; }
        public string TopicName { get; set; }
        public string TopicDesc { get; set; }
        public int? TopicOrder { get; set; }

        public Course Course { get; set; }
        public ICollection<Lesson> Lesson { get; set; }
        public ICollection<Quiz> Quiz { get; set; }
    }
}
