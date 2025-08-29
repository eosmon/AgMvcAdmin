using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class AddMenuItemModel
    {
        public List<SelectListItem> CustIndustry { get; set; }
        public List<SelectListItem> CustProfession { get; set; }
        public List<GunLockMenu> GunLockMfg { get; set; }
        public List<GunLockMenu> GunLockModel { get; set; }
        public List<GunCaliberMenu> Caliber { get; set; }
        public List<GunManufMenu> Manuf { get; set; }
        public List<BulletTypeMenu> Bullet { get; set; }
        public List<ColorMenu> Color { get; set; }
        public int SelectedId { get; set; }
        public int StandardId { get; set; }
        public bool IsDuplicate { get; set; }
    }
}