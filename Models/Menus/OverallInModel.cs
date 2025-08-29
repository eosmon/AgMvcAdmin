using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class OverallInModel
    {
        public List<SelectListItem> OverallInches { get; set; }
        public int? OverallId { get; set; }
        public string OverallTxt { get; set; }
    }
}