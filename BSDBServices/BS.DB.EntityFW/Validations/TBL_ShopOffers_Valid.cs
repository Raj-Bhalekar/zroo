using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW
{
    public partial class TBL_ShopOffers : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError> IsValid()
        {
            var errList = new List<System.Data.Entity.Validation.DbValidationError>();
            if (ShopID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopID", ValidationMsg.InvalidEntry + "Shop"));
            }

            if (string.IsNullOrWhiteSpace(OfferShortText))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("OfferShortText", "Offer Short Text " + ValidationMsg.IsRequired));
            }

            if (OfferEndDate ==null)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("OfferEndDate", "Offer End Date " + ValidationMsg.IsRequired));
            }

            if (OfferStartDate == null)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("OfferStartDate", "Offer Start Date " + ValidationMsg.IsRequired));
            }

            if (string.IsNullOrWhiteSpace(OfferDetailText))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("OfferDetailText", "Offer Detail Text " + ValidationMsg.IsRequired));
            }
            return errList;
        }
    }
}