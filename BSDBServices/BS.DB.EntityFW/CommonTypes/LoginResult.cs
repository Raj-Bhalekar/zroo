using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW.CommonTypes
{
   public class LoginResult
    {
        public int UserId { get; set; }
        public bool IsValid { get; set; }
        public IEnumerable<MenuVM> MenuDetailList { get; set; }

        public IEnumerable<ShopAssignedToUser> ShopAssignedList { get; set; }
    }

    public class MenuVM
    {
        public int MenuID { get; set; }
        public int? PageId { get; set; }
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

    public class ShopChangeStatus
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<MenuVM> MenuList { get; set; }
    }


    public class ShopChangeRequest
    {
        public int Userid { get; set; }
        public int ShopId { get; set; }
    }

    public class MapAddressViewModel
    {
        public string Address { get; set; }
        public TBL_ShopMapDetails shopMapDetails { get; set; }
    }
}
