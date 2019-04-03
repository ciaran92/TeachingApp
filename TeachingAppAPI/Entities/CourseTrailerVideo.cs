using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class CourseTrailerVideo
    {
        public int TrailerVideoId { get; set; }
        public int? CourseId { get; set; }
        public string TrailerVideoUrl { get; set; }
        public string S3fileName { get; set; }
        public string LocalFileName { get; set; }

        public Course Course { get; set; }
    }
}
