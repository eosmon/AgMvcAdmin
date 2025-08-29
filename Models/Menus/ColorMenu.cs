using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class ColorMenu
    {
        public List<SelectListItem> Colors { get; set; }
        public int? ColorId { get; set; }
        public int? Count { get; set; }
        public string ColorName { get; set; }
    }
}