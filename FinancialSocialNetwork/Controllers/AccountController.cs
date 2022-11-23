using Microsoft.AspNetCore.Mvc;

namespace FinancialSocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult MyAccount()
        {
            ViewBag.isLoggedIn = true;
            return View();
        }
    }
}
