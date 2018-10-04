using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class QuizInstance
    {
        public QuizInstance()
        {
            QuizInstanceAnswer = new HashSet<QuizInstanceAnswer>();
        }

        public int QuizInstanceId { get; set; }
        public int? EnrolmentId { get; set; }
        public int? QuizId { get; set; }
        public int? QuizUserStatusId { get; set; }
        public DateTime? QuizDateTimeStart { get; set; }

        public Enrolment Enrolment { get; set; }
        public Quiz Quiz { get; set; }
        public QuizUserStatus QuizUserStatus { get; set; }
        public ICollection<QuizInstanceAnswer> QuizInstanceAnswer { get; set; }
    }
}
