using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachingAppAPI.Entities;

namespace TeachingAppAPI.Services
{
    public interface IUserService
    {

        AppUser CreateUser(AppUser user, string password);
        //Boolean DoesUserAlreadyExist(string username);
        bool Authenticate(AppUser user, string username, string password);
    }
}
