using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class ShippingCollection
    {
        public double InsureCost { get; set; }
        public List<ShippingQuote> QuoteMenu { get; set; }
    }
}