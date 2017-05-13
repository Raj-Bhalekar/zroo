using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW
{
    public partial class TBL_ShopLoginDetails : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError> IsValid()
        {
            var errList = new List<System.Data.Entity.Validation.DbValidationError>();
            if (string.IsNullOrWhiteSpace(LoginName))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("LoginName", "Login Name " + ValidationMsg.IsRequired));
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("Password", "Password " + ValidationMsg.IsRequired));
            }
            return errList;
        }
    }
}