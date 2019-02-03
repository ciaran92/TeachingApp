using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachingAppAPI.Entities;

namespace TeachingAppAPI.Models
{
    public class TopicDto
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }       
        public string TopicDesc { get; set; }
        public int? TopicOrder { get; set; }
        public ICollection<LessonDto> Lesson { get; set; }
        //= new List<LessonDto>();
    }
}
