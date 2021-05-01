using Microsoft.EntityFrameworkCore;
using MovieUserManagerService.Models;

namespace MovieUserManagerService.Data
{
    public class UserManagerServiceContext : DbContext
    {
        public UserManagerServiceContext(DbContextOptions<UserManagerServiceContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; }
    }
}