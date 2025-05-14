using FarmersConnectWebApp.Models;
using Microsoft.AspNetCore.Identity;
using FarmersConnectWebApp.Repositories.Interfaces;

namespace FarmersConnectWebApp.Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    Console.WriteLine("❌ User does not exist.");
                }
                else
                {
                    Console.WriteLine("✅ User exists, but login failed.");
                }

                if (result.IsLockedOut)
                    Console.WriteLine("⚠️ Account is locked out.");
                else if (result.IsNotAllowed)
                    Console.WriteLine("⚠️ Account is not allowed to log in.");
                else
                    Console.WriteLine("⚠️ Invalid credentials.");
            }

            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
