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
           
        private Boolean checkLogin()
        {
            var check = HttpContext.Session.Get("isLoggedIn");
            Boolean b = false;
            if (check != null)
            {
                b = true;
            }
            return b;
        }
        
        private DataAccess.DataAccess DA = new DataAccess.DataAccess();

        public IActionResult Index()
        {

            ViewBag.isLoggedIn = checkLogin();
            return View();
        }

        public IActionResult CurrentCurrency()
        {
            ViewBag.isLoggedIn = checkLogin();

            return View();
        }

        public IActionResult FindMatches()
        {
            List<UserModel> UM = new List<UserModel>();
            UM = DA.getUsers();
            ViewBag.people = UM;

            ViewBag.isLoggedIn = checkLogin();
            return View();
        }
        public IActionResult RegisterLogin()
        {
            ViewBag.isLoggedIn = checkLogin();
            if (checkLogin())
            {
                return RedirectToAction("MyAccount", "Account");

            }
            else
            {
                return View();

            }
        }

        public IActionResult About()
        {
            ViewBag.isLoggedIn = checkLogin();

            return View();
        }

        public IActionResult Help()
        {
            ViewBag.isLoggedIn = checkLogin();

            return View();
        }

        public IActionResult GoPro()
        {
            ViewBag.isLoggedIn = checkLogin();

            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.isLoggedIn = checkLogin();

            return View();
        }

        public IActionResult Chat()
        {
            ViewBag.isLoggedIn = checkLogin();
            if (checkLogin())
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
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