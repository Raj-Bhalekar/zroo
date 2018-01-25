

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
    public class Product_Activity: BSActivity
    {
        public BSEntityFramework_ResultType InsertProducts(AddProductViewModel newProduct)
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
                            var shopId = newProduct.ProductDetails.ShopID;
                         var totalProducts=  EF.TBL_Products.Count(x => x.ShopID == shopId)+1;
                            newProduct.ProductDetails.ProductID 
                                = CommonSafeConvert.ToInt(Convert.ToString(shopId)+Convert.ToString(totalProducts));
                            newProduct.ProductDetails.TBL_ProductImages = newProduct.ProductImages;
                            EF.TBL_Products.Add(newProduct.ProductDetails);
                            EF.SaveChanges();
                            //var resultChild = InsertProductImages(newProduct.ProductImages,
                            //    newProduct.ProductDetails.ProductID);
                            //if (resultChild.Result != BSResult.Success)
                            //{
                            //    return resultChild;
                            //}
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            var logact = new LoggerActivity();
                            result = new BSEntityFramework_ResultType(BSResult.Fail, newProduct, null, "Technical issue");
                            logact.ErrorSetup("WebApp", "InsertShope Failed", "", "", "", ex.Message);
                            return result;
                        }
                    }

                    result = new BSEntityFramework_ResultType(BSResult.Success, newProduct.ProductDetails, null,
                        "Created Sucessfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, newProduct.ProductDetails);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newProduct.ProductDetails, null,
                    "Technical issue");
                logact.ErrorSetup("WebApp", "Insert Products Failed", "", "", "", ex.Message);
                return result;
            }

        }
        public BSEntityFramework_ResultType InsertProductImages(List<TBL_ProductImages> productImages, long productid)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    foreach (var prodimage in productImages)
                    {
                        prodimage.ProductID = productid;
                        EF.TBL_ProductImages.Add(prodimage);
                    }
                    EF.SaveChanges();

                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, productImages, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, productImages);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, productImages, null, "Technical issue");
                logact.ErrorSetup("WebApp", "Insert ProductImages Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetProducts(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var Products = EF.TBL_Products.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, Products, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetProducts Failed", "", "", "", ex.Message);
                return result;
            }

        }


        public BSEntityFramework_ResultType GetProductList(int shopId,string brand, string sortColumnName , string sortOrder , int pageSize , int currentPage)
        {
            var List = new object();
            int totalPage = 0;
            int totalRecord = 0;

            using (BSDBEntities EF = new BSDBEntities())
            {
                var prod = 
                    string.IsNullOrWhiteSpace(brand)?
                    EF.TBL_Products.Where(prd=>prd.ShopID == shopId && prd.IsActive)
                    : 
                    EF.TBL_Products.Where(prd => prd.ShopID == shopId && prd.IsActive &&
                                                      prd.ProductBrand.Equals(brand)
                );


               var prodListBS = prod.Select(prd=>new { prd.ProductID, prd.ProductName,
                        prd.ProductBrand,
                        prd.BarCode,
                        prd.IsAvailable
                        ,prd.MRP,
                        prd.ShopPrice,
                        prd.OtherJsonDetails
                        , prd.TBL_ProductCategories_CNFG.ProductCategoryName,
                        prd.TBL_ProductType_CNFG.ProductTypeName,
                        prd.TBL_ProductSubType_CNFG.ProductSubTypeName

                    }).ToArray();
                totalRecord = prodListBS.Length;
                sortColumnName = sortColumnName ?? "ProductID";
                if (pageSize > 0)
                {
                    totalPage = totalRecord / pageSize + ((totalRecord % pageSize) > 0 ? 1 : 0);
                    List= prodListBS
                        .OrderBy(sortColumnName + " " + sortOrder)
                        .Skip(pageSize * (currentPage - 1))
                        .Take(pageSize).ToArray();
                }
                else
                {
                    var prodList = prodListBS.ToArray();
                    List = prodList;
                }
            }
          
            var jsonresult= new JsonResult
            {
                Data = new { List = List, totalPage = totalPage, sortColumnName = sortColumnName, sortOrder = sortOrder, currentPage = currentPage },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            var result = new BSEntityFramework_ResultType(BSResult.Success, List, null, "Fetched Successfully");
            return result;
        }

        public BSEntityFramework_ResultType GetProductListView(int shopId, string sortColumnName, string sortOrder, int pageSize, int currentPage,
            string prodName = "", string brandName = "", string barCode = "", int productType = 0,
                bool isAvailable = true,
                string availableQty = "",
                bool isActive = true,
                int prodCategory =0,
                int prodSubType = 0,
                decimal prodMrp =0,
                decimal prodShopPrice =0
            )
        {
            var List = new object();
            int totalPage = 0;
            int totalRecord = 0;

            using (BSDBEntities EF = new BSDBEntities())
            {

                var prod =
                  EF.TBL_Products.Where(prd => prd.ShopID == shopId && prd.IsActive &&
                                                      (brandName == null|| brandName == "0" || brandName.Trim() == "" || prd.ProductBrand.Equals(brandName))
                                                     && (prodName == null || prodName.Trim() == "" || prd.ProductName.Equals(prodName))
                                                     && (barCode == null || barCode.Trim() == "" || prd.BarCode.Equals(barCode))
                                                     && (productType == 0  || prd.ProductTypeID==productType)
                                                     && (prd.IsAvailable==isAvailable)
                                                     && (availableQty == null || availableQty.Trim() == "" || prd.ProductBrand.Equals(availableQty))
                                                     && (prd.IsActive.Equals(isActive))
                                                     && (prodCategory == 0 || prd.ProductCategoryID.Equals(prodCategory))
                                                     && (prodSubType == 0 || prd.ProductSubTypeID.Equals(prodSubType))
                                                     && (prodMrp == 0 || prd.MRP==prodMrp)
                                                     && (prodShopPrice == 0 || prd.ShopPrice==prodShopPrice)
                                                      );

                var prodListBS = prod.Select(prd => new {
                    prd.ProductID,
                    prd.ProductName,
                    prd.ProductBrand,
                    prd.BarCode,
                    prd.IsActive,
                    IsAvailable= prd.IsActive,
                    prd.AvailableQuantity,
                    prd.MRP,
                    prd.ShopPrice,
                    prd.OtherJsonDetails,
                    prd.ProductCategoryID,
                    prd.TBL_ProductCategories_CNFG.ProductCategoryName,
                    prd.ProductTypeID,
                    prd.TBL_ProductType_CNFG.ProductTypeName,
                    prd.ProductSubTypeID,
                    prd.TBL_ProductSubType_CNFG.ProductSubTypeName,
                    prd.ShopID


                }).ToArray();


                totalRecord = prodListBS.Length;
                sortColumnName = sortColumnName ?? "ProductID";
                if (pageSize > 0)
                {
                    totalPage = totalRecord / pageSize + ((totalRecord % pageSize) > 0 ? 1 : 0);
                    List = prodListBS
                        .OrderBy(sortColumnName + " " + sortOrder)
                        .Skip(pageSize * (currentPage - 1))
                        .Take(pageSize).ToArray();
                }
                else
                {
                    var prodList = prodListBS.ToArray();
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


        public BSEntityFramework_ResultType UpdateProducts(AddProductViewModel Products)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                   // EF.TBL_Products.AddOrUpdate(Products.ProductDetails);
                    EF.TBL_Products.Attach(Products.ProductDetails);
                    EF.Entry(Products.ProductDetails).Property(x => x.IsActive).IsModified = true;
                    EF.Entry(Products.ProductDetails).Property(x => x.AvailableQuantity).IsModified = true;
                    EF.Entry(Products.ProductDetails).Property(x => x.BarCode).IsModified = true;
                    EF.Entry(Products.ProductDetails).Property(x => x.IsAvailable).IsModified = true;
                    EF.Entry(Products.ProductDetails).Property(x => x.MRP).IsModified = true;
                    EF.Entry(Products.ProductDetails).Property(x => x.OtherJsonDetails).IsModified = true;
                    EF.Entry(Products.ProductDetails).Property(x => x.ProductBrand).IsModified = true;
                    EF.Entry(Products.ProductDetails).Property(x => x.ProductCategoryID).IsModified = true;
                    EF.Entry(Products.ProductDetails).Property(x => x.ProductName).IsModified = true;
                    EF.Entry(Products.ProductDetails).Property(x => x.ProductSubTypeID).IsModified = true;
                    EF.Entry(Products.ProductDetails).Property(x => x.ProductTypeID).IsModified = true;
                    EF.Entry(Products.ProductDetails).Property(x => x.ShopPrice).IsModified = true;
                    EF.Entry(Products.ProductDetails).Property(x => x.ShopID).IsModified = false;

                    if (Products.ProductImages != null&& Products.ProductImages.Count>0 && Products.ProductImages[0]!=null)
                    {
                        EF.TBL_ProductImages.Attach(Products.ProductImages[0]);
                        EF.Entry(Products.ProductImages[0]).Property(x => x.ProductImage).IsModified = true;
                    }
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, Products, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, Products);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, Products, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateProducts Failed", "", "", "", ex.Message);
                return result;
            }
        }

        public BSEntityFramework_ResultType GetProductImage(int shopId , int prodId )
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var prodImage = EF.TBL_ProductImages.FirstOrDefault(p => p.ProductID == prodId && p.IsActive);
                    var result = new BSEntityFramework_ResultType(BSResult.Success,
                        new ImageDetails() { ImgID = prodImage?.ImageID ?? -1, ImgData = prodImage!=null? String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(prodImage.ProductImage)):null},
                        null, "Updated Successfully");
                
                    return result;
                }
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, null, null, "Technical issue");
                logact.ErrorSetup("WebApp", "Fetch Product Image Failed", "", "", "", ex.Message);
                return null;
            }
        }
    }
}
