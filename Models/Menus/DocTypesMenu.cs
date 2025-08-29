using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class DocTypesMenu
    {
        public List<SelectListItem> DocumentTypes { get; set; }
        public int? DocTypeId { get; set; }
        public int? Count { get; set; }
        public string DocType { get; set; }
    }
}