using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Application.Interfaces;
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

		private readonly IUserGetAllCase _userGetAll;
		private readonly IUserGetByEmail _userGetByEmail;
		private readonly IUserDelete _userDelete;
		private readonly IUserUpdate _userUpdate;
		private readonly IUserCreate _userCreate;
		public UserController(
			IUserGetAllCase userGetAll,
			IUserGetByEmail userGetByEmail,
			IUserDelete userDelete,
			IUserUpdate userUpdate,
			IUserCreate userCreate)
		{
			_userGetByEmail = userGetByEmail;
			_userGetAll = userGetAll;
			_userDelete = userDelete;
			_userUpdate = userUpdate;
			_userCreate = userCreate;
		}

		[HttpGet]
		public IActionResult Dashboard()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Users()
		{
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;

			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			IActionResult employeeCheck = EmployeeCantAccess();
			if (employeeCheck != null) return employeeCheck;

			UsersViewModelUsers model = new UsersViewModelUsers { };
			try
			{
				List<UserListDto> users = _userGetAll.Execute().ToList();
				model.Users = users;
				return View(model);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "Error", new { error = e.Message });
			}
		}


		[HttpGet]
		public IActionResult Profile(string email)
		{
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;

			try
			{
				Role role = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User")).Role;
				UserProfileDto user = _userGetByEmail.Execute(email);

				if (Enum.GetName(role) != Role.Administrator.ToString() && user.Email != email)
					return RedirectToAction("Index", "Error", new { error = "You lack of privileges to enter this page" });

				UsersViewModelProfile model = new UsersViewModelProfile
				{
					User = user.Email == email ? user : _userGetByEmail.Execute(email),
					UserRole = role
				};

				ViewData["Title"] = "Profile";
				return View(model);
			}
			catch (Exception e)
			{
				UsersViewModelUsers model = new UsersViewModelUsers
				{
					Users = _userGetAll.Execute().ToList(),
					Message = e.Message
				};

				return View("Users", model);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(string email)
		{
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;

			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			IActionResult employeeCheck = EmployeeCantAccess();
			if (employeeCheck != null) return employeeCheck;

			try
			{
				if (string.IsNullOrEmpty(email))
					throw new ArgumentException("Email is missing.");
				UserDto employee = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User"));
				// UserDelete usecase uses UserProfileDTO
				UserProfileDto employeeDto = _userGetByEmail.Execute(employee.Email);

				if (employeeDto == null)
				{
					throw new Exception("Something went wrong");
				}

				// This is the user that will be deleted
				UserProfileDto userDto = _userGetByEmail.Execute(email);
				if (userDto == null)
					throw new Exception("User not found.");

				userDto.UpdatedByID = employee.Id;
				userDto.LastUpdated = DateTime.Now;
				_userDelete.Execute(userDto);

				return RedirectToAction("Users");
			}
			catch (Exception e)
			{
				UsersViewModelUsers model = new UsersViewModelUsers
				{
					Users = _userGetAll.Execute().ToList(),
					Message = e.Message
				};

				return View("Users", model);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(UserProfileDto editedUser)
		{
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;

			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			IActionResult employeeCheck = EmployeeCantAccess();
			if (employeeCheck != null) return employeeCheck;

			UserDto employee = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User"));

			editedUser.UpdatedByID = employee.Id;
			editedUser.LastUpdated = DateTime.Now;
			_userUpdate.Execute(editedUser);

			// if not admin, return to dashboard, if it is, return to users list
			return RedirectToAction("Users");
		}


		[HttpGet]
		public IActionResult NewUser()
		{
			UsersViewModelNewUser model = new UsersViewModelNewUser { };

			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;

			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			IActionResult employeeCheck = EmployeeCantAccess();
			if (employeeCheck != null) return employeeCheck;

			return View(model);
		}

		[HttpPost]
		public IActionResult SubmitNewUser(UserRegistrationDto user)
		{
			UsersViewModelNewUser model = new UsersViewModelNewUser { };

			try
			{
				IActionResult loginCheck = CheckUserIsLogged();
				if (loginCheck != null) return loginCheck;

				IActionResult clientCheck = ClientCantAccess();
				if (clientCheck != null) return clientCheck;

				IActionResult employeeCheck = EmployeeCantAccess();
				if (employeeCheck != null) return employeeCheck;

				UserDto employee = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User"));
				user.CreatedByID = employee.Id;
				user.LastUpdated = DateTime.Now;

				_userCreate.Execute(user);
				return RedirectToAction("Users");
			}
			catch (Exception e)
			{
				model.Message = e.Message;
			}

			return View("NewUser", model);
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