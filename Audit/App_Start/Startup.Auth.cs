using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Audit.App_Func;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Audit
{
	public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                ExpireTimeSpan = TimeSpan.FromSeconds(AppStatic.SessionTimeOut),
                SlidingExpiration = true,
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(url.Action("Login", "Authentication")),
                LogoutPath = new PathString("/")
            });
        }
    }
}