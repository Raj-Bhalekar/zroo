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
    public class InfrastructureCNFG_Activity: BSActivity
    {
        public BSEntityFramework_ResultType InsertInfrastructure(TBL_Infrastructure_CNFG newInfrastructure)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_Infrastructure_CNFG.Add(newInfrastructure);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newInfrastructure, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, newInfrastructure);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newInfrastructure, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertInfrastructure Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetInfrastructure(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var Infrastructure = EF.TBL_Infrastructure_CNFG.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, Infrastructure, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetInfrastructure Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateInfrastructure(TBL_Infrastructure_CNFG Infrastructure)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_Infrastructure_CNFG.AddOrUpdate(Infrastructure);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, Infrastructure, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, Infrastructure);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, Infrastructure, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateInfrastructure Failed", "", "", "", ex.Message);
                return result;
            }
        }


        public BSEntityFramework_ResultType GetInfrastructureByPostal(string postalCode)
        {
            try
            {
                var pinCode = Convert.ToInt32(postalCode);
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var Infrastructure =
                        EF.TBL_Infrastructure_CNFG.Where(inf => inf.PostalCodeID == pinCode)
                            .Select(inf => new {inf.InfrastructureID, inf.InfrastructureName}).ToArray();
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, Infrastructure, null, "Success");
                    return result;
                }
            }
            catch (InvalidCastException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, null, null, "Invalid Postal Code");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, null);
                
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, null, null, "Technical issue");
                logact.ErrorSetup("WebApp", "GetInfrastructure Failed", "", "", "", ex.Message);
                return result;
            }

        }
    }


}
