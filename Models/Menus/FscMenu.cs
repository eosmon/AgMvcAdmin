using System.Collections.Generic;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class FscMenu
    {
        public List<SelectListItem> FscList { get; set; }
        public int? FscId { get; set; }
        public int? Count { get; set; }
        public string FscName { get; set; }
    }
}