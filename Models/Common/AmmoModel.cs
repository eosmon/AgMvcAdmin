using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgMvcAdmin.Models.Menus;

namespace AgMvcAdmin.Models.Common
{
    public class AmmoModel
    {
        public int CostBasisId { get; set; }
        public int InStockId { get; set; }
        public int AmmoId { get; set; }
        public int StkId { get; set; }
        public int MasterId { get; set; }
        public int ManufId { get; set; }
        public int CaliberId { get; set; }
        public int AmmoGroupId { get; set; }
        public int AmmoManufId { get; set; }
        public int AmmoTypeId { get; set; }
        public int BulletTypeId { get; set; }
        public int GrainWeight { get; set; }
        public int RoundsPerBox { get; set; }
        public int SubCategoryId { get; set; }
        public int ConditionId { get; set; }
        public bool IsAddToBook { get; set; }
        public bool IsActive { get; set; }
        public bool IsSlug { get; set; }
        public bool IsOnWeb { get; set; }
        public bool IsActualPpt { get; set; }
        public string TagCat { get; set; }
        public string TagCal { get; set; }
        public string TagMfg { get; set; }
        public string TagBtp { get; set; }
        public string BulAbbrev { get; set; }
        public string UpcCode { get; set; }
        public string WebSearchUpc { get; set; }
        public string ImageUrl { get; set; }
        public string ItemDesc { get; set; }
        public string MfgPartNumber { get; set; }
        public string ModelName { get; set; }
        public string SearchText { get; set; }
        public string TagSku { get; set; }
        public double TagPrice { get; set; }
        public double Chamber { get; set; }
        public double ShotSizeWeight { get; set; }
        public ImageModel Images { get; set; }
        public AcctModel AcctModel { get; set; }
        public BoundBookModel BookModel { get; set; }
        public DayModel MenuDates { get; set; }

        public AmmoModel()
        {
        }

        public AmmoModel(int rpb, int grn, double prc, bool owb, string mfg, string cat, string cal, string mpn, string abv, string dsc, string btp, ImageModel im)
        {
            RoundsPerBox = rpb;
            GrainWeight = grn;
            TagPrice = prc;
            IsOnWeb = owb;
            TagMfg = mfg;
            TagCat = cat;
            TagCal = cal;
            MfgPartNumber = mpn;
            BulAbbrev = abv;
            ItemDesc = dsc;
            TagBtp = btp;
        }

        //var i1 = Int32.TryParse(dr["RoundsPerBox"].ToString(), out i0) ? Convert.ToInt32(dr["RoundsPerBox"]) : i0;
        //var i2 = Int32.TryParse(dr["GrainWeight"].ToString(), out i0) ? Convert.ToInt32(dr["GrainWeight"]) : i0;
        //var d1 = Double.TryParse(dr["AskPrice"].ToString(), out d0) ? Convert.ToDouble(dr["AskPrice"]) : d0;

        //var v1 = dr["SKU"].ToString();
        //var v2 = dr["CatName"].ToString();
        //var v3 = dr["CaliberName"].ToString();
        //var v4 = dr["MfgPartNumber"].ToString();
        //var v5 = dr["BulletAbbrev"].ToString();


        public AmmoModel(int id, int sct, int mfg, int cal, int btp, int wgt, int rpb, int cndId, double shz, double chb, bool onWeb, bool isAtv, bool slug, bool iPpt, string mpn, string desc, string upc, string webUpc, AcctModel acct, BoundBookModel book)
        {
            CostBasisId = id;
            SubCategoryId = sct;
            AmmoManufId = mfg;
            CaliberId = cal;
            BulletTypeId = btp;
            GrainWeight = wgt;
            RoundsPerBox = rpb;
            ConditionId = cndId;
            ShotSizeWeight = shz;
            Chamber = chb;
            IsOnWeb = onWeb;
            IsActive = isAtv;
            IsSlug = slug;
            IsActualPpt = iPpt;
            MfgPartNumber = mpn;
            ItemDesc = desc;
            UpcCode = upc;
            WebSearchUpc = webUpc;
            AcctModel = acct;
            BookModel = book;
        }

        //public AmmoModel(int mstId, int stkId, bool isAddBook, int subCatId, int mfgId, int calId, int condId, int bulletTypeId, int grainWt, int rdsPerBox, int units, string sku, string mfgPartNum, string upc, string desc, double askPrice, bool isSlug, double chamber, double shotSz, AcctModel acct, BoundBookModel book)
        //{
        //    MasterId = mstId;
        //    StkId = stkId;
        //    IsAddToBook = isAddBook;
        //    SubCategoryId = subCatId;
        //    AmmoManufId = mfgId;
        //    CaliberId = calId;
        //    ConditionId = condId;
        //    BulletTypeId = bulletTypeId;
        //    GrainWeight = grainWt;
        //    RoundsPerBox = rdsPerBox;
        //    UnitsCal = units;
        //    TagSku = sku;
        //    MfgPartNumber = mfgPartNum;
        //    UpcCode = upc;
        //    ItemDesc = desc;
        //    AskingPrice = askPrice;
        //    IsSlug = isSlug;
        //    Chamber = chamber;
        //    ShotSizeWeight = shotSz;
        //    AcctModel = acct;
        //    BookModel = book;
        //}

        // ADD AMMO
        public AmmoModel(bool onWeb, bool isAtv, bool isPpt, int subCatId, int mfgId, int calId, int condId, int bulletTypeId, int grainWt, int rdsPerBox, string mfgPartNum, string upc, string webUpc, string desc, bool isSlug, double chamber, double shotSz, AcctModel acct, BoundBookModel book)
        {
            IsOnWeb = onWeb;
            IsActive = isAtv;
            IsActualPpt = isPpt;
            SubCategoryId = subCatId;
            AmmoManufId = mfgId;
            CaliberId = calId;
            ConditionId = condId;
            BulletTypeId = bulletTypeId;
            GrainWeight = grainWt;
            RoundsPerBox = rdsPerBox;
            MfgPartNumber = mfgPartNum;
            UpcCode = upc;
            WebSearchUpc = webUpc;
            ItemDesc = desc;
            IsSlug = isSlug;
            Chamber = chamber;
            ShotSizeWeight = shotSz;
            AcctModel = acct;
            BookModel = book;
        }

        public AmmoModel(int subCatId, int mfgId, int calId, int bulletTypeId, int grainWt, int rdsPerBox, int condId, string mfgPartNum, string mod, string upc, bool isSlug, double chamber, double size)
        {
            SubCategoryId = subCatId;
            AmmoManufId = mfgId;
            CaliberId = calId;
            BulletTypeId = bulletTypeId;
            GrainWeight = grainWt;
            RoundsPerBox = rdsPerBox;
            ConditionId = condId;
            MfgPartNumber = mfgPartNum;
            ModelName = mod;
            UpcCode = upc;
            IsSlug = isSlug;
            Chamber = chamber;
            ShotSizeWeight = size;
        }

        public AmmoModel(bool isAddBook, int condId, string sku, AcctModel acct, BoundBookModel book)
        {
            IsAddToBook = isAddBook;
            ConditionId = condId;
            TagSku = sku;
            AcctModel = acct;
            BookModel = book;
        }

 
        public AmmoModel(int mfg, int cal, int atp, int btp, string sch)
        {
            ManufId = mfg;
            CaliberId = cal;
            AmmoTypeId = atp;
            BulletTypeId = btp;
            SearchText = sch;
        }


        /** AMMO - RESTOCK **/
        public AmmoModel(int isi, AcctModel acct, BoundBookModel book)
        {
            InStockId = isi;
            AcctModel = acct;
            BookModel = book;
        }

        /** AMMO - RESTOCK **/
        public AmmoModel(int isi, bool isOwb, bool isPpt, AcctModel acct, BoundBookModel book)
        {
            InStockId = isi;
            IsOnWeb = isOwb;
            IsActualPpt = isPpt;
            AcctModel = acct;
            BookModel = book;
        }



        public AmmoModel(string sku, int calId, int manufId, int bulletId, int weight, int rounds, string imgUrl, string desc, string mpn, AcctModel am)
        {
            TagSku = sku;
            CaliberId = calId;
            AmmoManufId = manufId;
            BulletTypeId = bulletId;
            GrainWeight = weight;
            RoundsPerBox = rounds;
            ImageUrl = imgUrl;
            ItemDesc = desc;
            MfgPartNumber = mpn;
            AcctModel = am;
        }

        // READ AMMO ITEM
        public AmmoModel(int isi, int calId, int mfgId, int bTyp, int bWgt, int rpb, int catId, int cnd,  bool slug, bool onWeb, bool isAtv, bool isPpt, string upc, string webUpc, string desc, string mpn, string sku,
                         double chb, double shtWgt, ImageModel imd, AcctModel acct, BoundBookModel book)
        {
            InStockId = isi;
            CaliberId = calId;
            AmmoManufId = mfgId;
            BulletTypeId = bTyp;    
            GrainWeight = bWgt;
            RoundsPerBox = rpb;
            SubCategoryId = catId;
            ConditionId = cnd;
            IsSlug = slug;
            IsOnWeb = onWeb;
            IsActive = isAtv;
            IsActualPpt = isPpt;
            UpcCode = upc;
            WebSearchUpc = webUpc;
            ItemDesc = desc;
            MfgPartNumber = mpn;
            TagSku = sku;
            Chamber = chb;
            ShotSizeWeight = shtWgt;
            Images = imd;
            AcctModel = acct;
            BookModel = book;
        }

        // MISC GUN ITEM
        public AmmoModel(int id, int calId, int mfgId, int bTyp, int bWgt, int rpb, int catId, int cnd, bool slug, bool onWeb, string upc, string webUpc, string img, string desc, string mpn, string sku,
                         double chb, double shtWgt, AcctModel acct, BoundBookModel book)
        {
            AmmoId = id;
            CaliberId = calId;
            AmmoManufId = mfgId;
            BulletTypeId = bTyp;
            GrainWeight = bWgt;
            RoundsPerBox = rpb;
            SubCategoryId = catId;
            ConditionId = cnd;
            IsSlug = slug;
            IsOnWeb = onWeb;
            UpcCode = upc;
            WebSearchUpc = webUpc;
            ImageUrl = img;
            ItemDesc = desc;
            MfgPartNumber = mpn;
            TagSku = sku;
            Chamber = chb;
            ShotSizeWeight = shtWgt;
            AcctModel = acct;
            BookModel = book;
        }

        //CUSTOM AMMO ADD
        public AmmoModel(int mid, int atp, int cid, int cnd, int btp, int rpb, int wgt, bool slg, string mdl, string mpn, double chb, double ssz)
        {
            ManufId = mid;
            AmmoTypeId = atp;
            CaliberId = cid;
            ConditionId = cnd;
            BulletTypeId = btp;
            RoundsPerBox = rpb;
            GrainWeight = wgt;
            IsSlug = slg;
            ModelName = mdl;
            MfgPartNumber = mpn;
            Chamber = chb;
            ShotSizeWeight = ssz;
        }

        //CONSIGNMENT ADD 
        public AmmoModel(int mid, int atp, int cid, int btp, int cnd, int rpb, int bwt, string mod, string mpn, string upc)
        {
            ManufId = mid;
            AmmoTypeId = atp;
            CaliberId = cid;
            BulletTypeId = btp;
            ConditionId = cnd;
            RoundsPerBox = rpb;
            GrainWeight = bwt;
            ModelName = mod;
            MfgPartNumber = mpn;
            UpcCode = upc;
        }


 



    }
}