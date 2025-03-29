using System.Diagnostics;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;


namespace Presentation.Controllers
{
    public class UserController: Controller{

        [HttpGet]
        public IActionResult Dashboard()
        {
            ViewData["Title"] = "Dashboard";
            ViewBag.User = "Test";

            return View();
        }


        
        [HttpGet]
        public IActionResult Users()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != Role.Administrator.ToString())
            {
                return RedirectToAction("Login", "Auth");
            }   
            ViewBag.User = "Test";
            ViewData["Title"] = "Users";
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}