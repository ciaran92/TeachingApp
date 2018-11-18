using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class AppUserType
    {
        public AppUserType()
        {
            AppUser = new HashSet<AppUser>();
        }

        public int AppUserTypeId { get; set; }
        public string AppUserTypeDesc { get; set; }

        public ICollection<AppUser> AppUser { get; set; }
    }
}
