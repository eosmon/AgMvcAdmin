using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Web;
using System.Web.UI.WebControls;
using AppBase.FedEx.FedEx;

namespace AgMvcAdmin.Models.Common
{
    public class AcctModel
    {
        public bool SellerCollectedTax { get; set; }
        public bool CaTaxExempt { get; set; }
        public bool IsService { get; set; }
        public bool IsForSale { get; set; }
        public double ItemCost { get; set; }
        public double FreightCost { get; set; }
        public double ItemFees { get; set; }
        public double SellerTaxAmount { get; set; }
        public double AskingPrice { get; set; }
        public double SalePrice { get; set; }
        public double CustPricePaid { get; set; }
        public double Msrp { get; set; }
        public int CustomerId { get; set; }
        public int SellerId { get; set; }
        public int SupplierId { get; set; }
        public int UnitsCal { get; set; }
        public int UnitsWyo { get; set; }
        public int NotForSaleCal { get; set; }
        public int NotForSaleWyo { get; set; }
        public int LocationId { get; set; }
        public int TransTypeId { get; set; }
        public string SvcCustName { get; set; }
        public string PptSellName { get; set; }


        public AcctModel()
        {
        }

        public AcctModel(double cost, double freight, double fees)
        {
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
        }

        public AcctModel(double cost, double freight, double fees, double tax, bool gotTax)
        {
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
            SellerTaxAmount = tax;
            SellerCollectedTax = gotTax;
        }


        public AcctModel(double cost, double freight, double fees, double tax, double price, bool gotTax)
        {
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
            SellerTaxAmount = tax;
            AskingPrice = price;
            SellerCollectedTax = gotTax;
        }

        public AcctModel(double cost, double freight, double tax, double fees, bool gotTax, bool exmt)
        {
            ItemCost = cost;
            FreightCost = freight;
            SellerTaxAmount = tax;
            ItemFees = fees;
            SellerCollectedTax = gotTax;
            CaTaxExempt = exmt;
        }

        public AcctModel(double cost, double freight, double fees, double tax, double paid, int uca, int uwy, int nca, int nwy, int cid, int sid, bool gotTax, string svcCust)
        {
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
            SellerTaxAmount = tax;
            CustPricePaid = paid;
            UnitsCal = uca;
            UnitsWyo = uwy;
            NotForSaleCal = nca;
            NotForSaleWyo = nwy;
            CustomerId = cid;
            SupplierId = sid;
            SellerCollectedTax = gotTax;
            SvcCustName = svcCust;
        }

        public AcctModel(string svcCust, double cost, double freight, double fees, double tax, double paid, int uca, int uwy, int nca, int nwy, int cid, int sid, bool gotTax)
        {
            SvcCustName = svcCust;
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
            SellerTaxAmount = tax;
            CustPricePaid = paid;
            UnitsCal = uca;
            UnitsWyo = uwy;
            NotForSaleCal = nca;
            NotForSaleWyo = nwy;
            CustomerId = cid;
            SupplierId = sid;
            SellerCollectedTax = gotTax;
        }

        public AcctModel(double cost, double freight, double fees, double msrp, double tax, double price, double custPrc, int uca, int uwy, int custId, int supId, bool gotTax)
        {
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
            Msrp = msrp;
            SellerTaxAmount = tax;
            AskingPrice = price;
            CustPricePaid = custPrc;
            UnitsCal = uca;
            UnitsWyo = uwy;
            CustomerId = custId;
            SupplierId = supId;
            SellerCollectedTax = gotTax;
        }

        public AcctModel(double cost, double freight, double fees, double tax, double askPrice, double custPrice, int unitsCal, int unitsWyo, int nfsCa, int nfsWy, int cusId, int supId, bool gotTax, string svcCust)
        {
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees; 
            SellerTaxAmount = tax;
            SellerCollectedTax = gotTax;
            AskingPrice = askPrice;
            CustPricePaid = custPrice;
            UnitsCal = unitsCal;
            UnitsWyo = unitsWyo;
            NotForSaleCal = nfsCa;
            CustomerId = cusId;
            SupplierId = supId;
            NotForSaleWyo = nfsWy;
            SvcCustName = svcCust;
        }

        public AcctModel(double cost, double freight, double fees, double msrp, double tax, double askPrice, double custPrice, int unitsCal, int unitsWyo, int nfsCa, int nfsWy, int cusId, int supId, bool gotTax, string svcCust)
        {
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
            Msrp = msrp;
            SellerTaxAmount = tax;
            SellerCollectedTax = gotTax;
            AskingPrice = askPrice;
            CustPricePaid = custPrice;
            UnitsCal = unitsCal;
            UnitsWyo = unitsWyo;
            NotForSaleCal = nfsCa;
            CustomerId = cusId;
            SupplierId = supId;
            NotForSaleWyo = nfsWy;
            SvcCustName = svcCust;
        }


        public AcctModel(double paid, double cost, double freight, double fees, double msrp, double sellerTaxAmt, bool sellerCollTax, double askPrice, int unitsCal, int unitsWyo, int nfsCa, int nfsWy, int cusId, int supId, string svcCust)
        {
            CustPricePaid = paid;
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
            Msrp = msrp;
            SellerTaxAmount = sellerTaxAmt;
            SellerCollectedTax = sellerCollTax;
            AskingPrice = askPrice;
            UnitsCal = unitsCal;
            UnitsWyo = unitsWyo;
            NotForSaleCal = nfsCa;
            NotForSaleWyo = nfsWy;
            CustomerId = cusId;
            SupplierId = supId;
            SvcCustName = svcCust;
        }

        public AcctModel(double cost, double freight, double fees, double msrp, double sellerTaxAmt, bool sellerCollTax, double askPrice, double custPaid, int unitsCal, int unitsWyo, int custId, int locId)
        {
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
            Msrp = msrp;
            SellerTaxAmount = sellerTaxAmt;
            SellerCollectedTax = sellerCollTax;
            AskingPrice = askPrice;
            CustPricePaid = custPaid;
            UnitsCal = unitsCal;
            UnitsWyo = unitsWyo;
            CustomerId = custId;
            LocationId = locId;
        }

        public AcctModel(double cost, double freight, double fees, double sellerTaxAmt, bool sellerCollTax, double askPrice, int unitsCal, int unitsWyo)
        {
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
            SellerTaxAmount = sellerTaxAmt;
            SellerCollectedTax = sellerCollTax;
            AskingPrice = askPrice;
            UnitsCal = unitsCal;
            UnitsWyo = unitsWyo;
        }

        public AcctModel(bool caTaxEmpt, bool sellerCollTax, double cost, double freight, double fees, double sellerTaxAmt)
        {
            CaTaxExempt = caTaxEmpt;
            ItemCost =  cost;
            FreightCost = freight;
            ItemFees = fees;
            SellerCollectedTax = sellerCollTax;
            SellerTaxAmount = sellerTaxAmt;
        }

        // read gun by ID
        public AcctModel(int loc, int tid, bool forSale,  bool sellerCollTax, double cost, double freight, double fees, double sellerTaxAmt, double ask, double msrp, double salePrice, double custPrice, string svcName, string pptName)
        {
            LocationId = loc;
            TransTypeId = tid;
            IsForSale = forSale;
            SellerCollectedTax = sellerCollTax;
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
            SellerTaxAmount = sellerTaxAmt;
            AskingPrice = ask;
            Msrp = msrp;
            SalePrice = salePrice;
            CustPricePaid = custPrice;
            SvcCustName = svcName;
            PptSellName = pptName;
        }

        public AcctModel(int ttp, bool caTaxEmpt, bool sellerCollTax, double cost, double freight, double fees, double sellerTaxAmt, double ask, double msrp, double salePrice, double custPrice, string svcCust)
        {
            TransTypeId = ttp;
            CaTaxExempt = caTaxEmpt;
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
            SellerCollectedTax = sellerCollTax;
            SellerTaxAmount = sellerTaxAmt;
            AskingPrice = ask;
            Msrp = msrp;
            SalePrice = salePrice;
            CustPricePaid = custPrice;
            SvcCustName = svcCust;
        }

        public AcctModel(bool caTaxEmpt, bool sellerCollTax, double cost, double freight, double fees, double sellerTaxAmt, double ask, double msrp, double salePrice)
        {
            CaTaxExempt = caTaxEmpt;
            ItemCost = cost;
            FreightCost = freight;
            ItemFees = fees;
            SellerCollectedTax = sellerCollTax;
            SellerTaxAmount = sellerTaxAmt;
            AskingPrice = ask;
            Msrp = msrp;
            SalePrice = salePrice;
        }

        public AcctModel(bool gotTax, bool taxEmpt, double taxAmt, double cost, double fees, double ask, int uCal, int uWyo)
        {
            SellerCollectedTax = gotTax;
            CaTaxExempt = taxEmpt;
            SellerTaxAmount = taxAmt;
            ItemCost = cost;
            ItemFees = fees;
            AskingPrice = ask;
            UnitsCal = uCal;
            UnitsWyo = uWyo;
        }


        public AcctModel(int uCal, int uWyo, int nfsCal, int nfsWyo, double taxAmt, double ask, double sal, double msr, double cst, double frt, double fee, double cpr, bool sgt, bool exm, bool svc, string custName)
        {
            UnitsCal = uCal;
            UnitsWyo = uWyo;
            NotForSaleCal = nfsCal;
            NotForSaleWyo = nfsWyo;
            SellerTaxAmount = taxAmt;
            AskingPrice = ask;
            SalePrice = sal;
            Msrp = msr;
            ItemCost = cst;
            FreightCost = frt;
            ItemFees = fee;
            CustPricePaid = cpr;
            SellerCollectedTax = sgt;
            CaTaxExempt = exm;
            IsService = svc;
            SvcCustName = custName;
        }


        public AcctModel(int ttp, int uCal, int uWyo, int nfsCal, int nfsWyo, int cusId, int supId, double cst, double frt, double fee, double taxAmt, double cusPd, double comPr, bool sgt, bool exm, string custName)
        {
            TransTypeId = ttp;
            UnitsCal = uCal;
            UnitsWyo = uWyo;
            NotForSaleCal = nfsCal;
            NotForSaleWyo = nfsWyo;
            CustomerId = cusId;
            SupplierId = supId;
            ItemCost = cst;
            FreightCost = frt;
            ItemFees = fee;
            SellerTaxAmount = taxAmt;
            CustPricePaid = cusPd;
            AskingPrice = comPr;
            SellerCollectedTax = sgt;
            CaTaxExempt = exm;
            SvcCustName = custName;
        }

        public AcctModel(int uCal, int uWyo, int nfsCal, int nfsWyo, double prc, double sal, double msr, double cst, double frt, double fee, double cusPd, double taxAmt, bool sgt, string custName)
        {
            UnitsCal = uCal;
            UnitsWyo = uWyo;
            NotForSaleCal = nfsCal;
            NotForSaleWyo = nfsWyo;
            AskingPrice = prc;
            SalePrice = sal;
            Msrp = msr;
            ItemCost = cst;
            FreightCost = frt;
            ItemFees = fee;
            CustPricePaid = cusPd;
            SellerTaxAmount = taxAmt;
            SellerCollectedTax = sgt;
            SvcCustName = custName;
        }


    }


}