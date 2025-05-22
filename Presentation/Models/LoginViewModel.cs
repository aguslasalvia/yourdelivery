using Core.Entities;
using DTO;
using DTO.Users;

namespace Presentation.Models;

public class LoginViewModel
{
    public UserLoginDto UserLogin { get; set; }
    public string Message { get; set; }
}