using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int Units { get; set; }

        public double Msrp { get; set; }
        public double AskingPrice { get; set; }
        public double SalePrice { get; set; }
        public double GrossProfit { get; set; }
        public double GrossMargin { get; set; }

        public string ManufName { get; set; }
        public string ImageUrl { get; set; }
        public string ItemDesc { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string MfgPartNumber { get; set; }
        public string UpcCode { get; set; }
        public string StrStartDate { get; set; }
        public string StrEndDate { get; set; }

        public bool OnSale { get; set; }
        public bool InUse { get; set; }
        public bool IsUsed { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public SaleItem(){}

        public SaleItem(int mastId, bool onSale, double sale, double msr, double price, DateTime start, DateTime end)
        {
            MasterId = mastId;
            OnSale = onSale;
            SalePrice = sale;
            AskingPrice = price;
            Msrp = msr;
            StartDate = start;
            EndDate = end;
        }

        public SaleItem(int id, int mastId, bool used, bool sale, string cat, string subCat, string imgUrl, string desc, string upc)
        {
            Id = id;
            MasterId = mastId;
            IsUsed = used;
            OnSale = sale;
            CategoryName = cat;
            SubCategoryName = subCat;
            ImageUrl = imgUrl;
            ItemDesc = desc;
            UpcCode = upc;
        }


        public SaleItem(int id, int units, double msrp, double price, double sale, double profit, double margin, string manuf,
            string imgUrl, string sbcName, string desc, string mpn, string upc, string sDate, string eDate, bool isSale)
        {
            MasterId = id;
            Units = units;
            Msrp = msrp;
            AskingPrice = price;
            SalePrice = sale;
            GrossProfit = profit;
            GrossMargin = margin;
            ManufName = manuf;
            ImageUrl = imgUrl;
            SubCategoryName = sbcName;
            ItemDesc = desc;
            MfgPartNumber = mpn;
            UpcCode = upc;
            StrStartDate = sDate;
            StrEndDate = eDate;
            OnSale = isSale;
        }


    }
}