using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using AgMvcAdmin.Common;
using AgMvcAdmin.Models;
using AppBase;
using AppBase.Images;
using WebBase.Configuration;

namespace AgMvcAdmin.Controllers
{
    public class BaseController : Controller
    {

        protected readonly string Ow = ConfigurationHelper.GetPropertyValue("application", "ow");


        public string AddDocStr(PhotoUpload p)
        {
            var str = string.Empty;

            switch (p)
            {
                case PhotoUpload.Svc1:
                    str = "State ID";
                    break;
                case PhotoUpload.Svc2:
                    str = "CA BOE Permit";
                    break;
                case PhotoUpload.Svc3:
                    str = "CA Hi-Cap Permit";
                    break;
                case PhotoUpload.Svc4:
                    str = "Signed FFL";
                    break;
                case PhotoUpload.Svc5:
                    str = "Alien Res. I-9";
                    break;
                case PhotoUpload.Svc6:
                    str = "LEO ID";
                    break;
                case PhotoUpload.Svc7:
                    str = "CA COE";
                    break;
                case PhotoUpload.Svc8:
                    str = "CA FSC";
                    break;
            }

            return str;
        }

        //public void UpdateSitePic(ContentContext cc, HttpFileCollectionBase fcb, ImgSections section, int id)
        //{
        //    var bPath = ConfigurationHelper.GetPropertyValue("application", "PathSiteImgs");
        //    HttpPostedFileBase file = fcb[0];
        //    var fname = file.FileName;
        //    if (section == ImgSections.Ammo)
        //    {
        //        fname = string.Format("Ammo-{0}.jpg", id);
        //    }

        //    var folder = Enum.GetName(typeof(ImgSections), section);  
        //    var nFile = string.Format("{0}{1}\\{2}", bPath, folder, fname);
        //    file.SaveAs(nFile);

        //    if (System.IO.File.Exists(nFile))
        //    {

        //        switch (section)
        //        {
        //            case ImgSections.Banners:
        //                var cb = new ContentBanner();
        //                cb.BannerId = id;
        //                cb.ImageUrl = fname;
        //                cc.UpdateBannerImg(cb);
        //                break;

        //            case ImgSections.Headers:
        //                var cm = new ContentModel();
        //                cm.Id = id;
        //                cm.HeaderImg = fname;
        //                cc.UpdateHeaderImg(cm);
        //                break;
                    
        //            case ImgSections.Ammo:
        //                var ic = new InvContext();
        //                ic.UpdateAmmoImg(id, fname);
        //                break;
        //        }




        //    }
        //}

        //public string GetDocFolderCode(int c)
        //{
        //    var doc = (DocTypes)c;
        //    var f = string.Empty;

        //    switch (doc)
        //    {
        //        case DocTypes.CaBoeResale:
        //        case DocTypes.CaDojCoe:
        //        case DocTypes.CaDojHiCap:
        //        case DocTypes.CaSpecWpnPermit:
        //        case DocTypes.CaSafeHandlgAffid:
        //            f = ConfigurationHelper.GetPropertyValue("application", "m1");
        //            break;
        //        case DocTypes.FflCommercial:
        //        case DocTypes.FflCurioRelic:
        //        case DocTypes.AtfMultiPistol:
        //        case DocTypes.AtfMultiRifle:
        //            f = ConfigurationHelper.GetPropertyValue("application", "m2");
        //            break;
        //        case DocTypes.CaDojFsc:
        //        case DocTypes.CaFscInstr:
        //            f = ConfigurationHelper.GetPropertyValue("application", "m3");
        //            break;
        //        case DocTypes.DriversLicense:
        //        case DocTypes.StateId:
        //        case DocTypes.DodId:
        //        case DocTypes.DmvChgOfAddr:
        //        case DocTypes.PptSellerId:
        //            f = ConfigurationHelper.GetPropertyValue("application", "m4");
        //            break;
        //        case DocTypes.GunLockReceipt:
        //        case DocTypes.CaGunSafeAffid:
        //            f = ConfigurationHelper.GetPropertyValue("application", "m5");
        //            break;
        //        case DocTypes.AlienI9:
        //        case DocTypes.AlienPermRes:
        //        case DocTypes.BirthCertForgn:
        //        case DocTypes.BirthCertUs:
        //        case DocTypes.OtherGovtDoc:
        //        case DocTypes.PassportForgn:
        //        case DocTypes.PassportUs:
        //        case DocTypes.DmvRegistration:
        //        case DocTypes.ConcWpnPermit:
        //        case DocTypes.PropDeedLease:
        //        case DocTypes.UtilityBillGov:
        //        case DocTypes.UtilityBillPvt:
        //            f = ConfigurationHelper.GetPropertyValue("application", "m6");
        //            break;
        //        case DocTypes.PostCert:
        //        case DocTypes.LeoDutyLetter:
        //        case DocTypes.LeoAmmoLetter:
        //        case DocTypes.LeoId:
        //            f = ConfigurationHelper.GetPropertyValue("application", "m7");
        //            break;
        //        case DocTypes.HuntLicense:
        //        case DocTypes.ExpFirearmsCard:
        //            f = ConfigurationHelper.GetPropertyValue("application", "m8");
        //            break;

     
        //    }

        //    return f;
        //}

        //public void UpdateBookPic(HttpPostedFileBase fcb, string sku, int index, string fName)
        //{
        //    var m = new BaseModel();
        //    var t = ConfigurationHelper.GetPropertyValue("application", "p11");
        //    var p = ConfigurationHelper.GetPropertyValue("application", "p8");

        //    var tPath = String.Format("{0}\\{1}", m.DecryptIt(t), fName);
        //    fcb.SaveAs(tPath);

        //    if (System.IO.File.Exists(tPath))
        //    {
        //        var np = ImageBase.MountInStockImage(tPath, m.DecryptIt(p), fName);
        //        var ic = new InvContext();
        //        ic.UpdateBookImg(sku, index, fName);
        //    }
        //}

        public void UpdateAmmoPic(HttpPostedFileBase fcb, int id, string fName)
        {
            var m = new BaseModel();
            var t = ConfigurationHelper.GetPropertyValue("application", "p11");
            var h = ConfigurationHelper.GetPropertyValue("application", "p12");
            var p = ConfigurationHelper.GetPropertyValue("application", "p9");

            var tPath = String.Format("{0}\\{1}", m.DecryptIt(t), fName);
            fcb.SaveAs(tPath);

            if (System.IO.File.Exists(tPath))
            {
                var np = ImageBase.MountInStockImage(tPath, m.DecryptIt(p), fName);

                var hi = string.Format("{0}\\{1}", m.DecryptIt(h), fName);
                System.IO.File.Copy(np, hi, true);

                var ic = new InvContext();
                ic.UpdateAmmoImg(id, fName);
            }
        }

        //public void UpdateMerchPic(HttpPostedFileBase fcb, int id, int idx, string fName, string orig)
        //{
        //    var m = new BaseModel();
        //    var t = ConfigurationHelper.GetPropertyValue("application", "p11");
        //    var h = ConfigurationHelper.GetPropertyValue("application", "p12");
        //    var p = ConfigurationHelper.GetPropertyValue("application", "p10");

        //    var bTmp = m.DecryptIt(t);
        //    var bDir = m.DecryptIt(p);
        //    var bWeb = m.DecryptIt(h);

        //    var tPath = String.Format("{0}\\{1}", bTmp, fName);
        //    fcb.SaveAs(tPath);

        //    if (System.IO.File.Exists(tPath))
        //    {
        //        var np = ImageBase.MountInStockImage(tPath, bDir, fName);

        //        if (idx == 1)
        //        {
        //            var hi = string.Format("{0}\\{1}", bWeb, fName);
        //            System.IO.File.Copy(np, hi, true);

        //            //take out the trash
        //            if (orig.Length > 0)
        //            {
        //                var oFil = string.Format("{0}\\{1}", bWeb, orig);
        //                System.IO.File.Delete(oFil);
        //            }
        //        }

        //        var ic = new InvContext();
        //        ic.UpdateInvImg(id, idx, fName);

        //        //take out the trash
        //        if (orig.Length > 0)
        //        {
        //            var oTmp = string.Format("{0}\\{1}", bTmp, orig);
        //            var oDir = string.Format("{0}\\{1}", bDir, orig);
        //            System.IO.File.Delete(oTmp);
        //            System.IO.File.Delete(oDir);
        //        }

        //    }
        //}


        public void UpdateInventoryPic(HttpPostedFileBase fcb, int id, int idx, string fName, string orig, ImgSections sect)
        {
            var m = new BaseModel();
            var p = string.Empty;

            switch (sect)
            {
                case ImgSections.Guns:
                    p = ConfigurationHelper.GetPropertyValue("application", "p8");
                    break;
                case ImgSections.Ammo:
                    p = ConfigurationHelper.GetPropertyValue("application", "p9");
                    break;
                case ImgSections.Merchandise:
                    p = ConfigurationHelper.GetPropertyValue("application", "p10");
                    break;
            }

            var t = ConfigurationHelper.GetPropertyValue("application", "p11");
            var h = ConfigurationHelper.GetPropertyValue("application", "p12");

            var bTmp = m.DecryptIt(t);
            var bDir = m.DecryptIt(p);
            var bWeb = m.DecryptIt(h);

            var tPath = String.Format("{0}\\{1}", bTmp, fName);
            fcb.SaveAs(tPath);

            if (System.IO.File.Exists(tPath))
            {
                var np = ImageBase.MountInStockImage(tPath, bDir, fName);

                if (idx == 1)
                {
                    var hi = string.Format("{0}\\{1}", bWeb, fName);
                    System.IO.File.Copy(np, hi, true);

                    //take out the trash
                    if (orig.Length > 0)
                    {
                        var oFil = string.Format("{0}\\{1}", bWeb, orig);
                        System.IO.File.Delete(oFil);
                    }
                }

                var ic = new InvContext();
                ic.UpdateInvImg(id, idx, fName);

                //take out the trash
                if (orig.Length > 0)
                {
                    var oTmp = string.Format("{0}\\{1}", bTmp, orig);
                    var oDir = string.Format("{0}\\{1}", bDir, orig);
                    System.IO.File.Delete(oTmp);
                    System.IO.File.Delete(oDir);
                }

            }
        }


        public void UpdateSalePic(HttpPostedFileBase fcb, int id, int index, string fName, string orig, ImgSections se, PicFolders pf)
        {
            var m = new BaseModel();
            var igf = string.Format("s{0}", (int)pf);

            var b = ConfigurationHelper.GetPropertyValue("application", "s1");
            var f = ConfigurationHelper.GetPropertyValue("application", igf);
            var h = ConfigurationHelper.GetPropertyValue("application", "p12");
            var j = ConfigurationHelper.GetPropertyValue("application", "p14");

            var bTmp = m.DecryptIt(b);
            var bDir = m.DecryptIt(f);
            var bWeb = m.DecryptIt(h);
            var bStk = m.DecryptIt(j);

            var tPath = String.Format("{0}\\{1}\\T\\{2}", bTmp, bDir, fName);
            fcb.SaveAs(tPath);

            //make the basePath
            var cat = Enum.GetName(typeof(ImgSections), (int)se);
            var bPath = String.Format("{0}\\{1}\\{2}\\", bTmp, bDir, cat);

            if (System.IO.File.Exists(tPath))
            {
                var np = ImageBase.MountInStockImage(tPath, bPath, fName);

                if (index == 1)
                {
                    // Copy single pics to web folder for fast query
                    var hi = string.Format("{0}\\{1}", bWeb, fName);
                    System.IO.File.Copy(np, hi, true);

                    // Copy single pic to the inStock folder for inventory grid
                    var sk = string.Format("{0}\\{1}\\{2}", bStk, cat, fName);
                    System.IO.File.Copy(np, sk, true);

                    //take out the trash
                    if (orig.Length > 0)
                    {
                        var oFil = string.Format("{0}\\{1}", bWeb, orig);
                        System.IO.File.Delete(oFil);
                    }

                }

                //var oc = new OrderContext();
                //oc.UpdateItemImg(id, (int)pf, fName);

                var ic = new InvContext();
                ic.UpdateInvImg(id, index, fName);


                //take out the trash
                if (orig.Length > 0)
                {
                    var oTmp = String.Format("{0}\\{1}\\T\\{2}", bTmp, bDir, orig);
                    var oDir = string.Format("{0}\\{1}\\{2}\\{3}", bTmp, bDir, cat, orig);
                    System.IO.File.Delete(oTmp);
                    System.IO.File.Delete(oDir);
                }

            }
        }



        public void UpdateSvcPic(HttpPostedFileBase fcb, int id, string fName, ImgSections se, PicFolders pf)
        {
            var m = new BaseModel();
            var igf = string.Format("s{0}", (int)pf);

            var b = ConfigurationHelper.GetPropertyValue("application", "s1");
            var f = ConfigurationHelper.GetPropertyValue("application", igf);

            var bTmp = m.DecryptIt(b);
            var bDir = m.DecryptIt(f);

            var tPath = String.Format("{0}\\{1}\\T\\{2}", bTmp, bDir, fName);
            fcb.SaveAs(tPath);

            //make the basePath
            var cat = Enum.GetName(typeof(ImgSections), (int)se);
            var bPath = String.Format("{0}\\{1}\\{2}\\", bTmp, bDir, cat);

            if (System.IO.File.Exists(tPath))
            {
                var np = ImageBase.MountInStockImage(tPath, bPath, fName);
                var oc = new OrderContext();
                oc.UpdateItemImg(id, (int)pf, fName);
            }
        }



        public void UpdateItemPic(HttpPostedFileBase fcb, int id, string fName, ImgSections se, PicFolders pf)
        {
        var m = new BaseModel();
        var p = string.Empty;
        var t = string.Empty;

        switch (pf)
        {
            case PicFolders.Custom:
                t = ConfigurationHelper.GetPropertyValue("application", "p19");
                switch (se)
                {
                    case ImgSections.Guns:
                        p = ConfigurationHelper.GetPropertyValue("application", "p16");
                        break;
                    case ImgSections.Ammo:
                        p = ConfigurationHelper.GetPropertyValue("application", "p17");
                        break;
                    case ImgSections.Merchandise:
                        p = ConfigurationHelper.GetPropertyValue("application", "p18");
                        break;
                }
                break;
            case PicFolders.Acquisition:
                t = ConfigurationHelper.GetPropertyValue("application", "p23");
                switch (se)
                {
                    case ImgSections.Guns:
                        p = ConfigurationHelper.GetPropertyValue("application", "p20");
                        break;
                    case ImgSections.Ammo:
                        p = ConfigurationHelper.GetPropertyValue("application", "p21");
                        break;
                    case ImgSections.Merchandise:
                        p = ConfigurationHelper.GetPropertyValue("application", "p22");
                        break;
                }
                break;
            case PicFolders.Consignment:
                t = ConfigurationHelper.GetPropertyValue("application", "p27");
                switch (se)
                {
                    case ImgSections.Guns:
                        p = ConfigurationHelper.GetPropertyValue("application", "p24");
                        break;
                    case ImgSections.Ammo:
                        p = ConfigurationHelper.GetPropertyValue("application", "p25");
                        break;
                    case ImgSections.Merchandise:
                        p = ConfigurationHelper.GetPropertyValue("application", "p26");
                        break;
                }
                break;

        }

        var bTmp = m.DecryptIt(t);
        var bDir = m.DecryptIt(p);

        var tPath = String.Format("{0}\\{1}", bTmp, fName);
        fcb.SaveAs(tPath);

        if (System.IO.File.Exists(tPath))
        {
            var np = ImageBase.MountInStockImage(tPath, bDir, fName);
            var oc = new OrderContext();
            oc.UpdateItemImg(id, (int)pf, fName);
        }
        }



        public void ClearWebMerchPics(List<string> items)
        {
            var m = new BaseModel();
            var p = ConfigurationHelper.GetPropertyValue("application", "p10");
            var bDir = m.DecryptIt(p);

            foreach(var n in items)
            {
                var fn = string.Format("{0}\\{1}", bDir, n);
                if (System.IO.File.Exists(fn)) { System.IO.File.Delete(fn); }
            }

        }

        public void UpdateGunPic(HttpPostedFileBase fcb, int id, int idx, string fName)
        {
            var m = new BaseModel();
            var t = ConfigurationHelper.GetPropertyValue("application", "p11");
            var h = ConfigurationHelper.GetPropertyValue("application", "p12");
            var p = ConfigurationHelper.GetPropertyValue("application", "p8");

            var tPath = String.Format("{0}\\{1}", m.DecryptIt(t), fName);
            fcb.SaveAs(tPath);

            if (System.IO.File.Exists(tPath))
            {
                var np = ImageBase.MountInStockImage(tPath, m.DecryptIt(p), fName);

                if (idx == 1)
                {
                    var hi = string.Format("{0}\\{1}", m.DecryptIt(h), fName);
                    System.IO.File.Copy(np, hi, true);
                }

                var ic = new InvContext();
                ic.UpdateGunImg(id, idx, fName);
            }
        }



        public void UpdateSitePic(HttpPostedFileBase fcb, SiteImageSections section, int masterId, int index, string fName)
        {
            var m = new BaseModel();
            var t = ConfigurationHelper.GetPropertyValue("application", "p11");
            var h = ConfigurationHelper.GetPropertyValue("application", "p12");
            var p = string.Empty; 

            switch (section)
            {
                case SiteImageSections.Guns:
                    p = ConfigurationHelper.GetPropertyValue("application", "p8");
                    break;
                case SiteImageSections.Ammunition:
                    p = ConfigurationHelper.GetPropertyValue("application", "p9");
                    break;
                case SiteImageSections.Merchandise:
                    p = ConfigurationHelper.GetPropertyValue("application", "p10");
                    break;
            }

            var tPath = String.Format("{0}\\{1}", m.DecryptIt(t), fName);
            fcb.SaveAs(tPath);

            if (System.IO.File.Exists(tPath))
            {
                var np = ImageBase.MountInStockImage(tPath, m.DecryptIt(p), fName);

                if (index==1)
                {
                    var hi = string.Format("{0}\\{1}", m.DecryptIt(h), fName);
                    System.IO.File.Copy(np, hi, true);
                }

                var ic = new InvContext();
                ic.UpdateInStockImg(masterId, index, fName);

            }
        }

        public void SetProfilePic(HttpPostedFileBase fcb, int userId, string fName)
        {
            var m = new BaseModel();
            var bPath = ConfigurationHelper.GetPropertyValue("application", "p1");
            var mPath = ConfigurationHelper.GetPropertyValue("application", "m9");
            var nFile = string.Format("{0}\\{1}\\{2}", m.DecryptIt(bPath), m.DecryptIt(mPath), fName);
            fcb.SaveAs(nFile);

            if (System.IO.File.Exists(nFile))
            {
                var cc = new CustomerContext();
                cc.UpdateProfilePic(userId, fName);
            }
        }

        public void CookImgDoc(HttpPostedFileBase fcb, int rowId, string fPath)
        {
            var bPath = ConfigurationHelper.GetPropertyValue("application", "p3");
            var m = new BaseModel();
            var nPath = string.Format("{0}\\{1}", m.DecryptIt(bPath), fPath);
            var fName = Path.GetFileName(nPath);
            fcb.SaveAs(nPath);

            if (System.IO.File.Exists(nPath))
            {
                var cc = new CustomerContext();
                cc.UpdateDocImage(rowId, fName);
            }
        }


        //public void UpdateDocName(HttpPostedFileBase fcb, int rowId, int docId, string fName)
        //{
        //    var bPath = ConfigurationHelper.GetPropertyValue("application", "p3");
        //    var m = new BaseModel();
        //    var f = GetDocFolderCode(docId);
        //    var nFile = string.Format("{0}\\{1}\\{2}", m.DecryptIt(bPath), m.DecryptIt(f), fName); 
        //    fcb.SaveAs(nFile);

        //    if (System.IO.File.Exists(nFile))
        //    {
        //        var cc = new CustomerContext();
        //        cc.UpdateDocImage(rowId, fName);
        //    }
        //}


        public void UpdateSitePic(ContentContext cc, HttpFileCollectionBase fcb, SiteImageSections section, int id)
        {
            var bPath = ConfigurationHelper.GetPropertyValue("application", "p4");
            var hPath = ConfigurationHelper.GetPropertyValue("application", "p5");
 
            var m = new BaseModel();
            var p = section == SiteImageSections.Banner ? bPath : hPath;
 
            HttpPostedFileBase file = fcb[0];
            var fname = file.FileName;
            var nFile = string.Format("{0}\\{1}", m.DecryptIt(p), fname);
            file.SaveAs(nFile);

            if (System.IO.File.Exists(nFile))
            {

                if (section == SiteImageSections.Banner)
                {
                    var cm = new ContentBanner();
                    cm.BannerId = id;
                    cm.ImageUrl = fname;
                    cc.UpdateBannerImg(cm); 
                }
                else
                {
                    var cm = new ContentModel();
                    cm.Id = id;
                    cm.HeaderImg = fname;
                    cc.UpdateHeaderImg(cm);                    
                }


            }
        }

        public string GetDefaultPrinter(string cfgPrnt)
        {
            var retStr = string.Empty;

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if(printer.Contains(cfgPrnt))
                {
                    retStr = printer;
                    break;
                }
            }

            return retStr;
        }

        public void FlushWebPics(SiteImageSections section, int id)
        {

            var m = new BaseModel();
            var bPath = ConfigurationHelper.GetPropertyValue("application", "p14");
            var folder = Enum.GetName(typeof(SiteImageSections), (int)section);

            for (var i = 2; i < 7; i++)
            {
                var fName = string.Format("{0}-{1}-{2}.jpg", folder, id, i);
                var imgPath = m.DecryptIt(bPath) +"\\" + folder + "\\" + fName;
                if (System.IO.File.Exists(imgPath)) { System.IO.File.Delete(imgPath); }
            }            
        }

        public string CookRandomStr(int l)
        {
            var s = string.Empty;
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[l];
            var random = new Random();

            for (int i = 0; i < l; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            s = new String(stringChars);

            return s;
        }

        public string CookVeryRandomStr(int l, bool c)
        {
            var s = string.Empty;
            var chars = c ? "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ0123456789*!#^@" : "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ0123456789";
            var stringChars = new char[l];
            var random = new Random();

            for (int i = 0; i < l; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            s = new String(stringChars);

            return s;
        }


    }
}