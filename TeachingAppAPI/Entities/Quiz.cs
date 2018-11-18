using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class Quiz
    {
        public Quiz()
        {
            Question = new HashSet<Question>();
            QuizInstance = new HashSet<QuizInstance>();
        }

        public int QuizId { get; set; }
        public int? TopicId { get; set; }
        public int? QuizTypeId { get; set; }
        public string QuizDesc { get; set; }

        public QuizType QuizType { get; set; }
        public Topic Topic { get; set; }
        public ICollection<Question> Question { get; set; }
        public ICollection<QuizInstance> QuizInstance { get; set; }
    }
}
