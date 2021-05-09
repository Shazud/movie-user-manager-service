using Microsoft.AspNetCore.Http;
using MovieUserManagerService.Models;

namespace MovieUserManagerService.Services
{
    public interface IUserAuthenticationService
    {
        string CreateToken(User user);
        string HashPassword(string password);
        bool ComparePasswords(string a, string b);
        public string GetTokenClaimValue(string token, string claim);
        public string GetToken(HttpContext httpContext);
        public string ValidateToken(string token);
    }
}