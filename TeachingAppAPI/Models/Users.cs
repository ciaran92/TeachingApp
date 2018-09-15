using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class Users
    {
        public Users()
        {
            Enrollment = new HashSet<Enrollment>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }

        public ICollection<Enrollment> Enrollment { get; set; }
    }
}
