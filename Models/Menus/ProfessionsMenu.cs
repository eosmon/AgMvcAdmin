using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class ProfessionsMenu
    {
        public List<SelectListItem> Professions { get; set; }
        public string ProfessionName { get; set; }
        public int? ProfessionId { get; set; } 
    }
}