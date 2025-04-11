using Application.Interfaces;
using Application.UseCases;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Core.Interfaces;
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

            builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            builder.Services.AddScoped(typeof(IShippingRepository), typeof(ShippingRepository));
            builder.Services.AddScoped(typeof(IUserLoginCase), typeof(UserLoginCase));
            builder.Services.AddScoped(typeof(IUserGetAllCase), typeof(UserGetAllCase));
            builder.Services.AddScoped(typeof(IUserGetByEmail), typeof(UserGetByEmail));
            builder.Services.AddScoped(typeof(IUserDelete), typeof(UserDelete));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            
            app.UseSession();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                try
                {
                    dbContext.Database.EnsureCreated(); // Verifica si la BD existe, si no la crea
                    Console.WriteLine("✅ Database Connected Successfully ✅");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error Connecting to Databases: {ex.Message}");
                }
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
