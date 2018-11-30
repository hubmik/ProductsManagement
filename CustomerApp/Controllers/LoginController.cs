using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerApp.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(Model.Users user)
        {
            using (var context = new Model.Db())
            {
                Model.Users userDetails = context.Users.Where(x => x.Login == user.Login && x.Password == user.Password).FirstOrDefault();
                if (userDetails == null)
                    return View(nameof(Index), user);
                else
                {
                    Session["userId"] = userDetails.UserId;
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}