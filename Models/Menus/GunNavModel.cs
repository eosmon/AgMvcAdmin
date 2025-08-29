using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Menus
{
    public class GunNavModel
    {
        public int ItemCount { get; set; }
        public int IntValue { get; set; }
        public string GunType { get; set; }
        public string Link { get; set; }
        public string MenuText { get; set; }
    }
}