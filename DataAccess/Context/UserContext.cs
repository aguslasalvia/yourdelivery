using BusinessLogic.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class UserContext ( DbContextOptions options ) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating ( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<User>().HasKey(user => user.Email);

            base.OnModelCreating(modelBuilder);

        }



    }
}
