using BakeryOnline_MVC.Models;
using BakeryOnline_MVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakeryOnline_MVC.Controllers
{
    public class CategoryController : Controller
    {
        ILogger<CategoryController> logger;
        UnitOfWork uow;
        public CategoryController(UnitOfWork uow, ILogger<CategoryController> logger)
        {
            this.uow = uow;
            this.logger = logger;
        }

        [Route("/Category/{name?}")]
        public IActionResult Index(string? name)
        {
            try
            {
                if (name == null)
                {
                    name = string.Empty;
                }
                var categories = uow.CategoryRepository.GetAll().AsNoTracking().ToList();
                ViewBag.IsChecked = name;
                ViewBag.Categories = categories;

                return View();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
