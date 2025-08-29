using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using WebBase.Configuration;

namespace AgMvcAdmin.Models.Common
{
    public class FflContext : BaseModel
    {
        public List<FflLicenseModel> SearchFflByName(string s, int stateId)
        {
            var l = new List<FflLicenseModel>();

            using (var conn = new SqlConnection(BotDbSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetFFLByTradeName");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@Name", SqlDbType.VarChar) { Value = s };
                parameters[1] = new SqlParameter("@StateID", SqlDbType.Int) { Value = stateId };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return l;

                long n64 = 0;

                while (dr.Read())
                {
                    var x0 = 0;
                    var xB = false;
                    var fflId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                    var fCode = Int32.TryParse(dr["FFLCode"].ToString(), out x0) ? Convert.ToInt32(dr["FFLCode"]) : 0;
                    var trade = dr["TradeName"].ToString();
                    var addr = dr["BusAddress"].ToString();
                    var cityStZip = dr["BusCityStZip"].ToString();
                    var phone = Int64.TryParse(dr["Phone"].ToString(), out n64) ? PhoneToString(Convert.ToInt64(dr["Phone"])) : string.Empty;
                    var licNum = dr["LicNumber"].ToString();

                    var fullLicNum = dr["FullLicNumber"].ToString();
                    var licValid = Boolean.TryParse(dr["LicValid"].ToString(), out xB) ? Convert.ToBoolean(dr["LicValid"]) : false;
                    var licOnFile = Boolean.TryParse(dr["LicOnFile"].ToString(), out xB) ? Convert.ToBoolean(dr["LicOnFile"]) : false;
                    var licExpDate = String.Format("{0:MM/dd/yyyy}", dr["LicExpires"]);
                    var email = dr["EmailAddress"].ToString();

                    var ffl = new FflLicenseModel(fflId, fCode, trade, addr, cityStZip, phone, licNum, fullLicNum, licValid, licOnFile, licExpDate, email);
                    l.Add(ffl);
                }
            }

            return l;
        }

        public FflLicenseModel GetFflById(int fcd)
        {
            var l = new FflLicenseModel();

            using (var conn = new SqlConnection(BotDbSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcConfirmFFLByID");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var p = new SqlParameter("@FFLCode", SqlDbType.Int) { Value = fcd };
                cmd.Parameters.Add(p);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return l;

                dr.Read();

                var xB = false;
                var email = dr["EmailAddress"].ToString();
                var trade = dr["TradeName"].ToString();
                var fullLicNum = dr["FullLicNumber"].ToString();
                var licValid = Boolean.TryParse(dr["LicValid"].ToString(), out xB) ? Convert.ToBoolean(dr["LicValid"]) : false;
                var licOnFile = Boolean.TryParse(dr["LicOnFile"].ToString(), out xB) ? Convert.ToBoolean(dr["LicOnFile"]) : false;
                var licExpDate = String.Format("{0:MM/dd/yyyy}", dr["LicExpires"]);

                l = new FflLicenseModel(trade, fullLicNum, licValid, licOnFile, licExpDate, email);

            }

            return l;
        }

        public FflLicenseModel GetFflByLicense(FflLicenseModel l)
        {

            try
            {
                SqlDataReader dr;
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetFFLByID");
                var param = new IDataParameter[6];
                param[0] = new SqlParameter("@LicRegion", SqlDbType.Int) { Value = l.LicRegion > 0 ? l.LicRegion : SqlInt32.Null };
                param[1] = new SqlParameter("@LicDistrict", SqlDbType.Int) { Value = l.LicDistrict > 0 ? l.LicDistrict : SqlInt32.Null };
                param[2] = new SqlParameter("@LicCounty", SqlDbType.VarChar) { Value = (object)l.LicCounty ?? DBNull.Value };
                param[3] = new SqlParameter("@LicType", SqlDbType.VarChar) { Value = (object)l.LicType ?? DBNull.Value };
                param[4] = new SqlParameter("@LicExpCode", SqlDbType.VarChar) { Value = (object)l.LicExpCode ?? DBNull.Value };
                param[5] = new SqlParameter("@LicSequence", SqlDbType.VarChar) { Value = (object)l.LicSequence ?? DBNull.Value };

                using (var conn = new SqlConnection(BotDbSqlConnection))
                {
                    var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                    conn.Open();

                    long n64 = 0;
                    var d0 = DateTime.Now;

                    foreach (var parameter in param) { cmd.Parameters.Add(parameter); }
                    dr = cmd.ExecuteReader();

                    if (!dr.HasRows) return l;

                    var nZip = 0;
                    var nSid = 0;

                    dr.Read();
                    l.FflId = Int32.TryParse(dr["ID"].ToString(), out nSid) ? Convert.ToInt32(dr["ID"]) : 0;
                    l.FflCode = Int32.TryParse(dr["FFLCode"].ToString(), out nSid) ? Convert.ToInt32(dr["FFLCode"]) : 0;
                    l.LicName = dr["LicName"].ToString();
                    l.TradeName = dr["TradeName"].ToString();
                    l.FflAddress = dr["Address"].ToString();
                    l.FflCity = dr["City"].ToString();
                    l.FflStateId = Int32.TryParse(dr["StateID"].ToString(), out nSid) ? Convert.ToInt32(dr["StateID"]) : 0;
                    l.FflZipCode = Int32.TryParse(dr["ZipCode"].ToString(), out nZip) ? Convert.ToInt32(dr["ZipCode"]) : 0;
                    l.FflPhone = Int64.TryParse(dr["Phone"].ToString(), out n64) ? PhoneToString(Convert.ToInt64(dr["Phone"])) : string.Empty;
                    l.LicType = dr["LicType"].ToString();
                    l.FflExists = true;
                    l.ExpDate = DateTime.TryParse(dr["LicExpires"].ToString(), out d0) ? Convert.ToDateTime(dr["LicExpires"].ToString()) : d0;
                    l.FflExpDay = l.ExpDate.Day;
                    l.FflExpMo = l.ExpDate.Month;
                    l.FflExpYear = l.ExpDate.Year;
                    l.IsExpired = l.ExpDate < DateTime.Now;

                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return l;
        }

    }
}