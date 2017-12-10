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
    public class AdminOffersActivity:BSActivity
    {
        public BSEntityFramework_ResultType InsertAdminOffers(TBL_AdminOffers newAdminOffers)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_AdminOffers.Add(newAdminOffers);
                    EF.SaveChanges();
                }

                var result = new BSEntityFramework_ResultType(BSResult.Success, newAdminOffers, null, "Created Sucessfully");
                return result;
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, newAdminOffers);
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, newAdminOffers, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertAdminOffers Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType GetAdminOffers(int id)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var AdminOffers = EF.TBL_AdminOffers.Find(id);
                    var result = new BSEntityFramework_ResultType(BSResult.Success, AdminOffers, null, "Success");
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
                logact.ErrorSetup("WebApp", "GetAdminOffers Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public BSEntityFramework_ResultType UpdateAdminOffers(TBL_AdminOffers AdminOffers)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    EF.TBL_AdminOffers.AddOrUpdate(AdminOffers);
                    EF.SaveChanges();
                    var result = new BSEntityFramework_ResultType(BSResult.Success, AdminOffers, null, "Updated Successfully");
                    return result;
                }
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, AdminOffers);
            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, AdminOffers, null, "Technical issue");
                logact.ErrorSetup("WebApp", "UpdateAdminOffers Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
