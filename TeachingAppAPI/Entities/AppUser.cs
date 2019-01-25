using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Entities
{
    public partial class AppUser
    {
        public AppUser()
        {
            Enrolment = new HashSet<Enrolment>();
            RefreshToken = new HashSet<RefreshToken>();
        }

        public int AppUserId { get; set; }
        public int? AppUserTypeId { get; set; }
        public int? AppUserStatusId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordSalt { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool? AccountVerified { get; set; }
        public string VerificationCode { get; set; }
        public string Email { get; set; }

        public AppUserStatus AppUserStatus { get; set; }
        public AppUserType AppUserType { get; set; }
        public ICollection<Enrolment> Enrolment { get; set; }
        public ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
