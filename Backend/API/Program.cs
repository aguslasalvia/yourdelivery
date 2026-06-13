using System.Text;
using System.Threading.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.UseCases;
using Application.UseCases.Shipping;
using Infrastructure.Repositories;
using Infrastructure.Persistence;
using Core.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using API.Configuration;
using Microsoft.Extensions.Options;

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

        // Register JWT settings
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

        // Setup JWT auth
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        var secretKeyBytes = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ValidateIssuer = false,              
                ValidateAudience = false
            };
        });

        // Configurar la autorización
        builder.Services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
        
        // Add services:
        // Repositories
        builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        builder.Services.AddScoped(typeof(IShippingRepository), typeof(ShippingRepository));

        // USE CASES
        // User
        builder.Services.AddScoped(typeof(IUserLoginCase), typeof(UserLoginCase));
        builder.Services.AddScoped(typeof(IUserGetByEmail), typeof(UserGetByEmail));
        builder.Services.AddScoped(typeof(IUserChangePassword), typeof(UserChangePassword));

        // Shipping
        builder.Services.AddScoped(typeof(IShippingGetByClientId), typeof(ShippingGetByClientId));
        builder.Services.AddScoped(typeof(IShippingGetById), typeof(ShippingGetById));
        builder.Services.AddScoped(typeof(IShippingGetByDates), typeof(ShippingGetByDates));
        builder.Services.AddScoped(typeof(IShippingGetByCommentary), typeof(ShippingGetByCommentary));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter JWT with Bearer into field. Example: \"Bearer {token}\"",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        var app = builder.Build();
        
        // Try to connect to database but don't fail if it doesn't work
        try
        {
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();
            Console.WriteLine("✅ Database Connected Successfully ✅");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Warning: Could not connect to database: {ex.Message}");
            Console.WriteLine("⚠️ Application will continue without database connection");
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
