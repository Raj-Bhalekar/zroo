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
    public class InActiveShopListCNFG_Activity:BSActivity
    {
        public BSEntityFramework_ResultType InsertInActiveShopList(TBL_InActiveShopList_CNFG newInActiveShopList)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_InActiveShopList_CNFG.Add(newInActiveShopList);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newInActiveShopList, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, newInActiveShopList);
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newInActiveShopList, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertInActiveShopList Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetInActiveShopList(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var InActiveShopList = EF.TBL_InActiveShopList_CNFG.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, InActiveShopList, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetInActiveShopList Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateInActiveShopList(TBL_InActiveShopList_CNFG InActiveShopList)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_InActiveShopList_CNFG.AddOrUpdate(InActiveShopList);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, InActiveShopList, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, InActiveShopList);
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, InActiveShopList, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateInActiveShopList Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
