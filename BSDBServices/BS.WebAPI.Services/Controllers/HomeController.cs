using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BS.DB.EntityFW;
using BS.DB.EntityFW.CommonTypes;
using System.Web.Http.Results;
using BS.DB.EntityFW.BS.Activity;
using BS.DB.EntityFW.ViewModels;

namespace BS.WebAPI.Services.Controllers
{
    [System.Web.Http.Authorize]
    public class HomeController : ApiController
    {
        [Route("api/home/ValidateLogin")]
        [HttpPost]
        public JsonResult<BSEntityFramework_ResultType> ValidateLogin(LoginModel model)
        {
            Home_Activity homeActivity= new Home_Activity();
            var BSResult = homeActivity.ValidateLogin(model.UserName);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        [Route("api/home/SignUp")]
        [HttpPost]
        public JsonResult<BSEntityFramework_ResultType> SignUp(TBL_ShopLoginDetails model)
        {
            Home_Activity homeActivity = new Home_Activity();
            var BSResult = homeActivity.SignUp(model);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

    }
}
