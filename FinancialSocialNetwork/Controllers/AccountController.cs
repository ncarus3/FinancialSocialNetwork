using Microsoft.AspNetCore.Mvc;

namespace FinancialSocialNetwork.Controllers
{
    public class AccountController : Controller
    {

        DataAccess.DataAccess DA = new DataAccess.DataAccess();

        public IActionResult MyAccount()
        {
            var check = HttpContext.Session.Get("isLoggedIn");
            Boolean b = false;
            if (check != null)
            {
                b = true;
            }

            ViewBag.isLoggedIn = b;
            if (b)
            {

                ViewBag.bio = HttpContext.Session.GetString("Bio");
                ViewBag.profilePic = HttpContext.Session.GetString("ProfilePic");
                return View();

            } else
            {
                return RedirectToAction("Index", "Home");

            }
        }
    }
}
