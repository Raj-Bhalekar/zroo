using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW
{
    public partial class TBL_OfferOnProducts : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError> IsValid()
        {
            List<System.Data.Entity.Validation.DbValidationError>  errList = new List<System.Data.Entity.Validation.DbValidationError>();
           
            return errList;
        }
    }
}