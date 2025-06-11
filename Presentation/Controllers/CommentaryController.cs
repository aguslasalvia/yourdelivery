using System.Text.Json;
using Core.Enums;
using DTO;
using DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;


namespace Presentation.Controllers
{
	public class CommentaryController : Controller
	{
		public CommentaryController() { }

		[HttpGet]
		public IActionResult Add(int id)
		{

			return View();
		}

		

		private IActionResult CheckUserIsLogged()
		{
			string userJson = HttpContext.Session.GetString("User");
			if (string.IsNullOrEmpty(userJson))
			{
				LoginViewModel loginModel = new LoginViewModel
				{
					Message = "You must be logged in to access this page"
				};
				return View("~/Views/Auth/Login.cshtml", loginModel);
			}
			return null;
		}

		private IActionResult ClientCantAccess()
		{
			string userJson = HttpContext.Session.GetString("User");
			if (!string.IsNullOrEmpty(userJson))
			{
				Role role = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User")).Role;
				if (role == Role.Client)
					return RedirectToAction("Index", "Error", new { error = "You lack of privileges to enter this page" });
			}
			return null;
		}

		private IActionResult EmployeeCantAccess()
		{
			string userJson = HttpContext.Session.GetString("User");
			if (!string.IsNullOrEmpty(userJson))
			{
				Role role = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User")).Role;
				if (role == Role.Employee)
					return RedirectToAction("Index", "Error", new { error = "You lack of privileges to enter this page" });
			}
			return null;
		}
	}
}