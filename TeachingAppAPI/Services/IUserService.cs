using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachingAppAPI.Models;

namespace TeachingAppAPI.Services
{
    public interface IUserService
    {

        AppUser CreateUser(AppUser user, string password);
        //Boolean DoesUserAlreadyExist(string username);
        AppUser Authenticate(string username, string password);
    }
}
