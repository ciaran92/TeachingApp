using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class Questions
    {
        public int QuestionId { get; set; }
        public int? TopicId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public bool? Answered { get; set; }
        public int QuestionPriority { get; set; }

        public Topic Topic { get; set; }
    }
}
