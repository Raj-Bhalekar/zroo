

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using BS.DB.EntityFW.BS.Activity;
using BS.DB.EntityFW.CommonTypes;

namespace BS.DB.EntityFW
{
    public class ProductSubTypeCNFG_Activity:BSActivity
    {
        public BSEntityFramework_ResultType InsertProductSubType(TBL_ProductSubType_CNFG newProductSubType)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ProductSubType_CNFG.Add(newProductSubType);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newProductSubType, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, newProductSubType);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newProductSubType, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertProductSubType Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetProductSubType()
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var productSubType  = EF.TBL_ProductSubType_CNFG.Select(
                         type => new { Text = type.ProductSubTypeName, Value = type.ProductSubTypeID })
                      .ToList()
                      .Select(
                          ptype =>
                              new SelectListItem() { Text = ptype.Text, Value = Convert.ToString(ptype.Value) })
                      .ToArray();


                    SelectListItem[] DefaultItem = {
                    new SelectListItem()
                    {
                        Text = "Select Sub Type",
                        Value ="0"
                    }};
                    var finalproductSubType = DefaultItem.Concat(productSubType).OrderBy(v => v.Value).ToArray();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, finalproductSubType, null, "Success");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, null);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, null, null, "Technical issue");
                logact.ErrorSetup("WebApp", "GetProductSubType Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateProductSubType(TBL_ProductSubType_CNFG ProductSubType)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ProductSubType_CNFG.AddOrUpdate(ProductSubType);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, ProductSubType, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, ProductSubType);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, ProductSubType, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateProductSubType Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
