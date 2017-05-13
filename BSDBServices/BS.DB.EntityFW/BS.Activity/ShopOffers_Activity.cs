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
    public class ShopOffers_Activity
    {
        public BSEntityFramework_ResultType InsertShopOffer(TBL_ShopOffers newShopOffer)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ShopOffers.Add(newShopOffer);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newShopOffer, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, newShopOffer, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newShopOffer, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertShopOffer Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetShopOffer(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var ShopOffer = EF.TBL_ShopOffers.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, ShopOffer, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetShopOffer Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetAllShopOffers()
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var ShopOffer = EF.TBL_ShopOffers.Select(bs => bs).ToList();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, ShopOffer, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetShopOffer Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateShopOffer(TBL_ShopOffers ShopOffer)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ShopOffers.AddOrUpdate(ShopOffer);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, ShopOffer, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, ShopOffer, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, ShopOffer, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateShopOffer Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
