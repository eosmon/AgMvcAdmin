using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class WeightOzModel
    {
        public List<SelectListItem> WeightOunces { get; set; }
        public int? WtOzId { get; set; }
        public string WtOzTxt { get; set; }
    }
}