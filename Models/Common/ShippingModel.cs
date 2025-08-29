using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class ShippingModel
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public int CarrierId { get; set; }
        public int ShipOptionId { get; set; }
        public double QuoteAmount { get; set; }
        public string RateDesc { get; set; }
    }
}