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
    public class InfrastructureCategoriesCNFG_Activity:BSActivity
    {
        public BSEntityFramework_ResultType InsertInfrastructureCategories(TBL_InfrastructureCategories_CNFG newInfrastructureCategories)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_InfrastructureCategories_CNFG.Add(newInfrastructureCategories);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newInfrastructureCategories, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, newInfrastructureCategories);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newInfrastructureCategories, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertInfrastructureCategories Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetInfrastructureCategories(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var InfrastructureCategories = EF.TBL_InfrastructureCategories_CNFG.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, InfrastructureCategories, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetInfrastructureCategories Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateInfrastructureCategories(TBL_InfrastructureCategories_CNFG InfrastructureCategories)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_InfrastructureCategories_CNFG.AddOrUpdate(InfrastructureCategories);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, InfrastructureCategories, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, InfrastructureCategories);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, InfrastructureCategories, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateInfrastructureCategories Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
