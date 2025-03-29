using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class ErrorController() : Controller
{
    [HttpGet]
    public IActionResult Index(string error)
    {
        ViewBag.ErrorMessage = error;
        return View();
    }
}