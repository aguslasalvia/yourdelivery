using System.Diagnostics;
using System.Text.Json;
using Core.Enums;
using Core.Interfaces;
using DTO.Users;
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
            
            var role = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User")).Role;
            
            if (Enum.GetName(role) != Role.Administrator.ToString())
            {
                
                return RedirectToAction("Index", "Error", new { error = "You lack of privileges to enter this page" });
            }   
            ViewBag.User = "Test";
            ViewData["Title"] = "Users";
            var users = _userRepository.GetAll().ToList();
            ViewBag.Users = users;
            return View();
        }

        
        [HttpGet]
        public IActionResult Profile(string email)
        {
           Console.WriteLine(email);
            var user = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User")); 
            
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