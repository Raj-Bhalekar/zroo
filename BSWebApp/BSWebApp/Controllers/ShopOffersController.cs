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

        public ActionResult ViewShopOffers()
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
       

        [HttpGet]
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

                var data= new CommonAjaxCallToWebAPI().AjaxGet(@"/api/product/GetProductList", param, Convert.ToString(Session["BSWebApiToken"]));
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


        [HttpGet]
        public JsonResult GetOffersList(string offerShortDetails = "", string offerStartDate = "", string offerEndDate = "", string offerOnBrand = "",
              string isOfferOnProduct = null,
              string isActive = null,
              string bsGnd = "",
              string sord = "asc", int rows = 3, int page = 1, string sidx = "OfferID")
        {

            try
            {
                var currentShopId =
                    BSSecurityEncryption.Decrypt(bsGnd, WebAppConfig.GetConfigValue("BSGnd"));
                var param = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("offerShortDetails", offerShortDetails),
                    new KeyValuePair<string, string>("offerStartDate", offerStartDate),
                    new KeyValuePair<string, string>("offerEndDate", offerEndDate),
                    new KeyValuePair<string, string>("offerOnBrand", offerOnBrand),
                    new KeyValuePair<string, string>("isOfferOnProduct", isOfferOnProduct),
                    new KeyValuePair<string, string>("isActive", isActive),
                    new KeyValuePair<string, string>("shopId", currentShopId),
                    new KeyValuePair<string, string>("sortColumnName", sidx),
                    new KeyValuePair<string, string>("sortOrder", sord),
                    new KeyValuePair<string, string>("pageSize", Convert.ToString(rows)),
                    new KeyValuePair<string, string>("currentPage", Convert.ToString(page)),

                };

                var data = new CommonAjaxCallToWebAPI().AjaxGet(@"api/shopoffers/GetShopOffersListView", param
                    , Convert.ToString(Session["BSWebApiToken"]));
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

        [HttpGet]

        public string GetOfferImage(string id, string bsGnd)
        {
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("offerId", id),
                new KeyValuePair<string, string>("shopId", "0")
            };
            var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/shopoffers/GetOfferImage", param, Convert.ToString(Session["BSWebApiToken"]));
            var prodImageDtls = new JavaScriptSerializer().Deserialize<ImageDetails>(reslt);
            return "<img id= 'Offerid" + id + "img' name='" + prodImageDtls.ImgID + "' style='display: block; margin: 0 auto; height: 200px; width: 200px;' src = " + prodImageDtls.ImgData + " />";
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> EditOffer(ProductUpdateForm formDATA)
        {
            var ProductDetails = new JavaScriptSerializer().Deserialize<TBL_Products>(formDATA.ProductDetails);
            UserDetails currentUser = GetCurrentUserDetails();
            AddProductViewModel model = new AddProductViewModel();
            model.ProductDetails = ProductDetails;
            if (formDATA.file != null)
            {
                model.ProductImages = new List<TBL_ProductImages>();
                model.ProductImages.Add(new TBL_ProductImages()
                {
                    ImageID = CommonSafeConvert.ToInt(formDATA.imgId),
                    ProductID = ProductDetails.ProductID,
                    UpdatedBy = currentUser.UserId,
                    UpdateDate = DateTime.Now,
                    IsActive = true,
                    ProductImage = CommonSafeConvert.ConvertToBytesFromFile(formDATA.file)
                });
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WebAppConfig.GetConfigValue("WebAPIUrl"));

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = new CommonAjaxCallToWebAPI().AjaxPost("/api/product/PostEditProduct", model, Convert.ToString(Session["BSWebApiToken"])).Result;
                    //  var response = await client.PostAsJsonAsync("/api/product/PostEditProduct", model);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        var rslt = await response.Content.ReadAsStringAsync();
                        var reslt = new JavaScriptSerializer().Deserialize<BSEntityFramework_ResultType>(rslt);
                        if (reslt.Result == BSResult.FailForValidation)
                        {
                            foreach (var valerr in reslt.EntityValidationException)
                                ModelState.AddModelError("BS Errors", valerr);
                        }
                        //return reslt;
                        //FillViewDatasForAddShop();
                        var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                        return Json(allErrors, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Failed", JsonRequestBehavior.AllowGet);
                    }
                }


            }
            return Json(new
            {
                Valid = ModelState.IsValid,
                UserID = currentUser.UserId,
                //Errors = GetErrorsFromModelState(),
                Status = "Validation Failed"
            });
        }
    }
}
