using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class DayModel
    {
        public List<SelectListItem> Day { get; set; }
        public int? DayId { get; set; }
        public string DayTxt { get; set; }
    }
}