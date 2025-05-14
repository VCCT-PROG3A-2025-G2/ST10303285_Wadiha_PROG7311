using FarmersConnectWebApp.Models;

namespace FarmersConnectWebApp.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        void AddFarmer(Farmer farmer);
        IEnumerable<Product> GetProducts(int farmerId, DateTime startDate, DateTime endDate, string category);

        Employee GetByUserId(string userId);

        IEnumerable<Farmer> GetAllFarmers();
    }
}
