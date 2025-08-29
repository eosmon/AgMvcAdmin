using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBase;

namespace AgMvcAdmin.Models.Common
{
    public class GunModel
    {
        public int Id { get; set; }
        public int CostBasisId { get; set; }
        public int InStockId { get; set; }
        public int InqGunId { get; set; }
        public int MasterId { get; set; }
        public int TotalUnits { get; set; }
        public int NewCount { get; set; }
        public int UsedCount { get; set; }
        public int HseUnits { get; set; }
        public int GunTypeId { get; set; }
        public int ManufId { get; set; }
        public int CaliberId { get; set; }
        public int CaOkId { get; set; }
        public int CapacityInt { get; set; }
        public int ActionId { get; set; }
        public int BarrelIn { get; set; }
        public int ChamberIn { get; set; }
        public int OverallIn { get; set; }
        public int FinishId { get; set; }
        public int ConditionId { get; set; }
        public int GunNavGroupId { get; set; }
        public int LockMakeId { get; set; }
        public int LockModelId { get; set; }
        public int ImageDist { get; set; }
        public int Condition { get; set; }
        public int ValueId { get; set; }
        public int WeightLb { get; set; }
        public int ShippingBoxId { get; set; }
        public int ItemsPerBox { get; set; }
        public double BarrelDec { get; set; }
        public double ChamberDec { get; set; }
        public double OverallDec { get; set; }
        //public double AskingPrice { get; set; }
        //public double SalePrice { get; set; }
        //public double UnitCost { get; set; }
        //public double Freight { get; set; }
        //public double Fees { get; set; }
        //public double Msrp { get; set; }
        public double PriceLow { get; set; }
        public double PriceHigh { get; set; }
        public double WeightOz { get; set; }

        public bool InProduction { get; set; }
        public bool IsCurModel { get; set; }
        public bool IsActive { get; set; }
        public bool IsUsed { get; set; }
        public bool IsHidden { get; set; }
        public bool IsVerified { get; set; }
        public bool InStock { get; set; }
        public bool IsLocalStock { get; set; }
        public bool IsLink { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsShipOversize { get; set; }
        public bool IsReqFfl { get; set; }
        public bool IsOnWeb { get; set; }
        public bool IsLeo { get; set; }
        public bool IsWebBased { get; set; }
        public bool ItemMissing { get; set; }
        public bool OrigBox { get; set; }
        public bool OrigPaperwork { get; set; }
        public bool OnDataFeed { get; set; }
        public bool IsOldSku { get; set; }
        public bool HasLock { get; set; }
        public string OldSku { get; set; }
        public string TransId { get; set; }
        public string AddEdit { get; set; }
        public string ActionType { get; set; }
        public string BulletType { get; set; }
        public string SubCatName { get; set; }
        public string ManufName { get; set; }
        public string ModelName { get; set; }
        public string CaliberTitle { get; set; }
        public string CondName { get; set; }
        public string GunType { get; set; }
        public string FinishName { get; set; }
        public string MfgPartNumber { get; set; }
        public string UpcCode { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string SerialNumber { get; set; }
        public string ImageName { get; set; }
        public string GunImgUrl { get; set; }
        public string SearchText { get; set; }
        public string NavString { get; set; }
        public string SeoUrl { get; set; }
        public string ReturnUrl { get; set; }
        public string SvcImg1 { get; set; }
        public string SvcImg2 { get; set; }
        public string SvcImg3 { get; set; }
        public string SvcImg4 { get; set; }
        public string SvcImg5 { get; set; }
        public string SvcImg6 { get; set; }
        public string WebSearchUpc { get; set; }
        public Guid UserKey { get; set; }
        public FilterModel Filters { get; set; }
        public CountModel Count { get; set; }
        public ImageModel Images { get; set; }
        public CaRestrictModel CaRestrict { get; set; }


        public GunModel(FilterModel f)
        {
            Filters = f;
        }

        public GunModel(FilterModel f, CaRestrictModel cr)
        {
            Filters = f;
            CaRestrict = cr;
        }

        public GunModel(CountModel ct)
        {
            Count = ct;
        }

        public GunModel(int gunId)
        {
            Id = gunId;
        }

        public GunModel(int gunId, string sku, bool iow, bool ios)
        {
            Id = gunId;
            OldSku = sku;
            IsOnWeb = iow;
            IsOldSku = ios;
        }

        public GunModel(int gunId, string sku)
        {
            Id = gunId;
            TransId = sku;
        }


        /* AMMUNITION - READ */
        public GunModel(int id, int units, int rpb, string imgName, string subCat, string manuf, string caliber, string mfgNum, string upc, string bullet)
        {
            Id = id;
            HseUnits = units;
            TotalUnits = rpb;
            ImageName = imgName;
            SubCatName = subCat;
            ManufName = manuf;
            CaliberTitle = caliber;
            MfgPartNumber = mfgNum;
            UpcCode = upc;
            BulletType = bullet;
        }

        /* MERCHANDISE - READ */
        public GunModel(int mstId, int units, int imgDist, string imgName, string subCat, string manuf, string mfgNum, string upc, string desc)
        {
            MasterId = mstId;
            HseUnits = units;
            ImageDist = imgDist;
            ImageName = imgName;
            SubCatName = subCat;
            ManufName = manuf;
            MfgPartNumber = mfgNum;
            UpcCode = upc;
            Description = desc;
        }


        public GunModel(int inqGunId, int actionId, int finishId, int capacity, double barrelDec, string longDescription)
        {
            InqGunId = inqGunId;
            ActionId = actionId;
            FinishId = finishId;
            CapacityInt = capacity;
            BarrelDec = barrelDec;
            LongDescription = longDescription;
        }

        /* READ FULL GUN */
        public GunModel(string tid, int atn, int fin, int cap, int wgt, int cnd, double brl, double chm, double ovl, double wtOz, bool curMdl, 
                        bool atv, bool usd, bool hdn, bool ver, bool box, bool ppw, string mdl, string upc, string dsc, string lds, string mpn, ImageModel img)
        {
            TransId = tid;
            ActionId = atn;
            FinishId = fin;
            CapacityInt = cap;
            WeightLb = wgt;
            ConditionId = cnd;
            BarrelDec = brl;
            ChamberDec = chm;
            OverallDec = ovl;
            WeightOz = wtOz;
            IsCurModel = curMdl;
            IsActive = atv;
            IsUsed = usd;
            IsHidden = hdn;
            IsVerified = ver;
            OrigBox = box;
            OrigPaperwork = ppw;
            ModelName = mdl;
            UpcCode = upc;
            Description = dsc;
            LongDescription = lds;
            MfgPartNumber = mpn;
            Images = img;
        }


        /* READ NON-SALE GUN */
        public GunModel(int id, int atn, int fin, int cap, double brl, bool box, bool ppw, string lds, ImageModel img)
        {
            InStockId = id;
            ActionId = atn;
            FinishId = fin;
            CapacityInt = cap;
            BarrelDec = brl;
            OrigBox = box;
            OrigPaperwork = ppw;
            LongDescription = lds;
            Images = img;
        }


        /* UPDATE NON-SALE GUN */
        public GunModel(string tid, int atn, int fin, int cap, double brl, bool box, bool ppw, string lds)
        {
            TransId = tid;
            ActionId = atn;
            FinishId = fin;
            CapacityInt = cap;
            BarrelDec = brl;
            OrigBox = box;
            OrigPaperwork = ppw;
            LongDescription = lds;
        }


        /* DATA FEED - ITEM DUPLICATES */
        public GunModel(int mfgId, int mstId, int capId, int atnId, double brlDm, string imgName,
                        string mfgName, string gunType, string calName, string mdlName, string mfgPtNm, string upcCode, 
                        string descrip, string atnName, string finName, FilterModel fm)
        {

            ManufId = mfgId;
            MasterId = mstId;
            CapacityInt = capId;
            ActionId = atnId;
            BarrelDec = brlDm;
            ImageName = imgName;
            ManufName = mfgName;
            GunType = gunType;
            CaliberTitle = calName;
            ModelName = mdlName;
            MfgPartNumber = mfgPtNm;
            UpcCode = upcCode;
            Description = descrip;
            ActionType = atnName;
            FinishName = finName;
            Filters = fm;
        }



        /* DATA FEED - GUN GRID */
        public GunModel(int mstId, int mfgId, int gtpId, int calId, int atnId, int finId, int cndId, int capId, int wgtLb, int imgDs,
            bool isActv, bool isVerf, bool isHide, bool isCurM, bool isOgBx, bool isOgPw, bool isRqFl, bool isUsed, bool itemMiss, 
            double brlDm, double ovlDm, double chmDm, double wgtOz, string imgName, string mfgName, string gunType, string calName, 
            string mdlName, string mfgPtNm, string upcCode, string atnName, string fnhName, string cndName, string descrip, string longDsc, 
            FilterModel filters, CaRestrictModel ca)
        {
            MasterId = mstId;
            ManufId = mfgId;
            GunTypeId = gtpId;
            CaliberId = calId;
            ActionId = atnId;
            FinishId = finId;
            ConditionId = cndId;
            CapacityInt = capId;
            WeightLb = wgtLb;
            ImageDist = imgDs;
            IsActive = isActv;
            IsVerified = isVerf;
            IsHidden = isHide;
            IsCurModel = isCurM;
            OrigBox = isOgBx;
            OrigPaperwork = isOgPw;
            IsReqFfl = isRqFl;
            IsUsed = isUsed;
            ItemMissing = itemMiss;
            BarrelDec = brlDm;
            OverallDec = ovlDm;
            ChamberDec = chmDm;
            WeightOz = wgtOz;
            ImageName = imgName;
            ManufName = mfgName;
            GunType = gunType;
            CaliberTitle = calName;
            ModelName = mdlName;
            MfgPartNumber = mfgPtNm;
            UpcCode = upcCode;
            ActionType = atnName;
            FinishName = fnhName;
            CondName = cndName;
            Description = descrip;
            LongDescription = longDsc;
            Filters = filters;
            CaRestrict = ca;


        }


        //public GunModel(int id, string tid, string addEdit, string upcCode, string mfgNum, string desc, string longDesc, string model, double brlDec, double ovrDec, double chbDec, double wgtOz, int units, int gunTypeId, int mfgId, int calId, int actionId, int finishId, int condId, 
        //    int wgtLb, int magCap, bool inProd, bool used, bool hide, bool active, bool verified, bool origBox, bool hasPpw, bool caHide, bool caLegal, 
        //    bool caRoster, bool caCurRel, bool caSaRev, bool caSsPst, bool caPpt)
        //{
        //    MasterId = id;
        //    TransId = tid;
        //    AddEdit = addEdit;
        //    UpcCode = upcCode;
        //    MfgPartNumber = mfgNum;
        //    Description = desc;
        //    LongDescription = longDesc;
        //    ModelName = model;
        //    BarrelDec = brlDec;
        //    OverallDec = ovrDec;
        //    ChamberDec = chbDec;
        //    WeightOz = wgtOz;
        //    TotalUnits = units;
        //    GunTypeId = gunTypeId;
        //    ManufId = mfgId;
        //    CaliberId = calId;
        //    ActionId = actionId;
        //    FinishId = finishId;
        //    ConditionId = condId;
        //    WeightLb = wgtLb;
        //    CapacityInt = magCap;
        //    InProduction = inProd;
        //    IsUsed = used;
        //    IsHidden = hide;
        //    IsActive = active;
        //    IsVerified = verified;
        //    OrigBox = origBox;
        //    OrigPaperwork = hasPpw;
        //    CaHide = caHide;
        //    CaOkay = caLegal;
        //    CaRosterOk = caRoster;
        //    CaCurioOk = caCurRel;
        //    CaSglActnOk = caSaRev;
        //    CaSglShotOk = caSsPst;
        //    CaPptOk = caPpt;
        //}

        // Add New Gun
        public GunModel(int mid, int isi, int mfg, int gtp, int atn, int fin, int cap, int cal, int lbs, int cnd, int lmk, int lmd, double brl, double chm, double ovl, double ozs, string sku, string mdl, string upc, string wsu, string mpn, string des,
            string lds, string ser, bool cmd, bool hid, bool atv, bool ver, bool owb, bool usd, bool box, bool ppw, bool ios)
        {
            MasterId = mid;
            InStockId = isi;
            ManufId = mfg;
            GunTypeId = gtp;
            ActionId = atn;
            FinishId = fin;
            CapacityInt = cap;
            CaliberId = cal;
            WeightLb = lbs;
            ConditionId = cnd;
            LockMakeId = lmk;
            LockModelId = lmd;
            BarrelDec = brl;
            ChamberDec = chm;
            OverallDec = ovl;
            WeightOz = ozs;
            OldSku = sku;
            ModelName = mdl;
            UpcCode = upc;
            WebSearchUpc = wsu;
            MfgPartNumber = mpn;
            Description = des;
            LongDescription = lds;
            SerialNumber = ser;
            IsCurModel = cmd;
            IsHidden = hid;
            IsActive = atv;
            IsVerified = ver;
            IsOnWeb = owb;
            IsUsed = usd;
            OrigBox = box;
            OrigPaperwork = ppw;
            IsOldSku = ios;
        }

        public GunModel(int id, int mfgId, int calId, int finId, int condId, int gunTypeId, int barIn, bool origBox, bool origPw, double brlDec, string gunDesc, string thumb, string model, string serial)
        {
            Id = id;
            ManufId = mfgId;
            CaliberId = calId;
            FinishId = finId;
            ConditionId = condId;
            GunTypeId = gunTypeId;
            BarrelIn = barIn;
            OrigBox = origBox;
            OrigPaperwork = origPw;
            BarrelDec = brlDec;
            Description = gunDesc;
            ImageName = thumb;
            ModelName = model;
            SerialNumber = serial;
        }

        public GunModel(string model)
        {
            ModelName = model;            
        }


        public GunModel(int id, int cap, int wtLb, int gtId, int calId, int atnId, int finId, int cndId, double brl, double ovl, double chm, double wtOz, bool atv, bool ver, bool hid, bool cur,
            bool ovs, bool obx, bool ppw, bool ffl, bool used, string mdl, 
            string upc, string mpn, string dsc, string lds, FilterModel filters, CaRestrictModel ca)
        {
            Id = id;
            CapacityInt = cap;
            WeightLb = wtLb;
            GunTypeId = gtId;
            CaliberId = calId;
            ActionId = atnId;
            FinishId = finId;
            ConditionId = cndId;
            BarrelDec = brl;
            OverallDec = ovl;
            ChamberDec = chm;
            WeightOz = wtOz;
            IsActive = atv;
            IsVerified = ver;
            IsHidden = hid;
            IsCurModel = cur;
            IsShipOversize = ovs;
            OrigBox = obx;
            OrigPaperwork = ppw;
            IsReqFfl = ffl;
            IsUsed = used;
            ModelName = mdl;
            UpcCode = upc;
            MfgPartNumber = mpn;
            Description = dsc;
            LongDescription = lds;
            Filters = filters;
            CaRestrict = ca;
        }




        public GunModel(string model, string upc, string spc, string desc, string mpn, string longDesc, string img, int isi, int mid, int gtp, int cal, int ids, int wlb, int aid, int fin, int cap, int cnd, int brlIn, int chbIn, 
                        int ovrIn, double brlDec, double chbDec, double ovrDec, double woz, bool ipd, bool obx, bool opw, bool usd, bool hid, bool act, bool ver, bool iwb,
                        ImageModel igm, CaRestrictModel ca)
        {
            ModelName = model;
            UpcCode = upc;
            WebSearchUpc = spc;
            Description = desc;
            MfgPartNumber = mpn;
            LongDescription = longDesc;
            GunImgUrl = img;
            InStockId = isi;
            ManufId = mid;
            GunTypeId = gtp;
            CaliberId = cal;
            ImageDist = ids;
            WeightLb = wlb;
            WeightOz = woz;
            ActionId = aid;
            FinishId = fin;
            CapacityInt = cap;
            ConditionId = cnd;
            BarrelIn = brlIn;
            ChamberIn = chbIn;
            OverallIn = ovrIn;
            BarrelDec = brlDec;
            ChamberDec = chbDec;
            OverallDec = ovrDec;
            InProduction = ipd;
            OrigBox = obx;
            OrigPaperwork = opw;
            IsUsed = usd;
            IsHidden = hid;
            IsActive = act;
            IsVerified = ver;
            IsWebBased = iwb;
            Images = igm;
            CaRestrict = ca;
        }



        public GunModel(string model, string serial, int mfgId, int gunTypeId, int calId, int finId, int condId, int barIn, bool origBox, bool origPw, double brlDec, string img1, string img2, string img3, string img4, string img5, string img6)
        {
            ModelName = model;
            SerialNumber = serial;
            ManufId = mfgId;
            GunTypeId = gunTypeId;
            CaliberId = calId;
            FinishId = finId;
            ConditionId = condId;
            BarrelIn = barIn;
            OrigBox = origBox;
            OrigPaperwork = origPw;
            BarrelDec = brlDec;
            SvcImg1 = img1;
            SvcImg2 = img2;
            SvcImg3 = img3;
            SvcImg4 = img4;
            SvcImg5 = img5;
            SvcImg6 = img6;
        }


        public GunModel(double lowPrice, double hiPrice, int newCount, int usedCount)
        {
            PriceLow = lowPrice;
            PriceHigh = hiPrice;
            NewCount = newCount;
            UsedCount = usedCount;
        }

        public GunModel(int mfgId, int calId, int atnId, double lowPrice, double hiPrice, int newCount, int usedCount)
        {
            ManufId = mfgId;
            CaliberId = calId;
            ActionId = atnId;
            PriceLow = lowPrice;
            PriceHigh = hiPrice;
            NewCount = newCount;
            UsedCount = usedCount;
        }

        public GunModel(int mfgId, int typeId, int calId, int atnId, int caOkId, string search)
        {
            ManufId = mfgId;
            GunTypeId = typeId;
            CaliberId = calId;
            ActionId = atnId;
            CaOkId = caOkId;
            SearchText = search;
        }


        public GunModel(int mfgId, int typeId, int calId, int caOkId, string search)
        {
            ManufId = mfgId;
            GunTypeId = typeId;
            CaliberId = calId;
            CaOkId = caOkId;
            SearchText = search;
        }


        public GunModel(int mstId, int isi, string imgUrl, string gunDesc, int mfgId, string modelName, int calId, int gunTypeId, bool onw)
        {
            MasterId = mstId;
            InStockId = isi;
            GunImgUrl = imgUrl;
            Description = gunDesc;
            ManufId = mfgId;
            ModelName = modelName;
            CaliberId = calId;
            GunTypeId = gunTypeId;
            IsOnWeb = onw;
        }

        public GunModel()
        {
        }

        public GunModel(int isi, int atn, int fin, int cnd, int cap, int lbs, int gtp, double oz, double brl, double chm, double ovl, bool ios, bool hide, bool atv, 
                        bool used, bool onWeb, bool ver, bool obx, bool cur, bool ppw, bool iwb, string upc, string desc, string webUpc, string mdl, 
                        string mpn, string lds, string tid, string mfg, string caliber, string serial, string gunType, string sku)
        {
            InStockId = isi;
            ActionId = atn;
            FinishId = fin;
            ConditionId = cnd;
            CapacityInt = cap;
            WeightLb = lbs;
            GunTypeId = gtp;
            WeightOz = oz;
            BarrelDec = brl;
            ChamberDec = chm;
            OverallDec = ovl;
            IsOldSku = ios;
            IsHidden = hide;
            IsActive = atv;
            IsUsed = used;
            IsOnWeb = onWeb;
            IsVerified = ver;
            OrigBox = obx;
            IsCurModel = cur;
            OrigPaperwork = ppw;
            IsWebBased = iwb;
            UpcCode = upc;
            Description = desc;
            WebSearchUpc = webUpc;
            ModelName = mdl;
            MfgPartNumber = mpn;
            LongDescription = lds;
            TransId = tid;
            ManufName = mfg;
            CaliberTitle = caliber;
            SerialNumber = serial;
            GunType = gunType;
            OldSku = sku;
        }

        public GunModel(int id, int atn, int fin, int cnd, int cap, int lbs, double oz, double brl, double chm, double ovl, bool hide, bool atv, bool used,
            bool onWeb, bool ver, bool obx, bool cur, bool ppw, bool ios, string sku, string upc, string desc, string webUpc, string mdl, string mpn, string lds, string tid)
        {
            CostBasisId = id;
            ActionId = atn;
            FinishId = fin;
            ConditionId = cnd;
            CapacityInt = cap;
            WeightLb = lbs;
            WeightOz = oz;
            BarrelDec = brl;
            ChamberDec = chm;
            OverallDec = ovl;
            IsHidden = hide;
            IsActive = atv;
            IsUsed = used;
            IsOnWeb = onWeb;
            IsVerified = ver;
            OrigBox = obx;
            IsCurModel = cur;
            OrigPaperwork = ppw;
            IsOldSku = ios;
            OldSku = sku;
            UpcCode = upc;
            Description = desc;
            WebSearchUpc = webUpc;
            ModelName = mdl;
            MfgPartNumber = mpn;
            LongDescription = lds;
            TransId = tid;
        }

        // ADD CUSTOM GUN TO ORDER
        public GunModel(int aid, int fid, int cnd, int mid, int gtp, int cid, int cap, int ipb, int wlb, int ssz, double brl, double woz, bool box, bool ppw, string mdl, string mpn, string upc)
        {
            ActionId = aid;
            FinishId = fid;
            ConditionId = cnd;
            ManufId = mid;
            GunTypeId = gtp;
            CaliberId = cid;
            CapacityInt = cap;
            ItemsPerBox = ipb;
            WeightLb = wlb;
            ShippingBoxId = ssz;
            BarrelDec = brl;
            WeightOz = woz;
            OrigBox = box;
            OrigPaperwork = ppw;
            ModelName = mdl;
            MfgPartNumber = mpn;
            UpcCode = upc;
        }

        // CONSIGNMENT GUN ADD
        public GunModel(int mst, int mid, int gtp, int cid, int aid, int fid, int cap, int cnd, int lmk, int lmd, bool box, bool ppw, bool loc, string mod, string mpn, string upc, string ser, double brl)
        {
            MasterId = mst;
            ManufId = mid;
            GunTypeId = gtp;
            CaliberId = cid;
            ActionId = aid;
            FinishId = fid;
            CapacityInt = cap;
            ConditionId = cnd;
            LockMakeId = lmk;
            LockModelId = lmd;
            OrigBox = box;
            OrigPaperwork = ppw;
            HasLock = loc;
            ModelName = mod;
            MfgPartNumber = mpn;
            UpcCode = upc;
            SerialNumber = ser;
            BarrelDec = brl;
        }

        // SERVICES: INCLUDE WEIGHT
        public GunModel(int mst, int mid, int gtp, int cid, int aid, int fid, int cap, int cnd, int lmk, int lmd, int wlb, bool box, bool ppw, bool loc, string mod, string mpn, string upc, string ser, double brl, double woz)
        {
            MasterId = mst;
            ManufId = mid;
            GunTypeId = gtp;
            CaliberId = cid;
            ActionId = aid;
            FinishId = fid;
            CapacityInt = cap;
            ConditionId = cnd;
            LockMakeId = lmk;
            LockModelId = lmd;
            WeightLb = wlb;
            OrigBox = box;
            OrigPaperwork = ppw;
            HasLock = loc;
            ModelName = mod;
            MfgPartNumber = mpn;
            UpcCode = upc;
            SerialNumber = ser;
            BarrelDec = brl;
            WeightOz = woz;
        }




    }
}