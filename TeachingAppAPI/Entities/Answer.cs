using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class Answer
    {
        public Answer()
        {
            QuizInstanceAnswer = new HashSet<QuizInstanceAnswer>();
        }

        public int AnswerId { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerTypeId { get; set; }
        public string AnswerText { get; set; }

        public AnswerType AnswerType { get; set; }
        public Question Question { get; set; }
        public ICollection<QuizInstanceAnswer> QuizInstanceAnswer { get; set; }
    }
}
