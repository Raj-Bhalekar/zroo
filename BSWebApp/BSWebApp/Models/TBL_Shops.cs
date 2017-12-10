
namespace BS.DB.EntityFW
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class TBL_Shops
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
     

        public int ShopID { get; set; }

        [Required]
        [Display(Name = "Shop Name")]
        public string ShopName { get; set; }
        [Required]
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string ContactNumber { get; set; }


        [Display(Name = "WhatsApp/Hike Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string WhatsAppHikeNumber { get; set; }

        [Required]
        [Display(Name = "Shop Type")]
        public int ShopTypeID { get; set; }
             
        public string ShopTypeName { get; set; }

        [Display(Name = "Shop Opening Time")]
        public Nullable<System.TimeSpan> ShopOpningTime { get; set; }

        [Display(Name = "Shop Closing Time")]
        public Nullable<System.TimeSpan> ShopClosingTime { get; set; }

        [Display(Name="Ïnfrastructure Name")]
        public Nullable<int> InfrastructureID { get; set; }
        public string InfrastructureName { get; set; }

        [Required]
        [Display(Name = "Shop Address")]
        public string ShopAddress { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="Email ID")]
        public string Email { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Shop Category")]
        public int ShopCategoryID { get; set; }
        public string ShopCategoryName { get; set; }

        
        [Display(Name = "Shop Directory")]
        public Nullable<int> ShopDirectoryID { get; set; }
        public string ShopDirectoryName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
     
    }
}