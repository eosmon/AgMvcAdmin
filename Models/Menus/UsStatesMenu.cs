using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class UsStatesMenu
    {
        public List<SelectListItem> UsStates { get; set; }
        public int? UsStateId { get; set; }
        public string UsStateName { get; set; }
        public string UsStateAbbrev { get; set; }
    }
}