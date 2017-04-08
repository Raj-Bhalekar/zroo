using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW.CommonTypes
{
   public class BSEntityFramework_ResultType
    {
        public BSResult Result { get; private set; }
        public object Entity { get; private set; }

        public DbEntityValidationException EntityValidationException { get; private set; }

        public string ResultMsg { get; private set; }

        protected internal BSEntityFramework_ResultType(BSResult bsresult, object entity, DbEntityValidationException entityvalidationexception, string resultMsg)
        {
            Result = bsresult;
            Entity = entity;
            EntityValidationException = entityvalidationexception;
            ResultMsg = resultMsg;
        }

        private BSEntityFramework_ResultType(){}
    }



    public enum BSResult
    {
        Success=0,
        Fail=1,
        FailForValidation=2
    }
}
