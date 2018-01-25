using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BS.DB.EntityFW;
using BS.DB.EntityFW.CommonTypes;
using System.Web.Http.Results;
using System.Web.Mvc;
using BS.DB.EntityFW.BS.Activity;
using BS.DB.EntityFW.ViewModels;

namespace BS.WebAPI.Services.Controllers
{
    [System.Web.Http.Authorize]
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

        [System.Web.Http.Route("api/common/GetShopCategoryDetails")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetShopCategoryDetails()
        {
            ShopCategoryCNFG_Activity shopCatengoryActivity = new ShopCategoryCNFG_Activity();
            var BSResult = shopCatengoryActivity.GetShopCategory();
            return Json<object>(BSResult.Entity);
        }

        [System.Web.Http.Route("api/common/GetShopTypesDetails")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetShopTypesDetails()
        {
            ShopTypesCNFG_Activity shopTypeActivity = new ShopTypesCNFG_Activity();
            var BSResult = shopTypeActivity.GetShopTypes();
            return Json<object>(BSResult.Entity);

        }

        [System.Web.Http.Route("api/common/ValidateAndChangeShop")]
        [System.Web.Http.HttpPost]
        public JsonResult<ShopChangeStatus> ValidateAndChangeShop(ShopChangeRequest scr)
        {
            var validShopId = CommonSafeConvert.ToInt(scr.ShopId);
            if (validShopId > 0)
            {
                Home_Activity homeActivity = new Home_Activity();
                Plugins_Activity pluginActivity = new Plugins_Activity();
                if (homeActivity.IsValidShopForUser(scr.Userid, validShopId))
                {
                    var menuList = pluginActivity.GetPluginMenuDetailList(scr.Userid, validShopId);
                    return
                        Json<ShopChangeStatus>(
                            new ShopChangeStatus()
                            {
                                IsSuccess = true,
                                Message = "Shop change successfully",
                                MenuList = menuList
                            });

                }
                return Json<ShopChangeStatus>(
                            new ShopChangeStatus()
                            {
                                IsSuccess = false,
                                Message = "Invalid Shop. Please logoff and login again.",
                                MenuList = null
                            });
            }
            else
            {
               var result = new ShopChangeStatus
               {
                    IsSuccess = false,
                    Message = "Invalid Shop. Please logoff and login again.",
                    MenuList = null
                };
                return Json<ShopChangeStatus>(result); 
            }
        }

        [System.Web.Http.Route("api/common/GetProductTypesDetails")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetProductTypesDetails()
        {
            ProductTypeCNFG_Activity activity = new ProductTypeCNFG_Activity();
            var BSResult = activity.GetProductType();
           
            return Json<object>(BSResult.Entity);

        }
        [System.Web.Http.Route("api/common/GetProductSubTypesDetails")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetProductSubTypesDetails()
        {
            ProductSubTypeCNFG_Activity activity = new ProductSubTypeCNFG_Activity();
            var BSResult = activity.GetProductSubType();
            return Json<object>(BSResult.Entity);

        }
        [System.Web.Http.Route("api/common/GetProductCategoryDetails")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetProductCategoryDetails()
        {
            ProductCategoriesCNFG_Activity activity = new ProductCategoriesCNFG_Activity();
            var BSResult = activity.GetProductCategoriesCNFG();
            return Json<object>(BSResult.Entity);

        }

      }

    

}
