using MovieUserManagerService.Models;

namespace MovieUserManagerService.Services
{
    public interface IAuthenticationService
    {
        AuthenticationResult Register(string username, string password);
    }
}