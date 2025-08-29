using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class GunManufMenu
    {
        public List<SelectListItem> Manufacturers { get; set; }
        public int? SelectedManufId { get; set; }
        public int? ManufId { get; set; }
        public int? Count { get; set; }
        public string ManufName { get; set; }
    }
}