using Application.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class GetShippingController : ControllerBase
{
    private readonly IShippingGetById _shippingGetById;    
    public GetShippingController(IShippingGetById shippingGetById)
    {
        _shippingGetById = shippingGetById;
    }

    [HttpGet]
    public IActionResult Get(int tracking)
    {
        try
        {
            ShippingDto shipping = _shippingGetById.Execute(tracking);
            return Ok(shipping);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}