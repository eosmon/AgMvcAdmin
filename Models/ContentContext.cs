using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgMvcAdmin.Models.Common;
using AppBase;
using WebBase.Configuration;

namespace AgMvcAdmin.Models
{
    public class ContentContext : BaseModel
    {

        public string BPathDir = ConfigurationHelper.GetPropertyValue("application", "a1");
        public string BDir = ConfigurationHelper.GetPropertyValue("application", "a2");

        #region GetALL

        public ContentModel GetPageContent(int pageId)
        {
            var cm = new ContentModel();

            using (var conn = new SqlConnection(WebSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentGetPage");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@PageID", SqlDbType.Int) { Value = pageId };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cm;
                var x0 = 0;
                var b0 = false;

                var ltt = new List<ContentToolTip>();
                var lcc = new List<CampaignModel>();
                var lhc = new List<HomeModelConfig>();

                dr.Read();
                {
                    var id = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                    var stId = Int32.TryParse(dr["StaticID"].ToString(), out x0) ? Convert.ToInt32(dr["StaticID"]) : 0;
                    var isStat = Boolean.TryParse(dr["HasStatic"].ToString(), out b0) ? Convert.ToBoolean(dr["HasStatic"]) : false;
                    var isHdr = Boolean.TryParse(dr["HasHeader"].ToString(), out b0) ? Convert.ToBoolean(dr["HasHeader"]) : false;
                    var tPgNm = dr["PageName"].ToString();
                    var tSeoTtl = dr["SeoTitle"].ToString();
                    var tSeoKw = dr["SeoKeywords"].ToString();
                    var tSeoOg = dr["SeoOgType"].ToString();
                    var tSeoDesc = dr["SeoDesc"].ToString();
                    var hTitle = dr["HeaderTitle"].ToString();
                    var hTxt = dr["HeaderText"].ToString();
                    var hImg = dr["HeaderImg"].ToString();
                    var tStatic = dr["StaticTxt"].ToString();

                    var imgUrl = string.Empty;

                    if (hImg.Length > 0) { imgUrl = CookBaseUrl(GetHostUrl(), "Headers", hImg); }

                    cm = new ContentModel(id, stId, isHdr, isStat, tPgNm, tSeoTtl, tSeoKw, tSeoOg, tSeoDesc, hTitle, hTxt, imgUrl, tStatic);
                }

                dr.NextResult();
                if (!dr.HasRows) return cm;

                var tt = new ContentToolTip();

                while (dr.Read())
                {
                    var isTt = Boolean.TryParse(dr["HasToolTip"].ToString(), out b0) ? Convert.ToBoolean(dr["HasToolTip"]) : b0;
                    tt.HasToolTip = isTt;
                    if (!isTt)
                    {
                        ltt.Add(tt);
                        break;
                    }

                    var ttId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                    var ttDesc = dr["TipDesc"].ToString();
                    var ttTxt = dr["ToolTipTxt"].ToString();
                    tt = new ContentToolTip(true, ttId, ttDesc, ttTxt);
                    ltt.Add(tt);
                }

                cm.ToolTips = ltt;

                dr.NextResult();
                if (!dr.HasRows) return cm;

                var cb = new CampaignModel();

                while (dr.Read())
                {
                    var isBr = Boolean.TryParse(dr["HasCampaign"].ToString(), out b0) ? Convert.ToBoolean(dr["HasCampaign"]) : b0;
                    cb.HasCampaign = isBr;
                    if (!isBr) { break; }

                    var cId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                    var cPs = Int32.TryParse(dr["PositionID"].ToString(), out x0) ? Convert.ToInt32(dr["PositionID"]) : 0;
                    var cCt = Int32.TryParse(dr["BannerCount"].ToString(), out x0) ? Convert.ToInt32(dr["BannerCount"]) : 0;
                    var cTo = Int32.TryParse(dr["ShowDelay"].ToString(), out x0) ? Convert.ToInt32(dr["ShowDelay"]) : 0;
                    var cNm = dr["CampaignName"].ToString();

                    cb = new CampaignModel(true, cId, cPs, cCt, cTo, cNm);
                    lcc.Add(cb);
                }

                cm.Campaigns = lcc;

                /* ADDED */

                dr.NextResult();
                if (!dr.HasRows) return cm;

                var hc = new HomeModelConfig();

                while (dr.Read())
                {
                    var isHp = Boolean.TryParse(dr["IsHomePage"].ToString(), out b0) ? Convert.ToBoolean(dr["IsHomePage"]) : b0;
                    var ftCt = Int32.TryParse(dr["FeatureCount"].ToString(), out x0) ? Convert.ToInt32(dr["FeatureCount"]) : 0;
                    cm.IsHomePage = isHp;
                    if (!isHp) { break; }

                    if (ftCt > 0)
                    {
                        var isHd = Boolean.TryParse(dr["IsHeader"].ToString(), out b0) ? Convert.ToBoolean(dr["IsHeader"]) : b0;
                        var isPr = Boolean.TryParse(dr["IsPromo"].ToString(), out b0) ? Convert.ToBoolean(dr["IsPromo"]) : b0;
                        var isAv = Boolean.TryParse(dr["IsActive"].ToString(), out b0) ? Convert.ToBoolean(dr["IsActive"]) : b0;
                        var hPt = dr["PromoText"].ToString();

                        var hId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                        var hPs = Int32.TryParse(dr["PositionID"].ToString(), out x0) ? Convert.ToInt32(dr["PositionID"]) : 0;
                        var hOp = Int32.TryParse(dr["OptionID"].ToString(), out x0) ? Convert.ToInt32(dr["OptionID"]) : 0;
                        var hFt = Int32.TryParse(dr["FeatureID"].ToString(), out x0) ? Convert.ToInt32(dr["FeatureID"]) : 0;
                        var hSz = Int32.TryParse(dr["FeatureSizeID"].ToString(), out x0) ? Convert.ToInt32(dr["FeatureSizeID"]) : 0;
                        var hPc = Int32.TryParse(dr["PromoColorID"].ToString(), out x0) ? Convert.ToInt32(dr["PromoColorID"]) : 0;
                        var hGp = Int32.TryParse(dr["GroupID"].ToString(), out x0) ? Convert.ToInt32(dr["GroupID"]) : 0;


                        hc = new HomeModelConfig(hId, hPs, hOp, hFt, hSz, hPc, hGp, isHd, isPr, isAv, hPt);
                        lhc.Add(hc);                        
                    }


                }

                cm.Home = lhc;


            }

            return cm;
        }

        public ContentModel GetAllBannersCampaigns()
        {

            var cm = new ContentModel();
            var lcc = new List<CampaignModel>();
            var lcb = new List<ContentBanner>();

            using (var conn = new SqlConnection(WebSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentAllBannersCampaigns");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var x0 = 0;
                var b0 = false;

                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    var cc = new CampaignModel();

                    while (dr.Read())
                    {
                        var cId = Int32.TryParse(dr["CampaignID"].ToString(), out x0) ? Convert.ToInt32(dr["CampaignID"]) : 0;
                        var cDl = Int32.TryParse(dr["ShowDelay"].ToString(), out x0) ? Convert.ToInt32(dr["ShowDelay"]) : 0;
                        var cNm = dr["CampaignName"].ToString();

                        cc = new CampaignModel(cId, cDl, cNm);
                        lcc.Add(cc);
                    }
                    cm.Campaigns = lcc;
                }

                dr.NextResult();
                if (!dr.HasRows) return cm;

                var cb = new ContentBanner();



                while (dr.Read())
                {

                    var bId = Int32.TryParse(dr["BannerID"].ToString(), out x0) ? Convert.ToInt32(dr["BannerID"]) : 0;
                    var isNw = Boolean.TryParse(dr["NewWindow"].ToString(), out b0) ? Convert.ToBoolean(dr["NewWindow"]) : b0;
                    var bDs = dr["ItemDesc"].ToString();
                    var bIg = dr["ImageUrl"].ToString();
                    var bNv = dr["NavToUrl"].ToString();

                    var imgUrl = string.Empty;

                    if (bIg.Length > 0)
                    { imgUrl = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(BDir), bIg); }  


                    cb = new ContentBanner(bId, bDs, imgUrl, bNv, isNw);
                    lcb.Add(cb);
                }
                cm.Banners = lcb;
            }

            return cm;

        }

        public void AddStaticText(int id, string txt)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentStaticAdd");

            var param = new IDataParameter[2];
            param[0] = new SqlParameter("@PageID", DbType.Int32) { Value = id };
            param[1] = new SqlParameter("@StaticTxt", DbType.String) { Value = txt };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }



        public void UpdateStaticText(int id, string txt)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentStaticUpdate");

            var param = new IDataParameter[2];
            param[0] = new SqlParameter("@ID", DbType.Int32) { Value = id };
            param[1] = new SqlParameter("@StaticTxt", DbType.String) { Value = txt };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }


        public void DeleteStaticText(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentStaticDelete");

            var param = new SqlParameter("@ID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(WebSqlConnection, proc, param);
        }

        #endregion

        #region SEO

        public void UpdateSeo(ContentModel m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentUpdateSeo");

            var param = new IDataParameter[6];
            param[0] = new SqlParameter("@PageID", DbType.Int32) { Value = m.Id };
            param[1] = new SqlParameter("@PageName", DbType.String) { Value = m.PageName };
            param[2] = new SqlParameter("@SeoTitle", DbType.String) { Value = m.SeoTitle };
            param[3] = new SqlParameter("@SeoKeywords", DbType.String) { Value = m.SeoKeywords };
            param[4] = new SqlParameter("@SeoOgType", DbType.String) { Value = m.SeoOgType };
            param[5] = new SqlParameter("@SeoDesc", DbType.String) { Value = m.SeoDesc };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }

        #endregion

        #region Header

        public void AddHeader(ContentModel m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentHeaderAdd");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@PageID", DbType.Int32) { Value = m.Id };
            param[1] = new SqlParameter("@HeaderTitle", DbType.String) { Value = m.HeaderTitle };
            param[2] = new SqlParameter("@HeaderText", DbType.String) { Value = m.HeaderTxt };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }



        public void UpdateHeader(ContentModel m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentUpdateHeader");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@PageID", DbType.Int32) { Value = m.Id };
            param[1] = new SqlParameter("@HeaderTitle", DbType.String) { Value = m.HeaderTitle };
            param[2] = new SqlParameter("@HeaderText", DbType.String) { Value = m.HeaderTxt };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }

        public void UpdateHeaderImg(ContentModel m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentUpdateHeaderImage");

            var param = new IDataParameter[2];
            param[0] = new SqlParameter("@PageID", DbType.Int32) { Value = m.Id };
            param[1] = new SqlParameter("@HeaderImg", DbType.String) { Value = m.HeaderImg };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }

        public void DeleteHeaderPic(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDeleteHeaderPic");

            var param = new SqlParameter("@PageID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(WebSqlConnection, proc, param);
        }

        public void DeleteHeader(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentHeaderDelete");

            var param = new SqlParameter("@PageID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(WebSqlConnection, proc, param);
        }

        

        #endregion

        #region ToolTips

        public List<ContentModel> GetToolTipsByPage(int pageId)
        {
            var l = new List<ContentModel>();

            using (var conn = new SqlConnection(WebSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentGetToolTips");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@PageID", SqlDbType.Int) { Value = pageId };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return l;
                var x0 = 0;


                while (dr.Read())
                {
                    var id = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                    var td = dr["TipDesc"].ToString();
                    var cm = new ContentModel(id, td);
                    l.Add(cm);
                }
            }

            return l;
        }


        public void AddToolTip(ContentModel m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentAddToolTip");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@PageID", DbType.Int32) { Value = m.Id };
            param[1] = new SqlParameter("@TipDesc", DbType.String) { Value = m.ToolTipDesc };
            param[2] = new SqlParameter("@ToolTipTxt", DbType.String) { Value = m.ToolTipText };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }


        public void UpdateToolTip(ContentModel m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentToolTipUpdate");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@ID", DbType.Int32) { Value = m.Id };
            param[1] = new SqlParameter("@TipDesc", DbType.String) { Value = m.ToolTipDesc };
            param[2] = new SqlParameter("@ToolTipTxt", DbType.String) { Value = m.ToolTipText };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }


        public void DeleteToolTip(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentDeleteToolTip");

            var param = new SqlParameter("@ID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(WebSqlConnection, proc, param);
        }
        

        #endregion

        #region Campaigns

        public void DeleteCampaign(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentCampaignDelete");

            var param = new SqlParameter("@ID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(WebSqlConnection, proc, param);
        }

        public void UpdateCampaign(CampaignModel c)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentCampaignUpdate");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@ID", DbType.Int32) { Value = c.CampaignId };
            param[1] = new SqlParameter("@ShowDelay", DbType.String) { Value = c.ShowDelay };
            param[2] = new SqlParameter("@CampaignName", DbType.String) { Value = c.CampaignName };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }

        public int AddCampaign(CampaignModel c)
        {
            var i = 0;

            using (var conn = new SqlConnection(WebSqlConnection))
            {

                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentCampaignAdd");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new IDataParameter[2];
                param[0] = new SqlParameter("@CampaignName", DbType.String) { Value = c.CampaignName };
                param[1] = new SqlParameter("@ShowDelay", DbType.Int32) { Value = c.ShowDelay };
                foreach (var parameter in param) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return i;
                var x0 = 0;

                dr.Read();
                var bId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                i = bId;

            }

            return i;
        }

        public CampaignModel GetCampaignById(int id)
        {

            var cc = new CampaignModel();
            var la = new List<ContentBanner>();
            var lc = new List<ContentBanner>();

            using (var conn = new SqlConnection(WebSqlConnection))
            {

                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentCampaignById");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@CampaignID", DbType.Int32) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                var x0 = 0;

                /* AVAILABLE BANNERS */
                if (!dr.HasRows) { cc.IsAvailAll = false; }
                else
                {
                    var cb = new ContentBanner();
                    cc.IsAvailAll = true;

                    while (dr.Read())
                    {
                        var cId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                        var cDs = dr["ItemDesc"].ToString();
                        var cIg = dr["ImageUrl"].ToString();
                        var cNv = dr["NavToUrl"].ToString();

                        var imgUrl = string.Empty;
                        if (cIg.Length > 0) { imgUrl = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(BDir), cIg); }
                    
                        cb = new ContentBanner(cId, cDs, imgUrl, cNv);
                        la.Add(cb);
                    }

                    cc.AllBanners = la;
                }

                dr.NextResult();

                /* CURRENT BANNERS */
                if (!dr.HasRows) { cc.IsAvailCurrent = false; }
                else
                {
                    var cb = new ContentBanner();
                    cc.IsAvailCurrent = true;

                    while (dr.Read())
                    {
                        var cId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                        var cBr = Int32.TryParse(dr["BannerID"].ToString(), out x0) ? Convert.ToInt32(dr["BannerID"]) : 0;
                        var cSt = Int32.TryParse(dr["SortID"].ToString(), out x0) ? Convert.ToInt32(dr["SortID"]) : 0;
                        var cDs = dr["ItemDesc"].ToString();
                        var cIg = dr["ImageUrl"].ToString();
                        var cNv = dr["NavToUrl"].ToString();

                        var imgUrl = string.Empty;
                        if (cIg.Length > 0) { imgUrl = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(BDir), cIg); }

                        cb = new ContentBanner(cId, cBr, cSt, cDs, imgUrl, cNv);
                        lc.Add(cb);
                    }

                    cc.CurrentBanners = lc;
                }

            }

            return cc;
        }


        public void AddCampaignBanner(int cmpId, int bnrId)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentCampaignBannerAdd");

            var param = new IDataParameter[2];
            param[0] = new SqlParameter("@CampaignID", DbType.Int32) { Value = cmpId };
            param[1] = new SqlParameter("@BannerID", DbType.Int32) { Value = bnrId };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }

        public void DeleteCampaignBanner(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentCampaignBannerDelete");
            var param = new SqlParameter("@ID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(WebSqlConnection, proc, param);
        }
        
        public void SetBannerSort(int id, int sortId)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentCampaignBannerSort");

            var param = new IDataParameter[2];
            param[0] = new SqlParameter("@ID", DbType.Int32) { Value = id };
            param[1] = new SqlParameter("@SortID", DbType.Int32) { Value = sortId };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }

        public List<SelectListItem> GetAvailCampaigns(int pgId)
        {
            var list = new List<SelectListItem>();

            using (var conn = new SqlConnection(WebSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentCampaignGetAvailable");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@PageID", DbType.Int32) { Value = pgId };
                cmd.Parameters.Add(param);
 
                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return list;

                var x0 = 0;

                while (dr.Read())
                {
                    var cId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                    var cCt = Int32.TryParse(dr["BannerCount"].ToString(), out x0) ? Convert.ToInt32(dr["BannerCount"]) : 0;
                    var cTo = Int32.TryParse(dr["ShowDelay"].ToString(), out x0) ? Convert.ToInt32(dr["ShowDelay"]) : 0;
                    var cNm = dr["CampaignName"].ToString();

                    var txt = string.Format("{0} - {1} Banner(s) Timeout: {2} sec.", cNm, cCt, cTo);


                    list.Add(new SelectListItem { Value = cId.ToString(), Text = txt });
                }
            }

            return list;
        }

        public void AddPageCampaign(CampaignModel c)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentCampaignPageAdd");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@PageID", DbType.Int32) { Value = c.PageId };
            param[1] = new SqlParameter("@CampaignID", DbType.Int32) { Value = c.CampaignId };
            param[2] = new SqlParameter("@PositionID", DbType.Int32) { Value = c.PositionId };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }

        public void DeletePageCampaign(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentCampaignPageDelete");

            var param = new SqlParameter("@ID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(WebSqlConnection, proc, param);
        }

        

        #endregion

        #region Banners


        public void DeleteBanner(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentBannerDelete");

            var param = new SqlParameter("@ID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(WebSqlConnection, proc, param);
        }


        public int AddBanner(ContentBanner m)
        {
            var i = 0;

            using (var conn = new SqlConnection(WebSqlConnection))
            {

                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentBannerAdd");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new IDataParameter[3];
                param[0] = new SqlParameter("@ItemDesc", DbType.String) { Value = m.ItemDesc };
                param[1] = new SqlParameter("@NavUrl", DbType.String) { Value = m.NavToUrl };
                param[2] = new SqlParameter("@NewWindow", DbType.Boolean) { Value = m.NewWindow };
                foreach (var parameter in param) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return i;
                var x0 = 0;

                dr.Read();
                var bId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                i = bId;

            }

            return i;
        }


        public void UpdateBanner(ContentBanner b)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentBannerUpdate");

            var param = new IDataParameter[4];
            param[0] = new SqlParameter("@ID", DbType.Int32) { Value = b.BannerId };
            param[1] = new SqlParameter("@NavUrl", DbType.String) { Value = b.NavToUrl };
            param[2] = new SqlParameter("@ItemDesc", DbType.String) { Value = b.ItemDesc };
            param[3] = new SqlParameter("@NewWindow", DbType.Boolean) { Value = b.NewWindow };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }


        public void UpdateBannerImg(ContentBanner m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentBannerImg");

            var param = new IDataParameter[2];
            param[0] = new SqlParameter("@ID", DbType.Int32) { Value = m.BannerId };
            param[1] = new SqlParameter("@ImageUrl", DbType.String) { Value = m.ImageUrl };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }

        #endregion

        #region Sales

        public SaleModel GetSalesItems(SaleModel m)
        {

            var l = new List<SaleItem>();
            var hUrl = GetHostUrl();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {

                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetSaleItems");
                var cmd = new SqlCommand(proc, conn) {CommandType = CommandType.StoredProcedure};
                conn.Open();

                var param = new IDataParameter[6];
                param[0] = new SqlParameter("@CategoryID", DbType.Int32) {Value = m.CategoryId};
                param[1] = new SqlParameter("@ManufID", DbType.Int32) {Value = m.ManufId};
                param[2] = new SqlParameter("@CaliberID", DbType.Int32) {Value = m.CaliberId};
                param[3] = new SqlParameter("@SubCatID", DbType.Int32) { Value = m.SubCatId };
                param[4] = new SqlParameter("@IsUsed", DbType.Int32) {Value = m.IsUsed};
                param[5] = new SqlParameter("@OnSale", DbType.Int32) {Value = m.OnSale};
                foreach (var parameter in param) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                var i0 = 0;
                var b0 = false;
                var dt0 = new DateTime(1900, 1, 1);
                double d0 = 0.00;

                if (!dr.HasRows)
                {
                    return m;
                }

                var d = ConfigurationHelper.GetPropertyValue("application", "m10");

                while (dr.Read())
                {
                    var mstId = Int32.TryParse(dr["MasterID"].ToString(), out i0) ? Convert.ToInt32(dr["MasterID"]) : i0;
                    var units = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;

                    var msrp = Double.TryParse(dr["Msrp"].ToString(), out d0) ? Convert.ToDouble(dr["Msrp"]) : d0;
                    var askPr = Double.TryParse(dr["AskingPrice"].ToString(), out d0) ? Convert.ToDouble(dr["AskingPrice"]) : d0;
                    var salPr = Double.TryParse(dr["SalePrice"].ToString(), out d0) ? Convert.ToDouble(dr["SalePrice"]) : d0;
                    var grsPr = Double.TryParse(dr["Profit"].ToString(), out d0) ? Convert.ToDouble(dr["Profit"]) : d0;
                    var grsMg = Double.TryParse(dr["Margin"].ToString(), out d0) ? Convert.ToDouble(dr["Margin"]) : d0;

                    var onSal = Boolean.TryParse(dr["OnSale"].ToString(), out b0) ? Convert.ToBoolean(dr["OnSale"]) : b0;

                    var datSt = DateTime.TryParse(dr["SaleStartDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["SaleStartDate"]) : dt0;
                    var datEd = DateTime.TryParse(dr["SaleEndDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["SaleEndDate"]) : dt0;

                    var mfgNm = dr["ManufName"].ToString();
                    var sbcNm = dr["SubCategoryName"].ToString();
                    var imgUl = dr["ImageUrl"].ToString();
                    var itmDs = dr["ItemDesc"].ToString();
                    var mfgPn = dr["MfgPartNumber"].ToString();
                    var upcCd = dr["UpcCode"].ToString();

                    var strStDt = datSt.ToString("yyyy-MM-dd");
                    var strEdDt = datEd.ToString("yyyy-MM-dd");

                    var imgUrl = string.Empty;

                    if (imgUl.Length > 0) { imgUrl = string.Format("{0}/{1}/{2}/{3}", hUrl, DecryptIt(BPathDir), DecryptIt(d), imgUl); } 

                    var si = new SaleItem(mstId, units, msrp, askPr, salPr, grsPr, grsMg, mfgNm, imgUrl, sbcNm, itmDs, mfgPn, upcCd, strStDt, strEdDt, onSal);
                    l.Add(si);
                }
                m.Item = l;

                return m;

            }

        }

        public void UpdateSaleItem(SaleItem s)
        {

            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSaleUpdateItem");

            var param = new IDataParameter[7];
            param[0] = new SqlParameter("@MasterID", DbType.Int32) { Value = s.MasterId };
            param[1] = new SqlParameter("@OnSale", DbType.Boolean) { Value = s.OnSale };
            param[2] = new SqlParameter("@PriceSale", DbType.Decimal) { Value = s.SalePrice };
            param[3] = new SqlParameter("@PriceMsrp", DbType.Decimal) { Value = s.Msrp };
            param[4] = new SqlParameter("@PriceAsk", DbType.Decimal) { Value = s.AskingPrice };
            param[5] = new SqlParameter("@DateStart", DbType.DateTime) { Value = s.StartDate };
            param[6] = new SqlParameter("@DateEnd", DbType.DateTime) { Value = s.EndDate };
            DataProcs.ProcParams(GunDbSqlConnection, proc, param);
        }

        public void AddFeatureItem(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentFeatureInsert");
 
            var param = new IDataParameter[1];
            param[0] = new SqlParameter("@MasterID", DbType.String) { Value = id };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }

        public List<SaleItem> GetFeatureItems()
        {

            var l = new List<SaleItem>();
            var hUrl = GetHostUrl();

            using (var conn = new SqlConnection(WebSqlConnection))
            {

                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentFeatureGetAll");
                var cmd = new SqlCommand(proc, conn) {CommandType = CommandType.StoredProcedure};
                conn.Open();

                var dr = cmd.ExecuteReader();
                var i0 = 0;
                var b0 = false;

                if (!dr.HasRows)
                {
                    return l;
                }

                var d = ConfigurationHelper.GetPropertyValue("application", "m10");

                while (dr.Read())
                {
                        var id = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                        var mstId = Int32.TryParse(dr["MasterID"].ToString(), out i0) ? Convert.ToInt32(dr["MasterID"]) : i0;

                        var isUsd = Boolean.TryParse(dr["IsUsed"].ToString(), out b0) ? Convert.ToBoolean(dr["IsUsed"]) : b0;
                        var onSal = Boolean.TryParse(dr["OnSale"].ToString(), out b0) ? Convert.ToBoolean(dr["OnSale"]) : b0;

                        var catNm = dr["CategoryName"].ToString();
                        var sbcNm = dr["SubCategoryName"].ToString();
                        var imgUl = dr["ImageUrl"].ToString();
                        var itmDs = dr["ItemDesc"].ToString();
                        var upcCd = dr["UpcCode"].ToString();


                    var imgUrl = string.Empty;



                    if (imgUl.Length > 0) { imgUrl = string.Format("{0}/{1}/{2}/{3}", hUrl, DecryptIt(BPathDir), DecryptIt(d), imgUl); } 

                    var si = new SaleItem(id, mstId, isUsd, onSal, catNm, sbcNm, imgUrl, itmDs, upcCd);
                    l.Add(si);
                }

                return l;

            }
        }

        public void UpdateHomeConfig(HomeModelConfig m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentHomeUpdate");

            var param = new IDataParameter[5];
            param[0] = new SqlParameter("@ID", DbType.Int32) { Value = m.Id };
            param[1] = new SqlParameter("@PositionID", DbType.Int32) { Value = m.PositionId };
            param[2] = new SqlParameter("@OptionID", DbType.Int32) { Value = m.OptionId };
            param[3] = new SqlParameter("@FeatureID", DbType.Int32) { Value = m.FeatureId };
            param[4] = new SqlParameter("@IsActive", DbType.Boolean) { Value = m.IsActive };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }


        public void AddHomeConfig(HomeModelConfig m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentHomeAdd");

            var param = new IDataParameter[4];
            param[0] = new SqlParameter("@PositionID", DbType.Int32) { Value = m.PositionId };
            param[1] = new SqlParameter("@OptionID", DbType.Int32) { Value = m.OptionId };
            param[2] = new SqlParameter("@FeatureID", DbType.Int32) { Value = m.FeatureId };
            param[3] = new SqlParameter("@IsActive", DbType.Boolean) { Value = m.IsActive };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }


        public void AddFeature(HomeModelConfig m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentFeatureItemAdd");

            var param = new IDataParameter[6];
            param[0] = new SqlParameter("@GroupID", DbType.Int32) { Value = m.GroupId };
            param[1] = new SqlParameter("@FeatureID", DbType.Int32) { Value = m.FeatureId };
            param[2] = new SqlParameter("@FeatureSizeID", DbType.Int32) { Value = m.FeatureSizeId };
            param[3] = new SqlParameter("@PromoColorID", DbType.Int32) { Value = m.PromoColorId };
            param[4] = new SqlParameter("@IsPromo", DbType.Boolean) { Value = m.IsPromo };
            param[5] = new SqlParameter("@PromoText", DbType.String) { Value = m.PromoText };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }

        public void UpdateFeature(HomeModelConfig m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentFeatureUpdate");

            var param = new IDataParameter[6];
            param[0] = new SqlParameter("@ID", DbType.Int32) { Value = m.GroupId };
            param[1] = new SqlParameter("@FeatureID", DbType.Int32) { Value = m.FeatureId };
            param[2] = new SqlParameter("@FeatureSizeID", DbType.Int32) { Value = m.FeatureSizeId };
            param[3] = new SqlParameter("@PromoColorID", DbType.Int32) { Value = m.PromoColorId };
            param[4] = new SqlParameter("@IsPromo", DbType.Boolean) { Value = m.IsPromo };
            param[5] = new SqlParameter("@PromoText", DbType.String) { Value = m.PromoText };
            DataProcs.ProcParams(WebSqlConnection, proc, param);
        }

        


        public void DeleteHomeContent(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentHomeDelete");

            var param = new SqlParameter("@ID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(WebSqlConnection, proc, param);
        }


        public void DeleteFeatureContent(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentFeatureDelete");

            var param = new SqlParameter("@ID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(WebSqlConnection, proc, param);
        }
        

        public FeatureItem PreviewFeatureItem(int id)
        {
            var fi = new FeatureItem();

            using (var conn = new SqlConnection(WebSqlConnection))
            {

                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcContentFeaturePreview");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@FeatureID", DbType.Int32) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                var i0 = 0;
                double d0 = 0.00;
                var b0 = false;

                if (!dr.HasRows) { return fi; }

                dr.Read();
                
                var mstId = Int32.TryParse(dr["MasterID"].ToString(), out i0) ? Convert.ToInt32(dr["MasterID"]) : i0;
                var posId = Int32.TryParse(dr["PositionID"].ToString(), out i0) ? Convert.ToInt32(dr["PositionID"]) : i0;
                var sizId = Int32.TryParse(dr["FeatureSizeID"].ToString(), out i0) ? Convert.ToInt32(dr["FeatureSizeID"]) : i0;
                var catId = Int32.TryParse(dr["CategoryID"].ToString(), out i0) ? Convert.ToInt32(dr["CategoryID"]) : i0;
                var units = Int32.TryParse(dr["AvailableUnits"].ToString(), out i0) ? Convert.ToInt32(dr["AvailableUnits"]) : i0;
                var cndId = Int32.TryParse(dr["CondID"].ToString(), out i0) ? Convert.ToInt32(dr["CondID"]) : i0;
                var price = Double.TryParse(dr["Price"].ToString(), out d0) ? Convert.ToDouble(dr["Price"]) : d0;
                var savAm = Double.TryParse(dr["Savings"].ToString(), out d0) ? Convert.ToDouble(dr["Savings"]) : d0;
                var onSal = Boolean.TryParse(dr["OnSale"].ToString(), out b0) ? Convert.ToBoolean(dr["OnSale"]) : b0;
                var isPro = Boolean.TryParse(dr["IsPromo"].ToString(), out b0) ? Convert.ToBoolean(dr["IsPromo"]) : b0;
                var isCal = Boolean.TryParse(dr["CaOkay"].ToString(), out b0) ? Convert.ToBoolean(dr["CaOkay"]) : b0;
                var isPpt = Boolean.TryParse(dr["CaPptOk"].ToString(), out b0) ? Convert.ToBoolean(dr["CaPptOk"]) : b0;

                var prTxt = dr["PromoText"].ToString();
                var prBkg = dr["PromoBgColor"].ToString();
                var mfBkg = dr["MfgBgColor"].ToString();
                var mfImg = dr["MfgImageUrl"].ToString();
                var hdTtl = dr["HeaderTitle"].ToString();
                var imgUl = dr["ImageUrl"].ToString();
                var shtDs = dr["ShortDesc"].ToString();
                var talDs = dr["TallDesc"].ToString();
                var bigDs = dr["BigDesc"].ToString();
                var caTxt = dr["CaLegalTxt"].ToString();
                var itmCd = dr["ItemCond"].ToString();
                var mfgPn = dr["MfgPartNumber"].ToString();
                var mfgNm = dr["ManufName"].ToString();
                var urlNv = dr["NavURL"].ToString();

                var imgItemUrl = string.Empty;
                var imgMfgUrl = string.Empty;

                if (imgUl.Length > 0) { imgItemUrl = string.Format(catId == 100 ? "{0}/GunImages{1}" : "{0}/SiteImg{1}", GetHostUrl(), imgUl); }
                if (mfImg.Length > 0) { imgMfgUrl = CookBaseUrl(GetHostUrl(), "HomeScroll", mfImg); }


                urlNv = urlNv.Replace(" ", "-");
                urlNv = urlNv.Replace("&", "-");
                urlNv = urlNv.Replace(".", "-");
                urlNv = urlNv.Replace("---", "-");
                urlNv = urlNv.Replace("--", "-");
                urlNv = urlNv.ToLower();

                var pf = string.Empty;

                switch (catId)
                {
                    case 100:
                        pf = "gun";
                        break;
                    case 200:
                        pf = "ammo";
                        break;
                    case 300:
                        pf = "merchandise";
                        break;
                }

                var nav = string.Format("{0}/{1}-detail/{2}", GetHostWeb(), pf, urlNv);


                fi = new FeatureItem(mstId, posId, sizId, catId, units, cndId, price, savAm, onSal, isPro, isCal, isPpt, prTxt, prBkg,
                                     mfBkg, imgMfgUrl, hdTtl, imgItemUrl, shtDs, talDs, bigDs, caTxt, itmCd, mfgPn, mfgNm, nav);

                return fi;

            }
        }


        #endregion
    }
}