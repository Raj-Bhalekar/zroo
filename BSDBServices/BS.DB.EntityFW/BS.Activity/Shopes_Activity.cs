

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
using BS.DB.EntityFW.ViewModels;

namespace BS.DB.EntityFW
{
    public class Shopes_Activity : BSActivity
    {

        public BSEntityFramework_ResultType InsertShopInPostal(TBL_ShopsInPostalCodes shopsInPostal, int shopID)
        {
            try
            {
                BSEntityFramework_ResultType result;
                using (BSDBEntities EF = new BSDBEntities())
                {
                    using (var transaction = EF.Database.BeginTransaction())
                    {
                        try
                        {
                            EF.Database.CommandTimeout = 180;
                            shopsInPostal.ShopID = shopID;
                            EF.TBL_ShopsInPostalCodes.Add(shopsInPostal);
                            EF.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            var logact = new LoggerActivity();
                            result = new BSEntityFramework_ResultType(BSResult.Fail, shopsInPostal, null, "Technical issue");
                            logact.SaveLog(logact.ErrorSetup("WebApp", "InsertShopPostalDetails Failed", "", "", "", ex.Message));
                            return result;
                        }
                    }

                }
              result = new BSEntityFramework_ResultType(BSResult.Success, shopsInPostal, null,
                  "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, shopsInPostal);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, shopsInPostal, null, "Technical issue");
             logact.SaveLog(logact.ErrorSetup("WebApp", "InsertShopPostalDetails Failed", "", "", "", ex.Message));
                return result;
            }

        }



        public BSEntityFramework_ResultType InsertShopMapDetails(TBL_ShopMapDetails shopsMapDetails)
        {
            try
            {
                BSEntityFramework_ResultType result;
                using (BSDBEntities EF = new BSDBEntities())
                {
                    using (var transaction = EF.Database.BeginTransaction())
                    {
                        try
                        {
                            EF.Database.CommandTimeout = 180;

                            if (shopsMapDetails.CreateDate == DateTime.MinValue)
                            {
                                shopsMapDetails.CreateDate = DateTime.Now;
                                EF.TBL_ShopMapDetails.Add(shopsMapDetails);
                            }
                            else
                            {
                                EF.TBL_ShopMapDetails.Attach(shopsMapDetails);
                                EF.Entry(shopsMapDetails).Property(x => x.Latitude).IsModified = true;
                                EF.Entry(shopsMapDetails).Property(x => x.Longitude).IsModified = true;
                                EF.Entry(shopsMapDetails).Property(x => x.UpdateDate).IsModified = true;
                                EF.Entry(shopsMapDetails).Property(x => x.UpdatedBy).IsModified = true;
                            }

                            EF.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            var logact = new LoggerActivity();
                            result = new BSEntityFramework_ResultType(BSResult.Fail, shopsMapDetails, null, "Technical issue");
                            logact.SaveLog(logact.ErrorSetup("WebApp", "shops Map Details Failed", "", "", "", ex.Message));
                            return result;
                        }
                    }

                }
                result = new BSEntityFramework_ResultType(BSResult.Success, shopsMapDetails, null,
                    "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, shopsMapDetails);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, shopsMapDetails, null, "Technical issue");
                logact.SaveLog(logact.ErrorSetup("WebApp", "shops Map Details Failed", "", "", "", ex.Message));
                return result;
            }

        }


        public BSEntityFramework_ResultType InsertShope(AddShopViewModel newShop)
        {
            try
            {
                BSEntityFramework_ResultType result;
                using (BSDBEntities EF = new BSDBEntities())
                {
                    using (var transaction = EF.Database.BeginTransaction())
                    {
                        try
                        {
                           newShop.ShopDetails.ShopID = -1;
                            newShop.ShopDetails.TBL_UserAssignedShops.Add(new TBL_UserAssignedShops() {ShopLoginDetailsID = newShop.ShopDetails.CreatedBy, CreatedBy = newShop.ShopDetails.CreatedBy,IsActive=true });
                            newShop.ShopDetails.TBL_ShopsInPostalCodes.Add(newShop.ShopPostalDetails);
                            EF.TBL_Shops.Add(newShop.ShopDetails);
                            EF.SaveChanges();
                          
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            var logact = new LoggerActivity();
                            result = new BSEntityFramework_ResultType(BSResult.Fail, newShop, null, "Technical issue");
                            logact.ErrorSetup("WebApp", "InsertShope Failed", "", "", "", ex.Message);
                            return result;
                        }
                    }
                }

                result = new BSEntityFramework_ResultType(BSResult.Success, newShop, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, newShop);

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
                return FormatException(dbValidationEx, null);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, null, null, "Technical issue");
                logact.ErrorSetup("WebApp", "GetShope Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetShopMapDetails(int shopId)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var shopMapDetails = EF.TBL_ShopMapDetails.Where(x=>x.ShopId==shopId)
                        .Select(x=>new
                        {
                            ShopId = x.ShopId,
                            Latitude = x.Latitude,
                            Longitude = x.Longitude,
                            CreateBy = x.CreateBy,
                            CreateDate = x.CreateDate
                        }).First();
                    var shopAddress = EF.TBL_Shops.Where(shop => shop.ShopID == shopId)
                       .Select(
                           shop => shop.ShopAddress)
                       .First();

                    var result = new BSEntityFramework_ResultType(BSResult.Success, new MapAddressViewModel()
                    {
                        Address = shopAddress,
                        shopMapDetails = new TBL_ShopMapDetails()
                        {
                            ShopId = shopMapDetails.ShopId,
                            Latitude = shopMapDetails.Latitude,
                            Longitude = shopMapDetails.Longitude,
                            CreateBy = shopMapDetails.CreateBy,
                            CreateDate = shopMapDetails.CreateDate
                        }

                    }, null, "Success");
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
                logact.ErrorSetup("WebApp", "Get Shop Map details Failed", "", "", "", ex.Message);
                return result;
            }

        }
        public BSEntityFramework_ResultType GetShopDetails(int shopId)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var shopMapDetails = EF.TBL_Shops
                        .First(x => x.ShopID == shopId);
              
                    var result = new BSEntityFramework_ResultType(BSResult.Success, shopMapDetails
                        , null, "Success");
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
                logact.ErrorSetup("WebApp", "Get Shop details Failed", "", "", "", ex.Message);
                return result;
            }

        }


        public BSEntityFramework_ResultType GetAllShope()
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var Shop = EF.TBL_ShopLoginDetails.Select(bs => bs).ToArray();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, Shop, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetShope Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateShope(TBL_ShopLoginDetails Shop)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ShopLoginDetails.AddOrUpdate(Shop);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, Shop, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, Shop);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, Shop, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateShope Failed", "", "", "", ex.Message);
                return result;
            }
        }


        public BSEntityFramework_ResultType GetShopAllBrands(int shopId)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var brandList = EF.TBL_Products.Where( prod=> prod.ShopID== shopId)
                        .Select(
                            prod => new {Text = prod.ProductBrand, Value = prod.ProductBrand })
                        .Distinct()
                        .ToArray()
                        .Select(
                            ptype =>
                                new SelectListItem() {Text = ptype.Text, Value = Convert.ToString(ptype.Value)})
                        .ToArray();


                    SelectListItem[] DefaultItem = {
                    new SelectListItem()
                    {
                        Text = "Select Brand",
                        Value ="0"
                    }};
                    var finalBrands = DefaultItem.Concat(brandList).OrderBy(v => v.Value).ToArray();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, finalBrands, null, "Success");
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



        public BSEntityFramework_ResultType GetShopAddress(int shopId)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var shopAddress = EF.TBL_Shops.Where(shop => shop.ShopID == shopId)
                        .Select(
                            shop => shop.ShopAddress)
                        .First();
                        
                    var result = new BSEntityFramework_ResultType(BSResult.Success, shopAddress, null, "Success");
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
                logact.ErrorSetup("WebApp", "Shop Address Failed Retrived", "", "", "", ex.Message);
                return result;
            }

        }
    }

}