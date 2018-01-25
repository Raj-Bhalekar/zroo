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
using System.Linq.Dynamic;
using BS.DB.EntityFW.ViewModels;
using System.Data.Entity;

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
        public BSEntityFramework_ResultType GetShopOfferListView(int shopId, string sortColumnName, string sortOrder, int pageSize, int currentPage,
           string offerShortDetails = "",
               DateTime? offerStartDate = null,
               DateTime? offerEndDate = null,
               string offerOnBrand = "",
               bool? isOfferOnProduct = null,
               bool? isActive = null
           )
        {
            var List = new object();
            int totalPage = 0;
            int totalRecord = 0;

            using (BSDBEntities EF = new BSDBEntities())
            {

                var offers =
                  EF.TBL_ShopOffers.Where(shopoffer => shopoffer.ShopID == shopId
                     && (offerShortDetails == null || offerShortDetails.Trim() == "" || shopoffer.OfferShortText.Contains(offerShortDetails))
                     && (offerStartDate == null || offerStartDate == DateTime.MinValue || shopoffer.OfferStartDate.Equals(offerStartDate))
                     && (offerEndDate == null || offerEndDate == DateTime.MinValue || shopoffer.OfferEndDate.Equals(offerEndDate))
                     && (offerOnBrand == null || offerOnBrand.Trim()=="" || shopoffer.OfferOnBrand == offerOnBrand)
                     && (isOfferOnProduct == null || isOfferOnProduct==shopoffer.IsOfferOnProducts)
                     && (isActive == null || isActive == shopoffer.IsActive)
                      );

                var offlistBS = offers.Join(EF.TBL_ShopLoginDetails,off=>off.CreatedBy
                                ,CrdUser=>CrdUser.ShopLoginDetailsID
                                ,(off,CrdUser)=>new {
                   // .Select(off => new {
                    off.OfferShortText,
                    off.OfferStartDate,
                    off.OfferEndDate,
                    off.ShopID,
                    off.OfferID,
                    off.IsActive,
                    off.OfferOnBrand,
                    off.OfferDetailText,
                    off.IsOfferOnProducts,
                    off.CreateDate,
                    CreatedBy = CrdUser.LoginName,
                    off.UpdateDate,
                    off.UpdatedBy
                }).GroupJoin(EF.TBL_ShopLoginDetails
                                , off => off.UpdatedBy
                                , updtUser => updtUser.ShopLoginDetailsID
                                , (offersinfo, updateuserDetails) => new {
                                   offersinfo= offersinfo,
                                   updateuserDetails= updateuserDetails
                                })
                .SelectMany(
                    Gj=> Gj.updateuserDetails.DefaultIfEmpty(),
                  (off, updtUser) => new {
                     offersinfo = off,
                     updateuserDetails = updtUser
                  })
                  .Select(ofd => new {
                    ofd.offersinfo.offersinfo.OfferShortText,
                    ofd.offersinfo.offersinfo.OfferStartDate,
                    ofd.offersinfo.offersinfo.OfferEndDate,
                    ofd.offersinfo.offersinfo.ShopID,
                    ofd.offersinfo.offersinfo.OfferID,
                    ofd.offersinfo.offersinfo.IsActive,
                    ofd.offersinfo.offersinfo.OfferOnBrand,
                    ofd.offersinfo.offersinfo.OfferDetailText,
                    ofd.offersinfo.offersinfo.IsOfferOnProducts,
                    ofd.offersinfo.offersinfo.CreateDate,
                    ofd.offersinfo.offersinfo.CreatedBy,
                    ofd.offersinfo.offersinfo.UpdateDate,
                    UpdatedBy = ofd.updateuserDetails.LoginName
                })
                .ToArray();


                totalRecord = offlistBS.Length;
                sortColumnName = sortColumnName ?? "OfferID";
                if (pageSize > 0)
                {
                    totalPage = totalRecord / pageSize + ((totalRecord % pageSize) > 0 ? 1 : 0);
                    List = offlistBS
                        .OrderBy(sortColumnName + " " + sortOrder)
                        .Skip(pageSize * (currentPage - 1))
                        .Take(pageSize).ToArray();
                }
                else
                {
                    var prodList = offlistBS.ToArray();
                    List = prodList;
                }
            }

            var jsonresult = new JsonResult
            {
                Data = new { List = List, totalPage = totalPage, sortColumnName = sortColumnName, sortOrder = sortOrder, currentPage = currentPage },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            var result = new BSEntityFramework_ResultType(BSResult.Success, List, null, "Fetched Successfully");
            return result;
        }

    }
}
