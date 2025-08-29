using AgBase;
using AgMvcAdmin.Common;
using AgMvcAdmin.Controllers;
using AgMvcAdmin.Models.Common;
using AgMvcAdmin.Models.Menus;
using AppBase;
using PdfSharp.Pdf.Content.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebBase.Configuration;

namespace AgMvcAdmin.Models
{
    public class OrderContext : BaseModel
    {
        private readonly string BInqDir = ConfigurationHelper.GetPropertyValue("application", "a8");
        private readonly string BPathDir = ConfigurationHelper.GetPropertyValue("application", "a1");
        private readonly string PthGn = ConfigurationHelper.GetPropertyValue("application", "a4");
        private readonly string PthAm = ConfigurationHelper.GetPropertyValue("application", "a5");
        private readonly string PthMd = ConfigurationHelper.GetPropertyValue("application", "a6");

        private readonly string CusGn = ConfigurationHelper.GetPropertyValue("application", "a9");
        private readonly string CusAm = ConfigurationHelper.GetPropertyValue("application", "a10");
        private readonly string CusMd = ConfigurationHelper.GetPropertyValue("application", "a11");

        private readonly string AcqGn = ConfigurationHelper.GetPropertyValue("application", "a12");
        private readonly string AcqAm = ConfigurationHelper.GetPropertyValue("application", "a13");
        private readonly string AcqMd = ConfigurationHelper.GetPropertyValue("application", "a14");

        private readonly string ConGn = ConfigurationHelper.GetPropertyValue("application", "a15");
        private readonly string ConAm = ConfigurationHelper.GetPropertyValue("application", "a16");
        private readonly string ConMd = ConfigurationHelper.GetPropertyValue("application", "a17");

        private readonly string SvcBp = ConfigurationHelper.GetPropertyValue("application", "s1");
        private readonly string iStk = ConfigurationHelper.GetPropertyValue("application", "s150");
        private readonly string iCus = ConfigurationHelper.GetPropertyValue("application", "s151");

        public CustomerBaseModel GetLocation(int id)
        {
            var c = new CustomerBaseModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetLocationByID");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.VarChar) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return c;

                dr.Read();

                var i0 = 0;
                long n64 = 0;

                var v1 = dr["ShopName"].ToString();
                var v2 = dr["ShopAddress"].ToString();
                var v3 = dr["ShopCity"].ToString();
                var v4 = dr["StateAbbrev"].ToString();
                var v5 = Int64.TryParse(dr["Phone"].ToString(), out n64) ? PhoneToString(Convert.ToInt64(dr["Phone"])) : string.Empty;

                var i1 = Int32.TryParse(dr["ZipCode"].ToString(), out i0) ? Convert.ToInt32(dr["ZipCode"]) : i0;

                c = new CustomerBaseModel(v1, v2, v3, v4, v5, i1);
            }

            return c;
        }


        public List<SearchItem> SearchGuns(GunModel m)
        {
            var s = new List<SearchItem>();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcOrderGunSearch");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[5];
                parameters[0] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.ManufId };
                parameters[1] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = m.GunTypeId };
                parameters[2] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.CaliberId };
                parameters[3] = new SqlParameter("@CaOkay", SqlDbType.Int) { Value = m.CaOkId };
                parameters[4] = new SqlParameter("@SearchTxt", SqlDbType.VarChar) { Value = m.SearchText };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return s;

                var i0 = 0;
                var b0 = false;
                double d0 = 0.00;

                var h = GetHostUrl();
                var dir = ConfigurationHelper.GetPropertyValue("application", "m10");
                var ddr = DecryptIt(dir);
                var dbp = DecryptIt(BPathDir);
                var pgn = DecryptIt(PthGn);


                while (dr.Read())
                {
                    var i1 = Int32.TryParse(dr["InStockID"].ToString(), out i0) ? Convert.ToInt32(dr["InStockID"]) : i0;
                    var i2 = Int32.TryParse(dr["MasterID"].ToString(), out i0) ? Convert.ToInt32(dr["MasterID"]) : i0;
                    var i3 = Int32.TryParse(dr["ManufID"].ToString(), out i0) ? Convert.ToInt32(dr["ManufID"]) : i0;

                    var i4 = Int32.TryParse(dr["SsiUnits"].ToString(), out i0) ? Convert.ToInt32(dr["SsiUnits"]) : i0;
                    var i5 = Int32.TryParse(dr["CssUnits"].ToString(), out i0) ? Convert.ToInt32(dr["CssUnits"]) : i0;
                    var i6 = Int32.TryParse(dr["LipUnits"].ToString(), out i0) ? Convert.ToInt32(dr["LipUnits"]) : i0;
                    var i7 = Int32.TryParse(dr["MgeUnits"].ToString(), out i0) ? Convert.ToInt32(dr["MgeUnits"]) : i0;
                    var i8 = Int32.TryParse(dr["DavUnits"].ToString(), out i0) ? Convert.ToInt32(dr["DavUnits"]) : i0;
                    var i9 = Int32.TryParse(dr["RsrUnits"].ToString(), out i0) ? Convert.ToInt32(dr["RsrUnits"]) : i0;
                    var i10 = Int32.TryParse(dr["AmrUnits"].ToString(), out i0) ? Convert.ToInt32(dr["AmrUnits"]) : i0;
                    var i11 = Int32.TryParse(dr["BhcUnits"].ToString(), out i0) ? Convert.ToInt32(dr["BhcUnits"]) : i0;
                    var i12 = Int32.TryParse(dr["ZanUnits"].ToString(), out i0) ? Convert.ToInt32(dr["ZanUnits"]) : i0;
                    var i13 = Int32.TryParse(dr["KrlUnits"].ToString(), out i0) ? Convert.ToInt32(dr["KrlUnits"]) : i0;
                    var i14 = Int32.TryParse(dr["HseUnits"].ToString(), out i0) ? Convert.ToInt32(dr["HseUnits"]) : i0;
                    var i15 = Int32.TryParse(dr["WyoUnits"].ToString(), out i0) ? Convert.ToInt32(dr["WyoUnits"]) : i0;

                    var i16 = Int32.TryParse(dr["MarginSSI"].ToString(), out i0) ? Convert.ToInt32(dr["MarginSSI"]) : i0;
                    var i17 = Int32.TryParse(dr["MarginCSS"].ToString(), out i0) ? Convert.ToInt32(dr["MarginCSS"]) : i0;
                    var i18 = Int32.TryParse(dr["MarginLIP"].ToString(), out i0) ? Convert.ToInt32(dr["MarginLIP"]) : i0;
                    var i19 = Int32.TryParse(dr["MarginMGE"].ToString(), out i0) ? Convert.ToInt32(dr["MarginMGE"]) : i0;
                    var i20 = Int32.TryParse(dr["MarginDAV"].ToString(), out i0) ? Convert.ToInt32(dr["MarginDAV"]) : i0;
                    var i21 = Int32.TryParse(dr["MarginRSR"].ToString(), out i0) ? Convert.ToInt32(dr["MarginRSR"]) : i0;
                    var i22 = Int32.TryParse(dr["MarginAMR"].ToString(), out i0) ? Convert.ToInt32(dr["MarginAMR"]) : i0;
                    var i23 = Int32.TryParse(dr["MarginBHC"].ToString(), out i0) ? Convert.ToInt32(dr["MarginBHC"]) : i0;
                    var i24 = Int32.TryParse(dr["MarginZAN"].ToString(), out i0) ? Convert.ToInt32(dr["MarginZAN"]) : i0;
                    var i25 = Int32.TryParse(dr["MarginKRL"].ToString(), out i0) ? Convert.ToInt32(dr["MarginKRL"]) : i0;
                    var i26 = Int32.TryParse(dr["MarginHSE"].ToString(), out i0) ? Convert.ToInt32(dr["MarginHSE"]) : i0;
                    var i27 = Int32.TryParse(dr["MarginWYO"].ToString(), out i0) ? Convert.ToInt32(dr["MarginWYO"]) : i0;

                    var i28 = Int32.TryParse(dr["CodeSSI"].ToString(), out i0) ? Convert.ToInt32(dr["CodeSSI"]) : i0;
                    var i29 = Int32.TryParse(dr["CodeCSS"].ToString(), out i0) ? Convert.ToInt32(dr["CodeCSS"]) : i0;
                    var i30 = Int32.TryParse(dr["CodeLIP"].ToString(), out i0) ? Convert.ToInt32(dr["CodeLIP"]) : i0;
                    var i31 = Int32.TryParse(dr["CodeMGE"].ToString(), out i0) ? Convert.ToInt32(dr["CodeMGE"]) : i0;
                    var i32 = Int32.TryParse(dr["CodeDAV"].ToString(), out i0) ? Convert.ToInt32(dr["CodeDAV"]) : i0;
                    var i33 = Int32.TryParse(dr["CodeRSR"].ToString(), out i0) ? Convert.ToInt32(dr["CodeRSR"]) : i0;
                    var i34 = Int32.TryParse(dr["CodeAMR"].ToString(), out i0) ? Convert.ToInt32(dr["CodeAMR"]) : i0;
                    var i35 = Int32.TryParse(dr["CodeBHC"].ToString(), out i0) ? Convert.ToInt32(dr["CodeBHC"]) : i0;
                    var i36 = Int32.TryParse(dr["CodeZAN"].ToString(), out i0) ? Convert.ToInt32(dr["CodeZAN"]) : i0;
                    var i37 = Int32.TryParse(dr["CodeKRL"].ToString(), out i0) ? Convert.ToInt32(dr["CodeKRL"]) : i0;
                    var i38 = Int32.TryParse(dr["CodeHSE"].ToString(), out i0) ? Convert.ToInt32(dr["CodeHSE"]) : i0;
                    var i39 = Int32.TryParse(dr["CodeWYO"].ToString(), out i0) ? Convert.ToInt32(dr["CodeWYO"]) : i0;


                    var d1 = Double.TryParse(dr["CostSsi"].ToString(), out d0) ? Convert.ToDouble(dr["CostSsi"]) : d0;
                    var d2 = Double.TryParse(dr["CostCss"].ToString(), out d0) ? Convert.ToDouble(dr["CostCss"]) : d0;
                    var d3 = Double.TryParse(dr["CostLip"].ToString(), out d0) ? Convert.ToDouble(dr["CostLip"]) : d0;
                    var d4 = Double.TryParse(dr["CostMge"].ToString(), out d0) ? Convert.ToDouble(dr["CostMge"]) : d0;
                    var d5 = Double.TryParse(dr["CostDav"].ToString(), out d0) ? Convert.ToDouble(dr["CostDav"]) : d0;
                    var d6 = Double.TryParse(dr["CostRsr"].ToString(), out d0) ? Convert.ToDouble(dr["CostRsr"]) : d0;
                    var d7 = Double.TryParse(dr["CostAmr"].ToString(), out d0) ? Convert.ToDouble(dr["CostAmr"]) : d0;
                    var d8 = Double.TryParse(dr["CostBhc"].ToString(), out d0) ? Convert.ToDouble(dr["CostBhc"]) : d0;
                    var d9 = Double.TryParse(dr["CostZan"].ToString(), out d0) ? Convert.ToDouble(dr["CostZan"]) : d0;
                    var d10 = Double.TryParse(dr["CostKrl"].ToString(), out d0) ? Convert.ToDouble(dr["CostKrl"]) : d0;
                    var d11 = Double.TryParse(dr["CostHSE"].ToString(), out d0) ? Convert.ToDouble(dr["CostHSE"]) : d0;
                    var d12 = Double.TryParse(dr["CostWYO"].ToString(), out d0) ? Convert.ToDouble(dr["CostWYO"]) : d0;
                    var d13 = Double.TryParse(dr["AskingPrice"].ToString(), out d0) ? Convert.ToDouble(dr["AskingPrice"]) : d0;

                    var d14 = Double.TryParse(dr["GrossSSI"].ToString(), out d0) ? Convert.ToDouble(dr["GrossSSI"]) : d0;
                    var d15 = Double.TryParse(dr["GrossCSS"].ToString(), out d0) ? Convert.ToDouble(dr["GrossCSS"]) : d0;
                    var d16 = Double.TryParse(dr["GrossLIP"].ToString(), out d0) ? Convert.ToDouble(dr["GrossLIP"]) : d0;
                    var d17 = Double.TryParse(dr["GrossMGE"].ToString(), out d0) ? Convert.ToDouble(dr["GrossMGE"]) : d0;
                    var d18 = Double.TryParse(dr["GrossDAV"].ToString(), out d0) ? Convert.ToDouble(dr["GrossDAV"]) : d0;
                    var d19 = Double.TryParse(dr["GrossRSR"].ToString(), out d0) ? Convert.ToDouble(dr["GrossRSR"]) : d0;
                    var d20 = Double.TryParse(dr["GrossAMR"].ToString(), out d0) ? Convert.ToDouble(dr["GrossAMR"]) : d0;
                    var d21 = Double.TryParse(dr["GrossBHC"].ToString(), out d0) ? Convert.ToDouble(dr["GrossBHC"]) : d0;
                    var d22 = Double.TryParse(dr["GrossZAN"].ToString(), out d0) ? Convert.ToDouble(dr["GrossZAN"]) : d0;
                    var d23 = Double.TryParse(dr["GrossKRL"].ToString(), out d0) ? Convert.ToDouble(dr["GrossKRL"]) : d0;
                    var d24 = Double.TryParse(dr["GrossHSE"].ToString(), out d0) ? Convert.ToDouble(dr["GrossHSE"]) : d0;
                    var d25 = Double.TryParse(dr["GrossWYO"].ToString(), out d0) ? Convert.ToDouble(dr["GrossWYO"]) : d0;

                    var b1 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;
                    var b2 = Boolean.TryParse(dr["IsSSI"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSSI"]) : b0;
                    var b3 = Boolean.TryParse(dr["IsCSS"].ToString(), out b0) ? Convert.ToBoolean(dr["IsCSS"]) : b0;
                    var b4 = Boolean.TryParse(dr["IsLIP"].ToString(), out b0) ? Convert.ToBoolean(dr["IsLIP"]) : b0;
                    var b5 = Boolean.TryParse(dr["IsMGE"].ToString(), out b0) ? Convert.ToBoolean(dr["IsMGE"]) : b0;
                    var b6 = Boolean.TryParse(dr["IsDAV"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDAV"]) : b0;
                    var b7 = Boolean.TryParse(dr["IsRSR"].ToString(), out b0) ? Convert.ToBoolean(dr["IsRSR"]) : b0;
                    var b8 = Boolean.TryParse(dr["IsAMR"].ToString(), out b0) ? Convert.ToBoolean(dr["IsAMR"]) : b0;
                    var b9 = Boolean.TryParse(dr["IsBHC"].ToString(), out b0) ? Convert.ToBoolean(dr["IsBHC"]) : b0;
                    var b10 = Boolean.TryParse(dr["IsZAN"].ToString(), out b0) ? Convert.ToBoolean(dr["IsZAN"]) : b0;
                    var b11 = Boolean.TryParse(dr["IsKRL"].ToString(), out b0) ? Convert.ToBoolean(dr["IsKRL"]) : b0;
                    var b12 = Boolean.TryParse(dr["IsHSE"].ToString(), out b0) ? Convert.ToBoolean(dr["IsHSE"]) : b0;
                    var b13 = Boolean.TryParse(dr["IsWYO"].ToString(), out b0) ? Convert.ToBoolean(dr["IsWYO"]) : b0;

                    var v1 = dr["ImageName"].ToString();
                    var v2 = dr["DistributorCode"].ToString();
                    var v3 = dr["GunDesc"].ToString();

                    var v4 = string.Format("{0}%", Convert.ToInt32(dr["MarginSSI"]));
                    var v5 = string.Format("{0}%", Convert.ToInt32(dr["MarginCSS"]));
                    var v6 = string.Format("{0}%", Convert.ToInt32(dr["MarginLIP"]));
                    var v7 = string.Format("{0}%", Convert.ToInt32(dr["MarginMGE"]));
                    var v8 = string.Format("{0}%", Convert.ToInt32(dr["MarginDAV"]));
                    var v9 = string.Format("{0}%", Convert.ToInt32(dr["MarginRSR"]));
                    var v10 = string.Format("{0}%", Convert.ToInt32(dr["MarginAMR"]));
                    var v11 = string.Format("{0}%", Convert.ToInt32(dr["MarginBHC"]));
                    var v12 = string.Format("{0}%", Convert.ToInt32(dr["MarginZAN"]));
                    var v13 = string.Format("{0}%", Convert.ToInt32(dr["MarginKRL"]));
                    var v14 = string.Format("{0}%", Convert.ToInt32(dr["MarginHSE"]));
                    var v15 = string.Format("{0}%", Convert.ToInt32(dr["MarginWYO"]));

                    var prc = String.Format("{0:C}", d13);


                    //TODO: Calculate Margin for In-House 

                    var imgUrl = string.Empty;
                    var t = DateTime.Now.Ticks;

                    if (v1.Length > 0)
                    {
                        if (b1) { imgUrl = string.Format("{0}/{1}/{2}/{3}/L/{4}", h, dbp, ddr, v2, v1); }
                        else
                                { imgUrl = string.Format("{0}/{1}/{2}/{3}?{4}", h, dbp, pgn, v1, t); }
                    }

                    var id = b1 ? i2 : i1; // reference searches based on master id or in-stock id

                    var si = new List<SourceItem>();

 

                    if (b2) { var si2 = new SourceItem(Enum.GetName(typeof(DistCodes), (int)DistCodes.SSI), v4, i28, i4, string.Format("{0:C}", d1), string.Format("{0:C}", d14)); si.Add(si2); } //SSI 
                    if (b3) { var si3 = new SourceItem(Enum.GetName(typeof(DistCodes), (int)DistCodes.CSS), v5, i29, i5, string.Format("{0:C}", d2), string.Format("{0:C}", d15)); si.Add(si3); } //CSS 
                    if (b4) { var si4 = new SourceItem(Enum.GetName(typeof(DistCodes), (int)DistCodes.LIP), v6, i30, i6, string.Format("{0:C}", d3), string.Format("{0:C}", d16)); si.Add(si4); } //LIP 
                    if (b5) { var si5 = new SourceItem(Enum.GetName(typeof(DistCodes), (int)DistCodes.MGE), v7, i31, i7, string.Format("{0:C}", d4), string.Format("{0:C}", d17)); si.Add(si5); } //MGE 
                    if (b6) { var si6 = new SourceItem(Enum.GetName(typeof(DistCodes), (int)DistCodes.DAV), v8, i32, i8, string.Format("{0:C}", d5), string.Format("{0:C}", d18)); si.Add(si6); } //DAV 
                    if (b7) { var si7 = new SourceItem(Enum.GetName(typeof(DistCodes), (int)DistCodes.RSR), v9, i33, i9, string.Format("{0:C}", d6), string.Format("{0:C}", d19)); si.Add(si7); } //RSR 
                    if (b8) { var si8 = new SourceItem(Enum.GetName(typeof(DistCodes), (int)DistCodes.AMR), v10, i34, i10, string.Format("{0:C}", d7), string.Format("{0:C}", d20)); si.Add(si8); } //AMR 
                    if (b9) { var si9 = new SourceItem(Enum.GetName(typeof(DistCodes), (int)DistCodes.BHC), v11, i35, i11, string.Format("{0:C}", d8), string.Format("{0:C}", d21)); si.Add(si9); } //BHC 
                    if (b10) { var si10 = new SourceItem(Enum.GetName(typeof(DistCodes), (int)DistCodes.ZAN), v12, i36, i12, string.Format("{0:C}", d9), string.Format("{0:C}", d22)); si.Add(si10); } //ZAN 
                    if (b11) { var si11 = new SourceItem(Enum.GetName(typeof(DistCodes), (int)DistCodes.KRL), v13, i37, i13, string.Format("{0:C}", d10), string.Format("{0:C}", d23)); si.Add(si11); } //KRL 
                    if (b12) { var si12 = new SourceItem(Enum.GetName(typeof(DistCodes), (int)DistCodes.HSE), v14, i38, i14, string.Format("{0:C}", d11), string.Format("{0:C}", d24)); si.Add(si12); } //HSE 
                    if (b13) { var si13 = new SourceItem(Enum.GetName(typeof(DistCodes), (int)DistCodes.WYO), v15, i39, i15, string.Format("{0:C}", d12), string.Format("{0:C}", d25)); si.Add(si13); } //WYO 

                    var sci = new SearchItem(i1, i2, v3, imgUrl, prc, b1, si);
                    s.Add(sci);
                }
            }

            return s;
        }


        public List<SearchItem> SearchAmmo(AmmoModel m)
        {
            var s = new List<SearchItem>();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcOrderAmmoSearch");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[5];
                parameters[0] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.ManufId };
                parameters[2] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.CaliberId };
                parameters[1] = new SqlParameter("@AmmoTypeID", SqlDbType.Int) { Value = m.AmmoTypeId };
                parameters[3] = new SqlParameter("@BulletTypeID", SqlDbType.Int) { Value = m.BulletTypeId };
                parameters[4] = new SqlParameter("@SearchTxt", SqlDbType.VarChar) { Value = m.SearchText };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return s;

                var i0 = 0;
                var b0 = false;
                double d0 = 0.00;

                var h = GetHostUrl();
                var dir = ConfigurationHelper.GetPropertyValue("application", "m10");
                var ddr = DecryptIt(dir);
                var dbp = DecryptIt(BPathDir);
                var pam = DecryptIt(PthAm);


                while (dr.Read())
                {
                    var i1 = Int32.TryParse(dr["InStockID"].ToString(), out i0) ? Convert.ToInt32(dr["InStockID"]) : i0;
                    var i2 = Int32.TryParse(dr["MasterID"].ToString(), out i0) ? Convert.ToInt32(dr["MasterID"]) : i0;
                    var i3 = Int32.TryParse(dr["ManufID"].ToString(), out i0) ? Convert.ToInt32(dr["ManufID"]) : i0;

                    var i14 = Int32.TryParse(dr["HseUnits"].ToString(), out i0) ? Convert.ToInt32(dr["HseUnits"]) : i0;
                    var i15 = Int32.TryParse(dr["WyoUnits"].ToString(), out i0) ? Convert.ToInt32(dr["WyoUnits"]) : i0;

                    var i26 = Int32.TryParse(dr["MarginHSE"].ToString(), out i0) ? Convert.ToInt32(dr["MarginHSE"]) : i0;
                    var i27 = Int32.TryParse(dr["MarginWYO"].ToString(), out i0) ? Convert.ToInt32(dr["MarginWYO"]) : i0;

                    var d11 = Double.TryParse(dr["CostHSE"].ToString(), out d0) ? Convert.ToDouble(dr["CostHSE"]) : d0;
                    var d12 = Double.TryParse(dr["CostWYO"].ToString(), out d0) ? Convert.ToDouble(dr["CostWYO"]) : d0;
                    var d13 = Double.TryParse(dr["AskingPrice"].ToString(), out d0) ? Convert.ToDouble(dr["AskingPrice"]) : d0;

                    var d24 = Double.TryParse(dr["GrossHSE"].ToString(), out d0) ? Convert.ToDouble(dr["GrossHSE"]) : d0;
                    var d25 = Double.TryParse(dr["GrossWYO"].ToString(), out d0) ? Convert.ToDouble(dr["GrossWYO"]) : d0;

                    var b1 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;
                    var b12 = Boolean.TryParse(dr["IsHSE"].ToString(), out b0) ? Convert.ToBoolean(dr["IsHSE"]) : b0;
                    var b13 = Boolean.TryParse(dr["IsWYO"].ToString(), out b0) ? Convert.ToBoolean(dr["IsWYO"]) : b0;

                    var v1 = dr["ImageName"].ToString();
                    var v2 = dr["DistributorCode"].ToString();
                    var v3 = dr["ItemDesc"].ToString();

                    var v14 = string.Format("{0}%", Convert.ToInt32(dr["MarginHSE"]));
                    var v15 = string.Format("{0}%", Convert.ToInt32(dr["MarginWYO"]));

                    if (b12) // HSE Margin
                    {
                        d24 = d13 - d11;
                        var mgn = d24 / d13;
                        v14 = string.Format("{0:0%}", mgn);
                    }

                    if (b13) // WYO Margin
                    {
                        d25 = d13 - d12;
                        var mgn = d25 / d13;
                        v15 = string.Format("{0:0%}", mgn);
                    }

                    var prc = String.Format("{0:C}", d13);


                    //TODO: Calculate Margin for In-House 

                    var imgUrl = string.Empty;
                    var t = DateTime.Now.Ticks;

                    if (v1.Length > 0)
                    {
                        if (b1) { imgUrl = string.Format("{0}/{1}/{2}/{3}/L/{4}", h, dbp, ddr, v2, v1); }
                        else
                        { imgUrl = string.Format("{0}/{1}/{2}/{3}?{4}", h, dbp, pam, v1, t); }
                    }

                    var id = b1 ? i2 : i1; // reference searches based on master id or in-stock id

                    var si = new List<SourceItem>();

                    if (b12) { var si12 = new SourceItem("HSE", v14, 25, i14, string.Format("{0:C}", d11), string.Format("{0:C}", d24)); si.Add(si12); } // HSE 
                    if (b13) { var si13 = new SourceItem("WYO", v15, 99, i15, string.Format("{0:C}", d12), string.Format("{0:C}", d25)); si.Add(si13); } // WYO 

                    var sci = new SearchItem(i1, i2, v3, imgUrl, prc, b1, si);
                    s.Add(sci);
                }
            }

            return s;
        }


        public List<SearchItem> SearchMerch(MerchandiseModel m)
        {
            var s = new List<SearchItem>();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcOrderMerchSearch");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[3];
                parameters[0] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.ManufId };
                parameters[1] = new SqlParameter("@CategoryID", SqlDbType.Int) { Value = m.CategoryId };
                parameters[2] = new SqlParameter("@SearchTxt", SqlDbType.VarChar) { Value = m.SearchText };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return s;

                var i0 = 0;
                var b0 = false;
                double d0 = 0.00;

                var h = GetHostUrl();
                var dir = ConfigurationHelper.GetPropertyValue("application", "m10");
                var ddr = DecryptIt(dir);
                var dbp = DecryptIt(BPathDir);
                var pam = DecryptIt(PthMd);


                while (dr.Read())
                {
                    var i1 = Int32.TryParse(dr["InStockID"].ToString(), out i0) ? Convert.ToInt32(dr["InStockID"]) : i0;
                    var i2 = Int32.TryParse(dr["MasterID"].ToString(), out i0) ? Convert.ToInt32(dr["MasterID"]) : i0;
                    var i3 = Int32.TryParse(dr["ManufID"].ToString(), out i0) ? Convert.ToInt32(dr["ManufID"]) : i0;

                    var i14 = Int32.TryParse(dr["HseUnits"].ToString(), out i0) ? Convert.ToInt32(dr["HseUnits"]) : i0;
                    var i15 = Int32.TryParse(dr["WyoUnits"].ToString(), out i0) ? Convert.ToInt32(dr["WyoUnits"]) : i0;

                    var i26 = Int32.TryParse(dr["MarginHSE"].ToString(), out i0) ? Convert.ToInt32(dr["MarginHSE"]) : i0;
                    var i27 = Int32.TryParse(dr["MarginWYO"].ToString(), out i0) ? Convert.ToInt32(dr["MarginWYO"]) : i0;

                    var d11 = Double.TryParse(dr["CostHSE"].ToString(), out d0) ? Convert.ToDouble(dr["CostHSE"]) : d0;
                    var d12 = Double.TryParse(dr["CostWYO"].ToString(), out d0) ? Convert.ToDouble(dr["CostWYO"]) : d0;
                    var d13 = Double.TryParse(dr["AskingPrice"].ToString(), out d0) ? Convert.ToDouble(dr["AskingPrice"]) : d0;

                    var d24 = Double.TryParse(dr["GrossHSE"].ToString(), out d0) ? Convert.ToDouble(dr["GrossHSE"]) : d0;
                    var d25 = Double.TryParse(dr["GrossWYO"].ToString(), out d0) ? Convert.ToDouble(dr["GrossWYO"]) : d0;

                    var b1 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;
                    var b12 = Boolean.TryParse(dr["IsHSE"].ToString(), out b0) ? Convert.ToBoolean(dr["IsHSE"]) : b0;
                    var b13 = Boolean.TryParse(dr["IsWYO"].ToString(), out b0) ? Convert.ToBoolean(dr["IsWYO"]) : b0;

                    var v1 = dr["ImageName"].ToString();
                    var v2 = dr["DistributorCode"].ToString();
                    var v3 = dr["ItemDesc"].ToString();

                    var v14 = string.Format("{0}%", Convert.ToInt32(dr["MarginHSE"]));
                    var v15 = string.Format("{0}%", Convert.ToInt32(dr["MarginWYO"]));

                    if (b12) // HSE Margin
                    {
                        d24 = d13 - d11;
                        var mgn = d24 / d13;
                        v14 = string.Format("{0:0%}", mgn);
                    }

                    if (b13) // WYO Margin
                    {
                        d25 = d13 - d12;
                        var mgn = d25 / d13;
                        v15 = string.Format("{0:0%}", mgn);
                    }

                    var prc = String.Format("{0:C}", d13);


                    //TODO: Calculate Margin for In-House 

                    var imgUrl = string.Empty;
                    var t = DateTime.Now.Ticks;

                    if (v1.Length > 0)
                    {
                        if (b1) { imgUrl = string.Format("{0}/{1}/{2}/{3}/L/{4}", h, dbp, ddr, v2, v1); }
                        else
                        { imgUrl = string.Format("{0}/{1}/{2}/{3}?{4}", h, dbp, pam, v1, t); }
                    }

                    var id = b1 ? i2 : i1; // reference searches based on master id or in-stock id

                    var si = new List<SourceItem>();

                    if (b12) { var si12 = new SourceItem("HSE", v14, 25, i14, string.Format("{0:C}", d11), string.Format("{0:C}", d24)); si.Add(si12); } //HSE 
                    if (b13) { var si13 = new SourceItem(Enum.GetName(typeof(DistCodes), 26), v15, 99, i15, string.Format("{0:C}", d12), string.Format("{0:C}", d25)); si.Add(si13); } //WYO 

                    var sci = new SearchItem(i1, i2, v3, imgUrl, prc, b1, si);
                    s.Add(sci);
                }
            }

            return s;
        }

        public OrderModel StartOrder(OrderModel m)
        {
            var om = new OrderModel();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCookOrder");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[5];
                parameters[0] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = m.UserID};
                parameters[1] = new SqlParameter("@SalesRepID", SqlDbType.Int) { Value = m.SalesRepId };
                parameters[2] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = m.LocationId };
                parameters[3] = new SqlParameter("@IsQuote", SqlDbType.Bit) { Value = m.IsQuote };
                parameters[4] = new SqlParameter("@OrderDate", SqlDbType.DateTime) { Value = m.OrderDate };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return om;

                var i0 = 0;

                dr.Read();

                var id = Int32.TryParse(dr["OrderMasterID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderMasterID"]) : i0;
                var nb = dr["OrderNumber"].ToString();

                om = new OrderModel(id, nb);

            }

            return om;
        }

        public void DeleteOrder(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDeleteOrder");
            var param = new SqlParameter("@ID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(EcsmsSqlConnection, proc, param);
        }

        public void DeleteTransaction(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDeleteTransaction");
            var param = new SqlParameter("@TransID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(EcsmsSqlConnection, proc, param);
        }

        public void DeleteTransactionCart(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDeleteTransactionCart");
            var param = new SqlParameter("@TransID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(EcsmsSqlConnection, proc, param);
        }

        public void ResetFflTransType(int id, bool ppt)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcResetFFLTransType");

            var parameters = new IDataParameter[2];
            parameters[0] = new SqlParameter("@TransID", SqlDbType.Int) { Value = id };
            parameters[1] = new SqlParameter("@IsPPT", SqlDbType.Bit) { Value = ppt };
            DataProcs.ProcParams(EcsmsSqlConnection, proc, parameters);
        }



        public void UpdateBaseOrder(OrderModel m)
        {
            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateBasicOrder");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[6];
                parameters[0] = new SqlParameter("@OrderMasterID", SqlDbType.Int) { Value = m.OrderMasterId };
                parameters[1] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = m.UserID };
                parameters[2] = new SqlParameter("@SalesRepID", SqlDbType.Int) { Value = m.SalesRepId };
                parameters[3] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = m.LocationId };
                parameters[4] = new SqlParameter("@IsQuote", SqlDbType.Bit) { Value = m.IsQuote };
                parameters[5] = new SqlParameter("@OrderDate", SqlDbType.DateTime) { Value = m.OrderDate };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                cmd.ExecuteReader();
            }
        }


        public int RunOrderTrans(OrderModel m)
        {
            var i = 0;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCookOrderTransaction");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@OrderMasterID", SqlDbType.Int) { Value = m.OrderMasterId };
                parameters[1] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = m.TransTypeId };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return i;

                var i0 = 0;

                dr.Read();

                i = Int32.TryParse(dr["TransactionID"].ToString(), out i0) ? Convert.ToInt32(dr["TransactionID"]) : i0;
            }

            return i;
        }

        public CartView AddSaleToCart(CartModel m)
        {
            var cv = new CartView();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartSaleAddItem"); 
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[7];
                parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = m.TransactionId };
                parameters[1] = new SqlParameter("@DistributorID", SqlDbType.Int) { Value = m.DistributorId };
                parameters[2] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = m.SupplierId };
                parameters[3] = new SqlParameter("@InStockID", SqlDbType.Int) { Value = m.InStockId };
                parameters[4] = new SqlParameter("@MasterID", SqlDbType.Int) { Value = m.MasterId };
                parameters[5] = new SqlParameter("@Units", SqlDbType.Int) { Value = m.Units };
                parameters[6] = new SqlParameter("@Price", SqlDbType.Decimal) { Value = m.Price };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cv;

                dr.Read();

                var i0 = 0;
                var i1 = Int32.TryParse(dr["CartID"].ToString(), out i0) ? Convert.ToInt32(dr["CartID"]) : i0;

                cv = ViewCart(m.TransactionId);
                cv.CartId = i1;

                return cv;
            }
        }

        public CartView AddCartTransfer(int tid, int bid)
        {
            var cv = new CartView();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartAddTransfer");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = tid };
                parameters[1] = new SqlParameter("@BasisID", SqlDbType.Int) { Value = bid };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cv;

                dr.Read();

                cv = ViewCart(tid);

                return cv;
            }
        }

        public CartView ViewCart(int transId)
        {
            var cv = new CartView();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartView");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = transId };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cv;

                var i0 = 0;

                dr.Read();

                var cm = new List<CartModel>();

                var c1 = Int32.TryParse(dr["ItemCount"].ToString(), out i0) ? Convert.ToInt32(dr["ItemCount"]) : i0;
                cv.ItemCount = c1;
                cv.CartItems = cm;

                dr.NextResult();
                if (!dr.HasRows) return cv;

                double d0 = 0.00;
                bool b0 = false;

                while (dr.Read())
                {
                    var i1 = Int32.TryParse(dr["RowNumber"].ToString(), out i0) ? Convert.ToInt32(dr["RowNumber"]) : i0;
                    var i2 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                    var i3 = Int32.TryParse(dr["CategoryID"].ToString(), out i0) ? Convert.ToInt32(dr["CategoryID"]) : i0;
                    var i4 = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;
                    var i5 = Int32.TryParse(dr["ImageDist"].ToString(), out i0) ? Convert.ToInt32(dr["ImageDist"]) : i0;
                    var i6 = Int32.TryParse(dr["TransTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["TransTypeID"]) : i0;

                    var d1 = Double.TryParse(dr["Price"].ToString(), out d0) ? Convert.ToDouble(dr["Price"]) : d0;
                    var d2 = Double.TryParse(dr["Extension"].ToString(), out d0) ? Convert.ToDouble(dr["Extension"]) : d0;
                    var d3 = Double.TryParse(dr["CartTotal"].ToString(), out d0) ? Convert.ToDouble(dr["CartTotal"]) : d0;
                    var d4 = Double.TryParse(dr["Insurance"].ToString(), out d0) ? Convert.ToDouble(dr["Insurance"]) : d0;
                    var d5 = Double.TryParse(dr["MonthlyRent"].ToString(), out d0) ? Convert.ToDouble(dr["MonthlyRent"]) : d0;
                    var d6 = Double.TryParse(dr["Parts"].ToString(), out d0) ? Convert.ToDouble(dr["Parts"]) : d0;
                    var d7 = Double.TryParse(dr["Repairs"].ToString(), out d0) ? Convert.ToDouble(dr["Repairs"]) : d0;

                    var s1 = dr["Category"].ToString();
                    var s2 = dr["Manufacturer"].ToString();
                    var s3 = dr["ModelName"].ToString();
                    var s4 = dr["ItemDesc"].ToString();
                    var s5 = dr["MfgPartNumber"].ToString();
                    var s6 = dr["Condition"].ToString();
                    var s7 = dr["ImageName"].ToString();
                    var s8 = dr["DistributorCode"].ToString();
                    var s9 = dr["Notes"].ToString();

                    var b1 = Boolean.TryParse(dr["InHouse"].ToString(), out b0) ? Convert.ToBoolean(dr["InHouse"]) : b0;
                    var b2 = Boolean.TryParse(dr["IsCustomOrder"].ToString(), out b0) ? Convert.ToBoolean(dr["IsCustomOrder"]) : b0;

                    var imgUrl = string.Empty;
                    var t = DateTime.Now.Ticks;

                    var h = GetHostUrl();
                    var dir = ConfigurationHelper.GetPropertyValue("application", "m10");




                    // if distributor code is null, match the folder name to the trans type

                    if (s7.Length > 0) //image Handler
                    {
                        var ddr = DecryptIt(dir);
                        var dbp = DecryptIt(BPathDir);
                        var fph = GetImgDir(i6, i3, b1, b2, s8, ddr);
                        imgUrl = string.Format("{0}/{1}/{2}/{3}?{4}", h, dbp, fph, s7, t);
                        //GetImgDir(int ttp, int cid, bool ihs, bool ico, string dsc, string dir)

                        //var xd = i6;
                        //var cat = Enum.GetName(typeof(ItemCategories), i3);
                        //var pth = string.Empty;
                        //var pfd = string.Empty;

                        //if (string.IsNullOrEmpty(s8)) // custom order
                        //{
                        //    if (b2) { xd = (int)PicFolders.Custom; }
                        //    pfd = Enum.GetName(typeof(PicFolders), xd);
                        //    pth = string.Format("{0}/{1}", pfd, cat);
                        //}
                        //else  // has disctributor
                        //{
                        //    if (b1)
                        //    {
                        //        xd = (int)PicFolders.InStock;
                        //        pfd = Enum.GetName(typeof(PicFolders), xd);
                        //        pth = string.Format("{0}/{1}", pfd, cat);
                        //    }
                        //    else
                        //    {
                        //        pth = string.Format("{0}/{1}/L", ddr, s8);
                        //    }
                        //}
                    }





                    //if (s7.Length > 0)
                    //{
                    //    //if (b1) // In House Image
                    //    //{
                    //    //    //var cat = Enum.GetName(typeof(ItemCategories), i3);
                    //    //    imgUrl = string.Format("{0}/{1}/{2}/{3}/{4}?{5}", h, DecryptIt(BPathDir), DecryptIt(iStk), cat, s7, t);
                    //    //}
                    //    //else if (b2) // Custom Image
                    //    //{
                    //    //    //var cat = Enum.GetName(typeof(ItemCategories), i3);
                    //    //    imgUrl = string.Format("{0}/{1}/{2}/{3}/{4}?{5}", h, DecryptIt(BPathDir), DecryptIt(iCus), cat, s7, t);
                    //    //}
                    //    //else { imgUrl = string.Format("{0}/{1}/{2}/{3}/L/{4}", h, dbp, ddr, s8, s7); }  // Distributor
                    //}

                    var c = new CartModel(i1, i2, i3, i4, i5, i6, s1, s2, s3, s4, s5, s6, imgUrl, s9, d1, d2, d3, d4, d5, d6, d7);
                    cm.Add(c);

                }

                cv.CartItems = cm;

                return cv;
            }
        }

        public CartView IncrementCartCount(int cartId, bool isAdded)
        {
            var cv = new CartView();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartAdjustQuantity");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = cartId };
                parameters[1] = new SqlParameter("@AddItem", SqlDbType.Int) { Value = isAdded };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cv;

                var i0 = 0;
                dr.Read();

                var id = Int32.TryParse(dr["TransID"].ToString(), out i0) ? Convert.ToInt32(dr["TransID"]) : i0;

                cv = ViewCart(id);

                return cv;

            }
        }

        public CartView DeleteCartItem(int cartId)
        {
            var cv = new CartView();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartDeleteItem");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = cartId };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cv;

                var i0 = 0;
                dr.Read();

                var id = Int32.TryParse(dr["TransID"].ToString(), out i0) ? Convert.ToInt32(dr["TransID"]) : i0;

                cv = ViewCart(id);

                return cv;

            }
        }

        public int AddOtherSvcAmmo(ServiceModel m)
        {
            var id = 0;
            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartOtherServicesAddAmmo");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[19];
                parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = m.Cart.TransactionId };
                parameters[1] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.Ammo.ManufId };
                parameters[2] = new SqlParameter("@AmmoTypeID", SqlDbType.Int) { Value = m.Ammo.AmmoTypeId };
                parameters[3] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.Ammo.CaliberId };
                parameters[4] = new SqlParameter("@BulletTypeID", SqlDbType.Int) { Value = m.Ammo.BulletTypeId };
                parameters[5] = new SqlParameter("@ConditionID", SqlDbType.Int) { Value = m.Ammo.ConditionId };
                parameters[6] = new SqlParameter("@RoundsPerBox", SqlDbType.Int) { Value = m.Ammo.RoundsPerBox };
                parameters[7] = new SqlParameter("@GrainWeight", SqlDbType.Int) { Value = m.Ammo.GrainWeight };
                parameters[8] = new SqlParameter("@ValuationID", SqlDbType.Int) { Value = m.ValuationId };
                parameters[9] = new SqlParameter("@Units", SqlDbType.Int) { Value = m.Cart.Units };
                parameters[10] = new SqlParameter("@ModelName", SqlDbType.VarChar) { Value = m.Ammo.ModelName == "" ? (object)DBNull.Value : m.Ammo.ModelName };
                parameters[11] = new SqlParameter("@MfgPartNumber", SqlDbType.VarChar) { Value = m.Ammo.MfgPartNumber == "" ? (object)DBNull.Value : m.Ammo.MfgPartNumber };
                parameters[12] = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = m.Ammo.UpcCode == "" ? (object)DBNull.Value : m.Ammo.UpcCode };
                parameters[13] = new SqlParameter("@Notes", SqlDbType.VarChar) { Value = m.Notes == "" ? (object)DBNull.Value : m.Notes };
                parameters[14] = new SqlParameter("@CommRate", SqlDbType.Decimal) { Value = m.CommRate };
                parameters[15] = new SqlParameter("@FlatAmount", SqlDbType.Decimal) { Value = m.FlatAmount };
                parameters[16] = new SqlParameter("@OfferPrice", SqlDbType.Decimal) { Value = m.OfferPrice };
                parameters[17] = new SqlParameter("@Insurance", SqlDbType.Decimal) { Value = m.Insurance };
                parameters[18] = new SqlParameter("@MonthlyRent", SqlDbType.Decimal) { Value = m.MonthlyRent };

                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return id;

                var i0 = 0;
                dr.Read();

                id = Int32.TryParse(dr["CartID"].ToString(), out i0) ? Convert.ToInt32(dr["CartID"]) : i0;
                return id;
            }
        }

        public int AddOtherSvcGun(ServiceModel m)
        {
            var id = 0;
            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartOtherServicesAddGun");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[30];
                parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = m.Cart.TransactionId };
                parameters[1] = new SqlParameter("@MasterID", SqlDbType.Int) { Value = m.Gun.MasterId };
                parameters[2] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.Gun.ManufId };
                parameters[3] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = m.Gun.GunTypeId };
                parameters[4] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.Gun.CaliberId };
                parameters[5] = new SqlParameter("@ActionID", SqlDbType.Int) { Value = m.Gun.ActionId };
                parameters[6] = new SqlParameter("@FinishID", SqlDbType.Int) { Value = m.Gun.FinishId };
                parameters[7] = new SqlParameter("@Capacity", SqlDbType.Int) { Value = m.Gun.CapacityInt };
                parameters[8] = new SqlParameter("@ConditionID", SqlDbType.Int) { Value = m.Gun.ConditionId };
                parameters[9] = new SqlParameter("@ValuationID", SqlDbType.Int) { Value = m.ValuationId };
                parameters[10] = new SqlParameter("@LockMakeID", SqlDbType.Int) { Value = m.Gun.LockMakeId };
                parameters[11] = new SqlParameter("@LockModelID", SqlDbType.Int) { Value = m.Gun.LockModelId };
                parameters[12] = new SqlParameter("@WeightLbs", SqlDbType.Int) { Value = m.Gun.WeightLb };
                parameters[13] = new SqlParameter("@HasLock", SqlDbType.Bit) { Value = m.Gun.HasLock };
                parameters[14] = new SqlParameter("@HasBox", SqlDbType.Bit) { Value = m.Gun.OrigBox };
                parameters[15] = new SqlParameter("@HasPpw", SqlDbType.Bit) { Value = m.Gun.OrigPaperwork };
                parameters[16] = new SqlParameter("@ModelName", SqlDbType.VarChar) { Value = m.Gun.ModelName == "" ? (object)DBNull.Value : m.Gun.ModelName };
                parameters[17] = new SqlParameter("@MfgPartNumber", SqlDbType.VarChar) { Value = m.Gun.MfgPartNumber == "" ? (object)DBNull.Value : m.Gun.MfgPartNumber };
                parameters[18] = new SqlParameter("@SerialNumber", SqlDbType.VarChar) { Value = m.Gun.SerialNumber == "" ? (object)DBNull.Value : m.Gun.SerialNumber };
                parameters[19] = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = m.Gun.UpcCode == "" ? (object)DBNull.Value : m.Gun.UpcCode };
                parameters[20] = new SqlParameter("@Notes", SqlDbType.VarChar) { Value = m.Notes == "" ? (object)DBNull.Value : m.Notes };
                parameters[21] = new SqlParameter("@BarrrelDec", SqlDbType.Decimal) { Value = m.Gun.BarrelDec };
                parameters[22] = new SqlParameter("@PartsCost", SqlDbType.Decimal) { Value = m.PartsCost };
                parameters[23] = new SqlParameter("@LaborCost", SqlDbType.Decimal) { Value = m.LaborCost };
                parameters[24] = new SqlParameter("@CommRate", SqlDbType.Decimal) { Value = m.CommRate };
                parameters[25] = new SqlParameter("@FlatAmount", SqlDbType.Decimal) { Value = m.FlatAmount };
                parameters[26] = new SqlParameter("@OfferPrice", SqlDbType.Decimal) { Value = m.OfferPrice };
                parameters[27] = new SqlParameter("@Insurance", SqlDbType.Decimal) { Value = m.Insurance };
                parameters[28] = new SqlParameter("@MonthlyRent", SqlDbType.Decimal) { Value = m.MonthlyRent };
                parameters[29] = new SqlParameter("@WeightOzs", SqlDbType.Decimal) { Value = m.Gun.WeightOz };


                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return id;

                var i0 = 0;
                dr.Read();

                id = Int32.TryParse(dr["CartID"].ToString(), out i0) ? Convert.ToInt32(dr["CartID"]) : i0;
                return id;
            }
        }

        public int AddOtherSvcMerch(ServiceModel m)
        {
            var id = 0;
            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartOtherServicesAddMerch");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[16];
                parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = m.Cart.TransactionId };
                parameters[1] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.Merch.ManufId };
                parameters[2] = new SqlParameter("@SubCatID", SqlDbType.Int) { Value = m.Merch.SubCategoryId };
                parameters[3] = new SqlParameter("@ConditionID", SqlDbType.Int) { Value = m.Merch.ConditionId };
                parameters[4] = new SqlParameter("@ShipSizeID", SqlDbType.Int) { Value = m.Merch.ShippingBoxId };
                parameters[5] = new SqlParameter("@ShipLbs", SqlDbType.Int) { Value = m.Merch.ShippingLbs };
                parameters[6] = new SqlParameter("@ItemsPerBox", SqlDbType.Int) { Value = m.Merch.ItemsPerBox };
                parameters[7] = new SqlParameter("@Units", SqlDbType.Int) { Value = m.Cart.Units };
                parameters[8] = new SqlParameter("@ModelName", SqlDbType.VarChar) { Value = m.Merch.ModelName == "" ? (object)DBNull.Value : m.Merch.ModelName };
                parameters[9] = new SqlParameter("@ItemDesc", SqlDbType.VarChar) { Value = m.Merch.ItemDesc == "" ? (object)DBNull.Value : m.Merch.ItemDesc };
                parameters[10] = new SqlParameter("@MfgPartNum", SqlDbType.VarChar) { Value = m.Merch.MfgPartNumber == "" ? (object)DBNull.Value : m.Merch.MfgPartNumber };
                parameters[11] = new SqlParameter("@Notes", SqlDbType.VarChar) { Value = m.Cart.Notes == "" ? (object)DBNull.Value : m.Cart.Notes };
                parameters[12] = new SqlParameter("@ShipOzs", SqlDbType.Decimal) { Value = m.Merch.ShippingOzs };
                parameters[13] = new SqlParameter("@Insurance", SqlDbType.Decimal) { Value = m.Insurance };
                parameters[14] = new SqlParameter("@MonthlyRent", SqlDbType.Decimal) { Value = m.MonthlyRent };
                parameters[15] = new SqlParameter("@OfferPrice", SqlDbType.Decimal) { Value = m.OfferPrice };

                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return id;

                var i0 = 0;
                dr.Read();

                id = Int32.TryParse(dr["CartID"].ToString(), out i0) ? Convert.ToInt32(dr["CartID"]) : i0;
                return id;
            }
        }

        public int AddCustomGun(CustomModel m)
        {
            var id = 0;
            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartSaleAddCustomGun");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[30];
                parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = m.Cart.TransactionId };
                parameters[1] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.Gun.ManufId };
                parameters[2] = new SqlParameter("@SubCatID", SqlDbType.Int) { Value = m.Gun.GunTypeId };
                parameters[3] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.Gun.CaliberId };
                parameters[4] = new SqlParameter("@ActionID", SqlDbType.Int) { Value = m.Gun.ActionId };
                parameters[5] = new SqlParameter("@FinishID", SqlDbType.Int) { Value = m.Gun.FinishId };
                parameters[6] = new SqlParameter("@Capacity", SqlDbType.Int) { Value = m.Gun.CapacityInt };
                parameters[7] = new SqlParameter("@ConditionID", SqlDbType.Int) { Value = m.Gun.ConditionId };
                parameters[8] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = m.Book.SupplierID };
                parameters[9] = new SqlParameter("@FFLCode", SqlDbType.Int) { Value = m.Book.AcqFflCode };
                parameters[10] = new SqlParameter("@AcqSourceID", SqlDbType.Int) { Value = m.Book.AcqTypeId };
                parameters[11] = new SqlParameter("@ItemsPerBox", SqlDbType.Int) { Value = m.Gun.ItemsPerBox };
                parameters[12] = new SqlParameter("@ShipWtLb", SqlDbType.Int) { Value = m.Gun.WeightLb };
                parameters[13] = new SqlParameter("@ShipSizeID", SqlDbType.Int) { Value = m.Gun.ShippingBoxId };
                parameters[14] = new SqlParameter("@Units", SqlDbType.Int) { Value = m.Cart.Units };
                parameters[15] = new SqlParameter("@LockMakeID", SqlDbType.Int) { Value = 0 };
                parameters[16] = new SqlParameter("@LockModelID", SqlDbType.Int) { Value = 0 };
                parameters[17] = new SqlParameter("@HasBox", SqlDbType.Bit) { Value = m.Gun.OrigBox };
                parameters[18] = new SqlParameter("@HasPpw", SqlDbType.Bit) { Value = m.Gun.OrigPaperwork };
                parameters[19] = new SqlParameter("@ModelName", SqlDbType.VarChar) { Value = m.Gun.ModelName == "" ? (object)DBNull.Value : m.Gun.ModelName };
                parameters[20] = new SqlParameter("@MfgPartNumber", SqlDbType.VarChar) { Value = m.Gun.MfgPartNumber == "" ? (object)DBNull.Value : m.Gun.MfgPartNumber };
                parameters[21] = new SqlParameter("@Email", SqlDbType.VarChar) { Value = m.Book.AcqEmail == "" ? (object)DBNull.Value : m.Book.AcqEmail };
                parameters[22] = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = m.Gun.UpcCode == "" ? (object)DBNull.Value : m.Gun.UpcCode };
                parameters[23] = new SqlParameter("@Price", SqlDbType.Decimal) { Value = m.Cart.Price };
                parameters[24] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = m.Cart.Cost };
                parameters[25] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = m.Cart.Freight };
                parameters[26] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = m.Cart.Fees };
                parameters[27] = new SqlParameter("@BarrrelDec", SqlDbType.Decimal) { Value = m.Gun.BarrelDec };
                parameters[28] = new SqlParameter("@ShipWtOz", SqlDbType.Decimal) { Value = m.Gun.WeightOz };
                parameters[29] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = m.Book.AcqDate };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return id;

                var i0 = 0;
                dr.Read();

                id = Int32.TryParse(dr["CartID"].ToString(), out i0) ? Convert.ToInt32(dr["CartID"]) : i0;
                return id;
            }
        }

        public int AddCustomAmmo(CustomModel m)
        {
            var id = 0;
            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartSaleAddCustomAmmo");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[24];
                parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = m.Cart.TransactionId };
                parameters[1] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.Ammo.AmmoManufId };
                parameters[2] = new SqlParameter("@SubCatID", SqlDbType.Int) { Value = m.Ammo.SubCategoryId };
                parameters[3] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.Ammo.CaliberId };
                parameters[4] = new SqlParameter("@ConditionID", SqlDbType.Int) { Value = m.Ammo.ConditionId };
                parameters[5] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = m.Book.SupplierID };
                parameters[6] = new SqlParameter("@FFLCode", SqlDbType.Int) { Value = m.Book.AcqFflCode };
                parameters[7] = new SqlParameter("@AcqSourceID", SqlDbType.Int) { Value = m.Book.AcqTypeId };
                parameters[8] = new SqlParameter("@BulletTypeID", SqlDbType.Int) { Value = m.Ammo.BulletTypeId };
                parameters[9] = new SqlParameter("@RoundsPerBox", SqlDbType.Int) { Value = m.Ammo.RoundsPerBox };
                parameters[10] = new SqlParameter("@BulletWeight", SqlDbType.Int) { Value = m.Ammo.GrainWeight };
                parameters[11] = new SqlParameter("@Units", SqlDbType.Int) { Value = m.Cart.Units };
                parameters[12] = new SqlParameter("@IsSlug", SqlDbType.Bit) { Value = m.Ammo.IsSlug };
                parameters[13] = new SqlParameter("@ModelName", SqlDbType.VarChar) { Value = m.Ammo.ModelName == "" ? (object)DBNull.Value : m.Ammo.ModelName };
                parameters[14] = new SqlParameter("@MfgPartNumber", SqlDbType.VarChar) { Value = m.Ammo.MfgPartNumber == "" ? (object)DBNull.Value : m.Ammo.MfgPartNumber };
                parameters[15] = new SqlParameter("@Email", SqlDbType.VarChar) { Value = m.Book.AcqEmail == "" ? (object)DBNull.Value : m.Book.AcqEmail };
                parameters[16] = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = m.Ammo.UpcCode == "" ? (object)DBNull.Value : m.Ammo.UpcCode };
                parameters[17] = new SqlParameter("@Price", SqlDbType.Decimal) { Value = m.Cart.Price };
                parameters[18] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = m.Cart.Cost };
                parameters[19] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = m.Cart.Freight };
                parameters[20] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = m.Cart.Fees };
                parameters[21] = new SqlParameter("@ChamberDec", SqlDbType.Decimal) { Value = m.Ammo.Chamber };
                parameters[22] = new SqlParameter("@ShotSizeWt", SqlDbType.Decimal) { Value = m.Ammo.ShotSizeWeight };
                parameters[23] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = m.Book.AcqDate == dt0 ? (object)DBNull.Value : m.Book.AcqDate };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return id;

                var i0 = 0;
                dr.Read();

                id = Int32.TryParse(dr["CartID"].ToString(), out i0) ? Convert.ToInt32(dr["CartID"]) : i0;
                return id;
            }
        }

        public int AddCustomItem(CustomModel m)
        {
            var id = 0;
            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartSaleAddCustomItem");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[22];
                parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = m.Cart.TransactionId };
                parameters[1] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.Merch.ManufId };
                parameters[2] = new SqlParameter("@SubCatID", SqlDbType.Int) { Value = m.Merch.SubCategoryId };
                parameters[3] = new SqlParameter("@ConditionID", SqlDbType.Int) { Value = m.Merch.ConditionId };
                parameters[4] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = m.Book.SupplierID };
                parameters[5] = new SqlParameter("@FFLCode", SqlDbType.Int) { Value = m.Book.AcqFflCode };
                parameters[6] = new SqlParameter("@AcqSourceID", SqlDbType.Int) { Value = m.Book.AcqTypeId };
                parameters[7] = new SqlParameter("@ShipSizeID", SqlDbType.Int) { Value = m.Merch.ShippingBoxId };
                parameters[8] = new SqlParameter("@ShipWtLb", SqlDbType.Int) { Value = m.Merch.ShippingLbs };
                parameters[9] = new SqlParameter("@ItemsPerBox", SqlDbType.Int) { Value = m.Merch.ItemsPerBox };
                parameters[10] = new SqlParameter("@Units", SqlDbType.Int) { Value = m.Cart.Units };
                parameters[11] = new SqlParameter("@ModelName", SqlDbType.VarChar) { Value = m.Merch.ModelName == "" ? (object)DBNull.Value : m.Merch.ModelName };
                parameters[12] = new SqlParameter("@MfgPartNumber", SqlDbType.VarChar) { Value = m.Merch.MfgPartNumber == "" ? (object)DBNull.Value : m.Merch.MfgPartNumber };
                parameters[13] = new SqlParameter("@Email", SqlDbType.VarChar) { Value = m.Book.AcqEmail == "" ? (object)DBNull.Value : m.Book.AcqEmail };
                parameters[14] = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = m.Merch.UpcCode == "" ? (object)DBNull.Value : m.Merch.UpcCode };
                parameters[15] = new SqlParameter("@Description", SqlDbType.VarChar) { Value = m.Merch.ItemDesc == "" ? (object)DBNull.Value : m.Merch.ItemDesc };
                parameters[16] = new SqlParameter("@Price", SqlDbType.Decimal) { Value = m.Cart.Price };
                parameters[17] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = m.Cart.Cost };
                parameters[18] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = m.Cart.Freight };
                parameters[19] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = m.Cart.Fees };
                parameters[20] = new SqlParameter("@ShipWtOz", SqlDbType.Decimal) { Value = m.Merch.ShippingOzs };
                parameters[21] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = m.Book.AcqDate == dt0 ? (object)DBNull.Value : m.Book.AcqDate };
 
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return id;

                var i0 = 0;
                dr.Read();

                id = Int32.TryParse(dr["CartID"].ToString(), out i0) ? Convert.ToInt32(dr["CartID"]) : i0;
                return id;
            }
        }


        public void AddGenericItem(CartModel c)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartAddGeneric");

            var param = new IDataParameter[4];
            param[0] = new SqlParameter("@TransID", DbType.Int32) { Value = c.TransactionId };
            param[1] = new SqlParameter("@IsTax", DbType.Boolean) { Value = c.IsTaxable };
            param[2] = new SqlParameter("@Price", DbType.Decimal) { Value = c.Price };
            param[3] = new SqlParameter("@ItemDesc", DbType.String) { Value = c.ItemDesc };
            DataProcs.ProcParams(EcsmsSqlConnection, proc, param);
        }


        public void UpdateItemImg(int id, int md, string imgName)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateItemImage");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@CartID", DbType.Int32) { Value = id };
            param[1] = new SqlParameter("@ImgDist", DbType.Int32) { Value = md };
            param[2] = new SqlParameter("@ImageName", DbType.String) { Value = imgName };
            DataProcs.ProcParams(EcsmsSqlConnection, proc, param);
        }



        public void AddFulfillmentSale(FulfillModel f)
        {
            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartSaleAddFulfillment");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[18];
                parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = f.TransactionId };
                parameters[1] = new SqlParameter("@FulfillTypeID", SqlDbType.Int) { Value = f.FulfillSrcId };
                parameters[2] = new SqlParameter("@RcSupplierID", SqlDbType.Int) { Value = f.RcSupplierId };
                parameters[3] = new SqlParameter("@RcAcqTypeID", SqlDbType.Int) { Value = f.RcAcqSrcId };
                parameters[4] = new SqlParameter("@RcFFLCode", SqlDbType.Int) { Value = f.RcFflCode };
                parameters[5] = new SqlParameter("@PuSupplierID", SqlDbType.Int) { Value = f.PuSupplierId };
                parameters[6] = new SqlParameter("@PuAcqTypeID", SqlDbType.Int) { Value = f.PuAcqSrcId };
                parameters[7] = new SqlParameter("@PuFFLCode", SqlDbType.Int) { Value = f.PuFflCode };
                parameters[8] = new SqlParameter("@RecoveryObjID", SqlDbType.Int) { Value = f.RecoveryObjId };
                parameters[9] = new SqlParameter("@DstEmail", SqlDbType.VarChar) { Value = f.RcEmail == "" ? (object)DBNull.Value : f.RcEmail };
                parameters[10] = new SqlParameter("@PuEmail", SqlDbType.VarChar)  { Value = f.PuEmail == "" ? (object)DBNull.Value : f.PuEmail };
                parameters[11] = new SqlParameter("@Attorney", SqlDbType.VarChar) { Value = f.AttyName == "" ? (object)DBNull.Value : f.AttyName };
                parameters[12] = new SqlParameter("@AttPhone", SqlDbType.VarChar) { Value = f.AttyPhone == "" ? (object)DBNull.Value : f.AttyPhone };
                parameters[13] = new SqlParameter("@AttEmail", SqlDbType.VarChar) { Value = f.AttyEmail == "" ? (object)DBNull.Value : f.AttyEmail };
                parameters[14] = new SqlParameter("@Officer", SqlDbType.VarChar) { Value = f.CaseOfcName == "" ? (object)DBNull.Value : f.CaseOfcName };
                parameters[15] = new SqlParameter("@OfcPhone", SqlDbType.VarChar) { Value = f.CaseOfcPhone == "" ? (object)DBNull.Value : f.CaseOfcPhone };
                parameters[16] = new SqlParameter("@OfcEmail", SqlDbType.VarChar) { Value = f.CaseOfcEmail == "" ? (object)DBNull.Value : f.CaseOfcEmail };
                parameters[17] = new SqlParameter("@Notes", SqlDbType.VarChar) { Value = f.Notes == "" ? (object)DBNull.Value : f.Notes };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
 
            }
        }

        public Order SetBaseInvoiceFees(int id)
        {
            var o = new Order();
            var i0 = 0;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetBaseInvoiceFees");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;

                dr.Read();

                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                o = GetFullInvoice(i1);

                return o;

            }
        }


        public Order SetPrintInvoice(int oid)
        {
            var o = new Order();

            var ci = new List<OrderCartItem>();
            var oa = new List<OrderAddress>();
            var ot = new List<OrderTransaction>();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetPrintInvoice");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@OrderID", SqlDbType.Int) { Value = oid };
                cmd.Parameters.Add(param);

                var i0 = 0;
                var d0 = 0.00;
                var b0 = false;
                var dt0 = DateTime.MinValue;

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;
                dr.Read();

                var i1 = oid;

                var d1 = Double.TryParse(dr["SubTotal"].ToString(), out d0) ? Convert.ToDouble(dr["SubTotal"]) : d0;
                var d2 = Double.TryParse(dr["SalesTax"].ToString(), out d0) ? Convert.ToDouble(dr["SalesTax"]) : d0;
                var d3 = Double.TryParse(dr["OrderTotal"].ToString(), out d0) ? Convert.ToDouble(dr["OrderTotal"]) : d0;
                var d4 = Double.TryParse(dr["BalancePaid"].ToString(), out d0) ? Convert.ToDouble(dr["BalancePaid"]) : d0;
                var d5 = Double.TryParse(dr["BalanceDue"].ToString(), out d0) ? Convert.ToDouble(dr["BalanceDue"]) : d0;

                var dt1 = DateTime.TryParse(dr["OrderDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["OrderDate"]) : dt0;

                var b1 = Boolean.TryParse(dr["IsQuote"].ToString(), out b0) ? Convert.ToBoolean(dr["IsQuote"]) : b0;
 
                var v1 = dr["OrderCode"].ToString();
                var v2 = dr["Header"].ToString();
                var v3 = dr["SalesRep"].ToString();
                var v4 = dr["PayMethods"].ToString();
                var v5 = dr["FulfillTypes"].ToString();
                var v6 = dr["FFLCode"].ToString();
                var v7 = dr["TermsCondTxt"].ToString();
                var v8 = dr["LiabilityTxt"].ToString();
                var v9 = dt1.ToShortDateString();

                var v10 = dr["ShopName"].ToString();
                var v11 = dr["ShopAddress"].ToString();
                var v12 = dr["ShopCity"].ToString();
                var v13 = dr["ShopState"].ToString();
                var v14 = dr["ShopZip"].ToString();
                var v15 = dr["ShopPhone"].ToString();
                var v16 = dr["ShopEmail"].ToString();

                var v17 = dr["CustOrgName"].ToString();
                var v18 = dr["CustFirst"].ToString();
                var v19 = dr["CustLast"].ToString();
                var v20 = dr["CustAddr"].ToString();
                var v21 = dr["CustCity"].ToString();
                var v22 = dr["CustState"].ToString();
                var v23 = dr["CustZip"].ToString();
                var v24 = dr["CustPhone"].ToString();
                var v25 = dr["CustEmail"].ToString();
                var v26 = dr["Notes"].ToString();

                var oas = new OrderAddress(v10, v11, v12, v13, v14, v15, v16); //shop address
                var oac = new OrderAddress(v17, v18, v19, v20, v21, v22, v23, v24, v25); //cust address

                o = new Order(i1, b1, d1, d2, d3, d4, d5, v1, v2, v3, v4, v5, v6, v7, v8, v9, v26, oas, oac);

                // SHOPPING CART
                dr.NextResult();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
 
                        var ci1 = Int32.TryParse(dr["RowID"].ToString(), out i0) ? Convert.ToInt32(dr["RowID"]) : i0;
                        var ci2 = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;
                        var ci3 = Int32.TryParse(dr["FeeID"].ToString(), out i0) ? Convert.ToInt32(dr["FeeID"]) : i0;
                        var ci4 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                        var ci5 = Int32.TryParse(dr["CategoryID"].ToString(), out i0) ? Convert.ToInt32(dr["CategoryID"]) : i0;
                        var ci6 = Int32.TryParse(dr["TransTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["TransTypeID"]) : i0;

                        var cd1 = Double.TryParse(dr["Price"].ToString(), out d0) ? Convert.ToDouble(dr["Price"]) : d0;
                        var cd2 = Double.TryParse(dr["Extension"].ToString(), out d0) ? Convert.ToDouble(dr["Extension"]) : d0;

                        var cv1 = dr["Category"].ToString();
                        var cv2 = dr["InvSrcFeeDesc"].ToString();
                        var cv3 = dr["ItemDesc"].ToString();
                        var cv4 = dr["Taxable"].ToString();

                        var cb1 = Boolean.TryParse(dr["IsSellerAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSellerAddr"]) : b0;
                        var cb2 = Boolean.TryParse(dr["IsShipAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsShipAddr"]) : b0;
                        var cb3 = Boolean.TryParse(dr["IsPickupAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsPickupAddr"]) : b0;
                        var cb4 = Boolean.TryParse(dr["IsDeliverAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDeliverAddr"]) : b0;
                        var cb5 = Boolean.TryParse(dr["IsTax"].ToString(), out b0) ? Convert.ToBoolean(dr["IsTax"]) : b0;
                        var cb6 = Boolean.TryParse(dr["IsFee"].ToString(), out b0) ? Convert.ToBoolean(dr["IsFee"]) : b0;


                        var oci = new OrderCartItem(ci1, ci4, ci2, ci3, ci5, ci6, cd1, cd2, cv1, cv2, cv3, cv4, cb1, cb2, cb3, cb4, cb5, cb6);
                        ci.Add(oci);
                    }
                }

                // Data: Address Menu
                dr.NextResult();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var adi1 = Int32.TryParse(dr["TransactionID"].ToString(), out i0) ? Convert.ToInt32(dr["TransactionID"]) : i0;
                        var adi2 = Int32.TryParse(dr["CartID"].ToString(), out i0) ? Convert.ToInt32(dr["CartID"]) : i0;
                        var adi3 = Int32.TryParse(dr["FFLCode"].ToString(), out i0) ? Convert.ToInt32(dr["FFLCode"]) : i0;

                        var adb1 = Boolean.TryParse(dr["IsSellerAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSellerAddr"]) : b0;
                        var adb2 = Boolean.TryParse(dr["IsShipAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsShipAddr"]) : b0;
                        var adb3 = Boolean.TryParse(dr["IsPickupAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsPickupAddr"]) : b0;
                        var adb4 = Boolean.TryParse(dr["IsDeliverAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDeliverAddr"]) : b0;

                        var ads1 = dr["FirstName"].ToString();
                        var ads2 = dr["LastName"].ToString();
                        var ads3 = dr["OrgName"].ToString();
                        var ads4 = dr["SupAddress"].ToString();
                        var ads5 = dr["SupCity"].ToString();
                        var ads6 = dr["SupState"].ToString();
                        var ads7 = dr["SupZipCode"].ToString();
                        var ads8 = dr["SupZipExt"].ToString();
                        var ads9 = dr["SupPhone"].ToString();
                        var ads10 = dr["SupEmail"].ToString();
                        var ads11 = dr["ItemDesc"].ToString();

                        var al = new OrderAddress(adi1, adi2, adi3, adb1, adb2, adb3, adb4, ads1, ads2, ads3, ads4, ads5, ads6, ads7, ads8, ads9, ads10, ads11);
                        oa.Add(al);
                    }
                }

                // NOW: BIND MENUS TO RESPECTIVE CART ROWS
                if (ci.Count > 0)
                {
                    foreach (var r in ci)
                    {
                        var tid = r.TransactionId;
                        var cid = r.CartItemId;
                        var gun = r.IsGunRow;
                        var amo = r.IsAmmoRow;
                        var mch = r.IsMrchRow;
                        var lok = r.LockMakeID;
                        var isi = r.InStockId;
                        var lid = r.LocationId;


                        // SHIPPING ADDRESS
                        if (r.IsShipRow == true) { r.AddressShipping = GetInvoiceAddress(r.CartItemId, oa); }

                        // DELIVERY ADDRESS
                        if (r.IsDeliverRow == true) { r.AddressDelivery = GetInvoiceAddress(r.CartItemId, oa); }

                        // PICKUP ADDRESS
                        if (r.IsPickupRow == true) { r.AddressPickup = GetInvoiceAddress(r.CartItemId, oa); }

                        // SELLER ADDRESS
                        if (r.IsSellerRow == true) { r.AddressSeller = GetInvoiceAddress(r.CartItemId, oa); }


                    }
                }

                var t = new OrderTransaction();
                t.OrderCartItems = ci;
                ot.Add(t);
                o.OrderTransactions = ot;

            }
                return o;
        }



        public Order GetFullInvoice(int invId)
        {
            var o = new Order();

            var ci = new List<OrderCartItem>();
            var oa = new List<OrderAddress>();
            var ot = new List<OrderTransaction>();
            var gl = new List<GunLock>();
            var os = new List<OrderSupplier>();
            var vs = new List<OrderSupplier>();
            var pm = new List<OrderPayment>();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetFullInvoice");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@OrderID", SqlDbType.Int) { Value = invId };
                cmd.Parameters.Add(param);

                var i0 = 0;
                var d0 = 0.00;
                var b0 = false;
                var dt0 = DateTime.MinValue;
                long i64 = 0;

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;  
                dr.Read();

                // Get Section Counts
                var ctr = Int32.TryParse(dr["CountTransactions"].ToString(), out i0) ? Convert.ToInt32(dr["CountTransactions"]) : i0;
                var ctc = Int32.TryParse(dr["CountCart"].ToString(), out i0) ? Convert.ToInt32(dr["CountCart"]) : i0;
                var cta = Int32.TryParse(dr["CountAddress"].ToString(), out i0) ? Convert.ToInt32(dr["CountAddress"]) : i0;
                var clm = Int32.TryParse(dr["CountLockModel"].ToString(), out i0) ? Convert.ToInt32(dr["CountLockModel"]) : i0;
                var cdu = Int32.TryParse(dr["CountDistUnits"].ToString(), out i0) ? Convert.ToInt32(dr["CountDistUnits"]) : i0;

                var oct = new OrderCount(ctr, ctc, cta, clm, cdu);

                var taxMenu = new List<SelectListItem>();
                var feeMenu = new List<SelectListItem>();
                var fsdMenu = new List<SelectListItem>();
                var lokMenu = new List<SelectListItem>();
                var fscMenu = new List<SelectListItem>();


                // Static: Tax Menu
                dr.NextResult();
                if (!dr.HasRows) return o;

                while (dr.Read())
                {
                    var id = dr["ID"].ToString();
                    var ds = dr["TaxStatus"].ToString();
                    taxMenu.Add(new SelectListItem { Value = id, Text = ds });
                }

                // Static: Fee Menu
                dr.NextResult();
                if (!dr.HasRows) return o;

                while (dr.Read())
                {
                    var id = dr["ID"].ToString();
                    var ds = dr["FeeDesc"].ToString();
                    feeMenu.Add(new SelectListItem { Value = id, Text = ds });
                }

                // Static: FSD Menu
                dr.NextResult();
                if (!dr.HasRows) return o;

                while (dr.Read())
                {
                    var id = dr["ID"].ToString();
                    var ds = dr["FsdOption"].ToString();
                    fsdMenu.Add(new SelectListItem { Value = id, Text = ds });
                }

                // Static: Lock Manuf Menu
                dr.NextResult();
                if (!dr.HasRows) return o;

                while (dr.Read())
                {
                    var id = dr["ID"].ToString();
                    var ds = dr["LockManuf"].ToString();
                    lokMenu.Add(new SelectListItem { Value = id, Text = ds });
                }

                // Static: FSC Compliance
                dr.NextResult();
                if (!dr.HasRows) return o;

                while (dr.Read())
                {
                    var id = dr["ID"].ToString();
                    var ds = dr["FscType"].ToString();
                    fscMenu.Add(new SelectListItem { Value = id, Text = ds });
                }

                // Data: Order Info
                dr.NextResult();
                if (dr.HasRows)
                {
                    dr.Read();

                    var oi1 = invId;
                    var oi2 = Int32.TryParse(dr["CustomerId"].ToString(), out i0) ? Convert.ToInt32(dr["CustomerId"]) : i0;
                    var oi3 = Int32.TryParse(dr["SalesRepId"].ToString(), out i0) ? Convert.ToInt32(dr["SalesRepId"]) : i0;
                    var oi4 = Int32.TryParse(dr["LocationId"].ToString(), out i0) ? Convert.ToInt32(dr["LocationId"]) : i0;
                    var oi5 = Int32.TryParse(dr["FscOptId"].ToString(), out i0) ? Convert.ToInt32(dr["FscOptId"]) : i0;
                    var oi6 = Int32.TryParse(dr["OrderTypeId"].ToString(), out i0) ? Convert.ToInt32(dr["OrderTypeId"]) : i0;
                    
                    var od1 = Double.TryParse(dr["SubTotal"].ToString(), out d0) ? Convert.ToDouble(dr["SubTotal"]) : d0;
                    var od2 = Double.TryParse(dr["SalesTax"].ToString(), out d0) ? Convert.ToDouble(dr["SalesTax"]) : d0;
                    var od3 = Double.TryParse(dr["OrderTotal"].ToString(), out d0) ? Convert.ToDouble(dr["OrderTotal"]) : d0;
                    var od4 = Double.TryParse(dr["BalancePaid"].ToString(), out d0) ? Convert.ToDouble(dr["BalancePaid"]) : d0;
                    var od5 = Double.TryParse(dr["BalanceDue"].ToString(), out d0) ? Convert.ToDouble(dr["BalanceDue"]) : d0;
                    var od6 = Double.TryParse(dr["ExciseTax"].ToString(), out d0) ? Convert.ToDouble(dr["ExciseTax"]) : d0;

                    var dt1 = DateTime.TryParse(dr["OrderDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["OrderDate"]) : dt0;
                    var dt2 = DateTime.TryParse(dr["FscExpires"].ToString(), out dt0) ? Convert.ToDateTime(dr["FscExpires"]) : dt0;

                    var sod = dt1.ToShortDateString();
                    var sed = dt2.ToShortDateString();

                    var fsc = dr["FscNumber"].ToString();

                    var snm = dr["ShopName"].ToString();
                    var sad = dr["ShopAddress"].ToString();
                    var sct = dr["ShopCity"].ToString();
                    var sst = dr["ShopState"].ToString();
                    var szc = dr["ZipCode"].ToString();
                    //var sph = dr["Phone"].ToString();
                    var sem = dr["ShopEmail"].ToString();

                    var ccn = dr["CompanyName"].ToString();
                    var cfn = dr["CustFirst"].ToString();
                    var cln = dr["CustLast"].ToString();
                    var cad = dr["CustAddress"].ToString();
                    var csu = dr["CustSuite"].ToString();
                    var cct = dr["CustCity"].ToString();
                    var cst = dr["CustState"].ToString();
                    var czc = dr["CustZipCode"].ToString();
                    var cze = dr["CustZipExt"].ToString();
                    //var cph = dr["CustPhone"].ToString();
                    var cem = dr["CustEmail"].ToString();

                    var sph = Int64.TryParse(dr["Phone"].ToString(), out i64) ? PhoneToString(Convert.ToInt64(dr["Phone"])) : string.Empty;
                    var cph = Int64.TryParse(dr["CustPhone"].ToString(), out i64) ? PhoneToString(Convert.ToInt64(dr["CustPhone"])) : string.Empty;


                    var oas = new OrderAddress(snm, sad, sct, sst, szc, sph, sem);
                    var oac = new OrderAddress(ccn, cfn, cln, cad, csu, cct, cst, czc, cze, cph, cem);

                    o = new Order(oi1, oi2, oi3, oi4, oi5, oi6, od1, od2, od3, od4, od5, od6, sod, sed, DecryptIt(fsc), oas, oac);
                }



                // Data: Service Headings
                dr.NextResult();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var tri1 = Int32.TryParse(dr["TransactionID"].ToString(), out i0) ? Convert.ToInt32(dr["TransactionID"]) : i0;
                        var tri2 = Int32.TryParse(dr["GunCount"].ToString(), out i0) ? Convert.ToInt32(dr["GunCount"]) : i0;
                        var tri3 = Int32.TryParse(dr["AmmoCount"].ToString(), out i0) ? Convert.ToInt32(dr["AmmoCount"]) : i0;
                        var tri4 = Int32.TryParse(dr["MerchCount"].ToString(), out i0) ? Convert.ToInt32(dr["MerchCount"]) : i0;
                        var tri5 = Int32.TryParse(dr["TaxStatusID"].ToString(), out i0) ? Convert.ToInt32(dr["TaxStatusID"]) : i0;
                        var tri6 = Int32.TryParse(dr["RecObjID"].ToString(), out i0) ? Convert.ToInt32(dr["RecObjID"]) : i0;
                        var tri7 = Int32.TryParse(dr["Row"].ToString(), out i0) ? Convert.ToInt32(dr["Row"]) : i0;
                        var tri8 = Int32.TryParse(dr["LocationID"].ToString(), out i0) ? Convert.ToInt32(dr["LocationID"]) : i0;
                        var tri9 = Int32.TryParse(dr["FullfillmentTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["FullfillmentTypeID"]) : i0;

                        var trd1 = Double.TryParse(dr["Shipping"].ToString(), out d0) ? Convert.ToDouble(dr["Shipping"]) : d0;
                        var trd2 = Double.TryParse(dr["Fees"].ToString(), out d0) ? Convert.ToDouble(dr["Fees"]) : d0;
                        var trd3 = Double.TryParse(dr["Parts"].ToString(), out d0) ? Convert.ToDouble(dr["Parts"]) : d0;
                        var trd4 = Double.TryParse(dr["Labor"].ToString(), out d0) ? Convert.ToDouble(dr["Labor"]) : d0;
                        var trd5 = Double.TryParse(dr["SubTotal"].ToString(), out d0) ? Convert.ToDouble(dr["SubTotal"]) : d0;
                        var trd6 = Double.TryParse(dr["SalesTax"].ToString(), out d0) ? Convert.ToDouble(dr["SalesTax"]) : d0;
                        var trd7 = Double.TryParse(dr["TransTotal"].ToString(), out d0) ? Convert.ToDouble(dr["TransTotal"]) : d0;
                        var trd8 = Double.TryParse(dr["ExciseTax"].ToString(), out d0) ? Convert.ToDouble(dr["ExciseTax"]) : d0;

                        var trb1 = Boolean.TryParse(dr["IsShip"].ToString(), out b0) ? Convert.ToBoolean(dr["IsShip"]) : b0;
                        var trb2 = Boolean.TryParse(dr["IsFees"].ToString(), out b0) ? Convert.ToBoolean(dr["IsFees"]) : b0;
                        var trb3 = Boolean.TryParse(dr["IsParts"].ToString(), out b0) ? Convert.ToBoolean(dr["IsParts"]) : b0;
                        var trb4 = Boolean.TryParse(dr["IsLabor"].ToString(), out b0) ? Convert.ToBoolean(dr["IsLabor"]) : b0;
                        var trb5 = Boolean.TryParse(dr["IsDojCflc"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDojCflc"]) : b0;

                        var trs1 = dr["Title"].ToString();
                        var trs2 = dr["Heading"].ToString();
                        var trs3 = dr["Notes"].ToString();
                        var trs4 = dr["AttorneyName"].ToString();
                        var trs5 = dr["AttorneyPhone"].ToString();
                        var trs6 = dr["AttorneyEmail"].ToString();
                        var trs7 = dr["CaseOfficer"].ToString();
                        var trs8 = dr["OfficerPhone"].ToString();
                        var trs9 = dr["OfficerEmail"].ToString();
                        var trs10 = dr["CflcNumber"].ToString();

                        var t = new OrderTransaction(tri1, tri2, tri3, tri4, tri5, tri6, tri7, tri8, tri9, trd1, trd2, trd3, trd4, trd5, trd6, trd8, trd7, trb1, trb2, trb3, trb4, trb5, trs1, trs2, trs3, trs4, trs5, trs6, trs7, trs8, trs9, trs10, feeMenu);
                        ot.Add(t);


                    }
                }



                // Data: Cart Party Starts Here
                dr.NextResult();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //DO THIS BEFORE TESTING
                        var cti1 = Int32.TryParse(dr["RowID"].ToString(), out i0) ? Convert.ToInt32(dr["RowID"]) : i0;
                        var cti2 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                        var cti3 = Int32.TryParse(dr["TransactionID"].ToString(), out i0) ? Convert.ToInt32(dr["TransactionID"]) : i0;
                        var cti4 = Int32.TryParse(dr["CategoryID"].ToString(), out i0) ? Convert.ToInt32(dr["CategoryID"]) : i0;
                        var cti5 = Int32.TryParse(dr["TransTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["TransTypeID"]) : i0;
                        var cti6 = Int32.TryParse(dr["LocationID"].ToString(), out i0) ? Convert.ToInt32(dr["LocationID"]) : i0;
                        var cti7 = Int32.TryParse(dr["SupplierID"].ToString(), out i0) ? Convert.ToInt32(dr["SupplierID"]) : i0;
                        var cti8 = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;
                        var cti9 = Int32.TryParse(dr["ItemBasisID"].ToString(), out i0) ? Convert.ToInt32(dr["ItemBasisID"]) : i0;
                        var cti10 = Int32.TryParse(dr["FsdOptionID"].ToString(), out i0) ? Convert.ToInt32(dr["FsdOptionID"]) : i0;
                        var cti11 = Int32.TryParse(dr["LockMakeID"].ToString(), out i0) ? Convert.ToInt32(dr["LockMakeID"]) : i0;
                        var cti12 = Int32.TryParse(dr["LockModelID"].ToString(), out i0) ? Convert.ToInt32(dr["LockModelID"]) : i0;
                        var cti13 = Int32.TryParse(dr["FeeID"].ToString(), out i0) ? Convert.ToInt32(dr["FeeID"]) : i0;
                        var cti14 = Int32.TryParse(dr["FullfillmentTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["FullfillmentTypeID"]) : i0;
                        var cti15 = Int32.TryParse(dr["InStockID"].ToString(), out i0) ? Convert.ToInt32(dr["InStockID"]) : i0;
                        var cti16 = Int32.TryParse(dr["DistributorID"].ToString(), out i0) ? Convert.ToInt32(dr["DistributorID"]) : i0;
                        var cti17 = Int32.TryParse(dr["TaxStatusID"].ToString(), out i0) ? Convert.ToInt32(dr["TaxStatusID"]) : i0;

                        var ctb1 = Boolean.TryParse(dr["IsTaxable"].ToString(), out b0) ? Convert.ToBoolean(dr["IsTaxable"]) : b0;
                        var ctb2 = Boolean.TryParse(dr["IsFee"].ToString(), out b0) ? Convert.ToBoolean(dr["IsFee"]) : b0;
                        var ctb3 = Boolean.TryParse(dr["IsTax"].ToString(), out b0) ? Convert.ToBoolean(dr["IsTax"]) : b0; //public bool IsTaxRow { get; set; }
                        var ctb4 = Boolean.TryParse(dr["IsSellerAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSellerAddr"]) : b0; //public bool IsSellerRow { get; set; }
                        var ctb5 = Boolean.TryParse(dr["IsShipAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsShipAddr"]) : b0;
                        var ctb6 = Boolean.TryParse(dr["IsPickupAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsPickupAddr"]) : b0;
                        var ctb7 = Boolean.TryParse(dr["IsDeliverAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDeliverAddr"]) : b0;
                        var ctb8 = Boolean.TryParse(dr["IsGunInvMenu"].ToString(), out b0) ? Convert.ToBoolean(dr["IsGunInvMenu"]) : b0;
                        var ctb19 = Boolean.TryParse(dr["IsLock"].ToString(), out b0) ? Convert.ToBoolean(dr["IsLock"]) : b0;

                        var ctb9 = cti13 == (int)OrderFees.CA_FSC_EXAM ? true : false; //public bool IsDojFscRow { get; set; }
                        var ctb10 = cti4 == (int)ItemCategories.Guns ? true : false; //public bool IsGun { get; set; }
                        var ctb11 = cti4 == (int)ItemCategories.Ammo ? true : false; //public bool IsAmmoRow { get; set; }
                        var ctb12 = cti4 == (int)ItemCategories.Merchandise ? true : false; //public bool IsMrchRow { get; set; }
                        var ctb13 = cti13 == (int)OrderFees.SHIPPING ? true : false; //public bool IsShipRow { get; set; }
                        var ctb14 = cti13 == (int)OrderFees.PICKUP ? true : false; //public bool IsPickupRow { get; set; }
                        var ctb15 = cti13 == (int)OrderFees.DELIVERY ? true : false; //public bool IsDeliverRow { get; set; }
                        var ctb16 = cti5 == (int)TransTypes.RECOVERY ? true : false; //public bool IsRecoveryRow { get; set; }
                        var ctb17 = cti5 > 102 ? true : false; //public bool IsServiceRow { get; set; }
                        var ctb18 = false; //public bool IsFsDeviceRow { get; set; }

                        if (cti14 == (int)FulfillmentTypes.FaceToFace) //public bool IsFsDeviceRow { get; set; }
                        {
                            if (cti4 == (int)ItemCategories.Guns) { if (cti5 == (int)TransTypes.SALE || cti5 == (int)TransTypes.TRANSFER) { ctb18 = true; } }
                        }

                        var ctd1 = Double.TryParse(dr["Freight"].ToString(), out d0) ? Convert.ToDouble(dr["Freight"]) : d0;
                        var ctd2 = Double.TryParse(dr["Fees"].ToString(), out d0) ? Convert.ToDouble(dr["Fees"]) : d0;
                        var ctd3 = Double.TryParse(dr["Parts"].ToString(), out d0) ? Convert.ToDouble(dr["Parts"]) : d0;
                        var ctd4 = Double.TryParse(dr["Repairs"].ToString(), out d0) ? Convert.ToDouble(dr["Repairs"]) : d0;
                        var ctd5 = Double.TryParse(dr["Price"].ToString(), out d0) ? Convert.ToDouble(dr["Price"]) : d0;
                        var ctd6 = Double.TryParse(dr["TransferCost"].ToString(), out d0) ? Convert.ToDouble(dr["TransferCost"]) : d0;
                        var ctd7 = Double.TryParse(dr["TransferTaxPaid"].ToString(), out d0) ? Convert.ToDouble(dr["TransferTaxPaid"]) : d0;
                        var ctd8 = Double.TryParse(dr["TransferTaxDue"].ToString(), out d0) ? Convert.ToDouble(dr["TransferTaxDue"]) : d0;
                        var ctd9 = Double.TryParse(dr["TransferTaxAmount"].ToString(), out d0) ? Convert.ToDouble(dr["TransferTaxAmount"]) : d0;
                        var ctd10 = Double.TryParse(dr["Extension"].ToString(), out d0) ? Convert.ToDouble(dr["Extension"]) : d0;
                        var ctd11 = Double.TryParse(dr["TaxRate"].ToString(), out d0) ? Convert.ToDouble(dr["TaxRate"]) : d0;
                        var ctd12 = Double.TryParse(dr["TaxDue"].ToString(), out d0) ? Convert.ToDouble(dr["TaxDue"]) : d0;
                        var ctd13 = Double.TryParse(dr["ExciseTaxRate"].ToString(), out d0) ? Convert.ToDouble(dr["ExciseTaxRate"]) : d0;
                        var ctd14 = Double.TryParse(dr["ExciseTaxDue"].ToString(), out d0) ? Convert.ToDouble(dr["ExciseTaxDue"]) : d0;

                        var cts1 = dr["Category"].ToString();
                        var cts2 = dr["Serial"].ToString();
                        var cts3 = dr["InvSrcFeeDesc"].ToString();
                        var cts4 = dr["ItemTitle"].ToString();
                        var cts5 = dr["ItemDesc"].ToString();

                        var oci = new OrderCartItem(cti1, cti2, cti3, cti4, cti5, cti6, cti7, cti8, cti9, cti10, cti11, cti12, cti13, cti14, cti15, cti16, cti17,
                                                    ctd1, ctd2, ctd3, ctd4, ctd5, ctd6, ctd7, ctd8, ctd9, ctd11, ctd12, ctd13, ctd14, ctd10, ctb1, ctb2, ctb3, ctb4, ctb5, ctb6, ctb7, 
                                                    ctb8, ctb9, ctb10, ctb11, ctb12, ctb13, ctb14, ctb15, ctb16, ctb17, ctb18, ctb19, cts1, cts2, cts3, cts4, cts5);

                        ci.Add(oci);
                    }
                }

                // Data: Address Menu
                dr.NextResult();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var adi1 = Int32.TryParse(dr["TransactionID"].ToString(), out i0) ? Convert.ToInt32(dr["TransactionID"]) : i0;
                        var adi2 = Int32.TryParse(dr["CartID"].ToString(), out i0) ? Convert.ToInt32(dr["CartID"]) : i0;
                        var adi3 = Int32.TryParse(dr["FFLCode"].ToString(), out i0) ? Convert.ToInt32(dr["FFLCode"]) : i0;

                        var adb1 = Boolean.TryParse(dr["IsSellerAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSellerAddr"]) : b0;
                        var adb2 = Boolean.TryParse(dr["IsShipAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsShipAddr"]) : b0;
                        var adb3 = Boolean.TryParse(dr["IsPickupAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsPickupAddr"]) : b0;  
                        var adb4 = Boolean.TryParse(dr["IsDeliverAddr"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDeliverAddr"]) : b0;

                        var ads1 = dr["FirstName"].ToString();
                        var ads2 = dr["LastName"].ToString();
                        var ads3 = dr["OrgName"].ToString();
                        var ads4 = dr["SupAddress"].ToString();
                        var ads5 = dr["SupCity"].ToString();
                        var ads6 = dr["SupState"].ToString();
                        var ads7 = dr["SupZipCode"].ToString();
                        var ads8 = dr["SupZipExt"].ToString();
                        var ads9 = dr["SupPhone"].ToString();
                        var ads10 = dr["SupEmail"].ToString();
                        var ads11 = dr["ItemDesc"].ToString();

                        var al = new OrderAddress(adi1, adi2, adi3, adb1, adb2, adb3, adb4, ads1, ads2, ads3, ads4, ads5, ads6, ads7, ads8, ads9, ads10, ads11);
                        oa.Add(al);
                    }
                }

                // Data: Gun Lock Models
                dr.NextResult();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var gli1 = Int32.TryParse(dr["LockManufId"].ToString(), out i0) ? Convert.ToInt32(dr["LockManufId"]) : i0;
                        var gli2 = Int32.TryParse(dr["LockModelId"].ToString(), out i0) ? Convert.ToInt32(dr["LockModelId"]) : i0;

                        var gls1 = dr["LockModel"].ToString();

                        var l = new GunLock(gli1, gli2, gls1);
                        gl.Add(l);
                    }
                }

                // Data: Gun Distributor List
                dr.NextResult();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var osi1 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                        var osi2 = Int32.TryParse(dr["TransactionID"].ToString(), out i0) ? Convert.ToInt32(dr["TransactionID"]) : i0;
                        var osi3 = Int32.TryParse(dr["CartID"].ToString(), out i0) ? Convert.ToInt32(dr["CartID"]) : i0;
                        var osi4 = Int32.TryParse(dr["DistributorID"].ToString(), out i0) ? Convert.ToInt32(dr["DistributorID"]) : i0;

                        var oss1 = dr["Distributor"].ToString();

                        var s = new OrderSupplier(osi1, osi2, osi3, osi4, oss1);
                        os.Add(s);
                    }
                }


                // Data: InStock Inventory List
                dr.NextResult();
                if (dr.HasRows) 
                {
                    while (dr.Read())
                    {
                        var ivi1 = Int32.TryParse(dr["ItemBasisID"].ToString(), out i0) ? Convert.ToInt32(dr["ItemBasisID"]) : i0;
                        var ivi2 = Int32.TryParse(dr["InStockID"].ToString(), out i0) ? Convert.ToInt32(dr["InStockID"]) : i0;
                        var ivi3 = Int32.TryParse(dr["CartID"].ToString(), out i0) ? Convert.ToInt32(dr["CartID"]) : i0;  
                        var ivi4 = Int32.TryParse(dr["TransactionID"].ToString(), out i0) ? Convert.ToInt32(dr["TransactionID"]) : i0;
                        var ivi5 = Int32.TryParse(dr["LocationID"].ToString(), out i0) ? Convert.ToInt32(dr["LocationID"]) : i0;
                        var ivi6 = Int32.TryParse(dr["UnitsCAL"].ToString(), out i0) ? Convert.ToInt32(dr["UnitsCAL"]) : i0;
                        var ivi7 = Int32.TryParse(dr["UnitsWYO"].ToString(), out i0) ? Convert.ToInt32(dr["UnitsWYO"]) : i0;

                        var ivs1 = dr["TransID"].ToString();
                        var ivs2 = dr["SerialNumber"].ToString();

                        var ivb1 = Boolean.TryParse(dr["IsGun"].ToString(), out b0) ? Convert.ToBoolean(dr["IsGun"]) : b0;

                        var s = new OrderSupplier(ivi1, ivi2, ivi3, ivi4, ivi5, ivi6, ivi7, ivs1, ivs2, ivb1);
                        vs.Add(s);
                    }
                }

                // Data: Customer Payments Applied
                dr.NextResult();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var i1 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;

                        var d1 = Double.TryParse(dr["BeginBalance"].ToString(), out d0) ? Convert.ToDouble(dr["BeginBalance"]) : d0;
                        var d2 = Double.TryParse(dr["AmountPaid"].ToString(), out d0) ? Convert.ToDouble(dr["AmountPaid"]) : d0;
                        var d3 = Double.TryParse(dr["BalanceDue"].ToString(), out d0) ? Convert.ToDouble(dr["BalanceDue"]) : d0;

                        var dt1 = DateTime.TryParse(dr["PaymentDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["PaymentDate"]) : dt0;

                        var v1 = dr["PymtDesc"].ToString();
                        var v2 = dt1.ToShortDateString();

                        var p = new OrderPayment(i1, d1, d2, d3, v1, v2);
                        pm.Add(p);
                    }
                }


                // NOW: BIND MENUS TO RESPECTIVE CART ROWS
                if (ci.Count > 0)
                {
                    foreach(var r in ci)
                    {
                        var tid = r.TransactionId;
                        var cid = r.CartItemId;
                        var gun = r.IsGunRow;
                        var amo = r.IsAmmoRow;
                        var mch = r.IsMrchRow;
                        var lok = r.LockMakeID;
                        var isi = r.InStockId;
                        var lid = r.LocationId;

                        // Conditions
                        var cnd1 = r.IsInvMenuRow;
                        var cnd2 = vs.Count > 0 && gun;
                        var cnd3 = vs.Count > 0 && amo && r.TransTypeId == (int)TransTypes.SALE;
                        var cnd4 = vs.Count > 0 && mch && r.TransTypeId == (int)TransTypes.SALE;


                        if (cnd1) // GUN DIST MENU
                        {
                            if(os.Count > 0)
                            {
                               var disMenu = new List<SelectListItem>();

                                foreach(var i in os)
                                {
                                    var tv = i.TransactionId;
                                    var cv = i.CartId;

                                    if(tid != tv){ continue; }
                                    if(cv == cid)
                                    {
                                        var did = i.DistributorId;
                                        var dis = i.Distributor;
                                        disMenu.Add(new SelectListItem { Value = did.ToString(), Text = dis });
                                    }
                                }

                                r.MenuSupplier = disMenu;
                            }

                            // INSTOCK GUN INV MENU
                            if (cnd2)
                            {
                                var invMenu = new List<SelectListItem>();

                                foreach (var i in vs)
                                {
                                    var stk = i.InStockId;
                                    var loc = i.LocationId;

                                    if (isi != stk) { continue; }
                                    if (lid == loc)
                                    {
                                        var ibi = i.ItemBasisId;
                                        var sku = i.TransSku;
                                        var ser = i.SerialNumber;
                                        var ign = i.IsGun;

                                        var txt = ign ? sku + " SER#:" + ser : sku;
                                        invMenu.Add(new SelectListItem { Value = ibi.ToString(), Text = txt });
                                    }
                                }

                                r.MenuInventoryItem = invMenu;
                            }

                        }

                        // INSTOCK AMMO/MERCH INV MENU
                        if (cnd3||cnd4)
                        {
                            var amoMenu = new List<SelectListItem>();

                            foreach (var i in vs)
                            {
                                var stk = i.InStockId;
                                var loc = i.LocationId;

                                if (isi != stk) { continue; }

                                var uca = i.UnitsCal;
                                var uwy = i.UnitsWyo;
                                var ibi = i.ItemBasisId;

                                var txt = loc == 1 ? "CALIFORNIA: " + uca.ToString() : "WYOMING: " + uwy.ToString();
                                txt += " UNITS";
                                amoMenu.Add(new SelectListItem { Value = ibi.ToString(), Text = txt });
                            }

                            r.MenuInventoryItem = amoMenu;
                        }

                        //CA FSD - F2F ONLY
                        if(r.IsFsDeviceRow)
                        {
                            r.MenuFsdOptions = fsdMenu;
                            r.MenuLockMakes = lokMenu;

                            if(gl.Count > 0)
                            {
                                var lmdMenu = new List<SelectListItem>();
                                foreach(var m in gl)
                                {
                                    var lm = m.LockManufId;
                                    if(lok == lm)
                                    {   
                                        var id = m.LockModelId;
                                        var md = m.LockModel;
                                        lmdMenu.Add(new SelectListItem { Value = id.ToString(), Text = md });
                                    }
                                }
                                r.MenuLockModels = lmdMenu;
                            }
                        }


                        // GUN RECOVERY
                        if (r.IsRecoveryRow)
                        {
                            foreach (var rr in ot)
                            {
                                if (rr.TransactionId == r.TransactionId)
                                {
                                    r.AttorneyName = rr.AttorneyName;
                                    r.AttorneyEmail = rr.AttorneyEmail;
                                    r.AttorneyPhone = rr.AttorneyPhone;
                                    r.RecoveryObjective = Enum.GetName(typeof(GunRecoveryTypes), rr.RecoveryObdId); 
                                }
                            }

                            if (r.FeeID == (int)OrderFees.DOC_PROCESSING)
                            {
                                 
                            }

                        }

                        // SHIPPING ADDRESS
                        if (r.FeeID == (int)OrderFees.SHIPPING) { r.AddressShipping = GetInvoiceAddress(r.CartItemId, oa); }

                        // DELIVERY ADDRESS
                        if (r.FeeID == (int)OrderFees.DELIVERY) { r.AddressDelivery = GetInvoiceAddress(r.CartItemId, oa); }

                        // PICKUP ADDRESS
                        if (r.FeeID == (int)OrderFees.PICKUP) { r.AddressPickup = GetInvoiceAddress(r.CartItemId, oa); }

                        // SELLER ADDRESS
                        if (r.IsSellerRow == true) { r.AddressSeller = GetInvoiceAddress(r.CartItemId, oa); }

                        // TAX MENU
                        if (r.CategoryId > 0) { r.MenuTaxOptions = taxMenu; }
                   }
                }


                // NOW: BIND CART ITEMS TO RESPECTIVE TRANSACTIONS
                foreach (var t in ot)
                {
                    var cil = new List<OrderCartItem>();

                    foreach (var i in ci)
                    {
                        if (i.TransactionId == t.TransactionId)
                        {
                            cil.Add(i);
                        }
                    }

                    t.OrderCartItems = cil;
                }

                var xp = new Payment();
                xp.OrderPayments = pm;
                xp.AmountPaid = o.BalancePaid;
                xp.EndingBalance = o.BalanceDue;

                o.OrderTransactions = ot;
                o.Payments = xp;
                o.SectionCount = oct;

                return o;
            }
        }

        public Payment PostOrderPayment(OrderPayment o)
        {
            var p = new Payment();
            var op = new List<OrderPayment>();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcOrderInsertPayment");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[9];
                parameters[0] = new SqlParameter("@OrderID", SqlDbType.Int) { Value = o.OrderId };
                parameters[1] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = o.CustomerId };
                parameters[2] = new SqlParameter("@PaymentMethodID", SqlDbType.Int) { Value = o.PaymentTypeId };
                parameters[3] = new SqlParameter("@CardLastFour", SqlDbType.Int) { Value = o.CardLastFour };
                parameters[4] = new SqlParameter("@AuthCode", SqlDbType.VarChar) { Value = o.AuthCode };
                parameters[5] = new SqlParameter("@CheckNumber", SqlDbType.VarChar) { Value = o.CheckNumber };
                parameters[6] = new SqlParameter("@BeginBalance", SqlDbType.Decimal) { Value = o.BeginBalance };
                parameters[7] = new SqlParameter("@AmountPaid", SqlDbType.Decimal) { Value = o.AmountPaid };
                parameters[8] = new SqlParameter("@PaymentDate", SqlDbType.DateTime) { Value = o.PaymentDate };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return p;

                dr.Read();

                var i0 = 0;
                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                p = GetOrderPayments(i1);

                return p;

            }
        }


        public Payment DeletePayment(int id, int oid)
        {
            var p = new Payment();
            var i0 = 0;
            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcOrderPaymentDelete");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param);
                cmd.ExecuteReader();

                p = GetOrderPayments(oid);

                return p;
            }
        }


        public Payment GetOrderPayments(int oid)
        {
            var p = new Payment();
            var op = new List<OrderPayment>();
            var i0 = 0;
            var d0 = 0.00;
            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcOrderPaymentsView");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@OrderID", SqlDbType.Int) { Value = oid };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return p;

                while (dr.Read())
                {
                    var i1 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;

                    var d1 = Double.TryParse(dr["BeginBalance"].ToString(), out d0) ? Convert.ToDouble(dr["BeginBalance"]) : d0;
                    var d2 = Double.TryParse(dr["AmountPaid"].ToString(), out d0) ? Convert.ToDouble(dr["AmountPaid"]) : d0;
                    var d3 = Double.TryParse(dr["BalanceDue"].ToString(), out d0) ? Convert.ToDouble(dr["BalanceDue"]) : d0;

                    var dt1 = DateTime.TryParse(dr["PaymentDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["PaymentDate"]) : dt0;

                    var v1 = dr["PymtDesc"].ToString();
                    var v2 = dt1.ToShortDateString();

                    var x = new OrderPayment(i1, d1, d2, d3, v1, v2);
                    op.Add(x);
                }

                p.OrderPayments = op;

                dr.NextResult();
                if (dr.HasRows)
                {
                    dr.Read();

                    var d4 = Double.TryParse(dr["BalancePaid"].ToString(), out d0) ? Convert.ToDouble(dr["BalancePaid"]) : d0;
                    var d5 = Double.TryParse(dr["BalanceDue"].ToString(), out d0) ? Convert.ToDouble(dr["BalanceDue"]) : d0;

                    p.AmountPaid = d4;
                    p.EndingBalance = d5;

                }

                return p;

            }
        }

        public OrderAddress GetInvoiceAddress(int cid, List<OrderAddress> oa)
        {
            var z = new OrderAddress();

            foreach (var a in oa)
            {
                if (cid == a.CartId)
                {
                    var adOrg = a.OrgName;
                    var adFnm = a.FirstName;
                    var adLnm = a.LastName;
                    var adAdr = a.Address;
                    var adCty = a.City;
                    var adSta = a.StateName;
                    var adZip = a.ZipCode;
                    var adExt = a.ZipExt;
                    var adEml = a.EmailAddress;
                    var adPhn = a.Phone;
                    var adIds = a.ItemDesc;
                    var adFfl = a.FflCode.ToString();

                    var adNfl = string.Empty;

                    if (a.FflCode > 0)
                    {
                        var f1 = adFfl.Substring(0, 1);
                        var f2 = adFfl.Substring(1, 2);
                        var f3 = adFfl.Substring(3, 5);
                        adNfl = string.Format("{0}-{1}-{2}", f1, f2, f3);
                    }

                    var fnm = adFnm + " " + adLnm;

                    var adNor = fnm.Length > 0 ? fnm + " - " + adOrg : adOrg;

                    z = new OrderAddress(adIds, adNor, adOrg, adFnm, adLnm, adAdr, adCty, adSta, adZip, adExt, adEml, adPhn, adNfl);
                    break;
                }
            }
            return z;
        }


        //public List<SelectListItem> GenOrderMenus(bool c, List<OrderSupplier> l, int s, int p2, int p3)
        //{
        //    var list = new List<SelectListItem>();

        //    if (c)
        //    {
        //        if (l.Count > 0)
        //        {
        //            var a = 0;
        //            var b = 0;

        //            var uca = 0;
        //            var uwy = 0;
        //            var ibi = 0;
        //            var did = 0;
        //            var tid = 0;
        //            var cid = 0;
        //            var sku = string.Empty;
        //            var ser = string.Empty;
        //            var dis = string.Empty;
        //            var ign = false;



        //            foreach (var i in l)
        //            {
        //                switch (s)
        //                {
        //                    case 1:
        //                        tid = i.TransactionId;
        //                        cid = i.CartId;
        //                        did = i.DistributorId;
        //                        dis = i.Distributor;
        //                        break;
        //                    case 2:
        //                    case 3:
        //                    case 4:
        //                        a = i.InStockId;
        //                        b = i.LocationId;
        //                        break;
        //                }

        //                if (p2 != a) { continue; }
        //                if (p3 != b) { continue; }
        //            }
        //        }
        //    }

        //    return list;
        //}

        public List<SelectListItem> GetCustFflTransfers(int cat, int cus, bool ppt)
        {
            var list = new List<SelectListItem>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustGetFFLTransfers");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[3];
                parameters[0] = new SqlParameter("@CategoryID", SqlDbType.Int) { Value = cat };
                parameters[1] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = cus };
                parameters[2] = new SqlParameter("@IsPPT", SqlDbType.Int) { Value = ppt };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return list;

                while (dr.Read())
                {
                    var id = dr["ID"].ToString();
                    var ds = dr["ItemDesc"].ToString();
 
                    list.Add(new SelectListItem { Value = id, Text = ds });
                }

                return list;

            }
        }


        public List<SelectListItem> GetCustPptItems(int cat, int cus, int sup)
        {
            var list = new List<SelectListItem>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerGetPPTItems");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[3];
                parameters[0] = new SqlParameter("@CategoryID", SqlDbType.Int) { Value = cat };
                parameters[1] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = cus };
                parameters[2] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = sup };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return list;

                while (dr.Read())
                {
                    var id = dr["ID"].ToString();
                    var ds = dr["ItemDesc"].ToString();

                    list.Add(new SelectListItem { Value = id, Text = ds });
                }

                return list;

            }
        }

        public void AddSaleCartMenu(List<CartModel> cm)
        {
            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartSaleAddSupplierMenu");
                conn.Open();

                var trans = conn.BeginTransaction();
                var cmd = new SqlCommand(proc, conn, trans) { CommandType = CommandType.StoredProcedure };
 
                cmd.Parameters.AddWithValue("@TransID", DbType.Int32);
                cmd.Parameters.AddWithValue("@CartID", DbType.Int32);
                cmd.Parameters.AddWithValue("@SupplierID", DbType.Int32);
                cmd.Parameters.AddWithValue("@InStockID", DbType.Int32);
                cmd.Parameters.AddWithValue("@MasterID", DbType.Int32);
                cmd.Parameters.AddWithValue("@Units", DbType.Int32);
                cmd.Parameters.AddWithValue("@Cost", DbType.Decimal);

                foreach (var i in cm)
                {
                    cmd.Parameters[0].Value = i.TransactionId;
                    cmd.Parameters[1].Value = i.CartId;
                    cmd.Parameters[2].Value = i.SupplierId;
                    cmd.Parameters[3].Value = i.InStockId;
                    cmd.Parameters[4].Value = i.MasterId;
                    cmd.Parameters[5].Value = i.Units;
                    cmd.Parameters[6].Value = i.Cost;

                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
                conn.Close();
            }
        }


        public Order AddSaleCartFee(OrderCartItem i)
        {
            var o = new Order();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAddInvoiceFee");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[5];
                parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = i.TransactionId };
                parameters[1] = new SqlParameter("@Units", SqlDbType.Int) { Value = i.Units };
                parameters[2] = new SqlParameter("@FeeID", SqlDbType.Int) { Value = i.FeeID };
                parameters[3] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = i.Price };
                parameters[4] = new SqlParameter("@ItemDesc", SqlDbType.VarChar) { Value = i.ItemDesc };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;

                dr.Read();

                var i0 = 0;
                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                o = GetFullInvoice(i1);

                return o;

            }
        }

        public Order DeleteCartFee(int id)
        {
            var o = new Order();
            var i0 = 0;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDeleteOrderFee");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@CartItemID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;

                dr.Read();

                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                o = GetFullInvoice(i1);

                return o;

            }
        }

        public Order SetTaxStatus(int cid, int tsi)
        {
            var o = new Order();
            var i0 = 0;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetTaxStatus");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CartItemID", SqlDbType.Int) { Value = cid };
                parameters[1] = new SqlParameter("@TaxStatusID", SqlDbType.Int) { Value = tsi };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;

                dr.Read();

                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                o = GetFullInvoice(i1);

                return o;

            }
        }

        public Order SetCartFsd(int cid, int fsd)
        {
            var o = new Order();
            var i0 = 0;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetFsdOptionCart");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CartItemID", SqlDbType.Int) { Value = cid };
                parameters[1] = new SqlParameter("@FsdOptionID", SqlDbType.Int) { Value = fsd };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;

                dr.Read();

                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                o = GetFullInvoice(i1);

                return o;

            }
        }

        public Order SetCartLockMake(int cid, int lmk)
        {
            var o = new Order();
            var i0 = 0;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetLockMakeCart");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CartItemID", SqlDbType.Int) { Value = cid };
                parameters[1] = new SqlParameter("@LockMakeID", SqlDbType.Int) { Value = lmk };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;

                dr.Read();

                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                o = GetFullInvoice(i1);

                return o;

            }
        }

        public Order SetCartLockModel(int cid, int lmd)
        {
            var o = new Order();
            var i0 = 0;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetLockModelCart");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CartItemID", SqlDbType.Int) { Value = cid };
                parameters[1] = new SqlParameter("@LockModelID", SqlDbType.Int) { Value = lmd };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;

                dr.Read();

                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                o = GetFullInvoice(i1);

                return o;

            }
        }



        public Order SetItemSupplier(int cid, int sid)
        {
            var o = new Order();
            var i0 = 0;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetItemSupplier");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CartItemID", SqlDbType.Int) { Value = cid };
                parameters[1] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = sid };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;

                dr.Read();

                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                o = GetFullInvoice(i1);

                return o;

            }
        }

        public Order SetInventoryMenuItem(int cid, int mid)
        {
            var o = new Order();
            var i0 = 0;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetInventoryItem");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CartItemID", SqlDbType.Int) { Value = cid };
                parameters[1] = new SqlParameter("@ItemBasisID", SqlDbType.Int) { Value = mid };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;

                dr.Read();

                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                o = GetFullInvoice(i1);

                return o;

            }
        }




        public Order SetItemUnits(int cid, int unt)
        {
            var o = new Order();
            var i0 = 0;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetItemUnits");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CartItemID", SqlDbType.Int) { Value = cid };
                parameters[1] = new SqlParameter("@Units", SqlDbType.Int) { Value = unt };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;

                dr.Read();

                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                o = GetFullInvoice(i1);

                return o;

            }
        }

        public Order SetItemPrice(int cid, double prc)
        {
            var o = new Order();
            var i0 = 0;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetItemPrice");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CartItemID", SqlDbType.Int) { Value = cid };
                parameters[1] = new SqlParameter("@Price", SqlDbType.Decimal) { Value = prc };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;

                dr.Read();

                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                o = GetFullInvoice(i1);

                return o;

            }
        }

        public Order SetItemTaxRate(int cid, double rat)
        {
            var o = new Order();
            var i0 = 0;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetItemTaxRate");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CartItemID", SqlDbType.Int) { Value = cid };
                parameters[1] = new SqlParameter("@TaxRate", SqlDbType.Decimal) { Value = rat };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return o;

                dr.Read();

                var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                o = GetFullInvoice(i1);

                return o;

            }
        }





        public Order DeleteInvoiceTransaction(int oid, int tid)
        {
            var o = new Order();
 
            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDeleteTransaction");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@TransID", SqlDbType.Int) { Value = tid };
                cmd.Parameters.Add(param);
                cmd.ExecuteReader();

                o = GetFullInvoice(oid);

                return o;
            }
        }


        public List<SelectListItem> GetPptSuppliers(int cat, int cus)
        {
            var list = new List<SelectListItem>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetPptSuppliers");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CategoryID", SqlDbType.Int) { Value = cat };
                parameters[1] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = cus };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return list;

                while (dr.Read())
                {
                    var id = dr["ID"].ToString();
                    var ds = dr["Supplier"].ToString();

                    list.Add(new SelectListItem { Value = id, Text = ds });
                }

                return list;

            }
        }

        public double GetFeeMenuCost(int id)
        {
            double d0 = 0.00;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetFeeMenuPrice");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return d0;

                dr.Read();

                var d1 = Double.TryParse(dr["UnitCharge"].ToString(), out d0) ? Convert.ToDouble(dr["UnitCharge"]) : d0;

                return d1;

            }
        }

        public List<Order> GetOrdersList()
        {
            var ol = new List<Order>();
            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetAllOrders");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return ol;

                var i0 = 0;
                double d0 = 0.00;

                while (dr.Read())
                {
                    var i1 = Int32.TryParse(dr["OrderID"].ToString(), out i0) ? Convert.ToInt32(dr["OrderID"]) : i0;

                    var d1 = Double.TryParse(dr["OrderTotal"].ToString(), out d0) ? Convert.ToDouble(dr["OrderTotal"]) : d0;
                    var d2 = Double.TryParse(dr["BalancePaid"].ToString(), out d0) ? Convert.ToDouble(dr["BalancePaid"]) : d0;
                    var d3 = Double.TryParse(dr["BalanceDue"].ToString(), out d0) ? Convert.ToDouble(dr["BalanceDue"]) : d0;

                    var dt1 = DateTime.TryParse(dr["OrderDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["OrderDate"]) : dt0;

                    var v1 = dr["OrderCode"].ToString();
                    var v2 = dt1.ToShortDateString();
                    var v3 = dr["Location"].ToString();
                    var v4 = dr["CustName"].ToString();
                    var v5 = dr["CustPhone"].ToString();

                    var o = new Order(i1, d1, d2, d3, v1, v2, v3, v4, v5);
                    ol.Add(o);

                }

                return ol;
            }
        }

        

    }
}