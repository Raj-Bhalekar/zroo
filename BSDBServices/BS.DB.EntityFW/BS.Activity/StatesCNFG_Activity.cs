

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
    public class StatesCNFG_Activity
    {
        public BSEntityFramework_ResultType InsertState(TBL_States_CNFG newState)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_States_CNFG.Add(newState);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newState, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, newState, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newState, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertState Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetState(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var state = EF.TBL_States_CNFG.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.Success, state, null, "Success");
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

        public BSEntityFramework_ResultType GetALLStates()
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var states = EF.TBL_States_CNFG.Select(st =>st).ToList();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, states, null, "Success");
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


        public BSEntityFramework_ResultType UpdateState(TBL_States_CNFG state)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_States_CNFG.AddOrUpdate(state);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, state, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, state, dbValidationEx, "Validation Failed");
                return result;
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, state, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateState Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
