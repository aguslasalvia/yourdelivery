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
        private readonly IUserDelete _userDelete;
        private readonly IUserUpdate _userUpdate;
        
        public UserController(IUserGetAllCase userGetAll,IUserGetByEmail userGetByEmail, IUserDelete userDelete, IUserUpdate userUpdate)
        {
            _userGetByEmail = userGetByEmail;
            _userGetAll = userGetAll;
            _userDelete = userDelete;
            _userUpdate = userUpdate;
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
           var role = JsonSerializer.Deserialize<UserDto>(HttpContext.Session.GetString("User")).Role;
           var user = _userGetByEmail.Execute(email);
            
            if (Enum.GetName(role) != Role.Administrator.ToString() && user.Email != email )
                return RedirectToAction("Index", "Error", new { error = "You lack of privileges to enter this page" });
            
            UsersViewModelProfile model = new UsersViewModelProfile
            {
                User = user.Email == email ? user : _userGetByEmail.Execute(email)
            };
            ViewData["Title"] = "Profile";
            return View(model);
        }

        // HTML doesn't have a DELETE HTTP method you can use on submit through a form
        // so we'd have to use Javascript to do it if we wanted to use [HttpDelete].
        // While it is possible, it wouldn't let us use and validate the AntiForgeryToken.
        // I think it's best we keep it as HttpPost (less complexity == less stuff breaking up
        // and booming up later down the line). Same applies for PATCH.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email is missing.");
            }

            UserProfileDto userDto = _userGetByEmail.Execute(email);
            if (userDto == null)
            {
                throw new Exception("User not found.");
            }

            _userDelete.Execute(userDto);

            return RedirectToAction("Users");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UserProfileDto editedUser)
        {
            
            _userUpdate.Execute(editedUser);
            return RedirectToAction("Users");
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}