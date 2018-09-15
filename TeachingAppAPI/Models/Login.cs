using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class Login
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
