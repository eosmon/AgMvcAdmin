using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class ShippingFees
    {
        public double TotalCost { get; set; }
        public double BaseAmount { get; set; }
        public double Insurance { get; set; }
        public double Signature { get; set; }
        public double RateDiscount { get; set; }


        public ShippingFees() { }


        public ShippingFees(double bas, double ins, double sig, double dis)
        {
            BaseAmount = bas;
            Insurance = ins;
            Signature = sig;
            RateDiscount = dis;
        }
    }
}