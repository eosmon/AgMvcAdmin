using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class WeightLbModel
    {
        public List<SelectListItem> WeightPounds { get; set; }
        public int? WtLbId { get; set; }
        public string WtLbTxt { get; set; }
    }
}