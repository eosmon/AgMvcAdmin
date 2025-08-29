using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class ContentMenu
    {
        public List<SelectListItem> ContentList { get; set; }
        public int? Id { get; set; }
        public int? OgId { get; set; }
        public string OgType { get; set; }
        public string CatDesc { get; set; }
        public string PgName { get; set; }
    }
}