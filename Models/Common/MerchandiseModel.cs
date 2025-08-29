using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using AgMvcAdmin.Models.Menus;
using AppBase.FedEx.FedEx;

namespace AgMvcAdmin.Models.Common
{
    public class MerchandiseModel
    {
        //public int MerchId { get; set; }
        public int CostBasisId { get; set; }
        public int MasterId { get; set; }
        public int InStockId { get; set; }
        public int CategoryId { get; set; }
        public int ColorId { get; set; }
        public int SubCategoryId { get; set; }
        public int ManufId { get; set; }
        public int ConditionId { get; set; }
        public int TransTypeId { get; set; }
        public int ShippingBoxId { get; set; }
        public int ShippingLbs { get; set; }
        public int ItemsPerBox { get; set; }
        public int LocationId { get; set; }
        public string ItemDesc { get; set; }
        public string ModelName { get; set; }
        public string LongDesc { get; set; }
        public string MfgPartNumber { get; set; }
        public string SearchText { get; set; }
        public string UpcCode { get; set; }
        public string WebSearchUpc { get; set; }
        public string TagSku { get; set; }
        public bool CaOkay { get; set; }
        public bool IsOnWeb { get; set; }
        public bool IsActive { get; set; }
        public double ShippingOzs { get; set; }
        public ImageModel Images { get; set; }
        public AcctModel AcctMdl { get; set; }
        public BoundBookModel BookMdl { get; set; }
        public DayModel MenuDates { get; set; }


        public MerchandiseModel(int mid, int sct, int cnd, int ssz, int lbs, int ipb, string mod, string des, string mpn, double ozs)
        {
            ManufId = mid;
            SubCategoryId = sct;
            ConditionId = cnd;
            ShippingBoxId = ssz;
            ShippingLbs = lbs;
            ItemsPerBox = ipb;
            ModelName = mod;
            ItemDesc = des;
            MfgPartNumber = mpn;
            ShippingOzs = ozs;
        }


        public MerchandiseModel()
        {
        }


        public MerchandiseModel(int mid, int cid, string str)
        {
            ManufId = mid;
            CategoryId = cid;
            SearchText = str;
        }

        public MerchandiseModel(int tType, int cond, int units, AcctModel acct)
        {
            TransTypeId = tType;
            ConditionId = cond;
            AcctMdl = acct;
        }

        public MerchandiseModel(int id, int ttp, int loc, bool iow, AcctModel actMdl, BoundBookModel bm)
        {
            InStockId = id;
            TransTypeId = ttp;
            LocationId = loc;
            IsOnWeb = iow;
            AcctMdl = actMdl;
            BookMdl = bm;
        }


        //public MerchandiseModel(int mstId, int ttpId, int cndId, int units, AcctModel actMdl)
        //{
        //    MasterId = mstId;
        //    TransTypeId = ttpId;
        //    ConditionId = cndId;
        //    UnitsCa = units;
        //    AcctMdl = actMdl;
        //}


        public MerchandiseModel(int mstId, int stkId, int subCatId, int mfgId, int cndId, int tType, int color, int boxId, int lbs, int ozs, int ipb, string sku, string upc, string mpn, string mdl, string desc, string longDesc, bool caOk, AcctModel actMdl)
        {
            MasterId = mstId;
            InStockId = stkId;
            SubCategoryId = subCatId;
            ManufId = mfgId;
            ConditionId = cndId;
            TransTypeId = tType;
            ColorId = color;
            ShippingBoxId = boxId;
            ShippingLbs = lbs;
            ShippingOzs = ozs;
            ItemsPerBox = ipb;
            TagSku = sku;
            UpcCode = upc;
            MfgPartNumber = mpn;
            ModelName = mdl;
            ItemDesc = desc;
            LongDesc = longDesc;
            CaOkay = caOk;
            AcctMdl = actMdl;
        }

        public MerchandiseModel(int subCatId, int mfgId, int cndId, int color, int boxId, int ipb, int lbs, string upc, string srchUpc, string mpn, string desc, string longDesc, string model, double ozs, bool caOk, bool onWeb, bool isAtv, AcctModel actMdl, BoundBookModel bkMdl)
        {
            SubCategoryId = subCatId;
            ManufId = mfgId;
            ConditionId = cndId;
            ColorId = color;
            ShippingBoxId = boxId;
            ItemsPerBox = ipb;
            ShippingLbs = lbs;
            UpcCode = upc;
            WebSearchUpc = srchUpc;
            MfgPartNumber = mpn;
            ItemDesc = desc;
            LongDesc = longDesc;
            ModelName = model;
            ShippingOzs = ozs;
            CaOkay = caOk;
            IsOnWeb = onWeb;
            IsActive = isAtv;
            AcctMdl = actMdl;
            BookMdl = bkMdl;
        }

        public MerchandiseModel(int id, int subCatId, int mfgId, int cndId, int color, int boxId, int ipb, int lbs, string sku, string upc, string srchUpc, string mpn, string desc, string longDesc, string model, double ozs, bool caOk, bool onWeb, bool isAtv, AcctModel actMdl, BoundBookModel bkMdl)
        {
            CostBasisId = id;
            SubCategoryId = subCatId;
            ManufId = mfgId;
            ConditionId = cndId;
            ColorId = color;
            ShippingBoxId = boxId;
            ItemsPerBox = ipb;
            ShippingLbs = lbs;
            TagSku = sku;
            UpcCode = upc;
            WebSearchUpc = srchUpc;
            MfgPartNumber = mpn;
            ItemDesc = desc;
            LongDesc = longDesc;
            ModelName = model;
            ShippingOzs = ozs;
            CaOkay = caOk;
            IsOnWeb = onWeb;
            IsActive = isAtv;
            AcctMdl = actMdl;
            BookMdl = bkMdl;
        }

        public MerchandiseModel(int isi, int tType, int subCat, int mfgId, int condId, int color, int boxId, int ipb, int lbs, string mpn, string upc, string srchUpc, string desc, string longDesc, string model, string sku, double ozs, bool caOk, bool onWeb, bool isAtv, ImageModel img, AcctModel act, BoundBookModel book)
        {
            InStockId = isi;
            TransTypeId = tType;
            SubCategoryId = subCat;
            ManufId = mfgId;
            ConditionId = condId;
            ColorId = color;
            ShippingBoxId = boxId;
            ItemsPerBox = ipb;
            ShippingLbs = lbs;
            MfgPartNumber = mpn;
            UpcCode = upc;
            WebSearchUpc = srchUpc;
            ItemDesc = desc;
            LongDesc = longDesc;
            ModelName = model;
            TagSku = sku;
            ShippingOzs = ozs;
            CaOkay = caOk;
            IsOnWeb = onWeb;
            IsActive = isAtv;
            Images = img;
            AcctMdl = act;
            BookMdl = book;
        }

        //CUSTOM ITEM ADD
        public MerchandiseModel(int mid, int sci, int cnd, int sid, int ipb, int wlb, double woz, string mdl, string mpn, string upc, string dsc)
        {
            ManufId = mid;
            SubCategoryId = sci;
            ConditionId = cnd;
            ShippingBoxId = sid;
            ItemsPerBox = ipb;
            ShippingLbs = wlb;
            ShippingOzs = woz;
            ModelName = mdl;
            MfgPartNumber = mpn;
            UpcCode = upc;
            ItemDesc = dsc;
        }


        // CONSIGNMENT ADD

        public MerchandiseModel(int mid, int sci, int cnd, int sid, int slb, int ipb, string mdl, string mpn, string upc, string des, double soz)
        {
            ManufId = mid;
            SubCategoryId = sci;
            ConditionId = cnd;
            ShippingBoxId = sid;
            ShippingLbs = slb;
            ItemsPerBox = ipb;
            ModelName = mdl;
            MfgPartNumber = mpn;
            UpcCode = upc;
            ItemDesc = des;
            ShippingOzs = soz;
        }
 
 
 
 
 


    }
}