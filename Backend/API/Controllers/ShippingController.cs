using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using DTO;
using Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class ShippingController : ControllerBase
{
    private readonly IUserGetByEmail _userGetByEmail;
    private readonly IShippingGetById _shippingGetById;
    private readonly IShippingGetByClientId _shippingGetByClientId;
    private readonly IShippingGetByDates _shippingGetByDates;
    private readonly IShippingGetByCommentary _shippingGetByCommentary;
    public ShippingController(IUserGetByEmail userGetByEmail, IShippingGetById shippingGetById, IShippingGetByClientId shippingGetByClientId, IShippingGetByDates shippingGetByDates, IShippingGetByCommentary shippingGetByCommentary)
    {
        _userGetByEmail = userGetByEmail;
        _shippingGetById = shippingGetById;
        _shippingGetByClientId = shippingGetByClientId;
        _shippingGetByDates = shippingGetByDates;
        _shippingGetByCommentary = shippingGetByCommentary;
    }

    [HttpGet]
    [Route("shipping/tracking")]
    public IActionResult GetByTracking(int id)
    {
        try
        {
            ShippingDto shipping = _shippingGetById.Execute(id);
            return Ok(shipping);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("shipping/all")]
    public IActionResult GetAll()
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
            UserProfileDto user = _userGetByEmail.Execute(email.Value);
            List<ShippingDto> shippings = _shippingGetByClientId.Execute(user.Id);

            return Ok(shippings);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("shipping/by-dates")]
    public IActionResult GetByDates(DateTime from, DateTime to, ShippingState state)
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        if (claimsIdentity == null)
            return Unauthorized();

        var role = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
        var email = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

        if (role.Value != Role.Client.ToString())
            return Unauthorized("Your role must be Client to use this endpoint");

        if (from == default(DateTime))
            return BadRequest("From date is required");

        if (to == default(DateTime))
            return BadRequest("To date is required");

        try
        {
            UserProfileDto user = _userGetByEmail.Execute(email.Value);
            List<ShippingDto> shippings = _shippingGetByDates.Execute(from, to, state, user.Id);
            return Ok(shippings);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("shipping/by-comment")]
    public IActionResult GetByComment(string key)
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
            UserProfileDto user = _userGetByEmail.Execute(email.Value);
            List<ShippingDto> shippings = _shippingGetByCommentary.Execute(key, user.Id);
            return Ok(shippings);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}