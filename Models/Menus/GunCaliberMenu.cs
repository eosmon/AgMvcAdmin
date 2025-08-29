using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class GunCaliberMenu
    {
        public List<SelectListItem> WebCalibers { get; set; }
        public List<SelectListItem> AllCalibers { get; set; }
        public int? CaliberId { get; set; }
        public int? Count { get; set; }
        public string CaliberName { get; set; }
    }
}