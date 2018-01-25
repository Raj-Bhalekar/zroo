using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        public List<SelectListItem> GetShopBrandList(int shopId)
        {
            try
            {
                var param = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("shopId", shopId.ToString())
                };

                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/shop/GetallbrandList", param, Convert.ToString(Session["BSWebApiToken"]));
                return new JavaScriptSerializer().Deserialize<List<SelectListItem>>(reslt);
            }
            catch
            {
                return null;
            }

        }
    }
}