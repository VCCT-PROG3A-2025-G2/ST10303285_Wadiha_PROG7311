using FarmersConnectWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FarmersConnectWebApp.Services.Interfaces;

namespace FarmersConnectWebApp.Controllers
{
    public class AccountController : Controller
    {
       
        private readonly IAccountServices _accountService; 
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(IAccountServices accountService, UserManager<IdentityUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register(string role)
        {
            return View(new RegisterViewModel { Role = role });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _accountService.RegisterAsync(model);
            if (result.Succeeded) return RedirectToAction("Login");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _accountService.LoginAsync(model);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Farmer"))
                    return RedirectToAction("Dashboard", "Farmer");
                else if (roles.Contains("Employee"))
                    return RedirectToAction("Dashboard", "Employee");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
