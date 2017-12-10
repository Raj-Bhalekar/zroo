using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BS.DB.EntityFW;
using BSWebApp.Common;
using BSWebApp.Models.Common;
using BSWebApp.Models.ViewModels;

namespace BSWebApp.Controllers
{
    public class ShopOffersController : BaseController
    {
      

        // GET: ShopOffers/Create
        public ActionResult AddShopOffer()
        {
            FillViewDatasForAddShopOffers();
            return View();
        }
       
        
        [HttpPost]
        public async Task<ActionResult> AddShopOffer(AddShopOffersViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        var currentUserId = CommonSafeConvert.ToInt(Session["CurrentUserID"]);
                        model.ShopOffer.ShopID = GetCurrentShopId();
                        model.ShopOffer.CreatedBy = currentUserId;
                        
                        var selectedProductList = new JavaScriptSerializer().Deserialize<List<SelectedProductList>>(model.SelectedProductJson);
                        model.OfferonProducts= new List<TBL_OfferOnProducts>();
                        selectedProductList.ForEach(x=> model.OfferonProducts.Add(new TBL_OfferOnProducts() { ProductID = x.ProductID, OfferID = model.ShopOffer.OfferID}));

                        client.BaseAddress = new Uri(WebAppConfig.GetConfigValue("WebAPIUrl"));
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = await client.PostAsJsonAsync("/api/shopoffers/PostNewShopOffers", model);

                       // var response = new CommonAjaxCallToWebAPI().AjaxPost(@"/api/shopoffers/PostNewShopOffers", model);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var rslt = await response.Content.ReadAsStringAsync();
                            var reslt = new JavaScriptSerializer().Deserialize<BSEntityFramework_ResultType>(rslt);
                            if (reslt.Result == BSResult.FailForValidation)
                            {
                                foreach (var valerr in reslt.EntityValidationException)
                                    ModelState.AddModelError("BS Errors", valerr);
                            }
                            else
                            {
                                ModelState.AddModelError("BS Errors", reslt.ResultMsg);
                            }
                            //return reslt;
                            FillViewDatasForAddShopOffers();
                            return View();
                        }
                        else
                        {
                            FillViewDatasForAddShopOffers();
                            return View();
                        }
                    }


                }
                FillViewDatasForAddShopOffers();
                return View("AddShopOffer", model);
            }
            catch (Exception ex)
            {
                FillViewDatasForAddShopOffers();
                return View();
            }
        }


        private void FillViewDatasForAddShopOffers()
        {
            var shopId = GetCurrentShopId();
            Dictionary<string, List<SelectListItem>> selectListData = new Dictionary<string, List<SelectListItem>>();
            var brandList = GetShopBrandList(shopId);
            selectListData.Add("ShopBrandList", brandList);
            ViewData["SelectListData"] = selectListData;

            ViewData["SpGnd"] = BSSecurityEncryption.Encrypt(Convert.ToString(shopId),
                WebAppConfig.GetConfigValue("BSGnd"));
        }
        private List<SelectListItem> GetShopBrandList(int shopId)
        {
            try
            {
                var param = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("shopId", shopId.ToString())
                };

                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/shop/GetallbrandList", param);
                return new JavaScriptSerializer().Deserialize<List<SelectListItem>>(reslt);
            }
            catch
            {
                return null;
            }

        }


        [HttpGet]
       // public JsonResult GetProductList(string brand)
            public JsonResult GetProductList(string bsGnd ="", string brand = "", string sidx = "ProductName", string sord = "asc", int rows = 3, int page = 1)
        {
        
            try
            {
                var currentShopId =
                    BSSecurityEncryption.Decrypt(bsGnd, WebAppConfig.GetConfigValue("BSGnd"));
                var param = new List<KeyValuePair<string, string>>
                {
                new KeyValuePair<string, string>("shopId", currentShopId),
                new KeyValuePair<string, string>("sortColumnName", sidx),
                    new KeyValuePair<string, string>("sortOrder", sord),
                    new KeyValuePair<string, string>("pageSize", Convert.ToString(rows)),
                    new KeyValuePair<string, string>("currentPage", Convert.ToString(page)),
                    new KeyValuePair<string, string>("brand", Convert.ToString(brand))
                };

                var data= new CommonAjaxCallToWebAPI().AjaxGet(@"/api/product/GetProductList", param);
                //  return Json(data, JsonRequestBehavior.AllowGet);

                var jqGridData = new JqGridType()
                {
                    Data = data,
                    Page = "1",
                    PageSize = "3", // u can change this !
                    SortColumn = "1",
                    SortOrder = "asc"
                };

                return Json(jqGridData, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return null;
            }
        }

    }
}
