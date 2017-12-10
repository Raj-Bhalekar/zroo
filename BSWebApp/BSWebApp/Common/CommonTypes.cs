using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSWebApp.Common
{
    public class JqGridType
    {
        public string Page { get; set; }
        public string PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public object Data { get; set; }
    }

    

   public class ShopChangeStatus
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<MenuVM> MenuList { get; set; }
    }

    public class ShopChangeRequest
    {
        public int Userid { get; set; }
        public int ShopId { get; set; }
    }

    public class UserDetails
    {
        public int UserId { get; set; }
        public string UserLoginName { get; set; }
    }
}