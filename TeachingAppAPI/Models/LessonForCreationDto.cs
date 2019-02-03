using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachingAppAPI.Models
{
    public class LessonForCreationDto
    {
        public int? TopicId { get; set; }
        public string LessonName { get; set; }
        public int? LessonOrder { get; set; }
        public string LessonArticle { get; set; }
        public string LessonVideoUrl { get; set; }
        public string VideoFileName { get; set; }
        public string S3VideoFileName { get; set; }
    }
}
