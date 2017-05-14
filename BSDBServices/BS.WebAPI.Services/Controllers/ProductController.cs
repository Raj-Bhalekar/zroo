using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BS.DB.EntityFW;
using BS.DB.EntityFW.CommonTypes;
using System.Web.Http.Results;
using BS.DB.EntityFW.ViewModels;

namespace BS.WebAPI.Services.Controllers
{
    public class ProductController : ApiController
    {
        private Product_Activity ProductsActivity = new Product_Activity();
        [HttpPost]
        public JsonResult<BSEntityFramework_ResultType> PostNewProduct(AddProductViewModel newProduct)
        {
            var BSResult = ProductsActivity.InsertProducts(newProduct);
            // var newShopId = ((TBL_Shops) BSResult.Entity).ShopID;

            return Json<BSEntityFramework_ResultType>(BSResult);
        }
    }
}
