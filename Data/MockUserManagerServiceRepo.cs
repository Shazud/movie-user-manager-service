using System.Collections.Generic;
using MovieUserManagerService.Models;

namespace MovieUserManagerService.Data
{
    public class MockUserManagerServiceRepo : IUserManagerServiceRepo
    {
        public void CreateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = new List<User>
            {
                new User{username="test1", lastname="Name1", firstname="FName1", password="password1"},
                new User{username="test2", lastname="Name2", firstname="FName2", password="password2"},
                new User{username="test3", lastname="Name3", firstname="FName3", password="password3"},
                new User{username="test4", lastname="Name4", firstname="FName4", password="password4"},
            };
            return users;
        }

        public User GetUserByUsername(string username)
        {
            return new User{username="test", lastname="Name", firstname="FName", password="password"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}