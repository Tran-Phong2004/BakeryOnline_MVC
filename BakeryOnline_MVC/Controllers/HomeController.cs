using BakeryOnline_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Diagnostics;
using BakeryOnline_MVC.Filters.ExeptionHandling;
namespace BakeryOnline_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext Context)
        {
            _logger = logger;
            _context = Context;

        }

        public IActionResult Index()
        {
            var qr = (from p in _context.Product
                      join c in _context.Category on p.Category_ID equals c.ID
                      where c.Name == "Cakes"
                      select p).AsNoTracking().Take(10).Include(p => p.Category).ToList();

            var qrDesserts = (from p in _context.Product
                      join c in _context.Category on p.Category_ID equals c.ID
                      where c.Name == "Desserts"
                              select p).AsNoTracking().Include(p => p.Category).Take(10).ToList();
            ViewBag.Cakes = qr;
            ViewBag.Desserts = qrDesserts;
            return View();
        }

     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
