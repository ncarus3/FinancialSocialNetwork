using FinancialSocialNetwork.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinancialSocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private DataAccess.DataAccess DA = new DataAccess.DataAccess();

        public IActionResult Index()
        {
            ViewBag.isLoggedIn = false;
            return View();
        }

        public IActionResult RegisterLogin()
        {
            ViewBag.isLoggedIn = false;
            return View();
        }

        public IActionResult About()
        {
            ViewBag.isLoggedIn = false;

            return View();
        }

        public IActionResult GoPro()
        {
            ViewBag.isLoggedIn = false;

            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.isLoggedIn = false;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}