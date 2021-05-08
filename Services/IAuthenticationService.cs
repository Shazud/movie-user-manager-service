using MovieUserManagerService.Models;

namespace MovieUserManagerService.Services
{
    public interface IAuthenticationService
    {
        string CreateToken(User user);
    }
}