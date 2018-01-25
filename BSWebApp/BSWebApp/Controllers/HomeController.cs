using BSWebApp.Common;
using BSWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using BSWebApp.Models.Common;
using Newtonsoft.Json;

namespace BSWebApp.Controllers
{
    [ValidateAntiForgeryTokenWrapper(HttpVerbs.Post, null)]

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult About(LoginViewModel vm)
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(LoginViewModel model, string returnUrl)
        {
            // Lets first check if the Model is valid or not
            if (ModelState.IsValidField("signUpModel"))
            {
                    var response = new CommonAjaxCallToWebAPI().AjaxPost("/api/Home/SignUp",model.signUpModel,"").Result;

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        var rslt = await response.Content.ReadAsStringAsync();
                        var reslt = new JavaScriptSerializer().Deserialize<BSEntityFramework_ResultType>(rslt);
                        if (reslt.Result == BSResult.FailForValidation)
                        {
                            foreach (var valerr in reslt.EntityValidationException)
                                ModelState.AddModelError("BS Errors", valerr);
                        }
                        else if (reslt.Result == BSResult.Success)
                        {
                            ModelState.AddModelError("ServerError", reslt.ResultMsg);
                            return ValidateModel("FailedFromServer", null, "SignUp / Login");
                        }
                        else
                        {
                            ModelState.AddModelError("BS Errors", reslt.ResultMsg);
                        }
                        var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                        return Json(allErrors, JsonRequestBehavior.AllowGet);
                    }
                    return Json("Failed", JsonRequestBehavior.AllowGet);
            }
            return ValidateModel("", null,"SignUp / Login");
        }


        public JsonResult ValidateModel(string status, LoginResult data,string userId)
        {
            bool isValid = ModelState.IsValid;
            string menuData = CommonSafeConvert.GetMenuString(data.MenuDetailList);
            Session["HomeLinkMenuList"] = CommonSafeConvert.GetHomeLinkPageData(data.MenuDetailList);
            Session["menuData"] = menuData;
            return Json(new
            {
                Valid = isValid,
                UserID=userId,
                NewId = menuData,
                ShopList = GetShoplistString(data),
                Errors = GetErrorsFromModelState(),
                Status = !string.IsNullOrWhiteSpace(status) ?
                   status :
                   isValid ? "Success" : "Failure"
            });
        }

        public string GetMenuString(LoginResult data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<ul class='sidebar-nav'>");
            if(data?.MenuDetailList != null)
            foreach (var menu in data.MenuDetailList.Where(x=>x.ParentMenuId==null || x.ParentMenuId == 0))
            {
                sb.Append(@"<li  class='sidebar-brand'><span>");
                sb.Append(@"<span class='willBeInvisible'>
                <a class='tab' href = '"
                + menu.MenuURL+"' > " + @"<span class='glyphiconglyphicon-shopping-cart'>"
                + menu.MenuName
                 + "</span> </a ></span>");
                sb.Append(@"</span >");
                var listSubMenu = data.MenuDetailList.Where(xy => xy.ParentMenuId == menu.MenuID);
                if (listSubMenu.Any())
                {
                    sb.Append(@"<ul>");
                    foreach (var submenu in data.MenuDetailList.Where(xy => xy.ParentMenuId == menu.MenuID))
                    {

                        sb.Append(@"<li style = 'text-align: center' class='Items'><span>");
                        sb.Append(@"<span><a href = '" + submenu.MenuURL + "' > " +
                                  submenu.MenuName + " </a ></span>");
                        sb.Append(@"</span >");
                        sb.Append(@" </li> ");
                    }
                    sb.Append(@"</ul>");
                }

                sb.Append(@" </li> ");
            }
            sb.Append(@" </ul >");
            return sb.ToString();
        }

        private string GetShoplistString(LoginResult data)
        {
            if (data?.ShopAssignedList != null)
            {
                var DefaultShopId = data.ShopAssignedList.First().ShopId;
                Session["CurrentShopID"] = DefaultShopId;
                StringBuilder sb = new StringBuilder();
            sb.Append(@" <ul><li class='sub-menu-parent'>");
            sb.Append(@"<a href = '#' style = 'width: 200px'> "+ data.ShopAssignedList.First().ShopName + " &#9662;</a>");
                sb.Append("<ul class='sub-menu'>");
            if (data?.ShopAssignedList != null)
                foreach (var menu in data.ShopAssignedList)
                {
                    sb.Append("<li> <a href='/Shop/ChangeShop/" + menu.ShopId + "' class='LoggOffLinkClass'>" + menu.ShopName +"</a></li>");
                   
                }
            sb.Append(@" </ul >");
            Session["ShopAssignedList"] = sb.ToString();

            return sb.ToString();
            }
            else
            {
                return null;
            }
        }

        private Dictionary<string, string> GetErrorsFromModelState()
        {
            var errors = new Dictionary<string,string>();

            foreach (var key in ModelState.Keys)
            {
                // Only send the errors to the client.
             //   if (ModelState[key].Errors.Count > 0)
                {
                    var firstError = ModelState[key].Errors.FirstOrDefault();
                    if (firstError != null)
                    {
                        errors[key] = firstError.ErrorMessage;
                    }
                    else
                    {
                        errors[key] = null;

                    }
                }
            }
            return errors;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            // Lets first check if the Model is valid or not
            if (ModelState.IsValidField("loginModel"))
            {
              //  var response = new CommonAjaxCallToWebAPI().AjaxPost("/api/home/ValidateLogin", model.loginModel).Result;
                var responseToken = new CommonAjaxCallToWebAPI().AjaxPostToken(model.loginModel).Result;
                if (string.IsNullOrWhiteSpace(responseToken.Error))
                {
                    Session["BSWebApiToken"] = responseToken.AccessToken;
                    model.loginModel.Password = null;
                    var response = new CommonAjaxCallToWebAPI().AjaxPost("/api/home/ValidateLogin", model.loginModel,responseToken.AccessToken).Result;
                    var rslt = await response.Content.ReadAsStringAsync();
                    var reslt = new JavaScriptSerializer().Deserialize<BSEntityFramework_ResultType>(rslt);
                    bool userValid = false;
                    var loginResult =
                        new JavaScriptSerializer().Deserialize<LoginResult>(
                            (new JavaScriptSerializer().Serialize(reslt.Entity)));

                    if (loginResult != null && !(Convert.ToBoolean(loginResult.IsValid)))
                    {
                        if (reslt.Result == BSResult.FailForValidation)
                        {
                            foreach (var valerr in reslt.EntityValidationException)
                                ModelState.AddModelError("BS Errors", valerr);
                        }
                        else
                        {
                            ModelState.AddModelError("BS Errors", reslt.ResultMsg);
                        }
                    }
                    else
                    {
                        userValid = true;
                    }
                    // User found in the database

                    if (userValid)
                    {

                        FormsAuthentication.SetAuthCookie(model.loginModel.UserName, false);
                        var authTicket = new FormsAuthenticationTicket(1, model.loginModel.UserName, DateTime.Now,
                            DateTime.Now.AddMinutes(20), false, Convert.ToString(loginResult.UserId));
                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        HttpContext.Response.Cookies.Add(authCookie);
                        ModelState.AddModelError("ServerSuccess", reslt.ResultMsg);

                        if (loginResult?.MenuDetailList != null)
                        {
                            return ValidateModel("ServerSuccess", loginResult, model.loginModel.UserName);
                        }
                        return ValidateModel("ServerSuccess", null, "SignUp / Login");
                    }
                    ModelState.AddModelError("ServerError", "The user name or password provided is incorrect.");
                    return ValidateModel("FailedFromServer", null, "SignUp / Login");
                }
                ModelState.AddModelError("ServerError", "The user name or password provided is incorrect.");
                return ValidateModel("FailedFromServer", null, "SignUp / Login");
            }
            return ValidateModel("",null,"SignUp / Login");
        }

        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    // Lets first check if the Model is valid or not
        //    if (ModelState.IsValidField("loginModel"))
        //    {
        //        //  var response = new CommonAjaxCallToWebAPI().AjaxPost("/api/home/ValidateLogin", model.loginModel).Result;
        //        var response = new CommonAjaxCallToWebAPI().AjaxPostToken(model.loginModel).Result;
        //        if (string.IsNullOrWhiteSpace(response.Error))
        //        {
        //            var rslt = await response.Content.ReadAsStringAsync();
        //            var reslt = new JavaScriptSerializer().Deserialize<BSEntityFramework_ResultType>(rslt);
        //            bool userValid = false;
        //            var loginResult =
        //                new JavaScriptSerializer().Deserialize<LoginResult>(
        //                    (new JavaScriptSerializer().Serialize(reslt.Entity)));

        //            if (loginResult != null && !(Convert.ToBoolean(loginResult.IsValid)))
        //            {
        //                if (reslt.Result == BSResult.FailForValidation)
        //                {
        //                    foreach (var valerr in reslt.EntityValidationException)
        //                        ModelState.AddModelError("BS Errors", valerr);
        //                }
        //                else
        //                {
        //                    ModelState.AddModelError("BS Errors", reslt.ResultMsg);
        //                }
        //            }
        //            else
        //            {
        //                userValid = true;
        //            }
        //            // User found in the database

        //            if (userValid)
        //            {

        //                FormsAuthentication.SetAuthCookie(model.loginModel.UserName, false);
        //                var authTicket = new FormsAuthenticationTicket(1, model.loginModel.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, Convert.ToString(loginResult.UserId));
        //                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
        //                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        //                HttpContext.Response.Cookies.Add(authCookie);
        //                ModelState.AddModelError("ServerSuccess", reslt.ResultMsg);

        //                if (loginResult?.MenuDetailList != null)
        //                {
        //                    return ValidateModel("ServerSuccess", loginResult, model.loginModel.UserName);
        //                }
        //                return ValidateModel("ServerSuccess", null, "SignUp / Login");
        //            }
        //            ModelState.AddModelError("ServerError", "The user name or password provided is incorrect.");
        //            return ValidateModel("FailedFromServer", null, "SignUp / Login");
        //        }

        //        // If we got this far, something failed, redisplay form
        //        return View();

        //    }
        //    return ValidateModel("", null, "SignUp / Login");
        //}


        //  [NoCache]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            // clear authentication cookie
            var cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            var sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            var cookie2 = new HttpCookie(sessionStateSection.CookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(cookie2);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult HomeLink()
        {
            string message = Convert.ToString(TempData["HardStopMessage"]);
            ViewBag.HardStopMessage = message;
            return View();
        }

    }
}