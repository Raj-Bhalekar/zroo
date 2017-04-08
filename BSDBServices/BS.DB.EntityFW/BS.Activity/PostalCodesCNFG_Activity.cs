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
    public class PostalCodesCNFG_Activity
    {
        public BSEntityFramework_ResultType InsertPostalCodesCNFG(TBL_PostalCodes_CNFG newPostalCodesCNFG)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_PostalCodes_CNFG.Add(newPostalCodesCNFG);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newPostalCodesCNFG, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, newPostalCodesCNFG, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newPostalCodesCNFG, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertPostalCodesCNFG Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetPostalCodesCNFG(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var PostalCodesCNFG = EF.TBL_PostalCodes_CNFG.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, PostalCodesCNFG, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetPostalCodesCNFG Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdatePostalCodesCNFG(TBL_PostalCodes_CNFG PostalCodesCNFG)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_PostalCodes_CNFG.AddOrUpdate(PostalCodesCNFG);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, PostalCodesCNFG, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, PostalCodesCNFG, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, PostalCodesCNFG, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdatePostalCodesCNFG Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}

