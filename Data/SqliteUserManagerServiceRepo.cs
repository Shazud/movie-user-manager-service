using System;
using System.Collections.Generic;
using System.Linq;
using MovieUserManagerService.Models;

namespace MovieUserManagerService.Data
{
    public class SqliteUserManagerServiceRepo : IUserManagerServiceRepo
    {
        private readonly UserManagerServiceContext _context;

        public SqliteUserManagerServiceRepo(UserManagerServiceContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            if(user == null){
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(p => p.username == username);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateUser(User user)
        {

        }
    }
}