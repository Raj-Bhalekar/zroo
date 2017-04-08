

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
    public class Shopes_Activity
    {
        public BSEntityFramework_ResultType InsertShope(TBL_ShopLoginDetails newShope)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ShopLoginDetails.Add(newShope);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newShope, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, newShope, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newShope, null, "Technical issue");
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
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, null, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, null, null, "Technical issue");
                logact.ErrorSetup("WebApp", "GetShope Failed", "", "", "", ex.Message);
                return result;
            }

        }


        public BSEntityFramework_ResultType GetAllShope()
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var Shope = EF.TBL_ShopLoginDetails.Select(bs => bs).ToList();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, Shope, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetShope Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateShope(TBL_ShopLoginDetails Shope)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_ShopLoginDetails.AddOrUpdate(Shope);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, Shope, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, Shope, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, Shope, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateShope Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
