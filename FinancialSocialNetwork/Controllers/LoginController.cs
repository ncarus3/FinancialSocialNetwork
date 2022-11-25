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

            r = DA.login(username, password);
            if (r)
            {
                HttpContext.Session.SetString("isLoggedIn", "true");
                
            }
            return new JsonResult(r); 
        }
    }
}
