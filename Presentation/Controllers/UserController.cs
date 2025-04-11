using System.Diagnostics;
using System.Text.Json;
using Application.Interfaces;
using Core.Entities;
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
            // Using var rather than ShippingStates as data type in case there isn't a user saved on SessionStorage
            // If there is no user saved in SessionStorage and it tries to parse the user from JSON to ShippingStates
            // and we're using a strong type it will blow up. Boom! not working. Not good.
            var role = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User")).Role;
            
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
           // Using var rather than UserDTO as data type in case there isn't a user saved on SessionStorage
           // If there is no user saved in SessionStorage and it tries to parse the user from JSON to UserDTO
           // and we're using a strong type it will blow up.
           var user = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User")); 
            
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

        [HttpDelete]
        public IActionResult Delete(string email)
        {
            UserDto user = _userGetByEmail.Execute(email);
            // TODO: logic goes here
            return RedirectToAction("Users");
        }
        
        [HttpPatch]
        public IActionResult Update(string email)
        {
            UserDto user = _userGetByEmail.Execute(email);
            // TODO: logic goes here
            return RedirectToAction("Users");
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}