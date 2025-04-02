using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions options):DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Shipping> Shippings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder){
            
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            
            modelBuilder.Entity<Agency>().HasKey(agency => agency.Id);

            modelBuilder.Entity<Shipping>().HasKey(shipping => shipping.Id);
            
            modelBuilder.Entity<Shipping>()
                .HasOne(s => s.Employee)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey("EmployeeId");
                
                

            modelBuilder.Entity<Shipping>()
                .HasOne(s => s.Client)
                .WithMany()
                .HasForeignKey("ClientId")
                .OnDelete(DeleteBehavior.NoAction);      

            base.OnModelCreating(modelBuilder);
        }

    }
}