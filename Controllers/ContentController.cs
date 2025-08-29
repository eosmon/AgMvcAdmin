using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgMvcAdmin.Common;
using AgMvcAdmin.Models;
using AgMvcAdmin.Models.Common;
using AgMvcAdmin.Models.Menus;
using WebBase.Configuration;


namespace AgMvcAdmin.Controllers
{
    public class ContentController : BaseController
    {
        //
        // GET: /Content/
        [ValidateInput(false)]
        public ActionResult PageMgr()
        {
            var pg = new PageModel(Pages.ContentManager);
            return View(pg);
        }

        public ActionResult Sale()
        {
            var pg = new PageModel(Pages.SalesCampaigns);
            return View(pg);
        }


        public ActionResult Campaigns()
        {
            var pg = new PageModel(Pages.ContentCampaigns);
            return View(pg);
        }

        public ActionResult HomeScroll()
        {
            var pg = new PageModel(Pages.ContentHomeScroll);
            return View(pg);
        }


        [HttpPost]
        public JsonResult GetMenuPages()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["ContId"], out xInt1) ? Convert.ToInt32(Request["ContId"]) : 0;

            var mm = new MenuModel();
            var data = mm.GetContentPages(v1);

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetAllContent()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["PageId"], out xInt1) ? Convert.ToInt32(Request["PageId"]) : 0;

            var cm = new ContentContext();
            var pc = cm.GetPageContent(v1);

            if (pc.IsHomePage)
            {
                pc.AdOptions = GetMenuHomeOpts();
                pc.BgColors = GetPromoColors();
                pc.FtrSizes = GetFtrSizes();
            }

            return Json(new { CT = pc, TT = pc.ToolTips, CC = pc.Campaigns, HM = pc.Home, AO = pc.AdOptions, BC = pc.BgColors, SZ = pc.FtrSizes }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetFeatureMenus()
        {
            var cm = new ContentModel();
            cm.AdOptions = GetMenuHomeOpts();
            cm.BgColors = GetPromoColors();
            cm.FtrSizes = GetFtrSizes();

            return Json(new { AO = cm.AdOptions, BC = cm.BgColors, SZ = cm.FtrSizes }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult UpdatePgSeo()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["PgId"], out xInt1) ? Convert.ToInt32(Request["PgId"]) : 0;
            var v2 = Request["PgNam"];
            var v3 = Request["PgTtl"];
            var v4 = Request["PgKwd"];
            var v5 = Request["PgOgt"];
            var v6 = Request["PgDsc"];

            var cm = new ContentModel(v1, v2, v3, v4, v5, v6);
            var cc = new ContentContext();
            cc.UpdateSeo(cm);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddPgHdr()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["PgId"], out xInt1) ? Convert.ToInt32(Request["PgId"]) : 0;
            var v2 = Request["HdTtl"];
            var v3 = Request["HdTxt"];

            var cm = new ContentModel(v1, v2, v3);
            var cc = new ContentContext();
            cc.AddHeader(cm);

            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                UpdateSitePic(cc, files, SiteImageSections.Header, v1);

            }

            return Json("Success", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult UpdatePgHdr()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["PgId"], out xInt1) ? Convert.ToInt32(Request["PgId"]) : 0;
            var v2 = Request["HdTtl"];
            var v3 = Request["HdTxt"];

            var cm = new ContentModel(v1, v2, v3);
            var cc = new ContentContext();
            cc.UpdateHeader(cm);

            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                UpdateSitePic(cc, files, SiteImageSections.Header, v1);

            }


            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        public JsonResult NixHeader()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["PgId"], out xInt1) ? Convert.ToInt32(Request["PgId"]) : 0;
            var v2 = Request["IgNm"];

            var fn = Path.GetFileName(v2);
            if (fn.Contains("?"))
            {
                var bPath = ConfigurationHelper.GetPropertyValue("application", "PathSiteImgs");
                var f = fn.Split('?')[0];
                var fPath = bPath + "Headers" + "\\" + f;

                if (System.IO.File.Exists(fPath)) { System.IO.File.Delete(fPath); }
            }


            var cc = new ContentContext();
            cc.DeleteHeader(v1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult NixHeaderImage()
        {

            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["PgId"], out xInt1) ? Convert.ToInt32(Request["PgId"]) : 0;
            var v2 = Request["IgNm"];

            var fn = Path.GetFileName(v2);
            if (fn.Contains("?"))
            {
                var bPath = ConfigurationHelper.GetPropertyValue("application", "PathSiteImgs");
                var f = fn.Split('?')[0];
                var fPath = bPath + "Headers" + "\\" + f;

                if (System.IO.File.Exists(fPath))
                {
                    System.IO.File.Delete(fPath);
                    var cc = new ContentContext();
                    cc.DeleteHeaderPic(v1);
                }
            }

            return Json("Success", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult AddToolTip()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["PgId"], out xInt1) ? Convert.ToInt32(Request["PgId"]) : 0;
            var v2 = Request["TtDs"];
            var v3 = Request["TtTx"];
            var cc = new ContentContext();
            var cm = new ContentModel();
            cm.Id = v1;
            cm.ToolTipDesc = v2;
            cm.ToolTipText = v3;
            cc.AddToolTip(cm);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdToolTip()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["Id"], out xInt1) ? Convert.ToInt32(Request["Id"]) : 0;
            var v2 = Request["TtDs"];
            var v3 = Request["TtTx"];
            var cc = new ContentContext();
            var cm = new ContentModel();
            cm.Id = v1;
            cm.ToolTipDesc = v2;
            cm.ToolTipText = v3;
            cc.UpdateToolTip(cm);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NixToolTip()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["Id"], out xInt1) ? Convert.ToInt32(Request["Id"]) : 0;
            var cc = new ContentContext();
            cc.DeleteToolTip(v1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult NixBnr()
        {
            var m = new BaseModel();
            var bPath = ConfigurationHelper.GetPropertyValue("application", "p4");

            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["Id"], out xInt1) ? Convert.ToInt32(Request["Id"]) : 0;
            var v2 = Request["Img"];

            var fPath = string.Format("{0}\\{1}", m.DecryptIt(bPath), v2);

            if (System.IO.File.Exists(fPath))
            {
                System.IO.File.Delete(fPath);
                var cc = new ContentContext();
                cc.DeleteBanner(v1);
            }




            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddBnr()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["BrId"], out xInt1) ? Convert.ToInt32(Request["BrId"]) : 0;
            var v2 = Request["BrDs"];
            var v3 = Request["BrNu"];
            var v4 = Request["BrNw"];
            var nw = v4 == "1" ? true : false;

            var cb = new ContentBanner(v1, v2, v3, nw);
            var cc = new ContentContext();
            var id = cc.AddBanner(cb);

            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                UpdateSitePic(cc, files, SiteImageSections.Banner, id);

            }

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdBnr()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["Id"], out xInt1) ? Convert.ToInt32(Request["Id"]) : 0;
            var v2 = Request["BrDs"];
            var v3 = Request["BrUl"];
            var v4 = Request["BrNw"];

            var cc = new ContentContext();
            var b = new ContentBanner();
            b.BannerId = v1;
            b.ItemDesc = v2;
            b.NavToUrl = v3;
            b.NewWindow = v4 == "1" ? true : false;

            cc.UpdateBanner(b);

            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                UpdateSitePic(cc, files, SiteImageSections.Banner, v1);

            }

            return Json("Success", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult NixCpn()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["Id"], out xInt1) ? Convert.ToInt32(Request["Id"]) : 0;
            var cc = new ContentContext();
            cc.DeleteCampaign(v1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult AddCmp()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["CpDy"], out xInt1) ? Convert.ToInt32(Request["CpDy"]) : 0;
            var v2 = Request["CpTx"];

            var cp = new CampaignModel();
            cp.ShowDelay = v1;
            cp.CampaignName = v2;
            var cc = new ContentContext();
            var id = cc.AddCampaign(cp);


            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdCmp()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["Id"], out xInt1) ? Convert.ToInt32(Request["Id"]) : 0;
            var v2 = Int32.TryParse(Request["CpDy"], out xInt1) ? Convert.ToInt32(Request["CpDy"]) : 0;
            var v3 = Request["CpTx"];
            var cc = new ContentContext();
            var cm = new CampaignModel();
            cm.CampaignId = v1;
            cm.CampaignName = v3;
            cm.ShowDelay = v2;
            cc.UpdateCampaign(cm);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult AllCmpBnr()
        {

            var cm = new ContentContext();
            var cb = cm.GetAllBannersCampaigns();

            return Json(new { CP = cb.Campaigns, BN = cb.Banners },
                JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CmpById()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["Id"], out xInt1) ? Convert.ToInt32(Request["Id"]) : 0;
 
            var cc = new ContentContext();
            var b = cc.GetCampaignById(v1);

            return Json(new { AA = b.IsAvailAll, AC = b.IsAvailCurrent, CA = b.AllBanners, CC = b.CurrentBanners }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult AddCmpBnr()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["CpId"], out xInt1) ? Convert.ToInt32(Request["CpId"]) : 0;
            var v2 = Int32.TryParse(Request["BrId"], out xInt1) ? Convert.ToInt32(Request["BrId"]) : 0;
            var cc = new ContentContext();
            cc.AddCampaignBanner(v1, v2);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult NixCmpBnr()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["Id"], out xInt1) ? Convert.ToInt32(Request["Id"]) : 0;

            var cc = new ContentContext();
            cc.DeleteCampaignBanner(v1);

            return Json("Success", JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult SortCmpBnr()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["Id"], out xInt1) ? Convert.ToInt32(Request["Id"]) : 0;
            var v2 = Int32.TryParse(Request["StId"], out xInt1) ? Convert.ToInt32(Request["StId"]) : 0;
            var cc = new ContentContext();
            cc.SetBannerSort(v1, v2);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAvlCampaigns()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["PgId"], out xInt1) ? Convert.ToInt32(Request["PgId"]) : 0;

            var cc = new ContentContext();
            var data = cc.GetAvailCampaigns(v1);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddPgCampaign()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["PgId"], out xInt1) ? Convert.ToInt32(Request["PgId"]) : 0;
            var v2 = Int32.TryParse(Request["sCmp"], out xInt1) ? Convert.ToInt32(Request["sCmp"]) : 0;
            var v3 = Int32.TryParse(Request["sPos"], out xInt1) ? Convert.ToInt32(Request["sPos"]) : 0;

            var cc = new ContentContext();
            var cm = new CampaignModel(v1, v2, v3);
            cc.AddPageCampaign(cm);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult NixPgCampaign()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["Id"], out xInt1) ? Convert.ToInt32(Request["Id"]) : 0;

            var cc = new ContentContext();
            cc.DeletePageCampaign(v1);  

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonResult AddStcTxt()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["pgId"], out xInt1) ? Convert.ToInt32(Request["pgId"]) : 0;
            var v2 = Request["sTxt"];

            //v2 = v2.Replace("<p>", string.Empty);
            //v2 = v2.Replace("</p>", string.Empty);
            v2 = v2.Replace("&nbsp;", string.Empty);

            var cc = new ContentContext();
            cc.AddStaticText(v1, v2);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }



        [ValidateInput(false)]
        [HttpPost]
        public JsonResult UpdStcTxt()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["sId"], out xInt1) ? Convert.ToInt32(Request["sId"]) : 0;
            var v2 = Request["sTxt"];

            //v2 = v2.Replace("<p>", string.Empty);
            //v2 = v2.Replace("</p>", string.Empty);
            v2 = v2.Replace("&nbsp;", string.Empty);

            var cc = new ContentContext();
            cc.UpdateStaticText(v1, v2);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [ValidateInput(false)]
        [HttpPost]
        public JsonResult DelStcTxt()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["Id"], out xInt1) ? Convert.ToInt32(Request["Id"]) : 0;

            var cc = new ContentContext();
            cc.DeleteStaticText(v1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }




        #region Sales Campaigns

        [HttpPost]
        public JsonResult GetGroupMenus(string catId)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(catId, out x0) ? Convert.ToInt32(catId) : 0;

            var mm = new MenuModel();
            var sc = mm.GetManufByCategory(v1);

            return Json(new { Mfg = sc.List1, Cat = sc.List2 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSubMenu(string catId)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(catId, out x0) ? Convert.ToInt32(catId) : 0;

            var mm = new MenuModel();
            var sc = mm.GetSubCatByGroup(v1);

            return Json(sc, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public JsonResult GetCalMenu(string catId, string mfgId)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(catId, out x0) ? Convert.ToInt32(catId) : 0;
            var v2 = Int32.TryParse(mfgId, out x0) ? Convert.ToInt32(mfgId) : 0;

            var mm = new MenuModel();
            var sc = mm.CalibersByCategory(v1, v2);

            return Json(sc, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SearchSaleItems()
        {
            var xInt1 = 0;
            var i1 = Int32.TryParse(Request["catId"], out xInt1) ? Convert.ToInt32(Request["catId"]) : 0;
            var i2 = Int32.TryParse(Request["subId"], out xInt1) ? Convert.ToInt32(Request["subId"]) : 0;
            var i3 = Int32.TryParse(Request["mfgId"], out xInt1) ? Convert.ToInt32(Request["mfgId"]) : 0;
            var i4 = Int32.TryParse(Request["calId"], out xInt1) ? Convert.ToInt32(Request["calId"]) : 0;
            var i5 = Int32.TryParse(Request["cndId"], out xInt1) ? Convert.ToInt32(Request["cndId"]) : 0;
            var i6 = Int32.TryParse(Request["salId"], out xInt1) ? Convert.ToInt32(Request["salId"]) : 0;

            var sm = new SaleModel(i1, i2, i3, i4, i5, i6);

            var cm = new ContentContext();
            var si = cm.GetSalesItems(sm);

            return Json(si, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateSaleItem()
        {
            var i0 = 0;
            var b0 = false;
            var dt0 = new DateTime(1900, 1, 1);
            double d0 = 0.00;

            var i1 = Int32.TryParse(Request["MstId"], out i0) ? Convert.ToInt32(Request["MstId"]) : i0;

            var b1 = Boolean.TryParse(Request["OnSal"], out b0) ? Convert.ToBoolean(Request["OnSal"]) : b0;

            var d1 = Double.TryParse(Request["PrSal"], out d0) ? Convert.ToDouble(Request["PrSal"]) : d0;
            var d2 = Double.TryParse(Request["PrMsr"], out d0) ? Convert.ToDouble(Request["PrMsr"]) : d0;
            var d3 = Double.TryParse(Request["PrAsk"], out d0) ? Convert.ToDouble(Request["PrAsk"]) : d0;

            var dt1 = DateTime.TryParse(Request["StDat"], out dt0) ? Convert.ToDateTime(Request["StDat"]) : new DateTime(1900, 1, 1);
            var dt2 = DateTime.TryParse(Request["EdDat"], out dt0) ? Convert.ToDateTime(Request["EdDat"]) : new DateTime(1900, 1, 1);

            var si = new SaleItem(i1, b1, d1, d2, d3, dt1, dt2);

            var cc = new ContentContext();
            cc.UpdateSaleItem(si);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CreateFeatureItem()
        {
            var i0 = 0;
            var dt0 = new DateTime(1900, 1, 1);

            var i1 = Int32.TryParse(Request["MstId"], out i0) ? Convert.ToInt32(Request["MstId"]) : i0;

            var cc = new ContentContext();
            cc.AddFeatureItem(i1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AllFeatures()
        {

            var cm = new ContentContext();
            var cb = cm.GetFeatureItems();

            return Json(cb, JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> GetMenuHomeOpts()
        {
            var mm = new MenuModel();
            var data = mm.GetHomeAdOptions();

            return data;
        }

        public List<SelectListItem> GetPromoColors()
        {
            var mm = new MenuModel();
            var data = mm.GetHomeBgColors();

            return data;
        }

        public List<SelectListItem> GetFtrSizes()
        {
            var mm = new MenuModel();
            var data = mm.GetHomeFtrSizes();

            return data;
        }

        [HttpPost]
        public JsonResult UpdateHomeOption()
        {
            var i0 = 0;
            var b0 = false;

            var i1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0;
            var i2 = Int32.TryParse(Request["Pos"], out i0) ? Convert.ToInt32(Request["Pos"]) : i0;
            var i3 = Int32.TryParse(Request["Opt"], out i0) ? Convert.ToInt32(Request["Opt"]) : i0;
            var i4 = Int32.TryParse(Request["Ftr"], out i0) ? Convert.ToInt32(Request["Ftr"]) : i0;

            var b1 = Boolean.TryParse(Request["Atv"], out b0) ? Convert.ToBoolean(Request["Atv"]) : b0;

            var mc = new HomeModelConfig(i1, i2, i3, i4, b1);
            var cc = new ContentContext();
            cc.UpdateHomeConfig(mc);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddHomeOption()
        {
            var i0 = 0;
            var b0 = false;

            var i1 = Int32.TryParse(Request["Pos"], out i0) ? Convert.ToInt32(Request["Pos"]) : i0;
            var i2 = Int32.TryParse(Request["Opt"], out i0) ? Convert.ToInt32(Request["Opt"]) : i0;
            var i3 = Int32.TryParse(Request["Ftr"], out i0) ? Convert.ToInt32(Request["Ftr"]) : i0;

            var b1 = Boolean.TryParse(Request["Atv"], out b0) ? Convert.ToBoolean(Request["Atv"]) : b0;
 
            var mc = new HomeModelConfig(i1, i2, i3, b1);

            var cc = new ContentContext();
            cc.AddHomeConfig(mc);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddFeatureItem()
        {
            var i0 = 0;
            var b0 = false;

            var i1 = Int32.TryParse(Request["Grp"], out i0) ? Convert.ToInt32(Request["Grp"]) : i0;
            var i2 = Int32.TryParse(Request["Ftr"], out i0) ? Convert.ToInt32(Request["Ftr"]) : i0;
            var i3 = Int32.TryParse(Request["Siz"], out i0) ? Convert.ToInt32(Request["Siz"]) : i0;
            var i4 = Int32.TryParse(Request["Col"], out i0) ? Convert.ToInt32(Request["Col"]) : i0;

            var b1 = Boolean.TryParse(Request["IsP"], out b0) ? Convert.ToBoolean(Request["IsP"]) : b0;
            var v1 = Request["Txt"];

            var mc = new HomeModelConfig(i1, i2, i3, i4, b1, v1);

            var cc = new ContentContext();
            cc.AddFeature(mc);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateFeatureItem()
        {
            var i0 = 0;
            var b0 = false;

            var i1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0;
            var i2 = Int32.TryParse(Request["Ftr"], out i0) ? Convert.ToInt32(Request["Ftr"]) : i0;
            var i3 = Int32.TryParse(Request["Siz"], out i0) ? Convert.ToInt32(Request["Siz"]) : i0;
            var i4 = Int32.TryParse(Request["Col"], out i0) ? Convert.ToInt32(Request["Col"]) : i0;

            var b1 = Boolean.TryParse(Request["IsP"], out b0) ? Convert.ToBoolean(Request["IsP"]) : b0;
            var v1 = Request["Txt"];

            var mc = new HomeModelConfig(i1, i2, i3, i4, b1, v1);

            var cc = new ContentContext();
            cc.UpdateFeature(mc);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


 

        [HttpPost]
        public JsonResult NixHomeItem()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0;

            var cc = new ContentContext();
            cc.DeleteHomeContent(i1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult NixFeatureItem()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0;

            var cc = new ContentContext();
            cc.DeleteFeatureContent(i1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult NixFeatureCampaign()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0;

            var cc = new ContentContext();
            cc.DeleteFeatureContent(i1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddHomeMenus()
        {
            var a = GetMenuHomeOpts();
            var b = GetPromoColors();
            var c = GetFtrSizes();

            return Json(new { AO = a, BC = b, SZ = c }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetAdById()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0;

            var cc = new ContentContext();
            var x = cc.PreviewFeatureItem(i1);

            return Json(x, JsonRequestBehavior.AllowGet);
        }


        #endregion  

    }
}