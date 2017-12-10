using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSWebApp.Common
{
    public class LoginResult
    {
        public int UserId { get; set; }
        public bool IsValid { get; set; }
        public List<MenuVM> MenuDetailList { get; set; }
        public List<ShopAssignedToUser> ShopAssignedList { get; set; }

        public static bool VerifyUserId(int userid)
        {
            if (userid > 0)
                return true;
            else
                return false;
        }
    }

    public class MenuVM
    {
        public int MenuID { get; set; }
        public int PageId { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public int? ParentMenuId { get; set; }
        public string MenuIconPath { get; set; }
    }

    public class ShopAssignedToUser
    {
        public int ShopId { get; set; }
        public string ShopName { get; set; }
    }
}