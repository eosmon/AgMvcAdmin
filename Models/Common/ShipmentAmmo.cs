using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class ShipmentAmmo
    {
        public List<ShippingItem> Ammo { get; set; }
        public List<ShippingQuote> QuoteAmmo { get; set; }
        public string QuoteResult { get; set; }
        public int GroupBoxCount { get; set; }
    }
}