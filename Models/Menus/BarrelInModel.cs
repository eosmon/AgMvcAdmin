using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class BarrelInModel
    {
        public List<SelectListItem> BarrelInches { get; set; }
        public int? BarrelInId { get; set; }
        public string BarrelInTxt { get; set; }
    }
}