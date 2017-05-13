using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW
{
    public partial class TBL_InActiveShopList_CNFG : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError> IsValid()
        {
            List<System.Data.Entity.Validation.DbValidationError>  errList = new List<System.Data.Entity.Validation.DbValidationError>();
            if (string.IsNullOrWhiteSpace(Comment))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("Comment", "Comment " + ValidationMsg.IsRequired));
            }

            if (ShopID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopID", ValidationMsg.InvalidEntry + "Shop"));
            }

            if (ShopInActiveReasonID    == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopInActiveReasonID", ValidationMsg.PlsSelect + " ShopInActiveReasonID"));
            }
            return errList;
        }
    }
}