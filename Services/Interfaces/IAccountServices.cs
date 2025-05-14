using FarmersConnectWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FarmersConnectWebApp.Services.Interfaces
{
    public interface IAccountServices
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
