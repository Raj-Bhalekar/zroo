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
    public class PostalCodesCNFG_Activity:BSActivity
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
                return FormatException(dbValidationEx, newPostalCodesCNFG);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newPostalCodesCNFG, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertPostalCodesCNFG Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetPostalCodesCNFG(string hint)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var PostalCodesCNFG =
                        EF.TBL_PostalCodes_CNFG.Where(p => p.PostCodeID.ToString().StartsWith(hint))
                            .Join(EF.TBL_Cities_CNFG, p => p.CityID, c => c.CityID, (p, c) => new
                                {
                                    PostalCodeId = p.PostCodeID,
                                    CityName = c.CityName,
                                    StateID = c.StateID
                                }
                            ).Join(EF.TBL_States_CNFG, R => R.StateID, s => s.StateID, (R, s) => new
                            {
                                PostalCodeId = R.PostalCodeId
                                ,CityName = R.CityName,
                                StateName = s.StateName

                            }).ToArray();
                    var result = new BSEntityFramework_ResultType(BSResult.FailForValidation, PostalCodesCNFG, null, "Success");
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
                return FormatException(dbValidationEx, PostalCodesCNFG);

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

