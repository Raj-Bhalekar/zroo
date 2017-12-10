using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSWebApp.Models.Common
{
    public enum BSResult
    {
        Success = 0,
        Fail = 1,
        FailForValidation = 2
    }
    public class BSEntityFramework_ResultType
    {
        public BSResult Result { get;  set; }
        public object Entity { get;  set; }

        public IEnumerable<string> EntityValidationException { get;  set; }

        public string ResultMsg { get;  set; }
        
    }
}