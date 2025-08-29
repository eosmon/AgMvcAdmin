using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgMvcAdmin.Common;
using AgMvcAdmin.Models;
using AgMvcAdmin.Models.Common;
using WebBase.Configuration;

namespace AgMvcAdmin.Controllers
{
    public class ComplianceController : BaseController
    {
        //
        // GET: /Compliance/
        public ActionResult GunBook()
        {
            var pg = new PageModel(Pages.BoundBook);
            return View(pg);
        }

        public ActionResult CaHiCap()
        {
            var pg = new PageModel(Pages.CaHiCap);
            return View(pg);
        }


        public ActionResult Cflc()
        {
            var pg = new PageModel(Pages.Cflc);
            return View(pg);
        }


        [HttpPost]
        public JsonResult FillCflc()
        {
            var x0 = 0;
            var d0 = DateTime.MinValue;

            var i1 = Int32.TryParse(Request["LocId"], out x0) ? Convert.ToInt32(Request["LocId"]) : -1;
            var i2 = Int32.TryParse(Request["MfgId"], out x0) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var i3 = Int32.TryParse(Request["GtpId"], out x0) ? Convert.ToInt32(Request["GtpId"]) : 0;
            var i4 = Int32.TryParse(Request["CalId"], out x0) ? Convert.ToInt32(Request["CalId"]) : 0;
            var i5 = Int32.TryParse(Request["BtpId"], out x0) ? Convert.ToInt32(Request["BtpId"]) : 0;
            var i6 = Int32.TryParse(Request["IPrPg"], out x0) ? Convert.ToInt32(Request["IPrPg"]) : 0;
            var i7 = Int32.TryParse(Request["StaRw"], out x0) ? Convert.ToInt32(Request["StaRw"]) : 0;

            var dt1 = DateTime.TryParse(Request["DateFr"], out d0) ? Convert.ToDateTime(Request["DateFr"]) : d0;
            var dt2 = DateTime.TryParse(Request["DateTo"], out d0) ? Convert.ToDateTime(Request["DateTo"]) : d0;

            var v1 = Request["Search"];

            var b = new BoundBookModel(i1, i2, i3, i4, i5, i6, i7, dt1, dt2, v1);

            var c = new BookContext();
            var data = c.GetCflcData(b);

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult GetHiCapById(string itemId)
        {
            var x0 = 0;

            var i1 = Int32.TryParse(itemId, out x0) ? Convert.ToInt32(itemId) : 0;

            var c = new BookContext();
            var data = c.GetMagById(i1);

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult DeleteMag(string itemId)
        {
            var x0 = 0;

            var i1 = Int32.TryParse(itemId, out x0) ? Convert.ToInt32(itemId) : 0;

            var c = new BookContext();
            c.DeleteHiCapMag(i1);

            return Json("Success", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EditHiCap()
        {
            var x0 = 0;
            var b0 = false;
            var d0 = DateTime.MinValue;

            var b1 = Boolean.TryParse(Request["IsDsp"], out b0) ? Convert.ToBoolean(Request["IsDsp"]) : b0;

            var i1 = Int32.TryParse(Request["ItmId"], out x0) ? Convert.ToInt32(Request["ItmId"]) : 0;
            var i2 = Int32.TryParse(Request["MfgId"], out x0) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var i3 = Int32.TryParse(Request["GtpId"], out x0) ? Convert.ToInt32(Request["GtpId"]) : 0;
            var i4 = Int32.TryParse(Request["CalId"], out x0) ? Convert.ToInt32(Request["CalId"]) : 0;
            var i5 = Int32.TryParse(Request["CapId"], out x0) ? Convert.ToInt32(Request["CapId"]) : 0;

            var dt1 = DateTime.TryParse(Request["AcqDt"], out d0) ? Convert.ToDateTime(Request["AcqDt"]) : d0;
            var dt2 = DateTime.TryParse(Request["DspDt"], out d0) ? Convert.ToDateTime(Request["DspDt"]) : d0;

            var v1 = Request["Model"];
            var v2 = Request["AcqNm"];
            var v3 = Request["AcqAf"];
            var v4 = Request["DspNm"];
            var v5 = Request["DspAf"];

            var b = new BoundBookModel(i1, i2, i3, i4, i5, dt1, dt2, v1, v2, v3, v4, v5, b1);

            var c = new BookContext();
            c.EditHiCapMag(b);

            return Json("Success", JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult AddHiCap()
        {
            var x0 = 0;
            var b0 = false;
            var d0 = DateTime.MinValue;

            var b1 = Boolean.TryParse(Request["IsDsp"], out b0) ? Convert.ToBoolean(Request["IsDsp"]) : b0;

            var i1 = Int32.TryParse(Request["MfgId"], out x0) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var i2 = Int32.TryParse(Request["GtpId"], out x0) ? Convert.ToInt32(Request["GtpId"]) : 0;
            var i3 = Int32.TryParse(Request["CalId"], out x0) ? Convert.ToInt32(Request["CalId"]) : 0;
            var i4 = Int32.TryParse(Request["CapId"], out x0) ? Convert.ToInt32(Request["CapId"]) : 0;

            var dt1 = DateTime.TryParse(Request["AcqDt"], out d0) ? Convert.ToDateTime(Request["AcqDt"]) : d0;
            var dt2 = DateTime.TryParse(Request["DspDt"], out d0) ? Convert.ToDateTime(Request["DspDt"]) : d0;

            var v1 = Request["Model"];
            var v2 = Request["AcqNm"];
            var v3 = Request["AcqAf"];
            var v4 = Request["DspNm"];
            var v5 = Request["DspAf"];

            var b = new BoundBookModel(i1, i2, i3, i4, dt1, dt2, v1, v2, v3, v4, v5, b1);

            var c = new BookContext();
            c.AddHiCapMag(b);

            return Json("Success", JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult FillHiCaps()
        {
            var x0 = 0;
            var d0 = DateTime.MinValue;

            var i1 = Int32.TryParse(Request["MfgId"], out x0) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var i2 = Int32.TryParse(Request["GtpId"], out x0) ? Convert.ToInt32(Request["GtpId"]) : 0;
            var i3 = Int32.TryParse(Request["CalId"], out x0) ? Convert.ToInt32(Request["CalId"]) : 0;
            var i4 = Int32.TryParse(Request["CapId"], out x0) ? Convert.ToInt32(Request["CapId"]) : 0;
            var i5 = Int32.TryParse(Request["IPrPg"], out x0) ? Convert.ToInt32(Request["IPrPg"]) : 0;
            var i6 = Int32.TryParse(Request["StaRw"], out x0) ? Convert.ToInt32(Request["StaRw"]) : 0;

            var dt1 = DateTime.TryParse(Request["DateFr"], out d0) ? Convert.ToDateTime(Request["DateFr"]) : d0;
            var dt2 = DateTime.TryParse(Request["DateTo"], out d0) ? Convert.ToDateTime(Request["DateTo"]) : d0;

            var b = new BoundBookModel(i1, i2, i3, i4, i5, i6, dt1, dt2);

            var c = new BookContext();
            var data = c.GetHiCapData(b);

            return Json(data, JsonRequestBehavior.AllowGet);

        }




        [HttpPost]
        public JsonResult FillGrid()
        {
            var x0 = 0;
            var b0 = false;
            var d0 = DateTime.MinValue;

            var i1 = Int32.TryParse(Request["LocId"], out x0) ? Convert.ToInt32(Request["LocId"]) : -1;
            var i2 = Int32.TryParse(Request["DspId"], out x0) ? Convert.ToInt32(Request["DspId"]) : -1;
            var i3 = Int32.TryParse(Request["CorId"], out x0) ? Convert.ToInt32(Request["CorId"]) : 0;
            var i4 = Int32.TryParse(Request["SrtId"], out x0) ? Convert.ToInt32(Request["SrtId"]) : 0;
            var i5 = Int32.TryParse(Request["AdtId"], out x0) ? Convert.ToInt32(Request["AdtId"]) : 0;
            var i6 = Int32.TryParse(Request["IPrPg"], out x0) ? Convert.ToInt32(Request["IPrPg"]) : 0;
            var i7 = Int32.TryParse(Request["StaRw"], out x0) ? Convert.ToInt32(Request["StaRw"]) : 0;
            var i8 = Int32.TryParse(Request["DdtId"], out x0) ? Convert.ToInt32(Request["DdtId"]) : 0;

            var b1 = Boolean.TryParse(Request["IsSal"], out b0) ? Convert.ToBoolean(Request["IsSal"]) : b0;
            var b2 = Boolean.TryParse(Request["IsTrn"], out b0) ? Convert.ToBoolean(Request["IsTrn"]) : b0;
            var b3 = Boolean.TryParse(Request["IsCon"], out b0) ? Convert.ToBoolean(Request["IsCon"]) : b0;
            var b4 = Boolean.TryParse(Request["IsShp"], out b0) ? Convert.ToBoolean(Request["IsShp"]) : b0;
            var b5 = Boolean.TryParse(Request["IsStr"], out b0) ? Convert.ToBoolean(Request["IsStr"]) : b0;
            var b6 = Boolean.TryParse(Request["IsRep"], out b0) ? Convert.ToBoolean(Request["IsRep"]) : b0;
            var b7 = Boolean.TryParse(Request["IsAqn"], out b0) ? Convert.ToBoolean(Request["IsAqn"]) : b0;
            
            var b8 = Boolean.TryParse(Request["IsPst"], out b0) ? Convert.ToBoolean(Request["IsPst"]) : b0;
            var b9 = Boolean.TryParse(Request["IsRev"], out b0) ? Convert.ToBoolean(Request["IsRev"]) : b0;
            var b10 = Boolean.TryParse(Request["IsRif"], out b0) ? Convert.ToBoolean(Request["IsRif"]) : b0;
            var b11 = Boolean.TryParse(Request["IsSht"], out b0) ? Convert.ToBoolean(Request["IsSht"]) : b0;
            var b12 = Boolean.TryParse(Request["IsRec"], out b0) ? Convert.ToBoolean(Request["IsRec"]) : b0;

            var dt1 = DateTime.TryParse(Request["AqDateFr"], out d0) ? Convert.ToDateTime(Request["AqDateFr"]) : d0;
            var dt2 = DateTime.TryParse(Request["AqDateTo"], out d0) ? Convert.ToDateTime(Request["AqDateTo"]) : d0;
            var dt3 = DateTime.TryParse(Request["DpDateFr"], out d0) ? Convert.ToDateTime(Request["DpDateFr"]) : d0;
            var dt4 = DateTime.TryParse(Request["DpDateTo"], out d0) ? Convert.ToDateTime(Request["DpDateTo"]) : d0;

            var v1 = Request["Search"];

            var b = new BoundBookModel(i1, i2, i3, i4, i5, i8, i6, i7, b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, dt1, dt2, dt3, dt4, v1);

            var c = new BookContext();
            var data = c.GetBoundBook(b);

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetBookItem(string locId, string itemId)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(locId, out x0) ? Convert.ToInt32(locId) : 0;
            var v2 = Int32.TryParse(itemId, out x0) ? Convert.ToInt32(itemId) : 0;

            var c = new BookContext();
            var data = c.GetBookItemById(v1, v2);

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost] 
        public JsonResult EditBook(string itemId)
        {
            var x0 = 0;
            var b0 = false;
            var d0 = DateTime.MinValue;

            var i1 = Int32.TryParse(Request["BkAcqTypId"], out x0) ? Convert.ToInt32(Request["BkAcqTypId"]) : 0;
            var i2 = Int32.TryParse(Request["BkAcqFflId"], out x0) ? Convert.ToInt32(Request["BkAcqFflId"]) : 0;
            var i3 = Int32.TryParse(Request["BkDspTypId"], out x0) ? Convert.ToInt32(Request["BkDspTypId"]) : 0;
            var i4 = Int32.TryParse(Request["BkDspFflId"], out x0) ? Convert.ToInt32(Request["BkDspFflId"]) : 0;
            var i5 = Int32.TryParse(Request["BkGunTypId"], out x0) ? Convert.ToInt32(Request["BkGunTypId"]) : 0;
            

            var d1 = DateTime.TryParse(Request["BkAcqDate"], out d0) ? Convert.ToDateTime(Request["BkAcqDate"]) : d0;
            var d2 = DateTime.TryParse(Request["BkDspDate"], out d0) ? Convert.ToDateTime(Request["BkDspDate"]) : d0;
            var d3 = DateTime.TryParse(Request["BkAcqCurExp"], out d0) ? Convert.ToDateTime(Request["BkAcqCurExp"]) : d0;
            var d4 = DateTime.TryParse(Request["BkDspCurExp"], out d0) ? Convert.ToDateTime(Request["BkDspCurExp"]) : d0;

            var b1 = Boolean.TryParse(Request["BkIsDisp"], out b0) ? Convert.ToBoolean(Request["BkIsDisp"]) : b0;

            var s1 = Request["BkMfg"];
            var s2 = Request["BkImp"];
            var s3 = Request["BkMdl"];
            var s4 = Request["BkSer"];
            var s5 = Request["BkTyp"];
            var s6 = Request["BkCal"];
            var s7 = Request["BkAcqName"];
            var s8 = Request["BkAcqAddr"];
            var s9 = Request["BkDspName"];
            var s10 = Request["BkDspAddr"];
            var s11 = Request["BkTrId"];

            var b = new BoundBookModel(i1, i2, i3, i4, i5, d1, d2, d3, d4, b1, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11);

            var c = new BookContext();
            c.UpdateBookItem(b);

            return Json("YEP", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateBookEntry(string itemId)
        {
            var x0 = 0;
            var b0 = false;
            var d0 = DateTime.MinValue;
            var bb = new BaseModel();

            var aqNam = string.Empty;
            var aqAdf = string.Empty;
            var dpNam = string.Empty;
            var dpAdf = string.Empty;

            var b1 = Boolean.TryParse(Request["IsDsp"], out b0) ? Convert.ToBoolean(Request["IsDsp"]) : b0; //IsDisposed

            var i1 = Int32.TryParse(Request["LocId"], out x0) ? Convert.ToInt32(Request["LocId"]) : 0; //LocationId
            var i2 = Int32.TryParse(Request["ItmId"], out x0) ? Convert.ToInt32(Request["ItmId"]) : 0; //Id
            var i3 = Int32.TryParse(Request["MfgId"], out x0) ? Convert.ToInt32(Request["MfgId"]) : 0; //ManufId
            var i4 = Int32.TryParse(Request["ImpId"], out x0) ? Convert.ToInt32(Request["ImpId"]) : 0; //ImporterId
            var i5 = Int32.TryParse(Request["CalId"], out x0) ? Convert.ToInt32(Request["CalId"]) : 0; //CaliberId
            var i6 = Int32.TryParse(Request["GtpId"], out x0) ? Convert.ToInt32(Request["GtpId"]) : 0; //GunTypeId
            var i7 = Int32.TryParse(Request["AqTyp"], out x0) ? Convert.ToInt32(Request["AqTyp"]) : 0; //AcqTypeId
            var i8 = Int32.TryParse(Request["DpTyp"], out x0) ? Convert.ToInt32(Request["DpTyp"]) : 0; //DspTypeId
            var i9 = Int32.TryParse(Request["AqFsc"], out x0) ? Convert.ToInt32(Request["AqFsc"]) : 0; //AcqFflSrc
            var i10 = Int32.TryParse(Request["AqFfl"], out x0) ? Convert.ToInt32(Request["AqFfl"]) : 0; //AcqFflId
            var i11 = Int32.TryParse(Request["DpFsc"], out x0) ? Convert.ToInt32(Request["DpFsc"]) : 0; //DspFflSrc
            var i12 = Int32.TryParse(Request["DpFfl"], out x0) ? Convert.ToInt32(Request["DpFfl"]) : 0; //DispFflId
            var i13 = Int32.TryParse(Request["AqSid"], out x0) ? Convert.ToInt32(Request["AqSid"]) : 0; //AcqStateId
            var i14 = Int32.TryParse(Request["DpSid"], out x0) ? Convert.ToInt32(Request["DpSid"]) : 0; //DspStateId

            var i15 = Int32.TryParse(Request["AqFsi"], out x0) ? Convert.ToInt32(Request["AqFsi"]) : 0; //AcqFflStateId
            var i16 = Int32.TryParse(Request["DpFsi"], out x0) ? Convert.ToInt32(Request["DpFsi"]) : 0; //DspFflStateId

            var d1 = DateTime.TryParse(Request["AqDat"], out d0) ? Convert.ToDateTime(Request["AqDat"]) : d0; //AcqDate
            var d2 = DateTime.TryParse(Request["DpDat"], out d0) ? Convert.ToDateTime(Request["DpDat"]) : d0; //DispDate
            var d3 = DateTime.TryParse(Request["AqCue"], out d0) ? Convert.ToDateTime(Request["AqCue"]) : d0; //AcqCurExp
            var d4 = DateTime.TryParse(Request["DpCue"], out d0) ? Convert.ToDateTime(Request["DpCue"]) : d0; //DspCurExp

            var s1 = Request["Model"]; //GunModelName
            var s2 = Request["SerNm"]; //GunSerial
            var s3 = Request["Manuf"]; //GunMfg
            var s4 = Request["Imptr"]; //GunImpt
            var s6 = Request["GunTp"]; //GunType
            var s7 = Request["Calib"]; //GunCaliber
            var s8 = Request["AqNam"]; //AcqName
            var s9 = Request["AqAdf"]; //AcqAddrOrFfl
            var s10 = Request["AqOrg"]; //AcqOrgName
            var s11 = Request["AqFnm"]; //AcqFirstName
            var s12 = Request["AqLnm"]; //AcqLastName
            var s13 = Request["AqAdr"]; //AcqAddress
            var s14 = Request["AqCty"]; //AcqCity
            var s15 = Request["AqZip"]; //AcqZipCode
            var s16 = Request["AqExt"]; //AcqZipExt
            var s17 = Request["AqCur"]; //AcqCurioName
            var s18 = Request["AqCuf"]; //AcqCurioFfl
            var s19 = Request["DpNam"]; //DspName
            var s20 = Request["DpAdf"]; //DspAddrOrFfl
            var s21 = Request["DpOrg"]; //DispOrgName
            var s22 = Request["DpFnm"]; //DispFirstName
            var s23 = Request["DpLnm"]; //DispLastName
            var s24 = Request["DpAdr"]; //DispAddress
            var s25 = Request["DpCty"]; //DispCity
            var s26 = Request["DpZip"]; //DispZipCode
            var s27 = Request["DpExt"]; //DispZipExt
            var s28 = Request["DpCur"]; //DspCurioName
            var s29 = Request["DpCuf"]; //DspCurioFfl
            var ast = Request["AqSta"]; //AcqState
            var dst = Request["DpSta"]; //DispState

            var s30 = Request["AqCfl"]; //AcqCFLC
            var s31 = Request["DpCfl"]; //DspCFLC

            s4 = s4 == "- SELECT -" ? string.Empty : s4;

            var aqs = (AcquisitionSources)i7;
            var dps = (AcquisitionSources)i8;

            var aqZp = s16.Length == 4 ? s15 + "-" + s16 : s15;
            var dpZp = s27.Length == 4 ? s26 + "-" + s27 : s26;
            var owr = bb.DecryptIt(Ow);

            switch (aqs)
            {
                case AcquisitionSources.CommercialFfl:
                    aqNam = s8; //AcqName
                    aqAdf = s9; //AcqAddrOrFfl
                    break;
                case AcquisitionSources.CurioFfl:
                    aqNam = s17;
                    aqAdf = s18;
                    break;
                case AcquisitionSources.PrivateParty:
                    aqNam = s11 + " " + s12;
                    aqAdf = string.Format("{0} {1} {2} {3}", s13, s14, ast, aqZp);
                    break;
                case AcquisitionSources.Police:
                case AcquisitionSources.OtherOrg:
                    aqNam = s10;
                    aqAdf = string.Format("{0} {1} {2} {3}", s13, s14, ast, aqZp);
                    break;
                case AcquisitionSources.OwnersColl:
                    aqNam = owr;
                    aqAdf = "FROM OWNER'S PERSONAL COLLECTION";
                    break;
            }

            switch (dps)
            {
                case AcquisitionSources.CommercialFfl:
                    dpNam = s19; //DispName
                    dpAdf = s20; //DispAddrOrFfl
                    break;
                case AcquisitionSources.CurioFfl:
                    dpNam = s28;
                    dpAdf = s29;
                    break;
                case AcquisitionSources.PrivateParty:
                    dpNam = s22 + " " + s23;
                    dpAdf = string.Format("{0} {1} {2} {3}", s24, s25, dst, dpZp);
                    break;
                case AcquisitionSources.Police:
                case AcquisitionSources.OtherOrg:
                    dpNam = s21;
                    dpAdf = string.Format("{0} {1} {2} {3}", s24, s25, dst, dpZp);
                    break;
                case AcquisitionSources.OwnersColl:
                    dpNam = owr;
                    dpAdf = "FOR OWNER'S PERSONAL COLLECTION";
                    break;
                default:
                    dpNam = string.Empty;
                    dpAdf = string.Empty;
                    break;
            }

            var bm = new BoundBookModel(b1, d1, d2, d3, d4, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, i13, i14, i15, i16, s1, s2, s3, s4, s6, s7,
                aqNam, aqAdf, s10, s11, s12, s13, s14, s15, s16, s17, s18, dpNam, dpAdf, s21, s22, s23, s24, s25, s26, s27, s28, s29, s30, s31);

            var bc = new BookContext();
            bc.UpdateBookItem(bm);

            return Json("Done", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult DisposeGunEntry()
        {
            var x0 = 0;
            var b0 = false;
            var d0 = DateTime.MinValue;
            var bb = new BaseModel();

            var dpNam = string.Empty;
            var dpAdf = string.Empty;

            var b1 = Boolean.TryParse(Request["IsDsp"], out b0) ? Convert.ToBoolean(Request["IsDsp"]) : b0; //IsDisposed
            var b2 = Boolean.TryParse(Request["IsMag"], out b0) ? Convert.ToBoolean(Request["IsMag"]) : b0; //Hi-Caps Disposed

            var i1 = Int32.TryParse(Request["LocId"], out x0) ? Convert.ToInt32(Request["LocId"]) : 0; //LocationId
            var i2 = Int32.TryParse(Request["ItmId"], out x0) ? Convert.ToInt32(Request["ItmId"]) : 0; //Id
            var i3 = Int32.TryParse(Request["DpTyp"], out x0) ? Convert.ToInt32(Request["DpTyp"]) : 0; //DspTypeId
            var i4 = Int32.TryParse(Request["DpFsc"], out x0) ? Convert.ToInt32(Request["DpFsc"]) : 0; //DspFflSrc
            var i5 = Int32.TryParse(Request["DpFfl"], out x0) ? Convert.ToInt32(Request["DpFfl"]) : 0; //DispFflId
            var i6 = Int32.TryParse(Request["DpSid"], out x0) ? Convert.ToInt32(Request["DpSid"]) : 0; //DspStateId

            var d1 = DateTime.TryParse(Request["DpDat"], out d0) ? Convert.ToDateTime(Request["DpDat"]) : d0; //DispDate
            var d2 = DateTime.TryParse(Request["DpCue"], out d0) ? Convert.ToDateTime(Request["DpCue"]) : d0; //DspCurExp


            var s2 = Request["DpNam"]; //DspName
            var s3 = Request["DpAdf"]; //DspAddrOrFfl
            var s4 = Request["DpOrg"]; //DispOrgName
            var s5 = Request["DpFnm"]; //DispFirstName
            var s6 = Request["DpLnm"]; //DispLastName
            var s7 = Request["DpAdr"]; //DispAddress
            var s8 = Request["DpCty"]; //DispCity
            var s9 = Request["DpZip"]; //DispZipCode
            var s10 = Request["DpExt"]; //DispZipExt
            var s11 = Request["DpCur"]; //DspCurioName
            var s12 = Request["DpCuf"]; //DspCurioFfl
            var dst = Request["DpSta"]; //DispState
            var s13 = Request["DpCfl"]; //DispState

            dst = dst == "-SELECT-" ? string.Empty : dst;

            var dps = (AcquisitionSources)i3;

            var dpZp = s10.Length == 4 ? s9 + "-" + s10 : s9;
            var owr = bb.DecryptIt(Ow);

            switch (dps)
            {
                case AcquisitionSources.CommercialFfl:
                    dpNam = s2; //AcqName
                    dpAdf = s3; //AcqAddrOrFfl
                    break;
                case AcquisitionSources.CurioFfl:
                    dpNam = s11;
                    dpAdf = s12;
                    break;
                case AcquisitionSources.PrivateParty:
                    dpNam = s5 + " " + s6;
                    dpAdf = string.Format("{0} {1} {2} {3}", s7, s8, dst, dpZp);
                    break;
                case AcquisitionSources.Police:
                case AcquisitionSources.OtherOrg:
                    dpNam = s10;
                    dpAdf = string.Format("{0} {1} {2} {3}", s7, s8, dst, dpZp);
                    break;
                case AcquisitionSources.OwnersColl:
                    dpNam = owr;
                    dpAdf = "FROM OWNER'S PERSONAL COLLECTION";
                    break;
            }


            var bm = new BoundBookModel(b1, b2, i1, i2, i3, i4, i5, i6, d1, d2, dpNam, dpAdf, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13);

            var bc = new BookContext();
            bc.DisposeBookItem(bm);

            return Json("Done", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetFflBookData(string fflId)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(fflId, out x0) ? Convert.ToInt32(fflId) : 0;

            var bc = new BookContext();
            var wd = bc.GetFflBookEntry(v1);

            return Json(wd, JsonRequestBehavior.AllowGet);
        }

    }
}