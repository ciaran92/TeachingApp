using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class QuizUserStatus
    {
        public QuizUserStatus()
        {
            QuizInstance = new HashSet<QuizInstance>();
        }

        public int QuizUserStatusId { get; set; }
        public string QuizUserStatusDesc { get; set; }

        public ICollection<QuizInstance> QuizInstance { get; set; }
    }
}
