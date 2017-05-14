using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BS.DB.EntityFW;
using BSWebApp.Common;
using BSWebApp.Models.ViewModels;
using Newtonsoft.Json;

namespace BSWebApp.Areas.Product.Controllers
{
    [Common.ValidateAntiForgeryTokenWrapper(HttpVerbs.Post, null)]
    public class ProductController : Controller
    {
        // GET: product/Create
        public ActionResult AddProduct()
        {
            // TODO: Add insert logic here
             FillViewDatasForAddShop();
            return View();
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
    }
}
