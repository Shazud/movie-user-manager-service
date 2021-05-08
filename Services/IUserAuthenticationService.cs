using MovieUserManagerService.Models;

namespace MovieUserManagerService.Services
{
    public interface IUserAuthenticationService
    {
        string CreateToken(User user);
        string HashPassword(string password);
        bool ComparePasswords(string a, string b);
    }
}