﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS.DB.EntityFW;

namespace BSWebApp.Models.ViewModels
{
    public class AddShopViewModel
    {
        public TBL_Shops ShopDetails { get; set; }
        public TBL_ShopsInPostalCodes ShopPostalDetails { get; set; }
    }
}