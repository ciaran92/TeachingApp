using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
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
