using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class ShippingController() : Controller
    {
        [HttpGet]
        public IActionResult Tracking(int? trackingNumber)
        {
            ViewData["Title"] = "Tracking";
            ViewBag.Shipping = trackingNumber;
            ViewBag.State = null;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}