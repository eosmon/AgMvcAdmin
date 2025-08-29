using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class ShipmentOneRate
    {
        public List<ShippingItem> OneRate { get; set; }
        public List<ShippingQuote> QuoteOneRate { get; set; }
        public string QuoteResult { get; set; }
        public int GroupBoxCount { get; set; }
    }
}