using BakeryOnline_MVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BakeryOnline_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [Route("[controller]/danh-sach-role.html")]
        public async Task<IActionResult> Index()
        {
            var roleList = await _roleManager.Roles.OrderBy(role => role.Name).ToListAsync();
            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleVM input)
        {
            var role = new IdentityRole()
            { 
                Name = input.RoleName,
            };
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded) 
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }
            TempData["NewRoleName"] = role.Name;

            var roleList = await _roleManager.Roles.OrderBy(role => role.Name).ToListAsync();
            ViewBag.RoleList = roleList;
            return View("Index");
        }

        public async Task<IActionResult> DeleteRole(RoleVM input)
        {
            if(input != null)
            {
                var role = await _roleManager.FindByNameAsync(input.RoleName);
                var result = await _roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    TempData["DeleteError"] = string.Join(", ", result.Errors.Select(error => error.Description));
                }
            }
            return RedirectToAction("Index", "Role");
        }
    }
}
