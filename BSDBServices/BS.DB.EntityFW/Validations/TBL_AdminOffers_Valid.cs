using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW
{
    public partial class TBL_AdminOffers : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError> IsValid()
        {
           var errList = new List<System.Data.Entity.Validation.DbValidationError>();
            if (string.IsNullOrWhiteSpace(ShortText))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShortText", "Short Text " + ValidationMsg.IsRequired));
            }

            if (string.IsNullOrWhiteSpace(LongText))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("LongText", "Long Text " + ValidationMsg.IsRequired));
            }

            if (StartDate ==  null)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("StartDate","Start Date " + ValidationMsg.IsRequired));
            }

            if (EndDate == null)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("EndDate", "End Date " + ValidationMsg.IsRequired));
            }

         
            return errList;
        }
    }
}