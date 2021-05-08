using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MovieUserManagerService.Models;
using MovieUserManagerService.Settings;

namespace MovieUserManagerService.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly JwtSettings _jwt;

        public UserAuthenticationService(JwtSettings jwt)
        {
            _jwt = jwt;
        }

        public bool ComparePasswords(string pass, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(pass, hash);
        }

        public string CreateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_jwt.Secret);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []{
                    new Claim(JwtRegisteredClaimNames.Sub, user.username),
                    new Claim("id", user.username),
                    new Claim("role", user.role)
                }),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(securityTokenDescriptor);


            return tokenHandler.WriteToken(token);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 14);
        }
    }
}