using System.Diagnostics;
using Core.Enums;
using Core.Interfaces;
using System.Text.Json;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using DTO.Users;
using DTO.Users;


namespace Presentation.Controllers
{
    public class AuthController : Controller
    {

        private IUserLoginCase _login;

        public AuthController(IUserLoginCase login)
        {
            _login = login;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult SubmitLogin(UserLoginDto userLogin)
        {
            
            var user = _login.Execute(userLogin);
            if (user != null)
            {
                HttpContext.Session.SetString("User", JsonSerializer.Serialize(user));
                HttpContext.Session.SetString("Email",user.Email);
                HttpContext.Session.SetString("Role",Enum.GetName(user.Role));
                return RedirectToAction("Dashboard","User");
            }
            
            // TODO: Search the user on the DB
            return View("Login");

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