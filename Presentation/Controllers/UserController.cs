using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Core.Enums;
using DTO;
using DTO.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
	public class UserController : Controller
	{
		public UserController() { }

		[HttpGet]
		public IActionResult Dashboard()
		{
			ViewData["Title"] = "Dashboard";
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;
			return View();
		}

		[HttpGet]
		public IActionResult Users()
		{

			ViewData["Title"] = "Users";

			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;

			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			IActionResult employeeCheck = EmployeeCantAccess();
			if (employeeCheck != null) return employeeCheck;

			try
			{
				UsersViewModelUsers model = new UsersViewModelUsers
				{
					Users = new List<UserListDto>()
				{
					new UserListDto
					{
						Id = 1,
						Name = "John Doe",
						Email = "john.doe@yourdelivery.com",
						Phone = "123-456-7890",
					},
					new UserListDto
					{
						Id = 2,
						Name = "Jane Smith",
						Email = "jane.smith@yourdelivery.com",
						Phone = "987-654-3210",
					},
					new UserListDto
					{
						Id = 3,
						Name = "Alice Johnson",
						Email = "alice.johnson@yourdelivery.com",
						Phone = "555-555-5555",
					},
				}
				};
				return View(model);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "Error", new { error = e.Message });
			}
		}


		[HttpGet]
		public IActionResult Profile()
		{

			ViewData["Title"] = "Profile";

			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;

			UsersViewModelProfile model = new UsersViewModelProfile
			{
				User = JsonSerializer.Deserialize<UserProfileDto>(HttpContext.Session.GetString("User"))
			};

			return View(model);
		}




		[HttpGet]
		public IActionResult NewUser()
		{

			ViewData["Title"] = "New User";

			UsersViewModelNewUser model = new UsersViewModelNewUser { };

			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;

			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			IActionResult employeeCheck = EmployeeCantAccess();
			if (employeeCheck != null) return employeeCheck;

			return View(model);
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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