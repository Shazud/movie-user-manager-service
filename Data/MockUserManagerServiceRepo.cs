using System.Collections.Generic;
using MovieUserManagerService.Models;

namespace MovieUserManagerService.Data
{
    public class MockUserManagerServiceRepo : IUserManagerServiceRepo
    {
        public IEnumerable<User> GetAllUsers()
        {
            var users = new List<User>
            {
                new User{username="test1", name="Name1", firstname="FName1", password="password1"},
                new User{username="test2", name="Name2", firstname="FName2", password="password2"},
                new User{username="test3", name="Name3", firstname="FName3", password="password3"},
                new User{username="test4", name="Name4", firstname="FName4", password="password4"},
            };
            return users;
        }

        public User GetUserByUsername(string username)
        {
            return new User{username="test", name="Name", firstname="FName", password="password"};
        }
    }
}