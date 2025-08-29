using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AgMvcAdmin.Models.Common;
using WebBase.Configuration;

namespace AgMvcAdmin.Models
{
    public class BookContext : BaseModel
    {
        public List<BoundBookModel> GetBoundBook(BoundBookModel m)
        {
            var l = new List<BoundBookModel>();


            using (var conn = new SqlConnection(ComplSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetBoundBook");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                //var dateFrom = m.DateMin;
                //object dateFrom;
                //object dateTo;

                object aqMin;
                object aqMax;
                object dpMin;
                object dpMax;

                if (m.AcqDateId == 0) { aqMin = DBNull.Value; aqMax = DBNull.Value; } else { aqMin = m.AcqDateMin; aqMax = m.AcqDateMax; }
                if (m.DspDateId == 0) { dpMin = DBNull.Value; dpMax = DBNull.Value; } else { dpMin = m.DspDateMin; dpMax = m.DspDateMax; }

                var parameters = new IDataParameter[23];
                parameters[0] = new SqlParameter("@LocID", SqlDbType.Int) { Value = m.LocationId };
                parameters[1] = new SqlParameter("@DispID", SqlDbType.Int) { Value = m.DspTypeId };
                parameters[2] = new SqlParameter("@CorrID", SqlDbType.Int) { Value = m.CorrectionId};
                parameters[3] = new SqlParameter("@SortID", SqlDbType.Int) { Value = m.SortId };
                parameters[4] = new SqlParameter("@StartRowIndex", SqlDbType.Int) { Value = m.PagingStartRow };
                parameters[5] = new SqlParameter("@MaximumRows", SqlDbType.Int) { Value = m.PagingMaxRows };
                parameters[6] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = m.IsSale };
                parameters[7] = new SqlParameter("@IsCons", SqlDbType.Bit) { Value = m.IsConsign };
                parameters[8] = new SqlParameter("@IsTrns", SqlDbType.Bit) { Value = m.IsTransfer };
                parameters[9] = new SqlParameter("@IsShip", SqlDbType.Bit) { Value = m.IsShipping };
                parameters[10] = new SqlParameter("@IsStor", SqlDbType.Bit) { Value = m.IsStorage };
                parameters[11] = new SqlParameter("@IsRepr", SqlDbType.Bit) { Value = m.IsRepair };
                parameters[12] = new SqlParameter("@IsAcqn", SqlDbType.Bit) { Value = m.IsAcquisition };
                parameters[13] = new SqlParameter("@IsPst", SqlDbType.Bit) { Value = m.IsPistol };
                parameters[14] = new SqlParameter("@IsRev", SqlDbType.Bit) { Value = m.IsRevolver };
                parameters[15] = new SqlParameter("@IsRif", SqlDbType.Bit) { Value = m.IsRifle };
                parameters[16] = new SqlParameter("@IsSht", SqlDbType.Bit) { Value = m.IsShotgun };
                parameters[17] = new SqlParameter("@IsRec", SqlDbType.Bit) { Value = m.IsReceiver };
                parameters[18] = new SqlParameter("@AcqDateMin", SqlDbType.DateTime) { Value = aqMin };
                parameters[19] = new SqlParameter("@AcqDateMax", SqlDbType.DateTime) { Value = aqMax };
                parameters[20] = new SqlParameter("@DspDateMin", SqlDbType.DateTime) { Value = dpMin };
                parameters[21] = new SqlParameter("@DspDateMax", SqlDbType.DateTime) { Value = dpMax };
                parameters[22] = new SqlParameter("@SrchTxt", SqlDbType.VarChar) { Value = m.SearchTxt.Length==0 ? (object)DBNull.Value: m.SearchTxt };

                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();

                if (!dr.HasRows) return l;

                while (dr.Read())
                {
                    var x0 = 0;
                    var b0 = false;
                    var dt0 = DateTime.MinValue;

                    var i1 = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                    var i2 = Int32.TryParse(dr["GunRowCount"].ToString(), out x0) ? Convert.ToInt32(dr["GunRowCount"]) : 0;

                    var b1 = Boolean.TryParse(dr["IsDisposed"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDisposed"]) : b0;
                    var b2 = Boolean.TryParse(dr["IsCorrected"].ToString(), out b0) ? Convert.ToBoolean(dr["IsCorrected"]) : b0;
                    var b3 = Boolean.TryParse(dr["IsOriginal"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOriginal"]) : b0;

                    var adt = DateTime.TryParse(dr["AcqDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["AcqDate"]) : dt0;
                    var ddt = DateTime.TryParse(dr["DspDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["DspDate"]) : dt0;
                    var mdt = DateTime.TryParse(dr["LastUpdated"].ToString(), out dt0) ? Convert.ToDateTime(dr["LastUpdated"]) : dt0;

                    var v1 = adt == DateTime.MinValue ? string.Empty : adt.ToShortDateString();
                    var v2 = ddt == DateTime.MinValue ? string.Empty : ddt.ToShortDateString();
                    var v3 = mdt == DateTime.MinValue ? string.Empty : mdt.ToShortDateString();

                    var v4 = dr["TransID"].ToString();
                    var v5 = dr["Manufacturer"].ToString();
                    var v6 = dr["Importer"].ToString();
                    var v7 = dr["Model"].ToString();
                    var v8 = dr["CaliberGauge"].ToString();
                    var v9 = dr["SerialNumber"].ToString();
                    var v10 = dr["GunType"].ToString();
                    var v11 = dr["AcqName"].ToString();
                    var v12 = dr["AcqAddressFFL"].ToString();
                    var v13 = dr["DspName"].ToString();
                    var v14 = dr["DspAddressFFL"].ToString();
                    var v15 = dr["BookCode"].ToString();

                    var bm = new BoundBookModel(i1, i2, b1, b2, b3, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15);
                    l.Add(bm);

                }
            }

            return l;

        }


        public BoundBookModel GetBookItemById(int locId, int id)
        {
            var m = new BoundBookModel();

            using (var conn = new SqlConnection(ComplSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetBookEntryByID");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@LocID", SqlDbType.Int) { Value = locId };
                parameters[1] = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return m;

                dr.Read();

                var dt0 = DateTime.MinValue;
                var b0 = false;
                var i0 = 0;

                var i1 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : 0;
                var i2 = Int32.TryParse(dr["AcqFFLCode"].ToString(), out i0) ? Convert.ToInt32(dr["AcqFFLCode"]) : 0;
                var i3 = Int32.TryParse(dr["AcqFFLStateID"].ToString(), out i0) ? Convert.ToInt32(dr["AcqFFLStateID"]) : 0;
                var i4 = Int32.TryParse(dr["AcqFFLSourceID"].ToString(), out i0) ? Convert.ToInt32(dr["AcqFFLSourceID"]) : 0;
                var i5 = Int32.TryParse(dr["AcqSourceID"].ToString(), out i0) ? Convert.ToInt32(dr["AcqSourceID"]) : 0;
                var i6 = Int32.TryParse(dr["AcqStateID"].ToString(), out i0) ? Convert.ToInt32(dr["AcqStateID"]) : 0;
                var i7 = Int32.TryParse(dr["DspFFLCode"].ToString(), out i0) ? Convert.ToInt32(dr["DspFFLCode"]) : 0;
                var i8 = Int32.TryParse(dr["DspFFLStateID"].ToString(), out i0) ? Convert.ToInt32(dr["DspFFLStateID"]) : 0;
                var i9 = Int32.TryParse(dr["DspFFLSourceID"].ToString(), out i0) ? Convert.ToInt32(dr["DspFFLSourceID"]) : 0;
                var i10 = Int32.TryParse(dr["DspSourceID"].ToString(), out i0) ? Convert.ToInt32(dr["DspSourceID"]) : 0;
                var i11 = Int32.TryParse(dr["DspStateID"].ToString(), out i0) ? Convert.ToInt32(dr["DspStateID"]) : 0;
                var i12 = Int32.TryParse(dr["DspFulfillTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["DspFulfillTypeID"]) : 0;
                var i13 = Int32.TryParse(dr["ManufID"].ToString(), out i0) ? Convert.ToInt32(dr["ManufID"]) : 0;
                var i14 = Int32.TryParse(dr["ImporterID"].ToString(), out i0) ? Convert.ToInt32(dr["ImporterID"]) : 0;
                var i15 = Int32.TryParse(dr["CaliberID"].ToString(), out i0) ? Convert.ToInt32(dr["CaliberID"]) : 0;
                var i16 = Int32.TryParse(dr["GunTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["GunTypeID"]) : 0;

                var v1 = dr["TransID"].ToString();
                var v2 = dr["SerialNumber"].ToString();
                var v3 = dr["AcqOrgName"].ToString();
                var v4 = dr["AcqFirstName"].ToString();
                var v5 = dr["AcqLastName"].ToString();
                var v6 = dr["AcqAddress"].ToString();
                var v7 = dr["AcqCity"].ToString();
                var v8 = dr["AcqZip"].ToString();
                var v9 = dr["AcqZipExt"].ToString();
                var v10 = dr["AcqCurioFFL"].ToString();
                var v11 = dr["AcqCurioName"].ToString();
                var v12 = dr["DspOrgName"].ToString();
                var v13 = dr["DspFirstName"].ToString();
                var v14 = dr["DspMiddleName"].ToString();
                var v15 = dr["DspLastName"].ToString();
                var v16 = dr["DspAddress"].ToString();
                var v17 = dr["DspCity"].ToString();
                var v18 = dr["DspZip"].ToString();
                var v19 = dr["DspZipExt"].ToString();
                var v20 = dr["DspCurioFFL"].ToString();
                var v21 = dr["DspCurioName"].ToString();
                var v22 = dr["CFLCInbound"].ToString();
                var v23 = dr["CFLCOutbound"].ToString();

                var dt1 = DateTime.TryParse(dr["AcqDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["AcqDate"]) : dt0;
                var dt2 = DateTime.TryParse(dr["DisposeDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["DisposeDate"]) : dt0;
                var dt3 = DateTime.TryParse(dr["AcqCurioFFLExp"].ToString(), out dt0) ? Convert.ToDateTime(dr["AcqCurioFFLExp"]) : dt0;
                var dt4 = DateTime.TryParse(dr["DspCurioFFLExp"].ToString(), out dt0) ? Convert.ToDateTime(dr["DspCurioFFLExp"]) : dt0;

                var v24 = dt1.ToString("MM/dd/yyyy");
                var v25 = dt2 == DateTime.MinValue ? string.Empty : dt2.ToString("MM/dd/yyyy");
                var v26 = dt3 == DateTime.MinValue ? string.Empty : dt3.ToString("MM/dd/yyyy");
                var v27 = dt4 == DateTime.MinValue ? string.Empty : dt4.ToString("MM/dd/yyyy");


                var b1 = Boolean.TryParse(dr["IsDisposed"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDisposed"]) : b0;
                var b2 = Boolean.TryParse(dr["HasHiCaps"].ToString(), out b0) ? Convert.ToBoolean(dr["HasHiCaps"]) : b0;


                m = new BoundBookModel(i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, i13, i14, i15, i16, v1, v2, 
                                       v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, 
                                       v20, v21, v22, v23, v24, v25, v26, v27, b1, b2);

            }

            return m;
        }

        public BoundBookModel InsertBookEntry(AddToBookModel m)
        {

            var i0 = 0;
            var d1 = DateTime.MinValue;
            var d2 = DateTime.MinValue;
            var b = new BoundBookModel();

            using (var conn = new SqlConnection(ComplSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcInsertBookEntry");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[28];
                parameters[0] = new SqlParameter("@InStockID", SqlDbType.Int) { Value = m.BoundBook.InStockId };
                parameters[1] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = m.BoundBook.LocationId };
                parameters[2] = new SqlParameter("@ItemBasisID", SqlDbType.Int) { Value = m.BoundBook.ItemBasisID };
                parameters[3] = new SqlParameter("@SubCatID", SqlDbType.Int) { Value = m.BoundBook.SubCatId };
                parameters[4] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = m.BoundBook.TransTypeId };
                parameters[5] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.BoundBook.ManufId };
                parameters[6] = new SqlParameter("@ImporterID", SqlDbType.Int) { Value = m.BoundBook.ImporterId };
                parameters[7] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = m.BoundBook.GunTypeId };
                parameters[8] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.BoundBook.CaliberId };
                parameters[9] = new SqlParameter("@AcqSourceID", SqlDbType.Int) { Value = m.BoundBook.AcqTypeId };
                parameters[10] = new SqlParameter("@HiCapMagCt", SqlDbType.Int) { Value = m.Compliance.HiCapMagCount };
                parameters[11] = new SqlParameter("@HiCapCap", SqlDbType.Int) { Value = m.Compliance.HiCapCapacity };
                parameters[12] = new SqlParameter("@AcqFFLSrc", SqlDbType.Int) { Value = m.BoundBook.AcqFflSrc };
                parameters[13] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = m.SupplierId };
                parameters[14] = new SqlParameter("@FFLCode", SqlDbType.Int) { Value = m.BoundBook.AcqFflCode };
                parameters[15] = new SqlParameter("@IsHold30", SqlDbType.Bit) { Value = m.Compliance.HoldGun };
                parameters[16] = new SqlParameter("@TransID", SqlDbType.VarChar) { Value = m.BoundBook.TagSku };
                parameters[17] = new SqlParameter("@Manufacturer", SqlDbType.VarChar) { Value = m.BoundBook.GunMfg };
                parameters[18] = new SqlParameter("@Importer", SqlDbType.VarChar) { Value = m.BoundBook.GunImpt == "" ? (object)DBNull.Value : m.BoundBook.GunImpt };
                parameters[19] = new SqlParameter("@Model", SqlDbType.VarChar) { Value = m.BoundBook.GunModelName };
                parameters[20] = new SqlParameter("@SerialNumber", SqlDbType.VarChar) { Value = m.BoundBook.GunSerial };
                parameters[21]= new SqlParameter("@GunType", SqlDbType.VarChar) { Value = m.BoundBook.GunType };
                parameters[22] = new SqlParameter("@CaliberGauge", SqlDbType.VarChar) { Value = m.BoundBook.GunCaliber };
                parameters[23] = new SqlParameter("@CFLCInbound", SqlDbType.VarChar) { Value = m.BoundBook.CflcInbound };
                parameters[24] = new SqlParameter("@Email", SqlDbType.VarChar) { Value = m.BoundBook.AcqEmail };
                parameters[25] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = m.BoundBook.AcqDate };
                parameters[26] = new SqlParameter("@HoldExp", SqlDbType.DateTime) { Value = m.Compliance.HoldGunExpires == d2 ? (object)DBNull.Value : m.Compliance.HoldGunExpires };
                parameters[27] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = m.BoundBook.IsSale };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return b;

                dr.Read();

                var id = Int32.TryParse(dr["BookID"].ToString(), out i0) ? Convert.ToInt32(dr["BookID"]) : 0;
                var tid = dr["TransID"].ToString();
                b.BookId = id;
                b.TagSku = tid;
                
                return b;

            }
        }


        public void UpdateBookItem(BoundBookModel m)
        {
            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(ComplSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateBookEntry");
                var cmd = new SqlCommand(proc, conn) {CommandType = CommandType.StoredProcedure};
                conn.Open();

                var parameters = new IDataParameter[51];

                parameters[0] = new SqlParameter("@IsDisposed", SqlDbType.Bit) { Value = m.IsDisposed };
                parameters[1] = new SqlParameter("@LocID", SqlDbType.Int) { Value = m.LocationId };
                parameters[2] = new SqlParameter("@ID", SqlDbType.Int) { Value = m.Id };
                parameters[3] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.ManufId };
                parameters[4] = new SqlParameter("@ImporterID", SqlDbType.Int) { Value = m.ImporterId };
                parameters[5] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.CaliberId };
                parameters[6] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = m.GunTypeId };
                parameters[7] = new SqlParameter("@AcqFFLID", SqlDbType.Int) { Value = m.AcqFflId };
                parameters[8] = new SqlParameter("@AcqStateID", SqlDbType.Int) { Value = m.AcqStateId };
                parameters[9] = new SqlParameter("@AcqSourceID", SqlDbType.Int) { Value = m.AcqTypeId };
                parameters[10] = new SqlParameter("@AcqFFLSourceID", SqlDbType.Int) { Value = m.AcqFflSrc };
                parameters[11] = new SqlParameter("@AcqFflStateID", SqlDbType.Int) { Value = m.AcqFflStateId };
                parameters[12] = new SqlParameter("@DspFFLID", SqlDbType.Int) { Value = m.DispFflId };
                parameters[13] = new SqlParameter("@DspStateID", SqlDbType.Int) { Value = m.DspStateId };
                parameters[14] = new SqlParameter("@DspSourceID", SqlDbType.Int) { Value = m.DspTypeId };
                parameters[15] = new SqlParameter("@DspFFLSourceID", SqlDbType.Int) { Value = m.DspFflSrc };
                parameters[16] = new SqlParameter("@DspFflStateID", SqlDbType.Int) { Value = m.DspFflStateId };
                parameters[17] = new SqlParameter("@Model", SqlDbType.VarChar) { Value = m.GunModelName };
                parameters[18] = new SqlParameter("@SerialNumber", SqlDbType.VarChar) { Value = m.GunSerial };
                parameters[19] = new SqlParameter("@Manufacturer", SqlDbType.VarChar) { Value = m.GunMfg };
                parameters[20] = new SqlParameter("@Importer", SqlDbType.VarChar) { Value = m.GunImpt };
                parameters[21] = new SqlParameter("@GunType", SqlDbType.VarChar) { Value = m.GunType };
                parameters[22] = new SqlParameter("@CaliberGauge", SqlDbType.VarChar) { Value = m.GunCaliber };
                parameters[23] = new SqlParameter("@AcqName", SqlDbType.VarChar) { Value = m.AcqName };
                parameters[24] = new SqlParameter("@AcqAddressFFL", SqlDbType.VarChar) { Value = m.AcqAddrOrFfl };
                parameters[25] = new SqlParameter("@AcqOrgName", SqlDbType.VarChar) { Value = m.AcqOrgName };
                parameters[26] = new SqlParameter("@AcqFirstName", SqlDbType.VarChar) { Value = m.AcqFirstName };
                parameters[27] = new SqlParameter("@AcqLastName", SqlDbType.VarChar) { Value = m.AcqLastName };
                parameters[28] = new SqlParameter("@AcqAddress", SqlDbType.VarChar) { Value = m.AcqAddress };
                parameters[29] = new SqlParameter("@AcqCity", SqlDbType.VarChar) { Value = m.AcqCity };
                parameters[30] = new SqlParameter("@AcqZip", SqlDbType.VarChar) { Value = m.AcqZipCode };
                parameters[31] = new SqlParameter("@AcqZipExt", SqlDbType.VarChar) { Value = m.AcqZipExt };
                parameters[32] = new SqlParameter("@AcqCurioName", SqlDbType.VarChar) { Value = m.AcqCurioName };
                parameters[33] = new SqlParameter("@AcqCurioFFL", SqlDbType.VarChar) { Value = m.AcqCurioFfl };
                parameters[34] = new SqlParameter("@AcqCflc", SqlDbType.VarChar) { Value = m.CflcInbound };
                parameters[35] = new SqlParameter("@DspName", SqlDbType.VarChar) { Value = m.DispName };
                parameters[36] = new SqlParameter("@DspAddressFFL", SqlDbType.VarChar) { Value = m.DispAddrOrFfl };
                parameters[37] = new SqlParameter("@DspOrgName", SqlDbType.VarChar) { Value = m.DispOrgName };
                parameters[38] = new SqlParameter("@DspFirstName", SqlDbType.VarChar) { Value = m.DispFirstName };
                parameters[39] = new SqlParameter("@DspLastName", SqlDbType.VarChar) { Value = m.DispLastName };
                parameters[40] = new SqlParameter("@DspAddress", SqlDbType.VarChar) { Value = m.DispAddress };
                parameters[41] = new SqlParameter("@DspCity", SqlDbType.VarChar) { Value = m.DispCity };
                parameters[42] = new SqlParameter("@DspZip", SqlDbType.VarChar) { Value = m.DispZipCode };
                parameters[43] = new SqlParameter("@DspZipExt", SqlDbType.VarChar) { Value = m.DispZipExt };
                parameters[44] = new SqlParameter("@DspCurioName", SqlDbType.VarChar) { Value = m.DspCurioName };
                parameters[45] = new SqlParameter("@DspCurioFFL", SqlDbType.VarChar) { Value = m.DspCurioFfl };
                parameters[46] = new SqlParameter("@DspCflc", SqlDbType.VarChar) { Value = m.CflcOutbound };
                parameters[47] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = m.AcqDate };
                parameters[48] = new SqlParameter("@DisposeDate", SqlDbType.DateTime) { Value = m.DispDate == dt0 ? (object)DBNull.Value : m.DispDate };
                parameters[49] = new SqlParameter("@AcqCurioFFLExp", SqlDbType.DateTime) { Value = m.AcqCurExp == dt0 ? (object)DBNull.Value : m.AcqCurExp };
                parameters[50] = new SqlParameter("@DspCurioFFLExp", SqlDbType.DateTime) { Value = m.DspCurExp == dt0 ? (object)DBNull.Value : m.DspCurExp };

                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }
                cmd.ExecuteReader();
            }




        }


        public void DisposeBookItem(BoundBookModel m)
        {
            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(ComplSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDisposeBookEntry");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[22];

                parameters[0] = new SqlParameter("@IsDisposed", SqlDbType.Bit) { Value = m.IsDisposed };
                parameters[1] = new SqlParameter("@HiCapsDisposed", SqlDbType.Bit) { Value = m.IsHiCaps };
                parameters[2] = new SqlParameter("@LocID", SqlDbType.Int) { Value = m.LocationId };
                parameters[3] = new SqlParameter("@ID", SqlDbType.Int) { Value = m.Id };
                parameters[4] = new SqlParameter("@DspSourceID", SqlDbType.Int) { Value = m.DspTypeId };
                parameters[5] = new SqlParameter("@DspFFLSourceID", SqlDbType.Int) { Value = m.DspFflSrc };
                parameters[6] = new SqlParameter("@DspFFLID", SqlDbType.Int) { Value = m.DispFflId };
                parameters[7] = new SqlParameter("@DspStateID", SqlDbType.Int) { Value = m.DspStateId };
                parameters[8] = new SqlParameter("@DisposeDate", SqlDbType.DateTime) { Value = m.DispDate == dt0 ? (object)DBNull.Value : m.DispDate };
                parameters[9] = new SqlParameter("@DspCurioFFLExp", SqlDbType.DateTime) { Value = m.DspCurExp == dt0 ? (object)DBNull.Value : m.DspCurExp };
                parameters[10] = new SqlParameter("@DspName", SqlDbType.VarChar) { Value = m.DispName };
                parameters[11] = new SqlParameter("@DspAddressFFL", SqlDbType.VarChar) { Value = m.DispAddrOrFfl };
                parameters[12] = new SqlParameter("@DspOrgName", SqlDbType.VarChar) { Value = m.DispOrgName };
                parameters[13] = new SqlParameter("@DspFirstName", SqlDbType.VarChar) { Value = m.DispFirstName };
                parameters[14] = new SqlParameter("@DspLastName", SqlDbType.VarChar) { Value = m.DispLastName };
                parameters[15] = new SqlParameter("@DspAddress", SqlDbType.VarChar) { Value = m.DispAddress };
                parameters[16] = new SqlParameter("@DspCity", SqlDbType.VarChar) { Value = m.DispCity };
                parameters[17] = new SqlParameter("@DspZip", SqlDbType.VarChar) { Value = m.DispZipCode };
                parameters[18] = new SqlParameter("@DspZipExt", SqlDbType.VarChar) { Value = m.DispZipExt };
                parameters[19] = new SqlParameter("@DspCurioName", SqlDbType.VarChar) { Value = m.DspCurioName };
                parameters[20] = new SqlParameter("@DspCurioFFL", SqlDbType.VarChar) { Value = m.DspCurioFfl };
                parameters[21] = new SqlParameter("@DspCflc", SqlDbType.VarChar) { Value = m.CflcOutbound };

                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }
                cmd.ExecuteReader();
            }
        }


        public FflLicenseModel GetFflBookEntry(int fflId)
        {
            var f = new FflLicenseModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMenuGetFFLBookData");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@FFLID", DbType.String) { Value = fflId };
                cmd.Parameters.Add(param); 

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return f;

                dr.Read();
                var nam = dr["TradeName"].ToString();
                var lic = dr["FullLicNumber"].ToString();

                f.TradeName = nam;
                f.FflFullLic = lic;
            }

            return f;
        }

        public FflLicenseModel GetFflByCode(int fflCode)
        {
            var f = new FflLicenseModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetFFLByCode");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@FFLCode", DbType.String) { Value = fflCode };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return f;

                dr.Read();
                var nam = dr["TradeName"].ToString();
                var lic = dr["FullLicNumber"].ToString();

                f.TradeName = nam;
                f.FflFullLic = lic;
            }

            return f;
        }




        public List<BoundBookModel> GetCflcData(BoundBookModel m)
        {
            var bm = new List<BoundBookModel>();
            var dt0 = DateTime.MinValue;
            var dtd = DateTime.MinValue;
            var x0 = 0;

            using (var conn = new SqlConnection(ComplSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetCaCflc");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[9];

                parameters[0] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = m.LocationId };
                parameters[1] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.ManufId };
                parameters[2] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = m.GunTypeId };
                parameters[3] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.CaliberId };
                parameters[4] = new SqlParameter("@StartRowIndex", SqlDbType.Int) { Value = m.PagingStartRow };
                parameters[5] = new SqlParameter("@MaximumRows", SqlDbType.Int) { Value = m.PagingMaxRows };
                parameters[6] = new SqlParameter("@SrchTxt", SqlDbType.VarChar) { Value = m.SearchTxt };
                parameters[7] = new SqlParameter("@DateFrom", SqlDbType.DateTime) { Value = m.DateMin == dt0 ? (object)DBNull.Value : m.DateMin };
                parameters[8] = new SqlParameter("@DateTo", SqlDbType.DateTime) { Value = m.DateMax == dt0 ? (object)DBNull.Value : m.DateMax };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }


                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return bm;

                while (dr.Read())
                {
                    var dt1 = DateTime.TryParse(dr["AcqDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["AcqDate"]) : dt0;
                    var dt2 = DateTime.TryParse(dr["DisposeDate"].ToString(), out dtd) ? Convert.ToDateTime(dr["DisposeDate"]) : dtd;


                    var v1 = dr["TransID"].ToString();
                    var v2 = dr["GunDesc"].ToString();
                    var v3 = dr["AcqName"].ToString();
                    var v4 = dr["AcqAddressFFL"].ToString();
                    var v5 = dr["CFLCInbound"].ToString();
                    var v6 = dr["DspName"].ToString();
                    var v7 = dr["DspAddressFFL"].ToString();
                    var v8 = dr["CFLCOutbound"].ToString();
                    var v9 = dt1 == DateTime.MinValue ? String.Empty : dt1.Date.ToString("MM/dd/yyyy");
                    var v10 = dt2 == DateTime.MinValue ? String.Empty : dt2.Date.ToString("MM/dd/yyyy");
                    var trc = Int32.TryParse(dr["GunRowCount"].ToString(), out x0) ? Convert.ToInt32(dr["GunRowCount"]) : 0;

                    var b = new BoundBookModel(m.LocationId, trc, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10);
                    bm.Add(b);

                }
            }

            return bm;
        }


        public BoundBookModel GetMagById(int id)
        {

            var bm = new BoundBookModel();
            var dt0 = DateTime.MinValue;
            var dtd = DateTime.MinValue;
            var x0 = 0;
            var b0 = false;

            using (var conn = new SqlConnection(ComplSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetHiCapMag");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param); 


                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return bm;

                dr.Read();

                    var dt1 = DateTime.TryParse(dr["AcqDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["AcqDate"]) : dt0;
                    var dt2 = DateTime.TryParse(dr["DisposeDate"].ToString(), out dtd) ? Convert.ToDateTime(dr["DisposeDate"]) : dtd;

                    var i1 = Int32.TryParse(dr["Capacity"].ToString(), out x0) ? Convert.ToInt32(dr["Capacity"]) : 0;
                    var i2 = Int32.TryParse(dr["ManufID"].ToString(), out x0) ? Convert.ToInt32(dr["ManufID"]) : 0;
                    var i3 = Int32.TryParse(dr["CaliberID"].ToString(), out x0) ? Convert.ToInt32(dr["CaliberID"]) : 0;
                    var i4 = Int32.TryParse(dr["GunTypeID"].ToString(), out x0) ? Convert.ToInt32(dr["GunTypeID"]) : 0;


                    var v1 = dr["Model"].ToString();
                    var v2 = dr["AcqName"].ToString();
                    var v3 = dr["AcqAddressFFL"].ToString();
                    var v4 = dr["DisposeName"].ToString();
                    var v5 = dr["DisposeAddressFFL"].ToString();
                    var v6 = dt1 == DateTime.MinValue ? String.Empty : dt1.Date.ToString("MM/dd/yyyy");
                    var v7 = dt2 == DateTime.MinValue ? String.Empty : dt2.Date.ToString("MM/dd/yyyy");

                    var b1 = Boolean.TryParse(dr["IsDisposed"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDisposed"]) : b0;


                bm = new BoundBookModel(id, i1, i2, i3, i4, v1, v2, v3, v4, v5, v6, v7, b1);

            }

            return bm;
        }


        public List<BoundBookModel> GetHiCapData(BoundBookModel m)
        {
            var bm = new List<BoundBookModel>();
            var dt0 = DateTime.MinValue;
            var dtd = DateTime.MinValue;
            var x0 = 0;

            using (var conn = new SqlConnection(ComplSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCaHiCapLog");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[8];
                parameters[0] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.ManufId };
                parameters[1] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = m.GunTypeId };
                parameters[2] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.CaliberId };
                parameters[3] = new SqlParameter("@CapacityID", SqlDbType.Int) { Value = m.CapacityId };
                parameters[4] = new SqlParameter("@StartRowIndex", SqlDbType.Int) { Value = m.PagingStartRow };
                parameters[5] = new SqlParameter("@MaximumRows", SqlDbType.Int) { Value = m.PagingMaxRows };
                parameters[6] = new SqlParameter("@DateFrom", SqlDbType.DateTime) { Value = m.DateMin == dt0 ? (object)DBNull.Value : m.DateMin };
                parameters[7] = new SqlParameter("@DateTo", SqlDbType.DateTime) { Value = m.DateMax == dt0 ? (object)DBNull.Value : m.DateMax };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }


                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return bm;

                while (dr.Read())
                {
                    var dt1 = DateTime.TryParse(dr["AcqDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["AcqDate"]) : dt0;
                    var dt2 = DateTime.TryParse(dr["DisposeDate"].ToString(), out dtd) ? Convert.ToDateTime(dr["DisposeDate"]) : dtd;

                    var i1 = Int32.TryParse(dr["MagID"].ToString(), out x0) ? Convert.ToInt32(dr["MagID"]) : 0;
                    var i2 = Int32.TryParse(dr["Capacity"].ToString(), out x0) ? Convert.ToInt32(dr["Capacity"]) : 0;
                    var i3 = Int32.TryParse(dr["MagRowCount"].ToString(), out x0) ? Convert.ToInt32(dr["MagRowCount"]) : 0;


                    var v1 = dr["TransID"].ToString();
                    var v2 = dr["GunDesc"].ToString();
                    var v3 = dr["AcqName"].ToString();
                    var v4 = dr["AcqAddressFFL"].ToString();
                    var v5 = dr["DspName"].ToString();
                    var v6 = dr["DspAddressFFL"].ToString();
                    var v7 = dt1 == DateTime.MinValue ? String.Empty : dt1.Date.ToString("MM/dd/yyyy");
                    var v8 = dt2 == DateTime.MinValue ? String.Empty : dt2.Date.ToString("MM/dd/yyyy");


                    var b = new BoundBookModel(i1, i2, i3, v1, v2, v3, v4, v5, v6, v7, v8);
                    bm.Add(b);
                }
            }

            return bm;
        }

        public void AddHiCapMag(BoundBookModel m)
        {

            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(ComplSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAddHiCapMag");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[12];
                parameters[0] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.ManufId };
                parameters[1] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = m.GunTypeId };
                parameters[2] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.CaliberId };
                parameters[3] = new SqlParameter("@Capacity", SqlDbType.Int) { Value = m.CapacityId };
                parameters[4] = new SqlParameter("@Model", SqlDbType.VarChar) { Value = m.GunModelName };
                parameters[5] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = m.AcqDate == dt0 ? (object)DBNull.Value : m.AcqDate };
                parameters[6] = new SqlParameter("@AcqName", SqlDbType.VarChar) { Value = m.AcqName };
                parameters[7] = new SqlParameter("@AcqAddrFFL", SqlDbType.VarChar) { Value = m.AcqAddrOrFfl };
                parameters[8] = new SqlParameter("@DspDate", SqlDbType.DateTime) { Value = m.DispDate == dt0 ? (object)DBNull.Value : m.DispDate };
                parameters[9] = new SqlParameter("@DspName", SqlDbType.VarChar) { Value = m.DispName };
                parameters[10] = new SqlParameter("@DspAddrFFL", SqlDbType.VarChar) { Value = m.DispAddrOrFfl };
                parameters[11] = new SqlParameter("@IsDisposed", SqlDbType.Bit) { Value = m.IsDisposed };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                cmd.ExecuteReader();

            }
        }


        public void EditHiCapMag(BoundBookModel m)
        {

            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(ComplSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateHiCapMag");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[13];
                parameters[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = m.Id };
                parameters[1] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.ManufId };
                parameters[2] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = m.GunTypeId };
                parameters[3] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.CaliberId };
                parameters[4] = new SqlParameter("@Capacity", SqlDbType.Int) { Value = m.CapacityId };
                parameters[5] = new SqlParameter("@Model", SqlDbType.VarChar) { Value = m.GunModelName };
                parameters[6] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = m.AcqDate == dt0 ? (object)DBNull.Value : m.AcqDate };
                parameters[7] = new SqlParameter("@AcqName", SqlDbType.VarChar) { Value = m.AcqName };
                parameters[8] = new SqlParameter("@AcqAddrFFL", SqlDbType.VarChar) { Value = m.AcqAddrOrFfl };
                parameters[9] = new SqlParameter("@DspDate", SqlDbType.DateTime) { Value = m.DispDate == dt0 ? (object)DBNull.Value : m.DispDate };
                parameters[10] = new SqlParameter("@DspName", SqlDbType.VarChar) { Value = m.DispName };
                parameters[11] = new SqlParameter("@DspAddrFFL", SqlDbType.VarChar) { Value = m.DispAddrOrFfl };
                parameters[12] = new SqlParameter("@IsDisposed", SqlDbType.Bit) { Value = m.IsDisposed };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                cmd.ExecuteReader();

            }
        }


        public void DeleteHiCapMag(int id)
        {
            using (var conn = new SqlConnection(ComplSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDeleteHiCapMag");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param); 

                cmd.ExecuteReader();

            }
        }


    }
}