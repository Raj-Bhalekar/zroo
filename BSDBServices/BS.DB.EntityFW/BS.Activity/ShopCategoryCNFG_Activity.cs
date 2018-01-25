

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
    public class ShopCategoryCNFG_Activity:BSActivity
    {
        public BSEntityFramework_ResultType InsertShopCategory(TBL_ShopCategory_CNFG newShopCategory)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ShopCategory_CNFG.Add(newShopCategory);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newShopCategory, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, newShopCategory);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newShopCategory, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertShopCategory Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetShopCategory(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var ShopCategory = EF.TBL_ShopCategory_CNFG.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, ShopCategory, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetShopCategory Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetShopCategory()
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var ShopCategory =
                        EF.TBL_ShopCategory_CNFG.Select(category => new 
                        { Text = category.CategoryName, Value = category.ShopCategoryID}).ToList().Select(ct => new SelectListItem() {Text = ct.Text, Value = Convert.ToString(ct.Value)})
                        .ToArray();

                    SelectListItem[] DefaultItem = {
                    new SelectListItem()
                    {
                        Text = "Select Product Type",
                        Value ="0"
                    }};
                    var finalShopCategory = DefaultItem.Concat(ShopCategory).OrderBy(v => v.Value).ToArray();


                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, finalShopCategory, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetShopCategory Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateShopCategory(TBL_ShopCategory_CNFG ShopCategory)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ShopCategory_CNFG.AddOrUpdate(ShopCategory);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, ShopCategory, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, ShopCategory);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, ShopCategory, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateShopCategory Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
