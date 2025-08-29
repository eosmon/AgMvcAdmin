using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class FeatureItem
    {
        public int MasterId { get; set; }
        public int PositionId { get; set; }
        public int AdSizeId { get; set; }
        public int Units { get; set; }
        public int CategoryId { get; set; }
        public int ConditionId { get; set; }
        
        public double Price { get; set; }
        public double Savings { get; set; }

        public bool IsPromo { get; set; }
        public bool OnSale { get; set; }
        public bool CaOkay { get; set; }
        public bool CaPptOk { get; set; }

        public string ManufName { get; set; }
        public string MfgBgColor { get; set; }
        public string MfgImageUrl { get; set; }
        public string PromoTxt { get; set; }
        public string PromoBgColor { get; set; }
        public string HeaderTitle { get; set; }
        public string MfgPartNumber { get; set; }
        public string ShortDesc { get; set; }
        public string TallDesc { get; set; }
        public string BigDesc { get; set; }
        public string ItemImageUrl { get; set; }
        public string CaText { get; set; }
        public string ItemCond { get; set; }
        public string NavUrl { get; set; }

        public FeatureItem(){}

        public FeatureItem(int mstId, int posId, int sizId, int catId, int units, int cndId, double price, double saveAmt, bool onSale, bool isPromo, bool caOkay, bool caPpt, string prTxt, string prBgClr, 
                           string mfgBgClr, string mfgImgUrl, string title, string itemImgUrl, string shortDesc, string tallDesc, string bigDesc, string caTxt, string cond, string mpn, string mfg, string nav)
        {
            MasterId = mstId;
            PositionId = posId;
            AdSizeId = sizId;
            CategoryId = catId;
            Units = units;
            ConditionId = cndId;
            Price = price;
            Savings = saveAmt;
            OnSale = onSale;
            IsPromo = isPromo;
            CaOkay = caOkay;
            CaPptOk = caPpt;
            PromoTxt = prTxt;
            PromoBgColor = prBgClr;
            MfgBgColor = mfgBgClr;
            MfgImageUrl = mfgImgUrl;
            HeaderTitle = title;
            ItemImageUrl = itemImgUrl;
            ShortDesc = shortDesc;
            TallDesc = tallDesc;
            BigDesc = bigDesc;
            CaText = caTxt;
            ItemCond = cond;
            MfgPartNumber = mpn;
            ManufName = mfg;
            NavUrl = nav;
        }
    }
}