using FarmersConnectWebApp.Models;

namespace FarmersConnectWebApp.Services.Interfaces
{
    public interface IEmployeeService
    {
        void AddFarmer(Farmer farmer);
        IEnumerable<Product> GetProducts(int farmerId, DateTime startDate, DateTime endDate, string category);

        Employee GetByUserId(string userId);

        IEnumerable<Farmer> GetAllFarmers();
    }
}
