using System.Diagnostics;
using Application.Interfaces;
using System.Text.Json;
using Application.Interfaces.Agency;
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
		private readonly IAgencyShippingGetAll _agencyShippingGetAll;
		private readonly IAgencyGetById _agencyGetById;
		private readonly IUserGetAllClients _userGetAllClients;
		private readonly IUserGetByEmail _userGetByEmail;
		private readonly IShippingCreate _shippingCreate;
		private readonly IShippingGetByClientId _shippingGetByClientId;
		private readonly IShippingGetById _shippingGetById;
		private readonly IShippingClose _shippingClose;
		private readonly IShippingOnProcess _shippingOnProcess;

		private readonly IShippingGetAllOnProcess _shippingGetAllOnProcess;
		public ShippingController(
			IAgencyShippingGetAll agencyShippingGetAll,
			IAgencyGetById agencyGetById,
			IUserGetAllClients userGetAllClients,
			IUserGetByEmail userGetByEmail,
			IShippingCreate shippingCreate,
			IShippingGetByClientId shippingGetByClientId,
			IShippingGetAllOnProcess shippingGetAllOnProcess,
			IShippingGetById shippingGetById,
			IShippingClose shippingClose
			// ICommentaryGetAll commentaryGetAll
			)
		{
			_userGetByEmail = userGetByEmail;
			_shippingCreate = shippingCreate;
			_agencyShippingGetAll = agencyShippingGetAll;
			_agencyGetById = agencyGetById;
			_userGetAllClients = userGetAllClients;
			_shippingGetByClientId = shippingGetByClientId;
			_shippingGetById = shippingGetById;
			_shippingClose = shippingClose;
			_shippingGetAllOnProcess = shippingGetAllOnProcess;
			// _commentaryGetAll = commentaryGetAll;
		}

		[HttpGet]
		public IActionResult NewShipping()
		{
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;
			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			ShippingViewModelNew model = new ShippingViewModelNew { };
			List<AgencyShippingDto> agencies = _agencyShippingGetAll.Execute().ToList();
			List<UserListDto> clients = _userGetAllClients.Execute().ToList();
			model.Agencies = agencies;
			model.Clients = clients;
			return View(model);
		}

		[HttpPost]
		public IActionResult NewShipping(CreateShippingDto createShippingDto)
		{
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;
			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			ShippingViewModelNew model = new ShippingViewModelNew
			{
				Agencies = _agencyShippingGetAll.Execute().ToList(),
				Clients = _userGetAllClients.Execute().ToList()
			};

			try
			{
				UserDto employee = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User"));
				
				switch (createShippingDto.Type)
				{
					case "Common":
						CreateCommonShippingDto commonShipping = new CreateCommonShippingDto()
						{
							Weight = createShippingDto.Weight,
							EmployeeId = employee.Id,
							ClientId = _userGetByEmail.Execute(createShippingDto.ClientEmail).Id,
							State = ShippingState.OnProcess,
							PickupId = createShippingDto.PickupId
						};
						_shippingCreate.ExecuteCommon(commonShipping);
						break;

					case "Urgent":

						CreateUrgentShippingDto urgentShipping = new CreateUrgentShippingDto()
						{
							Weight = createShippingDto.Weight,
							Employee = employee.Id,
							Client = _userGetByEmail.Execute(createShippingDto.ClientEmail).Id,
							State = ShippingState.OnProcess,
							Address = createShippingDto.Address
						};

						_shippingCreate.ExecuteUrgent(urgentShipping);
						break;
					default:
						throw new Exception("Unknown shipping type");
				}
				return RedirectToAction("NewShipping");
			}
			catch (Exception e)
			{
				model.Message = e.Message;
				return View(model);
			}
		}

		[HttpGet]
		public IActionResult Tracking(int? trackingNumber)
		{
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;
			ShippingViewModelTracking model = new ShippingViewModelTracking { };

			try
			{
				if (trackingNumber != null)
				{
					model.Shipping = _shippingGetById.Execute((int)trackingNumber);
				}
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
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;
			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			UserDto user = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User"));

			ShippingViewModelList model = new ShippingViewModelList
			{
				Shippings = user.Role == Role.Client
					? _shippingGetByClientId.Execute(user.Id).ToList()
					: _shippingGetAllOnProcess.Execute().ToList(),
			};

			return View(model);
		}

		[HttpGet]
		public IActionResult Close(int id)
		{
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;
			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			_shippingClose.Execute(id);
			return RedirectToAction("List");
		}



		[HttpGet]
		public IActionResult Open(int id)
		{
			IActionResult loginCheck = CheckUserIsLogged();
			if (loginCheck != null) return loginCheck;
			IActionResult clientCheck = ClientCantAccess();
			if (clientCheck != null) return clientCheck;

			_shippingOnProcess.Execute(id);
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