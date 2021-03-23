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
            catch(Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return View();
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
                        XElement resss = AppStatic.SystemController.UserProfile(res.Value);

                        SystemUser loggedUser = new SystemUser().FromXml(resss.Element("SystemUser"));
                        loggedUser.UserName = user.UserName;
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, loggedUser.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, loggedUser.USER_ID.ToString()));

                        if (loggedUser.UserID != 0)
                            claims.Add(new Claim("UserID", loggedUser.USER_ID.ToString()));
                        if (loggedUser.USER_DEPARTMENT_ID != 0)
                            claims.Add(new Claim("DepartmentID", loggedUser.USER_DEPARTMENT_ID.ToString()));

                        var identity = new ClaimsIdentity(
                           claims,
                           DefaultAuthenticationTypes.ApplicationCookie,
                           ClaimTypes.Name, ClaimTypes.Role);

                        if (loggedUser.USER_TYPE_NAME == "Admin")
                            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                        if (loggedUser.USER_TYPE_NAME == "stat")
                            identity.AddClaim(new Claim(ClaimTypes.Role, "Stat"));

                        Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
                        if(loggedUser.USER_TYPE_NAME == "stat")
                            return RedirectToAction("Index", "Statistic", new { Area = "" });
                        return RedirectToAction("Index", "Home", new { Area = "" });
                    }
                    else
                    {
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                    }                    
                }

                return View();
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return View();
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
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return View();
        }
    }
}