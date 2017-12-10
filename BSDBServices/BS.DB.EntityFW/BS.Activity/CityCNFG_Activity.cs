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
    public class CityCNFG_Activity:BSActivity
    {
        public BSEntityFramework_ResultType InsertCity(TBL_Cities_CNFG newCity)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_Cities_CNFG.Add(newCity);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newCity, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, newCity);
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newCity, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertCity Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetCity(int cityid)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var city = EF.TBL_Cities_CNFG.Find(cityid);
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, city, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetCity Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateCity(TBL_Cities_CNFG city)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_Cities_CNFG.AddOrUpdate(city);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, city, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {

                return FormatException(dbValidationEx, city);
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, city, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateCity Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
