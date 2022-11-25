using Microsoft.AspNetCore.Mvc;

namespace FinancialSocialNetwork.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            var check = HttpContext.Session.Get("isLoggedIn");
            Boolean b = false;
            if (check != null)
            {
                b = true;
            }

            ViewBag.isLoggedIn = b;
            return View();
        }
    }
}
