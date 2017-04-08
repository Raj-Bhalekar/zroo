using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW.CommonTypes
{
  public  interface BSEntityInterface
    {
        List<System.Data.Entity.Validation.DbValidationError> IsValid();
    }
}
