using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW
{
    public partial class TBL_ProductCategories_CNFG : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError> IsValid()
        {
            var errList = new List<System.Data.Entity.Validation.DbValidationError>();
            if (string.IsNullOrWhiteSpace(ProductCategoryName))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ProductCategoryName", "Product Category Name " + ValidationMsg.IsRequired));
            }
            return errList;
        }
    }
}