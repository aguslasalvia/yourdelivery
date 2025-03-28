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
            
            modelBuilder.Entity<User>().HasKey(user => user.Email);
            
            modelBuilder.Entity<Agency>().HasKey(agency => agency.Name);
            
            
            modelBuilder.Entity<Shipping>()
                .HasOne(s => s.Employee)
                .WithMany()
                .HasForeignKey("EmployeeEmail")
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Shipping>()
                .HasOne(s => s.Client)
                .WithMany()
                .HasForeignKey("ClientEmail")
                .OnDelete(DeleteBehavior.Cascade);      

            base.OnModelCreating(modelBuilder);
        }

    }
}