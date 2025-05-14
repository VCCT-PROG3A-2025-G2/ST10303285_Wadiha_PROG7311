using FarmersConnectWebApp.Models;
using FarmersConnectWebApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using FarmersConnectWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using FarmersConnectWebApp.Data;

namespace FarmersConnectWebApp.Services.Implementations
{
    public class AccountService : IAccountServices
    {
        private readonly IAccountRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;


        public AccountService(
            IAccountRepository repository,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext dbContext)
        {
            _repository = repository;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return result;

            await _userManager.AddToRoleAsync(user, model.Role);

            
            if (model.Role == "Employee")
            {
                var employee = new Employee
                {
                    FullName = model.FullName,
                    UserId = user.Id
                };

                _dbContext.Employees.Add(employee);
                await _dbContext.SaveChangesAsync();
            }
            else if (model.Role == "Farmer")
            {
                var farmer = new Farmer
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    UserId = user.Id,
                    EmployeeId = 0 
                };

                _dbContext.Farmers.Add(farmer);
                await _dbContext.SaveChangesAsync();
            }

            return result;
        }


        public Task<SignInResult> LoginAsync(LoginViewModel model)
            => _repository.LoginAsync(model);

        public Task LogoutAsync()
            => _repository.LogoutAsync();
    }
}
