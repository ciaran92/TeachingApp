using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachingAppAPI.Entities;

namespace TeachingAppAPI.Services
{
    public interface ITokenGenerator
    {

        string CreateAccessToken(AppUser user);
        RefreshToken CreateRefreshToken(int userId);
    }
}
