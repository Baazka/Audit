using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Audit.App_Func;
using Audit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;

namespace Audit.Controllers
{
    public class AuthenticationController : Controller
    {
        IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [HttpGet]
        //[NoCache]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        [Route("Login", Name = "Login")]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                    Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                LoginViewModel lvm = new LoginViewModel();
                lvm.ReturnUrl = returnUrl;
                return View(lvm);
            }
            catch
            {
                Session["Message"] = "Системд алдаа гарлаа. Та түр хүлээгээд дахин оролдоно уу.";
                return View();
            }
        }

        [HttpPost]
        [Route("Login", Name = "UserLogin")]
        public ActionResult Login(LoginViewModel user)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                    Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                if (ModelState.IsValid)
                {
                    XElement res = AppStatic.SystemController.UserLogin(user.UserName, user.Password);

                    if (AppStatic.SystemController.Status)
                    {
                        SystemUser loggedUser = new SystemUser().FromXml(res.Element("User"));
                        loggedUser.UserName = user.UserName;
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, loggedUser.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, loggedUser.UserID.ToString()));

                        if (loggedUser.UserID != 0)
                            claims.Add(new Claim("UserID", loggedUser.UserID.ToString()));

                        var identity = new ClaimsIdentity(
                           claims,
                           DefaultAuthenticationTypes.ApplicationCookie,
                           ClaimTypes.Name, ClaimTypes.Role);

                        if (loggedUser.IsAdmin)
                            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

                        Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);                        
                        return RedirectToAction("Index", "Home", new { Area = "" });
                    }
                    else
                    {
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                        Session["Message"] = AppStatic.SystemController.Message;
                    }
                }

                return View();
            }
            catch
            {
                Session["Message"] = "Системд алдаа гарлаа. Та түр хүлээгээд дахин оролдоно уу.";
                return View();
            }
        }

        [ApplicationAuthorize]
        [Route("Logout", Name = "Logout")]
        public ActionResult Logout()
        {
            try
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Session.Abandon();
                return RedirectToAction("Login", "Authentication", new { Area = "" });
            }
            catch
            {
                Session["Message"] = "Системд алдаа гарлаа. Та түр хүлээгээд дахин оролдоно уу.";
                return View();
            }
        }
    }
}