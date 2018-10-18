using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class AppUserStatus
    {
        public AppUserStatus()
        {
            AppUser = new HashSet<AppUser>();
        }

        public int AppUserStatusId { get; set; }
        public string AppUserStatusDesc { get; set; }

        public ICollection<AppUser> AppUser { get; set; }
    }
}
