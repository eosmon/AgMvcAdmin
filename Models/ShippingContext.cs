using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgMvcAdmin.Models.Common;
using WebBase.Configuration;
using AgMvcAdmin.Common;
using AgMvcAdmin.FedEx;
using AppBase;



namespace AgMvcAdmin.Models
{
    public class ShippingContext : BaseModel
    {

        public Shipment GetShippingBase(int tid, Shipment s)
        {
            //var s = new Shipment();

            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetShippingBase");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = tid };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return s;

                var b0 = false;
                var i0 = 0;
                double d0 = 0.00;
                decimal c0 = 0;

                dr.Read();

                var cty = dr["SupCity"].ToString();
                var sta = dr["SupState"].ToString();
                var zip = dr["SupZipCode"].ToString();

                var amo = Boolean.TryParse(dr["HasAmmo"].ToString(), out b0) ? Convert.ToBoolean(dr["HasAmmo"]) : b0;
                var one = Boolean.TryParse(dr["HasOneRate"].ToString(), out b0) ? Convert.ToBoolean(dr["HasOneRate"]) : b0;
                var itm = Boolean.TryParse(dr["HasItems"].ToString(), out b0) ? Convert.ToBoolean(dr["HasItems"]) : b0;

                var ctAmo = Int32.TryParse(dr["BoxCtAmmo"].ToString(), out i0) ? Convert.ToInt32(dr["BoxCtAmmo"]) : i0;
                var ctOne = Int32.TryParse(dr["BoxCtOneRate"].ToString(), out i0) ? Convert.ToInt32(dr["BoxCtOneRate"]) : i0;
                var ctItm = Int32.TryParse(dr["BoxCtItems"].ToString(), out i0) ? Convert.ToInt32(dr["BoxCtItems"]) : i0;


                s.TransactionId = tid;
                s.DestCity = cty;
                s.DestState = sta;
                s.DestZipCode = zip;
                s.DestCountry = "US";
                s.HasAmmo = amo;
                s.HasOneRate = one;
                s.HasItems = itm;

                var la = new List<ShippingItem>();
                var lb = new List<ShippingItem>();
                var lc = new List<ShippingItem>();


                dr.NextResult(); // ITEMS

                if (s.HasItems && dr.HasRows)
                {

                    var a = 1;

                    while (dr.Read())
                    {
                        var units = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;
                        var sigId = Int32.TryParse(dr["SignatureTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["SignatureTypeID"]) : i0;
                        var boxCt = Int32.TryParse(dr["BoxCount"].ToString(), out i0) ? Convert.ToInt32(dr["BoxCount"]) : i0;
                        var boxSz = Int32.TryParse(dr["BoxSizeID"].ToString(), out i0) ? Convert.ToInt32(dr["BoxSizeID"]) : i0;
                        var insured = Boolean.TryParse(dr["IsInsured"].ToString(), out b0) ? Convert.ToBoolean(dr["IsInsured"]) : b0;
                        var isAmmo = Boolean.TryParse(dr["IsOrdnance"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOrdnance"]) : b0;
                        var oneRt = Boolean.TryParse(dr["IsOneRate"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOneRate"]) : b0;
                        var weight = Decimal.TryParse(dr["BoxWeight"].ToString(), out c0) ? Convert.ToDecimal(dr["BoxWeight"]) : c0;
                        var insAmt = Decimal.TryParse(dr["BoxValue"].ToString(), out c0) ? Convert.ToDecimal(dr["BoxValue"]) : c0;
                        var length = Double.TryParse(dr["ItemLength"].ToString(), out d0) ? Convert.ToDouble(dr["ItemLength"]) : d0;
                        var width = Double.TryParse(dr["ItemWidth"].ToString(), out d0) ? Convert.ToDouble(dr["ItemWidth"]) : d0;
                        var height = Double.TryParse(dr["ItemHeight"].ToString(), out d0) ? Convert.ToDouble(dr["ItemHeight"]) : d0;
                        var iLen = Math.Ceiling(length).ToString();
                        var iWid = Math.Ceiling(width).ToString();
                        var iHgt = Math.Ceiling(height).ToString();

                        var bs = boxSz > 7 ? 8 : boxSz;

                        var si = new ShippingItem(a, boxCt, units, (SignatureOptionType)sigId, (FedExPkgTypes)bs, weight, insAmt, iLen, iWid, iHgt, false, insured, isAmmo);
                        la.Add(si);
                        a++;

                    }
                }

                dr.NextResult(); // AMMO

                if (s.HasAmmo && dr.HasRows)
                {
                    var b = 1;

                    while (dr.Read())
                    {
                        var units = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;
                        var sigId = Int32.TryParse(dr["SignatureTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["SignatureTypeID"]) : i0;
                        var boxCt = Int32.TryParse(dr["BoxCount"].ToString(), out i0) ? Convert.ToInt32(dr["BoxCount"]) : i0;
                        var boxSz = Int32.TryParse(dr["BoxSizeID"].ToString(), out i0) ? Convert.ToInt32(dr["BoxSizeID"]) : i0;
                        var insured = Boolean.TryParse(dr["IsInsured"].ToString(), out b0) ? Convert.ToBoolean(dr["IsInsured"]) : b0;
                        var isAmmo = Boolean.TryParse(dr["IsOrdnance"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOrdnance"]) : b0;
                        var oneRt = Boolean.TryParse(dr["IsOneRate"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOneRate"]) : b0;
                        var weight = Decimal.TryParse(dr["BoxWeight"].ToString(), out c0) ? Convert.ToDecimal(dr["BoxWeight"]) : c0;
                        var insAmt = Decimal.TryParse(dr["BoxValue"].ToString(), out c0) ? Convert.ToDecimal(dr["BoxValue"]) : c0;
                        var length = Double.TryParse(dr["ItemLength"].ToString(), out d0) ? Convert.ToDouble(dr["ItemLength"]) : d0;
                        var width = Double.TryParse(dr["ItemWidth"].ToString(), out d0) ? Convert.ToDouble(dr["ItemWidth"]) : d0;
                        var height = Double.TryParse(dr["ItemHeight"].ToString(), out d0) ? Convert.ToDouble(dr["ItemHeight"]) : d0;
                        var iLen = Math.Ceiling(length).ToString();
                        var iWid = Math.Ceiling(width).ToString();
                        var iHgt = Math.Ceiling(height).ToString();

                        var bs = boxSz > 7 ? 8 : boxSz;

                        var si = new ShippingItem(b, boxCt, units, (SignatureOptionType)sigId, (FedExPkgTypes)bs, weight, insAmt, iLen, iWid, iHgt, false, insured, isAmmo);
                        lb.Add(si);
                        b++;

                    }
                }

                dr.NextResult(); // ONE RATE

                if (s.HasOneRate && dr.HasRows)
                {
                    var c = 1;

                    while (dr.Read())
                    {
                        var units = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;
                        var sigId = Int32.TryParse(dr["SignatureTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["SignatureTypeID"]) : i0;
                        var boxCt = Int32.TryParse(dr["BoxCount"].ToString(), out i0) ? Convert.ToInt32(dr["BoxCount"]) : i0;
                        var boxSz = Int32.TryParse(dr["BoxSizeID"].ToString(), out i0) ? Convert.ToInt32(dr["BoxSizeID"]) : i0;
                        var insured = Boolean.TryParse(dr["IsInsured"].ToString(), out b0) ? Convert.ToBoolean(dr["IsInsured"]) : b0;
                        var isAmmo = Boolean.TryParse(dr["IsOrdnance"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOrdnance"]) : b0;
                        var oneRt = Boolean.TryParse(dr["IsOneRate"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOneRate"]) : b0;
                        var weight = Decimal.TryParse(dr["BoxWeight"].ToString(), out c0) ? Convert.ToDecimal(dr["BoxWeight"]) : c0;
                        var insAmt = Decimal.TryParse(dr["BoxValue"].ToString(), out c0) ? Convert.ToDecimal(dr["BoxValue"]) : c0;
                        var length = Double.TryParse(dr["ItemLength"].ToString(), out d0) ? Convert.ToDouble(dr["ItemLength"]) : d0;
                        var width = Double.TryParse(dr["ItemWidth"].ToString(), out d0) ? Convert.ToDouble(dr["ItemWidth"]) : d0;
                        var height = Double.TryParse(dr["ItemHeight"].ToString(), out d0) ? Convert.ToDouble(dr["ItemHeight"]) : d0;
                        var iLen = Math.Ceiling(length).ToString();
                        var iWid = Math.Ceiling(width).ToString();
                        var iHgt = Math.Ceiling(height).ToString();

                        var bs = boxSz > 7 ? 8 : boxSz;

                        var si = new ShippingItem(c, boxCt, units, (SignatureOptionType)sigId, (FedExPkgTypes)bs, weight, insAmt, iLen, iWid, iHgt, false, insured, isAmmo);
                        lc.Add(si);
                        c++;

                    }
                }

                var sor = new ShipmentOneRate();
                var sam = new ShipmentAmmo();

                sor.OneRate = lc;
                sor.GroupBoxCount = ctOne;

                sam.Ammo = lb;
                sam.GroupBoxCount = ctAmo;

                s.ClsAmmo = sam;
                s.ClsOneRate = sor;

                s.Items = la;
                s.ItemBoxCount = ctItm;

                s.QuoteType = ShipQuoteTypes.Unknown;

            }

            return s;
        }

        public void SetInsuranceCost(int tid, double amount)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetInsuranceCost");

            var parameters = new IDataParameter[2];
            parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = tid };
            parameters[1] = new SqlParameter("@Amount", SqlDbType.Decimal) { Value = amount };

            try { DataProcs.ProcParams(EcsmsSqlConnection, proc, parameters); }
            catch (Exception exc) { throw exc; }
        }

        public void ClearShippingRates(int tid)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartSaleClearShipRates");
            var param = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = tid };
 
            try { DataProcs.ProcOneParam(EcsmsSqlConnection, proc, param); }
            catch (Exception exc) { throw exc; }
        }

        

        public void AddShippingRates(ShippingQuote q)
        {
            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartSaleAddShipRates");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[12];
                parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = q.TransactionId };
                parameters[1] = new SqlParameter("@CarrierID", SqlDbType.Int) { Value = (int)q.Carrier };
                parameters[2] = new SqlParameter("@ShipOptionID", SqlDbType.Int) { Value = q.ShipOptionId };
                parameters[3] = new SqlParameter("@TransitDays", SqlDbType.Int) { Value = q.TransitDays };
                parameters[4] = new SqlParameter("@BaseCharge", SqlDbType.Decimal) { Value = q.BaseCost };
                parameters[5] = new SqlParameter("@Insurance", SqlDbType.Decimal) { Value = q.InsureCost };
                parameters[6] = new SqlParameter("@Signature", SqlDbType.Decimal) { Value = q.SignatureCost };
                parameters[7] = new SqlParameter("@Discount", SqlDbType.Decimal) { Value = q.RateDiscount };
                parameters[8] = new SqlParameter("@IsAmmo", SqlDbType.Bit) { Value = q.IsAmmo };
                parameters[9] = new SqlParameter("@IsOneRate", SqlDbType.Bit) { Value = q.IsOneRate };
                parameters[10] = new SqlParameter("@IsError", SqlDbType.Bit) { Value = q.IsError };
                parameters[11] = new SqlParameter("@ShipText", SqlDbType.VarChar) { Value = q.ShipText };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                cmd.ExecuteReader();
            }
        }

        public void SetF2FTransfer(FulfillModel f)
        {
            using (var conn = new SqlConnection(EcsmsSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCartSetF2FTransfer");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[10];
                parameters[0] = new SqlParameter("@TransactionID", SqlDbType.Int) { Value = f.TransactionId };
                parameters[1] = new SqlParameter("@FulfillTypeID", SqlDbType.Int) { Value = f.FulfillSrcId };
                parameters[2] = new SqlParameter("@RecoveryObjID", SqlDbType.Int) { Value = f.RecoveryObjId };
                parameters[3] = new SqlParameter("@Attorney", SqlDbType.VarChar) { Value = f.AttyName == "" ? (object)DBNull.Value : f.AttyName };
                parameters[4] = new SqlParameter("@AttPhone", SqlDbType.VarChar) { Value = f.AttyPhone == "" ? (object)DBNull.Value : f.AttyPhone };
                parameters[5] = new SqlParameter("@AttEmail", SqlDbType.VarChar) { Value = f.AttyEmail == "" ? (object)DBNull.Value : f.AttyEmail };
                parameters[6] = new SqlParameter("@Officer", SqlDbType.VarChar) { Value = f.CaseOfcName == "" ? (object)DBNull.Value : f.CaseOfcName };
                parameters[7] = new SqlParameter("@OfcPhone", SqlDbType.VarChar) { Value = f.CaseOfcPhone == "" ? (object)DBNull.Value : f.CaseOfcPhone };
                parameters[8] = new SqlParameter("@OfcEmail", SqlDbType.VarChar) { Value = f.CaseOfcEmail == "" ? (object)DBNull.Value : f.CaseOfcEmail };
                parameters[9] = new SqlParameter("@Notes", SqlDbType.VarChar) { Value = f.Notes == "" ? (object)DBNull.Value : f.Notes };
 
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }
                cmd.ExecuteReader();
            }
        }




    }
}