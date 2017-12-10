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
    [Common.ValidateAntiForgeryTokenWrapper(HttpVerbs.Post, null)]
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
                        // StringContent content = new StringContent(JsonConvert.SerializeObject(model));//, Encoding.UTF8, "application/json");

                        //  var response = await client.PostAsJsonAsync("/api/product/PostNewProduct", content);
                        //var response2 =
                        //   client.PostAsJsonAsync("/api/product/PostNewProduct", content).Result.Content.ReadAsStringAsync();
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

            SelectListData.Add("TBL_ProductTypes_CNFG", productTypes);
            SelectListData.Add("TBL_ProductCategory_CNFG", productCategory);
            SelectListData.Add("TBL_ProductSubType_CNFG", productSubType);

            // SelectListData.Add("TBL_ShopCategory_CNFG", shopCategory);
            ViewData["SelectListData"] = SelectListData;
        }

        private List<SelectListItem> GetProductTypes()
        {
            try
            {
                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/common/GetProductTypesDetails", null);
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
                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/common/GetProductSubTypesDetails", null);
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
                var reslt = new CommonAjaxCallToWebAPI().AjaxGet(@"/api/common/GetProductCategoryDetails", null);
                return new JavaScriptSerializer().Deserialize<List<SelectListItem>>(reslt);
            }
            catch
            {
                return null;
            }

        }

        public ActionResult ViewProduct()
        {

            return View();
        }

    }
}