using FinancialSocialNetwork.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinancialSocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        //Cookie isLoggedIn = new Cookie();
        private readonly ILogger<HomeController> _logger;



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }
           

        
        private DataAccess.DataAccess DA = new DataAccess.DataAccess();

        public IActionResult Index()
        {
            /*Boolean isLoggedIn = false;
            Byte[] b = new Byte[1];
            b[0] = 1;
            HttpContext.Session.Set("d", b);*/

            ViewBag.isLoggedIn = false;
            return View();
        }

        public IActionResult CurrentCurrency()
        {
            ViewBag.isLoggedIn = false;
           
            return View();
        }

        public IActionResult FindMatches()
        {
            List<UserModel> UM = new List<UserModel>();
            UM = DA.getUsers();
            ViewBag.people = UM;

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

        public ActionResult RenderMatchList()
        {
            return PartialView("_MatchTemplate");
        }

    }
}