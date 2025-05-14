using FarmersConnectWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FarmersConnectWebApp.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
