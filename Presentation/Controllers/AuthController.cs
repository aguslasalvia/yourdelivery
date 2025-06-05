using System.Diagnostics;
using Core.Enums;
using Core.Interfaces;
using System.Text.Json;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using DTO.Users;
using System.Security.Authentication;


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
            HttpContext.Session.Clear();
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult SubmitLogin(LoginViewModel model)
        {
            try
            {
   
                    HttpContext.Session.SetString("User", "User");
                    HttpContext.Session.SetString("Email", "user");
                    HttpContext.Session.SetString("Role", "Administrator");
                    return RedirectToAction("Dashboard", "User");
										            }
            catch (Exception e)
            {
                model.Message = e.Message;
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