using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgMvcAdmin.Common;

namespace AgMvcAdmin.Models.Common
{
    public class Shipment
    {
        public string FxKey { get; set; }
        public string FxMeter { get; set; }
        public string FxPswd { get; set; }
        public string FxAccount { get; set; }
        public string TransId { get; set; }
        public string ServiceType { get; set; }
        public string ShipTime { get; set; }
        public string ShipStreet { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipZipCode { get; set; }
        public string ShipCountry { get; set; }
        public string DestStreet { get; set; }
        public string DestCity { get; set; }
        public string DestState { get; set; }
        public string DestZipCode { get; set; }
        public string DestCountry { get; set; }
        public string QuoteResult { get; set; }
        public int TransactionId { get; set; }
        public int PackageCount { get; set; }
        public int ItemBoxCount { get; set; }
        public bool IsOneRate { get; set; }
        public bool IsOrdnance { get; set; }
        public bool IsGround { get; set; }
        public bool IsError { get; set; }
        public bool HasAmmo { get; set; }
        public bool HasOneRate { get; set; }
        public bool HasItems { get; set; }
        public ShipmentAmmo ClsAmmo { get; set; }
        public ShipmentOneRate ClsOneRate { get; set; }
        public ShippingItem Item { get; set; }
        public List<ShippingItem> Items { get; set; }
        public List<ShippingQuote> Quote { get; set; }
        public FedExPkgTypes PackageType { get; set; }
        public ShipQuoteTypes QuoteType { get; set; }

    }
}