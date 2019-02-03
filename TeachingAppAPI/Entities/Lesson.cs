using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class Lesson
    {
        public int LessonId { get; set; }
        public int? TopicId { get; set; }
        public string LessonName { get; set; }
        public string LessonArticle { get; set; }
        public string LessonVideoUrl { get; set; }
        public int? LessonOrder { get; set; }
        public string VideoFileName { get; set; }
        public string S3VideoFileName { get; set; }

        public Topic Topic { get; set; }
    }
}
