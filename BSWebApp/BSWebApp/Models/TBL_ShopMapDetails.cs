using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BS.DB.EntityFW;

namespace BSWebApp.Models
{
    public partial class TBL_ShopMapDetails
    {
        public int ShopId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }

        public virtual TBL_ShopLoginDetails TBL_ShopLoginDetails { get; set; }
        public virtual TBL_ShopLoginDetails TBL_ShopLoginDetails1 { get; set; }
        public virtual TBL_Shops TBL_Shops { get; set; }
    }
}