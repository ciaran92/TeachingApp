using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class QuizType
    {
        public QuizType()
        {
            Quiz = new HashSet<Quiz>();
        }

        public int QuizTypeId { get; set; }
        public string QuizTypeDesc { get; set; }

        public ICollection<Quiz> Quiz { get; set; }
    }
}
