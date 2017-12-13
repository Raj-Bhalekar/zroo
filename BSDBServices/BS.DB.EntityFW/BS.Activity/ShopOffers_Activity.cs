using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using BS.DB.EntityFW.BS.Activity;
using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.ViewModels;

namespace BS.DB.EntityFW
{
    public class ShopOffers_Activity:BSActivity
    {
        public BSEntityFramework_ResultType InsertShopOffer(AddShopOffersViewModel newShopOffer)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {

                    var shopId = newShopOffer.ShopOffer.ShopID;
                    var totalOffers = EF.TBL_ShopOffers.Count(x => x.ShopID == shopId) + 1;
                    newShopOffer.ShopOffer.OfferID
                        = CommonSafeConvert.ToInt(Convert.ToString(shopId) + Convert.ToString(totalOffers));
                    foreach (var prod in newShopOffer.OfferonProducts)
                    {
                        prod.CreatedBy = newShopOffer.ShopOffer.CreatedBy;
                        prod.CreateDate= DateTime.Now;
                        prod.IsActive = true;
                    }
                    newShopOffer.ShopOffer.TBL_OfferOnProducts=(newShopOffer.OfferonProducts);
                    EF.TBL_ShopOffers.Add(newShopOffer.ShopOffer);
                    EF.SaveChanges();
                   
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newShopOffer.ShopOffer,null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, newShopOffer);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newShopOffer, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertShopOffer Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType InsertOffersonProduct(List<TBL_OfferOnProducts> offeronProducts, int offerid)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    foreach (var product in offeronProducts)
                    {
                        product.OfferID = offerid;
                        EF.TBL_OfferOnProducts.Add(product);
                    }
                    EF.SaveChanges();

                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, offeronProducts, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, offeronProducts);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, offeronProducts, null, "Technical issue");
                logact.ErrorSetup("WebApp", "Insert Offers on Product Failed", "", "", "", ex.Message);
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
                return FormatException(dbValidationEx, null);

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
                    var ShopOffer = EF.TBL_ShopOffers.Select(bs => bs).ToArray();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, ShopOffer, null, "Success");
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
                return FormatException(dbValidationEx, ShopOffer);

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
