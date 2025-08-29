using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class AddCustomerMenu
    {
        public List<SelectListItem> Customers { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerId { get; set; } 
    }
}