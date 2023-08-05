using Microsoft.AspNetCore.Mvc;
using STD.Web.Controllers;

namespace TeamManagment.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult hey() {
            TempData["msg"] = "e:Add Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult UpgradeRole()
        {
            return View();
        }

      
    }
}