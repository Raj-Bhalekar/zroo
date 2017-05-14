

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using BS.DB.EntityFW.CommonTypes;

namespace BS.DB.EntityFW
{
    public class ProductTypeCNFG_Activity
    {
        public BSEntityFramework_ResultType InsertProductType(TBL_ProductType_CNFG newProductType)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ProductType_CNFG.Add(newProductType);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newProductType, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, newProductType, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newProductType, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertProductType Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetProductType()
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                 var productTypes =  EF.TBL_ProductType_CNFG.Select(
                         type => new { Text = type.ProductTypeName, Value = type.ProductTypeID })
                      .ToList()
                      .Select(
                          ptype =>
                              new SelectListItem() { Text = ptype.Text, Value = Convert.ToString(ptype.Value) })
                      .ToList();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, productTypes, null, "Success");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, null, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, null, null, "Technical issue");
                logact.ErrorSetup("WebApp", "GetProductType Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateProductType(TBL_ProductType_CNFG ProductType)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ProductType_CNFG.AddOrUpdate(ProductType);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, ProductType, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, ProductType, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, ProductType, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateProductType Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
