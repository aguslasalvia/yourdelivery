using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Agency> Agencies { get; set; }
		public DbSet<Shipping> Shippings { get; set; }
		public DbSet<Commentary> Commentaries { get; set; }
		public AppDbContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasKey(user => user.Id);
			modelBuilder.Entity<Agency>().HasKey(agency => agency.Id);
			modelBuilder.Entity<Shipping>().HasKey(shipping => shipping.Id);
			modelBuilder.Entity<Commentary>().HasKey(comment => comment.Id);

			modelBuilder.Entity<Shipping>()
				.HasOne(s => s.Employee)
				.WithMany()
				.OnDelete(DeleteBehavior.NoAction)
				.HasForeignKey(s => s.EmployeeID);

			modelBuilder.Entity<Shipping>()
				.HasOne(s => s.Client)
				.WithMany()
				.OnDelete(DeleteBehavior.NoAction)
				.HasForeignKey(s => s.ClientID);

			modelBuilder.Entity<CommonShipping>()
				.HasOne(s => s.PickUpAgency)
				.WithMany()
				.OnDelete(DeleteBehavior.NoAction)
				.HasForeignKey(s => s.PickupId);

			modelBuilder.Entity<Shipping>()
				.HasDiscriminator<string>("Discriminator")
				.HasValue<Shipping>("Base")
				.HasValue<CommonShipping>("Common")
				.HasValue<UrgentShipping>("Urgent");

			modelBuilder.Entity<Commentary>()
				.HasOne(c => c.User)
				.WithMany()
				.OnDelete(DeleteBehavior.NoAction)
				.HasForeignKey(c => c.UserId);

			modelBuilder.Entity<Commentary>()
				.HasOne(c => c.Shipping)
				.WithMany()
				.OnDelete(DeleteBehavior.NoAction)
				.HasForeignKey(c => c.ShippingId);

			base.OnModelCreating(modelBuilder);
		}

	}
}