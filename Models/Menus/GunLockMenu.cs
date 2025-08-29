using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class GunLockMenu
    {
        public List<SelectListItem> GunLockMdls { get; set; }
        public List<SelectListItem> GunLockMfgs { get; set; }
        public int? LockManufId { get; set; }
        public int? LockModelId { get; set; }
        public string LockManuf { get; set; }
        public string LockModel { get; set; }
    }
}