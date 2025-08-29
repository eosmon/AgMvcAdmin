using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models
{
    public class OrderTransaction
    {
        public int TransactionId { get; set; }
        public int CountGuns { get; set; }
        public int CountAmmo { get; set; }
        public int CountMerch { get; set; }
        public int TaxStatusId { get; set; }
        public int RecoveryObdId { get; set; }
        public int RowNumber { get; set; }
        public int LocationId { get; set; }
        public int FulfillmentTypeId { get; set; }
        public double Shipping { get; set; }
        public double Fees { get; set; }
        public double Parts { get; set; }
        public double Labor { get; set; }
        public double SubTotal { get; set; }
        public double SalesTax { get; set; }
        public double ExciseTax { get; set; }
        public double TransTotal { get; set; }
        public bool IsCaDojCflc { get; set; }
        public bool IsShipping { get; set; }
        public bool IsFees { get; set; }
        public bool IsParts { get; set; }
        public bool IsLabor { get; set; }
        public string OrderTitle { get; set; }
        public string OrderHeading { get; set; }
        public string Notes { get; set; }
        public string CflcNumber { get; set; }
        public string AttorneyName { get; set; }
        public string AttorneyPhone { get; set; }
        public string AttorneyEmail { get; set; }
        public string CaseOfficer { get; set; }
        public string OfficerPhone { get; set; }
        public string OfficerEmail { get; set; }
        public List<OrderCartItem> OrderCartItems { get; set; }
        public List<SelectListItem> MenuOrderFees { get; set; }

        public OrderTransaction() { }

        public OrderTransaction(int tid, int ctg, int cta, int ctm, int tsi, int rco, int row, int loc, int ftp, double shp, double fee, double par, double lab, double sub, double tax, double ext, double ttl, bool ish, bool ife, bool ipa, bool ilb, bool ilc,
            string otl, string ohd, string nts, string atn, string atp, string ate, string cof, string oph, string ofe, string cfl, List<SelectListItem> mnu)
        {
            TransactionId = tid;
            CountGuns = ctg;
            CountAmmo = cta;
            CountMerch = ctm;
            TaxStatusId = tsi;
            RecoveryObdId = rco;
            RowNumber = row;
            LocationId = loc;
            FulfillmentTypeId = ftp;
            Shipping = shp;
            Fees = fee;
            Parts = par;
            Labor = lab;
            SubTotal = sub;
            SalesTax = tax;
            ExciseTax = ext;
            TransTotal = ttl;
            IsShipping = ish;
            IsFees = ife;
            IsParts = ipa;
            IsLabor = ilb;
            IsCaDojCflc = ilc;
            OrderTitle = otl;
            OrderHeading = ohd;
            Notes = nts;
            AttorneyName = atn;
            AttorneyPhone = atp;
            AttorneyEmail = ate;
            CaseOfficer = cof;
            OfficerPhone = oph;
            OfficerEmail = ofe;
            CflcNumber = cfl;
            MenuOrderFees = mnu;
    }

    }
}