

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
    public class Product_Activity
    {
        public BSEntityFramework_ResultType InsertProducts(AddProductViewModel newProduct)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    newProduct.ProductDetails.ProductID = -1;
                    EF.TBL_Products.Add(newProduct.ProductDetails);
                    EF.SaveChanges();
                    var resultChild = InsertProductImages(newProduct.ProductImages, newProduct.ProductDetails.ProductID);
                    if (resultChild.Result != BSResult.Success)
                    {
                        return resultChild;
                    }
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newProduct.ProductDetails, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, newProduct.ProductDetails, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newProduct.ProductDetails, null, "Technical issue");
                logact.ErrorSetup("WebApp", "Insert Products Failed", "", "", "", ex.Message);
                return result;
            }

        }
        public BSEntityFramework_ResultType InsertProductImages(List<TBL_ProductImages> productImages, int productid)
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
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, productImages, dbValidationEx, "Validation Failed");
                return result;
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
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, null, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, null, null, "Technical issue");
                logact.ErrorSetup("WebApp", "GetProducts Failed", "", "", "", ex.Message);
                return result;
            }

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
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, Products, dbValidationEx, "Validation Failed");
                return result;
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
