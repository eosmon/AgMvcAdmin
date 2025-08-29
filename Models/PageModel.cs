using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgMvcAdmin.Common;
using AgMvcAdmin.Models.Menus;
using System.Web.Security;
using AgMvcAdmin.Models.Common;

namespace AgMvcAdmin.Models
{
    public class PageModel
    {
        public Pages Page { get; set; }
        public string TempPswd { get; set; }
        public MenuModel Menus { get; set; }
        public GunModel Gun { get; set; }
        public SecurityModel Login { get; set; }

        public PageModel(Pages p)
        {
            Page = p;
            Menus = new MenuModel(p);
            TempPswd = Membership.GeneratePassword(12, 0);
        }

    }
}