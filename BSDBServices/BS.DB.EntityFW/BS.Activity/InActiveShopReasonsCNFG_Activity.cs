using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using BS.DB.EntityFW.BS.Activity;
using BS.DB.EntityFW.CommonTypes;

namespace BS.DB.EntityFW
{
    public class InActiveShopReasonsCNFG_Activity:BSActivity
    {
        public BSEntityFramework_ResultType InsertInActiveShopReasons(TBL_InActiveShopReasons_CNFG newInActiveShopReasons)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_InActiveShopReasons_CNFG.Add(newInActiveShopReasons);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newInActiveShopReasons, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, newInActiveShopReasons);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newInActiveShopReasons, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertInActiveShopReasons Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetInActiveShopReasons(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var InActiveShopReasons = EF.TBL_InActiveShopReasons_CNFG.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, InActiveShopReasons, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetInActiveShopReasons Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateInActiveShopReasons(TBL_InActiveShopReasons_CNFG InActiveShopReasons)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_InActiveShopReasons_CNFG.AddOrUpdate(InActiveShopReasons);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, InActiveShopReasons, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, InActiveShopReasons);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, InActiveShopReasons, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateInActiveShopReasons Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
