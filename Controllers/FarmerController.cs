using Microsoft.AspNetCore.Mvc;
using FarmersConnectWebApp.Models;
using FarmersConnectWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using FarmersConnectWebApp.Data;
using Microsoft.AspNetCore.Identity;
using FarmersConnectWebApp.Services.Implementations;


namespace FarmersConnectWebApp.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFarmerService _farmerService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;


        public FarmerController(
            IProductService productService,
            IFarmerService farmerService,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _productService = productService;
            _farmerService = farmerService;
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Dashboard()
        {
            var userId = _userManager.GetUserId(User);
            var hasProfile = _context.Farmers.Any(f => f.UserId == userId);

            if (!hasProfile)
            {
                TempData["ErrorMessage"] = "Your farmer profile has not been created yet. Please contact an employee.";
                return RedirectToAction("NoProfile");
            }

            ViewBag.Message = "Hello Farmer!";
            return View();
        }

        public IActionResult NoProfile()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var farmers = await _farmerService.GetAllFarmersAsync();
            return View(farmers);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                await _farmerService.AddFarmerAsync(farmer);
                return RedirectToAction("Index");
            }
            return View(farmer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var farmer = await _farmerService.GetFarmerByIdAsync(id);
            if (farmer == null) return NotFound();
            return View(farmer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                await _farmerService.UpdateFarmerAsync(farmer);
                return RedirectToAction("Index");
            }
            return View(farmer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _farmerService.DeleteFarmerAsync(id);
            return RedirectToAction("Index");
        }

        // View to Add Product
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        // Handle the Add Product Form Submission
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill in all required fields.";
                return View(model);
            }

            try
            {
                var userId = _userManager.GetUserId(User);
                var farmer = await _productService.GetFarmerByUserId(userId);

                if (farmer == null)
                {
                    TempData["ErrorMessage"] = "Farmer not found.";
                    return RedirectToAction("AddProduct");
                }

                var product = new Product
                {
                    Name = model.Name,
                    Category = model.Category,
                    DateAdded = model.DateAdded,
                    FarmerId = farmer.Id
                };

                await _productService.AddProductAsync(product);

                TempData["SuccessMessage"] = "Product added successfully.";
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return View(model);
            }
        }

    }
}
