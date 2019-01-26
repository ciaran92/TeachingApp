using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachingAppAPI.Models
{
    public class TopicsListDto
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int TopicOrder { get; set; }
    }
}
