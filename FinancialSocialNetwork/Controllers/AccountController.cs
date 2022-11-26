using Microsoft.AspNetCore.Mvc;

namespace FinancialSocialNetwork.Controllers
{
    public class AccountController : Controller
    {

        DataAccess.DataAccess DA = new DataAccess.DataAccess();

        public JsonResult updatePicture(String newPicture)
        {
            Boolean r = false;

             r = DA.updatePicture(int.Parse(HttpContext.Session.GetString("UserID")), newPicture);

            return new JsonResult(r);
        }

        public JsonResult updateBio(String newBio)
        {
            Boolean r = false;

            r = DA.updateBio(int.Parse(HttpContext.Session.GetString("UserID")), newBio);

            return new JsonResult(r);
        }

        public JsonResult addBank(int BankID)
        {
            Boolean r = false;

            r = DA.addBank(int.Parse(HttpContext.Session.GetString("UserID")), BankID);

            return new JsonResult(r);
        }
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

                ViewBag.bio = DA.getBio(int.Parse(HttpContext.Session.GetString("UserID")));
                ViewBag.profilePic = DA.getProfile(int.Parse(HttpContext.Session.GetString("UserID")));
                ViewBag.BankList = DA.getUserBanks(int.Parse(HttpContext.Session.GetString("UserID")));
                ViewBag.theBankList = DA.getBanks();
                return View();

            } else
            {
                return RedirectToAction("Index", "Home");

            }
        }
    }
}
