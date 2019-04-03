using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachingAppAPI.Models
{
    public class CoursesListDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseThumbnailUrl { get; set; }
        public int? CourseStatusId { get; set; }
    }
}
