using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class BarrelDecModel
    {
        public List<SelectListItem> BarrelDecimal { get; set; }
        public decimal BarrelDecId { get; set; }
        public string BarrelDecTxt { get; set; }
    }
}