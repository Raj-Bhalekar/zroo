using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW.ViewModels
{
    public class AddShopOffersViewModel
    {
        public TBL_ShopOffers ShopOffer { get; set; }
        public List<TBL_OfferOnProducts> OfferonProducts { get; set; }
    }
}
