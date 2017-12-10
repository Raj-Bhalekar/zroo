using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;

namespace BS.DB.EntityFW.BS.Activity
{
   public abstract class  BSActivity
    {
        public BSEntityFramework_ResultType FormatException(DbEntityValidationException dbValidationEx, object entity)
        {
            var errorMessages = dbValidationEx.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, entity, errorMessages, "Validation Failed");
            return result;
        }
    }
}
