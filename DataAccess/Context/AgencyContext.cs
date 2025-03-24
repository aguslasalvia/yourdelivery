using BusinessLogic.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class AgencyContext ( DbContextOptions options ) : DbContext(options)
    {
        public DbSet<Agency> Agencies { get; set; }

        protected override void OnModelCreating ( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<Agency>().HasKey(agency => agency.Name);

            base.OnModelCreating(modelBuilder);

        }



    }
}
