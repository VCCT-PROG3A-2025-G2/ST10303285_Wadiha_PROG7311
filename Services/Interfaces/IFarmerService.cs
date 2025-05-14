using FarmersConnectWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using FarmersConnectWebApp.Repositories.Implementations;
using FarmersConnectWebApp.Repositories.Interfaces;


namespace FarmersConnectWebApp.Services.Interfaces
{
    public interface IFarmerService
    {
        Task<IEnumerable<Farmer>> GetAllFarmersAsync();
        Task<Farmer> GetFarmerByIdAsync(int id);
        Task AddFarmerAsync(Farmer farmer);
        Task UpdateFarmerAsync(Farmer farmer);
        Task DeleteFarmerAsync(int id);
    }
}
