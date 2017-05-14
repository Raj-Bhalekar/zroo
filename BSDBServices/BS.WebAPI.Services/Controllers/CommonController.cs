using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BS.DB.EntityFW;
using Newtonsoft.Json;
using System.Web.Http.Results;

namespace BS.WebAPI.Services.Controllers
{
    public class CommonController : ApiController
    {
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetPostalCodes(string hint)
        {
           
            PostalCodesCNFG_Activity postalActivity = new PostalCodesCNFG_Activity();
            var BSResult = postalActivity.GetPostalCodesCNFG(hint);
            return Json<object>(BSResult.Entity);
        }


        [System.Web.Http.HttpGet]
        public JsonResult<object> GetInfrastructureDetails(string postalcode)
        {
            InfrastructureCNFG_Activity infraActivity = new InfrastructureCNFG_Activity();
            var BSResult = infraActivity.GetInfrastructureByPostal(postalcode);
            return Json<object>(BSResult.Entity);

        }

        [Route("api/common/GetShopCategoryDetails")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetShopCategoryDetails()
        {
            ShopCategoryCNFG_Activity shopCatengoryActivity = new ShopCategoryCNFG_Activity();
            var BSResult = shopCatengoryActivity.GetShopCategory();
            return Json<object>(BSResult.Entity);

        }

        [Route("api/common/GetShopTypesDetails")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetShopTypesDetails()
        {
            ShopTypesCNFG_Activity shopTypeActivity = new ShopTypesCNFG_Activity();
            var BSResult = shopTypeActivity.GetShopTypes();
            return Json<object>(BSResult.Entity);

        }

        [Route("api/common/GetProductTypesDetails")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetProductTypesDetails()
        {
            ProductTypeCNFG_Activity activity = new ProductTypeCNFG_Activity();
            var BSResult = activity.GetProductType();
            return Json<object>(BSResult.Entity);

        }
        [Route("api/common/GetProductSubTypesDetails")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetProductSubTypesDetails()
        {
            ProductSubTypeCNFG_Activity activity = new ProductSubTypeCNFG_Activity();
            var BSResult = activity.GetProductSubType();
            return Json<object>(BSResult.Entity);

        }
        [Route("api/common/GetProductCategoryDetails")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetProductCategoryDetails()
        {
            ProductCategoriesCNFG_Activity activity = new ProductCategoriesCNFG_Activity();
            var BSResult = activity.GetProductCategoriesCNFG();
            return Json<object>(BSResult.Entity);

        }
        

    }


}
