using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class InvoiceTotals
    {
        public double BalancePaid { get; set; }
        public double BalanceDue { get; set; }
        public double ItemTotal { get; set; }
        public double Shipping { get; set; }
        public double Fees { get; set; }
        public double Parts  { get; set; }
        public double Labor { get; set; }
        public double Subtotal { get; set; }
        public double SalesTax { get; set; }
        public double OrderTotal { get; set; }
        public double TransTotal { get; set; }
    }
}