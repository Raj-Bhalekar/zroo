using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS.DB.EntityFW;

namespace BSWebApp.Models.ViewModels
{
    public class AddProductViewModel
    {
        public TBL_Products ProductDetails { get; set; }
        public List<TBL_ProductImages> ProductImages { get; set; }
    }
}