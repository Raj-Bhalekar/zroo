using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
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
    }


}
