using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class AnswerType
    {
        public AnswerType()
        {
            Answer = new HashSet<Answer>();
        }

        public int AnswerTypeId { get; set; }
        public string AnswerTypeDesc { get; set; }

        public ICollection<Answer> Answer { get; set; }
    }
}
