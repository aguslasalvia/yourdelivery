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

namespace API;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

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

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();


		app.MapControllers();

		app.Run();
	}
}