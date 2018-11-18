using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachingAppAPI.Models
{
    public class UploadFile
    {
        
        public string CourseName { get; set; }
        public string Subtitle { get; set; }
        public string CourseDescription { get; set; }
    }
}
