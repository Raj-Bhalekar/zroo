using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS.DB.EntityFW;
using Newtonsoft.Json;

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

    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
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

    public class ProductUpdateForm
    {
        public HttpPostedFileBase file { get; set; }
        public string ProductDetails { get; set; }

        public string imgId { get; set; }
    }



    public class UserDetails
    {
        public int UserId { get; set; }
        public string UserLoginName { get; set; }
    }

    class ImageDetails
    {
        public int ImgID { get; set; }
        public string ImgData { get; set; }
    }
}