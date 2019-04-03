using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class Thumbnail
    {
        public int ThumbnailId { get; set; }
        public int? CourseId { get; set; }
        public string ThumbnailUrl { get; set; }
        public string S3fileName { get; set; }
        public string LocalFilename { get; set; }

        public Course Course { get; set; }
    }
}
