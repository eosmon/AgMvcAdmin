using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class ChamberInModel
    {
        public List<SelectListItem> ChamberInches { get; set; }
        public int? ChamberInId { get; set; }
        public string ChamberInTxt { get; set; }
    }
}