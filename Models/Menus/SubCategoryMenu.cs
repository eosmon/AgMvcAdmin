using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class SubCategoryMenu
    {
        public List<SelectListItem> SubCategories { get; set; }
        public string SubCategoryName { get; set; }
        public int? SubCategoryId { get; set; }
    }
}