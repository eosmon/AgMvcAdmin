using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models
{
    public class InvoiceTransaction
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int AddressOriginId { get; set; }
        public int AddressDestId { get; set; }
        public int ShipMethodId { get; set; }
        public int FulfillTypeId { get; set; }
        public int TaxStatusId { get; set; }
        public string TransType { get; set; }
        public string Notes { get; set; }
        public string FeeDesc { get; set; }
        public double ShipOneRate { get; set; }
        public double ShipAmmo { get; set; }
        public double ShipStandard { get; set; }
        public bool IsShipping { get; set; }
        public bool IsDelivery { get; set; }
        public bool IsPickup { get; set; }
        public InvoiceTotals TransTotals { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
        public List<SelectListItem> TaxStatus { get; set; }
        public List<SelectListItem> AddedFees { get; set; }
    }
}