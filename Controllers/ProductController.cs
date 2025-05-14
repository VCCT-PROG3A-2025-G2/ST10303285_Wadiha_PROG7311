using Microsoft.AspNetCore.Mvc;

namespace FarmersConnectWebApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
