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
    public class StateController : ApiController
    {
        private StatesCNFG_Activity StateActivity = new StatesCNFG_Activity();
        [System.Web.Http.HttpGet]
        public JsonResult<BSEntityFramework_ResultType> GetStateDetail(int id)
        {
           var BSResult= StateActivity.GetState(id);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        [System.Web.Http.HttpGet]
        public JsonResult<BSEntityFramework_ResultType> GetAllStates()
        {
            var BSResult = StateActivity.GetALLStates();
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        [HttpPost]
        public JsonResult<BSEntityFramework_ResultType> PostNewState(TBL_States_CNFG newState)
        {
            var BSResult = StateActivity.InsertState(newState);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }
      
        [HttpPut]
        public JsonResult<BSEntityFramework_ResultType> PutUpdateState(TBL_States_CNFG newState)
        {
            var BSResult = StateActivity.UpdateState(newState);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }



    }
}
