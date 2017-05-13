using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW
{
    public partial class TBL_Products : BSEntityInterface
    {
        public List<System.Data.Entity.Validation.DbValidationError> IsValid()
        {
            var errList = new List<System.Data.Entity.Validation.DbValidationError>();
            if (ShopID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopID", ValidationMsg.InvalidEntry + "Shop"));
            }

            if (string.IsNullOrWhiteSpace(ProductName))
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ProductName", "Product Name " + ValidationMsg.IsRequired));
            }

            if (ProductTypeID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ProductTypeID", ValidationMsg.PlsSelect + " Product Type"));
            }

            if (ProductCategoryID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ProductCategoryID", ValidationMsg.PlsSelect + " Product Category"));
            }

            if (ProductSubTypeID == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ProductSubTypeID", ValidationMsg.PlsSelect + " Product Sub Type"));
            }

            if (MRP==0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("MRP", "MRP " + ValidationMsg.IsRequired));
            }

            if (ShopPrice == 0)
            {
                errList.Add(new System.Data.Entity.Validation.DbValidationError("ShopPrice", "Shop Price " + ValidationMsg.IsRequired));
            }
            return errList;
        }
    }
}