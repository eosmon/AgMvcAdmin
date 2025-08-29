using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class MerchandiseMenu
    {
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> SubCategories { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    }
}