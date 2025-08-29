using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AgMvcAdmin.Models.Common;
using AppBase;
using WebBase.Configuration;

namespace AgMvcAdmin.Models
{
    public class DataFeedContext : BaseModel
    {

        public string BPathDir = ConfigurationHelper.GetPropertyValue("application", "a1");
        public string BDir = ConfigurationHelper.GetPropertyValue("application", "a2");

        public FeedImageCollection GetImages(GunModel m)
        {

            var fc = new FeedImageCollection();
            var l = new List<ImageModel>();

            using (var conn = new SqlConnection(GunDbSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDataFeedImages");
                var cmd = new SqlCommand(proc, conn) {CommandType = CommandType.StoredProcedure};
                conn.Open();

                var parameters = new IDataParameter[6];
                parameters[0] = new SqlParameter("@StartRowIndex", SqlDbType.Int) { Value = m.Filters.PagingStartRow };
                parameters[1] = new SqlParameter("@MaximumRows", SqlDbType.Int) { Value = m.Filters.PagingMaxRows };
                parameters[2] = new SqlParameter("@IsMissing", SqlDbType.Bit) { Value = m.ItemMissing };
                parameters[3] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.ManufId };
                parameters[4] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = m.GunTypeId };
                parameters[5] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.CaliberId };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return fc;
                var x0 = 0;

                dr.Read();

                /* Item Count */
                var ctMfg = Int32.TryParse(dr["ManufCt"].ToString(), out x0) ? Convert.ToInt32(dr["ManufCt"]) : 0;
                var ctDup = Int32.TryParse(dr["DupCt"].ToString(), out x0) ? Convert.ToInt32(dr["DupCt"]) : 0;
                var ctAll = Int32.TryParse(dr["AllCt"].ToString(), out x0) ? Convert.ToInt32(dr["AllCt"]) : 0;
                var ctImg = Int32.TryParse(dr["ImgCt"].ToString(), out x0) ? Convert.ToInt32(dr["ImgCt"]) : 0;
                var ctRes = Int32.TryParse(dr["RowCt"].ToString(), out x0) ? Convert.ToInt32(dr["RowCt"]) : 0;

                var cm = new CountModel();
                cm.MsAll = ctAll;
                cm.MsManuf = ctMfg;
                cm.MsDuplicates = ctDup;
                cm.MsImage = ctImg;
                cm.ResultCount = ctRes;

                fc.ItemCount = cm;

                dr.NextResult();

                if (!dr.HasRows) return fc;

                var hUrl = GetHostUrl();
                var d = ConfigurationHelper.GetPropertyValue("application", "m10");

                while (dr.Read())
                {
                    var mstId = Int32.TryParse(dr["MasterID"].ToString(), out x0) ? Convert.ToInt32(dr["MasterID"]) : 0;
                    var imgDs = Int32.TryParse(dr["CurDist"].ToString(), out x0) ? Convert.ToInt32(dr["CurDist"]) : 0;
                    var rowCt = Int32.TryParse(dr["ImgCount"].ToString(), out x0) ? Convert.ToInt32(dr["ImgCount"]) : 0;


                    var mfgName = dr["ManufName"].ToString();
                    var upcCode = dr["UpcCode"].ToString();
                    var mpn = dr["MfgPartNumber"].ToString();

                    var desc = dr["ItemDesc"].ToString();
                    var imgSsi = dr["ImgSSI"].ToString();
                    var imgWss = dr["ImgWSS"].ToString();
                    var imgLip = dr["ImgLIP"].ToString();
                    var imgDav = dr["ImgDAV"].ToString();
                    var imgRsr = dr["ImgRSR"].ToString();
                    var imgBhc = dr["ImgBHC"].ToString();
                    var imgZan = dr["ImgZAN"].ToString();
                    var imgMge = dr["ImgMGE"].ToString();
                    var imgHse = dr["ImgHSE"].ToString();
                    var imgAlt = dr["ImgALT"].ToString();
                    var imgCur = dr["CurImg"].ToString();

                    var isSsi = false;
                    var isWss = false;
                    var isLip = false;
                    var isDav = false;
                    var isRsr = false;
                    var isBhc = false;
                    var isZan = false;
                    var isMge = false;
                    var isHse = false;
                    var isAlt = false;
                    var isCur = false;

                    var img1 = string.Empty;
                    var img2 = string.Empty;
                    var img3 = string.Empty;
                    var img4 = string.Empty;
                    var img5 = string.Empty;
                    var img6 = string.Empty;
                    var img7 = string.Empty;
                    var img8 = string.Empty;
                    var img9 = string.Empty;
                    var img10 = string.Empty;
                    var img11 = string.Empty;

                    if (imgSsi.Length > 0) { isSsi = true; img1 = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(d), imgSsi); }
                    if (imgWss.Length > 0) { isWss = true; img2 = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(d), imgWss); }
                    if (imgLip.Length > 0) { isLip = true; img3 = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(d), imgLip); }
                    if (imgDav.Length > 0) { isDav = true; img4 = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(d), imgDav); }
                    if (imgRsr.Length > 0) { isRsr = true; img5 = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(d), imgRsr); }
                    if (imgBhc.Length > 0) { isBhc = true; img6 = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(d), imgBhc); }
                    if (imgZan.Length > 0) { isZan = true; img7 = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(d), imgZan); }
                    if (imgMge.Length > 0) { isMge = true; img8 = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(d), imgMge); }
                    if (imgHse.Length > 0) { isHse = true; img9 = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(d), imgHse); }
                    if (imgAlt.Length > 0) { isAlt = true; img10 = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(d), imgAlt); }
                    if (imgCur.Length > 0) { isCur = true; img11 = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(d), imgCur); }

                    //if (imgSsi.Length > 0) { imgSsi = CookBaseUrl(hUrl, 1, imgSsi); isSsi = true; }
                    //if (imgWss.Length > 0) { imgWss = CookBaseUrl(hUrl, 2, imgWss); isWss = true; }
                    //if (imgLip.Length > 0) { imgLip = CookBaseUrl(hUrl, 3, imgLip); isLip = true; }
                    //if (imgDav.Length > 0) { imgDav = CookBaseUrl(hUrl, 5, imgDav); isDav = true; }
                    //if (imgRsr.Length > 0) { imgRsr = CookBaseUrl(hUrl, 6, imgRsr); isRsr = true; }
                    //if (imgBhc.Length > 0) { imgBhc = CookBaseUrl(hUrl, 8, imgBhc); isBhc = true; }
                    //if (imgZan.Length > 0) { imgZan = CookBaseUrl(hUrl, 12, imgZan); isZan = true; }
                    //if (imgMge.Length > 0) { imgMge = CookBaseUrl(hUrl, 13, imgMge); isMge = true; }
                    //if (imgHse.Length > 0) { imgHse = CookBaseUrl(hUrl, 25, imgHse); isHse = true; }
                    //if (imgAlt.Length > 0) { imgAlt = CookBaseUrl(hUrl, 99, imgAlt); isAlt = true; }
                    //if (imgCur.Length > 0) { imgCur = CookBaseUrl(hUrl, imgDs, imgCur); isCur = true; }

                    var fm = new FilterModel();
                    fm.TotalRowCount = rowCt;
                    var isHsePath = imgDs == 25;

                    var gm = new ImageModel(mstId, imgDs, mfgName, upcCode, mpn, desc, img1, img2, img3,
                        img4, img5, img6, img7, img8, img9, img10, img11, isSsi, isWss, isLip, 
                        isDav, isRsr, isBhc, isZan, isMge, isHse, isAlt, isCur, isHsePath, fm);

                    l.Add(gm);
                }

                fc.Images = l;
            }

            return fc;
        }




        public List<GunModel> GetDuplicateGuns(GunModel m)
        {
            var l = new List<GunModel>();

            using (var conn = new SqlConnection(GunDbSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDataFeedDuplicates");
                var cmd = new SqlCommand(proc, conn) {CommandType = CommandType.StoredProcedure};
                conn.Open();

                var parameters = new IDataParameter[7];
                parameters[0] = new SqlParameter("@StartRowIndex", SqlDbType.Int) { Value = m.Filters.PagingStartRow };
                parameters[1] = new SqlParameter("@MaximumRows", SqlDbType.Int) { Value = m.Filters.PagingMaxRows };
                parameters[2] = new SqlParameter("@SrchID", SqlDbType.Int) {Value = m.ValueId};
                parameters[3] = new SqlParameter("@ManufID", SqlDbType.Int) {Value = m.ManufId};
                parameters[4] = new SqlParameter("@GunTypeID", SqlDbType.Int) {Value = m.GunTypeId};
                parameters[5] = new SqlParameter("@CaliberID", SqlDbType.Int) {Value = m.CaliberId};
                parameters[6] = new SqlParameter("@SrchTxt", SqlDbType.VarChar) {Value = m.SearchText};
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return l;
                var x0 = 0;
                var d0 = 0.00;
                var b0 = false;

                dr.Read();

                /* Item Count */
                var ctMfg = Int32.TryParse(dr["ManufCt"].ToString(), out x0) ? Convert.ToInt32(dr["ManufCt"]) : 0;
                var ctDup = Int32.TryParse(dr["DupCt"].ToString(), out x0) ? Convert.ToInt32(dr["DupCt"]) : 0;
                var ctAll = Int32.TryParse(dr["AllCt"].ToString(), out x0) ? Convert.ToInt32(dr["AllCt"]) : 0;
                var ctImg = Int32.TryParse(dr["ImgCt"].ToString(), out x0) ? Convert.ToInt32(dr["ImgCt"]) : 0;

                var cm = new CountModel();
                cm.MsAll = ctAll;
                cm.MsManuf = ctMfg;
                cm.MsDuplicates = ctDup;
                cm.MsImage = ctImg;

                var ic = new GunModel(cm);
                l.Add(ic);

                dr.NextResult();

                if (!dr.HasRows) return l;

                var d = ConfigurationHelper.GetPropertyValue("application", "m10");

                while (dr.Read())
                {
                    var mfgId = Int32.TryParse(dr["ManufID"].ToString(), out x0) ? Convert.ToInt32(dr["ManufID"]) : 0;
                    var mstId = Int32.TryParse(dr["MasterID"].ToString(), out x0) ? Convert.ToInt32(dr["MasterID"]) : 0;
                    var imgDs = Int32.TryParse(dr["ImageDist"].ToString(), out x0) ? Convert.ToInt32(dr["ImageDist"]) : 0;
                    var capId = Int32.TryParse(dr["CapacityInt"].ToString(), out x0) ? Convert.ToInt32(dr["CapacityInt"]) : 0;
                    var atnId = Int32.TryParse(dr["ActionID"].ToString(), out x0) ? Convert.ToInt32(dr["ActionID"]) : 0;
                    var rowCt = Int32.TryParse(dr["GunRowCount"].ToString(), out x0) ? Convert.ToInt32(dr["GunRowCount"]) : 0;
                    var brlDm = Double.TryParse(dr["BarrelDec"].ToString(), out d0) ? Convert.ToDouble(dr["BarrelDec"]) : 0.000;

                    var isSsi = Boolean.TryParse(dr["Ssi"].ToString(), out b0) ? Convert.ToBoolean(dr["Ssi"]) : false;
                    var isWss = Boolean.TryParse(dr["Wss"].ToString(), out b0) ? Convert.ToBoolean(dr["Wss"]) : false;
                    var isLip = Boolean.TryParse(dr["Lip"].ToString(), out b0) ? Convert.ToBoolean(dr["Lip"]) : false;
                    var isDav = Boolean.TryParse(dr["Dav"].ToString(), out b0) ? Convert.ToBoolean(dr["Dav"]) : false;
                    var isRsr = Boolean.TryParse(dr["Rsr"].ToString(), out b0) ? Convert.ToBoolean(dr["Rsr"]) : false;
                    var isBhc = Boolean.TryParse(dr["Bhc"].ToString(), out b0) ? Convert.ToBoolean(dr["Bhc"]) : false;
                    var isGrn = Boolean.TryParse(dr["Grn"].ToString(), out b0) ? Convert.ToBoolean(dr["Grn"]) : false;
                    var isZan = Boolean.TryParse(dr["Zan"].ToString(), out b0) ? Convert.ToBoolean(dr["Zan"]) : false;
                    var isMge = Boolean.TryParse(dr["Mge"].ToString(), out b0) ? Convert.ToBoolean(dr["Mge"]) : false;

                    var imgName = dr["ImageName"].ToString();
                    var mfgName = dr["ManufName"].ToString();
                    var gunType = dr["GunType"].ToString();
                    var calName = dr["CaliberName"].ToString();
                    var mdlName = dr["ModelName"].ToString();
                    var mfgPtNm = dr["MfgPartNumber"].ToString();
                    var upcCode = dr["UpcCode"].ToString();
                    var descrip = dr["Description"].ToString();
                    var atnName = dr["ActionName"].ToString();
                    var finName = dr["FinishName"].ToString();
                    var ig = dr["ImageURL"].ToString();

                    var fm = new FilterModel();
                    fm.IsSsi = isSsi;
                    fm.IsWss = isWss;
                    fm.IsLip = isLip;
                    fm.IsDav = isDav;
                    fm.IsRsr = isRsr;
                    fm.IsBhc = isBhc;
                    fm.IsGrn = isGrn;
                    fm.IsZan = isZan;
                    fm.IsMge = isMge;
                    fm.TotalRowCount = rowCt;

                    var imgUrl = string.Empty;
                    if (ig.Length > 0) { imgUrl = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(d), ig); } 

                    var gm = new GunModel(mfgId, mstId, capId, atnId, brlDm, imgUrl,
                        mfgName, gunType, calName, mdlName, mfgPtNm, upcCode,
                        descrip, atnName, finName, fm);

                    l.Add(gm);
                }
            }

            return l;
        }


        public void MergeDuplicateGun(int oldGunId, int newMstId)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMergeDuplicate");

            var param = new IDataParameter[2];
            param[0] = new SqlParameter("@OldGunID", DbType.Int32) { Value = oldGunId };
            param[1] = new SqlParameter("@NewMasterID", DbType.Int32) { Value = newMstId };
            DataProcs.ProcParams(GunDbSqlConnection, proc, param);
        }


        public List<GunModel> GetDataFeedGuns(GunModel m)
        {
            var l = new List<GunModel>();


            using (var conn = new SqlConnection(GunDbSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetDataFeedGrid");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();


                var parameters = new IDataParameter[32];
                parameters[0] = new SqlParameter("@IsGunType", SqlDbType.Bit) { Value = m.Filters.IsMissingGunType };
                parameters[1] = new SqlParameter("@IsCaliber", SqlDbType.Bit) { Value = m.Filters.IsMissingCaliber };
                parameters[2] = new SqlParameter("@IsCapacity", SqlDbType.Bit) { Value = m.Filters.IsMissingCapacity };
                parameters[3] = new SqlParameter("@IsAction", SqlDbType.Bit) { Value = m.Filters.IsMissingAction };
                parameters[4] = new SqlParameter("@IsFinish", SqlDbType.Bit) { Value = m.Filters.IsMissingFinish };
                parameters[5] = new SqlParameter("@IsModel", SqlDbType.Bit) { Value = m.Filters.IsMissingModel };
                parameters[6] = new SqlParameter("@IsDesc", SqlDbType.Bit) { Value = m.Filters.IsMissingDesc };
                parameters[7] = new SqlParameter("@IsLongDesc", SqlDbType.Bit) { Value = m.Filters.IsMissingLongDesc };
                parameters[8] = new SqlParameter("@IsBarLen", SqlDbType.Bit) { Value = m.Filters.IsMissingBrlLen };
                parameters[9] = new SqlParameter("@IsOvrLen", SqlDbType.Bit) { Value = m.Filters.IsMissingOvrLen };
                parameters[10] = new SqlParameter("@IsWeight", SqlDbType.Bit) { Value = m.Filters.IsMissingWeight };
                parameters[11] = new SqlParameter("@IsImage", SqlDbType.Bit) { Value = m.Filters.IsMissingImage };
                parameters[12] = new SqlParameter("@IsCaAwRest", SqlDbType.Bit) { Value = m.Filters.IsCaAwRestricted };
                parameters[13] = new SqlParameter("@IsHidden", SqlDbType.Bit) { Value = m.IsHidden };
                parameters[14] = new SqlParameter("@IsCurModel", SqlDbType.Bit) { Value = m.IsCurModel };
                parameters[15] = new SqlParameter("@IsMissOnly", SqlDbType.Bit) { Value = m.Filters.IsMissingAll };
                parameters[16] = new SqlParameter("@IsOnDataFeed", SqlDbType.Bit) { Value = m.Filters.IsOnDataFeed };
                parameters[17] = new SqlParameter("@IsLeo", SqlDbType.Bit) { Value = m.Filters.IsLeo };
                parameters[18] = new SqlParameter("@IsCaLegal", SqlDbType.Bit) { Value = m.CaRestrict.CaOkay };
                parameters[19] = new SqlParameter("@IsCaRoster", SqlDbType.Bit) { Value = m.CaRestrict.CaRosterOk };
                parameters[20] = new SqlParameter("@IsCaSaRev", SqlDbType.Bit) { Value = m.CaRestrict.CaSglActnOk };
                parameters[21] = new SqlParameter("@IsCaSsPst", SqlDbType.Bit) { Value = m.CaRestrict.CaSglShotOk };
                parameters[22] = new SqlParameter("@IsCaCurRel", SqlDbType.Bit) { Value = m.CaRestrict.CaCurioOk };
                parameters[23] = new SqlParameter("@IsCaPpt", SqlDbType.Bit) { Value = m.CaRestrict.CaPptOk };
                parameters[24] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.ManufId };
                parameters[25] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = m.GunTypeId };
                parameters[26] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.CaliberId };
                parameters[27] = new SqlParameter("@ActionID", SqlDbType.Int) { Value = m.ActionId };
                parameters[28] = new SqlParameter("@DaysBack", SqlDbType.Int) { Value = m.Filters.DaysBackToSearch };
                parameters[29] = new SqlParameter("@StartRowIndex", SqlDbType.Int) { Value = m.Filters.PagingStartRow };
                parameters[30] = new SqlParameter("@MaximumRows", SqlDbType.Int) { Value = m.Filters.PagingMaxRows  };
                parameters[31] = new SqlParameter("@SearchTxt", SqlDbType.VarChar) { Value = m.SearchText };

                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();

                var x0 = 0;

                if (!dr.HasRows) return l;


                dr.Read();

                /* Item Count */
                var mAll = Int32.TryParse(dr["mAll"].ToString(), out x0) ? Convert.ToInt32(dr["mAll"]) : 0;
                var mGtp = Int32.TryParse(dr["mGtp"].ToString(), out x0) ? Convert.ToInt32(dr["mGtp"]) : 0;
                var mCal = Int32.TryParse(dr["mCal"].ToString(), out x0) ? Convert.ToInt32(dr["mCal"]) : 0;
                var mCap = Int32.TryParse(dr["mCap"].ToString(), out x0) ? Convert.ToInt32(dr["mCap"]) : 0;
                var mAtn = Int32.TryParse(dr["mAtn"].ToString(), out x0) ? Convert.ToInt32(dr["mAtn"]) : 0;
                var mFin = Int32.TryParse(dr["mFin"].ToString(), out x0) ? Convert.ToInt32(dr["mFin"]) : 0;
                var mMdl = Int32.TryParse(dr["mMdl"].ToString(), out x0) ? Convert.ToInt32(dr["mMdl"]) : 0;
                var mDsc = Int32.TryParse(dr["mDsc"].ToString(), out x0) ? Convert.ToInt32(dr["mDsc"]) : 0;
                var mLds = Int32.TryParse(dr["mLds"].ToString(), out x0) ? Convert.ToInt32(dr["mLds"]) : 0;
                var mBrl = Int32.TryParse(dr["mBrl"].ToString(), out x0) ? Convert.ToInt32(dr["mBrl"]) : 0;
                var mOvl = Int32.TryParse(dr["mOvl"].ToString(), out x0) ? Convert.ToInt32(dr["mOvl"]) : 0;
                var mWgt = Int32.TryParse(dr["mWgt"].ToString(), out x0) ? Convert.ToInt32(dr["mWgt"]) : 0;
                var mImg = Int32.TryParse(dr["mImg"].ToString(), out x0) ? Convert.ToInt32(dr["mImg"]) : 0;

                var cLgl = Int32.TryParse(dr["cLgl"].ToString(), out x0) ? Convert.ToInt32(dr["cLgl"]) : 0;
                var cRst = Int32.TryParse(dr["cRst"].ToString(), out x0) ? Convert.ToInt32(dr["cRst"]) : 0;
                var cSar = Int32.TryParse(dr["cSar"].ToString(), out x0) ? Convert.ToInt32(dr["cSar"]) : 0;
                var cSsp = Int32.TryParse(dr["cSsp"].ToString(), out x0) ? Convert.ToInt32(dr["cSsp"]) : 0;
                var cCur = Int32.TryParse(dr["cCur"].ToString(), out x0) ? Convert.ToInt32(dr["cCur"]) : 0;
                var cPpt = Int32.TryParse(dr["cPpt"].ToString(), out x0) ? Convert.ToInt32(dr["cPpt"]) : 0;

                var cMfg = Int32.TryParse(dr["MfCt"].ToString(), out x0) ? Convert.ToInt32(dr["MfCt"]) : 0;
                var cDup = Int32.TryParse(dr["DpCt"].ToString(), out x0) ? Convert.ToInt32(dr["DpCt"]) : 0;
                var cAll = Int32.TryParse(dr["AllCt"].ToString(), out x0) ? Convert.ToInt32(dr["AllCt"]) : 0;
                var cImg = Int32.TryParse(dr["ImgCt"].ToString(), out x0) ? Convert.ToInt32(dr["ImgCt"]) : 0;


                var cm = new CountModel();
                cm.MsAll = mAll;
                cm.MsGunType = mGtp;
                cm.MsCaliber = mCal;
                cm.MsCapacity = mCap;
                cm.MsAction = mAtn;
                cm.MsFinish = mFin;
                cm.MsModel = mMdl;
                cm.MsDesc = mDsc;
                cm.MsLgDesc = mLds;
                cm.MsBarrel = mBrl;
                cm.MsOverall = mOvl;
                cm.MsWeight = mWgt;
                cm.MsImage = mImg;

                cm.CaLegal = cLgl;
                cm.CaRoster = cRst;
                cm.CaSaRev = cSar;
                cm.CaSsPst = cSsp;
                cm.CaCurio = cCur;
                cm.CaPvtPt = cPpt;

                cm.MsAll = cAll;
                cm.MsManuf = cMfg;
                cm.MsDuplicates = cDup;
                cm.MsImage = cImg;


                var gc = new GunModel(cm);
                l.Add(gc);
                

                dr.NextResult();

                if (!dr.HasRows) return l;

                var d = ConfigurationHelper.GetPropertyValue("application", "m10");
                var dc = DecryptIt(d);
                var h = GetHostUrl();
                var bd = DecryptIt(BPathDir);

                while (dr.Read())
                {

                    var d0 = 0.00;
                    var b0 = false;
                    var t0 = DateTime.MinValue;

                    var mstId = Int32.TryParse(dr["MasterID"].ToString(), out x0) ? Convert.ToInt32(dr["MasterID"]) : 0;
                    var mfgId = Int32.TryParse(dr["ManufID"].ToString(), out x0) ? Convert.ToInt32(dr["ManufID"]) : 0;
                    var gtpId = Int32.TryParse(dr["GunTypeID"].ToString(), out x0) ? Convert.ToInt32(dr["GunTypeID"]) : 0;
                    var calId = Int32.TryParse(dr["CaliberID"].ToString(), out x0) ? Convert.ToInt32(dr["CaliberID"]) : 0;
                    var atnId = Int32.TryParse(dr["ActionID"].ToString(), out x0) ? Convert.ToInt32(dr["ActionID"]) : 0;
                    var finId = Int32.TryParse(dr["FinishID"].ToString(), out x0) ? Convert.ToInt32(dr["FinishID"]) : 0;
                    var cndId = Int32.TryParse(dr["ConditionID"].ToString(), out x0) ? Convert.ToInt32(dr["ConditionID"]) : 0;
                    var capId = Int32.TryParse(dr["CapacityInt"].ToString(), out x0) ? Convert.ToInt32(dr["CapacityInt"]) : 0;
                    var wgtLb = Int32.TryParse(dr["WeightLb"].ToString(), out x0) ? Convert.ToInt32(dr["WeightLb"]) : 0;
                    var imgDs = Int32.TryParse(dr["ImageDist"].ToString(), out x0) ? Convert.ToInt32(dr["ImageDist"]) : 0;
                    var rowCt = Int32.TryParse(dr["GunRowCount"].ToString(), out x0) ? Convert.ToInt32(dr["GunRowCount"]) : 0;

                    var isActv = Boolean.TryParse(dr["Active"].ToString(), out b0) ? Convert.ToBoolean(dr["Active"]) : b0;
                    var isVerf = Boolean.TryParse(dr["Verified"].ToString(), out b0) ? Convert.ToBoolean(dr["Verified"]) : b0;
                    var isHide = Boolean.TryParse(dr["Hide"].ToString(), out b0) ? Convert.ToBoolean(dr["Hide"]) : b0;
                    var isDaFd = Boolean.TryParse(dr["IsDataFeed"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDataFeed"]) : b0;
                    var isLeo = Boolean.TryParse(dr["IsLeo"].ToString(), out b0) ? Convert.ToBoolean(dr["IsLeo"]) : b0;
                    var isCurM = Boolean.TryParse(dr["CurrentModel"].ToString(), out b0) ? Convert.ToBoolean(dr["CurrentModel"]) : b0;
                    var isHdCa = Boolean.TryParse(dr["HideCA"].ToString(), out b0) ? Convert.ToBoolean(dr["HideCA"]) : b0;
                    var isCaOk = Boolean.TryParse(dr["CaOkay"].ToString(), out b0) ? Convert.ToBoolean(dr["CaOkay"]) : b0;  
                    var isOgBx = Boolean.TryParse(dr["OriginalBox"].ToString(), out b0) ? Convert.ToBoolean(dr["OriginalBox"]) : b0;
                    var isOgPw = Boolean.TryParse(dr["OriginalPapers"].ToString(), out b0) ? Convert.ToBoolean(dr["OriginalPapers"]) : b0;
                    var isRqFl = Boolean.TryParse(dr["RequireFFL"].ToString(), out b0) ? Convert.ToBoolean(dr["RequireFFL"]) : b0;
                    var isUsed = Boolean.TryParse(dr["IsUsed"].ToString(), out b0) ? Convert.ToBoolean(dr["IsUsed"]) : b0;
                    var isCaRt = Boolean.TryParse(dr["CaRosterOk"].ToString(), out b0) ? Convert.ToBoolean(dr["CaRosterOk"]) : b0;
                    var isCaSa = Boolean.TryParse(dr["CaSglActnOk"].ToString(), out b0) ? Convert.ToBoolean(dr["CaSglActnOk"]) : b0;
                    var isCaSs = Boolean.TryParse(dr["CaSglShotOk"].ToString(), out b0) ? Convert.ToBoolean(dr["CaSglShotOk"]) : b0;
                    var isCaCr = Boolean.TryParse(dr["CaCurioOk"].ToString(), out b0) ? Convert.ToBoolean(dr["CaCurioOk"]) : b0;
                    var isCaPt = Boolean.TryParse(dr["CaPptOk"].ToString(), out b0) ? Convert.ToBoolean(dr["CaPptOk"]) : b0;
                    var isMiss = Boolean.TryParse(dr["ItemMissing"].ToString(), out b0) ? Convert.ToBoolean(dr["ItemMissing"]) : b0;

                    var brlDm = Double.TryParse(dr["BarrelDec"].ToString(), out d0) ? Convert.ToDouble(dr["BarrelDec"]) : 0.000;
                    var ovlDm = Double.TryParse(dr["OverallDec"].ToString(), out d0) ? Convert.ToDouble(dr["OverallDec"]) : 0.000;
                    var chmDm = Double.TryParse(dr["ChamberDec"].ToString(), out d0) ? Convert.ToDouble(dr["ChamberDec"]) : 0.000;
                    var wgtOz = Double.TryParse(dr["WeightOz"].ToString(), out d0) ? Convert.ToDouble(dr["WeightOz"]) : 0.00;

                    var addDt = DateTime.TryParse(dr["DateAdded"].ToString(), out t0) ? Convert.ToDateTime(dr["DateAdded"]) : t0;

                    var imgName = dr["ImageName"].ToString();
                    var mfgName = dr["ManufName"].ToString();
                    var gunType = dr["GunType"].ToString();
                    var calName = dr["CaliberName"].ToString();
                    var mdlName = dr["ModelName"].ToString();
                    var mfgPtNm = dr["MfgPartNumber"].ToString();
                    var upcCode = dr["UpcCode"].ToString();
                    var atnName = dr["ActionName"].ToString();
                    var fnhName = dr["FinishName"].ToString();
                    var cndName = dr["ConditionName"].ToString();
                    var descrip = dr["Description"].ToString();
                    var longDsc = dr["LongDescription"].ToString();
                    var ig = dr["ImageURL"].ToString();

                    var fm = new FilterModel();
                    fm.IsOnDataFeed = isDaFd;
                    fm.DateAdded = addDt;
                    fm.TotalRowCount = rowCt;
                    fm.IsLeo = isLeo;

                    var imgUrl = string.Empty;

                    if (ig.Length > 0) { imgUrl = string.Format("{0}/{1}/{2}/{3}", h, bd, dc, ig); }

                    var ca = new CaRestrictModel(isHdCa, isCaOk, isCaRt, isCaPt, isCaCr, isCaSa, isCaSs);

                    var gm = new GunModel(mstId, mfgId, gtpId, calId, atnId, finId, cndId, capId, wgtLb, imgDs,
                        isActv, isVerf, isHide, isCurM, isOgBx, isOgPw, isRqFl, isUsed, isMiss, brlDm, ovlDm, 
                        chmDm, wgtOz, imgUrl, mfgName, gunType, calName, mdlName, mfgPtNm, upcCode, atnName, 
                        fnhName, cndName, descrip, longDsc, fm, ca);

                        l.Add(gm);
                }
            }

            return l;

        }


        public void UpdateHouseImage(ItemData d)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateHouseImage");
            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@ID", DbType.Int32) { Value = d.ID };
            param[1] = new SqlParameter("@DistID", DbType.Int32) { Value = d.DistributorId };
            param[2] = new SqlParameter("@ImageName", DbType.String) { Value = d.ImageName };

            DataProcs.ProcParams(GunDbSqlConnection, proc, param);
        }


        public void UpdateDisplayGunPics(ItemData d)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateDisplayGunPics");
            var param = new IDataParameter[2];
            param[0] = new SqlParameter("@TransID", DbType.String) { Value = d.ItemKey };
            param[1] = new SqlParameter("@StockImg", DbType.String) { Value = d.ImageName };

            DataProcs.ProcParams(AdminSqlConnection, proc, param);
        }


        public void UpdateMenuItem(GunModel d)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateDataFeedMenuItem");
            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@MastID", DbType.Int32) { Value = d.MasterId };
            param[1] = new SqlParameter("@MenuID", DbType.Int32) { Value = d.Id };
            param[2] = new SqlParameter("@ValID", DbType.String) { Value = d.ValueId };

            DataProcs.ProcParams(GunDbSqlConnection, proc, param);
        }

        public void UpdateDataFeedItem(GunModel d)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateDataFeedItem");
            var param = new IDataParameter[34];
            param[0] = new SqlParameter("@MasterID", DbType.Int32) { Value = d.Id };
            param[1] = new SqlParameter("@Capacity", DbType.Int32) { Value = d.CapacityInt };
            param[2] = new SqlParameter("@GunTypeID", DbType.Int32) { Value = d.GunTypeId };
            param[3] = new SqlParameter("@CaliberID", DbType.Int32) { Value = d.CaliberId };
            param[4] = new SqlParameter("@ActionID", DbType.Int32) { Value = d.ActionId };
            param[5] = new SqlParameter("@FinishID", DbType.Int32) { Value = d.FinishId };
            param[6] = new SqlParameter("@ConditionID", DbType.Int32) { Value = d.ConditionId };
            param[7] = new SqlParameter("@Brl", DbType.Decimal) { Value = d.BarrelDec };
            param[8] = new SqlParameter("@Ovl", DbType.Decimal) { Value = d.OverallDec };
            param[9] = new SqlParameter("@Chm", DbType.Decimal) { Value = d.ChamberDec };
            param[10] = new SqlParameter("@WgtLb", DbType.Int32) { Value = d.WeightLb };
            param[11] = new SqlParameter("@WgtOz", DbType.Decimal) { Value = d.WeightOz };
            param[12] = new SqlParameter("@Model", DbType.String) { Value = d.ModelName };
            param[13] = new SqlParameter("@Upc", DbType.String) { Value = d.UpcCode };
            param[14] = new SqlParameter("@Mpn", DbType.String) { Value = d.MfgPartNumber };
            param[15] = new SqlParameter("@Desc", DbType.String) { Value = d.Description };
            param[16] = new SqlParameter("@LongDesc", DbType.String) { Value = d.LongDescription };
            param[17] = new SqlParameter("@Active", DbType.Boolean) { Value = d.IsActive };
            param[18] = new SqlParameter("@Verified", DbType.Boolean) { Value = d.IsVerified };
            param[19] = new SqlParameter("@Hide", DbType.Boolean) { Value = d.IsHidden };
            param[20] = new SqlParameter("@IsOnFeed", DbType.Boolean) { Value = d.Filters.IsOnDataFeed };
            param[21] = new SqlParameter("@IsLeo", DbType.Boolean) { Value = d.Filters.IsLeo };
            param[22] = new SqlParameter("@CurrentMdl", DbType.Boolean) { Value = d.IsCurModel };
            param[23] = new SqlParameter("@HideCA", DbType.Boolean) { Value = d.CaRestrict.CaHide };
            param[24] = new SqlParameter("@CaOkay", DbType.Boolean) { Value = d.CaRestrict.CaOkay };
            param[25] = new SqlParameter("@OrigBox", DbType.Boolean) { Value = d.OrigBox };
            param[26] = new SqlParameter("@OrigPapers", DbType.Boolean) { Value = d.OrigPaperwork };
            param[27] = new SqlParameter("@ReqFFL", DbType.Boolean) { Value = d.IsReqFfl };
            param[28] = new SqlParameter("@IsUsed", DbType.Boolean) { Value = d.IsUsed };
            param[29] = new SqlParameter("@IsCaRost", DbType.Boolean) { Value = d.CaRestrict.CaRosterOk };
            param[30] = new SqlParameter("@IsCaCurio", DbType.Boolean) { Value = d.CaRestrict.CaCurioOk };
            param[31] = new SqlParameter("@IsCaSglAtn", DbType.Boolean) { Value = d.CaRestrict.CaSglActnOk };
            param[32] = new SqlParameter("@IsCaSglSht", DbType.Boolean) { Value = d.CaRestrict.CaSglShotOk };
            param[33] = new SqlParameter("@IsCaPpt", DbType.Boolean) { Value = d.CaRestrict.CaPptOk };

            DataProcs.ProcParams(GunDbSqlConnection, proc, param);
 
        }


        public void MapToManuf(int distId, int manufId, string mapCode)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcEditManufUpdateCode");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@DistID", DbType.Int32) { Value = distId };
            param[1] = new SqlParameter("@ManufID", DbType.Int32) { Value = manufId };
            param[2] = new SqlParameter("@MapCode", DbType.String) { Value = mapCode };
            DataProcs.ProcParams(GunDbSqlConnection, proc, param);
        }


        public void CreateManuf(int distId, string mapCode, string newMfg)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcEditManufNewManuf");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@DistID", DbType.Int32) { Value = distId };
            param[1] = new SqlParameter("@MapCode", DbType.String) { Value = mapCode };
            param[2] = new SqlParameter("@NewMfgName", DbType.String) { Value = newMfg };
            DataProcs.ProcParams(GunDbSqlConnection, proc, param);
        }

        public void UpdateFeedMfg(ManufModel m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateDataFeedMfg");

            var param = new IDataParameter[6];
            param[0] = new SqlParameter("@ManufID", DbType.Int32) { Value = m.ManufId };
            param[1] = new SqlParameter("@ParentID", DbType.Int32) { Value = m.ParentId };
            param[2] = new SqlParameter("@ManufName", DbType.String) { Value = m.ManufName };
            param[3] = new SqlParameter("@ManufUrl", DbType.String) { Value = m.ManufUrl };
            param[4] = new SqlParameter("@IsOnFeed", DbType.Boolean) { Value = m.IsOnFeed };
            param[5] = new SqlParameter("@IsActive", DbType.Boolean) { Value = m.IsActive };
            DataProcs.ProcParams(GunDbSqlConnection, proc, param);
        }

        public void UpdateFeedImage(GunModel m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateDataFeedImage");

            var param = new IDataParameter[2];
            param[0] = new SqlParameter("@MasterID", DbType.Int32) { Value = m.ManufId };
            param[1] = new SqlParameter("@DistID", DbType.Int32) { Value = m.ImageDist };
            DataProcs.ProcParams(GunDbSqlConnection, proc, param);
        }

        public List<ManufModel> GetEditMfgGrid(ManufModel m)
        {
            var l = new List<ManufModel>();

            using (var conn = new SqlConnection(GunDbSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetMfgEditGrid");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[3];
                parameters[0] = new SqlParameter("@DistID", SqlDbType.Int) { Value = m.DistId };
                parameters[1] = new SqlParameter("@IsOnFeed", SqlDbType.Bit) { Value = m.IsOnFeed };
                parameters[2] = new SqlParameter("@IsParent", SqlDbType.Bit) { Value = m.IsParentOnly };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return l;
                var x0 = 0;
                var b0 = false;

                dr.Read();

                /* Item Count */
                var ctMfg = Int32.TryParse(dr["ManufCt"].ToString(), out x0) ? Convert.ToInt32(dr["ManufCt"]) : 0;
                var ctDup = Int32.TryParse(dr["DupCt"].ToString(), out x0) ? Convert.ToInt32(dr["DupCt"]) : 0;
                var ctAll = Int32.TryParse(dr["AllCt"].ToString(), out x0) ? Convert.ToInt32(dr["AllCt"]) : 0;
                var ctImg = Int32.TryParse(dr["ImgCt"].ToString(), out x0) ? Convert.ToInt32(dr["ImgCt"]) : 0;

                var cm = new CountModel();
                cm.MsAll = ctAll;
                cm.MsManuf = ctMfg;
                cm.MsDuplicates = ctDup;
                cm.MsImage = ctImg;

                var ic = new ManufModel(cm);
                l.Add(ic);
                
                dr.NextResult();

                if (!dr.HasRows) return l;

                while (dr.Read())
                {
                    var mfgId = Int32.TryParse(dr["ManufID"].ToString(), out x0) ? Convert.ToInt32(dr["ManufID"]) : 0;
                    var parId = Int32.TryParse(dr["ParentID"].ToString(), out x0) ? Convert.ToInt32(dr["ParentID"]) : 0;

                    var isSsi = Boolean.TryParse(dr["SSI"].ToString(), out b0) ? Convert.ToBoolean(dr["SSI"]) : false;
                    var isWss = Boolean.TryParse(dr["WSS"].ToString(), out b0) ? Convert.ToBoolean(dr["WSS"]) : false;
                    var isLip = Boolean.TryParse(dr["LIP"].ToString(), out b0) ? Convert.ToBoolean(dr["LIP"]) : false;
                    var isDav = Boolean.TryParse(dr["DAV"].ToString(), out b0) ? Convert.ToBoolean(dr["DAV"]) : false;
                    var isRsr = Boolean.TryParse(dr["RSR"].ToString(), out b0) ? Convert.ToBoolean(dr["RSR"]) : false;
                    var isBhc = Boolean.TryParse(dr["BHC"].ToString(), out b0) ? Convert.ToBoolean(dr["BHC"]) : false;
                    var isGrn = Boolean.TryParse(dr["GRN"].ToString(), out b0) ? Convert.ToBoolean(dr["GRN"]) : false;
                    var isZan = Boolean.TryParse(dr["ZAN"].ToString(), out b0) ? Convert.ToBoolean(dr["ZAN"]) : false;
                    var isMge = Boolean.TryParse(dr["MGE"].ToString(), out b0) ? Convert.ToBoolean(dr["MGE"]) : false;
                    var isOnFeed = Boolean.TryParse(dr["IsDataFeed"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDataFeed"]) : false;
                    var isActive = Boolean.TryParse(dr["Active"].ToString(), out b0) ? Convert.ToBoolean(dr["Active"]) : false;

                    var mfgName = dr["ManufName"].ToString();
                    var parName = dr["ParentName"].ToString();
                    var mfgUrl = dr["ManufURL"].ToString();

                    var mm = new ManufModel(mfgId, parId, isSsi, isWss, isLip, isDav, isRsr, isBhc, isGrn, isZan, isMge, isOnFeed, isActive, mfgName, parName, mfgUrl);

                    l.Add(mm);
                }



            }

            return l;
        }


        public HomeScrollModel GetHomeScrollGrid(ManufModel m)
        {
            var hs = new HomeScrollModel();
            var l = new List<ManufModel>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetHomeScrollGrid");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[3];
                parameters[0] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = m.IsActive };
                parameters[1] = new SqlParameter("@IsOnFeed", SqlDbType.Bit) { Value = m.IsOnFeed };
                parameters[2] = new SqlParameter("@IsParent", SqlDbType.Bit) { Value = m.IsParentOnly };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return hs;
                var x0 = 0;
                var b0 = false;

                dr.Read();

                var isAtv = Boolean.TryParse(dr["ScrollEnabled"].ToString(), out b0) ? Convert.ToBoolean(dr["ScrollEnabled"]) : b0;
                hs.IsScrollActive = isAtv;


                dr.NextResult();
                if (!dr.HasRows) return hs;

                var hUrl = GetHostUrl();
                var p1 = ConfigurationHelper.GetPropertyValue("application", "a3");

                while (dr.Read())
                {
                    var mfgId = Int32.TryParse(dr["ManufID"].ToString(), out x0) ? Convert.ToInt32(dr["ManufID"]) : 0;
                    var isAtvS = Boolean.TryParse(dr["IsActive"].ToString(), out b0) ? Convert.ToBoolean(dr["IsActive"]) : b0;
                    var mfgNm = dr["ManufName"].ToString();
                    var imgSl = dr["ImgHomeScroll"].ToString();
                    var bgClr = dr["ScrollBgColor"].ToString();
                    var mfgUl = dr["ManufURL"].ToString();

                    var imgUrl = string.Empty;

                    if (imgSl.Length > 0)
                    {
                        imgUrl = string.Format("{0}/{1}/{2}/{3}", hUrl, DecryptIt(BPathDir), DecryptIt(p1), imgSl);
                    } // CookBaseUrl(hUrl, "HomeScroll", imgSl);

                    var mm = new ManufModel(mfgId, isAtvS, mfgNm, imgUrl, bgClr, mfgUl);
                    l.Add(mm);
                }

                hs.Manuf = l;
            }

            return hs;
        }

        public void UpdateHomeScrollGrid(ManufModel m)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateHomeScrollGrid");

            var param = new IDataParameter[6];
            param[0] = new SqlParameter("@ManufId", DbType.Int32) { Value = m.ManufId };
            param[1] = new SqlParameter("@IsActive", DbType.Boolean) { Value = m.IsActive };
            param[2] = new SqlParameter("@UpdateImg", DbType.Boolean) { Value = m.UpdateImg };
            param[3] = new SqlParameter("@BgColor", DbType.String) { Value = m.ScrollBgColor };
            param[4] = new SqlParameter("@MfgUrl", DbType.String) { Value = m.ManufUrl };
            param[5] = new SqlParameter("@ImgScroll", DbType.String) { Value = m.ScrollImgUrl };
            
            DataProcs.ProcParams(AdminSqlConnection, proc, param);
        }

        public void UpdateHomeScrollStatus(bool b)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetHomeScrollStatus");
            var param = new SqlParameter("@IsEnabled", DbType.Boolean) { Value = b };
            
            DataProcs.ProcOneParam(AdminSqlConnection, proc, param);
        }


        public void DeleteScrollImage(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDeleteScrollGridImage");
            var param = new SqlParameter("@ManufId", DbType.Int32) { Value = id };
            
            DataProcs.ProcOneParam(AdminSqlConnection, proc, param);
        }
        
        

        
    }
}