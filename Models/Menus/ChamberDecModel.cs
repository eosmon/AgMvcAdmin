using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class ChamberDecModel
    {
        public List<SelectListItem> ChamberDecimal { get; set; }
        public decimal ChamberDecId { get; set; }
        public string ChamberDecTxt { get; set; }
    }
}