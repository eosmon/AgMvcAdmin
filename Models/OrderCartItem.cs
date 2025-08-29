using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgMvcAdmin.Common;


namespace AgMvcAdmin.Models
{
    public class OrderCartItem
    {
        public int CartItemId { get; set; }
        public int RowId { get; set; }
        public int Units { get; set; }
        public int TransactionId { get; set; }
        public int CategoryId { get; set; }
        public int DistributorId { get; set; }
        public int TransTypeId { get; set; }
        public int LocationId { get; set; }
        public int SupplierId { get; set; }
        public int ItemBasisID { get; set; }
        public int FsdOptionID { get; set; }
        public int LockMakeID { get; set; }
        public int LockModelID { get; set; }
        public int FeeID { get; set; }
        public int FullfillmentTypeID { get; set; }
        public int InStockId { get; set; }
        public int TaxStatusId { get; set; }
        public bool IsFee { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsDojFscRow { get; set; }
        public bool IsAmmoRow { get; set; }
        public bool IsMrchRow { get; set; }
        public bool IsGunRow { get; set; }
        public bool IsShipRow { get; set; }
        public bool IsPickupRow { get; set; }
        public bool IsDeliverRow { get; set; }
        public bool IsRecoveryRow { get; set; }
        public bool IsTaxRow { get; set; }
        public bool IsSellerRow { get; set; }
        public bool IsServiceRow { get; set; }
        public bool IsFsDeviceRow { get; set; }
        public bool IsInvMenuRow { get; set; }
        public bool IsLock { get; set; }
        public double Price { get; set; }
        public double Freight { get; set; }
        public double Fees { get; set; }
        public double Parts { get; set; }
        public double Repairs { get; set; }
        public double TransferCost { get; set; }
        public double TransferTaxPaid { get; set; }
        public double TransferTaxDue { get; set; }
        public double TransferTaxAmount { get; set; }
        public double TaxRate { get; set; }
        public double TaxDue { get; set; }
        public double ExciseTaxRate { get; set; }
        public double ExciseTaxDue { get; set; }
        public double Extension { get; set; }
        public string Category { get; set; }
        public string SerialNumber { get; set; }
        public string SrcInvDesc { get; set; }
        public string ItemTitle { get; set; }
        public string ItemDesc { get; set; }
        public string AttorneyName { get; set; }
        public string AttorneyPhone { get; set; }
        public string AttorneyEmail { get; set; }
        public string RecoveryObjective { get; set; }
        public string Taxable { get; set; }
        public OrderAddress AddressSeller { get; set; }
        public OrderAddress AddressShipping { get; set; }
        public OrderAddress AddressPickup { get; set; }
        public OrderAddress AddressDelivery { get; set; }
        public GunRecoveryTypes RecoveryType { get; set; }
        public DateTime EstDeliveryDate { get; set; }
        public List<SelectListItem> MenuSupplier { get; set; }
        public List<SelectListItem> MenuInventoryItem { get; set; }
        public List<SelectListItem> MenuFsdOptions { get; set; }
        public List<SelectListItem> MenuLockMakes { get; set; }
        public List<SelectListItem> MenuLockModels { get; set; }
        public List<SelectListItem> MenuTaxOptions { get; set; }


        public OrderCartItem() { }


        public OrderCartItem(int tid, int fid, int unt, double prc, string des)
        {
            TransactionId = tid;
            FeeID = fid;
            Units = unt;
            Price = prc;
            ItemDesc = des;
        }

        public OrderCartItem(int rid, int cid, int unt, int fid, int cti, int ttp, double prc, double ext, string cat, string sid, string dsc, string tax, bool isa, bool ish, bool ipu, bool idl, bool itr, bool ife)
        {
            RowId = rid;
            CartItemId = cid;
            Units = unt;
            FeeID = fid;
            CategoryId = cti;
            TransTypeId = ttp;
            Price = prc;
            Extension = ext;
            Category = cat;
            SrcInvDesc = sid;
            ItemDesc = dsc;
            Taxable = tax;
            IsSellerRow = isa;
            IsShipRow = ish;
            IsPickupRow = ipu;
            IsDeliverRow = idl;
            IsTaxRow = itr;
            IsFee = ife;
        }


        public OrderCartItem(int iRid, int iCrt, int iTid, int iCat, int iTtp, int iLoc, int iSup, int iUnt, int iBas, int iFsd, int iLmk, int iLmd, int iFee, int iFid, 
                             int iIsi, int iDid, int iTsi, double dFrt, double dFee, double dPts, double dRep, double dPrc, double dTcs, double dTtp, double dTtd, 
                             double dTta, double dTrt, double dTdu, double dEtr, double dEtd, double dExt, bool bTax, bool bFee, bool bTrw, bool bSla, bool bSha, bool bPua, bool bDla, bool bImn, bool bFsc, bool bGun, 
                             bool bAmo, bool bMch, bool bShp, bool bPku, bool bDel, bool bRec, bool bSvc, bool bFsd, bool bLok, string sCat, string sSer, string sSid, string sTtl, 
                             string sDsc)
        {
            RowId = iRid;
            CartItemId = iCrt;
            TransactionId = iTid;
            CategoryId = iCat;
            TransTypeId = iTtp;
            LocationId = iLoc;
            SupplierId = iSup;
            Units = iUnt;
            ItemBasisID = iBas;
            FsdOptionID = iFsd;
            LockMakeID = iLmk;
            LockModelID = iLmd;
            FeeID = iFee;
            FullfillmentTypeID = iFid;
            InStockId = iIsi;
            DistributorId = iDid;
            TaxStatusId = iTsi;
            Freight = dFrt;
            Fees = dFee;
            Parts = dPts;
            Repairs = dRep;
            Price = dPrc;
            TransferCost = dTcs;
            TransferTaxPaid = dTtp;
            TransferTaxDue = dTtd;
            TransferTaxAmount = dTta;
            TaxRate = dTrt;
            TaxDue = dTdu;
            ExciseTaxRate = dEtr;
            ExciseTaxDue = dEtd;
            Extension = dExt;
            IsTaxable = bTax;
            IsFee = bFee;
            IsTaxRow = bTrw;
            IsSellerRow = bSla;
            IsShipRow = bSha;
            IsPickupRow = bPua;
            IsDeliverRow = bDla;
            IsInvMenuRow = bImn;
            IsDojFscRow = bFsc;
            IsGunRow = bGun;
            IsAmmoRow = bAmo;
            IsMrchRow = bMch;
            IsShipRow = bShp;
            IsPickupRow = bPku;
            IsDeliverRow = bDel;
            IsRecoveryRow = bRec;
            IsServiceRow = bSvc;
            IsFsDeviceRow = bFsd;
            IsLock = bLok;
            Category = sCat;
            SerialNumber = sSer;
            SrcInvDesc = sSid;
            ItemTitle = sTtl;
            ItemDesc = sDsc;
    }

    }
}