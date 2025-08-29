using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class GunFinishMenu
    {
        public List<SelectListItem> GunFinishes { get; set; }
        public int? GunFinishId { get; set; }
        public int? GunFinish { get; set; } 
    }
}