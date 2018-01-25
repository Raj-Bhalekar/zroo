using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BS.DB.EntityFW;
using BS.DB.EntityFW.CommonTypes;
using System.Web.Http.Results;
using BS.DB.EntityFW.ViewModels;

namespace BS.WebAPI.Services.Controllers
{
    [System.Web.Http.Authorize]
    public class ProductController : ApiController
    {
        private Product_Activity ProductsActivity = new Product_Activity();
        [HttpPost]
        public JsonResult<object> PostNewProduct(AddProductViewModel newProduct)
        {
            var BSResult = ProductsActivity.InsertProducts(newProduct);
            // var newShopId = ((TBL_Shops) BSResult.Entity).ShopID;

            return Json<object>(BSResult);
        }

        
             [HttpPost]
        [System.Web.Http.Route("api/product/PostEditProduct")]
        public JsonResult<object> PostEditProduct(AddProductViewModel editProduct)
        {
            var BSResult = ProductsActivity.UpdateProducts(editProduct);
            // var newShopId = ((TBL_Shops) BSResult.Entity).ShopID;

            return Json<object>(BSResult);
        }

        [System.Web.Http.Route("api/product/GetProductList")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetProductList(int shopId,string brand, string sortColumnName, string sortOrder, int pageSize, int currentPage)
        {
            var BSResult = ProductsActivity.GetProductList(shopId,brand, sortColumnName, sortOrder, pageSize, currentPage);
            var gnd =  new
            {
                IsSelected=1,
                ProductID = "1",
                ProductName = "Potatos",
                ProductBrand = "NA",
                ProductCategoryName = "Raw Food",
                ProductTypeName = "FOOD",
                ProductSubTypeName = "Veg"

            };
            var gnd2 = new
            {
                IsSelected = 1,
                ProductID = "2",
                ProductName = "Onions",
                ProductBrand = "NA",
                ProductCategoryName = "Raw Food",
                ProductTypeName = "FOOD",
                ProductSubTypeName = "Veg"

            };
            var gnd3 = new
            {
                IsSelected = 0,
                ProductID = "3",
                ProductName = "Carrots",
                ProductBrand = "NA",
                ProductCategoryName = "Raw Food",
                ProductTypeName = "FOOD",
                ProductSubTypeName = "Veg"

            };
            var gnd4 = new
            {
                IsSelected = 0,
                ProductID = "4",
                ProductName = "banana",
                ProductBrand = "NA",
                ProductCategoryName = "Raw Food",
                ProductTypeName = "FOOD",
                ProductSubTypeName = "Veg"

            };
            var gnd5 = new
            {
                IsSelected = 0,
                ProductID = "5",
                ProductName = "Mango",
                ProductBrand = "NA",
                ProductCategoryName = "Raw Food",
                ProductTypeName = "FOOD",
                ProductSubTypeName = "Veg"

            };

            var gnd6 = new
            {
                IsSelected = 0,
                ProductID = "6",
                ProductName = "Eggs",
                ProductBrand = "NA",
                ProductCategoryName = "Raw Food",
                ProductTypeName = "FOOD",
                ProductSubTypeName = "NonVeg"

            };
            var gnd7 = new
            {
                IsSelected = 0,
                ProductID = "7",
                ProductName = "Soyabean",
                ProductBrand = "NA",
                ProductCategoryName = "Raw Food",
                ProductTypeName = "FOOD",
                ProductSubTypeName = "veg"

            };
            var gnd8 = new
            {
                IsSelected = 0,
                ProductID = "8",
                ProductName = "DryFruits",
                ProductBrand = "NA",
                ProductCategoryName = "Raw Food",
                ProductTypeName = "FOOD",
                ProductSubTypeName = "Veg"

            };
            var gnd9 = new
            {
                IsSelected = 0,
                ProductID = "9",
                ProductName = "Apples",
                ProductBrand = "NA",
                ProductCategoryName = "Raw Food",
                ProductTypeName = "FOOD",
                ProductSubTypeName = "Veg"

            };

            var gnd10 = new
            {
                IsSelected = 0,
                ProductID = "10",
                ProductName = "Oranges",
                ProductBrand = "NA",
                ProductCategoryName = "Raw Food",
                ProductTypeName = "FOOD",
                ProductSubTypeName = "Veg"

            };

            var gnd11 = new
            {
                IsSelected = 0,
                ProductID = "11",
                ProductName = "Milk",
                ProductBrand = "NA",
                ProductCategoryName = "Raw Food",
                ProductTypeName = "FOOD",
                ProductSubTypeName = "veg"

            };
            var gnd12 = new
            {
                IsSelected = 0,
                ProductID = "12",
                ProductName = "Guva",
                ProductBrand = "NA",
                ProductCategoryName = "Raw Food",
                ProductTypeName = "FOOD",
                ProductSubTypeName = "Veg"

            };
            List<object> obj = new List<object>();
            obj.Add(gnd);
            obj.Add(gnd2);
            obj.Add(gnd3);

            obj.Add(gnd4);
            obj.Add(gnd5);
            obj.Add(gnd6);
            obj.Add(gnd7);
            obj.Add(gnd8);
            obj.Add(gnd9);

            obj.Add(gnd10);
            obj.Add(gnd11);
            obj.Add(gnd12);
           return Json<object>(BSResult.Entity);
          //  return Json<object>(obj);

        }


        [System.Web.Http.Route("api/product/GetProductListView")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetProductListView(int shopId, string sortColumnName, string sortOrder, int pageSize, int currentPage,
            string prodName = "", string brandName = "", string barCode = "", string productType = "",
                string isAvailable = "true",
                string availableQty = "",
                string isActive = "true",
                string prodCategory = "",
                string prodSubType = "",
                string prodMrp = "",
                string prodShopPrice = ""
            )
        {
            var BSResult = ProductsActivity.GetProductListView(shopId, sortColumnName, sortOrder, pageSize, currentPage,
                prodName, brandName, barCode, CommonSafeConvert.ToInt(productType), Convert.ToBoolean(isAvailable), availableQty,Convert.ToBoolean(isActive), CommonSafeConvert.ToInt(prodCategory), CommonSafeConvert.ToInt(prodSubType), CommonSafeConvert.ToDecimal(prodMrp), CommonSafeConvert.ToDecimal(prodShopPrice)
                );
            return Json<object>(BSResult.Entity);
            //  return Json<object>(obj);
        }


        [System.Web.Http.Route("api/product/GetProductImage")]
        [System.Web.Http.HttpGet]
        public JsonResult<object> GetProductImage(int shopId, int prodId)
        {
            var BSResult = ProductsActivity.GetProductImage(shopId, prodId);
            return Json(BSResult?.Entity);
        }
    }
}
