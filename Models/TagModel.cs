using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class TagModel
    {
        public string TagSku { get; set; }
        public string MfgName { get; set; }
        public string Category { get; set; }
        public string Caliber { get; set; }
        public string BulType { get; set; }
        public string BulAbbrev { get; set; }
        public string GunType { get; set; }
        public string MfgPartNum { get; set; }
        public string Model { get; set; }
        public string SvcType { get; set; }
        public string SvcName { get; set; }
        public string Condition { get; set; }
        public string ItemDesc { get; set; }
        public string LongDesc { get; set; }
        public string UpcCode { get; set; }
        public string SerNumber { get; set; }

        public string Importer { get; set; }
        public string Action { get; set; }
        public string Finish { get; set; }
        public string BookModel { get; set; }


        public int ItemBasisID { get; set; }
        public int InStockId { get; set; }
        public int GunId { get; set; }
        public int AmmoId { get; set; }
        public int MerchId { get; set; }
        public int RdsPerBx { get; set; }
        public int GrainWt { get; set; }
        public int Capacity { get; set; }
        public int HiCapCap { get; set; }
        public int MagCount { get; set; }
        public int AcqSrcId { get; set; }
        public int LocId { get; set; }

        public double TagPrice { get; set; }
        public double ShotSzWt { get; set; }
        public double Chamber { get; set; }
        public double Barrel { get; set; }
        public double AskPrice { get; set; }
        public double Msrp { get; set; }
        public double SalePrice { get; set; }

        public bool IsSale { get; set; }
        public bool IsOnWeb { get; set; }
        public bool IsShtgn { get; set; }
        public bool IsSlug { get; set; }

        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public bool IsHidden { get; set; }
        public bool IsHideCa { get; set; }
        public bool IsUsed { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsRostOk { get; set; }
        public bool IsSglAtn { get; set; }
        public bool IsCurio { get; set; }
        public bool IsSglShot { get; set; }
        public bool IsPvtPty { get; set; }
        public bool IsCaOkay { get; set; }
 


        public DateTime CurExpDate { get; set; }

        public TagModel(){}

        /* AMMO */
        public TagModel(int id, int rpb, bool isSale, double prc, string sku, string mfg, string cat, string cal, string bul, string mpn, string svcTyp, string svcNam)
        {
            InStockId = id;
            RdsPerBx = rpb;
            IsSale = isSale;
            TagPrice = prc;
            TagSku = sku;
            MfgName = mfg;
            Category = cat;
            Caliber = cal;
            BulType = bul;
            MfgPartNum = mpn;
            SvcType = svcTyp;
            SvcName = svcNam;
        }

        /* AMMO RESTOCK */
        public TagModel(double ask, double chm, double swt, int rpb, int grn, bool sal, bool owb, bool ish, bool slg, string cat, string cal, string mpn,
            string abv, string mfg, string dsc, string btp)
        {
            TagPrice = ask;
            Chamber = chm;
            ShotSzWt = swt;
            RdsPerBx = rpb;
            GrainWt = grn;
            IsSale = sal;
            IsOnWeb = owb;
            IsShtgn = ish;
            IsSlug = slg;
            Category = cat;
            Caliber = cal;
            MfgPartNum = mpn;
            BulAbbrev = abv;
            MfgName = mfg;
            ItemDesc = dsc;
            BulType = btp;
        }

 
        /* MERCHANDISE */
        public TagModel(int id, bool isSale, double prc, string sku, string mfg, string cat, string cnd, string mpn, string dsc, string svc, string nam)
        {
            InStockId = id;
            IsSale = isSale;
            TagPrice = prc;
            TagSku = sku;
            MfgName = mfg;
            Category = cat;
            Condition = cnd;
            MfgPartNum = mpn;
            ItemDesc = dsc;
            SvcType = svc;
            SvcName = nam;
        }

        /* MERCHANDISE TAG  */
        public TagModel(double prc, bool ifs, bool web, string mfg, string cat, string dsc, string cnd, string mpn, string upc, string lds)
        {
            TagPrice = prc;
            IsSale = ifs;
            IsOnWeb = web;
            MfgName = mfg;
            Category = cat;
            ItemDesc = dsc;
            Condition = cnd;
            MfgPartNum = mpn;
            UpcCode = upc;
            LongDesc = lds;
        }




        /* MERCHANDISE RESTOCK  */
        public TagModel(int mid, double prc, bool ifs, string mfg, string cat, string dsc, string cnd, string mpn, string sku, string svc, string nam)
        {
            MerchId = mid;
            TagPrice = prc;
            IsSale = ifs;
            MfgName = mfg;
            Category = cat;
            ItemDesc = dsc;
            Condition = cnd;
            MfgPartNum = mpn;
            TagSku = sku;
            SvcType = svc;
            SvcName = nam;
        }


        /* GUN */
        public TagModel(int isi, int ibi, int cap, bool sal, double prc, double brl, string mfg, string typ, string cal, string cnd, string svc, string sku, string mdl, string mpn, string ser, string cus)
        {
            InStockId = isi;
            ItemBasisID = ibi;
            Capacity = cap;
            IsSale = sal;
            TagPrice = prc;
            Barrel = brl;
            MfgName = mfg;
            GunType = typ;
            Caliber = cal;
            Condition = cnd;
            SvcType = svc;
            TagSku = sku;
            Model = mdl;
            MfgPartNum = mpn;
            SerNumber = ser;
            SvcName = cus;
        }

        /* GUN RESTOCK */
        public TagModel(int cap, int hcc, int cnt, int loc, double prc, double msr, double sal, bool iow, bool atv, bool ver, bool hid, bool hca, bool usd, bool icr, bool cok, bool ros, 
                        bool san, bool cur, bool sst, bool ppt, string mdl, string fin, string cnd, string atn, string bmf, string imp, string bmd, string cal, string typ)
        {
            Capacity = cap;
            HiCapCap = hcc;
            MagCount = cnt;
            LocId = loc;
            AskPrice = prc;
            Msrp = msr;
            SalePrice = sal;
            IsOnWeb = iow;
            IsActive = atv;
            IsVerified = ver;
            IsHidden = hid;
            IsHideCa = hca;
            IsUsed = usd;
            IsCurrent = icr;
            IsCaOkay = cok;
            IsRostOk = ros;
            IsSglAtn = san;
            IsCurio = cur;
            IsSglShot = sst;
            IsPvtPty = ppt;
            Model = mdl;
            Finish = fin;
            Condition = cnd;
            Action = atn;
            MfgName = bmf;
            Importer = imp;
            BookModel = bmd;
            Caliber = cal;
            GunType = typ;
        }

 
    }
}