using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class GunActionMenu
    {
        public List<SelectListItem> Actions { get; set; }
        public int? ActionId { get; set; }
        public int? Count { get; set; }
        public string ActionName { get; set; }
    }
}