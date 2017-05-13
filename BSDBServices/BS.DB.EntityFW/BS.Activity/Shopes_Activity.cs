

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.ViewModels;

namespace BS.DB.EntityFW
{
    public class Shopes_Activity
    {

        public BSEntityFramework_ResultType InsertShopInPostal(TBL_ShopsInPostalCodes shopsInPostal, int shopID)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    shopsInPostal.ShopID = shopID;
                    EF.TBL_ShopsInPostalCodes.Add(shopsInPostal);
                    EF.SaveChanges();

                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, shopsInPostal, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, shopsInPostal, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, shopsInPostal, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertShope Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType InsertShope(AddShopViewModel newShop)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    newShop.ShopDetails.ShopID = -1;
                    EF.TBL_Shops.Add(newShop.ShopDetails);
                    EF.SaveChanges();
                   var resultChild= InsertShopInPostal(newShop.ShopPostalDetails, newShop.ShopDetails.ShopID);
                    if (resultChild.Result != BSResult.Success)
                    {
                        return resultChild;
                    }
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newShop, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, newShop, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newShop, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertShope Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetShope(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var Shope = EF.TBL_ShopLoginDetails.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.Success, Shope, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetShope Failed", "", "", "", ex.Message);
                return result;
            }

        }


        public BSEntityFramework_ResultType GetAllShope()
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var Shope = EF.TBL_ShopLoginDetails.Select(bs => bs).ToList();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, Shope, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetShope Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateShope(TBL_ShopLoginDetails Shope)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ShopLoginDetails.AddOrUpdate(Shope);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, Shope, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, Shope, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, Shope, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateShope Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
