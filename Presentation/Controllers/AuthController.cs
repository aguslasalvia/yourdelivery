using System.Diagnostics;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using DTO.User;


namespace Presentation.Controllers
{
    public class AuthController() : Controller{

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult SubmitLogin(LoginUserDTO userLogin)
        {
            // TODO: Search the user on the DB
            Console.WriteLine(userLogin.Password);
            Console.WriteLine(userLogin.Email);
            HttpContext.Session.SetString("Role",Role.Administrator.ToString());
            return RedirectToAction("Dashboard","User");
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