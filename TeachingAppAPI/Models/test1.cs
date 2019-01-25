using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachingAppAPI.Entities;

namespace TeachingAppAPI.Models
{
    public partial class test1
    {

        public int EnrolmentId { get; set; }
        public int QuizInstanceId { get; set; }
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public int? AnswerTypeId { get; set; }
        public string AnswerType_Desc { get; set; }

        public Enrolment Enrolment { get; set; }
        public QuizInstance QuizInstance { get; set; }
        public Quiz Quiz { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public AnswerType AnswerType { get; set; }

    }
}

