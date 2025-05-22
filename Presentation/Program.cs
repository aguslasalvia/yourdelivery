using Application.Interfaces;
using Application.Interfaces.Agency;
using Application.UseCases;
using Application.UseCases.Agency;
using Application.UseCases.Shipping;
using Application.UseCases.Commentary;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Core.Interfaces;
using Core.Entities;
namespace Presentation
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddSession();

			// Connection to SQL
			builder.Services.AddDbContext<AppDbContext>(
				options => options.UseSqlServer(
					builder.Configuration.GetConnectionString("StringConnection")
				)
			);

			// Repositories
			builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
			builder.Services.AddScoped(typeof(IShippingRepository), typeof(ShippingRepository));
			builder.Services.AddScoped(typeof(IAgencyRepository), typeof(AgencyRepository));
			builder.Services.AddScoped(typeof(ICommentaryRepository), typeof(CommentaryRepository));

			// USE CASES
			// User
			builder.Services.AddScoped(typeof(IUserLoginCase), typeof(UserLoginCase));
			builder.Services.AddScoped(typeof(IUserGetAllCase), typeof(UserGetAllCase));
			builder.Services.AddScoped(typeof(IUserGetByEmail), typeof(UserGetByEmail));
			builder.Services.AddScoped(typeof(IUserDelete), typeof(UserDelete));
			builder.Services.AddScoped(typeof(IUserUpdate), typeof(UserUpdate));
			builder.Services.AddScoped(typeof(IUserCreate), typeof(UserCreate));
			builder.Services.AddScoped(typeof(IUserGetAllClients), typeof(UserGetAllClients));

			// Shipping
			builder.Services.AddScoped(typeof(IShippingCreate), typeof(ShippingCreate));
			builder.Services.AddScoped(typeof(IShippingGetByClientId), typeof(ShippingGetByClientId));
			builder.Services.AddScoped(typeof(IShippingGetAllOnProcess), typeof(ShippingGetAllOnProcess));
			builder.Services.AddScoped(typeof(IShippingGetById), typeof(ShippingGetById));
			builder.Services.AddScoped(typeof(IShippingClose), typeof(ShippingClose));
			builder.Services.AddScoped(typeof(IShippingOnProcess), typeof(ShippingOnProcess));

			// Agencies
			builder.Services.AddScoped(typeof(IAgencyGetAll), typeof(AgencyGetAll));
			builder.Services.AddScoped(typeof(IAgencyShippingGetAll), typeof(AgencyShippingGetAll));
			builder.Services.AddScoped(typeof(IAgencyGetById), typeof(AgencyGetById));

			// Commentaries
			builder.Services.AddScoped(typeof(ICommentaryCreate), typeof(CommentaryCreate));
			builder.Services.AddScoped(typeof(ICommentaryGetAll), typeof(CommentaryGetAll));

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			app.UseSession();

			// DB connection && existence check
			// app.Services.CreateScope().ServiceProvider.GetService<AppDbContext>().Database.EnsureCreated()

			var scope = app.Services.CreateScope();

			var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			try
			{
				dbContext.Database.EnsureCreated();
				Console.WriteLine("✅ Database Connected Successfully ✅");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"❌ Error Connecting to Databases: {ex.Message}");
			}


			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
