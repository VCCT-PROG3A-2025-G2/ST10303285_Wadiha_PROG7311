using System;
using System.Collections.Generic;
using System.Linq;
using FarmersConnectWebApp.Data;
using FarmersConnectWebApp.Models;
using FarmersConnectWebApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmersConnectWebApp.Repositories.Implementations
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddFarmer(Farmer farmer)
        {
            _context.Farmers.Add(farmer);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetProducts(int farmerId, DateTime startDate, DateTime endDate, string category)
        {
            return _context.Products
                .Where(p => p.FarmerId == farmerId &&
                            p.DateAdded >= startDate &&
                            p.DateAdded <= endDate &&
                            p.Category == category)
                .ToList();
        }

        public Employee GetByUserId(string userId)
        {
            return _context.Employees.FirstOrDefault(e => e.UserId == userId);
        }

        // Implement GetAllFarmers method
        public IEnumerable<Farmer> GetAllFarmers()
        {
            return _context.Farmers.ToList();
        }
    }


}

