using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgMvcAdmin.Common;

namespace AgMvcAdmin.Models.Common
{
    public class ShippingQuote
    {
        public double QuoteAmount { get; set; }
        public double BaseCost { get; set; }
        public double InsureCost { get; set; }
        public double SignatureCost { get; set; }
        public double RateDiscount { get; set; }
        public string ShipText { get; set; }
        public string SelectText { get; set; }
        public int SelectValue { get; set; }
        public int ShipOptionId { get; set; }
        public int TransitDays { get; set; }
        public int TransactionId { get; set; }
        public bool IsAmmo { get; set; }
        public bool IsOneRate { get; set; }
        public bool IsError { get; set; }
        public ShipTimes ShipOption { get; set; }
        public FreightCarriers Carrier { get; set; }
        public ShippingFees FeesOvernight { get; set; }
        public ShippingFees Fees2ndDay { get; set; }
        public ShippingFees FeesExpressSaver { get; set; }
        public ShippingFees FeesGround { get; set; }



        public ShippingQuote() { }

        public ShippingQuote(FreightCarriers car, int tid, bool err, bool amo, bool one, string txt)
        {
            Carrier = car;
            TransactionId = tid;
            IsError = err;
            IsAmmo = amo;
            IsOneRate = one;
            ShipText = txt;
        }

        public ShippingQuote(int val, string txt)
        {
            SelectValue = val;
            SelectText = txt;
        }

        public ShippingQuote(int val, string txt, double amt, double baseAmt)
        {
            SelectValue = val;
            SelectText = txt;
            QuoteAmount = amt;
            BaseCost = baseAmt;
        }

        public ShippingQuote(FreightCarriers car, int tid, int sop, int day, double cos, double ins, double sig, double dis, bool amo, bool one, bool err, string txt)
        {
            Carrier = car;
            TransactionId = tid;
            ShipOptionId = sop;
            TransitDays = day;
            BaseCost = cos;
            InsureCost = ins;
            SignatureCost = sig;
            RateDiscount = dis;
            IsAmmo = amo;
            IsOneRate = one;
            IsError = err;
            ShipText = txt;
        }
    }
}