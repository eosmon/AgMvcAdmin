using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class GunConditionMenu
    {
        public List<SelectListItem> GunConditions { get; set; }
        public int? GunConditionId { get; set; }
        public int? GunCondition { get; set; }  
    }
}