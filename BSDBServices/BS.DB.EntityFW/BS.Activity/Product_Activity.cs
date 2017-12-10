

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



        public BSEntityFramework_ResultType UpdateProducts(TBL_Products Products)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_Products.AddOrUpdate(Products);
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
    }
}
