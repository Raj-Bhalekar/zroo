using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using BSWebApp.Common;

namespace BSWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private string CurrentUserName = "";
        private int CurrentUserID = 0;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
            {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (authTicket != null && !authTicket.Expired)
                    {
                        var roles = authTicket.UserData.Split(',');
                        HttpContext.Current.User =
                            new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                        CurrentUserName = authTicket.Name;
                        CurrentUserID = (roles != null && roles.Length > 0) ? CommonSafeConvert.ToInt(roles[0]) : 0;
                    }
                else
                {
                    CurrentUserName = "";
                    CurrentUserID = 0;
                }
            }
                else
                {
                    CurrentUserName = "";
                    CurrentUserID = 0;
                }
            }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (context != null && context.Session != null)
            {
                Session["CurrentUser"] = CurrentUserName;
                Session["CurrentUserID"] = CurrentUserID;
            }
        }
    }
}
