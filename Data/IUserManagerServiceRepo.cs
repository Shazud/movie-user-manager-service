using System.Collections.Generic;
using MovieUserManagerService.Models;

namespace MovieUserManagerService.Data
{
    public interface IUserManagerServiceRepo
    {
        bool SaveChanges();
        IEnumerable<User> GetAllUsers();
        User GetUserByUsername(string username);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}