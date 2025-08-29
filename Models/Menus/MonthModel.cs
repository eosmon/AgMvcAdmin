using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class MonthModel
    {
        public List<SelectListItem> Month { get; set; }
        public int? MonthId { get; set; }
        public string MonthTxt { get; set; }
    }
}