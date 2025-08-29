using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class GunTypeMenu
    {
        public List<SelectListItem> GunTypes { get; set; }
        public int? GunTypeId { get; set; }
        public int? GunType { get; set; }   
    }
}