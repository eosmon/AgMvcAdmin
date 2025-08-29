using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using AgMvcAdmin.Common;
using AgMvcAdmin.Models;
using AgMvcAdmin.Models.Common;
using AgMvcAdmin.Models.Menus;
using AppBase;
using AppBase.Images;
using WebBase.Configuration;

namespace AgMvcAdmin.Controllers
{
    public class DataAdminController : Controller
    {

        public ActionResult DataFeed()
        {
            var g = GetSectionFromLink(Request.RawUrl.ToLower());

            var pg = new PageModel(Pages.DataFeedGuns);
            pg.Menus = new MenuModel(g, 1);
            pg.Gun = g;
            return View(pg);
        }

        public ActionResult Duplicates()
        {
            var f = new FilterModel();
            var g = new GunModel(f);

            var pg = new PageModel(Pages.DataFeedDuplicates);
            g.ValueId = 1;
            pg.Menus = new MenuModel(g, DupMenus.Unknown);
            pg.Gun = g;
            return View(pg);
        }

        public ActionResult Images()
        {
            var f = new FilterModel();
            var g = new GunModel(f);

            var pg = new PageModel(Pages.DataFeedImages);
            g.ItemMissing = true;
            pg.Menus = new MenuModel(g, ImgMenus.Unknown);
            pg.Gun = g;
            return View(pg);
        }

        public ActionResult Manufacturers()
        {
            var pg = new PageModel(Pages.DataFeedManuf);
            return View(pg);
        }





        [HttpPost]
        public JsonResult GetGunsMenus()
        {
            DataFeedContext df = new DataFeedContext();

            var xInt1 = 0;
            var xB = false;
            var v0 = Request["TxtSch"];
            var v1 = Request["PageUrl"];
            var v2 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var v3 = Int32.TryParse(Request["GtpId"], out xInt1) ? Convert.ToInt32(Request["GtpId"]) : 0;
            var v4 = Int32.TryParse(Request["CalId"], out xInt1) ? Convert.ToInt32(Request["CalId"]) : 0;
            var v5 = Int32.TryParse(Request["AtnId"], out xInt1) ? Convert.ToInt32(Request["AtnId"]) : 0;
            var v6 = Int32.TryParse(Request["DysBk"], out xInt1) ? Convert.ToInt32(Request["DysBk"]) : 0;
            var v7 = Int32.TryParse(Request["GunsPerPg"], out xInt1) ? Convert.ToInt32(Request["GunsPerPg"]) : 0;
            var v8 = Int32.TryParse(Request["StartRow"], out xInt1) ? Convert.ToInt32(Request["StartRow"]) : 0;
            var b1 = Boolean.TryParse(Request["IsCaAwRest"], out xB) ? Convert.ToBoolean(Request["IsCaAwRest"]) : xB;
            var b2 = Boolean.TryParse(Request["IsHidden"], out xB) ? Convert.ToBoolean(Request["IsHidden"]) : xB;
            var b3 = Boolean.TryParse(Request["IsCurModel"], out xB) ? Convert.ToBoolean(Request["IsCurModel"]) : xB;
            var b4 = Boolean.TryParse(Request["MissOrAll"], out xB) ? Convert.ToBoolean(Request["MissOrAll"]) : xB;
            var b5 = Boolean.TryParse(Request["IsOnDataFeed"], out xB) ? Convert.ToBoolean(Request["IsOnDataFeed"]) : xB;
            var b6 = Boolean.TryParse(Request["IsCaLegal"], out xB) ? Convert.ToBoolean(Request["IsCaLegal"]) : xB;
            var b7 = Boolean.TryParse(Request["IsCaRoster"], out xB) ? Convert.ToBoolean(Request["IsCaRoster"]) : xB;
            var b8 = Boolean.TryParse(Request["IsCaSaRev"], out xB) ? Convert.ToBoolean(Request["IsCaSaRev"]) : xB;
            var b9 = Boolean.TryParse(Request["IsCaSsPst"], out xB) ? Convert.ToBoolean(Request["IsCaSsPst"]) : xB;
            var b10 = Boolean.TryParse(Request["IsCaCurRel"], out xB) ? Convert.ToBoolean(Request["IsCaCurRel"]) : xB;
            var b11 = Boolean.TryParse(Request["IsCaPpt"], out xB) ? Convert.ToBoolean(Request["IsCaPpt"]) : xB;
            var b12 = Boolean.TryParse(Request["IsLeo"], out xB) ? Convert.ToBoolean(Request["IsLeo"]) : xB;

            var dir = new Uri(v1).Segments.LastOrDefault();
            var lc = dir.Contains("missing-gun-specs");

            if (b4)
            {
                if (lc)
                {
                    v1 = string.Format("{0}/data-feed-admin/missing-gun-specs/all-specs", df.GetHostUrl());
                }
            }
            else
            {
                v1 = string.Format("{0}/data-feed-admin/missing-gun-specs", df.GetHostUrl());
            }

            var g = GetSectionFromLink(v1);
            var ca = new CaRestrictModel(b6, b7, b11, b10, b8, b9);

            g.ManufId = v2;
            g.GunTypeId = v3;
            g.CaliberId = v4;
            g.ActionId = v5;
            g.IsHidden = b2;
            g.IsCurModel = b3;
            //g.CaOkay = b6;
            //g.CaRosterOk = b7;
            //g.CaSglActnOk = b8;
            //g.CaSglShotOk = b9;
            //g.CaCurioOk = b10;
            //g.CaPptOk = b11;
            g.SearchText = v0;
            g.CaRestrict = ca;

            g.Filters.DaysBackToSearch = v6;
            g.Filters.IsCaAwRestricted = b1;
            g.Filters.IsMissingAll = b4;
            g.Filters.IsOnDataFeed = b5;
            g.Filters.PagingMaxRows = v7;
            g.Filters.PagingStartRow = v8;
            g.Filters.IsLeo = b12;

            var m = new MenuModel(g, 1);

            var guns = df.GetDataFeedGuns(g);
            var ct = guns.FirstOrDefault().Count;

            guns.RemoveAt(0);

            var jsonResult =
                Json(
                    new
                    {
                        Count = ct,
                        Guns = guns,
                        Manuf = m.FeedManuf,
                        GunType = m.FeedGunType,
                        Caliber = m.FeedCaliber,
                        Action = m.FeedAction,
                        DaysPast = m.FeedDaysPast
                    }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        [HttpPost]
        public JsonResult GetDataGrid()
        {

            DataFeedContext df = new DataFeedContext();

            var xInt1 = 0;
            var xB = false;
            var v0 = Request["TxtSch"];
            var v1 = Request["PageUrl"];
            var v2 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var v3 = Int32.TryParse(Request["GtpId"], out xInt1) ? Convert.ToInt32(Request["GtpId"]) : 0;
            var v4 = Int32.TryParse(Request["CalId"], out xInt1) ? Convert.ToInt32(Request["CalId"]) : 0;
            var v5 = Int32.TryParse(Request["AtnId"], out xInt1) ? Convert.ToInt32(Request["AtnId"]) : 0;
            var v6 = Int32.TryParse(Request["DysBk"], out xInt1) ? Convert.ToInt32(Request["DysBk"]) : 0;
            var v7 = Int32.TryParse(Request["GunsPerPg"], out xInt1) ? Convert.ToInt32(Request["GunsPerPg"]) : 0;
            var v8 = Int32.TryParse(Request["StartRow"], out xInt1) ? Convert.ToInt32(Request["StartRow"]) : 0;
            var b1 = Boolean.TryParse(Request["IsCaAwRest"], out xB) ? Convert.ToBoolean(Request["IsCaAwRest"]) : xB;
            var b2 = Boolean.TryParse(Request["IsHidden"], out xB) ? Convert.ToBoolean(Request["IsHidden"]) : xB;
            var b3 = Boolean.TryParse(Request["IsCurModel"], out xB) ? Convert.ToBoolean(Request["IsCurModel"]) : xB;
            var b4 = Boolean.TryParse(Request["MissOrAll"], out xB) ? Convert.ToBoolean(Request["MissOrAll"]) : xB;
            var b5 = Boolean.TryParse(Request["IsOnDataFeed"], out xB) ? Convert.ToBoolean(Request["IsOnDataFeed"]) : xB;
            var b6 = Boolean.TryParse(Request["IsCaLegal"], out xB) ? Convert.ToBoolean(Request["IsCaLegal"]) : xB;
            var b7 = Boolean.TryParse(Request["IsCaRoster"], out xB) ? Convert.ToBoolean(Request["IsCaRoster"]) : xB;
            var b8 = Boolean.TryParse(Request["IsCaSaRev"], out xB) ? Convert.ToBoolean(Request["IsCaSaRev"]) : xB;
            var b9 = Boolean.TryParse(Request["IsCaSsPst"], out xB) ? Convert.ToBoolean(Request["IsCaSsPst"]) : xB;
            var b10 = Boolean.TryParse(Request["IsCaCurRel"], out xB) ? Convert.ToBoolean(Request["IsCaCurRel"]) : xB;
            var b11 = Boolean.TryParse(Request["IsCaPpt"], out xB) ? Convert.ToBoolean(Request["IsCaPpt"]) : xB;
            var b12 = Boolean.TryParse(Request["IsLeo"], out xB) ? Convert.ToBoolean(Request["IsLeo"]) : xB;

            var dir = new Uri(v1).Segments.LastOrDefault();
            var lc = dir.Contains("missing-gun-specs");

            if (b4)
            {
                if (lc)
                {
                    v1 = string.Format("{0}/data-feed-admin/missing-gun-specs/all-specs", df.GetHostUrl());
                }
            }
            else
            {
                if (lc)
                {
                    v1 = string.Format("{0}/data-feed-admin/missing-gun-specs", df.GetHostUrl());
                }
            }

            var g = GetSectionFromLink(v1);
            var ca = new CaRestrictModel(b6, b7, b11, b10, b8, b9);

            g.ManufId = v2;
            g.GunTypeId = v3;
            g.CaliberId = v4;
            g.ActionId = v5;
            g.IsHidden = b2;
            g.IsCurModel = b3;
            //g.CaOkay = b6;
            //g.CaRosterOk = b7;
            //g.CaSglActnOk = b8;
            //g.CaSglShotOk = b9;
            //g.CaCurioOk = b10;
            //g.CaPptOk = b11;
            g.SearchText = v0;
            g.CaRestrict = ca;

            g.Filters.DaysBackToSearch = v6;
            g.Filters.IsCaAwRestricted = b1;
            g.Filters.IsMissingAll = b4;
            g.Filters.IsOnDataFeed = b5;
            g.Filters.PagingMaxRows = v7;
            g.Filters.PagingStartRow = v8;
            g.Filters.IsLeo = b12;

            var guns = df.GetDataFeedGuns(g);
            var ct = guns.FirstOrDefault().Count;

            guns.RemoveAt(0);

            var jsonResult = Json(new {Count = ct, Guns = guns}, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }



        private GunModel GetSectionFromLink(string url)
        {
            var f = new FilterModel();
            var cr = new CaRestrictModel();
            var g = new GunModel(f, cr);

            if (url.Contains("missing-gun-specs"))
            {
                var s = Regex.Match(url, "[^/]+(?=/$|$)").Groups[0].Value.Trim();

                switch (s)
                {
                    case "all-specs":
                    {
                        g.Filters.IsMissingAll = true;
                        break;
                    }
                    case "gun-types":
                    {
                        g.Filters.IsMissingGunType = true;
                        break;
                    }
                    case "caliber":
                    {
                        g.Filters.IsMissingCaliber = true;
                        break;
                    }
                    case "capacity":
                    {
                        g.Filters.IsMissingCapacity = true;
                        break;
                    }
                    case "action":
                    {
                        g.Filters.IsMissingAction = true;
                        break;
                    }
                    case "finish":
                    {
                        g.Filters.IsMissingFinish = true;
                        break;
                    }
                    case "model":
                    {
                        g.Filters.IsMissingModel = true;
                        break;
                    }
                    case "desc":
                    {
                        g.Filters.IsMissingDesc = true;
                        break;
                    }
                    case "long-desc":
                    {
                        g.Filters.IsMissingLongDesc = true;
                        break;
                    }
                    case "barrel":
                    {
                        g.Filters.IsMissingBrlLen = true;
                        break;
                    }
                    case "overall":
                    {
                        g.Filters.IsMissingOvrLen = true;
                        break;
                    }
                    case "weight":
                    {
                        g.Filters.IsMissingWeight = true;
                        break;
                    }
                    case "images":
                    {
                        g.Filters.IsMissingImage = true;
                        break;
                    }
                }
            }



            return g;
        }


        [HttpPost]
        public JsonResult SetAllMenus()
        {
            var xB = false;
            var v1 = Request["PageUrl"];
            var b1 = Boolean.TryParse(Request["IsCaAwRest"], out xB) ? Convert.ToBoolean(Request["IsCaAwRest"]) : xB;
            var b2 = Boolean.TryParse(Request["IsHidden"], out xB) ? Convert.ToBoolean(Request["IsHidden"]) : xB;
            var b3 = Boolean.TryParse(Request["IsCurModel"], out xB) ? Convert.ToBoolean(Request["IsCurModel"]) : xB;
            var b4 = Boolean.TryParse(Request["MissOrAll"], out xB) ? Convert.ToBoolean(Request["MissOrAll"]) : xB;
            var b5 = Boolean.TryParse(Request["IsOnDataFeed"], out xB)
                ? Convert.ToBoolean(Request["IsOnDataFeed"])
                : xB;
            var b6 = Boolean.TryParse(Request["IsCaLegal"], out xB) ? Convert.ToBoolean(Request["IsCaLegal"]) : xB;
            var b7 = Boolean.TryParse(Request["IsCaRoster"], out xB) ? Convert.ToBoolean(Request["IsCaRoster"]) : xB;
            var b8 = Boolean.TryParse(Request["IsCaSaRev"], out xB) ? Convert.ToBoolean(Request["IsCaSaRev"]) : xB;
            var b9 = Boolean.TryParse(Request["IsCaSsPst"], out xB) ? Convert.ToBoolean(Request["IsCaSsPst"]) : xB;
            var b10 = Boolean.TryParse(Request["IsCaCurRel"], out xB) ? Convert.ToBoolean(Request["IsCaCurRel"]) : xB;
            var b11 = Boolean.TryParse(Request["IsCaPpt"], out xB) ? Convert.ToBoolean(Request["IsCaPpt"]) : xB;

            var bm = new BaseModel();
            var dir = new Uri(v1).Segments.LastOrDefault();
            var lc = dir.Contains("missing-gun-specs");

            if (b4)
            {
                if (lc)
                {
                    v1 = string.Format("{0}/data-feed-admin/missing-gun-specs/all-specs", bm.GetHostUrl());
                }
            }
            else
            {
                if (lc)
                {
                    v1 = string.Format("{0}/data-feed-admin/missing-gun-specs", bm.GetHostUrl());
                }
            }

            var g = GetSectionFromLink(v1);
            var ca = new CaRestrictModel(b6, b7, b11, b10, b8, b9);

            g.IsHidden = b2;
            g.IsCurModel = b3;
            //g.CaOkay = b6;
            //g.CaRosterOk = b7;
            //g.CaSglActnOk = b8;
            //g.CaSglShotOk = b9;
            //g.CaCurioOk = b10;
            //g.CaPptOk = b11;
            g.SearchText = "";
            g.Filters.IsCaAwRestricted = b1;
            g.Filters.IsMissingAll = b4;
            g.Filters.IsOnDataFeed = b5;
            g.CaRestrict = ca;
            var m = new MenuModel(g, 1);

            return Json(
                new
                {
                    Manuf = m.FeedManuf,
                    GunType = m.FeedGunType,
                    Caliber = m.FeedCaliber,
                    Action = m.FeedAction,
                    DaysPast = m.FeedDaysPast
                }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult SetMenuMfg()
        {
            var xInt1 = 0;
            var xB = false;
            var v1 = Request["PageUrl"];
            var v2 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var b1 = Boolean.TryParse(Request["IsCaAwRest"], out xB) ? Convert.ToBoolean(Request["IsCaAwRest"]) : xB;
            var b2 = Boolean.TryParse(Request["IsHidden"], out xB) ? Convert.ToBoolean(Request["IsHidden"]) : xB;
            var b3 = Boolean.TryParse(Request["IsCurModel"], out xB) ? Convert.ToBoolean(Request["IsCurModel"]) : xB;
            var b4 = Boolean.TryParse(Request["MissOrAll"], out xB) ? Convert.ToBoolean(Request["MissOrAll"]) : xB;
            var b5 = Boolean.TryParse(Request["IsOnDataFeed"], out xB)
                ? Convert.ToBoolean(Request["IsOnDataFeed"])
                : xB;
            var b6 = Boolean.TryParse(Request["IsCaLegal"], out xB) ? Convert.ToBoolean(Request["IsCaLegal"]) : xB;
            var b7 = Boolean.TryParse(Request["IsCaRoster"], out xB) ? Convert.ToBoolean(Request["IsCaRoster"]) : xB;
            var b8 = Boolean.TryParse(Request["IsCaSaRev"], out xB) ? Convert.ToBoolean(Request["IsCaSaRev"]) : xB;
            var b9 = Boolean.TryParse(Request["IsCaSsPst"], out xB) ? Convert.ToBoolean(Request["IsCaSsPst"]) : xB;
            var b10 = Boolean.TryParse(Request["IsCaCurRel"], out xB) ? Convert.ToBoolean(Request["IsCaCurRel"]) : xB;
            var b11 = Boolean.TryParse(Request["IsCaPpt"], out xB) ? Convert.ToBoolean(Request["IsCaPpt"]) : xB;
            var b12 = Boolean.TryParse(Request["IsLeo"], out xB) ? Convert.ToBoolean(Request["IsLeo"]) : xB;


            var g = GetSectionFromLink(v1);
            var ca = new CaRestrictModel(b6, b7, b11, b10, b8, b9);
            g.ManufId = v2;
            g.IsHidden = b2;
            g.IsCurModel = b3;
            //g.CaOkay = b6;
            //g.CaRosterOk = b7;
            //g.CaSglActnOk = b8;
            //g.CaSglShotOk = b9;
            //g.CaCurioOk = b10;
            //g.CaPptOk = b11;
            g.SearchText = "";
            g.Filters.IsCaAwRestricted = b1;
            g.Filters.IsMissingAll = b4;
            g.Filters.IsOnDataFeed = b5;
            g.Filters.IsLeo = b12;
            g.CaRestrict = ca;
            var m = new MenuModel(g, 1);

            return Json(
                new
                {
                    GunType = m.FeedGunType,
                    Caliber = m.FeedCaliber,
                    Action = m.FeedAction,
                    DaysPast = m.FeedDaysPast
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetGunType()
        {
            var xInt1 = 0;
            var xB = false;
            var v1 = Request["PageUrl"];
            var v2 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var v3 = Int32.TryParse(Request["GunTypeId"], out xInt1) ? Convert.ToInt32(Request["GunTypeId"]) : 0;
            var b1 = Boolean.TryParse(Request["IsCaAwRest"], out xB) ? Convert.ToBoolean(Request["IsCaAwRest"]) : xB;
            var b2 = Boolean.TryParse(Request["IsHidden"], out xB) ? Convert.ToBoolean(Request["IsHidden"]) : xB;
            var b3 = Boolean.TryParse(Request["IsCurModel"], out xB) ? Convert.ToBoolean(Request["IsCurModel"]) : xB;
            var b4 = Boolean.TryParse(Request["MissOrAll"], out xB) ? Convert.ToBoolean(Request["MissOrAll"]) : xB;
            var b5 = Boolean.TryParse(Request["IsOnDataFeed"], out xB)
                ? Convert.ToBoolean(Request["IsOnDataFeed"])
                : xB;
            var b6 = Boolean.TryParse(Request["IsCaLegal"], out xB) ? Convert.ToBoolean(Request["IsCaLegal"]) : xB;
            var b7 = Boolean.TryParse(Request["IsCaRoster"], out xB) ? Convert.ToBoolean(Request["IsCaRoster"]) : xB;
            var b8 = Boolean.TryParse(Request["IsCaSaRev"], out xB) ? Convert.ToBoolean(Request["IsCaSaRev"]) : xB;
            var b9 = Boolean.TryParse(Request["IsCaSsPst"], out xB) ? Convert.ToBoolean(Request["IsCaSsPst"]) : xB;
            var b10 = Boolean.TryParse(Request["IsCaCurRel"], out xB) ? Convert.ToBoolean(Request["IsCaCurRel"]) : xB;
            var b11 = Boolean.TryParse(Request["IsCaPpt"], out xB) ? Convert.ToBoolean(Request["IsCaPpt"]) : xB;

            var g = GetSectionFromLink(v1);
            var ca = new CaRestrictModel(b6, b7, b11, b10, b8, b9);
            g.ManufId = v2;
            g.GunTypeId = v3;
            g.IsHidden = b2;
            g.IsCurModel = b3;
            //g.CaOkay = b6;
            //g.CaRosterOk = b7;
            //g.CaSglActnOk = b8;
            //g.CaSglShotOk = b9;
            //g.CaCurioOk = b10;
            //g.CaPptOk = b11;
            g.SearchText = "";
            g.Filters.IsCaAwRestricted = b1;
            g.Filters.IsMissingAll = b4;
            g.Filters.IsOnDataFeed = b5;
            g.CaRestrict = ca;

            var m = new MenuModel(g, 2);

            return Json(new {Caliber = m.FeedCaliber, Action = m.FeedAction, DaysPast = m.FeedDaysPast},
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetCaliber()
        {
            var xInt1 = 0;
            var xB = false;
            var v1 = Request["PageUrl"];
            var v2 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var v3 = Int32.TryParse(Request["GunTypeId"], out xInt1) ? Convert.ToInt32(Request["GunTypeId"]) : 0;
            var v4 = Int32.TryParse(Request["CaliberId"], out xInt1) ? Convert.ToInt32(Request["CaliberId"]) : 0;
            var b1 = Boolean.TryParse(Request["IsCaAwRest"], out xB) ? Convert.ToBoolean(Request["IsCaAwRest"]) : xB;
            var b2 = Boolean.TryParse(Request["IsHidden"], out xB) ? Convert.ToBoolean(Request["IsHidden"]) : xB;
            var b3 = Boolean.TryParse(Request["IsCurModel"], out xB) ? Convert.ToBoolean(Request["IsCurModel"]) : xB;
            var b4 = Boolean.TryParse(Request["MissOrAll"], out xB) ? Convert.ToBoolean(Request["MissOrAll"]) : xB;
            var b5 = Boolean.TryParse(Request["IsOnDataFeed"], out xB)
                ? Convert.ToBoolean(Request["IsOnDataFeed"])
                : xB;
            var b6 = Boolean.TryParse(Request["IsCaLegal"], out xB) ? Convert.ToBoolean(Request["IsCaLegal"]) : xB;
            var b7 = Boolean.TryParse(Request["IsCaRoster"], out xB) ? Convert.ToBoolean(Request["IsCaRoster"]) : xB;
            var b8 = Boolean.TryParse(Request["IsCaSaRev"], out xB) ? Convert.ToBoolean(Request["IsCaSaRev"]) : xB;
            var b9 = Boolean.TryParse(Request["IsCaSsPst"], out xB) ? Convert.ToBoolean(Request["IsCaSsPst"]) : xB;
            var b10 = Boolean.TryParse(Request["IsCaCurRel"], out xB) ? Convert.ToBoolean(Request["IsCaCurRel"]) : xB;
            var b11 = Boolean.TryParse(Request["IsCaPpt"], out xB) ? Convert.ToBoolean(Request["IsCaPpt"]) : xB;

            var g = GetSectionFromLink(v1);
            var ca = new CaRestrictModel(b6, b7, b11, b10, b8, b9);
            g.ManufId = v2;
            g.GunTypeId = v3;
            g.CaliberId = v4;
            g.IsHidden = b2;
            g.IsCurModel = b3;
            //g.CaOkay = b6;
            //g.CaRosterOk = b7;
            //g.CaSglActnOk = b8;
            //g.CaSglShotOk = b9;
            //g.CaCurioOk = b10;
            //g.CaPptOk = b11;
            g.SearchText = "";
            g.Filters.IsCaAwRestricted = b1;
            g.Filters.IsMissingAll = b4;
            g.Filters.IsOnDataFeed = b5;
            g.CaRestrict = ca;

            var m = new MenuModel(g, 3);

            return Json(new {Caliber = m.FeedCaliber, Action = m.FeedAction, DaysPast = m.FeedDaysPast},
                JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SetAction()
        {
            var xInt1 = 0;
            var xB = false;
            var v1 = Request["PageUrl"];
            var v2 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var v3 = Int32.TryParse(Request["GunTypeId"], out xInt1) ? Convert.ToInt32(Request["GunTypeId"]) : 0;
            var v4 = Int32.TryParse(Request["CaliberId"], out xInt1) ? Convert.ToInt32(Request["CaliberId"]) : 0;
            var v5 = Int32.TryParse(Request["ActionId"], out xInt1) ? Convert.ToInt32(Request["ActionId"]) : 0;
            var b1 = Boolean.TryParse(Request["IsCaAwRest"], out xB) ? Convert.ToBoolean(Request["IsCaAwRest"]) : xB;
            var b2 = Boolean.TryParse(Request["IsHidden"], out xB) ? Convert.ToBoolean(Request["IsHidden"]) : xB;
            var b3 = Boolean.TryParse(Request["IsCurModel"], out xB) ? Convert.ToBoolean(Request["IsCurModel"]) : xB;
            var b4 = Boolean.TryParse(Request["MissOrAll"], out xB) ? Convert.ToBoolean(Request["MissOrAll"]) : xB;
            var b5 = Boolean.TryParse(Request["IsOnDataFeed"], out xB)
                ? Convert.ToBoolean(Request["IsOnDataFeed"])
                : xB;
            var b6 = Boolean.TryParse(Request["IsCaLegal"], out xB) ? Convert.ToBoolean(Request["IsCaLegal"]) : xB;
            var b7 = Boolean.TryParse(Request["IsCaRoster"], out xB) ? Convert.ToBoolean(Request["IsCaRoster"]) : xB;
            var b8 = Boolean.TryParse(Request["IsCaSaRev"], out xB) ? Convert.ToBoolean(Request["IsCaSaRev"]) : xB;
            var b9 = Boolean.TryParse(Request["IsCaSsPst"], out xB) ? Convert.ToBoolean(Request["IsCaSsPst"]) : xB;
            var b10 = Boolean.TryParse(Request["IsCaCurRel"], out xB) ? Convert.ToBoolean(Request["IsCaCurRel"]) : xB;
            var b11 = Boolean.TryParse(Request["IsCaPpt"], out xB) ? Convert.ToBoolean(Request["IsCaPpt"]) : xB;

            var g = GetSectionFromLink(v1);
            var ca = new CaRestrictModel(b6, b7, b11, b10, b8, b9);
            g.ManufId = v2;
            g.GunTypeId = v3;
            g.CaliberId = v4;
            g.ActionId = v5;
            g.IsHidden = b2;
            g.IsCurModel = b3;
            //g.CaOkay = b6;
            //g.CaRosterOk = b7;
            //g.CaSglActnOk = b8;
            //g.CaSglShotOk = b9;
            //g.CaCurioOk = b10;
            //g.CaPptOk = b11;
            g.Filters.IsCaAwRestricted = b1;
            g.Filters.IsMissingAll = b4;
            g.Filters.IsOnDataFeed = b5;
            g.CaRestrict = ca;

            g.SearchText = "";

            var m = new MenuModel(g, 4);

            return Json(new {Caliber = m.FeedCaliber, Action = m.FeedAction, DaysPast = m.FeedDaysPast},
                JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SetDaysBack()
        {
            var xInt1 = 0;
            var xB = false;
            var v1 = Request["PageUrl"];
            var v2 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var v3 = Int32.TryParse(Request["GunTypeId"], out xInt1) ? Convert.ToInt32(Request["GunTypeId"]) : 0;
            var v4 = Int32.TryParse(Request["CaliberId"], out xInt1) ? Convert.ToInt32(Request["CaliberId"]) : 0;
            var v5 = Int32.TryParse(Request["ActionId"], out xInt1) ? Convert.ToInt32(Request["ActionId"]) : 0;
            var v6 = Int32.TryParse(Request["DaysBack"], out xInt1) ? Convert.ToInt32(Request["DaysBack"]) : 0;
            var b1 = Boolean.TryParse(Request["IsCaAwRest"], out xB) ? Convert.ToBoolean(Request["IsCaAwRest"]) : xB;
            var b2 = Boolean.TryParse(Request["IsHidden"], out xB) ? Convert.ToBoolean(Request["IsHidden"]) : xB;
            var b3 = Boolean.TryParse(Request["IsCurModel"], out xB) ? Convert.ToBoolean(Request["IsCurModel"]) : xB;
            var b4 = Boolean.TryParse(Request["MissOrAll"], out xB) ? Convert.ToBoolean(Request["MissOrAll"]) : xB;
            var b5 = Boolean.TryParse(Request["IsOnDataFeed"], out xB)
                ? Convert.ToBoolean(Request["IsOnDataFeed"])
                : xB;
            var b6 = Boolean.TryParse(Request["IsCaLegal"], out xB) ? Convert.ToBoolean(Request["IsCaLegal"]) : xB;
            var b7 = Boolean.TryParse(Request["IsCaRoster"], out xB) ? Convert.ToBoolean(Request["IsCaRoster"]) : xB;
            var b8 = Boolean.TryParse(Request["IsCaSaRev"], out xB) ? Convert.ToBoolean(Request["IsCaSaRev"]) : xB;
            var b9 = Boolean.TryParse(Request["IsCaSsPst"], out xB) ? Convert.ToBoolean(Request["IsCaSsPst"]) : xB;
            var b10 = Boolean.TryParse(Request["IsCaCurRel"], out xB) ? Convert.ToBoolean(Request["IsCaCurRel"]) : xB;
            var b11 = Boolean.TryParse(Request["IsCaPpt"], out xB) ? Convert.ToBoolean(Request["IsCaPpt"]) : xB;

            var g = GetSectionFromLink(v1);
            var ca = new CaRestrictModel(b6, b7, b11, b10, b8, b9);
            g.ManufId = v2;
            g.GunTypeId = v3;
            g.CaliberId = v4;
            g.ActionId = v5;
            g.IsHidden = b2;
            g.IsCurModel = b3;
            //g.CaOkay = b6;
            //g.CaRosterOk = b7;
            //g.CaSglActnOk = b8;
            //g.CaSglShotOk = b9;
            //g.CaCurioOk = b10;
            //g.CaPptOk = b11;
            g.Filters.IsCaAwRestricted = b1;
            g.Filters.IsMissingAll = b4;
            g.Filters.IsOnDataFeed = b5;
            g.Filters.DaysBackToSearch = v6;
            g.CaRestrict = ca;

            g.SearchText = "";

            var m = new MenuModel(g, 5);

            return Json(new {Caliber = m.FeedCaliber, Action = m.FeedAction, DaysPast = m.FeedDaysPast},
                JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateImage()
        {

            var url = string.Empty;

            if (Request.Files.Count > 0)
            {

                HttpFileCollectionBase files = Request.Files;
                var x0 = 0;
                var id = Int32.TryParse(Request["MasterId"], out x0) ? Convert.ToInt32(Request["MasterId"]) : 0;
                var imgName = Request["NewImg"];
                var aPath = ConfigurationHelper.GetPropertyValue("application", "p13"); 
                var bm = new BaseModel();
                var di = bm.DecryptIt(aPath);

                var fullPath = String.Format("{0}T\\{1}.jpg", di, imgName);
                files[0].SaveAs(fullPath);

                if (System.IO.File.Exists(fullPath))
                {
                    ImageBase.MountDataAdminImage(fullPath, di, imgName);

                    ItemData d = new ItemData();
                    d.ID = id;
                    d.DistributorId = (int) Distributors.ALT;
                    d.ImageName = imgName + ".jpg";

                    var dfc = new DataFeedContext();
                    dfc.UpdateHouseImage(d);

                    var a7 = ConfigurationHelper.GetPropertyValue("application", "a7"); 
                    url = string.Format("{0}/{1}{2}", bm.GetHostUrl(), bm.DecryptIt(a7), d.ImageName);

                    //url = dfc.CookImgUrl(d.DistributorId, d.ImageName);

                }
            }

            return Json(new {ImgUrl = url}, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public JsonResult UpdateHseImage()
        //{

        //    var url = string.Empty;

        //    if (Request.Files.Count > 0)
        //    {

        //        HttpFileCollectionBase files = Request.Files;
        //        var x0 = 0;
        //        var id = Int32.TryParse(Request["MasterId"], out x0) ? Convert.ToInt32(Request["MasterId"]) : 0;
        //        var imgName = Request["ImgName"].Replace(".jpg", string.Empty);
        //        var aPath = ConfigurationHelper.GetPropertyValue("application", "PathHseFeed");

        //        var fullPath = String.Format("{0}T\\{1}.jpg", aPath, imgName);
        //        files[0].SaveAs(fullPath);

        //        if (System.IO.File.Exists(fullPath))
        //        {
        //            ImageBase.MountDataAdminImage(fullPath, aPath, imgName);

        //            ItemData d = new ItemData();
        //            d.ID = id;
        //            d.DistributorId = (int)Distributors.ALT;
        //            d.ImageName = imgName + ".jpg";

        //            var dfc = new DataFeedContext();
        //            dfc.UpdateHouseImage(d);

        //            url = dfc.CookImgUrl(d.DistributorId, d.ImageName);

        //        }
        //    }

        //    return Json(new { ImgUrl = url }, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public JsonResult UpdateMenuItem()
        {

            var x0 = 0;
            var v1 = Int32.TryParse(Request["MstId"], out x0) ? Convert.ToInt32(Request["MstId"]) : 0;
            var v2 = Int32.TryParse(Request["CatId"], out x0) ? Convert.ToInt32(Request["CatId"]) : 0;
            var v3 = Int32.TryParse(Request["ValId"], out x0) ? Convert.ToInt32(Request["ValId"]) : 0;

            var gm = new GunModel();
            gm.MasterId = v1;
            gm.Id = v2;
            gm.ValueId = v3;

            var dfc = new DataFeedContext();
            dfc.UpdateMenuItem(gm);

            return Json("success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateDataItem()
        {
            var b0 = false;
            var i0 = 0;
            var d0 = 0.000;
            var d2 = 0.00;

            var v1 = Request["Mdl"];
            var v2 = Request["Upc"];
            var v3 = Request["Mpn"];
            var v4 = Request["Dsc"];
            var v5 = Request["Lds"];

            var v0 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : 0;
            var v6 = Int32.TryParse(Request["Cap"], out i0) ? Convert.ToInt32(Request["Cap"]) : 0;

            var i1 = Int32.TryParse(Request["Gtp"], out i0) ? Convert.ToInt32(Request["Gtp"]) : 0;
            var i2 = Int32.TryParse(Request["Cal"], out i0) ? Convert.ToInt32(Request["Cal"]) : 0;
            var i3 = Int32.TryParse(Request["Atn"], out i0) ? Convert.ToInt32(Request["Atn"]) : 0;
            var i4 = Int32.TryParse(Request["Fin"], out i0) ? Convert.ToInt32(Request["Fin"]) : 0;
            var i5 = Int32.TryParse(Request["Cnd"], out i0) ? Convert.ToInt32(Request["Cnd"]) : 0;

            var v7 = Double.TryParse(Request["Brl"], out d0) ? Convert.ToDouble(Request["Brl"]) : d0;
            var v8 = Double.TryParse(Request["Ovl"], out d0) ? Convert.ToDouble(Request["Ovl"]) : d0;
            var v9 = Double.TryParse(Request["Chm"], out d0) ? Convert.ToDouble(Request["Chm"]) : d0;
            var v10 = Double.TryParse(Request["Wgt"], out d2) ? Convert.ToDouble(Request["Wgt"]) : d2;

            var v11 = Boolean.TryParse(Request["Atv"], out b0) ? Convert.ToBoolean(Request["Atv"]) : b0;
            var v12 = Boolean.TryParse(Request["Ver"], out b0) ? Convert.ToBoolean(Request["Ver"]) : b0;
            var v13 = Boolean.TryParse(Request["Hid"], out b0) ? Convert.ToBoolean(Request["Hid"]) : b0;
            var v14 = Boolean.TryParse(Request["Ofd"], out b0) ? Convert.ToBoolean(Request["Ofd"]) : b0;
            var v15 = Boolean.TryParse(Request["Cur"], out b0) ? Convert.ToBoolean(Request["Cur"]) : b0;
            var v16 = Boolean.TryParse(Request["Ovs"], out b0) ? Convert.ToBoolean(Request["Ovs"]) : b0;
            var v17 = Boolean.TryParse(Request["Hca"], out b0) ? Convert.ToBoolean(Request["Hca"]) : b0;
            var v18 = Boolean.TryParse(Request["Cok"], out b0) ? Convert.ToBoolean(Request["Cok"]) : b0;
            var v19 = Boolean.TryParse(Request["Obx"], out b0) ? Convert.ToBoolean(Request["Obx"]) : b0;
            var v20 = Boolean.TryParse(Request["Ppw"], out b0) ? Convert.ToBoolean(Request["Ppw"]) : b0;
            var v21 = Boolean.TryParse(Request["Ffl"], out b0) ? Convert.ToBoolean(Request["Ffl"]) : b0;
            var v22 = Boolean.TryParse(Request["Usd"], out b0) ? Convert.ToBoolean(Request["Usd"]) : b0;
            var v23 = Boolean.TryParse(Request["Rst"], out b0) ? Convert.ToBoolean(Request["Rst"]) : b0;
            var v24 = Boolean.TryParse(Request["Cnr"], out b0) ? Convert.ToBoolean(Request["Cnr"]) : b0;
            var v25 = Boolean.TryParse(Request["Sar"], out b0) ? Convert.ToBoolean(Request["Sar"]) : b0;
            var v26 = Boolean.TryParse(Request["Ssp"], out b0) ? Convert.ToBoolean(Request["Ssp"]) : b0;
            var v27 = Boolean.TryParse(Request["Ppt"], out b0) ? Convert.ToBoolean(Request["Ppt"]) : b0;
            var v28 = Boolean.TryParse(Request["Leo"], out b0) ? Convert.ToBoolean(Request["Leo"]) : b0;


            var wtLb = Convert.ToInt32(v10);
            var wtOz = v10 - Math.Floor(v10);

            var f = new FilterModel();
            f.IsOnDataFeed = v14;
            f.IsLeo = v28;

            var ca = new CaRestrictModel(v17, v18, v23, v27, v24, v25, v26);
            var gm = new GunModel(v0, v6, wtLb, i1, i2, i3, i4, i5, v7, v8, v9, wtOz, v11, v12, v13, v15, v16, v19, v20, v21, v22, v1, v2, v3, v4, v5, f, ca);

            var dfc = new DataFeedContext();
            dfc.UpdateDataFeedItem(gm);

            return Json("success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetDuplicates()
        {
            var xInt1 = 0;

            var v1 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var v2 = Int32.TryParse(Request["GtpId"], out xInt1) ? Convert.ToInt32(Request["GtpId"]) : 0;
            var v3 = Int32.TryParse(Request["CalId"], out xInt1) ? Convert.ToInt32(Request["CalId"]) : 0;
            var v4 = Int32.TryParse(Request["FilId"], out xInt1) ? Convert.ToInt32(Request["FilId"]) : 1;
            var v5 = Int32.TryParse(Request["GunsPerPg"], out xInt1) ? Convert.ToInt32(Request["GunsPerPg"]) : 0;
            var v6 = Int32.TryParse(Request["StartRow"], out xInt1) ? Convert.ToInt32(Request["StartRow"]) : 0;
            var v7 = Request["TxtSch"];

            var f = new FilterModel();
            var g = new GunModel(f);
            g.ValueId = v4;
            g.ManufId = v1;
            g.GunTypeId = v2;
            g.CaliberId = v3;
            g.Filters.PagingMaxRows = v5;
            g.Filters.PagingStartRow = v6;
            g.SearchText = v7;

            DataFeedContext df = new DataFeedContext();
            var guns = df.GetDuplicateGuns(g);
            var ct = guns.Count;
            var cm = new CountModel();

            if (ct > 0)
            {
                cm = guns.FirstOrDefault().Count;
                guns.RemoveAt(0);
            }

            var jsonResult = Json(new {Count = cm, Guns = guns}, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        [HttpPost]
        public JsonResult ReplaceDuplicate()
        {
            var xInt1 = 0;

            var v1 = Int32.TryParse(Request["OldGunId"], out xInt1) ? Convert.ToInt32(Request["OldGunId"]) : 0;
            var v2 = Int32.TryParse(Request["NewMstId"], out xInt1) ? Convert.ToInt32(Request["NewMstId"]) : 0;

            DataFeedContext df = new DataFeedContext();
            df.MergeDuplicateGun(v1, v2);

            return Json("success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SetDupMenus()
        {
            var xInt1 = 0;
            var v0 = Int32.TryParse(Request["FilId"], out xInt1) ? Convert.ToInt32(Request["FilId"]) : 1;

            var g = new GunModel();
            g.ValueId = v0;
            var m = new MenuModel(g, DupMenus.Unknown);

            return Json(new {Manuf = m.FeedManuf, GunType = m.FeedGunType, Caliber = m.FeedCaliber},
                JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SetDupMenuMfg()
        {
            var xInt1 = 0;
            var v0 = Int32.TryParse(Request["FilId"], out xInt1) ? Convert.ToInt32(Request["FilId"]) : 1;
            var v1 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;

            var g = new GunModel();
            g.ValueId = v0;
            g.ManufId = v1;
            var m = new MenuModel(g, DupMenus.Mfg);

            return Json(new {GunType = m.FeedGunType, Caliber = m.FeedCaliber}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SetDupMenuGtp()
        {
            var xInt1 = 0;
            var v0 = Int32.TryParse(Request["FilId"], out xInt1) ? Convert.ToInt32(Request["FilId"]) : 1;
            var v1 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var v2 = Int32.TryParse(Request["GtpId"], out xInt1) ? Convert.ToInt32(Request["GtpId"]) : 0;

            var g = new GunModel();
            g.ValueId = v0;
            g.ManufId = v1;
            g.GunTypeId = v2;
            var m = new MenuModel(g, DupMenus.Gtp);

            return Json(new {Caliber = m.FeedCaliber}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDistMapCodes()
        {
            var xInt1 = 0;
            var v0 = Int32.TryParse(Request["DistId"], out xInt1) ? Convert.ToInt32(Request["DistId"]) : 0;

            var g = new GunModel();
            g.ValueId = v0;
            var mm = new MenuModel();
            var data = mm.MfgMapCodes(v0);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AssignToMfg()
        {
            var xInt1 = 0;
            var v0 = Int32.TryParse(Request["DistId"], out xInt1) ? Convert.ToInt32(Request["DistId"]) : 0;
            var v1 = Int32.TryParse(Request["ManufId"], out xInt1) ? Convert.ToInt32(Request["ManufId"]) : 0;
            var v2 = Request["MapCode"];

            var df = new DataFeedContext();
            df.MapToManuf(v0, v1, v2);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CreateNewMfg()
        {
            var xInt1 = 0;
            var v0 = Int32.TryParse(Request["DistId"], out xInt1) ? Convert.ToInt32(Request["DistId"]) : 0;
            var v1 = Request["MapCode"];
            var v2 = Request["NewMfg"];


            var df = new DataFeedContext();
            df.CreateManuf(v0, v1, v2);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetManufGrid()
        {

            DataFeedContext df = new DataFeedContext();

            var xInt1 = 0;
            var xB = false;
            var v1 = Int32.TryParse(Request["DistId"], out xInt1) ? Convert.ToInt32(Request["DistId"]) : 0;
            var v2 = Boolean.TryParse(Request["IsOnFeed"], out xB) ? Convert.ToBoolean(Request["IsOnFeed"]) : xB;
            var v3 = Boolean.TryParse(Request["IsParent"], out xB) ? Convert.ToBoolean(Request["IsParent"]) : xB;

            var m = new ManufModel();
            m.DistId = v1;
            m.IsOnFeed = v2;
            m.IsParentOnly = v3;

            var mfg = df.GetEditMfgGrid(m);
            var ct = mfg.FirstOrDefault().ItemCount;

            mfg.RemoveAt(0);

            var jsonResult = Json(new { Count = ct, Mfg = mfg }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        [HttpPost]
        public JsonResult HomeScrollGrid()
        {
            var xB = false;
            var v1 = Boolean.TryParse(Request["IsActive"], out xB) ? Convert.ToBoolean(Request["IsActive"]) : xB;
            var v2 = Boolean.TryParse(Request["IsOnFeed"], out xB) ? Convert.ToBoolean(Request["IsOnFeed"]) : xB;
            var v3 = Boolean.TryParse(Request["IsParent"], out xB) ? Convert.ToBoolean(Request["IsParent"]) : xB;

            var m = new ManufModel();
            m.IsActive = v1;
            m.IsOnFeed = v2;
            m.IsParentOnly = v3;

            DataFeedContext fc = new DataFeedContext();
            var x = fc.GetHomeScrollGrid(m);

            return Json(new { SA = x.IsScrollActive, MF = x.Manuf }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateHomeScroll()
        {
            var x0 = 0;
            var xB = false;
            var i1 = Int32.TryParse(Request["MfgId"], out x0) ? Convert.ToInt32(Request["MfgId"]) : x0;
            var i2 = Int32.TryParse(Request["ImgCt"], out x0) ? Convert.ToInt32(Request["ImgCt"]) : x0;
            var b1 = Boolean.TryParse(Request["IsAtv"], out xB) ? Convert.ToBoolean(Request["IsAtv"]) : xB;
            var v1 = Request["Color"].Length > 0 ? Request["Color"] : null;
            var v2 = Request["MfUrl"].Length > 0 ? Request["MfUrl"] : null;
            var v3 = Request["ImgNm"].Length > 0 ? Request["ImgNm"] : null;
            var b2 = false;

            var fullPath = string.Empty;
            var aPath = ConfigurationHelper.GetPropertyValue("application", "p6");

            var bm = new BaseModel();
            var di = bm.DecryptIt(aPath);

            /* IMAGE WAS REMOVED ON GRID - DELETE IN DIRECTORY */
            if (i2 == 0 && v3.Length > 0)
            {
                fullPath = String.Format("{0}{1}", di, v3);
                if (System.IO.File.Exists(fullPath)) { System.IO.File.Delete(fullPath); }
                v3 = null;
                b2 = true;
            }

            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                v3 = v3 ?? files[0].FileName;
                fullPath = String.Format("{0}\\{1}", di, v3);
                files[0].SaveAs(fullPath);
                b2 = true;
            }

            DataFeedContext df = new DataFeedContext();
            var m = new ManufModel(i1, b1, b2, v1, v2, v3);
            df.UpdateHomeScrollGrid(m);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult NixScrollImg()
        {

            var m = new BaseModel();
            var bPath = ConfigurationHelper.GetPropertyValue("application", "p6");

            var x0 = 0;
            var v1 = Int32.TryParse(Request["Id"], out x0) ? Convert.ToInt32(Request["Id"]) : x0;
            var v2 = Request["Img"];

            var f = string.Format("{0}\\{1}", m.DecryptIt(bPath), v2);

            if (System.IO.File.Exists(f))
            {
                DataFeedContext df = new DataFeedContext();
                System.IO.File.Delete(f);
                df.DeleteScrollImage(v1);
            }


            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateHomeScrollStatus()
        {
            var xB = false;
            var b1 = Boolean.TryParse(Request["IsAtv"], out xB) ? Convert.ToBoolean(Request["IsAtv"]) : xB;
            DataFeedContext df = new DataFeedContext();
            df.UpdateHomeScrollStatus(b1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateManuf()
        {
            var xInt1 = 0;
            var xB = false;
            var v1 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var v2 = Int32.TryParse(Request["ParId"], out xInt1) ? Convert.ToInt32(Request["ParId"]) : 0;
            var v3 = Request["MfgTx"];
            var v4 = Request["UrlTx"];
            var v5 = Boolean.TryParse(Request["IsDfd"], out xB) ? Convert.ToBoolean(Request["IsDfd"]) : xB;
            var v6 = Boolean.TryParse(Request["IsPar"], out xB) ? Convert.ToBoolean(Request["IsPar"]) : xB;

            DataFeedContext df = new DataFeedContext();

            var m = new ManufModel();
            m.ManufId = v1;
            m.ParentId = v2;
            m.ManufName = v3;
            m.ManufUrl = v4;
            m.IsOnFeed = v5;
            m.IsActive = v6;

            df.UpdateFeedMfg(m);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateFeedImage()
        {
            var xInt1 = 0;
            var v1 = Int32.TryParse(Request["MstId"], out xInt1) ? Convert.ToInt32(Request["MstId"]) : 0;
            var v2 = Int32.TryParse(Request["DstId"], out xInt1) ? Convert.ToInt32(Request["DstId"]) : 0;

            DataFeedContext df = new DataFeedContext();

            var m = new GunModel();
            m.ManufId = v1;
            m.ImageDist = v2;

            df.UpdateFeedImage(m);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetImages()
        {
            var xInt1 = 0;
            var xB = false;

            var v1 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var v2 = Int32.TryParse(Request["GtpId"], out xInt1) ? Convert.ToInt32(Request["GtpId"]) : 0;
            var v3 = Int32.TryParse(Request["CalId"], out xInt1) ? Convert.ToInt32(Request["CalId"]) : 0;
            var v4 = Boolean.TryParse(Request["IsMis"], out xB) ? Convert.ToBoolean(Request["IsMis"]) : xB;
            var v5 = Int32.TryParse(Request["GunsPerPg"], out xInt1) ? Convert.ToInt32(Request["GunsPerPg"]) : 0;
            var v6 = Int32.TryParse(Request["StartRow"], out xInt1) ? Convert.ToInt32(Request["StartRow"]) : 0;

            var f = new FilterModel();
            var g = new GunModel(f);
            g.ManufId = v1;
            g.GunTypeId = v2;
            g.CaliberId = v3;
            g.ItemMissing = v4;
            g.Filters.PagingMaxRows = v5;
            g.Filters.PagingStartRow = v6;
 
            DataFeedContext df = new DataFeedContext();
            var im = df.GetImages(g);

            var jsonResult = Json(new { Count = im.ItemCount, Imgs = im.Images }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;





        }


        [HttpPost]
        public JsonResult SetImgMenuAll()
        {
            var xB = false;
            var v1 = Boolean.TryParse(Request["IsMis"], out xB) ? Convert.ToBoolean(Request["IsMis"]) : xB;

            var g = new GunModel();
            g.ItemMissing = v1;
            var m = new MenuModel(g, ImgMenus.Unknown);

            return Json(new { Manuf = m.FeedManuf, GunType = m.FeedGunType, Caliber = m.FeedCaliber }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult SetImgMenuMfg()
        {
            var xInt1 = 0;
            var xB = false;
            var v1 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var v2 = Boolean.TryParse(Request["IsMis"], out xB) ? Convert.ToBoolean(Request["IsMis"]) : xB;

            var g = new GunModel();
            g.ManufId = v1;
            g.ItemMissing = v2;
            var m = new MenuModel(g, ImgMenus.Mfg);

            return Json(new { GunType = m.FeedGunType, Caliber = m.FeedCaliber }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SetImgMenuGtp()
        {
            var xInt1 = 0;
            var xB = false;
            var v1 = Int32.TryParse(Request["MfgId"], out xInt1) ? Convert.ToInt32(Request["MfgId"]) : 0;
            var v2 = Int32.TryParse(Request["GtpId"], out xInt1) ? Convert.ToInt32(Request["GtpId"]) : 0;
            var v3 = Boolean.TryParse(Request["IsMis"], out xB) ? Convert.ToBoolean(Request["IsMis"]) : xB;

            var g = new GunModel();
            g.ManufId = v1;
            g.GunTypeId = v2;
            g.ItemMissing = v3;
            var m = new MenuModel(g, ImgMenus.Gtp);

            return Json(new { Caliber = m.FeedCaliber }, JsonRequestBehavior.AllowGet);
        }

    }
}