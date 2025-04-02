using System.Diagnostics;
using System.Text.Json;
using Core.Enums;
using Core.Entities;
using Core.Interfaces;
using DTO.User;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;


namespace Presentation.Controllers
{
    public class UserController: Controller{
        
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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
            
            var role = JsonSerializer.Deserialize<UserDTO>(HttpContext.Session.GetString("User")).Role;
            
            if (Enum.GetName(role) != Role.Administrator.ToString())
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
           Console.WriteLine(email);
            var user = JsonSerializer.Deserialize<UserDTO>(HttpContext.Session.GetString("User")); 
            
            if (Enum.GetName(user.Role) != Role.Administrator.ToString() && user.Email != email )
                return RedirectToAction("Index", "Error", new { error = "You lack of privileges to enter this page" });

            if (user.Email == email)
                ViewBag.User = user;
            else
                ViewBag.User = _userRepository.GetByEmail(email);
            
            
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}