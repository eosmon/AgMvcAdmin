using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class GunSellerTypeMenu
    {
        public List<SelectListItem> GunSellerTypes { get; set; }
        public int? GunSellerTypeId { get; set; }
        public int? GunSellerType { get; set; }   
    }
}