using System;
using System.Collections.Generic;
using System.Linq;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Entities;
using TeachingAppAPI.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace TeachingAppAPI.Services
{
    public class UserService : IUserService
    {
        private TestDB_Phase2Context _context;

        public UserService(TestDB_Phase2Context context)
        {
            _context = context;
        }

        public bool Authenticate(AppUser user, string username, string password)
        {

            if (ValidatePassword(password, user.PasswordSalt, user.UserPassword))
            {
                return true;
            }
            return false;
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
                // Hash users password before storing in database
                //var HashedPassword = HashPassword(password);
                //user.UserPassword = HashedPassword;
                string salt = CreateSalt();
                user.PasswordSalt = salt;
                user.UserPassword = HashPassword(password, salt);
                var verificationCode = GenerateCode(6);
                user.VerificationCode = verificationCode;
                user.AccountVerified = true;
                _context.AppUser.Add(user);
                _context.SaveChanges();
                Console.WriteLine("created user");
                return user;
            }
            
        }

        private string HashPassword(string password, string salt)
        {
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedPassword;
        }

        private string CreateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }

        private bool ValidatePassword(string password, string salt, string hashedPwdFromDB)
        {
            if (HashPassword(password, salt) == hashedPwdFromDB)
            {
                return true;
            }
            return false;
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
