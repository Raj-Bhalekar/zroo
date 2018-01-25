using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BS.DB.EntityFW;
using BSWebApp.Common;
using BSWebApp.Models;
using BSWebApp.Models.Common;
using BSWebApp.Models.ViewModels;
using Newtonsoft.Json;

namespace BSWebApp.Controllers
{
    [Common.ValidateAntiForgeryTokenWrapper(HttpVerbs.Post, null)]
    [Authorize]
    public class ShopController : BaseController
    {


        // GET: Shops/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Shops/Create

        public ActionResult AddShop()
        {
            FillViewDatasForAddShop();
            return View();
        }

        private void FillViewDatasForAddShop()
        {
            Dictionary<string, List<SelectListItem>> SelectListData = new Dictionary<string, List<SelectListItem>>();

            var shopTypes = GetShopTypes();
            var shopCategory = GetShopCategories();

            SelectListData.Add("TBL_ShopTypes_CNFG", shopTypes);

            SelectListData.Add("TBL_ShopCategory_CNFG", shopCategory);
            ViewData["SelectListData"] = SelectListData;
        }

        // POST: Shops/Create
        [Authorize]
        [HttpPost]
        public ActionResult AddShop(AddShopViewModel model)
        {
            try
            {
                var currentUserId = CommonSafeConvert.ToInt(Session["CurrentUserID"]);
                if (!LoginResult.VerifyUserId(currentUserId))
                {

                }

                if (ModelState.IsValid)
                {

                        model.ShopDetails.CreatedBy = currentUserId;
                        model.ShopPostalDetails.CreatedBy = currentUserId;
                        model.ShopPostalDetails.IsActive = true;
                     
                        var response = new CommonAjaxCallToWebAPI().AjaxPost("/api/shop/PostNewShopes", model,Convert.ToString(Session["BSWebApiToken"])).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var rslt = response.Content.ReadAsStringAsync().Result;
                            return Json(new JavaScriptSerializer().Deserialize<object>(rslt),
                                JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return null;
                        }
                


                }
                FillViewDatasForAddShop();
                return View("AddShop", model);
            }
            catch
            {
                FillViewDatasForAddShop();
                return View();
            }
        }

        // GET: Shops/Edit/5
        public ActionResult Edit()
        {
            var response = new CommonAjaxCallToWebAPI().AjaxPost("/api/shop/EditShopDetails", GetCurrentShopId(),Convert.ToString(Session["BSWebApiToken"])).Result;
            if (response.IsSuccessStatusCode)
            {
                var rslt = response.Content.ReadAsStringAsync().Result;
                var BSresult = new JavaScriptSerializer().Deserialize<BSEntityFramework_ResultType>(rslt);
                if (BSresult.Result == BSResult.Success)
                {
                    return View(BSresult.Entity);
                }
                if (BSresult.Result == BSResult.FailForValidation)
                {
                    foreach (var valerr in BSresult.EntityValidationException)
                        ModelState.AddModelError("BS Errors", valerr);
                }
                else
                {
                    ModelState.AddModelError("BS Errors", BSresult.ResultMsg);
                }
                return View();
            }
            ModelState.AddModelError("BS Errors", "Technical Error");
            return View();
        }

        // POST: Shops/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shops/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shops/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public JsonResult GetPostalsAutoComplete(string hint)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebAppConfig.GetConfigValue("WebAPIUrl"));

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/common?hint=" + hint).Result;

            if (response.IsSuccessStatusCode)
            {
                var rslt = response.Content.ReadAsStringAsync().Result;
                return Json(new JavaScriptSerializer().Deserialize<object>(rslt), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }

        public JsonResult GetInfrastureDetails(string postalCode)
        {
            try
            {
                var paramlist = new List<KeyValuePair<string, string>>();
                var param = new KeyValuePair<string, string>("postalCode", postalCode);
                paramlist.Add(param);

                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/common/GetInfrastructureDetails", paramlist, Convert.ToString(Session["BSWebApiToken"]));
                return Json(new JavaScriptSerializer().Deserialize<object>(reslt), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null;
            }

        }


        private List<SelectListItem> GetShopCategories()
        {
            try
            {
                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/common/GetShopCategoryDetails", null, Convert.ToString(Session["BSWebApiToken"]));
                return new JavaScriptSerializer().Deserialize<List<SelectListItem>>(reslt);
            }
            catch
            {
                return null;
            }

        }

        private List<SelectListItem> GetShopTypes()
        {
            try
            {
                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/common/GetShopTypesDetails", null, Convert.ToString(Session["BSWebApiToken"]));
                return new JavaScriptSerializer().Deserialize<List<SelectListItem>>(reslt);
            }
            catch
            {
                return null;
            }

        }

        private string GetShopAddress()
        {
            try
            {
              var shopId=  GetCurrentShopId();
                var paramlist = new List<KeyValuePair<string, string>>();
                var param = new KeyValuePair<string, string>("shopId", Convert.ToString(shopId));
                paramlist.Add(param);
                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/shop/GetShopAddress", paramlist, Convert.ToString(Session["BSWebApiToken"]));
                return new JavaScriptSerializer().Deserialize<string>(reslt);
            }
            catch
            {
                return "India";
            }

        }


        private MapAddressViewModel GetShopMapAddress()
        {
            try
            {
                var shopId = GetCurrentShopId();
                var paramlist = new List<KeyValuePair<string, string>>();
                var param = new KeyValuePair<string, string>("shopId", Convert.ToString(shopId));
                paramlist.Add(param);
                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/shop/GetShopMapDetails", paramlist, Convert.ToString(Session["BSWebApiToken"]));
                var finalresult= new JavaScriptSerializer().Deserialize<BSEntityFramework_ResultType>(reslt);
                return
                           new JavaScriptSerializer().Deserialize<MapAddressViewModel>(
                               (new JavaScriptSerializer().Serialize(finalresult.Entity)));
            }
            catch
            {
                return new MapAddressViewModel() { Address = "India"};
            }

        }

        private ShopChangeStatus ValidateAndChangeShop(int shopId)
        {
            try
            {
                var currentUserId = CommonSafeConvert.ToInt(Session["CurrentUserID"]);
               
                    var changeShopRequestobject = new ShopChangeRequest();

                    changeShopRequestobject.ShopId = shopId;
                    changeShopRequestobject.Userid = currentUserId;
                var response = new CommonAjaxCallToWebAPI().AjaxPost("/api/common/ValidateAndChangeShop", changeShopRequestobject,Convert.ToString(Session["BSWebApiToken"])).Result;
                
                    if (response.IsSuccessStatusCode)
                    {
                        var rslt = response.Content.ReadAsStringAsync().Result;
                        var shopChangeStatus = new JavaScriptSerializer().Deserialize<ShopChangeStatus>(rslt);
                        return shopChangeStatus;
                    }
                    else
                    {
                        return null;
                    }
              

            }
            catch
            {
                return null;
            }


        }

        public ActionResult ChangeShop(string id)
        {
            var shopId = CommonSafeConvert.ToInt(id);
            if (shopId > 0)
            {
                var shopChangeStatus = ValidateAndChangeShop(shopId);
                ViewBag.ShopChangeStatus = shopChangeStatus;
                Session["menuData"] = CommonSafeConvert.GetMenuString(shopChangeStatus.MenuList);
                Session["CurrentShopID"] = shopId;
            }
            else
            {
                ViewBag.ShopChangeStatus = new ShopChangeStatus()
                {
                    IsSuccess = false,
                    Message = "Invalid Shop. Please logoff and login again."
                };
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public ActionResult ShopMapAddress()
        {
            var shopAddress = GetShopAddress();
            var shopMapViewModel = GetShopMapAddress();
            return View(shopMapViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ShopMapAddress(MapAddressViewModel model)
        {
            try
            {
                UserDetails currentUser = GetCurrentUserDetails();
                model.shopMapDetails.CreateBy= currentUser.UserId;
                model.shopMapDetails.ShopId = GetCurrentShopId();
                if (ModelState.IsValidField("shopMapDetails"))
                {
                    using (var client = new HttpClient())
                    {
                        var response = new CommonAjaxCallToWebAPI().AjaxPost("/api/shop/PostShopMapDetails", model.shopMapDetails, Convert.ToString(Session["BSWebApiToken"])).Result;
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var rslt = await response.Content.ReadAsStringAsync();
                            var reslt = new JavaScriptSerializer().Deserialize<BSEntityFramework_ResultType>(rslt);
                            if (reslt.Result == BSResult.Success)
                            {
                                return View("~/Views/Product/AddProduct.cshtml");
                            }
                            else
                            {
                                foreach (var valerr in reslt.EntityValidationException)
                                    ModelState.AddModelError("BS Errors", valerr);
                                return View();
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("BS Errors", "Internal server error");
                            return View();
                        }
                    }


                }
           
                return View();
            }
            catch (Exception ex)
            {
              //  FillViewDatasForAddShop();
                return View();
            }
        }

    }
}
