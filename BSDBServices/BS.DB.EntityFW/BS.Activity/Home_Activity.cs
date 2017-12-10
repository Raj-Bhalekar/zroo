using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using BS.DB.EntityFW.BS.Activity;
using BS.DB.EntityFW.CommonTypes;
using BS.DB.EntityFW.ViewModels;

namespace BS.DB.EntityFW.BS.Activity
{
    public class Home_Activity: BSActivity
    {
        public BSEntityFramework_ResultType ValidateLogin(string userId, string password)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    
                    var Userid = EF.TBL_ShopLoginDetails.Where(p => p.LoginName == userId && p.Password == password).Select(x=>x.ShopLoginDetailsID).First();
                    if (Userid>0)
                    {
                        Plugins_Activity obj = new Plugins_Activity();
                        var menus=obj.GetPluginMenuDetailList(userId);
                        var shopAssignedList = GetShopAssignedList(userId);
                        var rslt = new LoginResult()
                        { UserId= Userid,
                            IsValid = true,
                            MenuDetailList = menus,
                            ShopAssignedList = shopAssignedList
                        };
                        var result = new BSEntityFramework_ResultType(BSResult.Success, rslt, null, "Success");
                        return result;
                    }
                    return new BSEntityFramework_ResultType(BSResult.Success, new LoginResult()
                    { UserId = 0,
                        IsValid = false,
                        MenuDetailList = null,
                        ShopAssignedList = null
                    }
                    ,
                        null, "Success");
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
                logact.ErrorSetup("WebApp", "Signin Failed", "", "", "", ex.Message);
                return result;
            }

        }

        public IEnumerable<ShopAssignedToUser> GetShopAssignedList(string userID)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var shopList = EF.TBL_ShopLoginDetails.Where(sld => sld.LoginName == userID)
                        .Join(EF.TBL_UserAssignedShops, sld => sld.ShopLoginDetailsID, uas => uas.ShopLoginDetailsID,
                            (sld, uas) => new {sld.ShopLoginDetailsID, uas.ShopID, uas.IsActive})
                        .Where(UAS => UAS.IsActive == true)
                        .Join(EF.TBL_Shops, us => us.ShopID, ts => ts.ShopID,
                            (us, ts) => new ShopAssignedToUser() {ShopId = ts.ShopID, ShopName= ts.ShopName})
                       .ToArray();
                  
                    return shopList;
                }


            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return null;

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, userID, null, "Technical issue");
                logact.ErrorSetup("WebApp", "Shop list Failed", "", "", "", ex.Message);
                return null;
            }
        }



        public bool IsValidShopForUser(int userID,int ShopId)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var isValidShopChange = EF.TBL_ShopLoginDetails
                        .Where(sld => sld.ShopLoginDetailsID == userID)
                        .Join(EF.TBL_UserAssignedShops, sld => sld.ShopLoginDetailsID, uas => uas.ShopLoginDetailsID,
                            (sld, uas) => new {sld.ShopLoginDetailsID, uas.ShopID, uas.IsActive})
                        .Where(UAS => UAS.IsActive == true)
                         .Join(EF.TBL_Shops, us => us.ShopID, ts => ts.ShopID,
                            (us, ts) => new {ShopId = ts.ShopID, ShopName = ts.ShopName,isActive=ts.IsActive}).Any(x => x.isActive);
                    return isValidShopChange;
                }


            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return false;

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, userID, null, "Technical issue");
                logact.ErrorSetup("WebApp", "Shop Change IsValid Failed", "", "", "", ex.Message);
                return false;
            }
        }


        public BSEntityFramework_ResultType SignUp(TBL_ShopLoginDetails model)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    if (EF.TBL_ShopLoginDetails.Any(x => (x.LoginName == model.LoginName)))
                    {   
                        var result = new BSEntityFramework_ResultType(BSResult.Success, model, null, "Login Name already exist");
                        return result; 
                    }
                    else if(EF.TBL_ShopLoginDetails.Any(x => (x.EmailID == model.EmailID)))
                    {
                        var result = new BSEntityFramework_ResultType(BSResult.Success, model, null, "Email ID already exist");
                        return result;
                    } 
                    else
                    {
                        EF.TBL_ShopLoginDetails.Add(model);
                        EF.SaveChanges();

                        var result = new BSEntityFramework_ResultType(BSResult.Success, model, null, "Created Sucessfully");
                        return result;
                    }
                }

                
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return FormatException(dbValidationEx, model);

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, model, null, "Technical issue");
                logact.ErrorSetup("WebApp", "InsertShopOffer Failed", "", "", "", ex.Message);
                return result;
            }
        }
    }
}
