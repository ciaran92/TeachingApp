using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TeachingAppAPI.Data;
using TeachingAppAPI.Entities;

namespace TeachingAppAPI.Services
{
    public class TokenGenerator : ITokenGenerator
    {

        private TestDB_Phase2Context _context;
        private readonly AppSettings _appSettings;
        private byte[] _secretKey;

        public TokenGenerator(TestDB_Phase2Context context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _secretKey = Encoding.ASCII.GetBytes(_appSettings.Secret);
        }

        public string CreateAccessToken(AppUser user)
        {
            var key = new SymmetricSecurityKey(_secretKey);
            var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.AppUserId.ToString()),
                    new Claim("AccountConfirmed", user.AccountVerified.ToString()),
                    new Claim("username", user.UserName)
                };

            var token = new JwtSecurityToken
                        (
                        issuer: "http://localhost:52459",
                        audience: "http://localhost:52459",
                        expires: DateTime.Now.AddMinutes(1),
                        claims: claims,
                        signingCredentials: signInCred
                        );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public RefreshToken CreateRefreshToken(int userId)
        {
            var refreshToken = _context.RefreshToken.SingleOrDefault(t => t.AppUserId == userId);
            if (refreshToken != null)
            {
                _context.RefreshToken.Remove(refreshToken);
                _context.SaveChanges();
            }
            var newRefreshToken = new RefreshToken
            {
                AppUserId = userId,
                Token = Guid.NewGuid().ToString(),
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.AddMinutes(5)
            };

            _context.RefreshToken.Add(newRefreshToken);
            _context.SaveChanges();

            return newRefreshToken;
        }
    }
}
