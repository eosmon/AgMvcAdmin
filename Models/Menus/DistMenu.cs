using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class DistMenu
    {
        public List<SelectListItem> Distributors { get; set; }
        public int? DistId { get; set; }
        public int? Count { get; set; }
        public string DistCode { get; set; }
    }
}