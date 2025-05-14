using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FarmersConnectWebApp.Services;
using FarmersConnectWebApp.Repositories;
using FarmersConnectWebApp.Models;
using FarmersConnectWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarmersConnectWebApp.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProductService _productService;

        public EmployeeController(IProductService productService, IEmployeeService employeeService, UserManager<IdentityUser> userManager)
        {
            _employeeService = employeeService;
            _userManager = userManager;
            _productService = productService;
        }

        public IActionResult Dashboard()
        {
            ViewBag.Message = "Hello Employee!";
            return View();
        }

        // Add Farmer Profile
        [HttpGet]
        public IActionResult AddFarmer()
        {
            return View(new CreateFarmerViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddFarmer(CreateFarmerViewModel model)
        {

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill in all required fields correctly.";
                return View(model);
            }

            try
            {
                var userId = _userManager.GetUserId(User);
                var employee = _employeeService.GetByUserId(userId);

                if (employee == null)
                {
                    TempData["ErrorMessage"] = "Logged-in employee not found.";
                    return RedirectToAction("AddFarmer");
                }

                // 1. Create IdentityUser
                var identityUser = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var createResult = await _userManager.CreateAsync(identityUser, "Farmer@123"); // TODO: Generate or allow secure password

                if (!createResult.Succeeded)
                {
                    TempData["ErrorMessage"] = "Failed to create user account.";
                    return View(model);
                }

                // 2. Assign Farmer role
                await _userManager.AddToRoleAsync(identityUser, "Farmer");

                // 3. Create Farmer entity and save
                var farmer = new Farmer
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    UserId = identityUser.Id,
                    EmployeeId = employee.Id
                };

                _employeeService.AddFarmer(farmer);

                TempData["SuccessMessage"] = "Farmer profile and login created successfully.";
                return RedirectToAction("Dashboard");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the Farmer profile.";
                return View(model);
            }
        }

     

        // View Products from Farmers
        [HttpGet]
        public IActionResult ViewProducts(DateTime? startDate, DateTime? endDate, string category, int? farmerId)
        {

            var farmers = _employeeService.GetAllFarmers();

            ViewBag.Farmers = new SelectList(farmers, "Id", "FullName");

            var products = _productService.GetProducts(startDate, endDate, category, farmerId);
            return View(products);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
