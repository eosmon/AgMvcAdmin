using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Web.Mvc;
using AgMvcAdmin.Common;
using AgMvcAdmin.Models;
using AgMvcAdmin.Models.Common;
using AgMvcAdmin.Models.Menus;
using AppBase;
using AppBase.Images;
using Newtonsoft.Json;
using RestSharp;
using WebBase.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace AgMvcAdmin.Controllers
{
    [System.Runtime.InteropServices.GuidAttribute("59C3BEAD-13E0-4F04-9192-FC50F5DE1979")]
    public class InventoryController : PrintController
    {

        public ActionResult Guns()
        {
            var l = new SecurityModel("", "");

            var pg = new PageModel(Pages.InventoryGuns);
            pg.Login = l;
            return View(pg);
        }


        public void CheckRest()
        {
            var client = new RestClient("https://functions-prod.gunsamerica.com/api/identity/connect/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "scope=InventoryServiceAPI&grant_type=client_credentials&client_id=11536065383442-c&client_secret=eb5bc76b-a942-465d-87a0-5afff696f4b6", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            //var body = JsonSerializer.DeserializeAsynch<> (response.Content);
        }

        public ActionResult Ammo()
        {

            /** TEMP TESTING **/

            /* IP ADDRESS */
            var a = string.Empty;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    a =  ip.ToString();
                    break;
                }
            }

            /* HOST NAME*/
            var b = Environment.MachineName;

            /** END TESTING **/

            var pg = new PageModel(Pages.InventoryAmmo);
            return View(pg);
        }

        public ActionResult Merchandise()
        {
            var pg = new PageModel(Pages.InventoryMerchandise);
            return View(pg);
        }




        [HttpPost]
        public JsonResult CreateGunEntry()
        {
            var ic = new InvContext();
            var bb = new BaseModel();
 
            var cr = new CaRestrictModel();
            var bm = new BoundBookModel();

            var owr = bb.DecryptIt(Ow);
            var acqNm = string.Empty;
            var acqAf = string.Empty;

            var i0 = 0;
            double d0 = 0.00;
            var b0 = false;
 
            var bgm = Request["GunMfg"];
            var bgi = Request["GunImp"];
            var bgd = Request["GunMdl"];
            var bse = Request["Serial"];
            var bgt = Request["GunTyp"];
            var bca = Request["Calibr"];
            var cfl = Request["Cflchk"];
            var upc = Request["UpcCod"];
            var wsu = Request["WebUpc"];
            var mpn = Request["MfgNum"];
            var des = Request["Descrp"];
            var lds = Request["LngDes"];
            var mdl = Request["GModel"];
            var osk = Request["OldSku"];

            var ig1 = Request["ImgGn1"];
            var ig2 = Request["ImgGn2"];
            var ig3 = Request["ImgGn3"];
            var ig4 = Request["ImgGn4"];
            var ig5 = Request["ImgGn5"];
            var ig6 = Request["ImgGn6"];
            var igD = Request["ImgDst"];

            var bcu = Request["TgCus"];
            var eml = Request["BkEmail"];


            var mid = Int32.TryParse(Request["MstrId"], out i0) ? Convert.ToInt32(Request["MstrId"]) : 0;
            var loc = Int32.TryParse(Request["LocaId"], out i0) ? Convert.ToInt32(Request["LocaId"]) : 0;
            var gtp = Int32.TryParse(Request["GnTpId"], out i0) ? Convert.ToInt32(Request["GnTpId"]) : 0;
            var ttp = Int32.TryParse(Request["TrTpId"], out i0) ? Convert.ToInt32(Request["TrTpId"]) : 0;
            var acq = Int32.TryParse(Request["AcqTyp"], out i0) ? Convert.ToInt32(Request["AcqTyp"]) : 0;
            var ffl = Int32.TryParse(Request["FlCode"], out i0) ? Convert.ToInt32(Request["FlCode"]) : 0;
            var isi = Int32.TryParse(Request["IstkId"], out i0) ? Convert.ToInt32(Request["IstkId"]) : 0;
            var mfg = Int32.TryParse(Request["ManfId"], out i0) ? Convert.ToInt32(Request["ManfId"]) : 0;
            var imp = Int32.TryParse(Request["ImptId"], out i0) ? Convert.ToInt32(Request["ImptId"]) : 0;
            var cal = Int32.TryParse(Request["ClbrId"], out i0) ? Convert.ToInt32(Request["ClbrId"]) : 0;
            var atn = Int32.TryParse(Request["ActnId"], out i0) ? Convert.ToInt32(Request["ActnId"]) : 0;
            var fin = Int32.TryParse(Request["FinhId"], out i0) ? Convert.ToInt32(Request["FinhId"]) : 0;
            var cnd = Int32.TryParse(Request["CondId"], out i0) ? Convert.ToInt32(Request["CondId"]) : 0;
            var lbs = Int32.TryParse(Request["WgtLbs"], out i0) ? Convert.ToInt32(Request["WgtLbs"]) : 0;
            var cap = Int32.TryParse(Request["MagCap"], out i0) ? Convert.ToInt32(Request["MagCap"]) : 0;
            var lmk = Int32.TryParse(Request["LokMak"], out i0) ? Convert.ToInt32(Request["LokMak"]) : 0;
            var lmd = Int32.TryParse(Request["LokMod"], out i0) ? Convert.ToInt32(Request["LokMod"]) : 0;
            var hct = Int32.TryParse(Request["HiCpCt"], out i0) ? Convert.ToInt32(Request["HiCpCt"]) : 0;
            var hcc = Int32.TryParse(Request["HiCpCp"], out i0) ? Convert.ToInt32(Request["HiCpCp"]) : 0;
            var fsc = Int32.TryParse(Request["FlScId"], out i0) ? Convert.ToInt32(Request["FlScId"]) : 0;
            var cus = Int32.TryParse(Request["CustId"], out i0) ? Convert.ToInt32(Request["CustId"]) : 0;
            var sup = Int32.TryParse(Request["SuppId"], out i0) ? Convert.ToInt32(Request["SuppId"]) : 0;

            var cst = Double.TryParse(Request["GunCst"], out d0) ? Convert.ToDouble(Request["GunCst"]) : 0.00;
            var frt = Double.TryParse(Request["Freigt"], out d0) ? Convert.ToDouble(Request["Freigt"]) : 0.00;
            var fee = Double.TryParse(Request["GunFee"], out d0) ? Convert.ToDouble(Request["GunFee"]) : 0.00;
            var tax = Double.TryParse(Request["TaxAmt"], out d0) ? Convert.ToDouble(Request["TaxAmt"]) : 0.00;
            var brl = Double.TryParse(Request["BrlDec"], out d0) ? Convert.ToDouble(Request["BrlDec"]) : 0.00;
            var ovl = Double.TryParse(Request["OvrDec"], out d0) ? Convert.ToDouble(Request["OvrDec"]) : 0.00;
            var chm = Double.TryParse(Request["ChbDec"], out d0) ? Convert.ToDouble(Request["ChbDec"]) : 0.00;
            var ozs = Double.TryParse(Request["WgtOzs"], out d0) ? Convert.ToDouble(Request["WgtOzs"]) : 0.00;
            var ask = Double.TryParse(Request["PrcAsk"], out d0) ? Convert.ToDouble(Request["PrcAsk"]) : 0.00;
            var msr = Double.TryParse(Request["PrcMsr"], out d0) ? Convert.ToDouble(Request["PrcMsr"]) : 0.00;
            var sal = Double.TryParse(Request["PrcSal"], out d0) ? Convert.ToDouble(Request["PrcSal"]) : 0.00;
            var prc = Double.TryParse(Request["CusPrc"], out d0) ? Convert.ToDouble(Request["CusPrc"]) : 0.00;

            //var bpt = Boolean.TryParse(Request["PrtTag"], out b0) ? Convert.ToBoolean(Request["PrtTag"]) : b0;
            var owb = Boolean.TryParse(Request["IsOnWb"], out b0) ? Convert.ToBoolean(Request["IsOnWb"]) : b0;
            var iwb = Boolean.TryParse(Request["IsWbsd"], out b0) ? Convert.ToBoolean(Request["IsWbsd"]) : b0;
            var sgt = Boolean.TryParse(Request["GotTax"], out b0) ? Convert.ToBoolean(Request["GotTax"]) : b0;
            var cmd = Boolean.TryParse(Request["CurMdl"], out b0) ? Convert.ToBoolean(Request["CurMdl"]) : b0;
            var usd = Boolean.TryParse(Request["IsUsed"], out b0) ? Convert.ToBoolean(Request["IsUsed"]) : b0;
            var hid = Boolean.TryParse(Request["HideGn"], out b0) ? Convert.ToBoolean(Request["HideGn"]) : b0;
            var atv = Boolean.TryParse(Request["Active"], out b0) ? Convert.ToBoolean(Request["Active"]) : b0;
            var ver = Boolean.TryParse(Request["Verifd"], out b0) ? Convert.ToBoolean(Request["Verifd"]) : b0;
            var box = Boolean.TryParse(Request["OrgBox"], out b0) ? Convert.ToBoolean(Request["OrgBox"]) : b0;
            var ppw = Boolean.TryParse(Request["HasPpw"], out b0) ? Convert.ToBoolean(Request["HasPpw"]) : b0;
            var hca = Boolean.TryParse(Request["CaHide"], out b0) ? Convert.ToBoolean(Request["CaHide"]) : b0;
            var cok = Boolean.TryParse(Request["CaLegl"], out b0) ? Convert.ToBoolean(Request["CaLegl"]) : b0;
            var ros = Boolean.TryParse(Request["CaRost"], out b0) ? Convert.ToBoolean(Request["CaRost"]) : b0;
            var cur = Boolean.TryParse(Request["CaCuro"], out b0) ? Convert.ToBoolean(Request["CaCuro"]) : b0;
            var san = Boolean.TryParse(Request["CaSaRv"], out b0) ? Convert.ToBoolean(Request["CaSaRv"]) : b0;
            var sst = Boolean.TryParse(Request["CaSsPt"], out b0) ? Convert.ToBoolean(Request["CaSsPt"]) : b0;
            var ppt = Boolean.TryParse(Request["CaPptr"], out b0) ? Convert.ToBoolean(Request["CaPptr"]) : b0;
            var apt = Boolean.TryParse(Request["ActPpt"], out b0) ? Convert.ToBoolean(Request["ActPpt"]) : b0;
            var hol = Boolean.TryParse(Request["Hold30"], out b0) ? Convert.ToBoolean(Request["Hold30"]) : b0;
            var ios = Boolean.TryParse(Request["IsOlSk"], out b0) ? Convert.ToBoolean(Request["IsOlSk"]) : b0;

            var d1 = DateTime.MinValue;
            var d2 = DateTime.MinValue;
            var d3 = DateTime.MinValue;

            var adt = DateTime.TryParse(Request["AcqDat"], out d2) ? Convert.ToDateTime(Request["AcqDat"]) : d2;
            var hed = DateTime.TryParse(Request["HldExp"], out d3) ? Convert.ToDateTime(Request["HldExp"]) : d3;

            var uca = 0;
            var uwy = 0;
            var nca = 0;
            var nwy = 0;

            var fs = ttp < 103;

            if (ttp == 101) { cus = 0; bcu = string.Empty; } // Item placed for sale inventory, no customer assigned

            if (loc == 1) { if (fs) { uca = 1; } else { nca = 1; } }
            if (loc == 2) { if (fs) { uwy = 1; } else { nwy = 1; } }

            /* CREATE BASE GUN ENTRY*/ 
            var gm = new GunModel(mid, isi, mfg, gtp, atn, fin, cap, cal, lbs, cnd, lmk, lmd, brl, chm, ovl, ozs, osk, mdl, upc, wsu, mpn, des, lds, bse, cmd, hid, atv, ver, owb, usd, box, ppw, ios);
            var am = new AcctModel(uca, uwy, nca, nwy, ask, sal, msr, cst, frt, fee, prc, tax, sgt, bcu);
            bm = new BoundBookModel(loc, ttp, fs, adt); 
            cr = new CaRestrictModel(hca, cok, ros, ppt, cur, san, sst, apt);

            var m = new AddToBookModel(gm, am, bm, cr, cus, sup);
            var tm = ic.AddGunInventory(m);

            /* COMPLIANCE INSERT */
            var bc = new BookContext();
            var abm = new AddToBookModel();
 
            bm = new BoundBookModel(tm.InStockId, loc, tm.ItemBasisID, gtp, ttp, acq, mfg, imp, gtp, cal, fsc, ffl, tm.TagSku, acqNm, acqAf, bgm, bgi, bgd, bse, bgt, bca, cfl, eml, adt, fs);
            cr = new CaRestrictModel(hol, hcc, hct, hed);
            abm.BoundBook = bm;
            abm.Compliance = cr;
            abm.SupplierId = sup;

            var bkm = bc.InsertBookEntry(abm);



            var str = new string[] { ig1, ig2, ig3, ig4, ig5, ig6 };
            var ps = ConfigurationHelper.GetPropertyValue("application", "p7");
            var gp = ConfigurationHelper.GetPropertyValue("application", "p1");
            var hp = ConfigurationHelper.GetPropertyValue("application", "p12");
            var ip = ConfigurationHelper.GetPropertyValue("application", "p15");
            var vip = bb.DecryptIt(ip);
            var dgp = bb.DecryptIt(gp);
            var sgp = bb.DecryptIt(ps);


            var cat = Enum.GetName(typeof(ImgSections), (int)ImgSections.Guns);

            if (Request.Files.Count > 0)
            {
                var f = Request.Files;
                var gid = Request["GroupId"];
                var oig = Request["OrigImg"];

                var arr = gid.Contains(",") ? gid.Split(',') : new[] { gid };
                var iar = oig.Contains(",") ? oig.Split(',') : new[] { oig };
                var nl = owb ? arr.Length : 1; /* RULE: Not Posted on Web - 1 Pic MAX */

                for (int i = 0; i < nl; i++)
                {
                    var x = arr[i].Replace("ImgHse_", string.Empty);
                    var ix = Int32.TryParse(x, out i0) ? Convert.ToInt32(x) : i0;

                    var imgName = string.Format("{0}-{1}.jpg", cat, CookRandomStr(8));
                    UpdateSalePic(f[i], tm.InStockId, ix, imgName, iar[i], ImgSections.Guns, (PicFolders)ttp);

                }
            }

            if (iwb) //SINGLE DISTRIBUTOR IMG ON WEBSITE, COPY TO INSTOCK & IMG SECTION DIRECTORY
            {
                //var dir = Enum.GetName(typeof(PicFolders), ttp);
                var j = ConfigurationHelper.GetPropertyValue("application", "p14");
                var s1 = ConfigurationHelper.GetPropertyValue("application", "s1");
                var bStk = bb.DecryptIt(j);


                var img = ig1;
                var dst = igD;
                var fromPath = string.Format("{0}\\{1}\\L\\{2}", sgp, dst, img);

                if (System.IO.File.Exists(fromPath))
                {
                    var xbs = bb.DecryptIt(s1);
                    var xtt = string.Format("s{0}", ttp);
                    var dtt = ConfigurationHelper.GetPropertyValue("application", xtt);
                    var xdr = bb.DecryptIt(dtt);
                    var xPath = string.Format("{0}\\{1}\\{2}\\{3}", xbs, xdr, cat, img);

                    // Category Folder Copy
                    System.IO.File.Copy(fromPath, xPath, true);

                    // InStock folder for inventory grid
                    var toPath = string.Format("{0}\\{1}\\{2}", bStk, cat, img);

                    System.IO.File.Copy(fromPath, toPath, true);
                    ic.UpdateGunImg(tm.InStockId, img);
                }
            }

            //switch (aed)
            //{
            //    case "Create": // FROM INQUIRY IMAGES

            //        for (var x = 0; x < 6; x++)
            //        {
            //            var r = str[x];
            //            if (r.Length < 20) { continue; }
            //            var n = r.Substring(r.Length - 5, 5);
            //            var o = n.Replace(".jpg", string.Empty);
            //            var p = Int32.TryParse(o, out i0) ? Convert.ToInt32(o) : i0;

            //            var fName = string.Format("{0}-{1}-{2}.jpg", folder, tm.GunId, p);
            //            var fr = vip + "\\" + r;
            //            var to = dgp + "\\" + fName;
            //            System.IO.File.Copy(fr, to, true);
            //            if (p == 1)
            //            {
            //                var hi = string.Format("{0}\\{1}", bb.DecryptIt(hp), fName);
            //                System.IO.File.Copy(fr, hi, true);
            //            }
            //            ic.UpdateGunImg(tm.GunId, x + 1, fName);
            //        }
            //        break;

            //    case "AddUnit": // COPY WEB IMAGE TO INSTOCK
            //        var fn = string.Format("{0}-{1}-{2}.jpg", folder, tm.GunId, 1);
            //        var sf = sgp + ig1;
            //        var st = dgp + "\\" + fn;
            //        System.IO.File.Copy(sf, st, true);
            //        ic.UpdateGunImg(tm.GunId, fn);
            //        break;

            //    case "InStock": // COPY WEB IMAGE TO INSTOCK
            //        ic.UpdateGunImg(tm.GunId, ig1);
            //        break;
            //}

            // PHOTOS: ADD FOR SALE AND NON-SALE GUNS 
            //if (Request.Files.Count > 0)  
            //{
            //    var f = Request.Files;
            //    var gid = Request["GroupId"];

            //    var arr = gid.Contains(",") ? gid.Split(',') : new[] { gid };
            //    for (int i = 0; i < arr.Length; i++)
            //    {
            //        var x = arr[i].Replace("ImgGun_", string.Empty);
            //        var ix = Int32.TryParse(x, out i0) ? Convert.ToInt32(x) : i0;

            //        var imgName = string.Format("{0}-{1}-{2}.jpg", folder, tm.GunId, ix);
            //        UpdateGunPic(f[i], tm.GunId, ix, imgName);
            //     }

            //}

            return Json(tm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RestockGun()
        {

            var i0 = 0;
            var b0 = false;
            var dt0 = DateTime.MinValue;
            double d0 = 0.00;

            var bb = new BaseModel();
            var owr = bb.DecryptIt(Ow);

            var acqNm = string.Empty;
            var acqAf = string.Empty;

            var i1 = Int32.TryParse(Request["GunId"], out i0) ? Convert.ToInt32(Request["GunId"]) : i0;
            var i2 = Int32.TryParse(Request["LocId"], out i0) ? Convert.ToInt32(Request["LocId"]) : i0;
            var i3 = Int32.TryParse(Request["TtpId"], out i0) ? Convert.ToInt32(Request["TtpId"]) : i0;
            var i4 = Int32.TryParse(Request["LokMk"], out i0) ? Convert.ToInt32(Request["LokMk"]) : i0;
            var i5 = Int32.TryParse(Request["LokMd"], out i0) ? Convert.ToInt32(Request["LokMd"]) : i0;
            var i6 = Int32.TryParse(Request["HcpCt"], out i0) ? Convert.ToInt32(Request["HcpCt"]) : i0;
            var i7 = Int32.TryParse(Request["HcpCp"], out i0) ? Convert.ToInt32(Request["HcpCp"]) : i0;
            var i8 = Int32.TryParse(Request["AcqSc"], out i0) ? Convert.ToInt32(Request["AcqSc"]) : i0;
            var i9 = Int32.TryParse(Request["FflSc"], out i0) ? Convert.ToInt32(Request["FflSc"]) : i0;
            var i10 = Int32.TryParse(Request["FflCd"], out i0) ? Convert.ToInt32(Request["FflCd"]) : i0;
            var i11 = Int32.TryParse(Request["StaId"], out i0) ? Convert.ToInt32(Request["StaId"]) : i0;
            var i12 = Int32.TryParse(Request["CusId"], out i0) ? Convert.ToInt32(Request["CusId"]) : i0;
            var i13 = Int32.TryParse(Request["SupId"], out i0) ? Convert.ToInt32(Request["SupId"]) : i0;

            var d1 = Double.TryParse(Request["Ucost"], out d0) ? Convert.ToDouble(Request["Ucost"]) : d0;
            var d2 = Double.TryParse(Request["Frght"], out d0) ? Convert.ToDouble(Request["Frght"]) : d0;
            var d3 = Double.TryParse(Request["Ufees"], out d0) ? Convert.ToDouble(Request["Ufees"]) : d0;
            var d4 = Double.TryParse(Request["TaxAm"], out d0) ? Convert.ToDouble(Request["TaxAm"]) : d0;
            var d5 = Double.TryParse(Request["CusPd"], out d0) ? Convert.ToDouble(Request["CusPd"]) : d0;
            var d6 = Double.TryParse(Request["ComPr"], out d0) ? Convert.ToDouble(Request["ComPr"]) : d0;

            var b1 = Boolean.TryParse(Request["IsTax"], out b0) ? Convert.ToBoolean(Request["IsTax"]) : b0;
            var b2 = Boolean.TryParse(Request["IsTxp"], out b0) ? Convert.ToBoolean(Request["IsTxp"]) : b0;
            var b3 = Boolean.TryParse(Request["IsHld"], out b0) ? Convert.ToBoolean(Request["IsHld"]) : b0;
            var b4 = Boolean.TryParse(Request["IsOwb"], out b0) ? Convert.ToBoolean(Request["IsOwb"]) : b0;
            var b5 = Boolean.TryParse(Request["IsOsk"], out b0) ? Convert.ToBoolean(Request["IsOsk"]) : b0;
            var b6 = Boolean.TryParse(Request["IsPpt"], out b0) ? Convert.ToBoolean(Request["IsPpt"]) : b0;
            var b7 = Boolean.TryParse(Request["CaPpt"], out b0) ? Convert.ToBoolean(Request["CaPpt"]) : b0;

            var dt1 = DateTime.TryParse(Request["AcqDt"], out dt0) ? Convert.ToDateTime(Request["AcqDt"]) : DateTime.MinValue;
            var dt2 = DateTime.TryParse(Request["HxpDt"], out dt0) ? Convert.ToDateTime(Request["HxpDt"]) : DateTime.MinValue;
 
            var v1 = Request["SvcCu"];
            var v2 = Request["Seria"];
            var v3 = Request["CflcN"];
            var v4 = Request["OlSku"];
            var v5 = Request["Email"];

            var uca = 0;
            var uwy = 0;
            var nca = 0;
            var nwy = 0;

            var fs = i3 < 103;

            if (i2 == 1) { if (fs) { uca = 1; } else { nca = 1; } }
            if (i2 == 2) { if (fs) { uwy = 1; } else { nwy = 1; } }

            var gm = new GunModel(i1, v4, b4, b5);
            var am = new AcctModel(i3, uca, uwy, nca, nwy, i12, i13, d1, d2, d3, d4, d5, d6, b1, b2, v1);
            var ca = new CaRestrictModel(i4, i5, i6, i7, v3, dt2, dt1, b3, b6, b7);
            var bm = new BoundBookModel(i2, i8, i9, i10, v2, v5, dt1);
 
            var a = new AddToBookModel();
            a.Gun = gm;
            a.Accounting = am;
            a.Compliance = ca;
            a.BoundBook = bm;

            var ic = new InvContext();
            var tm = ic.RestockGun(a);

            return Json(tm, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult SetMfg()
        {
            var i0 = 0;

            var i1 = Int32.TryParse(Request["Loc"], out i0) ? Convert.ToInt32(Request["Loc"]) : 0;

            var mm = new MenuModel();
            var m = mm.GetManufacturersInStockGuns(i1);

            return Json(m, JsonRequestBehavior.AllowGet);

        }


        //[HttpPost]
        //public JsonResult ReadFullGun(string id)
        //{
        //    var i0 = 0;

        //    var v1 = Int32.TryParse(id, out i0) ? Convert.ToInt32(id) : 0;

        //    var ic = new InvContext();
        //    var m = ic.GetFullGunProfile(v1);

        //    return Json(new { Guns = m.Gun, Acct = m.Accounting, Cali = m.Compliance }, JsonRequestBehavior.AllowGet);

        //}


        //[HttpPost]
        //public JsonResult ReadNonSaleGun(string tid)
        //{
        //    var ic = new InvContext();
        //    var m = ic.GetNonSaleGun(tid);

        //    return Json(new { Guns = m.Gun, Acct = m.Accounting, Cali = m.Compliance }, JsonRequestBehavior.AllowGet);

        //}


        //[HttpPost]
        //public JsonResult UpdateNonSaleGun()
        //{
        //    var i0 = 0;
        //    var b0 = false;
        //    double d0 = 0.00;

        //    var i1 = Int32.TryParse(Request["Atn"], out i0) ? Convert.ToInt32(Request["Atn"]) : 0;
        //    var i2 = Int32.TryParse(Request["Fin"], out i0) ? Convert.ToInt32(Request["Fin"]) : 0;
        //    var i3 = Int32.TryParse(Request["Cap"], out i0) ? Convert.ToInt32(Request["Cap"]) : 0;
        //    var i4 = Int32.TryParse(Request["Lmk"], out i0) ? Convert.ToInt32(Request["Lmk"]) : 0;
        //    var i5 = Int32.TryParse(Request["Lmd"], out i0) ? Convert.ToInt32(Request["Lmd"]) : 0;
        //    var i6 = Int32.TryParse(Request["Hcc"], out i0) ? Convert.ToInt32(Request["Hcc"]) : 0;
        //    var i7 = Int32.TryParse(Request["Hcp"], out i0) ? Convert.ToInt32(Request["Hcp"]) : 0;

        //    var d1 = Double.TryParse(Request["Bdc"], out d0) ? Convert.ToDouble(Request["Bdc"]) : 0;
        //    var d2 = Double.TryParse(Request["Cos"], out d0) ? Convert.ToDouble(Request["Cos"]) : 0;
        //    var d3 = Double.TryParse(Request["Frt"], out d0) ? Convert.ToDouble(Request["Frt"]) : 0;
        //    var d4 = Double.TryParse(Request["Fee"], out d0) ? Convert.ToDouble(Request["Fee"]) : 0;
        //    var d5 = Double.TryParse(Request["Tam"], out d0) ? Convert.ToDouble(Request["Tam"]) : 0;

        //    var b1 = Boolean.TryParse(Request["Box"], out b0) ? Convert.ToBoolean(Request["Box"]) : b0;
        //    var b2 = Boolean.TryParse(Request["Ppw"], out b0) ? Convert.ToBoolean(Request["Ppw"]) : b0;
        //    var b3 = Boolean.TryParse(Request["Txp"], out b0) ? Convert.ToBoolean(Request["Txp"]) : b0;
        //    var b4 = Boolean.TryParse(Request["Hgn"], out b0) ? Convert.ToBoolean(Request["Hgn"]) : b0;
        //    var b5 = Boolean.TryParse(Request["Itx"], out b0) ? Convert.ToBoolean(Request["Itx"]) : b0;

        //    var v1 = Request["Tid"];
        //    var v2 = Request["Lds"];
        //    var v3 = Request["Cfl"];
        //    var v4 = Request["Hex"];

        //    var a = new AddToBookModel();
        //    var gm = new GunModel(v1, i1, i2, i3, d1, b1, b2, v2);
        //    var am = new AcctModel(d2, d3, d5, d4, b5, b3);
        //    var cm = new CaRestrictModel(i4, i5, i6, i7, b4, v3, v4);

        //    a.Gun = gm;
        //    a.Accounting = am;
        //    a.Compliance = cm;

        //    var ic = new InvContext();
        //    ic.UpdateNonSaleGun(a);

        //    if (Request.Files.Count > 0)
        //    {
        //        var f = Request.Files;
        //        var gid = Request["GroupId"];

        //        var arr = gid.Contains(",") ? gid.Split(',') : new[] { gid };

        //        for (int i = 0; i < arr.Length; i++)
        //        {
        //            var x = arr[i].Replace("ImgGun_", string.Empty);
        //            var imgName = string.Format("Gun-{0}-{1}.jpg", v1, x);
        //            UpdateBookPic(f[i], v1, Convert.ToInt32(x), imgName);
        //        }

        //    }


        //    return Json("success", JsonRequestBehavior.AllowGet);

        //}


        //[HttpPost]
        //public JsonResult GetInStockGun()
        //{
        //    var x0 = 0;
        //    var v1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : x0;

        //    var ic = new InvContext();
        //    var a = ic.GetAmmoItem(v1);

        //    return Json(a, JsonRequestBehavior.AllowGet);
        //}


        //public void WriteDfImg(int i, HttpPostedFileBase file, string sku, int mstId, bool addPm)
        //{
        //    if (Request.Files.Count > 0)
        //    {
        //        var bPath = ConfigurationHelper.GetPropertyValue("application", "PathSiteImgs");
        //        var gunPath = ConfigurationHelper.GetPropertyValue("application", "PathHseImgs");
        //        var imgName = string.Format("{0}_{1}.jpg", sku, i+1);
        //        var fullPath = String.Format("{0}T\\{1}", bPath, imgName);

        //        file.SaveAs(fullPath);

        //        if (System.IO.File.Exists(fullPath))
        //        {
        //            ImageBase.MountInStockImage(fullPath, gunPath, imgName);
        //            UpdateSitePic(file, SiteImageSections.Guns, mstId, imgName, addPm);

        //            //ItemData d = new ItemData();
        //            //d.ID = mstId;
        //            //d.DistributorId = (int)Distributors.HSE;
        //            //d.ImageName = imgName;
        //            //d.ItemKey = sku;

        //            //var dfc = new DataFeedContext();
        //            //if (isWebImg) { dfc.UpdateHouseImage(d); } else { dfc.UpdateDisplayGunPics(d); }

        //        }
        //    }
        //}



        [HttpPost]
        public JsonResult GetWarehouses(string fflId)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(fflId, out x0) ? Convert.ToInt32(fflId) : 0;

            var mm = new MenuModel();
            var wd = mm.GetWarehouse(v1);

            return Json(wd, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetInquiries(string transTypeId)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(transTypeId, out x0) ? Convert.ToInt32(transTypeId) : 0;

            var mm = new MenuModel();
            var wd = mm.GetSvcInqHistory();

            return Json(wd, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FflByName(string search, string state)
        {
            //if (search.Length < 3) { return Json("min 3 characters required"); }
            var x0 = 0;
            var v1 = Int32.TryParse(state, out x0) ? Convert.ToInt32(state) : 0;

            var fc = new FflContext();
            var data = fc.SearchFflByName(search, v1);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FflById(string fcd)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(fcd, out x0) ? Convert.ToInt32(fcd) : 0;

            var fc = new FflContext();
            var data = fc.GetFflById(v1);

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetSubCats(string catId)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(catId, out x0) ? Convert.ToInt32(catId) : 0;

            var mm = new MenuModel();
            var sc = mm.GetSubCategories(v1);

            return Json(sc, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateGunCtl()
        {

            var i0 = 0;
            var b0 = false;
            double d0 = 0.00;
            DateTime dt0 = DateTime.MinValue;

            var im = Int32.TryParse(Request["IskId"], out i0) ? Convert.ToInt32(Request["IskId"]) : i0;
            var i1 = Int32.TryParse(Request["CbsId"], out i0) ? Convert.ToInt32(Request["CbsId"]) : i0;
            var i3 = Int32.TryParse(Request["LMake"], out i0) ? Convert.ToInt32(Request["LMake"]) : i0;
            var i4 = Int32.TryParse(Request["LModl"], out i0) ? Convert.ToInt32(Request["LModl"]) : i0;
            var i5 = Int32.TryParse(Request["HcpCt"], out i0) ? Convert.ToInt32(Request["HcpCt"]) : i0;
            var i6 = Int32.TryParse(Request["MCapa"], out i0) ? Convert.ToInt32(Request["MCapa"]) : i0;
            var i7 = Int32.TryParse(Request["AtnId"], out i0) ? Convert.ToInt32(Request["AtnId"]) : i0;
            var i8 = Int32.TryParse(Request["FinId"], out i0) ? Convert.ToInt32(Request["FinId"]) : i0;
            var i9 = Int32.TryParse(Request["CndId"], out i0) ? Convert.ToInt32(Request["CndId"]) : i0;
            var i10 = Int32.TryParse(Request["Capty"], out i0) ? Convert.ToInt32(Request["Capty"]) : i0;
            var i11 = Int32.TryParse(Request["WtLbs"], out i0) ? Convert.ToInt32(Request["WtLbs"]) : i0;
            var i12 = Int32.TryParse(Request["TtpId"], out i0) ? Convert.ToInt32(Request["TtpId"]) : i0;
            var i13 = Int32.TryParse(Request["CusId"], out i0) ? Convert.ToInt32(Request["CusId"]) : i0;
            var i14 = Int32.TryParse(Request["SelId"], out i0) ? Convert.ToInt32(Request["SelId"]) : i0;

            var d1 = Double.TryParse(Request["ICost"], out d0) ? Convert.ToDouble(Request["ICost"]) : d0;
            var d2 = Double.TryParse(Request["Frght"], out d0) ? Convert.ToDouble(Request["Frght"]) : d0;
            var d3 = Double.TryParse(Request["IFees"], out d0) ? Convert.ToDouble(Request["IFees"]) : d0;
            var d4 = Double.TryParse(Request["TaxAm"], out d0) ? Convert.ToDouble(Request["TaxAm"]) : d0;
            var d5 = Double.TryParse(Request["Price"], out d0) ? Convert.ToDouble(Request["Price"]) : d0;
            var d6 = Double.TryParse(Request["PMsrp"], out d0) ? Convert.ToDouble(Request["PMsrp"]) : d0;
            var d7 = Double.TryParse(Request["PSale"], out d0) ? Convert.ToDouble(Request["PSale"]) : d0;
            var d8 = Double.TryParse(Request["WtOzs"], out d0) ? Convert.ToDouble(Request["WtOzs"]) : d0;
            var d9 = Double.TryParse(Request["BarDc"], out d0) ? Convert.ToDouble(Request["BarDc"]) : d0;
            var d10 = Double.TryParse(Request["ChmDc"], out d0) ? Convert.ToDouble(Request["ChmDc"]) : d0;
            var d11 = Double.TryParse(Request["OvrDc"], out d0) ? Convert.ToDouble(Request["OvrDc"]) : d0;
            var d12 = Double.TryParse(Request["CuPrc"], out d0) ? Convert.ToDouble(Request["CuPrc"]) : d0;

            var b1 = Boolean.TryParse(Request["TaxEx"], out b0) ? Convert.ToBoolean(Request["TaxEx"]) : b0;
            var b2 = Boolean.TryParse(Request["ScTax"], out b0) ? Convert.ToBoolean(Request["ScTax"]) : b0;
            var b3 = Boolean.TryParse(Request["CaOky"], out b0) ? Convert.ToBoolean(Request["CaOky"]) : b0;
            var b4 = Boolean.TryParse(Request["CaHid"], out b0) ? Convert.ToBoolean(Request["CaHid"]) : b0;
            var b5 = Boolean.TryParse(Request["HoldG"], out b0) ? Convert.ToBoolean(Request["HoldG"]) : b0;
            var b6 = Boolean.TryParse(Request["CaPpt"], out b0) ? Convert.ToBoolean(Request["CaPpt"]) : b0;
            var b7 = Boolean.TryParse(Request["CaCur"], out b0) ? Convert.ToBoolean(Request["CaCur"]) : b0;
            var b8 = Boolean.TryParse(Request["CaRos"], out b0) ? Convert.ToBoolean(Request["CaRos"]) : b0;
            var b9 = Boolean.TryParse(Request["CaSae"], out b0) ? Convert.ToBoolean(Request["CaSae"]) : b0;
            var b10 = Boolean.TryParse(Request["CaSsp"], out b0) ? Convert.ToBoolean(Request["CaSsp"]) : b0;
            var b11 = Boolean.TryParse(Request["HideG"], out b0) ? Convert.ToBoolean(Request["HideG"]) : b0;
            var b12 = Boolean.TryParse(Request["Activ"], out b0) ? Convert.ToBoolean(Request["Activ"]) : b0;
            var b13 = Boolean.TryParse(Request["IUsed"], out b0) ? Convert.ToBoolean(Request["IUsed"]) : b0;
            var b14 = Boolean.TryParse(Request["OnWeb"], out b0) ? Convert.ToBoolean(Request["OnWeb"]) : b0;
            var b15 = Boolean.TryParse(Request["Verif"], out b0) ? Convert.ToBoolean(Request["Verif"]) : b0;
            var b16 = Boolean.TryParse(Request["OgBox"], out b0) ? Convert.ToBoolean(Request["OgBox"]) : b0;
            var b17 = Boolean.TryParse(Request["OgPpw"], out b0) ? Convert.ToBoolean(Request["OgPpw"]) : b0;
            var b18 = Boolean.TryParse(Request["CuMdl"], out b0) ? Convert.ToBoolean(Request["CuMdl"]) : b0;
            var b19 = Boolean.TryParse(Request["Print"], out b0) ? Convert.ToBoolean(Request["Print"]) : b0;
            var b20 = Boolean.TryParse(Request["IfSal"], out b0) ? Convert.ToBoolean(Request["IfSal"]) : b0;
            var b21 = Boolean.TryParse(Request["IsOsk"], out b0) ? Convert.ToBoolean(Request["IsOsk"]) : b0;
            var b22 = Boolean.TryParse(Request["IsPpt"], out b0) ? Convert.ToBoolean(Request["IsPpt"]) : b0;


            var dt1 = DateTime.TryParse(Request["HldEx"], out dt0) ? Convert.ToDateTime(Request["HldEx"]) : dt0;

            var v1 = Request["Caflc"];
            var v2 = Request["UpcCd"];
            var v3 = Request["Descr"];
            var v4 = Request["WbUpc"];
            var v5 = Request["Model"];
            var v6 = Request["MfgPn"];
            var v7 = Request["LgDes"];
            var v8 = Request["TgSku"];
            var v9 = Request["TgMfg"];
            var v10 = Request["TgBrl"];
            var v11 = Request["TgCap"];
            var v12 = Request["TgCnd"];
            var v13 = Request["TgTyp"];
            var v14 = Request["TgCal"];
            var v15 = Request["TgCus"];
            var v16 = Request["OldSk"];


            var am = new AcctModel(i12, b1, b2, d1, d2, d3, d4, d5, d6, d7, d12, v15);
            var ca = new CaRestrictModel(i3, i4, i5, i6, b3, b4, b5, b6, b7, b8, b9, b10, b22, v1, dt1);
            var gm = new GunModel(i1, i7, i8, i9, i10, i11, d8, d9, d10, d11, b11, b12, b13, b14, b15, b16, b18, b17, b21, v16, v2, v3, v4, v5, v6, v7, v8);

            var abm = new AddToBookModel(am, ca, gm, i13, i14);

            if (!b2) /* Clean Up Excess Web Images */
            {
                FlushWebPics(SiteImageSections.Guns, im);
            }


            var ic = new InvContext();
            var tm = ic.UpdateGunCtx(abm);

            if (Request.Files.Count > 0)
            {
                var f = Request.Files;
                var gid = Request["GroupId"];
                var oig = Request["OrigImg"];

                var arr = gid.Contains(",") ? gid.Split(',') : new[] { gid };
                var iar = oig.Contains(",") ? oig.Split(',') : new[] { oig };
                var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Guns);
                var nl = b14 ? arr.Length : 1; /* RULE: Not Posted on Web - 1 Pic MAX */

                for (int i = 0; i < nl; i++)
                {
                    var x = arr[i].Replace("ImgHse_", string.Empty);
                    var ix = Int32.TryParse(x, out i0) ? Convert.ToInt32(x) : i0;

                    var imgName = string.Format("{0}-{1}.jpg", folder, CookRandomStr(8));
                    UpdateSalePic(f[i], tm.InStockId, ix, imgName, iar[i], ImgSections.Guns, (PicFolders)i12);
                }
            }

            return Json(tm, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GunTagRestock()
        {
            var x0 = 0;
            var b0 = false;

            var v1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : x0;
            var v2 = Int32.TryParse(Request["Lo"], out x0) ? Convert.ToInt32(Request["Lo"]) : x0;
            var b1 = Boolean.TryParse(Request["Sa"], out b0) ? Convert.ToBoolean(Request["Sa"]) : b0;

            var ic = new InvContext();
            var d = ic.GetGunTagData(v1, v2, b0);

            return Json(d, JsonRequestBehavior.AllowGet);
        }


        #region Ammunition







        //[HttpPost]
        //public JsonResult GetAmmoSku(string tTypeId)
        //{
        //    var x0 = 0;
        //    var v1 = Int32.TryParse(tTypeId, out x0) ? Convert.ToInt32(tTypeId) : 0;


        //    var ic = new InvContext();
        //    var sg = ic.MakeAmmoSku(v1);

        //    return Json(new { sku = sg }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult SetSku()
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Request["catId"], out x0) ? Convert.ToInt32(Request["catId"]) : 0;
            var v2 = Int32.TryParse(Request["sctId"], out x0) ? Convert.ToInt32(Request["sctId"]) : 0;
            var v3 = Int32.TryParse(Request["trnTp"], out x0) ? Convert.ToInt32(Request["trnTp"]) : 0;
            var v4 = Int32.TryParse(Request["locId"], out x0) ? Convert.ToInt32(Request["locId"]) : 0;

            var ic = new InvContext();
            var sg = ic.CookSku(v1, v2, v3, v4);

            return Json(new { sku = sg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetRestockGunSku()
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Request["gunId"], out x0) ? Convert.ToInt32(Request["gunId"]) : 0;
            var v2 = Int32.TryParse(Request["ttpId"], out x0) ? Convert.ToInt32(Request["ttpId"]) : 0;

            var ic = new InvContext();
            var sg = ic.CookGunSkuFromId(v1, v2);

            return Json(new { sku = sg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddAmmo()
        {

            var x0 = 0;
            var b0 = false;
            double d0 = 0.00;
            var dt0 = DateTime.MinValue;

            var dt1 = DateTime.TryParse(Request["AcqDt"], out dt0) ? Convert.ToDateTime(Request["AcqDt"]) : DateTime.MinValue;

            var b1 = Boolean.TryParse(Request["AddWb"], out b0) ? Convert.ToBoolean(Request["AddWb"]) : b0;
            var b2 = Boolean.TryParse(Request["SClTx"], out b0) ? Convert.ToBoolean(Request["SClTx"]) : b0;
            var b3 = Boolean.TryParse(Request["iActv"], out b0) ? Convert.ToBoolean(Request["iActv"]) : b0;
            var b4 = Boolean.TryParse(Request["iSlug"], out b0) ? Convert.ToBoolean(Request["iSlug"]) : b0;
            var b5 = Boolean.TryParse(Request["iPptr"], out b0) ? Convert.ToBoolean(Request["iPptr"]) : b0;
            
            var i1 = Int32.TryParse(Request["SctId"], out x0) ? Convert.ToInt32(Request["SctId"]) : 0;
            var i2 = Int32.TryParse(Request["MfgId"], out x0) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var i3 = Int32.TryParse(Request["CalId"], out x0) ? Convert.ToInt32(Request["CalId"]) : 0;
            var i4 = Int32.TryParse(Request["CndId"], out x0) ? Convert.ToInt32(Request["CndId"]) : 0;
            var i5 = Int32.TryParse(Request["BulTp"], out x0) ? Convert.ToInt32(Request["BulTp"]) : 0;
            var i6 = Int32.TryParse(Request["GrWgt"], out x0) ? Convert.ToInt32(Request["GrWgt"]) : 0;
            var i7 = Int32.TryParse(Request["RdpBx"], out x0) ? Convert.ToInt32(Request["RdpBx"]) : 0;
            var i8 = Int32.TryParse(Request["UntCa"], out x0) ? Convert.ToInt32(Request["UntCa"]) : 0;
            var i9 = Int32.TryParse(Request["TtpId"], out x0) ? Convert.ToInt32(Request["TtpId"]) : 0;
            var i10 = Int32.TryParse(Request["UntWy"], out x0) ? Convert.ToInt32(Request["UntWy"]) : 0;
            var i11 = Int32.TryParse(Request["CusId"], out x0) ? Convert.ToInt32(Request["CusId"]) : 0;
            var i12 = Int32.TryParse(Request["LocId"], out x0) ? Convert.ToInt32(Request["LocId"]) : 0;
            var i13 = Int32.TryParse(Request["SupId"], out x0) ? Convert.ToInt32(Request["SupId"]) : 0;
            var i15 = Int32.TryParse(Request["FflCd"], out x0) ? Convert.ToInt32(Request["FflCd"]) : 0; // FIND A HOME FOR THIS
            var i16 = Int32.TryParse(Request["AcqSc"], out x0) ? Convert.ToInt32(Request["AcqSc"]) : 0; // AcqSourceID

            var d1 = Double.TryParse(Request["Ucost"], out d0) ? Convert.ToDouble(Request["Ucost"]) : 0;
            var d2 = Double.TryParse(Request["Frght"], out d0) ? Convert.ToDouble(Request["Frght"]) : 0;
            var d3 = Double.TryParse(Request["Ufees"], out d0) ? Convert.ToDouble(Request["Ufees"]) : 0;
            var d4 = Double.TryParse(Request["TaxAm"], out d0) ? Convert.ToDouble(Request["TaxAm"]) : 0;
            var d5 = Double.TryParse(Request["Price"], out d0) ? Convert.ToDouble(Request["Price"]) : 0;
            var d6 = Double.TryParse(Request["Chamb"], out d0) ? Convert.ToDouble(Request["Chamb"]) : 0;
            var d7 = Double.TryParse(Request["ShtSz"], out d0) ? Convert.ToDouble(Request["ShtSz"]) : 0;
            var d8 = Double.TryParse(Request["CusPd"], out d0) ? Convert.ToDouble(Request["CusPd"]) : 0;

            var v14 = Request["MfgPn"];
            var v15 = Request["UpcCd"];
            var v16 = Request["Descr"];
            var v18 = Request["Email"];
            var v19 = Request["UpcWb"];
            var v20 = Request["TgCus"];

            var uca = 0;
            var uwy = 0;
            var nca = 0;
            var nwy = 0;

            var fs = i9 < 103;

            if (fs)
            {
                uca = i8;
                uwy = i10;

                if (i9 == 101) { i11 = 0; v20 = string.Empty; } // Item placed for sale inventory, no customer assigned
            }
            else
            {
                nca = i8;
                nwy = i10;
            }

            var atm = new AcctModel(d1, d2, d3, d4, d5, d8, uca, uwy, nca, nwy, i11, i13, b2, v20); 
            var bbm = new BoundBookModel(i12, i9, i15, i16, v18, fs, dt1);  
            var amo = new AmmoModel(b1, b3, b5, i1, i2, i3, i4, i5, i6, i7, v14, v15, v19, v16, b4, d6, d7, atm, bbm);

            var ic = new InvContext();
            var tm = ic.AddAmmoItem(amo);

            if (Request.Files.Count > 0)
            {
                var f = Request.Files;
                var gid = Request["GroupId"];
                var oig = Request["OrigImg"];

                var arr = gid.Contains(",") ? gid.Split(',') : new[] { gid };
                var iar = oig.Contains(",") ? oig.Split(',') : new[] { oig };
                var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Ammo);
                var nl = b1 ? arr.Length : 1; /* RULE: Not Posted on Web - 1 Pic MAX */

                for (int i = 0; i < nl; i++)
                {
                    var x = arr[i].Replace("ImgHse_", string.Empty);
                    var ix = Int32.TryParse(x, out x0) ? Convert.ToInt32(x) : x0;

                    var imgName = string.Format("{0}-{1}.jpg", folder, CookRandomStr(8));
                    //UpdateInventoryPic(f[i], tm.InStockId, ix, imgName, iar[i], ImgSections.Ammo);
                    UpdateSalePic(f[i], tm.InStockId, ix, imgName, iar[i], ImgSections.Ammo, (PicFolders)i9);
                }
            }


            return Json(tm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateAmmoItem()
        {

            var x0 = 0;
            var b0 = false;
            double d0 = 0.00;
            DateTime dt0 = DateTime.MinValue;

            var im = Int32.TryParse(Request["Isi"], out x0) ? Convert.ToInt32(Request["Isi"]) : 0;
            var i1 = Int32.TryParse(Request["Cbi"], out x0) ? Convert.ToInt32(Request["Cbi"]) : 0;
            var i2 = Int32.TryParse(Request["Sct"], out x0) ? Convert.ToInt32(Request["Sct"]) : 0;
            var i3 = Int32.TryParse(Request["Mfg"], out x0) ? Convert.ToInt32(Request["Mfg"]) : 0;
            var i4 = Int32.TryParse(Request["Clb"], out x0) ? Convert.ToInt32(Request["Clb"]) : 0;
            var i5 = Int32.TryParse(Request["Btp"], out x0) ? Convert.ToInt32(Request["Btp"]) : 0;
            var i6 = Int32.TryParse(Request["Bwt"], out x0) ? Convert.ToInt32(Request["Bwt"]) : 0;
            var i7 = Int32.TryParse(Request["Rpb"], out x0) ? Convert.ToInt32(Request["Rpb"]) : 0;
            var i8 = Int32.TryParse(Request["Cnd"], out x0) ? Convert.ToInt32(Request["Cnd"]) : 0;
            var i9 = Int32.TryParse(Request["UCa"], out x0) ? Convert.ToInt32(Request["UCa"]) : 0;
            var i10 = Int32.TryParse(Request["Ttp"], out x0) ? Convert.ToInt32(Request["Ttp"]) : 0;
            var i11 = Int32.TryParse(Request["UWy"], out x0) ? Convert.ToInt32(Request["UWy"]) : 0;
            var i12 = Int32.TryParse(Request["Cus"], out x0) ? Convert.ToInt32(Request["Cus"]) : 0;
            var i13 = Int32.TryParse(Request["Loc"], out x0) ? Convert.ToInt32(Request["Loc"]) : 0;
            var i14 = Int32.TryParse(Request["Sup"], out x0) ? Convert.ToInt32(Request["Sup"]) : 0;
            var i15 = Int32.TryParse(Request["Fcd"], out x0) ? Convert.ToInt32(Request["Fcd"]) : 0;
            var i16 = Int32.TryParse(Request["Acq"], out x0) ? Convert.ToInt32(Request["Acq"]) : 0;

            var d1 = Double.TryParse(Request["Cos"], out d0) ? Convert.ToDouble(Request["Cos"]) : 0;
            var d2 = Double.TryParse(Request["Frt"], out d0) ? Convert.ToDouble(Request["Frt"]) : 0;
            var d3 = Double.TryParse(Request["Fee"], out d0) ? Convert.ToDouble(Request["Fee"]) : 0;
            var d4 = Double.TryParse(Request["Txc"], out d0) ? Convert.ToDouble(Request["Txc"]) : 0;
            var d5 = Double.TryParse(Request["Shz"], out d0) ? Convert.ToDouble(Request["Shz"]) : 0;
            var d6 = Double.TryParse(Request["Prc"], out d0) ? Convert.ToDouble(Request["Prc"]) : 0;
            var d7 = Double.TryParse(Request["Chb"], out d0) ? Convert.ToDouble(Request["Chb"]) : 0;
            var d8 = Double.TryParse(Request["Msr"], out d0) ? Convert.ToDouble(Request["Msr"]) : 0;
            var d9 = Double.TryParse(Request["Cpd"], out d0) ? Convert.ToDouble(Request["Cpd"]) : 0;

            var b1 = Boolean.TryParse(Request["Web"], out b0) ? Convert.ToBoolean(Request["Web"]) : b0;
            var b2 = Boolean.TryParse(Request["Slg"], out b0) ? Convert.ToBoolean(Request["Slg"]) : b0;
            var b3 = Boolean.TryParse(Request["Sgt"], out b0) ? Convert.ToBoolean(Request["Sgt"]) : b0;
            var b4 = Boolean.TryParse(Request["Atv"], out b0) ? Convert.ToBoolean(Request["Atv"]) : b0;
            var b5 = Boolean.TryParse(Request["Ipt"], out b0) ? Convert.ToBoolean(Request["Ipt"]) : b0;

            var dt1 = DateTime.TryParse(Request["Adt"], out dt0) ? Convert.ToDateTime(Request["Adt"]) : dt0;

 
            var v8 = Request["Mpn"];
            var v9 = Request["Dsc"];
            var v10 = Request["Upc"];
            var v12 = Request["Eml"];
            var v15 = Request["Wup"];
            var v16 = Request["TgCus"];

            var uca = 0;
            var uwy = 0;
            var nca = 0;
            var nwy = 0;

            var fs = i10 < 103;

            if (fs)
            {
                uca = i9;
                uwy = i11;

                if (i10 == 101) { i12 = 0; v16 = string.Empty; } // Item placed for sale inventory, no customer assigned
            }
            else
            {
                nca = i9;
                nwy = i11;
            }


            var atm = new AcctModel(d9, d1, d2, d3, d8, d4, b3, d6, uca, uwy, nca, nwy, i12, i14, v16);
            var bbm = new BoundBookModel(dt1, i10, i13, i16, i15, v12, fs);

            var am = new AmmoModel(i1, i2, i3, i4, i5, i6, i7, i8, d5, d7, b1, b4, b2, b5, v8, v9, v10, v15, atm, bbm);

            if (!b1) /* Clean Up Excess Web Images */
            {
                FlushWebPics(SiteImageSections.Ammunition, im);
            }


            var ic = new InvContext();
            var tm = ic.UpdateAmmoItem(am);

            if (Request.Files.Count > 0)
            {
                var f = Request.Files;
                var gid = Request["GroupId"];
                var oig = Request["OrigImg"];

                var arr = gid.Contains(",") ? gid.Split(',') : new[] { gid };
                var iar = oig.Contains(",") ? oig.Split(',') : new[] { oig };
                var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Ammo);
                var nl = b1 ? arr.Length : 1; /* RULE: Not Posted on Web - 1 Pic MAX */

                for (int i = 0; i < nl; i++)
                {
                    var x = arr[i].Replace("ImgHse_", string.Empty);
                    var ix = Int32.TryParse(x, out x0) ? Convert.ToInt32(x) : x0;
                    var imgName = string.Format("{0}-{1}.jpg", folder, CookRandomStr(8));
                    UpdateInventoryPic(f[i], im, ix, imgName, iar[i], ImgSections.Ammo);
                    UpdateSalePic(f[i], tm.InStockId, ix, imgName, iar[i], ImgSections.Ammo, (PicFolders)i10);
                }
            }

            return Json(tm, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult RestockAmmo()
        {

            var x0 = 0;
            var b0 = false;
            double d0 = 0.00;
            DateTime dt0 = DateTime.MinValue;

            var dt1 = Request["AqDat"].Length > 9 ? Convert.ToDateTime(Request["AqDat"]) : dt0;

            var i1 = Int32.TryParse(Request["TtpId"], out x0) ? Convert.ToInt32(Request["TtpId"]) : 0;
            var i2 = Int32.TryParse(Request["InStk"], out x0) ? Convert.ToInt32(Request["InStk"]) : 0;
            var i3 = Int32.TryParse(Request["UntCa"], out x0) ? Convert.ToInt32(Request["UntCa"]) : 0;
            var i4 = Int32.TryParse(Request["UntWy"], out x0) ? Convert.ToInt32(Request["UntWy"]) : 0;
            var i5 = Int32.TryParse(Request["CusId"], out x0) ? Convert.ToInt32(Request["CusId"]) : 0;
            var i6 = Int32.TryParse(Request["LocId"], out x0) ? Convert.ToInt32(Request["LocId"]) : 0;
            var i7 = Int32.TryParse(Request["SupId"], out x0) ? Convert.ToInt32(Request["SupId"]) : 0;
            var i8 = Int32.TryParse(Request["FflCd"], out x0) ? Convert.ToInt32(Request["FflCd"]) : 0;
            var i9 = Int32.TryParse(Request["AcqTp"], out x0) ? Convert.ToInt32(Request["AcqTp"]) : 0;

            var d1 = Double.TryParse(Request["CostA"], out d0) ? Convert.ToDouble(Request["CostA"]) : 0;
            var d2 = Double.TryParse(Request["FrgtA"], out d0) ? Convert.ToDouble(Request["FrgtA"]) : 0;
            var d3 = Double.TryParse(Request["FeesA"], out d0) ? Convert.ToDouble(Request["FeesA"]) : 0;
            var d4 = Double.TryParse(Request["TaxAm"], out d0) ? Convert.ToDouble(Request["TaxAm"]) : 0;
            var d5 = Double.TryParse(Request["CusPd"], out d0) ? Convert.ToDouble(Request["CusPd"]) : 0;

            var b1 = Boolean.TryParse(Request["GotTx"], out b0) ? Convert.ToBoolean(Request["GotTx"]) : b0;
            var b3 = Boolean.TryParse(Request["IsOwb"], out b0) ? Convert.ToBoolean(Request["IsOwb"]) : b0;
            var b4 = Boolean.TryParse(Request["CaPpt"], out b0) ? Convert.ToBoolean(Request["CaPpt"]) : b0;

 
            var v12 = Request["TgCus"];
            var v14 = Request["Email"];

            var uca = 0;
            var uwy = 0;
            var nca = 0;
            var nwy = 0;

            var fs = i1 < 103;

            if (fs)
            {
                uca = i3;
                uwy = i4;

                if (i9 == 101) { i5 = 0; v12 = string.Empty; } // Item placed for sale inventory, no customer assigned
            }
            else
            {
                nca = i3;
                nwy = i4;
            }

            dt1 = dt1 == DateTime.MinValue ? DateTime.Today : dt1;

            var atm = new AcctModel(d1, d2, d3, d4, d5, uca, uwy, nca, nwy, i5, i7, b1, v12);
            var bbm = new BoundBookModel(i6, i1, i8, i9, v14, fs, dt1);

            var amo = new AmmoModel(i2, b3, b4, atm, bbm);
 
            var ic = new InvContext();
            var tm = ic.RestockAmmoCtx(amo);

            return Json(tm, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult GetAmmo()
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Request["MfgId"], out x0) ? Convert.ToInt32(Request["MfgId"]) : x0;
            var v2 = Int32.TryParse(Request["CalId"], out x0) ? Convert.ToInt32(Request["CalId"]) : x0;

            var ic = new InvContext();
            var ag = ic.GetAmmoGrid(v1, v2);

            return Json(ag, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult NixAmmo()
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : x0;

            var ic = new InvContext();
            ic.DeleteAmmoItem(v1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetAmmoProduct()
        {
            var x0 = 0;
            var b0 = false;

            var v1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : x0;
            var b1 = Boolean.TryParse(Request["Sa"], out b0) ? Convert.ToBoolean(Request["Sa"]) : b0;

            var ic = new InvContext();
            var a = ic.GetAmmoItem(v1, b1);

            return Json(a, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult InStockMenu(string itemId, string catId)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(itemId, out x0) ? Convert.ToInt32(itemId) : x0;
            var v2 = Int32.TryParse(catId, out x0) ? Convert.ToInt32(catId) : x0;

            var ic = new InvContext();
            var a = ic.GetInStockMenu(v1, v2); 

            return Json(a, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult GunsInStockMenu(string gunId)
        //{
        //    var x0 = 0;
        //    var v1 = Int32.TryParse(gunId, out x0) ? Convert.ToInt32(gunId) : x0;

        //    var ic = new InvContext();
        //    var a = ic.GetGunsInStockMenu(v1);

        //    return Json(a, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult AmmoInStockMenu(string ammoId)
        //{
        //    var x0 = 0;
        //    var v1 = Int32.TryParse(ammoId, out x0) ? Convert.ToInt32(ammoId) : x0;

        //    var ic = new InvContext();
        //    var a = ic.GetAmmoInStockMenu(v1);

        //    return Json(a, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult MerchInStockMenu(string merchId)
        //{
        //    var x0 = 0;
        //    var v1 = Int32.TryParse(merchId, out x0) ? Convert.ToInt32(merchId) : x0;

        //    var ic = new InvContext();
        //    var a = ic.GetMerchInStockMenu(v1);

        //    return Json(a, JsonRequestBehavior.AllowGet);
        //}



        [HttpPost]
        public JsonResult GetAmmoInvById()
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : x0;

            var ic = new InvContext();
            var ai = ic.GetAmmoInventoryItem(v1);

            return Json(ai, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AmmoTagRestock()
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Request["Aid"], out x0) ? Convert.ToInt32(Request["Aid"]) : x0;

            var ic = new InvContext();
            var ai = ic.GetAmmoTagData(v1);

            return Json(ai, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Merchandise

        [HttpPost]
        public JsonResult MerchTagRestock()
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Request["Mid"], out x0) ? Convert.ToInt32(Request["Mid"]) : x0;

            var ic = new InvContext();
            var ai = ic.GetMerchTagData(v1);

            return Json(ai, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public JsonResult GetMerchSku(string tTypeId)
        //{
        //    var x0 = 0;
        //    var v1 = Int32.TryParse(tTypeId, out x0) ? Convert.ToInt32(tTypeId) : 0;


        //    var ic = new InvContext();
        //    var sg = ic.MakeMerchSku(v1);

        //    return Json(new { sku = sg }, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public JsonResult GetMerchById()
        {
            var x0 = 0;
            var b0 = false;

            var v1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : x0;
            var b1 = Boolean.TryParse(Request["Sa"], out b0) ? Convert.ToBoolean(Request["Sa"]) : b0;

            var ic = new InvContext();
            var ai = ic.GetMerchItem(v1, b1);

            return Json(ai, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult GetMerchAcct()
        //{
        //    var x0 = 0;
        //    var v1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : x0;

        //    var ic = new InvContext();
        //    var ai = ic.GetMerchAccounting(v1);

        //    return Json(ai, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public JsonResult GetMerchandise()
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Request["MfgId"], out x0) ? Convert.ToInt32(Request["MfgId"]) : x0;
            var v2 = Int32.TryParse(Request["CatId"], out x0) ? Convert.ToInt32(Request["CatId"]) : x0;

            var ic = new InvContext();
            var ag = ic.GetMerchGrid(v1, v2);

            return Json(ag, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetGuns()
        {

            var x0 = 0;
            var i1 = Int32.TryParse(Request["Loc"], out x0) ? Convert.ToInt32(Request["Loc"]) : -1;
            var i2 = Int32.TryParse(Request["Mfg"], out x0) ? Convert.ToInt32(Request["Mfg"]) : x0;
            var i3 = Int32.TryParse(Request["Ttp"], out x0) ? Convert.ToInt32(Request["Ttp"]) : x0;
            var i4 = Int32.TryParse(Request["Gtp"], out x0) ? Convert.ToInt32(Request["Gtp"]) : x0;
            var i5 = Int32.TryParse(Request["Cus"], out x0) ? Convert.ToInt32(Request["Cus"]) : x0;

            var ns = new InStockModel(i1, i2, i3, i4, i5);
 
            var ic = new InvContext();
            var ag = ic.GetGunGrid(ns);

            return Json(ag, JsonRequestBehavior.AllowGet);
        }

 


        [HttpPost]
        public JsonResult AddMerchandise()
        {
            var i0 = 0;
            var b0 = false;
            double d0 = 0.00;
            DateTime dt0 = DateTime.MinValue;

            var i1 = Int32.TryParse(Request["Sct"], out i0) ? Convert.ToInt32(Request["Sct"]) : i0;
            var i2 = Int32.TryParse(Request["Mid"], out i0) ? Convert.ToInt32(Request["Mid"]) : i0;
            var i3 = Int32.TryParse(Request["Cnd"], out i0) ? Convert.ToInt32(Request["Cnd"]) : i0;
            var i4 = Int32.TryParse(Request["Col"], out i0) ? Convert.ToInt32(Request["Col"]) : i0;
            var i5 = Int32.TryParse(Request["Box"], out i0) ? Convert.ToInt32(Request["Box"]) : i0;
            var i6 = Int32.TryParse(Request["Ipb"], out i0) ? Convert.ToInt32(Request["Ipb"]) : i0;
            var i7 = Int32.TryParse(Request["Lbs"], out i0) ? Convert.ToInt32(Request["Lbs"]) : i0;
            var i9 = Int32.TryParse(Request["Uca"], out i0) ? Convert.ToInt32(Request["Uca"]) : i0;
            var i10 = Int32.TryParse(Request["Uwy"], out i0) ? Convert.ToInt32(Request["Uwy"]) : i0;
            var i11 = Int32.TryParse(Request["Ttp"], out i0) ? Convert.ToInt32(Request["Ttp"]) : i0;
            var i12 = Int32.TryParse(Request["CusId"], out i0) ? Convert.ToInt32(Request["CusId"]) : i0;
            var i13 = Int32.TryParse(Request["LocId"], out i0) ? Convert.ToInt32(Request["LocId"]) : i0;

            var i14 = Int32.TryParse(Request["SupId"], out i0) ? Convert.ToInt32(Request["SupId"]) : i0;
            var i15 = Int32.TryParse(Request["FflCd"], out i0) ? Convert.ToInt32(Request["FflCd"]) : i0;
            var i16 = Int32.TryParse(Request["Acq"], out i0) ? Convert.ToInt32(Request["Acq"]) : i0;

            var d1 = Double.TryParse(Request["Cst"], out d0) ? Convert.ToDouble(Request["Cst"]) : d0;
            var d2 = Double.TryParse(Request["Frt"], out d0) ? Convert.ToDouble(Request["Frt"]) : d0;
            var d3 = Double.TryParse(Request["Fee"], out d0) ? Convert.ToDouble(Request["Fee"]) : d0;
            var d4 = Double.TryParse(Request["Tcl"], out d0) ? Convert.ToDouble(Request["Tcl"]) : d0;
            var d5 = Double.TryParse(Request["Prc"], out d0) ? Convert.ToDouble(Request["Prc"]) : d0;
            var d6 = Double.TryParse(Request["Ozs"], out d0) ? Convert.ToDouble(Request["Ozs"]) : d0;
            var d7 = Double.TryParse(Request["CusPd"], out d0) ? Convert.ToDouble(Request["CusPd"]) : d0;
            var d8 = Double.TryParse(Request["Msr"], out d0) ? Convert.ToDouble(Request["Msr"]) : d0;


            var dt1 = DateTime.TryParse(Request["Adt"], out dt0) ? Convert.ToDateTime(Request["Adt"]) : dt0;

            var b1 = Boolean.TryParse(Request["Cok"], out b0) ? Convert.ToBoolean(Request["Cok"]) : b0;
            var b2 = Boolean.TryParse(Request["Owb"], out b0) ? Convert.ToBoolean(Request["Owb"]) : b0;
            var b3 = Boolean.TryParse(Request["Sgt"], out b0) ? Convert.ToBoolean(Request["Sgt"]) : b0;
            var b4 = Boolean.TryParse(Request["Atv"], out b0) ? Convert.ToBoolean(Request["Atv"]) : b0;

            var v1 = Request["Eml"];
            var v2 = Request["Upc"];
            var v3 = Request["Mpn"];
            var v4 = Request["Des"];
            var v5 = Request["Lds"];
            var v6 = Request["Mdl"];
            var v10 = Request["Wup"];
            var v11 = Request["Cus"];
            
            var uca = 0;
            var uwy = 0;
            var nca = 0;
            var nwy = 0;

            var fs = i11 < 103;

            if (fs)
            {
                uca = i9;
                uwy = i10;

                if (i11==101) { i12 = 0; v11 = string.Empty; } // Item placed for sale inventory, no customer assigned
            }
            else
            {
                nca = i9;
                nwy = i10;
            }

            var am = new AcctModel(d1, d2, d3, d8, d4, d5, d7, uca, uwy, nca, nwy, i12, i14, b3, v11);
            var bm = new BoundBookModel(dt1, i11, i13, i16, i15, v1, fs);

            var mi = new MerchandiseModel(i1, i2, i3, i4, i5, i6, i7, v2, v10, v3, v4, v5, v6, d6, b1, b2, b4, am, bm);

            var ic = new InvContext();
            var tm = ic.AddMerchandiseItem(mi);


            if (Request.Files.Count > 0)
            {
                var f = Request.Files;
                var gid = Request["GroupId"];
                var oig = Request["OrigImg"];

                var arr = gid.Contains(",") ? gid.Split(',') : new[] { gid };
                var iar = oig.Contains(",") ? oig.Split(',') : new[] { oig };
                var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Merchandise);
                var nl = b2 ? arr.Length : 1; /* RULE: Not Posted on Web - 1 Pic MAX */

                for (int i = 0; i < nl; i++)
                {
                    var x = arr[i].Replace("ImgHse_", string.Empty);
                    var ix = Int32.TryParse(x, out i0) ? Convert.ToInt32(x) : i0;

                    var imgName = string.Format("{0}-{1}.jpg", folder, CookRandomStr(8));
                    //UpdateInventoryPic(f[i], tm.InStockId, ix, imgName, iar[i], ImgSections.Merchandise);
                    UpdateSalePic(f[i], tm.InStockId, ix, imgName, iar[i], ImgSections.Merchandise, (PicFolders)i11);
                }
            }

            return Json(tm, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateMerchItem()
        {

            var i0 = 0;
            var b0 = false;
            double d0 = 0.00;
            DateTime dt0 = DateTime.MinValue;

            var im = Int32.TryParse(Request["Isi"], out i0) ? Convert.ToInt32(Request["Isi"]) : i0;
            var i1 = Int32.TryParse(Request["Cbi"], out i0) ? Convert.ToInt32(Request["Cbi"]) : i0;
            var i2 = Int32.TryParse(Request["Sct"], out i0) ? Convert.ToInt32(Request["Sct"]) : i0;
            var i3 = Int32.TryParse(Request["Mfg"], out i0) ? Convert.ToInt32(Request["Mfg"]) : i0;
            var i4 = Int32.TryParse(Request["Cnd"], out i0) ? Convert.ToInt32(Request["Cnd"]) : i0;
            var i5 = Int32.TryParse(Request["Col"], out i0) ? Convert.ToInt32(Request["Col"]) : i0;
            var i6 = Int32.TryParse(Request["Box"], out i0) ? Convert.ToInt32(Request["Box"]) : i0;
            var i7 = Int32.TryParse(Request["Ipb"], out i0) ? Convert.ToInt32(Request["Ipb"]) : i0;
            var i8 = Int32.TryParse(Request["Lbs"], out i0) ? Convert.ToInt32(Request["Lbs"]) : i0;
            var i10 = Int32.TryParse(Request["Uca"], out i0) ? Convert.ToInt32(Request["Uca"]) : i0;
            var i11 = Int32.TryParse(Request["Uwy"], out i0) ? Convert.ToInt32(Request["Uwy"]) : i0;
            var i12 = Int32.TryParse(Request["Ttp"], out i0) ? Convert.ToInt32(Request["Ttp"]) : i0;

            var i13 = Int32.TryParse(Request["CusId"], out i0) ? Convert.ToInt32(Request["CusId"]) : i0;
            var i14 = Int32.TryParse(Request["LocId"], out i0) ? Convert.ToInt32(Request["LocId"]) : i0;
            var i15 = Int32.TryParse(Request["SupId"], out i0) ? Convert.ToInt32(Request["SupId"]) : i0;
            var i16 = Int32.TryParse(Request["FflCd"], out i0) ? Convert.ToInt32(Request["FflCd"]) : i0;
            var i17 = Int32.TryParse(Request["AcqTp"], out i0) ? Convert.ToInt32(Request["AcqTp"]) : i0;

            var d1 = Double.TryParse(Request["Cst"], out d0) ? Convert.ToDouble(Request["Cst"]) : d0;
            var d2 = Double.TryParse(Request["Frt"], out d0) ? Convert.ToDouble(Request["Frt"]) : d0;
            var d3 = Double.TryParse(Request["Fee"], out d0) ? Convert.ToDouble(Request["Fee"]) : d0;
            var d4 = Double.TryParse(Request["Tcl"], out d0) ? Convert.ToDouble(Request["Tcl"]) : d0;
            var d5 = Double.TryParse(Request["Prc"], out d0) ? Convert.ToDouble(Request["Prc"]) : d0;
            var d6 = Double.TryParse(Request["Ozs"], out d0) ? Convert.ToDouble(Request["Ozs"]) : d0;
            var d7 = Double.TryParse(Request["CusPd"], out d0) ? Convert.ToDouble(Request["CusPd"]) : d0;
            var d8 = Double.TryParse(Request["Msr"], out d0) ? Convert.ToDouble(Request["Msr"]) : d0;

            var b1 = Boolean.TryParse(Request["Cok"], out b0) ? Convert.ToBoolean(Request["Cok"]) : b0;
            var b2 = Boolean.TryParse(Request["Owb"], out b0) ? Convert.ToBoolean(Request["Owb"]) : b0;
            var b3 = Boolean.TryParse(Request["Sgt"], out b0) ? Convert.ToBoolean(Request["Sgt"]) : b0;
            var b4 = Boolean.TryParse(Request["Atv"], out b0) ? Convert.ToBoolean(Request["Atv"]) : b0;

            var dt1 = DateTime.TryParse(Request["Adt"], out dt0) ? Convert.ToDateTime(Request["Adt"]) : dt0;

            var v1 = Request["Sku"];
            var v2 = Request["Upc"];
            var v3 = Request["Mpn"];
            var v4 = Request["Des"];
            var v5 = Request["Lds"];
            var v6 = Request["Mdl"];
            var v7 = Request["Cus"];
            var v8 = Request["Eml"];
            var v10 = Request["Wsu"];

            var uca = 0;
            var uwy = 0;
            var nca = 0;
            var nwy = 0;

            var fs = i12 < 103;

            if (fs)
            {
                uca = i10;
                uwy = i11;

                if (i12 == 101) { i13 = 0; v7 = string.Empty; } // Item placed for sale inventory, no customer assigned
            }
            else
            {
                nca = i10;
                nwy = i11;
            }

            var am = new AcctModel(d1, d2, d3, d8, d4, d5, d7, uca, uwy, nca, nwy, i13, i15, b3, v7);
            var bm = new BoundBookModel(v1, v8, i12, i14, i16, i17, dt1, fs);
            var mi = new MerchandiseModel(i1, i2, i3, i4, i5, i6, i7, i8, v1, v2, v10, v3, v4, v5, v6, d6, b1, b2, b4, am, bm);

            if (!b2) /* Clean Up Excess Web Images */
            {
                FlushWebPics(SiteImageSections.Merchandise, im);
            }


            var ic = new InvContext();
            var tm = ic.UpdateMerchandiseItem(mi);

            if (Request.Files.Count > 0)
            {
                var f = Request.Files;
                var gid = Request["GroupId"];
                var oig = Request["OrigImg"];

                var arr = gid.Contains(",") ? gid.Split(',') : new[] { gid };
                var iar = oig.Contains(",") ? oig.Split(',') : new[] { oig };
                var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Merchandise);
                var nl = b2 ? arr.Length : 1; /* RULE: Not Posted on Web - 1 Pic MAX */

                for (int i = 0; i < nl; i++)
                {
                    var x = arr[i].Replace("ImgHse_", string.Empty);
                    var ix = Int32.TryParse(x, out i0) ? Convert.ToInt32(x) : i0;
                    var imgName = string.Format("{0}-{1}.jpg", folder, CookRandomStr(8));
                    UpdateSalePic(f[i], tm.InStockId, ix, imgName, iar[i], ImgSections.Merchandise, (PicFolders)i12);
                }
            }

            return Json(tm, JsonRequestBehavior.AllowGet);
        }

        #region Shared

        [HttpPost]
        public JsonResult NixItem()
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : x0;

            var ic = new InvContext();
            ic.DeleteInStock(v1);

            ///* SCRUB IMAGES */
            //var bPath = ConfigurationHelper.GetPropertyValue("application", "PathSiteImgs");
            //for (var i = 1; i < 7; i++)
            //{
            //    var fName = string.Format("Merch-{0}-{1}.jpg", v1, i);
            //    var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Merchandise);
            //    var imgPath = bPath + folder + "\\" + fName;
            //    if (System.IO.File.Exists(imgPath)) { System.IO.File.Delete(imgPath); }
            //}


            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        #endregion


        [HttpPost]
        public JsonResult RestockMerch()
        {

            var i0 = 0;
            var b0 = false;
            double d0 = 0.00;
            DateTime dt0 = DateTime.MinValue;

            var i1 = Int32.TryParse(Request["Mid"], out i0) ? Convert.ToInt32(Request["Mid"]) : i0;
            var i2 = Int32.TryParse(Request["Ttp"], out i0) ? Convert.ToInt32(Request["Ttp"]) : i0;
            var i3 = Int32.TryParse(Request["Uca"], out i0) ? Convert.ToInt32(Request["Uca"]) : i0;
            var i4 = Int32.TryParse(Request["UWy"], out i0) ? Convert.ToInt32(Request["UWy"]) : i0;
            var i5 = Int32.TryParse(Request["Cid"], out i0) ? Convert.ToInt32(Request["Cid"]) : i0;
            var i6 = Int32.TryParse(Request["Loc"], out i0) ? Convert.ToInt32(Request["Loc"]) : i0;
            var i7 = Int32.TryParse(Request["Sid"], out i0) ? Convert.ToInt32(Request["Sid"]) : i0;
            var i8 = Int32.TryParse(Request["Fcd"], out i0) ? Convert.ToInt32(Request["Fcd"]) : i0;
            var i9 = Int32.TryParse(Request["Acq"], out i0) ? Convert.ToInt32(Request["Acq"]) : i0;

            var d1 = Double.TryParse(Request["Cst"], out d0) ? Convert.ToDouble(Request["Cst"]) : d0;
            var d2 = Double.TryParse(Request["Frt"], out d0) ? Convert.ToDouble(Request["Frt"]) : d0;
            var d3 = Double.TryParse(Request["Fee"], out d0) ? Convert.ToDouble(Request["Fee"]) : d0;
            var d4 = Double.TryParse(Request["Tax"], out d0) ? Convert.ToDouble(Request["Tax"]) : d0;
            var d5 = Double.TryParse(Request["Cpd"], out d0) ? Convert.ToDouble(Request["Cpd"]) : d0;

            var b1 = Boolean.TryParse(Request["Sgt"], out b0) ? Convert.ToBoolean(Request["Sgt"]) : b0;
            var b2 = Boolean.TryParse(Request["Iow"], out b0) ? Convert.ToBoolean(Request["Iow"]) : b0;

            var dt1 = DateTime.TryParse(Request["Adt"], out dt0) ? Convert.ToDateTime(Request["Adt"]) : dt0;

            var v1 = Request["Cus"];
            var v2 = Request["Eml"];

            var uca = 0;
            var uwy = 0;
            var nca = 0;
            var nwy = 0;

            var fs = i2 < 103;

            if (fs)
            {
                uca = i3;
                uwy = i4;

                if (i2 == 101) { i5 = 0; v1 = string.Empty; } // Item placed for sale inventory, no customer assigned
            }
            else
            {
                nca = i3;
                nwy = i4;
            }


            var am = new AcctModel(v1, d1, d2, d3, d4, d5, uca, uwy, nca, nwy, i5, i7, b1);
            var bm = new BoundBookModel(dt1, i8, i9, v2, fs);
            var mi = new MerchandiseModel(i1, i2, i6, b2, am, bm);

            var ic = new InvContext();
            var tm = ic.RestockMerchandise(mi);

            return Json(tm, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult GetMerchandiseSku(string tTypeId)
        //{
        //    var x0 = 0;
        //    var v1 = Int32.TryParse(tTypeId, out x0) ? Convert.ToInt32(tTypeId) : 0;


        //    var ic = new InvContext();
        //    var sg = ic.MakeMerchSku(v1);

        //    return Json(new { sku = sg }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult NixAmmoImage()
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : 0;
            var v2 = Request["Img"];

            var m = new BaseModel();
            var pa = ConfigurationHelper.GetPropertyValue("application", "p9");
            var ph = ConfigurationHelper.GetPropertyValue("application", "p12");


            var fn = Path.GetFileName(v2);

            if (fn.Contains("?"))
            {
                var f = fn.Split('?')[0];
                fn = f;
            }

            var fP1 = m.DecryptIt(pa) + "\\" + fn;
            var fP2 = m.DecryptIt(ph) + "\\" + fn;

            System.IO.File.Delete(fP1);
            System.IO.File.Delete(fP2);
            var ic = new InvContext();
            ic.ClearAmmoPic(v1);


            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NixImage()
        {
            var x0 = 0;

            //var v1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : 0;
            //var v2 = Int32.TryParse(Request["Idx"], out x0) ? Convert.ToInt32(Request["Idx"]) : 0;
            //var v3 = Int32.TryParse(Request["Cat"], out x0) ? Convert.ToInt32(Request["Cat"]) : 0; //Category to update
            //var v5 = Int32.TryParse(Request["Ttp"], out x0) ? Convert.ToInt32(Request["Ttp"]) : 0; //TransType
            //var v4 = Request["Img"];

            var i1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : 0;
            var i2 = Int32.TryParse(Request["Idx"], out x0) ? Convert.ToInt32(Request["Idx"]) : 0;
            var i3 = Int32.TryParse(Request["Cat"], out x0) ? Convert.ToInt32(Request["Cat"]) : 0; //Category to update
            var i4 = Int32.TryParse(Request["Ttp"], out x0) ? Convert.ToInt32(Request["Ttp"]) : 0; //TransType
            var v1 = Request["Img"];

            var igf = string.Format("s{0}", (int)i4);
            var b = ConfigurationHelper.GetPropertyValue("application", "s1");
            var f = ConfigurationHelper.GetPropertyValue("application", igf);
            var h = ConfigurationHelper.GetPropertyValue("application", "p12");

            var ttp = Enum.GetName(typeof(PicFolders), i4);
            var cat = Enum.GetName(typeof(ItemCategories), i3);

            var m = new BaseModel();
            var bTmp = m.DecryptIt(b);
            var bDir = m.DecryptIt(f);
            var bWeb = m.DecryptIt(h);

            // Delete Temp File
            var tPath = String.Format("{0}\\{1}\\T\\{2}", bTmp, bDir, v1);
            if (System.IO.File.Exists(tPath)) { System.IO.File.Delete(tPath); }

            //Delete Image File
            var iPath = String.Format("{0}\\{1}\\{2}\\{3}", bTmp, bDir, cat, v1);
            if (System.IO.File.Exists(iPath)) { System.IO.File.Delete(iPath); }

            var s = (SiteImageSections)i3;
            //var pp = string.Empty;

            //switch (s)
            //{
            //    case SiteImageSections.Guns:
            //        pp = ConfigurationHelper.GetPropertyValue("application", "p8");
            //        break;
            //    case SiteImageSections.Ammunition:
            //        pp = ConfigurationHelper.GetPropertyValue("application", "p9");
            //        break;
            //    case SiteImageSections.Merchandise:
            //            pp = ConfigurationHelper.GetPropertyValue("application", "p10");
            //            break;
            //}



            //if (i2 == 1) //cover image. Delete if on website - KEEP THIS
            //{
            //    var wPath = string.Format("{0}\\{1}", bWeb, v1);

            //    if (System.IO.File.Exists(wPath)) { System.IO.File.Delete(wPath); }
            //}




            //var img1 = ig1.Length > 0 ? string.Format("{0}/{1}/{2}/{3}/{4}?{5}", GetHostUrl(), DecryptIt(BPathDir), ttp, cat, ig1, t) : String.Empty;



            //var bDir = m.DecryptIt(pp);
            //var mPath = string.Format("{0}\\{1}", bDir, v4);
            //if (System.IO.File.Exists(mPath)) { System.IO.File.Delete(mPath); }

            var ic = new InvContext();
            var im = ic.ClearPic(i1, i2, s);

            //if (!b1)
            //{
            //    var iml = new List<string>();
            //    iml.Add(im.Io1);
            //    iml.Add(im.Io2);
            //    iml.Add(im.Io3);
            //    iml.Add(im.Io4);
            //    iml.Add(im.Io5);
            //    iml.Add(im.Io6);
            //    ClearWebMerchPics(iml);
            //}

            return Json(im, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult NixMerchandise()
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Request["MstId"], out x0) ? Convert.ToInt32(Request["MstId"]) : x0;

            var ic = new InvContext();
            ic.DeleteMerchandiseItem(v1);

            /* SCRUB IMAGES */
            var bPath = ConfigurationHelper.GetPropertyValue("application", "PathSiteImgs");
            for (var i = 1; i < 7; i++)
            {
                var fName = string.Format("Merch-{0}-{1}.jpg", v1, i);
                var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Merchandise);
                var imgPath = bPath + folder + "\\" + fName;
                if (System.IO.File.Exists(imgPath)) { System.IO.File.Delete(imgPath); }
            }


            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        #endregion


        [HttpPost]
        public JsonResult FindExistingGun(string mfg, string typ, string cal, string atn, string cok, string str)
        {
            if (str.Length < 2)
            {
                return Json("string too short", JsonRequestBehavior.AllowGet);
            }

            var x0 = 0;
            var v1 = Int32.TryParse(mfg, out x0) ? Convert.ToInt32(mfg) : 0;
            var v2 = Int32.TryParse(typ, out x0) ? Convert.ToInt32(typ) : 0;
            var v3 = Int32.TryParse(cal, out x0) ? Convert.ToInt32(cal) : 0;
            var v4 = Int32.TryParse(atn, out x0) ? Convert.ToInt32(atn) : 0;
            var v5 = Int32.TryParse(cok, out x0) ? Convert.ToInt32(cok) : -1;

            var gm = new GunModel(v1, v2, v3, v4, v5, str);
            var ic = new InvContext();
            var g = ic.CheckForExistingGun(gm);

            return Json(g, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetGunSpecs(string Mid, string Isi, string Iow)
        {
            var x0 = 0;
            var b0 = false;

            var i1 = Int32.TryParse(Mid, out x0) ? Convert.ToInt32(Mid) : 0;
            var i2 = Int32.TryParse(Isi, out x0) ? Convert.ToInt32(Isi) : 0;
            var b1 = Boolean.TryParse(Iow, out b0) ? Convert.ToBoolean(Iow) : b0;

            var ic = new InvContext();
            var g = ic.GetGunSpecsWeb(i1, i2, b1);

            return Json(g, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetGunById()
        {
            var x0 = 0;
            var b0 = false;
            var v1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : x0;
            var b1 = Boolean.TryParse(Request["Sa"], out b0) ? Convert.ToBoolean(Request["Sa"]) : b0;

            var ic = new InvContext();
            var g = ic.GetGunItem(v1, b1);

            return Json(g, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult AddGunLockMaker(string newmfg)
        {
            var ic = new InvContext();
            var am = ic.AddGunLockMfg(newmfg);

            return Json(am, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetGunLockModels(string lockMfgId)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(lockMfgId, out x0) ? Convert.ToInt32(lockMfgId) : 0;

            var mm = new MenuModel();
            var lm = mm.GetGunLockModels(v1);

            return Json(lm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddGunLockModel(string lockMfgId, string lockModel)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(lockMfgId, out x0) ? Convert.ToInt32(lockMfgId) : 0;

            var ic = new InvContext();
            var am = ic.AddGunLockModel(v1, lockModel);

            return Json(am, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddBullet(string bulletType, string code)
        {
            var ic = new InvContext();
            var am = ic.AddBulletType(bulletType, code);

            return Json(am, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddColor(string color)
        {
            var ic = new InvContext();
            var am = ic.AddColor(color);

            return Json(am, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddOtherManuf(string newMfg, string sectId)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(sectId, out x0) ? Convert.ToInt32(sectId) : 0;

            var ic = new InvContext();
            var am = ic.AddOtherManufacturer(newMfg, v1);

            return Json(am, JsonRequestBehavior.AllowGet);
        }
        




        [HttpPost]
        public JsonResult AddManufacturer(string newmfg, string imp)
        {
            var b0 = false;
            var ip = Boolean.TryParse(imp, out b0) ? Convert.ToBoolean(imp) : false;
            var ic = new InvContext();
            var am = ic.AddGunManufacturer(newmfg, ip);

            return Json(am, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddCaliber(string newcal, string std)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(std, out x0) ? Convert.ToInt32(std) : 0;
            var ic = new InvContext();
            var am = ic.AddCaliber(newcal, v1);

            return Json(am, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetSvcInqGuns(string inqNum)
        {
            long x0 = 0;
            var v1 = Int64.TryParse(inqNum, out x0) ? Convert.ToInt64(inqNum) : 0;
            var ic = new InvContext();
            var sg = ic.GetSvcGunsFromInquiry(v1);

            return Json(sg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetBookDataFromSvc(string inqNum, string gunId)
        {
            long x0 = 0;
            var x1 = 0;
            var v1 = Int64.TryParse(inqNum, out x0) ? Convert.ToInt64(inqNum) : 0;
            var v2 = Int32.TryParse(gunId, out x1) ? Convert.ToInt32(gunId) : 0;

            var ic = new InvContext();
            var d = ic.GetBookDataFromService(v1, v2);

            return Json(d, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetBookIdCode(string gType, string tCode)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(gType, out x0) ? Convert.ToInt32(gType) : 0;
            var v2 = Int32.TryParse(tCode, out x0) ? Convert.ToInt32(tCode) : 0;
            var ic = new InvContext();
            var sg = ic.MakeNewTransCode(v1, v2);

            return Json(new { bookId = sg }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetOtherSku()
        {
            var ic = new InvContext();
            var sg = ic.MakeOtherSku();

            return Json(new { sku = sg }, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public JsonResult MakeUpcCode()
        {
            var ic = new InvContext();
            var g = ic.CookUpcCode();

            return Json(g, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckUpcCode(string upc)
        {
            var ic = new InvContext();
            var g = ic.CheckUpcCode(upc);

            return Json(g, JsonRequestBehavior.AllowGet);
        }






    }


}