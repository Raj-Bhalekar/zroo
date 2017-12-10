using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;

namespace BS.DB.EntityFW
{
      public partial class TBL_ShopMapDetails : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError> IsValid()
        {
            var errList = new List<System.Data.Entity.Validation.DbValidationError>();
            if (string.IsNullOrWhiteSpace(Latitude))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("Latitude", "Latitude " + ValidationMsg.IsRequired));
            }

            if (string.IsNullOrWhiteSpace(Longitude))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("Longitude", "Longitude " + ValidationMsg.IsRequired));
            }

            if (ShopId == null || ShopId==0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopId", "ShopId " + ValidationMsg.InvalidEntry));
            }
            return errList;
        }
    }
}
