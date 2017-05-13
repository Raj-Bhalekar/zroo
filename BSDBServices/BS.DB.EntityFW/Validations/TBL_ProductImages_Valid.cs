using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW
{
    public partial class TBL_ProductImages : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError> IsValid()
        {
            var errList = new List<System.Data.Entity.Validation.DbValidationError>();
            if (ProductID==0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ProductID", ValidationMsg.PlsSelect+ " Product"));
            }
            
            return errList;
        }
    }
}