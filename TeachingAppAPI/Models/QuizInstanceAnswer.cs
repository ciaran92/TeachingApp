using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class QuizInstanceAnswer
    {
        public int QuizInstanceAnswerId { get; set; }
        public int? QuizInstanceId { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public string AppUserAnswer { get; set; }
        public DateTime? AppUserAnswerDateTime { get; set; }

        public Answer Answer { get; set; }
        public Question Question { get; set; }
        public QuizInstance QuizInstance { get; set; }
    }
}
