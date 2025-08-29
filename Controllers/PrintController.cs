using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using WebBase.Configuration;
using Primera.Print;
using Primera.Print.Modules;

namespace AgMvcAdmin.Controllers
{
    public class PrintController : BaseController
    {

        private LX500 MyPrinter { get; set; }

        private readonly PrinterManager Manager = PrinterManager.Instance;
        private Type PrinterType = typeof(LX500);

        

        protected GunTag TagData { get; set; }

        public void CookTagsAdded(GunTag tag)
        {
            TagData = tag;
            PrintGunTag();
            PrintBarcode();
        }

        private int GetWordLen(string s)
        {
            var ml = 0;

            string[] words = s.Split(' ');
            foreach (string w in words)
            {
                var wl = w.Trim().Length;
                if (wl > ml) { ml = wl; }
            }

            return ml;
        }


        [HttpPost]
        public JsonResult CookGunTag()
        {

            var x0 = 0;
            var cnt = Int32.TryParse(Request["Cnt"], out x0) ? Convert.ToInt32(Request["Cnt"]) : 0;
            var b1 = Int32.TryParse(Request["Sal"], out x0) ? Convert.ToInt32(Request["Sal"]) : x0;
            var mfg = Request["Mfg"];
            var mdl = Request["Mdl"];
            var act = Request["Act"];
            var cal = Request["Cal"];
            var cap = Request["Cap"];
            var brl = Request["Brl"];
            var cnd = Request["Cnd"];
            var mpn = Request["Mpn"];
            var upc = Request["Upc"];
            var prc = Request["Prc"];
            var sku = Request["Sku"];
            var sal = b1 < 103;
            TagData = new GunTag(cnt, sal, mfg, mdl, act, cal, cap, brl, cnd, mpn, upc, prc, sku);

            PrintGunTag();

            return Json("good", JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult CookAmmoTag()
        {
            var x0 = 0;

            var cnt = Int32.TryParse(Request["Cnt"], out x0) ? Convert.ToInt32(Request["Cnt"]) : 0;
            var b1 = Int32.TryParse(Request["Sal"], out x0) ? Convert.ToInt32(Request["Sal"]) : x0;
            var v1 = Request["TgMfg"];
            var v2 = Request["TgCat"];
            var v3 = Request["TgCal"];
            var v4 = Request["TgTyp"];
            var v5 = Request["TgRpb"];
            var v6 = Request["TgMpn"];
            var v7 = Request["TgPrc"];
            var v8 = Request["TgSku"];
            var sal = b1 < 3;

            TagData = new GunTag(cnt, sal, v1, v2, v3, v4, v5, v6, v7, v8);

            PrintAmmoTag();

            return Json("SUCCESS", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CookMerchTag()
        {
            var x0 = 0;

            var cnt = Int32.TryParse(Request["Cnt"], out x0) ? Convert.ToInt32(Request["Cnt"]) : 0;
            var b1 = Int32.TryParse(Request["Sal"], out x0) ? Convert.ToInt32(Request["Sal"]) : x0;
            var v1 = Request["TgMfg"];
            var v2 = Request["TgCat"];
            var v3 = Request["TgDsc"];
            var v4 = Request["TgCnd"];
            var v5 = Request["TgMpn"];
            var v6 = Request["TgSvc"];
            var v7 = Request["TgPrc"];
            var v8 = Request["TgSku"];
            var sal = b1 < 3;

            TagData = new GunTag(cnt, v1, v2, v3, v4, v5, v6, v7, v8, sal);

            PrintMerchandiseTag();

            return Json("SUCCESS", JsonRequestBehavior.AllowGet);
        }


        protected void PrintGunTag()
        {

            Manager.Register(PrimeraRegistry.LX500);
            MyPrinter = Manager.GetPrinterFromName<LX500>(LX500.DefaultDriver.DriverName);

            PrintDocument pd = new PrintDocument();
            pd.PrintController = new StandardPrintController();
            pd.PrinterSettings.PrinterName = "Color Label 500"; //"Brother MFC-9330CDW Printer";
            pd.PrinterSettings.Copies = 1;
            pd.DefaultPageSettings.PaperSource.RawKind = 0x100;
            pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 400, 200) { RawKind = 0x100 };
            pd.DefaultPageSettings.Landscape = false;

            var quality = 3;
            var saturation = 100;

            var values = new PrivateDevModeValues()
            {
                Quality = (Quality)quality,
                Saturation = saturation,
                RotateImage180 = false,
                MirrorImage = false
            };

            MyPrinter.Print.SetPrivateSettings(pd.PrinterSettings, values);
            pd.PrintPage += pd_NewGunTag; //pd_PrintGunTag;
            pd.Print();
        }

        public void PrintItemTag(int i)
        {
            Manager.Register(PrimeraRegistry.LX500);
            MyPrinter = Manager.GetPrinterFromName<LX500>(LX500.DefaultDriver.DriverName);

            PrintDocument pd = new PrintDocument();
            pd.PrintController = new StandardPrintController();
            pd.PrinterSettings.PrinterName = "Color Label 500"; //"Brother MFC-9330CDW Printer";
            pd.PrinterSettings.Copies = 1;
            pd.DefaultPageSettings.PaperSource.RawKind = 0x100;
            pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 400, 200) { RawKind = 0x100 };
            pd.DefaultPageSettings.Landscape = false;

            var quality = 3;
            var saturation = 100;

            var values = new PrivateDevModeValues()
            {
                Quality = (Quality)quality,
                Saturation = saturation,
                RotateImage180 = false,
                MirrorImage = false
            };

            MyPrinter.Print.SetPrivateSettings(pd.PrinterSettings, values);

            if (i == 1) { pd.PrintPage += pd_NewGunTag; }
            if (i == 2) { pd.PrintPage += pd_PrintAmmoTag; }
            if (i == 3) { pd.PrintPage += pd_PrintMerchandiseTag; }

            pd.Print();
        }


        public void PrintAmmoTag()
        {
            //var pd = MakePrintDoc();
            //pd.PrintPage += pd_PrintAmmoTag;
            //pd.Print();
            PrintItemTag(2);
        }

        protected void PrintMerchandiseTag()
        {
            //var pd = MakePrintDoc();
            //pd.PrintPage += pd_PrintMerchandiseTag;
            //pd.Print();
            PrintItemTag(3);
        }


        protected PrintDocument MakePrintDoc()
        {

            var prt = ConfigurationHelper.GetPropertyValue("application", "DefaultPrint");
            var pn = GetDefaultPrinter(prt);
            var pr = new PrinterResolution();
            pr.Kind = PrinterResolutionKind.High;

            PrintDocument pd = new PrintDocument();
            pd.PrintController = new StandardPrintController();
            pd.PrinterSettings.PrinterName = MyPrinter.PrinterName;
            pd.PrinterSettings.Copies = 1;
            pd.DefaultPageSettings.PaperSource.RawKind = 0x100;
            pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 400, 300) { RawKind = 0x100 };
            pd.DefaultPageSettings.Landscape = true;

            var quality = 3;
            var saturation = 100;

            var values = new PrivateDevModeValues()
            {
                Quality = (Quality)quality,
                Saturation = saturation,
                RotateImage180 = false,
                MirrorImage = false
            };

            MyPrinter.Print.SetPrivateSettings(pd.PrinterSettings, values);


            //PaperSize ps = new PaperSize("Tags", 400, 200);
            ////pd.DefaultPageSettings.PaperSize = ps;
            ////pd.DefaultPageSettings.Landscape = true;
            ////pd.DefaultPageSettings.PrinterResolution = pr;

            //PageSettings pst = new PageSettings();
            //pst.PaperSize = ps;
            //pst.PrinterResolution = pr;
            //pst.Landscape = true;

            //pd.DefaultPageSettings = pst;
            //pd.PrinterSettings.PrinterName = "Color Label 500";
            //pd.PrinterSettings.Copies = (Int16)TagData.Copies;
            return pd;
        }


        protected void PrintBarcode()
        {
            var pd = MakePrintDoc();
            pd.PrintPage += pd_PrintBarcode;
            pd.Print();
        }

        public void pd_PrintAmmoTag(object sender, PrintPageEventArgs e)
        {
            var td = TagData;

            var mfg = td.Manuf;
            var cat = td.Categ;
            var cal = td.Calbr.Length > 24 ? td.Calbr.Substring(0, 24) : td.Calbr;
            var typ = td.BulTp;
            var rpb = td.RdsPb;
            var mpn = td.MfgPn;
            var prc = td.Price;
            var sku = td.SkuNm;
            var iSl = td.ForSa;
 
            var calN = cal.Length > 25 ? cal.Substring(0, 25) + "..." : cal;
            var mpnN = mpn.Length > 20 ? mpn.Substring(0, 20) + "..." : mpn;


            // Spacing
            var dep = 20;
            var spcStart = 67 + dep;
            var spcIncr = 20;

            // Stardards
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            // Fonts
            var f1 = new Font("Arial", 7, FontStyle.Regular);
            var f2 = new Font("Arial", 8, FontStyle.Regular);
            var f3 = new Font("Arial", 7, FontStyle.Bold);
            var f4 = new Font("Arial", 8, FontStyle.Bold);
            var f5 = new Font("Arial", 9, FontStyle.Bold);
            var f7 = new Font("Arial", 6, FontStyle.Bold);
            var f9 = new Font("Arial", 18, FontStyle.Bold);
            var f10 = new Font("Arial", 12, FontStyle.Bold);

            // Alignments
            var alnRtCtr = new StringFormat();
            alnRtCtr.LineAlignment = StringAlignment.Center;
            alnRtCtr.Alignment = StringAlignment.Near;

            var alnCtrCtr = new StringFormat();
            alnCtrCtr.LineAlignment = StringAlignment.Center;
            alnCtrCtr.Alignment = StringAlignment.Center;


            // Brushes
            var brushWht = new SolidBrush(Color.White);
            var brushBlk = new SolidBrush(Color.Black);
            var brushOrg = new SolidBrush(Color.Orange);
            var brushDrd = new SolidBrush(Color.DarkRed);
            var brushBlu = new SolidBrush(Color.Blue);

            // Main Body
            var rect = new Rectangle(0, 0, 200, 400);
            e.Graphics.FillRectangle(brushWht, rect);

            // Banner URL
            var rect1 = new Rectangle(0, dep + 4, 200, 23);
            e.Graphics.FillRectangle(brushBlk, rect1);

            // Logo
            using (var logo = new Bitmap(Server.MapPath("~/Common/Images/LogoBaseClear.png")))
            {
                e.Graphics.DrawString("view at Hillcrest Pawn", f5, brushWht, new PointF(22, dep + 8));
                e.Graphics.DrawImage(logo, 5, dep + 31, 60, 48);
            }

            // Manuf Name
            var fSz = 0;

            if (mfg.Length < 6) { fSz = 25; }
            else if (mfg.Length < 9) { fSz = 20; }
            else if (mfg.Length < 12) { fSz = 16; }
            else if (mfg.Length < 19) { fSz = 13; }
            else if (mfg.Length < 32) { fSz = 11; }
            else if (mfg.Length < 42) { fSz = 10; }
            else { fSz = 9; }

            using (var f6 = new Font("Arial Black", fSz, FontStyle.Bold, GraphicsUnit.Pixel, Byte.MaxValue))
            {
                using (var pw = new Pen(Color.White))
                {
                    var rect2 = new RectangleF(67, dep + 34, 131, 43);
                    e.Graphics.DrawString(mfg, f6, brushBlk, rect2, alnRtCtr);
                    e.Graphics.DrawRectangle(pw, Rectangle.Round(rect2));
                }
            }

            if (cat.Length > 0) // Category
            {
                spcStart += spcIncr;
                e.Graphics.DrawString("Category:", f1, brushBlk, 7, spcStart);
                e.Graphics.DrawString(cat, f4, brushBlk, 51, spcStart - 1);
            }

            if (cal.Length > 0) // Caliber
            {
                spcStart += spcIncr;
                e.Graphics.DrawString("Caliber:", f1, brushBlk, 7, spcStart);
                e.Graphics.DrawString(calN, f4, brushBlk, 45, spcStart - 1);
            }

            if (typ.Length > 0) // Bullet Type
            {
                spcStart += spcIncr;
                e.Graphics.DrawString("Type:", f1, brushBlk, 7, spcStart);
                e.Graphics.DrawString(typ, f4, brushBlk, 35, spcStart - 1);
            }

            if (rpb.Length > 0) // Round Per Box
            {
                spcStart += spcIncr;
                e.Graphics.DrawString("Rounds Per Box:", f1, brushBlk, 7, spcStart);
                e.Graphics.DrawString(rpb, f4, brushBlk, 83, spcStart - 1);
            }

            if (mpn.Length > 0) // MPN
            {
                spcStart += spcIncr;
                e.Graphics.DrawString("MFG #:", f1, brushBlk, 7, spcStart);
                e.Graphics.DrawString(mpnN, f4, brushBlk, 43, spcStart - 1);
            }


            if (iSl)
            {
                // Price 
                e.Graphics.DrawString("Our Price:", f1, brushBlk, 7, spcStart + 30);
                e.Graphics.DrawString(prc, f9, brushDrd, 53, spcStart + 23);
            }
            else
            {
                var rect8 = new RectangleF(0, 135, 200, 200);
                e.Graphics.DrawString(prc, f9, brushDrd, rect8, alnCtrCtr);
            }


            // Prop 65 Warning

            using (var imgProp65 = new Bitmap(Server.MapPath("~/Common/Images/prop65.png")))
            {
                using (var pb = new Pen(Color.Black))
                {
                    e.Graphics.DrawImage(imgProp65, 62, dep + 238, 17, 14);
                    var rect3 = new RectangleF(20, dep + 235, 156, 51);
                    e.Graphics.DrawRectangle(pb, Rectangle.Round(rect3));
                    e.Graphics.DrawString("WARNING", f4, brushBlk, 79, dep + 239);
                    e.Graphics.DrawString("Cancer And Reproductive Harm", f3, brushBlk, rect3, alnCtrCtr);
                    e.Graphics.DrawString("www.P65Warnings.ca.gov", f1, brushBlu, 40, dep + 268);
                }
            }

            // BARCODE
            var f = new Font("IDAutomationHC39M", 8);
            var rect6 = new RectangleF(0, dep + 267, 200, 100);
            e.Graphics.DrawString(sku, f, brushBlk, rect6, alnCtrCtr);


            // SKU 
            var rect4 = new RectangleF(0, dep + 343, 200, 26);
            e.Graphics.FillRectangle(brushBlk, rect4);


            var rect5 = new RectangleF(0, dep + 344, 200, 26);
            e.Graphics.DrawString(sku, f10, brushWht, rect5, alnCtrCtr);

            brushWht.Dispose();
            brushBlk.Dispose();
            brushOrg.Dispose();
            brushBlu.Dispose();
            brushDrd.Dispose();

            f1.Dispose();
            f2.Dispose();
            f3.Dispose();
            f4.Dispose();
            f5.Dispose();
            f9.Dispose();
        }

        protected void pd_NewGunTag(object sender, PrintPageEventArgs e)
        {
            var td = TagData;

            var mfg = td.Manuf;
            var mdl = td.Model;
            var cal = td.Calbr.Length > 24 ? td.Calbr.Substring(0, 24) : td.Calbr;
            var typ = td.GunTp; 
            var cap = td.MagCp;
            var brl = td.Barrl;
            var cnd = td.Condt;
            var mpn = td.MfgPn;
            var upc = td.UpcCd;
            var prc = td.Price;
            var sku = td.SkuNm;
            var iSl = td.ForSa;

            var cond = string.Empty;
            if(cnd.Length > 0) { cond = cnd.Contains("New") ? "New" : "Used"; }

            // Spacing
            var dep = 0;
            var spcStart = 62 + dep;
            var spcIncr = 29;

            // Stardards
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            // Fonts
            var f1 = new Font("Arial", 7, FontStyle.Regular);
            var f2 = new Font("Arial", 8, FontStyle.Regular);
            var f3 = new Font("Arial", 7, FontStyle.Bold);
            var f4 = new Font("Arial", 8, FontStyle.Bold);
            var f5 = new Font("Arial", 9, FontStyle.Bold);
            var f7 = new Font("Arial", 6, FontStyle.Bold);
            var f8 = new Font("Arial", 11, FontStyle.Bold);
            var f9 = new Font("Arial", 18, FontStyle.Bold);
            var f10 = new Font("Arial", 12, FontStyle.Bold);
            var f11 = new Font("Arial", 9, FontStyle.Regular);
            var f12 = new Font("Arial", 16, FontStyle.Bold);
            var f13 = new Font("Arial", 10, FontStyle.Bold);
            var f14 = new Font("Arial", 14, FontStyle.Bold);

            // Alignments
            var alnRtCtr = new StringFormat();
            alnRtCtr.LineAlignment = StringAlignment.Center;
            alnRtCtr.Alignment = StringAlignment.Near;

            var alnCtrCtr = new StringFormat();
            alnCtrCtr.LineAlignment = StringAlignment.Center;
            alnCtrCtr.Alignment = StringAlignment.Center;


            // Brushes
            var brushWht = new SolidBrush(Color.White);
            var brushBlk = new SolidBrush(Color.Black);
            var brushOrg = new SolidBrush(Color.Orange);
            var brushDrd = new SolidBrush(Color.DarkRed);
            var brushBlu = new SolidBrush(Color.Blue);

            // Main Body
            var rect = new Rectangle(0, 0, 200, 400);
            e.Graphics.FillRectangle(brushWht, rect);

            // Banner URL
            var rect1 = new Rectangle(0, 7, 200, 23);
            e.Graphics.FillRectangle(brushBlk, rect1);

            // Logo
            using (var logo = new Bitmap(Server.MapPath("~/Common/Images/LogoBaseClear.png")))
            {
                e.Graphics.DrawString("Hillcrest Pawn", f10, brushWht, new PointF(27, 8));
                e.Graphics.DrawImage(logo, 10, 35, 60, 48);
            }

            // Manuf Name
            var fSz = 0;
            var wl = GetWordLen(mfg);

            switch (wl)
            {
                case 21:
                case 20:
                    fSz = 8;
                    break;
                case 19:
                case 18:
                    fSz = 9;
                    break;
                case 17:
                case 16:
                    fSz = 10;
                    break;
                case 15:
                case 14:
                    fSz = 11;
                    break;
                case 13:
                case 12:
                    fSz = 12;
                    break;
                case 10:
                    fSz = 13;
                    break;
                default:
                    if (mfg.Length < 6) { fSz = 25; }
                    else if (mfg.Length < 8) { fSz = 20; }
                    else if (mfg.Length < 9) { fSz = 19; }
                    else if (mfg.Length < 12) { fSz = 18; }
                    else if (mfg.Length < 15) { fSz = 16; }
                    else if (mfg.Length < 17) { fSz = 15; }
                    else if (mfg.Length < 23) { fSz = 14; }
                    else if (mfg.Length < 26) { fSz = 13; }
                    else if (mfg.Length < 32) { fSz = 11; }
                    else if (mfg.Length < 42) { fSz = 10; }
                    else { fSz = 9; }
                    break;

            }


            using (var f6 = new Font("Arial Black", fSz, FontStyle.Bold, GraphicsUnit.Pixel, Byte.MaxValue))
            {
                using (var pw = new Pen(Color.White))
                {
                    var rect2 = new RectangleF(70, 31, 145, 60);
                    e.Graphics.DrawString(mfg, f6, brushBlk, rect2, alnRtCtr);
                    e.Graphics.DrawRectangle(pw, Rectangle.Round(rect2));
                }
            }

            // Specs

            if (mdl.Length > 0) // Model
            {
                spcStart += 32;
                e.Graphics.DrawString("Model:", f11, brushBlk, 10, spcStart);
                //e.Graphics.DrawString(mdl, f5, brushBlk, 40, spcStart);

                if (mdl.Length < 19) { e.Graphics.DrawString(mdl, f8, brushBlk, 50, spcStart - 2); } // 11px
                else if (mdl.Length < 21) { e.Graphics.DrawString(mdl, f13, brushBlk, 50, spcStart - 1); }  // 10px
                else if (mdl.Length < 23) { e.Graphics.DrawString(mdl, f5, brushBlk, 50, spcStart); }  // 9px
                else if (mdl.Length < 28) { e.Graphics.DrawString(mdl, f4, brushBlk, 50, spcStart + 2); }  // 8px
                else if (mdl.Length < 31) { e.Graphics.DrawString(mdl, f3, brushBlk, 50, spcStart + 3); }  // 7px
                else { e.Graphics.DrawString(mdl, f7, brushBlk, 50, spcStart + 3); }  // 6px

            }

            if (typ.Length > 0) // Caliber & Type
            {
                spcStart += 27;
                e.Graphics.DrawString("Type:", f11, brushBlk, 10, spcStart);

                if (typ.Length < 19) { e.Graphics.DrawString(typ, f8, brushBlk, 43, spcStart - 2); } // 11px
                else if (typ.Length < 21) { e.Graphics.DrawString(typ, f13, brushBlk, 43, spcStart - 1); }  // 10px
                else if (typ.Length < 23) { e.Graphics.DrawString(typ, f5, brushBlk, 43, spcStart); }  // 9px
                else if (typ.Length < 28) { e.Graphics.DrawString(typ, f4, brushBlk, 43, spcStart + 2); }  // 8px
                else if (typ.Length < 31) { e.Graphics.DrawString(typ, f3, brushBlk, 45, spcStart + 3); }  // 7px
                else { e.Graphics.DrawString(typ, f7, brushBlk, 45, spcStart + 3); }  // 6px
            }

            if (iSl)
            {
                if (brl.Length > 0) // Barrel Length
                {
                    spcStart += 27; //default is 29
                    e.Graphics.DrawString("Barrel:", f11, brushBlk, 10, spcStart);
                    e.Graphics.DrawString(brl, f8, brushBlk, 49, spcStart - 1);
                }

                if (cap.Length > 0) // Capacity
                {
                    spcStart += 27;
                    e.Graphics.DrawString("Capacity:", f11, brushBlk, 10, spcStart);
                    e.Graphics.DrawString(cap, f8, brushBlk, 66, spcStart - 1);
                }

                if (cond.Length > 0) // Condition
                {
                    //spcStart += spcIncr;
                    e.Graphics.DrawString("Cond:", f11, brushBlk, 104, spcStart + 1);
                    e.Graphics.DrawString(cond, f8, brushBlk, 140, spcStart - 1);
                }

                if (mpn.Length > 0) // MPN 
                {
                    spcStart += 27;
                    e.Graphics.DrawString("MFG #:", f11, brushBlk, 10, spcStart);

                    if (mpn.Length < 12) { e.Graphics.DrawString(mpn, f8, brushBlk, 55, spcStart - 2); } // 11px
                    else if (mpn.Length < 13) { e.Graphics.DrawString(mpn, f13, brushBlk, 55, spcStart - 1); }  // 10px
                    else if (mpn.Length < 14) { e.Graphics.DrawString(mpn, f5, brushBlk, 5550, spcStart); }  // 9px
                    else if (mpn.Length < 17) { e.Graphics.DrawString(mpn, f4, brushBlk, 55, spcStart + 1); }  // 8px
                    else if (mpn.Length < 20) { e.Graphics.DrawString(mpn, f3, brushBlk, 55, spcStart + 2); }  // 7px
                    else { e.Graphics.DrawString(mpn, f7, brushBlk, 55, spcStart + 2); }  // 6px

                }

                // Price & SKU

                e.Graphics.DrawString("Price:", f11, brushBlk, 10, 237);
                e.Graphics.DrawString(prc, f9, brushDrd, 47, 228); //-9 above
            }
            else
            {
                prc = "MARK S";

                var rect8 = new RectangleF(0, 135, 200, 200);
                e.Graphics.DrawString(prc, f9, brushDrd, rect8, alnCtrCtr);
            }


            // BARCODE
            var f = new Font("IDAutomationHC39M", 8);
            var rect6 = new RectangleF(0, 235, 200, 100);
            e.Graphics.DrawString(sku, f, brushBlk, rect6, alnCtrCtr);

            using (var imgProp65 = new Bitmap(Server.MapPath("~/Common/Images/prop65.png")))
            {
                using (var pb = new Pen(Color.Black))
                {
                    e.Graphics.DrawImage(imgProp65, 62, 315, 17, 14);
                    var rect3 = new RectangleF(20, 312, 156, 51); //-3 from above
                    e.Graphics.DrawRectangle(pb, Rectangle.Round(rect3));
                    e.Graphics.DrawString("WARNING", f4, brushBlk, 79, 315); //same as top
                    e.Graphics.DrawString("Cancer And Reproductive Harm", f7, brushBlk, rect3, alnCtrCtr);
                    e.Graphics.DrawString("www.P65Warnings.ca.gov", f1, brushBlu, 40, 345); //+30
                }
            }


            // SKU 
            var rect4 = new Rectangle(0, 372, 200, 26); //dep + 343
            e.Graphics.FillRectangle(brushBlk, rect4);

            var rect5 = new RectangleF(0, 374, 200, 26); //dep + 344
            e.Graphics.DrawString(sku, f10, brushWht, rect5, alnCtrCtr);

            rect.Size = new Size(100, 200);

            brushWht.Dispose();
            brushBlk.Dispose();
            brushOrg.Dispose();
            brushBlu.Dispose();
            brushDrd.Dispose();

            f1.Dispose();
            f2.Dispose();
            f3.Dispose();
            f4.Dispose();
            f5.Dispose();
            f9.Dispose();
        }

        protected void pd_PrintGunTag(object sender, PrintPageEventArgs e)
        {
            var td = TagData;

            var mfg = td.Manuf;
            var mdl = td.Model;
            var cal = td.Calbr.Length > 24 ? td.Calbr.Substring(0, 24) : td.Calbr;
            var typ = td.GunTp;
            var cap = td.MagCp;
            var brl = td.Barrl;
            var cnd = td.Condt;
            var mpn = td.MfgPn;
            var upc = td.UpcCd;
            var prc = td.Price;
            var sku = td.SkuNm;
            var iSl = td.ForSa;

            // Spacing
            var dep = 20;
            var spcStart = 62 + dep;
            var spcIncr = 19;

            // Stardards
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            // Fonts
            var f1 = new Font("Arial", 7, FontStyle.Regular);
            var f2 = new Font("Arial", 8, FontStyle.Regular);
            var f3 = new Font("Arial", 7, FontStyle.Bold);
            var f4 = new Font("Arial", 8, FontStyle.Bold);
            var f5 = new Font("Arial", 9, FontStyle.Bold);
            var f7 = new Font("Arial", 6, FontStyle.Bold);
            var f9 = new Font("Arial", 18, FontStyle.Bold);
            var f10 = new Font("Arial", 12, FontStyle.Bold);

            // Alignments
            var alnRtCtr = new StringFormat();
            alnRtCtr.LineAlignment = StringAlignment.Center;
            alnRtCtr.Alignment = StringAlignment.Near;

            var alnCtrCtr = new StringFormat();
            alnCtrCtr.LineAlignment = StringAlignment.Center;
            alnCtrCtr.Alignment = StringAlignment.Center;


            // Brushes
            var brushWht = new SolidBrush(Color.White);
            var brushBlk = new SolidBrush(Color.Black);
            var brushOrg = new SolidBrush(Color.Orange);
            var brushDrd = new SolidBrush(Color.DarkRed);
            var brushBlu = new SolidBrush(Color.Blue);

            // Main Body
            var rect = new Rectangle(0, 0, 200, 400);
            e.Graphics.FillRectangle(brushWht, rect);

            // Banner URL
            var rect1 = new Rectangle(0, dep + 4, 200, 23);
            e.Graphics.FillRectangle(brushBlk, rect1);

            // Logo
            using (var logo = new Bitmap(Server.MapPath("~/Common/Images/LogoBaseClear.png")))
            {
                e.Graphics.DrawString("view at Hillcrest Pawn", f5, brushWht, new PointF(22, dep + 8));
                e.Graphics.DrawImage(logo, 5, dep + 31, 60, 48);
            }

            // Manuf Name
            var fSz = 0;

            if (mfg.Length < 6) { fSz = 25; }
            else if (mfg.Length < 9) { fSz = 20; }
            else if (mfg.Length < 12) { fSz = 16; }
            else if (mfg.Length < 19) { fSz = 13; }
            else if (mfg.Length < 32) { fSz = 11; }
            else if (mfg.Length < 42) { fSz = 10; }
            else { fSz = 9; }

            using (var f6 = new Font("Arial Black", fSz, FontStyle.Bold, GraphicsUnit.Pixel, Byte.MaxValue))
            {
                using (var pw = new Pen(Color.White))
                {
                    var rect2 = new RectangleF(67, dep + 34, 131, 43);
                    e.Graphics.DrawString(mfg, f6, brushBlk, rect2, alnRtCtr);
                    e.Graphics.DrawRectangle(pw, Rectangle.Round(rect2));
                }
            }

            // Specs
            if (mdl.Length > 0) // Model
            {
                spcStart += spcIncr + 5;
                e.Graphics.DrawString("Model:", f1, brushBlk, 7, spcStart);
                e.Graphics.DrawString(mdl, f3, brushBlk, 40, spcStart);
            }

            if (cal.Length > 0) // Caliber
            {
                spcStart += spcIncr;
                e.Graphics.DrawString("Caliber:", f1, brushBlk, 7, spcStart);
                e.Graphics.DrawString(cal, f3, brushBlk, 45, spcStart);
            }

            if (typ.Length > 0) // Gun Type
            {
                spcStart += spcIncr;
                e.Graphics.DrawString("Type:", f1, brushBlk, 7, spcStart);
                e.Graphics.DrawString(typ, f3, brushBlk, 35, spcStart);
            }


            if (iSl)
            {
                if (cap.Length > 0) // Capacity
                {
                    spcStart += spcIncr;
                    e.Graphics.DrawString("Capacity:", f1, brushBlk, 7, spcStart);
                    e.Graphics.DrawString(cap, f3, brushBlk, 51, spcStart);
                }

                if (brl.Length > 0) // Barrel Length
                {
                    spcStart += spcIncr;
                    e.Graphics.DrawString("Barrel:", f1, brushBlk, 7, spcStart);
                    e.Graphics.DrawString(brl, f3, brushBlk, 40, spcStart);
                }

                if (cnd.Length > 0) // Condition
                {
                    spcStart += spcIncr;
                    e.Graphics.DrawString("Condition:", f1, brushBlk, 7, spcStart);
                    e.Graphics.DrawString(cnd, f3, brushBlk, 55, spcStart);
                }

                if (mpn.Length > 0) // MPN
                {
                    spcStart += spcIncr;
                    e.Graphics.DrawString("MFG #:", f1, brushBlk, 7, spcStart);
                    e.Graphics.DrawString(mpn, f3, brushBlk, 43, spcStart);
                }

                //if (upc.Length > 0) // UPC
                //{
                //    spcStart += spcIncr;
                //    e.Graphics.DrawString("UPC Code:", f1, brushBlk, 7, spcStart);
                //    e.Graphics.DrawString(upc, f3, brushBlk, 59, spcStart);
                //}

                // Price & SKU
                e.Graphics.DrawString("Our Price:", f1, brushBlk, 7, dep + 206);
                e.Graphics.DrawString(prc, f9, brushDrd, 53, dep + 197);
            }
            else
            {
                var rect8 = new RectangleF(0, 135, 200, 200);
                e.Graphics.DrawString(prc, f9, brushDrd, rect8, alnCtrCtr);
            }


            // BARCODE
            var f = new Font("IDAutomationHC39M", 8);
            var rect6 = new RectangleF(0, dep + 204, 200, 100);
            e.Graphics.DrawString(sku, f, brushBlk, rect6, alnCtrCtr);


            // Prop 65 Warning

            using (var imgProp65 = new Bitmap(Server.MapPath("~/Common/Images/prop65.png")))
            {
                using (var pb = new Pen(Color.Black))
                {
                    e.Graphics.DrawImage(imgProp65, 62, dep + 285, 17, 14);
                    var rect3 = new RectangleF(20, dep + 282, 156, 51);
                    e.Graphics.DrawRectangle(pb, Rectangle.Round(rect3));
                    e.Graphics.DrawString("WARNING", f4, brushBlk, 79, dep + 285);
                    e.Graphics.DrawString("Cancer And Reproductive Harm", f7, brushBlk, rect3, alnCtrCtr);
                    e.Graphics.DrawString("www.P65Warnings.ca.gov", f1, brushBlu, 40, dep + 315);
                }
            }

            var rect4 = new Rectangle(0, dep + 343, 200, 26);
            e.Graphics.FillRectangle(brushBlk, rect4);


            var rect5 = new RectangleF(0, dep + 344, 200, 26);
            e.Graphics.DrawString(sku, f10, brushWht, rect5, alnCtrCtr);

            //???????
            //e.Graphics.RotateTransform(-90);


            brushWht.Dispose();
            brushBlk.Dispose();
            brushOrg.Dispose();
            brushBlu.Dispose();
            brushDrd.Dispose();

            f1.Dispose();
            f2.Dispose();
            f3.Dispose();
            f4.Dispose();
            f5.Dispose();
            f9.Dispose();
        }

        public void pd_PrintMerchandiseTag(object sender, PrintPageEventArgs e)
        {
            var td = TagData;

            var mfg = td.Manuf;
            var cat = td.Categ;
            var dsc = td.Descr;
            var cnd = td.Condt;
            var mpn = td.MfgPn;
            var prc = td.Price;
            var sku = td.SkuNm;
            var svc = td.SrvTp;
            var iSl = td.ForSa;

            var dscN = dsc.Length > 50 ? dsc.Substring(0, 50) + "..." : dsc;
            var mpnN = mpn.Length > 20 ? mpn.Substring(0, 20) + "..." : mpn;

            // Spacing
            var dep = 20;
            var spcStart = 67 + dep;
            var spcIncr = 22;

            // Stardards
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            // Fonts
            var f1 = new Font("Arial", 7, FontStyle.Regular);
            var f2 = new Font("Arial", 8, FontStyle.Regular);
            var f3 = new Font("Arial", 7, FontStyle.Bold);
            var f4 = new Font("Arial", 8, FontStyle.Bold);
            var f5 = new Font("Arial", 9, FontStyle.Bold);
            var f7 = new Font("Arial", 6, FontStyle.Bold);
            var f9 = new Font("Arial", 18, FontStyle.Bold);
            var f10 = new Font("Arial", 12, FontStyle.Bold);

            // Alignments
            var alnLtCtr = new StringFormat();
            alnLtCtr.LineAlignment = StringAlignment.Near;
            alnLtCtr.Alignment = StringAlignment.Near;

            var alnRtCtr = new StringFormat();
            alnRtCtr.LineAlignment = StringAlignment.Center;
            alnRtCtr.Alignment = StringAlignment.Near;

            var alnCtrCtr = new StringFormat();
            alnCtrCtr.LineAlignment = StringAlignment.Center;
            alnCtrCtr.Alignment = StringAlignment.Center;


            // Brushes
            var brushWht = new SolidBrush(Color.White);
            var brushBlk = new SolidBrush(Color.Black);
            var brushOrg = new SolidBrush(Color.Orange);
            var brushDrd = new SolidBrush(Color.DarkRed);
            var brushBlu = new SolidBrush(Color.Blue);

            // Main Body
            var rect = new Rectangle(0, 0, 200, 400);
            e.Graphics.FillRectangle(brushWht, rect);

            // Banner URL
            var rect1 = new Rectangle(0, dep + 4, 200, 23);
            e.Graphics.FillRectangle(brushBlk, rect1);

            // Logo
            //using (var logo = new Bitmap(Properties.Resources.LogoBaseClear))
            using (var logo = new Bitmap(Server.MapPath("~/Common/Images/LogoBaseClear.png")))
            {
                e.Graphics.DrawString("view at Hillcrest Pawn", f5, brushWht, new PointF(22, dep + 8));
                e.Graphics.DrawImage(logo, 5, dep + 31, 60, 48);
            }

            // Manuf Name
            var fSz = 0;

            if (mfg.Length < 6) { fSz = 25; }
            else if (mfg.Length < 9) { fSz = 20; }
            else if (mfg.Length < 12) { fSz = 16; }
            else if (mfg.Length < 19) { fSz = 13; }
            else if (mfg.Length < 32) { fSz = 11; }
            else if (mfg.Length < 42) { fSz = 10; }
            else { fSz = 9; }

            using (var f6 = new Font("Arial Black", fSz, FontStyle.Bold, GraphicsUnit.Pixel, Byte.MaxValue))
            {
                using (var pw = new Pen(Color.White))
                {
                    var rect2 = new RectangleF(67, dep + 34, 131, 41);
                    e.Graphics.DrawString(mfg, f6, brushBlk, rect2, alnRtCtr);
                    e.Graphics.DrawRectangle(pw, Rectangle.Round(rect2));
                }
            }


            if (cat.Length > 0) // Category
            {
                spcStart += spcIncr;
                e.Graphics.DrawString("Category:", f1, brushBlk, 7, spcStart);
                e.Graphics.DrawString(cat, f4, brushBlk, 51, spcStart - 1);
            }

            if (iSl)
            {
                if (cnd.Length > 0) // Condition
                {
                    spcStart += spcIncr;
                    e.Graphics.DrawString("Cond:", f1, brushBlk, 7, spcStart);
                    e.Graphics.DrawString(cnd, f4, brushBlk, 35, spcStart - 1);
                }

                if (mpn.Length > 0) // MPN
                {
                    spcStart += spcIncr;
                    e.Graphics.DrawString("MFG #:", f1, brushBlk, 7, spcStart);
                    e.Graphics.DrawString(mpnN, f4, brushBlk, 41, spcStart - 1);
                }
            }
            else
            {
                if (svc.Length > 0) // Service
                {
                    spcStart += spcIncr;
                    e.Graphics.DrawString("Service:", f1, brushBlk, 7, spcStart);
                    e.Graphics.DrawString(svc, f4, brushDrd, 45, spcStart - 1);
                }
            }

            if (dsc.Length > 0) // Description
            {
                spcStart += spcIncr;

                using (var pb = new Pen(Color.White))
                {
                    var rect7 = new RectangleF(35, spcStart - 1, 168, 50);
                    e.Graphics.DrawString("Desc:", f1, brushBlk, 7, spcStart);
                    e.Graphics.DrawString(dscN, f4, brushBlk, rect7, alnLtCtr);
                    e.Graphics.DrawRectangle(pb, Rectangle.Round(rect7));
                }
            }


            if (iSl)
            {
                // Price 
                e.Graphics.DrawString("Our Price:", f1, brushBlk, 7, 226);
                e.Graphics.DrawString(prc, f9, brushDrd, 53, 218);
            }
            else
            {
                var rect8 = new RectangleF(0, 123, 200, 200);
                e.Graphics.DrawString(prc, f9, brushDrd, rect8, alnCtrCtr);
            }



            // Prop 65 Warning

            //using (var imgProp65 = new Bitmap(Properties.Resources.prop65))
            using (var imgProp65 = new Bitmap(Server.MapPath("~/Common/Images/prop65.png")))
            {
                using (var pb = new Pen(Color.Black))
                {
                    e.Graphics.DrawImage(imgProp65, 62, dep + 238, 17, 14);
                    var rect3 = new RectangleF(20, dep + 235, 156, 51);
                    e.Graphics.DrawRectangle(pb, Rectangle.Round(rect3));
                    e.Graphics.DrawString("WARNING", f4, brushBlk, 79, dep + 239);
                    e.Graphics.DrawString("Cancer And Reproductive Harm", f7, brushBlk, rect3, alnCtrCtr);
                    e.Graphics.DrawString("www.P65Warnings.ca.gov", f1, brushBlu, 40, dep + 268);
                }
            }

            // BARCODE
            var f = new Font("IDAutomationHC39M", 8);
            var rect6 = new RectangleF(0, dep + 267, 200, 100);
            e.Graphics.DrawString(sku, f, brushBlk, rect6, alnCtrCtr);


            // SKU 
            var rect4 = new RectangleF(0, dep + 343, 200, 26);
            e.Graphics.FillRectangle(brushBlk, rect4);


            var rect5 = new RectangleF(0, dep + 344, 200, 26);
            e.Graphics.DrawString(sku, f10, brushWht, rect5, alnCtrCtr);

            brushWht.Dispose();
            brushBlk.Dispose();
            brushOrg.Dispose();
            brushBlu.Dispose();
            brushDrd.Dispose();

            f1.Dispose();
            f2.Dispose();
            f3.Dispose();
            f4.Dispose();
            f5.Dispose();
            f9.Dispose();
        }

        protected void pd_PrintBarcode(object sender, PrintPageEventArgs e)
        {
            // Stardards
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            // Brushes
            var brushWht = new SolidBrush(Color.White);
            var brushBlk = new SolidBrush(Color.Black);

            // Main Body
            var rect = new Rectangle(0, 0, 200, 400);
            e.Graphics.FillRectangle(brushWht, rect);

            // Alignments
            var alnCtrCtr = new StringFormat();
            alnCtrCtr.FormatFlags = StringFormatFlags.DirectionVertical;
            //alnCtrCtr.LineAlignment = StringAlignment.Center;
            alnCtrCtr.Alignment = StringAlignment.Center;

            var barcode = TagData.SkuNm;
            var f = new Font("IDAutomationHC39M", 13);
            var p = new PointF(60, 200);
            e.Graphics.DrawString(barcode, f, brushBlk, p, alnCtrCtr);
        }



	}

    public class GunTag
    {
        public string Descr { get; set; }
        public string Manuf { get; set; }
        public string Model { get; set; }
        public string GunTp { get; set; }
        public string Calbr { get; set; }
        public string MagCp { get; set; }
        public string Categ { get; set; }
        public string Barrl { get; set; }
        public string BulTp { get; set; }
        public string Condt { get; set; }
        public string MfgPn { get; set; }
        public string UpcCd { get; set; }
        public string Price { get; set; }
        public string RdsPb { get; set; }
        public string SkuNm { get; set; }
        public string SrvTp { get; set; }
        public string TrnTp { get; set; }
        public bool ForSa { get; set; }
        public int Copies { get; set; }

        public GunTag(){}

        /// Guns
        public GunTag(int i1, bool b1, string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8, string v9, string v10, string v11)
        {
            Copies = i1;
            ForSa = b1;
            Manuf = v1;
            Model = v2;
            GunTp = v3;
            Calbr = v4;
            MagCp = v5;
            Barrl = v6;
            Condt = v7;
            MfgPn = v8;
            UpcCd = v9;
            Price = v10;
            SkuNm = v11;
        }

        /// Ammunition
        public GunTag(int i1, bool b1, string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8)
        {
            Copies = i1;
            ForSa = b1;
            Manuf = v1;
            Categ = v2;
            Calbr = v3;
            BulTp = v4;
            RdsPb = v5;
            MfgPn = v6;
            Price = v7;
            SkuNm = v8;
        }


        /// Merchandise
        public GunTag(int i1, string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8, bool b1)
        {
            Copies = i1;
            Manuf = v1;
            Categ = v2;
            Descr = v3;
            Condt = v4;
            MfgPn = v5;
            SrvTp = v6;
            Price = v7;
            SkuNm = v8;
            ForSa = b1;
        }

    }
}