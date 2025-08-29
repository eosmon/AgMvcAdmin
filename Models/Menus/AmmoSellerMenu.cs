using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class AmmoSellerMenu
    {
        public List<SelectListItem> SellerTypes { get; set; }
        public int? SellerTypeId { get; set; }
        public int? Count { get; set; }
        public string SellerTypeName { get; set; }
    }
}