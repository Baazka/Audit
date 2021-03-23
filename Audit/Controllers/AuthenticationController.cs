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

                        if (loggedUser.USER_TYPE_NAME.ToUpper() == "ADMIN")
                            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                        if (loggedUser.USER_TYPE_NAME.ToUpper() == "HEAD_AUDITOR")
                            identity.AddClaim(new Claim(ClaimTypes.Role, "Head_Auditor"));
                        if (loggedUser.USER_TYPE_NAME.ToUpper() == "BRANCH_AUDITOR")
                            identity.AddClaim(new Claim(ClaimTypes.Role, "Branch_Auditor"));
                        if (loggedUser.USER_TYPE_NAME.ToUpper() == "DIRECTOR")
                            identity.AddClaim(new Claim(ClaimTypes.Role, "Director"));

                        Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);

                        //return RedirectToAction("Index", "Home", new { Area = "" });
                        return RedirectToAction("Home", "Home", new { Area = "" });
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