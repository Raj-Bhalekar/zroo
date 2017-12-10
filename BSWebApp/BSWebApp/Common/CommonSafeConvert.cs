using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace BSWebApp.Common
{
    public class CommonSafeConvert
    {
        public static int ToInt(string val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return -1;
            }
        }
        public static int ToInt(object val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return -1;
            }
        }


        public static byte[] ConvertToBytesFromFile(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }


        #region Common functions
        public static string GetMenuString(List<MenuVM> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<ul class='sidebar-nav'>");
            if (data != null)
            {
                foreach (var menu in data.Where(x => x.ParentMenuId == null || x.ParentMenuId == 0))
                {
                    sb.Append(@"<li  class='sidebar-brand'><span>");
                    sb.Append(@"<span class='willBeInvisible'>
                <a class='tab' href = '"
                              + menu.MenuURL + "' > " + @"<span class='glyphiconglyphicon-shopping-cart'>"
                              + menu.MenuName
                              + "</span> </a ></span>");
                    sb.Append(@"</span >");
                    var listSubMenu = data.Where(xy => xy.ParentMenuId == menu.MenuID);
                    if (listSubMenu.Any())
                    {
                        sb.Append(@"<ul>");
                        foreach (var submenu in data.Where(xy => xy.ParentMenuId == menu.MenuID))
                        {

                            sb.Append(@"<li style = 'text-align: center' class='Items'><span>");
                            sb.Append(@"<span><a href = '" + submenu.MenuURL + "' > " +
                                      submenu.MenuName + " </a ></span>");
                            sb.Append(@"</span >");
                            sb.Append(@" </li> ");
                        }
                        sb.Append(@"</ul>");
                    }

                    sb.Append(@" </li> ");
                }

                sb.Append(@" </ul >");
                // Session["menuData"] = sb.ToString();
            }
            return sb.ToString();
        }

        public static string GetHomeLinkPageData(List<MenuVM> coreMenuData)
        {
            StringBuilder sb = new StringBuilder();
            if (coreMenuData != null && coreMenuData.Count > 0)
            {
                int rowLimit = 4;
                foreach (var menuItem in coreMenuData)
                {
                    if (rowLimit == 4)
                    {
                        sb.Append("<div class='row'>");
                    }

                    if (menuItem.ParentMenuId > 0)
                    {
                        sb.Append(@"<div class='col-sm-3'>
                    <a href = '" + menuItem.MenuURL + "'><img class='HomeLink' src='" + menuItem.MenuIconPath +
                                  "' title='" + menuItem.MenuName
                                  + @"'/>
                    </a>
                </div>");
                    }

                    if (rowLimit == 1)
                    {
                        rowLimit = 4;
                        sb.Append(@"</div>");
                    }
                    else
                    {
                        if (menuItem.ParentMenuId > 0)
                        {
                            rowLimit--;
                        }
                    }

                }
                sb.Append(@"</div>");
            }
            return sb.ToString();
        }


        #endregion
    }


}