using System;
using DSchedule.BusinessComponent;
using DSchedule.Contracts;
using DSchedule.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DSchedule.Web.Controllers
{
    public class HomeController : Controller
    {
        UserBusinessComponent loginComponent = new UserBusinessComponent();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid && Login(model.UserName, model.Password, persistCookie: true ))
            {
                FormsAuthentication.SetAuthCookie(model.UserName,true);
                return RedirectToAction("UserDetails", "UserAccounts");
            }            
            ModelState.AddModelError("", "Incorrect Credentials.");
            return View(model);
        }

        private bool Login(string userName, string password, bool persistCookie)
        {
            return loginComponent.ValidateUser(userName, password);
        }
    }
}