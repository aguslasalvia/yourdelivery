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
                /// TODO: que en vez de redireccionar al login muestre un vista
                /// TODO: que le informe al usuario que no tiene permisos suficientes
                /// TODO: para acceder a esa página. Siento que sería una mejor UX que
                /// TODO: redirigirlo al login directamente
            }   
            ViewBag.User = "Test";
            ViewData["Title"] = "Users";
            return View();
        }

        
        [HttpGet]
        public IActionResult Profile(string email)
        {
            var role = HttpContext.Session.GetString("Role");
            var sessionEmail = HttpContext.Session.GetString("Email");

            if (role != Role.Administrator.ToString() && sessionEmail != email)
                return RedirectToAction("Index", "Error", new { error = "No tenés privilegios para acceder a esta página" });
            
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}