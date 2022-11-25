using Microsoft.AspNetCore.Mvc;

namespace FinancialSocialNetwork.Controllers
{
    public class LoginController : Controller
    {
        private DataAccess.DataAccess DA = new DataAccess.DataAccess();
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

        public JsonResult login(String username, String password)
        {
            Boolean r = false;
            String UserID = "";
            r = DA.login(username, password, out UserID);
            if (r)
            {
                HttpContext.Session.SetString("isLoggedIn", "true");
                HttpContext.Session.SetString("UserID", UserID);
                HttpContext.Session.SetString("Bio", DA.getBio(int.Parse(UserID)));
                HttpContext.Session.SetString("ProfilePic", DA.getProfile(int.Parse(UserID)));
                
                
                
            }
            return new JsonResult(r); 
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
