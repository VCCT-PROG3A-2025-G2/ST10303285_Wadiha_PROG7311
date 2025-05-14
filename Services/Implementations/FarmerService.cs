using FarmersConnectWebApp.Models;
using FarmersConnectWebApp.Repositories.Interfaces;
using FarmersConnectWebApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarmersConnectWebApp.Services.Implementations
{
    public class FarmerService: IFarmerService
    {
        private readonly IFarmerRepository _repository;

        public FarmerService(IFarmerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Farmer>> GetAllFarmersAsync() => await _repository.GetAllFarmersAsync();

        public async Task<Farmer> GetFarmerByIdAsync(int id) => await _repository.GetFarmerByIdAsync(id);

        public async Task AddFarmerAsync(Farmer farmer) => await _repository.AddFarmerAsync(farmer);

        public async Task UpdateFarmerAsync(Farmer farmer) => await _repository.UpdateFarmerAsync(farmer);

        public async Task DeleteFarmerAsync(int id) => await _repository.DeleteFarmerAsync(id);
    }
}
