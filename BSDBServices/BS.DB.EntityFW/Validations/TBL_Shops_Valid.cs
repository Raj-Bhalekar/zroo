using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW
{
    public partial class TBL_Shops : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError> IsValid()
        {
            var errList = new List<System.Data.Entity.Validation.DbValidationError>();
            if (ShopID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopID", ValidationMsg.InvalidEntry + "Shop"));
            }

            if (string.IsNullOrWhiteSpace(ShopName))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopName", "Shop Name " + ValidationMsg.IsRequired));
            }
            if (string.IsNullOrWhiteSpace(OwnerName))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("OwnerName", "Owner Name " + ValidationMsg.IsRequired));
            }
            if (string.IsNullOrWhiteSpace(ContactNumber))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ContactNumber", "Contact Number " + ValidationMsg.IsRequired));
            }

            if (ShopTypeID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopTypeID", ValidationMsg.PlsSelect + " Shop Type"));
            }

            if (ShopOpningTime == null)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopOpningTime", " Shop Opening Time" +  ValidationMsg.IsRequired));
            }

            if (ShopClosingTime == null)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopClosingTime", " Shop Closing Time" + ValidationMsg.IsRequired));
            }

            if (string.IsNullOrWhiteSpace(ShopAddress))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopAddress", "Shop Address " + ValidationMsg.IsRequired));
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("Email", "Email " + ValidationMsg.IsRequired));
            }
            else if (BSCommonValidation.IsValidEmail(Email))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("EmailID",ValidationMsg.InvalidEntry + " Email ID"));
            }
            if (ShopCategoryID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopCategoryID", ValidationMsg.PlsSelect+ " ShopCategoryID"));
            }

            return errList;
        }
    }
}