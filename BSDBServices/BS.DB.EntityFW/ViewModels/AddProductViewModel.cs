using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW.ViewModels
{
   public class AddProductViewModel
    {
        public TBL_Products ProductDetails { get; set; }
        public List<TBL_ProductImages> ProductImages { get; set; }
    }
}
