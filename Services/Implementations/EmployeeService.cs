using FarmersConnectWebApp.Models;
using FarmersConnectWebApp.Repositories.Interfaces;
using FarmersConnectWebApp.Services.Interfaces;

namespace FarmersConnectWebApp.Services.Implementations
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void AddFarmer(Farmer farmer)
        {
            _employeeRepository.AddFarmer(farmer);
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


    }
}
