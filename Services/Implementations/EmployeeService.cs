using FarmersConnectWebApp.Data;
using FarmersConnectWebApp.Models;
using FarmersConnectWebApp.Repositories.Interfaces;
using FarmersConnectWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using SQLitePCL;

namespace FarmersConnectWebApp.Services.Implementations
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context; // Store the context for saving changes later
        

        public EmployeeService(IEmployeeRepository employeeRepository, UserManager<IdentityUser> userManager,
        ApplicationDbContext context)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _context = context; // Store the context for saving changes later
            
        }

        public async Task AddFarmerAsync(Farmer farmer)
        {
           await _employeeRepository.AddFarmerAsync(farmer);
            await _context.SaveChangesAsync(); // Ensure changes are saved to the database

        }

        public IEnumerable<Product> GetProducts(int farmerId, DateTime startDate, DateTime endDate, string category)
        {
            return _employeeRepository.GetProducts(farmerId, startDate, endDate, category);
        }

        public Employee GetByUserId(string userId)
        {
            return _employeeRepository.GetByUserId(userId);
        }

        public IEnumerable<Farmer> GetAllFarmers()
        {
            return _employeeRepository.GetAllFarmers(); // Call repository to fetch farmers
        }

        public async Task<(bool Success, string ErrorMessage)> CreateFarmerWithLoginAsync(CreateFarmerViewModel model, string employeeUserId)
        {
            var employee = _employeeRepository.GetByUserId(employeeUserId);
            if (employee == null)
                return (false, "Employee not found.");

            var identityUser = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(identityUser, "Farmer@123");
            if (!createResult.Succeeded)
                return (false, "Failed to create Identity user.");

            await _userManager.AddToRoleAsync(identityUser, "Farmer");

            var farmer = new Farmer
            {
                FullName = model.FullName,
                Email = model.Email,
                UserId = identityUser.Id,
                EmployeeId = employee.Id
            };

            await _employeeRepository.AddFarmerAsync(farmer);
            return (true, null);
        }
    }
}
