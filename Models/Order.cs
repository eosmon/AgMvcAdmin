using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int CountHeading { get; set; }
        public int CountCart { get; set; }
        public int CountAddress { get; set; }
        public int CountLockModel { get; set; }
        public int CountDistUnits { get; set; }
        public int FscOptId { get; set; }
        public int LocationId { get; set; }
        public int SalesRepId { get; set; }
        public int OrderTypeId { get; set; }
        public double SubTotal { get; set; }
        public double SalesTax { get; set; }
        public double ExciseTax { get; set; }
        public double OrderTotal { get; set; }
        public double BalancePaid { get; set; }
        public double BalanceDue { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime FscExpDate { get; set; }
        public OrderTotals InvTotals { get; set; }
        public List<OrderTransaction> OrderTransactions { get; set; }
        public Payment Payments { get; set; }
        public OrderAddress ShopAddress { get; set; }
        public OrderAddress CustAddress { get; set; }
        public OrderCount SectionCount { get; set; }
        public string FscNumber { get; set; }
        public string StrOrderDate { get; set; }
        public string StrFscExpires { get; set; }
        public string StrLocation { get; set; }
        public string StrCustName { get; set; }
        public string StrCustPhone { get; set; }
        public string OrderCode { get; set; }
        public string Header { get; set; }
        public string SalesRep { get; set; }
        public string PayMethods { get; set; }
        public string FulfillTypes { get; set; }
        public string FflCode { get; set; }
        public string TermsCond { get; set; }
        public string LiabilityTxt { get; set; }
        public string InvoiceUrl { get; set; }
        public string Notes { get; set; }
        public bool IsQuote { get; set; }


        public Order() { }

        public Order(int oid, int cid, int srp, int lid, int fso, int otp, double sub, double tax, double otl, double bpd, double bdu, double ext, string odt, string fxp, string fsn, OrderAddress sad, OrderAddress cad)
        {
            OrderId = oid;
            CustomerId = cid;
            SalesRepId = srp;
            LocationId = lid;
            FscOptId = fso;
            OrderTypeId = otp;
            SubTotal = sub;
            SalesTax = tax;
            OrderTotal = otl;
            BalancePaid = bpd;
            BalanceDue = bdu;
            ExciseTax = ext;
            StrOrderDate = odt;
            StrFscExpires = fxp;
            FscNumber = fsn;
            ShopAddress = sad;
            CustAddress = cad;
        }


        // READ PRINT INVOICE
        public Order(int oid, bool iqt, double sub, double tax, double ttl, double bpd, double due, string ocd, string hdr, string rep, string pay, string ful, string ffl,
                     string tcd, string lib, string dat, string not, OrderAddress sad, OrderAddress cad)
        {
            OrderId = oid;
            IsQuote = iqt;
            SubTotal = sub;
            SalesTax = tax;
            OrderTotal = ttl;
            BalancePaid = bpd;
            BalanceDue = due;
            OrderCode = ocd;
            Header = hdr;
            SalesRep = rep;
            PayMethods = pay;
            FulfillTypes = ful;
            FflCode = ffl;
            TermsCond = tcd;
            LiabilityTxt = lib;
            StrOrderDate = dat;
            Notes = not;
            ShopAddress = sad;
            CustAddress = cad;
        }

        // READ ALL ORDERS
        public Order(int oid, double ttl, double apd, double adu, string ocd, string odt, string loc, string cus, string phn)
        {
            OrderId = oid;
            OrderTotal = ttl;
            BalancePaid = apd;
            BalanceDue = adu;
            OrderCode = ocd;
            StrOrderDate = odt;
            StrLocation = loc;
            StrCustName = cus;
            StrCustPhone = phn;
        }



    }
}