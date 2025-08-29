using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class TransTypesMenu
    {
        public List<SelectListItem> TransactionTypes { get; set; }
        public int? TransTypeId { get; set; }
        public int? TransType { get; set; }  
    }
}