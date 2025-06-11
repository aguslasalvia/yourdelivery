using System.Diagnostics;
using Core.Enums;
using Core.Interfaces;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using DTO.Users;
using System.Security.Authentication;


namespace Presentation.Controllers
{
	public class AuthController : Controller
	{
		public AuthController() { }

		[HttpGet]
		public IActionResult Login()
		{
			HttpContext.Session.Clear();
			return View(new LoginViewModel());
		}

		[HttpPost]
		public IActionResult SubmitLogin(LoginViewModel model)
		{
			try
			{
				if (model.UserLogin.Email == "admin@gmail.com" && model.UserLogin.Password == "admin")
				{
					HttpContext.Session.SetString("User", JsonSerializer.Serialize(new UserProfileDto
					{
						Id = 1,
						Name = "Admin",
						Email = "admin",
						Password = "admin",
						Role = Role.Administrator,
						Gender = Gender.Male,
						Birth = new DateOnly(1990, 1, 1),
						Phone = "123-456-7890",
					}));
					HttpContext.Session.SetString("Role", Role.Administrator.ToString());
					return RedirectToAction("Dashboard", "User");
				}
				if (model.UserLogin.Email == "employee@gmail.com" && model.UserLogin.Password == "employee")
				{
					HttpContext.Session.SetString("User", JsonSerializer.Serialize(new UserProfileDto
					{
						Id = 2,
						Name = "User",
						Email = "user",
						Role = Role.Employee,
						Gender = Gender.Female,
						Phone = "987-654-3210",
						Password = "employee",
						Birth = new DateOnly(1995, 5, 15),
						

					}));
					HttpContext.Session.SetString("Role", Role.Employee.ToString());
					return RedirectToAction("Dashboard", "User");
				}
			}
			catch (Exception e)
			{
				model.Message = "An error occurred while processing your request. Please try again later.";
			}

			return View("Login", model);
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}