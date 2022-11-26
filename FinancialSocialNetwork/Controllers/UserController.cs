using Microsoft.AspNetCore.Mvc;

namespace FinancialSocialNetwork.Controllers
{
    public class UserController : Controller
    {

        DataAccess.DataAccess DA = new DataAccess.DataAccess(); 
        public IActionResult User(int ID)
        {
            var check = HttpContext.Session.Get("isLoggedIn");
            Boolean b = false;
            if (check != null)
            {
                b = true;
            }
            ViewBag.user = DA.getUser(ID);
            ViewBag.isLoggedIn = b;
            return View();
        }
    }
}
