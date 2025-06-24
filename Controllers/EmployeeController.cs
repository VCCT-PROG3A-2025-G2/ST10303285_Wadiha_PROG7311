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
                var result = await _employeeService.CreateFarmerWithLoginAsync(model, _userManager.GetUserId(User));

                if (!result.Success)
                {
                    TempData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }

                TempData["SuccessMessage"] = "";
                return RedirectToAction("Dashboard");

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating the farmer profile: {ex.Message}";
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
