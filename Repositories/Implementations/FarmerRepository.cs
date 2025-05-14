using FarmersConnectWebApp.Models;
using FarmersConnectWebApp.Data;
using FarmersConnectWebApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarmersConnectWebApp.Repositories.Implementations
{
    public class FarmerRepository: IFarmerRepository
    {
        private readonly ApplicationDbContext _context;

        public FarmerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Farmer>> GetAllFarmersAsync()
        {
            return await _context.Farmers.Include(f => f.Products).ToListAsync();
        }

        public async Task<Farmer> GetFarmerByIdAsync(int id)
        {
            return await _context.Farmers.Include(f => f.Products).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddFarmerAsync(Farmer farmer)
        {
            _context.Farmers.Add(farmer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFarmerAsync(Farmer farmer)
        {
            _context.Farmers.Update(farmer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFarmerAsync(int id)
        {
            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer != null)
            {
                _context.Farmers.Remove(farmer);
                await _context.SaveChangesAsync();
            }
        }              
    }
}
