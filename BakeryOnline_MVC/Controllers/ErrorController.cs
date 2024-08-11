using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BakeryOnline_MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
            if (statusCode >= 400 && statusCode < 500)
            {
                ViewBag.ErrorMessage = "Client error occurred. Please check your request.";
            }
            if (statusCode >= 500)
            {
                ViewBag.ErrorMessage = "Server error occurred. Please try again later.";
            }
            return View(statusCode);
        }
    }
}
