using Application.Interfaces;
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
        private readonly IShippingGetById _shippingGetById;
        private readonly ICommentaryCreate _commentaryCreate;

        public CommentaryController(IShippingGetById shippingGetById, ICommentaryCreate commentaryCreate)
        {
            _shippingGetById = shippingGetById;
            _commentaryCreate = commentaryCreate;
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            IActionResult loginCheck = CheckUserIsLogged();
            if (loginCheck != null) return loginCheck;
            IActionResult clientCheck = ClientCantAccess();
            if (clientCheck != null) return clientCheck;
            
            ShippingDto shipping = _shippingGetById.Execute(id);

            CommentViewModelAdd model = new CommentViewModelAdd
            {
                Shipping = new ShippingCommentaryDto(shipping.Id, shipping.Employee.Id, shipping.Client.Id)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(int shippingID, string comment)
        {
            IActionResult loginCheck = CheckUserIsLogged();
            if (loginCheck != null) return loginCheck;
            IActionResult clientCheck = ClientCantAccess();
            if (clientCheck != null) return clientCheck;

            UserDto user = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User"));
            CommentaryCreateDto commentary = new CommentaryCreateDto
            {
                Text = comment,
                Date = DateTime.Now,
                UserId = user.Id,
                ShippingId = shippingID
            };

            _commentaryCreate.Execute(commentary);
            return RedirectToAction("List", "Shipping");
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