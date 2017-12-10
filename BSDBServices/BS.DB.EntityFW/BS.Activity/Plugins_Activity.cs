using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS.DB.EntityFW.CommonTypes;

namespace BS.DB.EntityFW.BS.Activity
{
  public class Plugins_Activity : BSActivity
    {
        public IEnumerable<MenuVM> GetPluginMenuDetailList(string userId)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                var menus=  EF.TBL_PlugIns.Where(x => x.IsFreeToAll.Value).Join(EF.TBL_Page_Plugins,
                        x => x.PlugInID, y => y.PlugInID,
                        (x, y) => new {x.PlugInID, y.PageID}).Join(EF.TBL_Page_Menues
                        , p => p.PageID, m => m.PageID,
                        (p, m) => new {p.PageID, p.PlugInID, m.MenuID}).Join(EF.TBL_Menues, pm => pm.MenuID
                        , mid => mid.MenuID , (pm, mid) => new {pm.PageID, mid.MenuName,mid.MenuID,mid.ParentMenuId, mid.MenuIconPath})
                .Join(EF.TBL_Pages, pmid => pmid.PageID, TP => TP.PageID, (pmid, TP) => new MenuVM()
                        {
                           PageId = pmid.PageID,
                          MenuName  = pmid.MenuName,
                           MenuURL = TP.PageURL,
                            ParentMenuId= pmid.ParentMenuId.Value,
                            MenuID = pmid.MenuID,
                            MenuIconPath = pmid.MenuIconPath

                }).Select(u => u)
                        .Union(EF.TBL_Menues.Where(mainMenu => mainMenu.ParentMenuId == null).Select(
                            mms => new MenuVM()
                            {
                                PageId = 0,
                                MenuName = mms.MenuName,
                                MenuURL = "#",
                                ParentMenuId = 0,
                                MenuID = mms.MenuID,
                                  MenuIconPath = mms.MenuIconPath
                            }
                            ))
                        .ToArray();
                   // var result = new BSEntityFramework_ResultType(BSResult.Success, Menus, null,
                    //"Success");
                    return menus;
                }

                
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return null;

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, userId, null, "Technical issue");
                logact.ErrorSetup("WebApp", "Menu list Failed", "", "", "", ex.Message);
                return null;
            }
        }
        public IEnumerable<MenuVM> GetPluginMenuDetailList(int userId,int shopId)
        {
            try
            {
                using (BSDBEntities EF = new BSDBEntities())
                {
                    var menus = EF.TBL_PlugIns.Where(x =>x.IsActive==true)
                        .Join(EF.TBL_Shop_PlugIn
                            ,x=>x.PlugInID 
                            ,sp=>sp.PlugInID
                            ,(x,sp)=>new {x.PlugInID,sp.ShopID,x.IsFreeToAll }
                            ).Where(sp=>sp.ShopID==shopId || sp.IsFreeToAll==true)
                        // .Join(EF.TBL_Shops,x=>x.ShopID,ts=>ts.ShopID,(x,ts)=>new {x.PlugInID,ts.ShopID})
                        .Join(EF.TBL_Page_Plugins,
                            x => x.PlugInID, y => y.PlugInID,
                            (x, y) => new { x.PlugInID, y.PageID })
                        .Join(EF.TBL_Page_Menues
                            , p => p.PageID, m => m.PageID,
                            (p, m) => new { p.PageID, p.PlugInID, m.MenuID })
                         .Join(EF.TBL_Menues, pm => pm.MenuID
                            , mid => mid.MenuID, (pm, mid) => new { pm.PageID, mid.MenuName, mid.MenuID, mid.ParentMenuId, mid.MenuIconPath })
                         .Join(EF.TBL_Pages, pmid => pmid.PageID, TP => TP.PageID, (pmid, TP) => new MenuVM()
                    {
                        PageId = pmid.PageID,
                        MenuName = pmid.MenuName,
                        MenuURL = TP.PageURL,
                        ParentMenuId = pmid.ParentMenuId.Value,
                        MenuID = pmid.MenuID,
                             MenuIconPath=   pmid.MenuIconPath
                         }).Select(u => u)
                            .Union(EF.TBL_Menues.Where(mainMenu => mainMenu.ParentMenuId == null).Select(
                                mms => new MenuVM()
                                {
                                    PageId = 0,
                                    MenuName = mms.MenuName,
                                    MenuURL = "#",
                                    ParentMenuId = 0,
                                    MenuID = mms.MenuID,
                                           MenuIconPath = mms.MenuIconPath
                                }
                                ))
                            .ToArray();
                    // var result = new BSEntityFramework_ResultType(BSResult.Success, Menus, null,
                    //"Success");
                    return menus;
                }


            }
            catch (DbEntityValidationException dbValidationEx)
            {
                return null;

            }
            catch (Exception ex)
            {
                var logact = new LoggerActivity();
                var result = new BSEntityFramework_ResultType(BSResult.Fail, userId, null, "Technical issue");
                logact.ErrorSetup("WebApp", "Menu list Failed", "", "", "", ex.Message);
                return null;
            }
        }
    }
}
