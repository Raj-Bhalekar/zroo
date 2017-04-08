

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
    public class Shops5TopProducts_Activity
    {
        public BSEntityFramework_ResultType InsertTop5ProductForShop(List<TBL_Shops5TopProducts> top5List)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    foreach (TBL_Shops5TopProducts p in top5List)
                    {
                        EF.TBL_Shops5TopProducts.Add(p);
                    }
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, null, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, top5List, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, top5List, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertState Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetShops5TopProducts(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var Top5Productlist = EF.TBL_Shops5TopProducts.Where(sht => sht.ShopID == id).ToList();
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, Top5Productlist, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetState Failed", "", "", "", ex.Message);
                return result; 
            }

        }

        //public BSEntityFramework_ResultType UpdateState(TBL_Shops5TopProducts state)
        //{
        //    try
        //    {
        //        using (BSDBEntities EF = new BSDBEntities())
        //        {
        //            EF.TBL_Shops5TopProducts.AddOrUpdate(state);
        //            EF.SaveChanges();
        //            var result = new BSEntityFramework_ResultType(BSResult.Success, state, null, "Updated Successfully");
        //            return result;
        //        }
        //    }
        //    catch (DbEntityValidationException dbValidationEx)
        //    {
        //        var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, state, dbValidationEx, "Validation Failed");
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        var logact = new LoggerActivity();
        //        var result = new BSEntityFramework_ResultType(BSResult.Fail, state, null, "Technical issue");
        //        logact.ErrorSetup("WebApp", "UpdateState Failed", "", "", "", ex.Message);
        //        return result;
        //    }
        //}
    }
}
