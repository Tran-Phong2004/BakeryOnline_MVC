using BakeryOnline_MVC.Areas.Admin.Models;
using BakeryOnline_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;

namespace BakeryOnline_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RoleController : Controller
    {
        RoleManager<AppRole> _roleManager;
        UserManager<AppUser> _userManager;
        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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
            var role = new AppRole()
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

        public async Task<string> SetDefaultRole(string defaultRole) 
        {
            var role = await _roleManager.FindByNameAsync(defaultRole);
            if (role != null) 
            {
                role.IsDefaultRole = true;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded) 
                {
                    await SetFalse(role);
                    return "Ok";
                }
                else
                {
                    return "Failure: " + string.Join(", ",result.Errors.Select(error => error.Description));
                }
            }
            return "Failure : Role Not Found";
        }

        //Set field IsRoleDefault = false exept 'appRole'
        private async Task SetFalse(AppRole appRole)
        {
            var allRole = await _roleManager.Roles.ToListAsync();
            for(int i = 0; i < allRole.Count; i++)
            {
                var role = allRole[i];
                if(role.Id !=  appRole.Id)
                {
                    role.IsDefaultRole = false;
                    await _roleManager.UpdateAsync(role);
                }
            }
        }

        public async Task<IActionResult> EditRole(string oldName, string newName)
        {
            if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrWhiteSpace(newName))
            {
                TempData["UpdateRoleError"] = "Old name and new name must not be empty.";
                return RedirectToAction("Index","Role");
            }
            var oldRole = await _roleManager.FindByNameAsync(oldName); // lấy ra role cũ
            var newRole = await _roleManager.FindByNameAsync(newName); // kiểm tra role mới đã tồn tại chưa
            if (newRole != null)
            {
                TempData["RoleExists"] = $"Role {newName} is exists";
                return RedirectToAction("Index", "Role");
            }

            //Nếu có tồn tại 'oldRole' thì update tên 'oldRole' bằng tên 'newName'
            if (oldRole != null) 
            {
               var userList = await _userManager.GetUsersInRoleAsync(oldName);

               oldRole.Name = newName;
               var result = await _roleManager.UpdateAsync(oldRole);
               if(!result.Succeeded)
               {
                    TempData["UpdateRoleError"] = string.Join(", ",result.Errors.Select(error => error.Description));
                    return RedirectToAction("Index", "Role");
                }

               //Update user có tên role cũ bị thay đổi thành role mới
               var updateUserErr = new List<string>();
               foreach (var user in userList)
               {
                    var updateUserResult = await _userManager.AddToRoleAsync(user, newName);
                    if (!updateUserResult.Succeeded) 
                    {
                        updateUserErr.Add(string.Join(", ",$"Error user {user.UserName}: " + result.Errors.Select(error => error.Description)));
                    }
               }
               TempData["UpdateRoleUserErr"] = updateUserErr;
            }
            return RedirectToAction("Index","Role"); 
        }
    }
}
