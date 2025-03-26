using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions options):DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Shipping> Shippings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasKey(user => user.Email);
            modelBuilder.Entity<Agency>().HasKey(agency => agency.Name);
            modelBuilder.Entity<Shipping>().HasKey(shipping => shipping.ID);

            base.OnModelCreating(modelBuilder);
        }

    }
}
