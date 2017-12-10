using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSWebApp.Common
{
    public class BaseController : Controller
    {
        public UserDetails GetCurrentUserDetails()
        {
            return new UserDetails()
            {
                UserId = CommonSafeConvert.ToInt(Session["CurrentUserID"]),
                UserLoginName = Convert.ToString(Session["CurrentUser"])
            };
        }

        public int GetCurrentShopId()
        {
            return CommonSafeConvert.ToInt(Session["CurrentShopID"]);
        }
    }
}