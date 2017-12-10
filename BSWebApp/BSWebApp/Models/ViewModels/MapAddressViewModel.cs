using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSWebApp.Models.ViewModels
{
    public class MapAddressViewModel
    {
        public string Address { get; set; }
        public TBL_ShopMapDetails shopMapDetails { get; set; }
    }
}