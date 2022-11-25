using Microsoft.AspNetCore.Mvc;

namespace FinancialSocialNetwork.Controllers
{
    public class UserController : Controller
    {
        public IActionResult User()
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
