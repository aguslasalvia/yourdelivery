using System.Diagnostics;
using System.Text.Json;
using Application.Interfaces;
using Core.Enums;
using Core.Interfaces;
using DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class UserController: Controller{
        
        private readonly IUserGetAllCase _userGetAll;
        private readonly IUserGetByEmail _userGetByEmail;

        public UserController(IUserGetAllCase userGetAll,IUserGetByEmail userGetByEmail)
        {
            _userGetByEmail = userGetByEmail;
            _userGetAll = userGetAll;
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
            
            Role role = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User")).Role;
            
            if (Enum.GetName(role) != Role.Administrator.ToString())
            {
                
                return RedirectToAction("Index", "Error", new { error = "You lack of privileges to enter this page" });
            }   
            
            List<UserListDto> users = _userGetAll.Execute().ToList();

            UsersViewModelUsers model = new UsersViewModelUsers
            {
                Users = users
            };
            
            return View(model);
        }

        
        [HttpGet]
        public IActionResult Profile(string email)
        {
           Console.WriteLine(email);
            UserDto user = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User")); 
            
            if (Enum.GetName(user.Role) != Role.Administrator.ToString() && user.Email != email )
                return RedirectToAction("Index", "Error", new { error = "You lack of privileges to enter this page" });
            
            UsersViewModelProfile model = new UsersViewModelProfile
            {
                User = user.Email == email
                    ? user
                    : _userGetByEmail.Execute(email)
            };
            
            return View(model);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}