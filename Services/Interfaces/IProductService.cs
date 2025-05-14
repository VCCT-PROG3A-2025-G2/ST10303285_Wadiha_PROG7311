namespace FarmersConnectWebApp.Services.Interfaces
{
    using FarmersConnectWebApp.Models;
    using System.Threading.Tasks;
    public interface IProductService
    {
        Task AddProductAsync(Product product);
        Task<Farmer> GetFarmerByUserId(string userId);

        IEnumerable<Product> GetProducts(DateTime? startDate, DateTime? endDate, string category, int? farmerId);
    }
}
