using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BS.DB.EntityFW;
using BS.DB.EntityFW.CommonTypes;
using System.Web.Http.Results;
using System.Collections.Generic;

namespace BS.WebAPI.Services.Controllers
{
    public class Shops5TopProductsController : ApiController
    {
        private Shops5TopProducts_Activity Shops5TopProductsActivity = new Shops5TopProducts_Activity();
        [System.Web.Http.HttpGet]
        public JsonResult<BSEntityFramework_ResultType> GetShops5TopProductsDetail(int id)
        {
            var BSResult = Shops5TopProductsActivity.GetShops5TopProducts(id);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

      
        [HttpPost]
        public JsonResult<BSEntityFramework_ResultType> PostNewShops5TopProducts(List<TBL_Shops5TopProducts> newShops5TopProducts)
        {
            var BSResult = Shops5TopProductsActivity.InsertTop5ProductForShop(newShops5TopProducts);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        //[HttpPut]
        //public JsonResult<BSEntityFramework_ResultType> PutUpdateShops5TopProducts(TBL_Shops5TopProductss_CNFG newShops5TopProducts)
        //{
        //    var BSResult = Shops5TopProductsActivity.UpdateShops5TopProducts(newShops5TopProducts);
        //    return Json<BSEntityFramework_ResultType>(BSResult);
        //}



    }
}
