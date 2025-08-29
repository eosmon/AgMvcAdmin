using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using AppBase.FedEx.FedEx;

namespace AgMvcAdmin.Models.Common
{
    public class InStockModel
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int ManufId { get; set; }
        public int GroupId { get; set; }
        public int SubCatId { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public int TransTypeId { get; set; }
        public int RoundsPerBox { get; set; }
        public int UnitsCa { get; set; }
        public int UnitsWy { get; set; }
        public int Restocks { get; set; }
        public bool IsOnWeb { get; set; }
        public bool IsForSale { get; set; }
        public double Cost { get; set; }
        public string AcqDate { get; set; }
        public string Model { get; set; }
        public string TransId { get; set; }
        public string TransType { get; set; }
        public string GunDesc { get; set; }
        public string ManufName { get; set; }
        public string ImageUrl { get; set; }
        public string UpcCode { get; set; }
        public string ItemDesc { get; set; }
        public string AcqName { get; set; }
        public string AcqAddrFfl { get; set; }
        public string AcqCustType { get; set; }
        public string Customer { get; set; }
        public string CaliberName { get; set; }
        public string MfgPartNumber { get; set; }
        public string SubCatName { get; set; }
        public string Location { get; set; }

        public InStockModel() {}

        public InStockModel(int loc, int mfg, int ttp, int sub, int cus)
        {
            LocationId = loc;
            ManufId = mfg;
            TransTypeId = ttp;
            SubCatId = sub;
            CustomerId = cus;
        }


        public InStockModel(int id, int uca, int uwy, int rsk,  bool sal, bool owb, string img, string mfg, string mpn, string upc, string dsc)
        {
            Id = id;
            UnitsCa = uca;
            UnitsWy = uwy;
            Restocks = rsk;
            IsForSale = sal;
            IsOnWeb = owb;
            ImageUrl = img;
            ManufName = mfg;
            MfgPartNumber = mpn;
            UpcCode = upc;
            ItemDesc = dsc;
        }

        public InStockModel(int id, int uca, int uwy, int rpb, int rsk, string mpn, string upc, string mfg, string mdl, string dsc, string img, bool owb, bool sal)
        {
            Id = id;
            UnitsCa = uca;
            UnitsWy = uwy;
            RoundsPerBox = rpb;
            Restocks = rsk;
            MfgPartNumber = mpn;
            UpcCode = upc;
            ManufName = mfg;
            Model = mdl;
            ItemDesc = dsc;
            ImageUrl = img;
            IsOnWeb = owb;
            IsForSale = sal;
        }


        public InStockModel(int id, int ca, int wy, int rpb, bool onWeb, string transId, string img, string mpn, string upc, string mfgName, string model, string desc, string transType, string acqDate, string acqName, string acqCustType, string adqAdrFfl)
        {
            Id = id;
            UnitsCa = ca;
            UnitsWy = wy;
            RoundsPerBox = rpb;
            IsOnWeb = onWeb;
            TransId = transId;
            ImageUrl = img;
            MfgPartNumber = mpn;
            UpcCode = upc;
            ManufName = mfgName;
            Model = model;
            ItemDesc = desc;
            TransType = transType;
            AcqDate = acqDate;
            AcqName = acqName;
            AcqCustType = acqCustType;
            AcqAddrFfl = adqAdrFfl;
        }



        public InStockModel(int id, int mstId, bool onWeb, double cost, string acqDt, string transId, string desc, string mfg, string img, string name, string adFfl)
        {
            Id = id;
            MasterId = mstId;
            IsOnWeb = onWeb;
            Cost = cost;
            AcqDate = acqDt;
            TransId = transId;
            GunDesc = desc;
            ManufName = mfg;
            ImageUrl = img;
            AcqName = name;
            AcqAddrFfl = adFfl;

        }

 
        public InStockModel(int id, int ca, int wy, int rpb, int rstk, bool onWeb, string img, string mpn, string upc, string mfgName, string model, string desc)
        {
            Id = id;
            UnitsCa = ca;
            UnitsWy = wy;
            RoundsPerBox = rpb;
            Restocks = rstk;
            IsOnWeb = onWeb;
            ImageUrl = img;
            MfgPartNumber = mpn;
            UpcCode = upc;
            ManufName = mfgName;
            Model = model;
            ItemDesc = desc;
        }


        public InStockModel(int id, int ca, int wy, int rstk, string img, string mpn, string upc, string mfgName, string subCatName, string desc, bool onWeb)
        {
            Id = id;
            UnitsCa = ca;
            UnitsWy = wy;
            Restocks = rstk;
            ImageUrl = img;
            MfgPartNumber = mpn;
            UpcCode = upc;
            ManufName = mfgName;
            SubCatName = subCatName;
            ItemDesc = desc;
            IsOnWeb = onWeb;
        }

        public InStockModel(int id, int ca, int wy, int rstk, string img, string mpn, string upc, string mfgName, string subCatName, string desc, bool onWeb, bool isSale)
        {
            Id = id;
            UnitsCa = ca;
            UnitsWy = wy;
            Restocks = rstk;
            ImageUrl = img;
            MfgPartNumber = mpn;
            UpcCode = upc;
            ManufName = mfgName;
            SubCatName = subCatName;
            ItemDesc = desc;
            IsOnWeb = onWeb;
            IsForSale = isSale;
        }

    }
}