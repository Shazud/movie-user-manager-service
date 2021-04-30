using System.Collections.Generic;
using MovieUserManagerService.Models;

namespace MovieUserManagerService.Data
{
    public interface IUserManagementServiceRepo
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByUsername(string username);
    }
}