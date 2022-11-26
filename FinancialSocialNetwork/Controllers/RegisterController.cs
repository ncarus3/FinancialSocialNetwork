using Microsoft.AspNetCore.Mvc;

namespace FinancialSocialNetwork.Controllers
{
    public class RegisterController : Controller
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

        public JsonResult Register(String fName, String lName, String username, String email, String password, String phoneNumber, String country)
        {
            Boolean r = false;



            r = DA.register(fName, lName, username, email, password, phoneNumber, country);





            return new JsonResult(r);

		}
    }
}
