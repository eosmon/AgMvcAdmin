using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class DocCategoriesMenu
    {
        public List<SelectListItem> DocumentCategories { get; set; }
        public int? DocCatId { get; set; }
        public int? Count { get; set; }
        public string DocCat { get; set; }
    }
}