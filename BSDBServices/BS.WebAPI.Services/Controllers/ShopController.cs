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
    public class ShopController : ApiController
    {
        private Shopes_Activity ShopesActivity = new Shopes_Activity();
        [System.Web.Http.HttpGet]
        public JsonResult<BSEntityFramework_ResultType> GetShopesDetail(int id)
        {
            var BSResult = ShopesActivity.GetShope(id);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        

        [System.Web.Http.HttpGet]
        public JsonResult<BSEntityFramework_ResultType> GetAllShopess()
        {
            var BSResult = ShopesActivity.GetAllShope();
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        [HttpPost]
        public JsonResult<BSEntityFramework_ResultType> PostNewShopes(AddShopViewModel newShop)
        {
            var BSResult = ShopesActivity.InsertShope(newShop);
           // var newShopId = ((TBL_Shops) BSResult.Entity).ShopID;

            
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        [HttpPost]
        [Route("api/shop/EditShopDetails")]
        public JsonResult<BSEntityFramework_ResultType> EditShopDetails(int shopId)
        {
            var BSResult = ShopesActivity.GetShopDetails(shopId);
           
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        [Route("api/shop/PostShopMapDetails")]
        [HttpPost]
        public JsonResult<BSEntityFramework_ResultType> PostShopMapDetails(TBL_ShopMapDetails newShopMapDetails)
        {
            var BSResult = ShopesActivity.InsertShopMapDetails(newShopMapDetails);
            return Json(BSResult);
        }

        [HttpPut]
        public JsonResult<BSEntityFramework_ResultType> PutUpdateShopes(TBL_ShopLoginDetails updateShopes)
        {
            var BSResult = ShopesActivity.UpdateShope(updateShopes);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        [Route("api/shop/GetallbrandList")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetShopAllbrandList(int shopid)
        {
            Shopes_Activity activity = new Shopes_Activity();
            var BSResult = activity.GetShopAllBrands(shopid);
            return Json<object>(BSResult.Entity);
            
        }

        [Route("api/shop/GetShopAddress")]
        [System.Web.Http.HttpGet]
        public string GetShopAddress(int shopId)
        {
          var BSResult = ShopesActivity.GetShopAddress(shopId);
            return Convert.ToString(BSResult.Entity);

        }
        [Route("api/shop/GetShopMapDetails")]
        [System.Web.Http.HttpGet]
        public JsonResult<BSEntityFramework_ResultType> GetShopMapDetails(int shopId)
        {
            var BSResult = ShopesActivity.GetShopMapDetails(shopId);
            return Json(BSResult);
        }

    }
}
