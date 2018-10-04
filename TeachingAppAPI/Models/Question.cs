using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
            QuizInstanceAnswer = new HashSet<QuizInstanceAnswer>();
        }

        public int QuestionId { get; set; }
        public int? QuizId { get; set; }
        public string QuestionText { get; set; }

        public Quiz Quiz { get; set; }
        public ICollection<Answer> Answer { get; set; }
        public ICollection<QuizInstanceAnswer> QuizInstanceAnswer { get; set; }
    }
}
