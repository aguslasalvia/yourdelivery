using Microsoft.EntityFrameworkCore;
// using Infrastructure.Persistence;
namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //// Connection to SQL
            // builder.Services.AddDbContext<DbContext, AppDbContext>(
            //     options => options.UseSqlServer(
            //         builder.Configuration.GetConnectionString("StringConnection")
            //         )
            //     );


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

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
