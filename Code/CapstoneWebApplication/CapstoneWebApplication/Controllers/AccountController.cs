using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationCapstone.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(WebApplicationCapstone.Models.AccountModel User)
        {
            string user_username = User.username;
            string user_password = User.password;

            if (user_username == User.role_admin_username &&
                user_password == User.role_admin_password)
            {
                User.user_id = 1;
                Session["user_id"] = User.user_id.ToString();
                Session["test_message"] = "Hello, team!";
                //return RedirectToAction("Assign", "Experimenter");
                return RedirectToAction("Text.aspx", "Subject");
            }
            else
            {
                Session["user_id"] = null;
                User.login_error_message = "Wrong username or password";
                return View("Index", User);
            }
        }
    }
}