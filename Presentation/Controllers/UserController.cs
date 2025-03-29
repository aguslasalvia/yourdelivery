using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
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
            ViewBag.User = "Testing";
            return View();
        }



        [HttpGet]
        public IActionResult Users()
        {

            int? role = HttpContext.Session.GetInt32("Role");
            
            if (role == null || (Role)role != Role.Administrator)
            {
                return RedirectToAction("Login", "Auth");
            }   
            
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