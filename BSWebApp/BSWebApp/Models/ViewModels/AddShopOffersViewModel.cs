using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using BS.DB.EntityFW;

namespace BSWebApp.Models.ViewModels
{
    public class AddShopOffersViewModel
    {
        public TBL_ShopOffers ShopOffer { get; set; }
        public List<TBL_OfferOnProducts> OfferonProducts { get; set; }
        public string SelectedProductJson { get; set; }
    }

    public class SelectedProductList
    {
        public string IsSelected { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
}