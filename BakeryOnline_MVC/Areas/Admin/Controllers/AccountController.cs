using BakeryOnline_MVC.Areas.Admin.Models;
using BakeryOnline_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BakeryOnline_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        ILogger<AccountController> _logger;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [Route("[controller]/Login.html")]
        public IActionResult LogIn()
        {
            return View();
        }


        [HttpPost("[controller]/Login.html")]
        public async Task<IActionResult> LogIn(LoginVM input)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(input.Username, input.Password, isPersistent: input.RememberMe, lockoutOnFailure: false);
                if(result.Succeeded)
                {
                    _logger.LogInformation($"User {input.Username} was successfully signed in");
                    return RedirectToAction("Index", "Home" , new  { area = "" });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your username and password");
                }
            }
            return View();
        }

        [Route("[controller]/Lockout")]
        public async Task<IActionResult> Lockout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [Route("[controller]/Signup.html")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("[controller]/Signup.html")]
        public async Task<IActionResult> SignUp([Bind("Username","Email","Password", "ConfirmPassword")]RegisterVM input)
        {
            if (ModelState.IsValid) {
                var user = new AppUser()
                {
                    UserName = input.Username,
                    Email = input.Email,
                };
                var result = await _userManager.CreateAsync(user,input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Create user {user.UserName} success!");
                    TempData["UserName"] = user.UserName;
                    return RedirectToAction("LogIn", "Account");
                }
                result.Errors.ToList().ForEach(error => {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }
            return View();
        }

        [Route("[controller]/Access-Denied.html")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
