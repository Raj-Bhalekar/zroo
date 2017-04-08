using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using BS.DB.EntityFW.CommonTypes;

namespace BS.DB.EntityFW
{
    public class ProductCategoriesCNFG_Activity
    {
        public BSEntityFramework_ResultType InsertProductCategoriesCNFG(TBL_ProductCategories_CNFG newProductCategoriesCNFG)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ProductCategories_CNFG.Add(newProductCategoriesCNFG);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newProductCategoriesCNFG, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, newProductCategoriesCNFG, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newProductCategoriesCNFG, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertProductCategoriesCNFG Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetProductCategoriesCNFG(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var ProductCategoriesCNFG = EF.TBL_ProductCategories_CNFG.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, ProductCategoriesCNFG, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetProductCategoriesCNFG Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateProductCategoriesCNFG(TBL_ProductCategories_CNFG ProductCategoriesCNFG)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ProductCategories_CNFG.AddOrUpdate(ProductCategoriesCNFG);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, ProductCategoriesCNFG, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, ProductCategoriesCNFG, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, ProductCategoriesCNFG, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateProductCategoriesCNFG Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
