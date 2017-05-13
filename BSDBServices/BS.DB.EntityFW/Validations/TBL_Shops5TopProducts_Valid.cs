

using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW
{
    public partial class TBL_Shops5TopProducts : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError> IsValid()
        {
            List<System.Data.Entity.Validation.DbValidationError>  errList = new List<System.Data.Entity.Validation.DbValidationError>();
            if (ProductID==0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ProductID", ValidationMsg.PlsSelect + " Product"));
            }

            if (ShopID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopID", ValidationMsg.InvalidEntry + " Shop"));
            }

            return errList;
        }
    }
}