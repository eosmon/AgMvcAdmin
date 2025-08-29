using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int InvoiceAddressId { get; set; }
        public int ShopAddressId { get; set; }
        public bool IsQuote { get; set; }
        public string SalesRep { get; set; }
        public DateTime OrderDate { get; set; }
        public InvoiceTotals InvTotals { get; set; }
        public List<InvoiceTransaction> Transactions { get; set; }
    }
}