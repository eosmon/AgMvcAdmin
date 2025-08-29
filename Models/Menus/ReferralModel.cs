using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class ReferralModel
    {
        public List<SelectListItem> ReferralSources { get; set; }
        public int? ReferralId { get; set; }
        public string ReferralName { get; set; }
    }
}