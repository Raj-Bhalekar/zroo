using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BS.DB.EntityFW;
using BSWebApp.Common;
using BSWebApp.Models.ViewModels;
using Newtonsoft.Json;

namespace BSWebApp.Areas.Shop.Controllers
{
    [Common.ValidateAntiForgeryTokenWrapper(HttpVerbs.Post, null)]
    public class ShopController : Controller
    {
        

        // GET: Shops/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Shops/Create
        public ActionResult AddShop()
        {
            // TODO: Add insert logic here
            FillViewDatasForAddShop();
            return View();
        }

        private void FillViewDatasForAddShop()
        {
            Dictionary<string, List<SelectListItem>> SelectListData = new Dictionary<string, List<SelectListItem>>();


            var shopTypes = new List<SelectListItem>();
            shopTypes.Add(new SelectListItem() { Value = "1", Text = "Stationary" });
            shopTypes.Add(new SelectListItem() { Value = "2", Text = "Grossry" });
            shopTypes.Add(new SelectListItem() { Value = "3", Text = "Garment" });
            shopTypes.Add(new SelectListItem() { Value = "4", Text = "HardWare" });
            shopTypes.Add(new SelectListItem() { Value = "5", Text = "Mobile Electronic" });
            shopTypes.Add(new SelectListItem() { Value = "6", Text = "General" });
            shopTypes.Add(new SelectListItem() { Value = "7", Text = "Other" });

            SelectListData.Add("TBL_ShopTypes_CNFG", shopTypes);

            var shopCategory = new List<SelectListItem>();
            shopCategory.Add(new SelectListItem() { Value = "1", Text = "FOOD" });
            shopCategory.Add(new SelectListItem() { Value = "2", Text = "Appearls" });
            shopCategory.Add(new SelectListItem() { Value = "3", Text = "Electronic" });
            shopCategory.Add(new SelectListItem() { Value = "4", Text = "Custruction" });
            shopCategory.Add(new SelectListItem() { Value = "5", Text = "Other" });
            shopCategory.Add(new SelectListItem() { Value = "6", Text = "FOOD" });


            SelectListData.Add("TBL_ShopCategory_CNFG", shopCategory);
            ViewData["SelectListData"] = SelectListData;
        }

        // POST: Shops/Create
        [HttpPost]
        public ActionResult AddShop(AddShopViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:8080/");

                        // Add an Accept header for JSON format.
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                        HttpResponseMessage response = client.PostAsync("/api/shop/PostNewShopes",content).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var rslt = response.Content.ReadAsStringAsync().Result;
                            return Json(new JavaScriptSerializer().Deserialize<object>(rslt),
                                JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return null;
                        }
                    }
              

                }
                FillViewDatasForAddShop();
                return View("AddShop", model);
            }
            catch
            {
                FillViewDatasForAddShop();
                return View();
            }
        }

        // GET: Shops/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shops/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shops/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shops/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public JsonResult GetPostalsAutoComplete(string hint)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/common?hint="+ hint).Result;

            if (response.IsSuccessStatusCode)
            {
                var rslt = response.Content.ReadAsStringAsync().Result;
                return Json(new JavaScriptSerializer().Deserialize<object>(rslt), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }

        public JsonResult GetInfrastureDetails(string postalCode)
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:8080/");

            //// Add an Accept header for JSON format.
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage response = client.GetAsync("/api/common/GetInfrastructureDetails?postalcode=" + postalCode).Result;

            //if (response.IsSuccessStatusCode)
            //{
            //    var rslt = response.Content.ReadAsStringAsync().Result;
            //    return Json(new JavaScriptSerializer().Deserialize<object>(rslt), JsonRequestBehavior.AllowGet);
            //}
            var paramlist = new List<KeyValuePair<string,string>>();
            var param = new KeyValuePair<string, string>("postalCode", postalCode);
            paramlist.Add(param);

          var reslt=  new CommonAjaxCallToWebAPI().AjaxGet(@"/api/common/GetInfrastructureDetails", paramlist);
          return  Json(new JavaScriptSerializer().Deserialize<object>(reslt), JsonRequestBehavior.AllowGet);
        }
    }
}
