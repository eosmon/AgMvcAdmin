using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class YearModel
    {
        public List<SelectListItem> Year { get; set; }
        public int? YearId { get; set; }
        public string YearTxt { get; set; }
    }
}