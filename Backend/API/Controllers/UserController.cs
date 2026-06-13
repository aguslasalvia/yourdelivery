using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Core.Interfaces;
using DTO;
using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using API.Configuration;
using Microsoft.Extensions.Options;

namespace API.Controllers;

public class UserController : ControllerBase
{
    private readonly IUserLoginCase _userLoginCase;
    private readonly IUserGetByEmail _userGetByEmail;
    private readonly IUserChangePassword _userChangePassword;
    private readonly JwtSettings _jwtSettings;

    public UserController(
        IUserLoginCase userLoginCase,
        IUserGetByEmail userGetByEmail,
        IUserChangePassword userChangePassword,
        IOptions<JwtSettings> jwtSettings)
    {
        _userLoginCase = userLoginCase;
        _userGetByEmail = userGetByEmail;
        _userChangePassword = userChangePassword;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] UserAuthDto credentials)
    {
        try
        {
            if (credentials != null)
            {
                UserDto user = _userLoginCase.Execute(credentials);
                var token = CreateJwtToken(credentials.Email, user.Role);
                return Ok(new { AccessToken = token, User = user });
            }
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPatch]
    [Route("password-change")]
    public IActionResult ChangePassword([FromBody] UserPasswordChangeDto passwordChangeDto)
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        if (claimsIdentity == null)
            return Unauthorized();

        var role = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
        var email = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

        if (role.Value != Role.Client.ToString())
            return Unauthorized("Your role must be Client to use this endpoint");

        try
        {
            if (passwordChangeDto != null)
            {
                _userChangePassword.Execute(email.Value, passwordChangeDto);
                return Ok("Password changed successfully");
            }
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return BadRequest();
    }

    [Authorize]
    [HttpGet]
    [Route("profile")]
    public IActionResult GetProfile()
    {
        try
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return Unauthorized();

            var role = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
            var email = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            if (role.Value != Role.Client.ToString())
                return Unauthorized("Your role must be Client to use this endpoint");

            UserProfileDto user = _userGetByEmail.Execute(email.Value);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [NonAction]
    public string CreateJwtToken(string email, Role role)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey));

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role.ToString()),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        String tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }

}