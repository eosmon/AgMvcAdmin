using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class OverallDecModel
    {
        public List<SelectListItem> OverallDecimal { get; set; }
        public decimal OverallDecId { get; set; }
        public string OverallDecTxt { get; set; }
    }
}