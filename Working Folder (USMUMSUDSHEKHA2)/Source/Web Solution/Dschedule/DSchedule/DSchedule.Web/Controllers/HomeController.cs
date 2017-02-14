using System;
using DSchedule.BusinessComponent;
using DSchedule.Contracts;
using DSchedule.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Configuration;

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
            if (ModelState.IsValid && Login(model.UserName, model.Password, persistCookie: true))
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}