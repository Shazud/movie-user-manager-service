using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MovieUserManagerService.Models;
using MovieUserManagerService.Settings;
using MovieUserManagerService.Utils;

namespace MovieUserManagerService.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly JwtSettings _jwt;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public UserAuthenticationService(JwtSettings jwt)
        {
            _jwt = jwt;
            _tokenHandler = new JwtSecurityTokenHandler();
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

            var token = _tokenHandler.CreateToken(securityTokenDescriptor);


            return _tokenHandler.WriteToken(token);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 14);
        }

        public string GetTokenClaimValue(string token, string claim)
        {
            try
            {
                var val = _tokenHandler.ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == claim).Value;
                return val;
            }
            catch
            {
                return string.Empty;
            }
        }

        public string GetToken(HttpContext httpContext)
        {
            var token =  httpContext.Request.Headers["authorization"];
            return ValidateToken(token);
        }

        public string ValidateToken(string token)
        {
            if(token == string.Empty)
            {
                return string.Empty;
            }
            try
            {
                _tokenHandler.ValidateToken(token, new TokenValidationParameters{
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwt.Secret)),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
            }
            catch
            {
                return string.Empty;
            }
            return token;
        }
    }
}