using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class ShippingMenu
    {
        public List<SelectListItem> ShippingBoxes { get; set; }
        public int? ShippingBoxId { get; set; }
        public int? ShippingBox { get; set; }  
    }
}