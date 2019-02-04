using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachingAppAPI.Entities;

namespace TeachingAppAPI.Models
{
    public class RefreshTest
    {
        public RefreshToken RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
