using BS.DB.EntityFW;
using BS.DB.EntityFW.CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BS.WebAPI.Services.Controllers
{
    public class ShopTypesController : ApiController
    { 
     
        private ShopTypesCNFG_Activity ShopTypesActivity = new ShopTypesCNFG_Activity();
    [System.Web.Http.HttpGet]
    public JsonResult<BSEntityFramework_ResultType> GetShopTypeDetail(int id)
    {
        var BSResult = ShopTypesActivity.GetShopTypes(id);
        return Json<BSEntityFramework_ResultType>(BSResult);
    }

    [System.Web.Http.HttpGet]
    public JsonResult<BSEntityFramework_ResultType> GetAllStates()
    {
        var BSResult = ShopTypesActivity.GetAllShopTypes();
        return Json<BSEntityFramework_ResultType>(BSResult);
    }

    [HttpPost]
    public JsonResult<BSEntityFramework_ResultType> PostNewState(TBL_ShopTypes_CNFG newShopType)
    {
        var BSResult = ShopTypesActivity.InsertShopTypes(newShopType);
        return Json<BSEntityFramework_ResultType>(BSResult);
    }

    [HttpPut]
    public JsonResult<BSEntityFramework_ResultType> PutUpdateState(TBL_ShopTypes_CNFG UpdateShopType)
    {
        var BSResult = ShopTypesActivity.UpdateShopTypes(UpdateShopType);
        return Json<BSEntityFramework_ResultType>(BSResult);
    }

}
}
