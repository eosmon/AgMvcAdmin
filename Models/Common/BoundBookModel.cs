using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class BoundBookModel
    {
 

        public bool IsCorrected { get; set; }
        public bool IsDisposed { get; set; }
        public bool IsHiCaps { get; set; }
        public bool IsOriginal { get; set; }
        public bool PrintTag { get; set; }
        public bool IsSale { get; set; }
        public bool IsTransfer { get; set; }
        public bool IsConsign { get; set; }
        public bool IsShipping { get; set; }
        public bool IsStorage { get; set; }
        public bool IsRepair { get; set; }
        public bool IsAcquisition { get; set; }
        public bool IsPistol { get; set; }
        public bool IsRevolver { get; set; }
        public bool IsRifle { get; set; }
        public bool IsShotgun { get; set; }
        public bool IsReceiver { get; set; }

        public bool IsChgManuf { get; set; }
        public bool IsChgImpor { get; set; }
        public bool IsChgModel { get; set; }
        public bool IsChgSerial { get; set; }
        public bool IsChgType { get; set; }
        public bool IsChgCal { get; set; }
        public bool IsChgAcqDate { get; set; }
        public bool IsChgAcqName { get; set; }
        public bool IsChgAcqAddr { get; set; }
        public bool IsChgDspDate { get; set; }
        public bool IsChgDspName { get; set; }
        public bool IsChgDspAddr { get; set; }

        public int Id { get; set; }
        public int InStockId { get; set; }
        public int ItemBasisID { get; set; }
        public int BookId { get; set; }
        public int SubCatId { get; set; }
        public int LocationId { get; set; }
        public int CorrectionId { get; set; }
        public int SortId { get; set; }
        public int AcqDateId { get; set; }
        public int DspDateId { get; set; }
        public int AcqFflCode { get; set; }
        public int DspFflCode { get; set; }
        public int AcqFflId { get; set; }
        public int DispFflId { get; set; }
        public int TransTypeId { get; set; }
        public int AcqTypeId { get; set; }
        public int AcqStateId { get; set; }
        public int AcqFflSrc { get; set; }
        public int DspFflSrc { get; set; }
        public int DspTypeId { get; set; }
        public int ManufId { get; set; }
        public int ImporterId { get; set; }
        public int GunTypeId { get; set; }
        public int CaliberId { get; set; }
        public int CapacityId { get; set; }
        public int DspStateId { get; set; }
        public int AcqSourceId { get; set; }
        public int DspSourceId { get; set; }
        public int AcqFflSrcId { get; set; }
        public int DspFflSrcId { get; set; }
        public int AcqFflStateId { get; set; }
        public int DspFflStateId { get; set; }
        public int DspFulfillTypeId { get; set; }

        public int PagingMaxRows { get; set; }
        public int PagingStartRow { get; set; }
        public int TotalRowCount { get; set; }
        public int SupplierID { get; set; }
        public string AcqName { get; set; }
        public string AcqOrgName { get; set; }
        public string AcqFflName { get; set; }
        public string AcqFflNumber { get; set; }
        public string AcqCaAvNumber { get; set; }
        public string AcqFirstName { get; set; }
        public string AcqLastName { get; set; }
        public string AcqFullName { get; set; }
        public string AcqAddress { get; set; }
        public string AcqAddrOrFfl { get; set; }
        public string AcqCustType { get; set; }
        public string AcqCity { get; set; }
        public string AcqState { get; set; }
        public string AcqZipCode { get; set; }
        public string AcqZipExt { get; set; }
        public string CustomerName { get; set; }
        public string CflcInbound { get; set; }
        public string CflcOutbound { get; set; }
        public string AcqCurioName { get; set; }
        public string AcqCurioFfl { get; set; }
        public string AcqPhone { get; set; }
        public string AcqEmail { get; set; }
        public string DspCurioName { get; set; }
        public string DspCurioFfl { get; set; }
        public string CurioName { get; set; }
        public string CurioFflNumber { get; set; }
        public string DispName { get; set; }
        public string DispOrgName { get; set; }
        public string DispFflName { get; set; }
        public string DispFflNumber { get; set; }
        public string DispCaAvNumber { get; set; }
        public string DispFirstName { get; set; }
        public string DispMiddleName { get; set; }
        public string DispLastName { get; set; }
        public string DispFullName { get; set; }
        public string DispAddress { get; set; }
        public string DispAddrOrFfl { get; set; }
        public string DispCustType { get; set; }
        public string DispCity { get; set; }
        public string DispState { get; set; }
        public string DispZipCode { get; set; }
        public string DispZipExt { get; set; }
        public string GunDesc { get; set; }
        public string GunMfg { get; set; }
        public string GunImpt { get; set; }
        public string GunModelName { get; set; }
        public string GunSerial { get; set; }
        public string GunType { get; set; }
        public string GunCaliber { get; set; }
        public string TagSku { get; set; }
        public string TransId { get; set; }
        public string SearchTxt { get; set; }
        public string StrDateAcq { get; set; }
        public string StrDateDsp { get; set; }
        public string StrDateAcqCurExp { get; set; }
        public string StrDateDspCurExp { get; set; }
        public string StrDateMod { get; set; }
        public string BookCode { get; set;  }
        public DateTime AcqDateMin { get; set; }
        public DateTime AcqDateMax { get; set; }
        public DateTime DspDateMin { get; set; }
        public DateTime DspDateMax { get; set; }
        public DateTime DateMax { get; set; }
        public DateTime DateMin { get; set; }
        public DateTime AcqDate { get; set; }
        public DateTime DispDate { get; set; }
        public DateTime ModDate { get; set; }
        public DateTime CurioExpDate { get; set; }
        public DateTime AcqCurExp { get; set; }
        public DateTime DspCurExp { get; set; }
        public string StrCurioDate { get; set; }

        public string StrAcqCurExp { get; set; }
        public string StrDspCurExp { get; set; }

        public BoundBookModel()
        {

        }

        // read for edit
        public BoundBookModel(int id, int afcd, int afst, int afsr, int afso, int asid, int dfcd, int dfst, int dfsr, int dfso, int dsid, int dftp, 
                              int mfid, int mpid, int clid, int gtid, string trid, string gser, string aonm, string afnm, string alnm, string aadr, string acty, 
                              string azip, string aext, string acfl, string acnm, string donm, string dfnm, string dmnm, string dlnm, string dadr, string dcty, 
                              string dzip, string dext, string dcfl, string dcnm, string cfli, string cflo, string staq, string stdp, string sace, string sdce,
                              bool idsp, bool hicp)
        {
            Id = id;
            AcqFflCode = afcd;
            AcqFflStateId = afst;
            AcqFflSrcId = afsr;
            AcqSourceId = afso;
            AcqStateId = asid;
            DspFflCode = dfcd;
            DspFflStateId = dfst;
            DspFflSrcId = dfsr;
            DspSourceId = dfso;
            DspStateId = dsid;
            DspFulfillTypeId = dftp;
            ManufId = mfid;
            ImporterId = mpid;
            CaliberId = clid;
            GunTypeId = gtid;
            TransId = trid;
            GunSerial = gser;
            AcqOrgName = aonm;
            AcqFirstName = afnm;
            AcqLastName = alnm;
            AcqAddress = aadr;
            AcqCity = acty;
            AcqZipCode = azip;
            AcqZipExt = aext;
            AcqCurioFfl = acfl;
            AcqCurioName = acnm;
            DispOrgName = donm;
            DispFirstName = dfnm;
            DispMiddleName = dmnm;
            DispLastName = dlnm;
            DispAddress = dadr;
            DispCity = dcty;
            DispZipCode = dzip;
            DispZipExt = dext;
            DspCurioFfl = dcfl;
            DspCurioName = dcnm;
            CflcInbound = cfli;
            CflcOutbound = cflo;
            StrDateAcq = staq;
            StrDateDsp = stdp;
            StrDateAcqCurExp = sace;
            StrDateDspCurExp = sdce;
            IsDisposed = idsp;
            IsHiCaps = hicp;
        }



        //Read Gun Book
        public BoundBookModel(int id, int trc, bool dsp, bool cor, bool org, string adt, string ddt, string mdt, string tid,
                              string mfg, string imp, string mod, string cal, string ser, string typ, string anm, string aaf, 
                              string dnm, string daf, string bkc)
        {
            Id = id;
            TotalRowCount = trc;
            IsDisposed = dsp;
            IsCorrected = cor;
            IsOriginal = org;
            StrDateAcq = adt;
            StrDateDsp = ddt;
            StrDateMod = mdt;
            TransId = tid;
            GunMfg = mfg;
            GunImpt = imp;
            GunModelName = mod;
            GunCaliber = cal;
            GunSerial = ser;
            GunType = typ;
            AcqName = anm;
            AcqAddrOrFfl = aaf;
            DispFullName = dnm;
            DispAddrOrFfl = daf;
            BookCode = bkc;
        }


        public BoundBookModel(string transId, int tTypeId)
        {
            TagSku = transId;
            TransTypeId = tTypeId;
        }

        public BoundBookModel(DateTime acqDate, int fflCd, int acqtp, string email, bool isl)
        {
            AcqDate = acqDate;
            AcqFflCode = fflCd;
            AcqTypeId = acqtp;
            AcqEmail = email;
            IsSale = isl;
        }


        public BoundBookModel(string transId, string email, int tTypeId, int locId, int fflCd, int acqTp, DateTime acqDt, bool isSal)
        {
            TagSku = transId;
            AcqEmail = email;
            TransTypeId = tTypeId;
            LocationId = locId;
            AcqFflCode = fflCd;
            AcqTypeId = acqTp;
            AcqDate = acqDt;
            IsSale = isSal;
        }

        public BoundBookModel(DateTime acqDate, int tTypeId, int loc, int acq, int fcd, string eml, bool isSale)
        {
            AcqDate = acqDate;
            TransTypeId = tTypeId;
            LocationId = loc;
            AcqTypeId = acq;
            AcqFflCode = fcd;
            AcqEmail = eml;
            IsSale = isSale;
        }

        public BoundBookModel(int loc, int bid, int ttpid, string tid, bool isl, DateTime acqDate)
        {
            LocationId = loc;
            BookId = bid;
            TransTypeId = ttpid;
            TagSku = tid;
            IsSale = isl;
            AcqDate = acqDate;
        }

        public BoundBookModel(int loc, int ttpid, bool isl, DateTime acqDate)
        {
            LocationId = loc;
            TransTypeId = ttpid;
            IsSale = isl;
            AcqDate = acqDate;
        }

        // Restock Gun
        public BoundBookModel(int locId, int acqTyp, int fflSrc, int fflCode, string serial, string email, DateTime acqDt)
        {
            LocationId = locId;
            AcqTypeId = acqTyp;
            AcqFflSrc = fflSrc;
            AcqFflCode = fflCode;
            GunSerial = serial;
            //AcqName = acqNam;
            //AcqAddrOrFfl = acqAddr;
            //AcqOrgName = org;
            //AcqFirstName = fName;
            //AcqLastName = lName;
            //AcqAddress = addr;
            //AcqCity = city;
            //AcqZipCode = zip;
            //AcqZipExt = zipExt;
            //CurioName = curName;
            //CurioFflNumber = curFfl;
            AcqEmail = email;
            AcqDate = acqDt;
        }


        public BoundBookModel(int tTypeId, string acqName, string acqType, string acqFflNum, string acqCaAv,
            string acqAddress, string acqDate)
        {
            TransTypeId = tTypeId;
            AcqFullName = acqName;
            AcqCustType = acqType;
            AcqFflNumber = acqFflNum;
            AcqCaAvNumber = acqCaAv;
            AcqAddress = acqAddress;
            StrDateAcq = acqDate;
        }

        public BoundBookModel(int tTypeId, int locId, string acqName, string acqType, string acqFflNum, string acqCaAv,
            string acqAddress, string phone, string email, DateTime acqDate)
        {
            TransTypeId = tTypeId;
            LocationId = locId;
            AcqFullName = acqName;
            AcqCustType = acqType;
            AcqFflNumber = acqFflNum;
            AcqCaAvNumber = acqCaAv;
            AcqAddress = acqAddress;
            AcqPhone = phone;
            AcqEmail = email;
            AcqDate = acqDate;
        }

        public BoundBookModel(int tTypeId, int locId, int fflCode, int acqTypeId, string acqName, string acqType, string acqFflNum, string acqCaAv,
           string acqAddress, string phone, string email, DateTime acqDate)
        {
            TransTypeId = tTypeId;
            LocationId = locId;
            AcqFflCode = fflCode;
            AcqTypeId = acqTypeId;
            AcqEmail = email;
            AcqDate = acqDate;
        }



        public BoundBookModel(DateTime acqDate, int tTypeId, int locId, int fflCd, int acqTyp, string email)
        {
            AcqDate = acqDate;
            TransTypeId = tTypeId;
            LocationId = locId;
            AcqFflCode = fflCd;
            AcqTypeId = acqTyp;
            AcqEmail = email;
        }



        public BoundBookModel(int tTypeId, string transId, string mfg, string cal, string acqName, string acqType,
            string acqFflNum, string acqCaAv, string acqAddress, DateTime acqDate)
        {
            TransTypeId = tTypeId;
            TagSku = transId;
            GunMfg = mfg;
            GunCaliber = cal;
            AcqFullName = acqName;
            AcqCustType = acqType;
            AcqFflNumber = acqFflNum;
            AcqCaAvNumber = acqCaAv;
            AcqAddress = acqAddress;
            AcqDate = acqDate;
        }

        // AMMO GET
        public BoundBookModel(int tTypeId, int locId, string transId, string acqName, string acqType, string acqFflNum,
            string acqCaAv, string acqAddress,
            string dispName, string dispType, string dispFflNum, string dispCaAv, string dispAddress, string acqDate,
            string dispDate, string phone, string email, bool isDisp, bool isSale)
        {
            TransTypeId = tTypeId;
            LocationId = locId;
            TagSku = transId;
            AcqFullName = acqName;
            AcqCustType = acqType;
            AcqFflNumber = acqFflNum;
            AcqCaAvNumber = acqCaAv;
            AcqAddress = acqAddress;
            DispFullName = dispName;
            DispCustType = dispType;
            DispFflNumber = dispFflNum;
            DispCaAvNumber = dispCaAv;
            DispAddress = dispAddress;
            StrDateAcq = acqDate;
            StrDateDsp = dispDate;
            AcqPhone = phone;
            AcqEmail = email;
            IsDisposed = isDisp;
            IsSale = isSale;
        }


        // NEW AMMO GET
        public BoundBookModel(bool isSal, int supId, int acqType, int tTypeId, int fflCode, int stateId, string customer, string acqName, string email, string acqDate)
        {
            IsSale = isSal;
            SupplierID = supId;
            AcqTypeId = acqType;
            TransTypeId = tTypeId;
            AcqFflCode = fflCode;
            AcqStateId = stateId;
            CustomerName = customer;
            AcqName = acqName;
            AcqEmail = email;
            StrDateAcq = acqDate;
        }


        // AMMO GET
        public BoundBookModel(int tTypeId, string transId, string acqName, string acqType, string acqFflNum,
            string acqCaAv, string acqAddress,
            string dispName, string dispType, string dispFflNum, string dispCaAv, string dispAddress, string acqDate,
            string dispDate, bool isDisp)
        {
            TransTypeId = tTypeId;
            TagSku = transId;
            AcqFullName = acqName;
            AcqCustType = acqType;
            AcqFflNumber = acqFflNum;
            AcqCaAvNumber = acqCaAv;
            AcqAddress = acqAddress;
            DispFullName = dispName;
            DispCustType = dispType;
            DispFflNumber = dispFflNum;
            DispCaAvNumber = dispCaAv;
            DispAddress = dispAddress;
            StrDateAcq = acqDate;
            StrDateDsp = dispDate;
            IsDisposed = isDisp;
        }

        // AMMO ADD
        public BoundBookModel(int lid, int tTypeId, string transId, string mfg, string cal, string acqName, string acqType,
            string acqFflNum, string acqCaAv, string acqAddress,
            string dispName, string dispType, string dispFflNum, string dispCaAv, string dispAddress, string phone, string email, bool isDisp,
            bool isSale, DateTime acqDate, DateTime dispDate)
        {
            LocationId = lid;
            TransTypeId = tTypeId;
            TagSku = transId;
            GunMfg = mfg;
            GunCaliber = cal;
            AcqFullName = acqName;
            AcqCustType = acqType;
            AcqFflNumber = acqFflNum;
            AcqCaAvNumber = acqCaAv;
            AcqAddress = acqAddress;
            DispFullName = dispName;
            DispCustType = dispType;
            DispFflNumber = dispFflNum;
            DispCaAvNumber = dispCaAv;
            DispAddress = dispAddress;
            AcqPhone = phone;
            AcqEmail = email;
            IsDisposed = isDisp;
            IsSale = isSale;
            AcqDate = acqDate;
            DispDate = dispDate;


        }


        public BoundBookModel(int tTypeId, int locId, string acqName, string acqType, string acqFflNum, string acqCaAv,
            string acqAddress, string phone, string email,
            string dispName, string dispType, string dispFflNum, string dispCaAv, string dispAddress,
            bool isDsp, DateTime acqDate, DateTime dispDate)
        {
            TransTypeId = tTypeId;
            LocationId = locId;
            AcqFullName = acqName;
            AcqCustType = acqType;
            AcqFflNumber = acqFflNum;
            AcqCaAvNumber = acqCaAv;
            AcqAddress = acqAddress;
            AcqPhone = phone;
            AcqEmail = email;
            AcqDate = acqDate;
            DispFullName = dispName;
            DispCustType = dispType;
            DispFflNumber = dispFflNum;
            DispCaAvNumber = dispCaAv;
            IsDisposed = isDsp;
            DispAddress = dispAddress;
            DispDate = dispDate;
        }



        public BoundBookModel(int locId, int dspId, int corrId, int sortId, int acqDateId, int dspDateId, int pgs, int str, bool isSale, bool isTrns,
            bool isCons, bool isShip, bool isStor, bool isRepr, bool isAcq,
            bool isPst, bool isRev, bool isRif, bool isSht, bool isRec, DateTime aDateFr, DateTime aDateTo, DateTime dDateFr, DateTime dDateTo, string srch)
        {
            LocationId = locId;
            DspTypeId = dspId;
            CorrectionId = corrId;
            SortId = sortId;
            AcqDateId = acqDateId;
            DspDateId = dspDateId;
            PagingMaxRows = pgs;
            PagingStartRow = str;
            IsSale = isSale;
            IsTransfer = isTrns;
            IsConsign = isCons;
            IsShipping = isShip;
            IsStorage = isStor;
            IsRepair = isRepr;
            IsAcquisition = isAcq;
            IsPistol = isPst;
            IsRevolver = isRev;
            IsRifle = isRif;
            IsShotgun = isSht;
            IsReceiver = isRec;
            AcqDateMin = aDateFr;
            AcqDateMax = aDateTo;
            DspDateMin = dDateFr;
            DspDateMax = dDateTo;
            SearchTxt = srch;
        }

        public BoundBookModel(int locId, int ibi, int subCat, int transTypeId, int acqTypeId, int fflCode, int manufId, int imptId,
            int gunTypeId, int calId, int fflSrc, int aStateId, string tagSku, string acqName, string addrFfl,
            DateTime acqDate, DateTime curExp, string gunMfg, string gunImpt, string gunMdl, string gunSerial,
            string gunType, string gunCaliber,
            string cflcIn, string aOrgName, string aFname, string aLname, string aAddr, string aCity, string aZip,
            string aZipExt, string curName, string curNum, string phone, string email)
        {
            LocationId = locId;
            ItemBasisID = ibi;
            SubCatId = subCat;
            TransTypeId = transTypeId;
            AcqTypeId = acqTypeId;
            AcqFflCode = fflCode;
            ManufId = manufId;
            ImporterId = imptId;
            GunTypeId = gunTypeId;
            CaliberId = calId;
            AcqFflSrc = fflSrc;
            AcqStateId = aStateId;
            TagSku = tagSku;
            AcqName = acqName;
            AcqAddrOrFfl = addrFfl;
            AcqDate = acqDate;
            CurioExpDate = curExp;
            //TransId = transId;
            GunMfg = gunMfg;
            GunImpt = gunImpt;
            GunModelName = gunMdl;
            GunSerial = gunSerial;
            GunType = gunType;
            GunCaliber = gunCaliber;
            CflcInbound = cflcIn;
            AcqOrgName = aOrgName;
            AcqFirstName = aFname;
            AcqLastName = aLname;
            AcqAddress = aAddr;
            AcqCity = aCity;
            AcqZipCode = aZip;
            AcqZipExt = aZipExt;
            CurioName = curName;
            CurioFflNumber = curNum;
            AcqPhone = phone;
            AcqEmail = email;
        }


        public BoundBookModel(int isi, int locId, int ibi, int subCat, int transTypeId, int acqTypeId, int manufId, int imptId,
            int gunTypeId, int calId, int fflSrc, int fflCode, string tagSku, string acqName, string addrFfl,
            string gunMfg, string gunImpt, string gunMdl, string gunSerial,
            string gunType, string gunCaliber,
            string cflcIn, string email, DateTime acqDate, bool iSal)
        {
            InStockId = isi;
            LocationId = locId;
            ItemBasisID = ibi;
            SubCatId = subCat;
            TransTypeId = transTypeId;
            AcqTypeId = acqTypeId;
            ManufId = manufId;
            ImporterId = imptId;
            GunTypeId = gunTypeId;
            CaliberId = calId;
            AcqFflSrc = fflSrc;
            AcqFflCode = fflCode;
            TagSku = tagSku;
            AcqName = acqName;
            AcqAddrOrFfl = addrFfl;
            GunMfg = gunMfg;
            GunImpt = gunImpt;
            GunModelName = gunMdl;
            GunSerial = gunSerial;
            GunType = gunType;
            GunCaliber = gunCaliber;
            CflcInbound = cflcIn;
            AcqEmail = email;
            AcqDate = acqDate;
            IsSale = iSal;
        }


        public BoundBookModel(int locId, int transTypeId, int acqTypeId, int fflId, string fflName, string fflNum,
            DateTime acqDate, DateTime curExp, string orgName, string fName, string lName,
            string addr, string city, string state, string zip, string zipExt, string gunMfg, string gunImpt,
            string gunMdl,
            string gunSerial, string gunType, int gunTypeId, string gunCaliber, bool printTag)
        {
            LocationId = locId;
            TransTypeId = transTypeId;
            AcqTypeId = acqTypeId;
            AcqFflId = fflId;
            AcqFflName = fflName;
            AcqFflNumber = fflNum;
            AcqDate = acqDate;
            CurioExpDate = curExp;
            AcqOrgName = orgName;
            AcqFirstName = fName;
            AcqLastName = lName;
            AcqAddress = addr;
            AcqCity = city;
            AcqState = state;
            AcqZipCode = zip;
            AcqZipExt = zipExt;
            GunMfg = gunMfg;
            GunImpt = gunImpt;
            GunModelName = gunMdl;
            GunSerial = gunSerial;
            GunType = gunType;
            GunTypeId = gunTypeId;
            GunCaliber = gunCaliber;
            PrintTag = printTag;
        }


        public BoundBookModel(int id, int acqTypeId, int dspTypeId, int acqFflId, int dspFflId, int acqFflStId,
            int dspFflStId, string transId, string manuf, string importer, string model,
            string serial, string gunType, string caliber, string acqName, string acqAddrFfl, string dspName,
            string dspAddrFfl, DateTime acqDate, DateTime dispDate,
            DateTime curioExpDate, bool isDisp, string strAcqDt, string strDspDt, string strCurDt)
        {
            Id = id;
            AcqTypeId = acqTypeId;
            DspTypeId = dspTypeId;
            AcqFflId = acqFflId;
            DispFflId = dspFflId;
            AcqStateId = acqFflStId;
            DspFflStateId = dspFflStId;
            TagSku = transId;
            GunMfg = manuf;
            GunImpt = importer;
            GunModelName = model;
            GunSerial = serial;
            GunType = gunType;
            GunCaliber = caliber;
            AcqFullName = acqName;
            AcqAddrOrFfl = acqAddrFfl;
            DispFullName = dspName;
            DispAddrOrFfl = dspAddrFfl;
            AcqDate = acqDate;
            DispDate = dispDate;
            CurioExpDate = curioExpDate;
            IsDisposed = isDisp;
            StrDateAcq = strAcqDt;
            StrDateDsp = strDspDt;
            StrCurioDate = strCurDt;
        }


        public BoundBookModel(int acqTypId, int acqFflId, int dspTypeId, int dspFflId, int gunTypId, DateTime acqDate,
            DateTime dispDate,
            DateTime acqCurDate, DateTime dspCurDate, bool isDisp, string manuf, string importer, string model,
            string serial,
            string gunType, string caliber, string acqName, string acqAddrFfl, string dspName, string dspAddrFfl,
            string transId)
        {
            AcqTypeId = acqTypId;
            AcqFflId = acqFflId;
            DspTypeId = dspTypeId;
            DispFflId = dspFflId;
            GunTypeId = gunTypId;
            AcqDate = acqDate;
            DispDate = dispDate;
            CurioExpDate = acqCurDate;
            DspCurExp = dspCurDate;
            IsDisposed = isDisp;
            GunMfg = manuf;
            GunImpt = importer;
            GunModelName = model;
            GunSerial = serial;
            GunType = gunType;
            GunCaliber = caliber;
            AcqFullName = acqName;
            AcqAddrOrFfl = acqAddrFfl;
            DispFullName = dspName;
            DispAddrOrFfl = dspAddrFfl;
            TagSku = transId;
        }

        /* EDIT RECORD */
        public BoundBookModel(int mfg, int imp, int gtp, int cal, int aSrc, int dSrc, int aFsc, int dFsc, int aFid,
            int dFid, int aSta, int dSta, int aFst, int dFst, string tid, string mdl, string ser, string aCnm,
            string dCnm,
            string aCfl, string dCfl, string aOrg, string aFnm, string aLnm, string aAdr, string aCty, string aZip,
            string aZxt, string dOrg, string dFnm, string dLnm, string dAdr,
            string dCty, string dZip, string dZxt, string aDat, string dDat, string aCxp, string dCxp, string aNam, string aAdf, string dNam, string dAdf, string cIn, string cOut, bool disp, bool hiCap)
        {
            ManufId = mfg;
            ImporterId = imp;
            GunTypeId = gtp;
            CaliberId = cal;
            AcqTypeId = aSrc;
            DspTypeId = dSrc;
            AcqFflSrc = aFsc;
            DspFflSrc = dFsc;
            AcqFflId = aFid;
            DispFflId = dFid;
            AcqStateId = aSta;
            DspStateId = dSta;
            AcqFflStateId = aFst;
            DspFflStateId = dFst;
            TagSku = tid;
            GunModelName = mdl;
            GunSerial = ser;
            AcqFflName = aCnm;
            DispFflName = dCnm;
            AcqFflNumber = aCfl;
            DispFflNumber = dCfl;
            AcqOrgName = aOrg;
            AcqFirstName = aFnm;
            AcqLastName = aLnm;
            AcqAddress = aAdr;
            AcqCity = aCty;
            AcqZipCode = aZip;
            AcqZipExt = aZxt;
            DispOrgName = dOrg;
            DispFirstName = dFnm;
            DispLastName = dLnm;
            DispAddress = dAdr;
            DispCity = dCty;
            DispZipCode = dZip;
            DispZipExt = dZxt;
            StrDateAcq = aDat;
            StrDateDsp = dDat;
            StrAcqCurExp = aCxp;
            StrDspCurExp = dCxp;
            AcqName = aNam;
            AcqAddrOrFfl = aAdf;
            DispName = dNam;
            DispAddrOrFfl = dAdf;
            CflcInbound = cIn;
            CflcOutbound = cOut;
            IsDisposed = disp;
            IsHiCaps = hiCap;

        }

        /* EDIT BIG RECORD */
        public BoundBookModel(bool isDsp, DateTime aqDat, DateTime dpDat, DateTime aqCxp, DateTime dpCxp, int locId,
            int itmId, int mfgId, int impId, int calId, int gtpId, int atpId,
            int dtpId, int aflSc, int aflId, int dflSc, int dflId, int aqSid, int dpSid, int aqFsi, int aqDsi, string model, string seria,
            string manuf, string imprt, string gunTp,
            string calib, string aqNam, string aqAdf, string aqOrg, string aqFnm, string aqLnm, string aqAdr,
            string aqCty, string aqZip, string aqExt, string aqCnm,
            string aqCfl, string dpNam, string dpAdf, string dpOrg, string dpFnm, string dpLnm, string dpAdr,
            string dpCty, string dpZip, string dpExt, string dpCnm, string dpCfl, string aCflc, string dCflc)
        {
            IsDisposed = isDsp;
            AcqDate = aqDat;
            DispDate = dpDat;
            AcqCurExp = aqCxp;
            DspCurExp = dpCxp;
            LocationId = locId;
            Id = itmId;
            ManufId = mfgId;
            ImporterId = impId;
            CaliberId = calId;
            GunTypeId = gtpId;
            AcqTypeId = atpId;
            DspTypeId = dtpId;
            AcqFflSrc = aflSc;
            AcqFflId = aflId;
            DspFflSrc = dflSc;
            DispFflId = dflId;
            AcqStateId = aqSid;
            DspStateId = dpSid;
            AcqFflStateId = aqFsi;
            DspFflStateId = aqDsi;
            GunModelName = model;
            GunSerial = seria;
            GunMfg = manuf;
            GunImpt = imprt;
            GunType = gunTp;
            GunCaliber = calib;
            AcqName = aqNam;
            AcqAddrOrFfl = aqAdf;
            AcqOrgName = aqOrg;
            AcqFirstName = aqFnm;
            AcqLastName = aqLnm;
            AcqAddress = aqAdr;
            AcqCity = aqCty;
            AcqZipCode = aqZip;
            AcqZipExt = aqExt;
            AcqCurioName = aqCnm;
            AcqCurioFfl = aqCfl;
            DispName = dpNam;
            DispAddrOrFfl = dpAdf;
            DispOrgName = dpOrg;
            DispFirstName = dpFnm;
            DispLastName = dpLnm;
            DispAddress = dpAdr;
            DispCity = dpCty;
            DispZipCode = dpZip;
            DispZipExt = dpExt;
            DspCurioName = dpCnm;
            DspCurioFfl = dpCfl;
            CflcInbound = aCflc;
            CflcOutbound = dCflc;

        }


        ///* EDIT BIG RECORD */
        public BoundBookModel(bool isDsp, bool isMag, int locId, int itmId, int dtpId, int dflSc, int dflId, int dpSid, DateTime dpDat, DateTime dpCxp,
                              string dpNam, string dpAdf, string dpOrg, string dpFnm, string dpLnm, string dpAdr, string dpCty, 
                              string dpZip, string dpExt, string dpCnm, string dpCfl, string cflc)
        {
            IsDisposed = isDsp;
            IsHiCaps = isMag;
            LocationId = locId;
            Id = itmId;
            DspTypeId = dtpId;
            DspFflSrc = dflSc;
            DispFflId = dflId;
            DspStateId = dpSid;
            DispDate = dpDat;
            DspCurExp = dpCxp;
            DispName = dpNam;
            DispAddrOrFfl = dpAdf;
            DispOrgName = dpOrg;
            DispFirstName = dpFnm;
            DispLastName = dpLnm;
            DispAddress = dpAdr;
            DispCity = dpCty;
            DispZipCode = dpZip;
            DispZipExt = dpExt;
            DspCurioName = dpCnm;
            DspCurioFfl = dpCfl;
            CflcOutbound = cflc;
        }


        /// CFLC READ ALL
        public BoundBookModel(int loc, int rwc, string tid, string gun, string anm, string aaf, string cfi, string dnm, string daf, string cfo, string dmn, string dmx)
        {
            LocationId = loc;
            TotalRowCount = rwc;
            TagSku = tid;
            GunDesc = gun;
            AcqName = anm;
            AcqAddrOrFfl = aaf;
            CflcInbound = cfi;
            DispName = dnm;
            DispAddrOrFfl = daf;
            CflcOutbound = cfo;
            StrDateAcq = dmn;
            StrDateDsp = dmx;
        }


        /// HI-CAP MAG BY ID
        public BoundBookModel(int id, int cap, int mfg, int cal, int gtp, string mod, string aqn, string aaf, string dnm, string daf, string adt, string ddt, bool idp)
        {
            Id = id;
            CapacityId = cap;
            ManufId = mfg;
            CaliberId = cal;
            GunTypeId = gtp;
            GunModelName = mod;
            AcqName = aqn;
            AcqAddrOrFfl = aaf;
            DispName = dnm;
            DispAddrOrFfl = daf;
            StrDateAcq = adt;
            StrDateDsp = ddt;
            IsDisposed = idp;
        }


        /// HI-CAP MAGS GRID OUTPUT
        public BoundBookModel(int id, int cap, int rwc, string tid, string gun, string anm, string aaf, string dnm, string daf, string dmn, string dmx)
        {
            Id = id;
            CapacityId = cap;
            TotalRowCount = rwc;
            TagSku = tid;
            GunDesc = gun;
            AcqName = anm;
            AcqAddrOrFfl = aaf;
            DispName = dnm;
            DispAddrOrFfl = daf;
            StrDateAcq = dmn;
            StrDateDsp = dmx;
        }


        /// CFLC SEARCH PARAMS
        public BoundBookModel(int loc, int mfg, int gtp, int cal, int btp, int pgs, int str, DateTime dfr, DateTime dto, string sch)
        {
            LocationId = loc;
            ManufId = mfg;
            GunTypeId = gtp;
            CaliberId = cal;
            BookId = btp;
            PagingMaxRows = pgs;
            PagingStartRow = str;
            DateMin = dfr;
            DateMax = dto;
            SearchTxt = sch;
        }

        /// HI-CAP SEARCH PARAMS
        public BoundBookModel(int mfg, int gtp, int cal, int cap, int pgs, int str, DateTime dfr, DateTime dto)
        {
            ManufId = mfg;
            GunTypeId = gtp;
            CaliberId = cal;
            CapacityId = cap;
            PagingMaxRows = pgs;
            PagingStartRow = str;
            DateMin = dfr;
            DateMax = dto;
        }


        /// HI-CAP EDIT
        public BoundBookModel(int id, int mfg, int gtp, int cal, int cap, DateTime adt, DateTime ddt, string mod, string anm, string aaf, string dnm, string daf, bool idp)
        {
            Id = id;
            ManufId = mfg;
            GunTypeId = gtp;
            CaliberId = cal;
            CapacityId = cap;
            AcqDate = adt;
            DispDate = ddt;
            GunModelName = mod;
            AcqName = anm;
            AcqAddrOrFfl = aaf;
            DispName = dnm;
            DispAddrOrFfl = daf;
            IsDisposed = idp;
        }


        /// HI-CAP ADD
        public BoundBookModel(int mfg, int gtp, int cal, int cap, DateTime adt, DateTime ddt, string mod, string anm, string aaf, string dnm, string daf, bool idp)
        {
            ManufId = mfg;
            GunTypeId = gtp;
            CaliberId = cal;
            CapacityId = cap;
            AcqDate = adt;
            DispDate = ddt;
            GunModelName = mod;
            AcqName = anm;
            AcqAddrOrFfl = aaf;
            DispName = dnm;
            DispAddrOrFfl = daf;
            IsDisposed = idp;
        }


        /// CUSTOM GUN ADD PARAMS
        public BoundBookModel(int asc, int fsc, int sid, int fid, int fsi, string org, string fnm, string lnm, string adr, string cty, string zip, string ext, string cfl, string cfn, string nam, string adf, DateTime exp)
        {
            AcqTypeId = asc; //@AcqSourceID int,
            AcqFflSrc = fsc;
            AcqStateId = sid;
            AcqFflId = fid;
            AcqFflStateId = fsi;
            AcqOrgName = org;
            AcqFirstName = fnm;
            AcqLastName = lnm;
            AcqAddress = adr;
            AcqCity = cty;
            AcqZipCode = zip;
            AcqZipExt = ext;
            AcqCurioFfl = cfl;
            AcqCurioName = cfn;
            AcqName = nam;
            AcqAddrOrFfl = adf;
            CurioExpDate = exp;
        }


        /// CUSTOM GUN ADD PARAMS
        public BoundBookModel(string nam, string adf, DateTime exp)
        {
            AcqName = nam;
            AcqAddrOrFfl = adf;
            CurioExpDate = exp;
        }


        /// CUSTOM GUN ADD PARAMS
        public BoundBookModel(string eml, int sup, int fcd, int atp, DateTime acqDt)
        {
            AcqEmail = eml;
            SupplierID = sup;
            AcqFflCode = fcd;
            AcqTypeId = atp;
            AcqDate = acqDt;
        }


        public BoundBookModel(int locId, int ttpId, int fflCd, int acqTp, string email, bool sale, DateTime acqDt) 
        {
            LocationId = locId;
            TransTypeId = ttpId;
            AcqFflCode = fflCd;
            AcqTypeId = acqTp;
            AcqEmail = email;
            IsSale = sale;
            AcqDate = acqDt;
        }


        // MERCHANDISE RESTOCK
        public BoundBookModel(int locId, int ttpId, int fflCd, int acqTp, int staId, string acqName, string email, string cust, string strDt, bool sale)
        {
            LocationId = locId;
            TransTypeId = ttpId;
            AcqFflCode = fflCd;
            AcqTypeId = acqTp;
            AcqFflStateId = staId;
            AcqName = acqName;
            AcqEmail = email;
            CustomerName = cust;
            StrDateAcq = strDt;
            IsSale = sale;
        }

 









    }


}