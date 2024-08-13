using BakeryOnline_MVC.Areas.Admin.Models;
using BakeryOnline_MVC.Models;
using BakeryOnline_MVC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakeryOnline_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        UserManager<AppUser> _userManager;
        RoleManager<AppRole> _roleManager;
        IRepositoryBase<AppUser> _repositoryBase;
        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IRepositoryBase<AppUser> repositoryBase)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _repositoryBase = repositoryBase;
        }
        [Route("Admin/[controller]/Management/{p:int?}")]           
        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = 10;
            var collection = _userManager.Users;
            var paging = await _repositoryBase.Paging(collection, page, pageSize,filter: null ,orderBy: qr => qr.OrderBy(u => u.UserName));

            var model = new UserVM()
            {
                Paging = paging
            };

            foreach (var user in paging.Result)
            {
                var roleUser = await _userManager.GetRolesAsync(user);
                if (roleUser != null)
                {
                    model.UserRoles[user.UserName] = roleUser.ToList();
                }
            }

            var roleList = await _roleManager.Roles.ToListAsync();
            ViewBag.RoleList = roleList;
            return View(model);
        }

        public async Task<IActionResult> AddRoleUser(string userName,string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _roleManager.FindByNameAsync(roleName);
            if(user != null && role != null)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                TempData["ErrorAddRoleUser"] = string.Join(", ", result.Errors.Select(error => error.Description));
            }
            
            return RedirectToAction("Index","User");
        }

        public async Task<IActionResult> DeleteRoleUser(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _roleManager.FindByNameAsync(roleName);
            if (user != null && role != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                TempData["ErrorDeleteRoleUser"] = string.Join(", ", result.Errors.Select(error => error.Description));
            }

            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> DeleteUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                TempData["ErrorDeleteRoleUser"] = string.Join(", ", result.Errors.Select(error => error.Description));
            }
            return RedirectToAction("Index", "User");
        }


    }
}
