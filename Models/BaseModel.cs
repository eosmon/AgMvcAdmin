using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AgMvcAdmin.Common;
using WebBase.Configuration;
using WebBase.Security;

namespace AgMvcAdmin.Models
{
    public class BaseModel : WebBase.Configuration.BaseObjects.ConfiguredPageBase
    {
        #region Database Connection Helpers

        public string WebSqlConnection
        {
            get { return GetConnectionString(ConfigurationNames.Application, ConnectionNames.WebDbSql, SecurityKey, SecurityVector, SecurityProvider); }
        }

        public string AdminSqlConnection
        {
            get { return GetConnectionString(ConfigurationNames.Application, ConnectionNames.AdminSql, SecurityKey, SecurityVector, SecurityProvider); }
        }

        public string ComplSqlConnection
        {
            get { return GetConnectionString(ConfigurationNames.Application, ConnectionNames.ComplSql, SecurityKey, SecurityVector, SecurityProvider); }
        }

        public string EcsmsSqlConnection
        {
            get { return GetConnectionString(ConfigurationNames.Application, ConnectionNames.EcsmsSql, SecurityKey, SecurityVector, SecurityProvider); }
        }

        public string BotDbSqlConnection
        {
            get { return GetConnectionString(ConfigurationNames.Application, ConnectionNames.BotDbSql, SecurityKey, SecurityVector, SecurityProvider); }
        }

        public string GunDbSqlConnection
        {
            get { return GetConnectionString(ConfigurationNames.Application, ConnectionNames.GunDbSql, SecurityKey, SecurityVector, SecurityProvider); }
        }

        public string SecurityKey
        {
            get { return GetPropertyValue("Crypto", "SecurityKey"); }
        }

        public string SecurityVector
        {
            get { return GetPropertyValue("Crypto", "SecurityVector"); }
        }

        public CryptoProviders SecurityProvider
        {
            get { return EncryptionTool.GetProviderEnum(GetPropertyValue("Crypto", "SecurityProvider")); }
        }


        public string EncryptIt(string name)
        {
            return EncryptionTool.Encrypt(name, SecurityKey, SecurityVector, SecurityProvider);
        }

        public string DecryptIt(string name)
        {
            return EncryptionTool.Decrypt(name, SecurityKey, SecurityVector, SecurityProvider);
        }

        #endregion

        protected List<SelectListItem> MeasureInches(int limit)
        {
            var bli = new List<SelectListItem>();

            for (var i = 0; i < limit; i++)
            {
                bli.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            return bli;
        }

        protected List<SelectListItem> MeasureDecimal()
        {
            var dec = new List<SelectListItem>();
            dec.Add(new SelectListItem { Value = "0.000", Text = "0\"" });
            dec.Add(new SelectListItem { Value = "0.125", Text = "1/8\"" });
            dec.Add(new SelectListItem { Value = "0.250", Text = "1/4\"" });
            dec.Add(new SelectListItem { Value = "0.375", Text = "3/8\"" });
            dec.Add(new SelectListItem { Value = "0.500", Text = "1/2\"" });
            dec.Add(new SelectListItem { Value = "0.625", Text = "5/8\"" });
            dec.Add(new SelectListItem { Value = "0.750", Text = "3/4\"" });
            dec.Add(new SelectListItem { Value = "0.875", Text = "7/8\"" });
            return dec;
        }

        protected List<SelectListItem> WeightDecimal(double min, double max)
        {
            var dec = new List<SelectListItem>();
            for (double i = min; i < max; i += .01)
            {
                var v = String.Format("{0:0.00}", i);
                var t = String.Format("{0:#.00}", i);
                dec.Add(new SelectListItem { Value = v, Text = t });
            }
            return dec;
        }

        public string GetHostWeb()
        {
            var x0 = 0;
            var b0 = false;
            var s = string.Empty;

            var l = ConfigurationHelper.GetPropertyValue("application", "SSL");
            var m = ConfigurationHelper.GetPropertyValue("application", "WebBox");
            var h = Int32.TryParse(m, out x0) ? Convert.ToInt32(m) : 3;
            var ssl = Boolean.TryParse(l, out b0) ? Convert.ToBoolean(b0) : b0;
            s = ssl ? "https://" : "http://";

            switch ((HostUrl)h)
            {
                case HostUrl.LocalWeb:
                    s += "localhost:8002";
                    break;
                case HostUrl.Production:
                    s += "www.allguns.com";
                    break;
            }

            return s;
        }


        public string GetHostUrl()
        {
            var x0 = 0;
            var b0 = false;
            var s = string.Empty;

            var l = ConfigurationHelper.GetPropertyValue("application", "SSL");
            var m = ConfigurationHelper.GetPropertyValue("application", "Box");
            var h = Int32.TryParse(m, out x0) ? Convert.ToInt32(m) : 3;
            var ssl = Boolean.TryParse(l, out b0) ? Convert.ToBoolean(b0) : false;
            s = ssl ? "https://" : "http://";

            switch ((HostUrl)h)
            {
                case HostUrl.Local:
                    s += "localhost:8003";
                    break;
                case HostUrl.Beta:
                    s += "beta.allguns.com";
                    break;
                case HostUrl.Production:
                    s += "www.allguns.com";
                    break;
            }

            return s;
        }


        public string CookImgUrl(int imgDist, string imgName)
        {
            var retStr = string.Empty;
            var name = Enum.GetName(typeof(AppBase.Distributors), imgDist);

            retStr = string.Format("{0}/GunImages/{1}/L/{2}", GetHostUrl(), name, imgName);
            return retStr;
        }

        public string CookBaseUrl(string hostUrl, int imgDist, string imgName)
        {
            var retStr = string.Empty;
            var name = Enum.GetName(typeof(AppBase.Distributors), imgDist);

            retStr = string.Format("{0}/GunImages/{1}/L/{2}", hostUrl, name, imgName);
            retStr += "?" + DateTime.Now.Ticks;
            return retStr;
        }

        public string CookBaseUrl(string hostUrl, string dirName, string imgName)
        {
            var retStr = string.Empty;

            retStr = string.Format("{0}/SiteImg/{1}/{2}", hostUrl, dirName, imgName);
            retStr += "?" + DateTime.Now.Ticks;
            return retStr;
        }

 





 


        protected List<SelectListItem> GetMonths()
        {
            var dec = new List<SelectListItem>();
            dec.Add(new SelectListItem() { Value = "1", Text = "JAN" });
            dec.Add(new SelectListItem() { Value = "2", Text = "FEB" });
            dec.Add(new SelectListItem() { Value = "3", Text = "MAR" });
            dec.Add(new SelectListItem() { Value = "4", Text = "APR" });
            dec.Add(new SelectListItem() { Value = "5", Text = "MAY" });
            dec.Add(new SelectListItem() { Value = "6", Text = "JUN" });
            dec.Add(new SelectListItem() { Value = "7", Text = "JUL" });
            dec.Add(new SelectListItem() { Value = "8", Text = "AUG" });
            dec.Add(new SelectListItem() { Value = "9", Text = "SEP" });
            dec.Add(new SelectListItem() { Value = "10", Text = "OCT" });
            dec.Add(new SelectListItem() { Value = "11", Text = "NOV" });
            dec.Add(new SelectListItem() { Value = "12", Text = "DEC" });
            return dec;
        }

        protected List<SelectListItem> GetDays()
        {
            var bli = new List<SelectListItem>();

            for (var i = 1; i < 32; i++)
            {
                var t = i < 10 ? "0" + i : i.ToString();

                bli.Add(new SelectListItem() { Value = i.ToString(), Text = t });
            }
            return bli;
        }

        protected List<SelectListItem> GetYears(int begYr, int endYr)
        {

            // birth up to 100 years old
            //End date at least 18

            var begYear = DateTime.Now.Year + begYr;
            var endYear = DateTime.Now.Year + endYr;


            var bli = new List<SelectListItem>();

            for (var i = begYear; i < endYear; i++)
            {
                bli.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }
            return bli;
        }

        public long PhoneToInt(string phone)
        {
            long i64 = 0;

            var phStr = Regex.Replace(phone, "[^.0-9]", "");
            var phInt = Int64.TryParse(phStr, out i64) ? Convert.ToInt64(phStr) : i64;
            return phInt;
        }

        public string PhoneToString(long phone)
        {
            return phone.ToString("(###) ###-####");
        }

        public string CookSku(int catId, int subCatId, int transTypeId, int locId)
        {
            var retStr = String.Empty;

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMakeSku");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[4];
                parameters[0] = new SqlParameter("@CategoryID", SqlDbType.Int) { Value = catId };
                parameters[1] = new SqlParameter("@SubCatID", SqlDbType.Int) { Value = subCatId };
                parameters[2] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = transTypeId };
                parameters[3] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = locId };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return retStr;

                dr.Read();

                retStr = dr["Sku"].ToString();
            }

            return retStr;
        }


        public string CookGunSkuFromId(int gunId, int ttp)
        {
            var retStr = String.Empty;

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMakeGunSkuFromID");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@InStockID", SqlDbType.Int) { Value = gunId };
                parameters[1] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = ttp };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return retStr;

                dr.Read();

                retStr = dr["Sku"].ToString();
            }

            return retStr;
        }


        public string GetImgDir(int ttp, int cid, bool ihs, bool ico, string dsc, string dir) 
        {
            var idr = string.Empty;

                var cat = Enum.GetName(typeof(ItemCategories), cid);
                var pth = string.Empty;
                var pfd = string.Empty;

                if (string.IsNullOrEmpty(dsc)) // custom order
                {
                    if (ico) { ttp = (int)PicFolders.Custom; }
                    idr = GetSubDir(ttp, cat);
                }
                else  // has disctributor
                {
                    if (ihs)
                    {
                        ttp = (int)PicFolders.InStock; 
                        idr = GetSubDir(ttp, cat);
                }
                    else
                    {
                        idr = string.Format("{0}/{1}/L", dir, dsc);
                    }
                }


            return idr;
        }

        public string GetSubDir(int ttp, string cat) 
        {
            var sdr = string.Empty;

            var pfd = Enum.GetName(typeof(PicFolders), ttp);
            sdr = string.Format("{0}/{1}", pfd, cat);

            return sdr;
        }





        

    }
}