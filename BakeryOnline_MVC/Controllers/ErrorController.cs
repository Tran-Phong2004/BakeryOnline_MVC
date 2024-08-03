using Microsoft.AspNetCore.Mvc;

namespace BakeryOnline_MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
            return View(statusCode);
        }
    }
}
