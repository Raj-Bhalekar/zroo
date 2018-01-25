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
using BSWebApp.Models.Common;
using BSWebApp.Models.ViewModels;
using Newtonsoft.Json;

namespace BSWebApp.Controllers
{
   // [Common.ValidateAntiForgeryTokenWrapper(HttpVerbs.Post, null)]
    public class ProductController : BaseController
    {
        // GET: product/Create
        public ActionResult AddProduct()
        {
            // TODO: Add insert logic here
            var currentShop = GetCurrentShopId();
            if (currentShop == 0)
            {
                TempData["HardStopMessage"] = "Add shop details first!";
                if (Request != null)
                {
                    var parentUrl= Request.UrlReferrer;
                    if (parentUrl != null)
                    {
                        return Redirect(parentUrl.ToString());
                    }
                }
                return View();
            }
            else
            {
                FillViewDatasForAddShop();
                // return View(@"../shop/addshop");
                return View();

            }
          
        }

        // POST: Shops/Create
        [HttpPost]
        public async Task<ActionResult> AddProduct(AddProductViewModel model)
        {
            try
            {
                UserDetails currentUser = GetCurrentUserDetails();
                HttpPostedFileBase file = Request.Files["ImageData"];
                model.ProductImages= new List<TBL_ProductImages>();
                model.ProductImages.Add(new TBL_ProductImages()
                {
                    ProductID = -1,
                    CreatedBy = currentUser.UserId, 
                    IsActive = true,
                    ProductImage = CommonSafeConvert.ConvertToBytesFromFile(file)

                });
                model.ProductDetails.CreatedBy = currentUser.UserId;
                model.ProductDetails.ShopID = GetCurrentShopId();
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(WebAppConfig.GetConfigValue("WebAPIUrl"));

                        // Add an Accept header for JSON format.
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = await client.PostAsJsonAsync("/api/product/PostNewProduct", model);
                 
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var rslt = await response.Content.ReadAsStringAsync();
                            var reslt = new JavaScriptSerializer().Deserialize<BSEntityFramework_ResultType>(rslt);

                            foreach (var valerr in reslt.EntityValidationException)
                                ModelState.AddModelError("BS Errors", valerr);
                            //return reslt;
                            FillViewDatasForAddShop();
                            return View();
                        }
                        else
                        {
                            FillViewDatasForAddShop();
                            return View();
                        }
                    }


                }
                FillViewDatasForAddShop();
                return View("AddProduct", model);
            }
            catch (Exception ex)
            {
                FillViewDatasForAddShop();
                return View();
            }
        }

        private void FillViewDatasForAddShop()
        {
            Dictionary<string, List<SelectListItem>> SelectListData = new Dictionary<string, List<SelectListItem>>();

            var productTypes = GetProductTypes();
            var productCategory = GetProductCategory();
            var productSubType = GetProductSubTypes();
            var shopId = GetCurrentShopId();
            var brandList = GetShopBrandList(shopId);
            SelectListData.Add("ShopBrandList", brandList);
            SelectListData.Add("TBL_ProductTypes_CNFG", productTypes);
            SelectListData.Add("TBL_ProductCategory_CNFG", productCategory);
            SelectListData.Add("TBL_ProductSubType_CNFG", productSubType);

            // SelectListData.Add("TBL_ShopCategory_CNFG", shopCategory);
            ViewData["SelectListData"] = SelectListData;
        }

        private void FillViewDatasForViewProduct()
        {
            Dictionary<string, List<KeyValuePair<string,string>>> SelectListData = new Dictionary<string, List<KeyValuePair<string, string>>>();

            var productTypes = GetProductTypes();
            var productCategory = GetProductCategory();
            var productSubType = GetProductSubTypes();
            var shopId = GetCurrentShopId();
            var brandList = GetShopBrandList(shopId);
            SelectListData.Add("ShopBrandList", new CommonFunctions().ConvertSelectListItemToSimpleType(brandList));
            SelectListData.Add("TBL_ProductTypes_CNFG", new CommonFunctions().ConvertSelectListItemToSimpleType(productTypes));
            SelectListData.Add("TBL_ProductCategory_CNFG", new CommonFunctions().ConvertSelectListItemToSimpleType(productCategory));
            SelectListData.Add("TBL_ProductSubType_CNFG", new CommonFunctions().ConvertSelectListItemToSimpleType(productSubType));

            // SelectListData.Add("TBL_ShopCategory_CNFG", shopCategory);
            ViewData["SelectListData"] = SelectListData;
        }

        private List<SelectListItem> GetProductTypes()
        {
            try
            {
                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/common/GetProductTypesDetails", null,Convert.ToString(Session["BSWebApiToken"]));
                return new JavaScriptSerializer().Deserialize<List<SelectListItem>>(reslt);
            }
            catch
            {
                return null;
            }

        }

        private List<SelectListItem> GetProductSubTypes()
        {
            try
            {
                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/common/GetProductSubTypesDetails", null, Convert.ToString(Session["BSWebApiToken"]));
                return new JavaScriptSerializer().Deserialize<List<SelectListItem>>(reslt);
            }
            catch
            {
                return null;
            }

        }

        private List<SelectListItem> GetProductCategory()
        {
            try
            {
                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/common/GetProductCategoryDetails", null, Convert.ToString(Session["BSWebApiToken"]));
                return new JavaScriptSerializer().Deserialize<List<SelectListItem>>(reslt);
            }
            catch
            {
                return null;
            }

        }

        public ActionResult ViewProducts()
        {

            var shopId = GetCurrentShopId();
            ViewData["SpGnd"] = BSSecurityEncryption.Encrypt(Convert.ToString(shopId),
                WebAppConfig.GetConfigValue("BSGnd"));
            FillViewDatasForAddShop();
            // return View(@"../shop/addshop");
            return View();
        }


        [HttpGet]
            public JsonResult GetProductList(string prodName ="", string brandName ="", string barCode="", string productType="",
                string isAvailable="true",
                string availableQty="",
                string isActive="true",
                string prodCategory="",
                string prodSubType="",
                string prodMrp="",
                string prodShopPrice="",
            string bsGnd = "", string sidx = "ProductName", string sord = "asc", int rows = 3, int page = 1)
        {

            try
            {
                var currentShopId =
                    BSSecurityEncryption.Decrypt(bsGnd, WebAppConfig.GetConfigValue("BSGnd"));
                var param = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("prodName", prodName),
                    new KeyValuePair<string, string>("brandName", brandName),
                    new KeyValuePair<string, string>("barCode", barCode),
                    new KeyValuePair<string, string>("productType", productType),
                    new KeyValuePair<string, string>("isAvailable", isAvailable),
                    new KeyValuePair<string, string>("availableQty", availableQty),
                    new KeyValuePair<string, string>("isActive", isActive),
                    new KeyValuePair<string, string>("prodCategory", prodCategory),
                    new KeyValuePair<string, string>("prodSubType", prodSubType),
                    new KeyValuePair<string, string>("prodMrp", prodMrp),
                    new KeyValuePair<string, string>("prodShopPrice", prodShopPrice),
                    new KeyValuePair<string, string>("shopId", currentShopId),
                    new KeyValuePair<string, string>("sortColumnName", sidx),
                    new KeyValuePair<string, string>("sortOrder", sord),
                    new KeyValuePair<string, string>("pageSize", Convert.ToString(rows)),
                    new KeyValuePair<string, string>("currentPage", Convert.ToString(page)),
                    
                };

                var data = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/product/GetProductListView", param
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> EditProduct(ProductUpdateForm formDATA)
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
                Errors = GetErrorsFromModelState(),
                Status = "Validation Failed"
            });
        }

        [HttpGet]
        
        public string GetProdImage(string id, string bsGnd)
        {
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("prodId", id),
                new KeyValuePair<string, string>("shopId", "0")
            };
            var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/product/GetProductImage", param, Convert.ToString(Session["BSWebApiToken"]));
           var prodImageDtls=  new JavaScriptSerializer().Deserialize<ImageDetails>(reslt);
            return "<img id= 'Prodid"+id+"img' name='"+ prodImageDtls.ImgID + "' style='display: block; margin: 0 auto; height: 200px; width: 200px;' src = " + prodImageDtls.ImgData + " />" ;
        }


        private Dictionary<string, string> GetErrorsFromModelState()
        {
            var errors = new Dictionary<string, string>();

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

    }
}