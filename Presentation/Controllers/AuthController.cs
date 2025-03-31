using System.Diagnostics;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using DTO.User;


namespace Presentation.Controllers
{
    public class AuthController : Controller{
        
        private readonly  IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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
            
            var user = _userRepository.GetByEmailAndPassword(userLogin.Email, userLogin.Password);

            if (user != null)
            {
                Console.WriteLine("Email: " + user?.Email);
                Console.WriteLine("PWD: " + user?.Password);
                HttpContext.Session.SetString("Role",Enum.GetName(typeof(Role), user.Role));
                HttpContext.Session.SetString("Email",userLogin.Email);
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