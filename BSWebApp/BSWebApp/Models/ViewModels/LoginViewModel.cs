using BS.DB.EntityFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSWebApp.Models.ViewModels
{
    public class LoginViewModel
    {
        public LoginModel loginModel { get; set; }
        public TBL_ShopLoginDetails signUpModel { get; set; }
    }
}