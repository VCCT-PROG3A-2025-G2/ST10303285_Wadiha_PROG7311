namespace FarmersConnectWebApp.Services.Implementations
{
    using FarmersConnectWebApp.Data;
    using FarmersConnectWebApp.Models;
    using FarmersConnectWebApp.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Farmer> GetFarmerByUserId(string userId)
        {
            return await _context.Farmers.FirstOrDefaultAsync(f => f.UserId == userId);
        }

        public IEnumerable<Product> GetProducts(DateTime? startDate, DateTime? endDate, string category, int? farmerId)
        {
            var query = _context.Products.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(p => p.DateAdded >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(p => p.DateAdded <= endDate.Value);
            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category == category);
            if (farmerId.HasValue)
                query = query.Where(p => p.FarmerId == farmerId.Value);

            return query.ToList();
        }
    }
}
