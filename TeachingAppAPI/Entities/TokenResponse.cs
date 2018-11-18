using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachingAppAPI.Entities
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime TokenExpiration { get; set; }
    }
}
