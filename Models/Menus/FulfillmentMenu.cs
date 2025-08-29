using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class FulfillmentMenu
    {
        public List<SelectListItem> Fulfillments { get; set; }
        public int? FulfillmentId { get; set; }
        public int? Count { get; set; }
        public string FulfillmentName { get; set; }
    }
}