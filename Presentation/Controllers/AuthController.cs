using System.Diagnostics;
using System.Net;
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
            Console.WriteLine("Email: " + userLogin.Email);
            Console.WriteLine("PWD: " + userLogin.Password);
            HttpContext.Session.SetString("Role",Role.Administrator.ToString());
            HttpContext.Session.SetString("Email",userLogin.Email);
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