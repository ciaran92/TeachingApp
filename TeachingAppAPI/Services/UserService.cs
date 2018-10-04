using System;
using System.Collections.Generic;
using System.Linq;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Models;
using TeachingAppAPI.Data;

namespace TeachingAppAPI.Services
{
    public class UserService : IUserService
    {
        private TestDB_Phase2Context _context;

        public UserService(TestDB_Phase2Context context)
        {
            _context = context;
        }

        public AppUser Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public AppUser CreateUser(AppUser user, string password)
        {
            if (string.IsNullOrWhiteSpace(user.UserPassword))
            {
                throw new CustomException("Password is required");
            }

            if (DoesUserAlreadyExist(user.UserName))
            {
                throw new CustomException("Username already taken");
            }
            else
            {
                var verificationCode = GenerateCode(6);
                user.VerificationCode = verificationCode;
                user.AccountVerified = false;
                _context.AppUser.Add(user);
                _context.SaveChanges();
                Console.WriteLine("created user");
                return user;
            }
            
        }

        private Boolean DoesUserAlreadyExist(string username)
        {
            if (_context.AppUser.Any(x => x.UserName == username))
            {
                return true;
            }
            return false;
        }

        private string GenerateCode(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string code = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return code;
        }
    }
}
