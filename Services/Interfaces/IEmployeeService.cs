using FarmersConnectWebApp.Models;

namespace FarmersConnectWebApp.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task AddFarmerAsync(Farmer farmer);
        IEnumerable<Product> GetProducts(int farmerId, DateTime startDate, DateTime endDate, string category);

        Employee GetByUserId(string userId);

        IEnumerable<Farmer> GetAllFarmers();

        Task<(bool Success, string ErrorMessage)> CreateFarmerWithLoginAsync(CreateFarmerViewModel model, string employeeUserId);

    }
}
