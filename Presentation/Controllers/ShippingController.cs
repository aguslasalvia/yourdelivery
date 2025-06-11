using System.Diagnostics;
using System.Text.Json;
using Core.Enums;
using DTO;
using DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Core.Entities;


namespace Presentation.Controllers
{
	public class ShippingController : Controller
	{
		public ShippingController() { }

		[HttpGet]
		public IActionResult NewShipping()
		{

			ViewData["Title"] = "New Shipping";
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;
			return View();
		}



		[HttpGet]
		public IActionResult Tracking(int? trackingNumber)
		{

			ViewData["Title"] = "Tracking";

			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;
			ShippingViewModelTracking model = new ShippingViewModelTracking { };

			try
			{
				if (trackingNumber == null)
				{
					throw new KeyNotFoundException("Tracking number is required.");
				}

				// Simulate fetching shipping details based on tracking number
				// In a real application, this would involve a service call to fetch data from a database or API
				model.Shipping = new ShippingDto
				{
					Id = trackingNumber.Value,
					Weight = 2.5f,
					Employee = new UserDto
					{
						Id = 1,
						Name = "John Doe",
						Email = "john.doe@yourdelivery.com",
						Phone = "123-456-7890",
						Role = Role.Employee
					},
					Client = new UserDto
					{
						Id = 2,
						Name = "Jane Smith",
						Email = "jane.smith@yourdelivery.com",
						Phone = "987-654-3210",
						Role = Role.Client
					},
					State = ShippingState.OnProcess
				};
			}
			catch (KeyNotFoundException ex)
			{
				model.Message = ex.Message;
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult List()
		{
			ViewData["Title"] = "Shippings";

			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;
			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			return View();
		}

		[HttpGet]
		public IActionResult Close(int id)
		{
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;
			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			return RedirectToAction("List");
		}



		[HttpGet]
		public IActionResult Open(int id)
		{
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;
			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;
			return RedirectToAction("List");
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
	}
}