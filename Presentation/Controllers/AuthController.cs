using System.Diagnostics;
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
            return View();
        }

        [HttpPost]
        public IActionResult SubmitLogin([FromBody] LoginUserDTO userLogin)
        {
            // TODO: Search the user on the DB
            
            return RedirectToAction("Dashboard","User");
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}