using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW
{
    public partial class TBL_Infrastructure_CNFG : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError> IsValid()
        {
            var errList = new List<System.Data.Entity.Validation.DbValidationError>();
            if (string.IsNullOrWhiteSpace(InfrastructureName))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("InfrastructureName", "Infrastructure Name " + ValidationMsg.IsRequired));
            }
            if (CityID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("CityID", ValidationMsg.PlsSelect + " City"));
            }

            if (PostalCodeID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("PostalCodeID", ValidationMsg.PlsSelect + " Postal Code"));
            }

            if (string.IsNullOrWhiteSpace(Address))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("Address", "Address " + ValidationMsg.IsRequired));
            }

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("PhoneNumber", "Phone Number " + ValidationMsg.IsRequired));
            }

            return errList;
        }
    }
}