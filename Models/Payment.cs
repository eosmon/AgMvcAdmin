using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class Payment
    {
        public List<OrderPayment> OrderPayments { get; set; }
        public double AmountPaid { get; set; }
        public double EndingBalance { get; set; }
    }
}