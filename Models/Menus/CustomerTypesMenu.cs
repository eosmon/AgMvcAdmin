using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class CustomerTypesMenu
    {
        public List<SelectListItem> CustomerTypes { get; set; }
        public string CustomerType { get; set; }
        public int? CustomerTypeId { get; set; }
    }
}