using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class BulletTypeMenu
    {
        public List<SelectListItem> BulletTypes { get; set; }
        public int? BulletTypeId { get; set; }
        public int? Count { get; set; }
        public string BulletFullName { get; set; }
        public string BulletName { get; set; }
        public string BulletAbbrev { get; set; }
    }
}