

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
    public class ShopTypesCNFG_Activity:BSActivity
    {
        public BSEntityFramework_ResultType InsertShopTypes(TBL_ShopTypes_CNFG newShopTypes)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ShopTypes_CNFG.Add(newShopTypes);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newShopTypes, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, newShopTypes);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newShopTypes, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertShopTypes Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetShopTypes(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var ShopTypes = EF.TBL_ShopTypes_CNFG.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.Success, ShopTypes, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetShopTypes Failed", "", "", "", ex.Message);
                return result;
            }

        }


        public BSEntityFramework_ResultType GetShopTypes()
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var ShopTypes =
                        EF.TBL_ShopTypes_CNFG.Select(
                                shoptype => new {Text = shoptype.TypeName, Value = shoptype.ShopTypeID})
                            .ToList()
                            .Select(
                                shptype =>
                                    new SelectListItem() {Text = shptype.Text, Value = Convert.ToString(shptype.Value)})
                            .ToArray();

                    SelectListItem[] DefaultItem = {
                    new SelectListItem()
                    {
                        Text = "Select Shop Type",
                        Value ="0"
                    }};

                    var finalShopTypes = DefaultItem.Concat(ShopTypes).OrderBy(v=>v.Value).ToArray(); 
                    var result = new BSEntityFramework_ResultType(BSResult.Success, finalShopTypes, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetShopTypes Failed", "", "", "", ex.Message);
                return result;
            }

        }
        public BSEntityFramework_ResultType GetAllShopTypes()
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var ShopTypes = EF.TBL_ShopTypes_CNFG.Select(st => st).ToArray();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, ShopTypes, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetShopTypes Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateShopTypes(TBL_ShopTypes_CNFG ShopTypes)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ShopTypes_CNFG.AddOrUpdate(ShopTypes);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, ShopTypes, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, ShopTypes);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, ShopTypes, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateShopTypes Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
