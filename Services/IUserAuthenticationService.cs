using MovieUserManagerService.Models;

namespace MovieUserManagerService.Services
{
    public interface IUserAuthenticationService
    {
        string CreateToken(User user);
    }
}