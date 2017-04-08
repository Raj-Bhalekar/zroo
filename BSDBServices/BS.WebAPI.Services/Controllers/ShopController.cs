using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BS.DB.EntityFW;
using BS.DB.EntityFW.CommonTypes;
using System.Web.Http.Results;

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
        public JsonResult<BSEntityFramework_ResultType> PostNewShopes(TBL_ShopLoginDetails newShope)
        {
            var BSResult = ShopesActivity.InsertShope(newShope);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        [HttpPut]
        public JsonResult<BSEntityFramework_ResultType> PutUpdateShopes(TBL_ShopLoginDetails updateShopes)
        {
            var BSResult = ShopesActivity.UpdateShope(updateShopes);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }



    }
}
