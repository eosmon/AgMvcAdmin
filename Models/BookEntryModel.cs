using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgMvcAdmin.Models.Common;

namespace AgMvcAdmin.Models
{
    public class BookEntryModel
    {
        public GunModel Gun { get; set; }
        public CustomerBaseModel Customer { get; set; }
        public FflLicenseModel Ffl { get; set; }
        public String BookName { get; set; }
        public int MenuSellerId { get; set; }
        public int SellerTypeId { get; set; }
        public int PickupLocId { get; set; }
    }
}