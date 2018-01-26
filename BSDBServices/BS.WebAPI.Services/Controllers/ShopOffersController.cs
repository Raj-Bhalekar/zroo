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
    public class ShopOffersController : ApiController
    {
        private ShopOffers_Activity ShopOffersActivity = new ShopOffers_Activity();
        [System.Web.Http.HttpGet]
        public JsonResult<BSEntityFramework_ResultType> GetShopOffersDetail(int id)
        {
            var BSResult = ShopOffersActivity.GetShopOffer(id);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        [System.Web.Http.HttpGet]
        public JsonResult<BSEntityFramework_ResultType> GetAllShopOfferss()
        {
            var BSResult = ShopOffersActivity.GetAllShopOffers();
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        [HttpPost]
        public JsonResult<BSEntityFramework_ResultType> PostNewShopOffers(AddShopOffersViewModel newShopOffers)
        {
            var BSResult = ShopOffersActivity.InsertShopOffer(newShopOffers);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        [HttpPut]
        public JsonResult<BSEntityFramework_ResultType> PutUpdateShopOffers(TBL_ShopOffers upateShopOffers)
        {
            var BSResult = ShopOffersActivity.UpdateShopOffer(upateShopOffers);
            return Json<BSEntityFramework_ResultType>(BSResult);
        }

        [Route("api/shopoffers/GetShopOffersListView")]
        [HttpGet]
        public JsonResult<object> GetShopOffersListView(int shopId, string sortColumnName, string sortOrder, int pageSize, int currentPage,
           string offerShortDetails = "", 
               DateTime? offerStartDate = null,
               DateTime? offerEndDate = null,
               string offerOnBrand = "",
               bool? isOfferOnProduct = null,
               bool? isActive = null
           )
        {
            var BSResult = ShopOffersActivity.GetShopOfferListView(shopId, sortColumnName, sortOrder, pageSize, currentPage,
                offerShortDetails, offerStartDate, offerEndDate, offerOnBrand,isOfferOnProduct, isActive
                );
            return Json<object>(BSResult.Entity);
            //  return Json<object>(obj);
        }

        [System.Web.Http.Route("api/shopoffers/GetOfferImage")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetProductImage(int shopId, int offerId)
        {
            var BSResult = ShopOffersActivity.GetOfferImage(shopId, offerId);
            return Json(BSResult?.Entity);
        }

    }
}
