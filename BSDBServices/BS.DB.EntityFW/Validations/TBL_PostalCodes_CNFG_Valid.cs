using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW
{
    public partial class TBL_PostalCodes_CNFG : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError>  IsValid()
        {
            List<System.Data.Entity.Validation.DbValidationError>  errList = new List<System.Data.Entity.Validation.DbValidationError>();
            if (PostCodeID==0)                 
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("PostCodeID", "Postal Code " + ValidationMsg.IsRequired));
            }

            if (CityID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("CityID", ValidationMsg.PlsSelect + " City"));
            }
            
            return errList;
        }
    }
}