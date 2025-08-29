using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using AgMvcAdmin.Common;
using AgMvcAdmin.Models.Common;
using AgMvcAdmin.Models.Menus;
using AppBase;
using WebBase.Configuration;

namespace AgMvcAdmin.Models
{
    public class InvContext : BaseModel
    {
        private readonly string BInqDir = ConfigurationHelper.GetPropertyValue("application", "a8");
        private readonly string BPathDir = ConfigurationHelper.GetPropertyValue("application", "a1");
        private readonly string PthGn = ConfigurationHelper.GetPropertyValue("application", "a4");
        private readonly string PthAm = ConfigurationHelper.GetPropertyValue("application", "a5");
        private readonly string PthMd = ConfigurationHelper.GetPropertyValue("application", "a6");
        private readonly string iStk = ConfigurationHelper.GetPropertyValue("application", "s150");


        public List<InStockModel> GetGunGrid(InStockModel ism)
        {
            var sm = new List<InStockModel>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGunsGetAll");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[5];
                parameters[0] = new SqlParameter("@LocID", SqlDbType.Int) { Value = ism.LocationId };
                parameters[1] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = ism.TransTypeId };
                parameters[2] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = ism.ManufId };
                parameters[3] = new SqlParameter("@SubCatID", SqlDbType.Int) { Value = ism.SubCatId };
                parameters[4] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = ism.CustomerId };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return sm;

                var i0 = 0;
                var b0 = false;

                while (dr.Read())
                {
                    var i1 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                    var i2 = Int32.TryParse(dr["UnitsCAL"].ToString(), out i0) ? Convert.ToInt32(dr["UnitsCAL"]) : i0;
                    var i3 = Int32.TryParse(dr["UnitsWYO"].ToString(), out i0) ? Convert.ToInt32(dr["UnitsWYO"]) : i0;
                    var i4 = Int32.TryParse(dr["Purchases"].ToString(), out i0) ? Convert.ToInt32(dr["Purchases"]) : i0;
                    var i5 = Int32.TryParse(dr["CategoryID"].ToString(), out i0) ? Convert.ToInt32(dr["CategoryID"]) : i0;

                    var b1 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                    var b2 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;

                    var v2 = dr["ImageName"].ToString();
                    var v3 = dr["ManufName"].ToString();
                    var v4 = dr["MfgPartNumber"].ToString();
                    var v5 = dr["UpcCode"].ToString();
                    var v6 = dr["Description"].ToString();

                    var imgUrl = string.Empty;
                    var t = DateTime.Now.Ticks;

                    if (v2.Length > 0)
                    {
                        var cat = Enum.GetName(typeof(ItemCategories), i5);
                        imgUrl = string.Format("{0}/{1}/{2}/{3}/{4}?{5}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(iStk), cat, v2, t);
                    }


                    var m = new InStockModel(i1, i2, i3, i4, b1, b2, imgUrl, v3, v4, v5, v6);
                    sm.Add(m);
                }

            }

            return sm;
        }

        public List<InStockModel> GetAmmoGrid(int mid, int cid)
        {
            var l = new List<InStockModel>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetInStockAmmo");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = mid };
                parameters[1] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = cid };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return l;
                var x0 = 0;
                var b0 = false;

                while (dr.Read())
                {
                    var i1 = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                    var i2 = Int32.TryParse(dr["UnitsCA"].ToString(), out x0) ? Convert.ToInt32(dr["UnitsCA"]) : 0;
                    var i3 = Int32.TryParse(dr["UnitsWY"].ToString(), out x0) ? Convert.ToInt32(dr["UnitsWY"]) : 0;
                    var i4 = Int32.TryParse(dr["RoundsPerBox"].ToString(), out x0) ? Convert.ToInt32(dr["RoundsPerBox"]) : 0;
                    var i5 = Int32.TryParse(dr["Restocks"].ToString(), out x0) ? Convert.ToInt32(dr["Restocks"]) : 0;
                    var i6 = Int32.TryParse(dr["CategoryID"].ToString(), out x0) ? Convert.ToInt32(dr["CategoryID"]) : x0;

                    var v1 = dr["MfgPartNumber"].ToString();
                    var v2 = dr["UpcCode"].ToString();
                    var v3 = dr["ManufName"].ToString();
                    var v4 = dr["Model"].ToString();
                    var v5 = dr["Description"].ToString();
                    var v6 = dr["ImageName"].ToString();

                    var b1 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;
                    var b2 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;



                    var t = DateTime.Now.Ticks;
                    var imgUrl = string.Empty;
                    if (v6.Length > 0)
                    {
                        var cat = Enum.GetName(typeof(ItemCategories), i6);
                        imgUrl = string.Format("{0}/{1}/{2}/{3}/{4}?{5}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(iStk), cat, v6, t);
                    }

                    var im = new InStockModel(i1, i2, i3, i4, i5, v1, v2, v3, v4, v5, imgUrl, b1, b2);

                    l.Add(im);
                }
            }

            return l;
        }

        public List<InStockModel> GetMerchGrid(int mid, int sct)
        {
            var l = new List<InStockModel>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetAllMerchandise");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = mid };
                parameters[1] = new SqlParameter("@SubCatID", SqlDbType.Int) { Value = sct };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return l;
                var x0 = 0;
                var b0 = false;

                while (dr.Read())
                {
                    var i1 = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : x0;
                    var i2 = Int32.TryParse(dr["UnitsCA"].ToString(), out x0) ? Convert.ToInt32(dr["UnitsCA"]) : x0;
                    var i3 = Int32.TryParse(dr["UnitsWY"].ToString(), out x0) ? Convert.ToInt32(dr["UnitsWY"]) : x0;
                    var i4 = Int32.TryParse(dr["Restocks"].ToString(), out x0) ? Convert.ToInt32(dr["Restocks"]) : x0;
                    var i5 = Int32.TryParse(dr["CategoryID"].ToString(), out x0) ? Convert.ToInt32(dr["CategoryID"]) : x0;

                    var v1 = dr["ImageName"].ToString();
                    var v2 = dr["MfgPartNumber"].ToString();
                    var v3 = dr["UpcCode"].ToString();
                    var v4 = dr["ManufName"].ToString();
                    var v5 = dr["SubCategoryName"].ToString();
                    var v6 = dr["Description"].ToString();

                    var b1 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;
                    var b2 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;

                    var imgUrl = string.Empty;
                    var t = DateTime.Now.Ticks;

                    if (v1.Length > 0)
                    {
                        var cat = Enum.GetName(typeof(ItemCategories), i5);
                        imgUrl = string.Format("{0}/{1}/{2}/{3}/{4}?{5}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(iStk), cat, v1, t);
                    }

                    var im = new InStockModel(i1, i2, i3, i4, imgUrl, v2, v3, v4, v5, v6, b1, b2);
                    l.Add(im);
                }
            }

            return l;
        }


        public TagModel AddGunInventory(AddToBookModel m)
        {

            var tm = new TagModel();

            var dt0 = DateTime.MinValue;

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGunCreate");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[61];
                parameters[0] = new SqlParameter("@MasterID", SqlDbType.Int) { Value = m.Gun.MasterId };
                parameters[1] = new SqlParameter("@InStockID", SqlDbType.Int) { Value = m.Gun.InStockId };
                parameters[2] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = m.BoundBook.LocationId };
                parameters[3] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.Gun.ManufId };
                parameters[4] = new SqlParameter("@SubCatId", SqlDbType.Int) { Value = m.Gun.GunTypeId };
                parameters[5] = new SqlParameter("@ActionID", SqlDbType.Int) { Value = m.Gun.ActionId};
                parameters[6] = new SqlParameter("@FinishID", SqlDbType.Int) { Value = m.Gun.FinishId };
                parameters[7] = new SqlParameter("@CapacityInt", SqlDbType.Int) { Value = m.Gun.CapacityInt };
                parameters[8] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = m.Gun.CaliberId };
                parameters[9] = new SqlParameter("@WeightLb", SqlDbType.Int) { Value = m.Gun.WeightLb };
                parameters[10] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = m.BoundBook.TransTypeId };
                parameters[11] = new SqlParameter("@CondID", SqlDbType.Int) { Value = m.Gun.ConditionId };
                parameters[12] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = m.CustomerId };
                parameters[13] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = m.SupplierId };
                parameters[14] = new SqlParameter("@UnitsCAL", SqlDbType.Int) { Value = m.Accounting.UnitsCal };
                parameters[15] = new SqlParameter("@UnitsWYO", SqlDbType.Int) { Value = m.Accounting.UnitsWyo };
                parameters[16] = new SqlParameter("@NotForSaleCAL", SqlDbType.Int) { Value = m.Accounting.NotForSaleCal };
                parameters[17] = new SqlParameter("@NotForSaleWYO", SqlDbType.Int) { Value = m.Accounting.NotForSaleWyo };
                parameters[18] = new SqlParameter("@LockMakeID", SqlDbType.Int) { Value = m.Gun.LockMakeId };
                parameters[19] = new SqlParameter("@LockModelID", SqlDbType.Int) { Value = m.Gun.LockModelId };
                parameters[20] = new SqlParameter("@CustomerPrice", SqlDbType.Decimal) { Value = m.Accounting.CustPricePaid };
                parameters[21] = new SqlParameter("@SellerCollTaxAmt", SqlDbType.Decimal) { Value = m.Accounting.SellerTaxAmount };
                parameters[22] = new SqlParameter("@AskingPrice", SqlDbType.Decimal) { Value = m.Accounting.AskingPrice };
                parameters[23] = new SqlParameter("@SalePrice", SqlDbType.Decimal) { Value = m.Accounting.SalePrice };
                parameters[24] = new SqlParameter("@MSRP", SqlDbType.Decimal) { Value = m.Accounting.Msrp };
                parameters[25] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = m.Accounting.FreightCost };
                parameters[26] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = m.Accounting.ItemCost };
                parameters[27] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = m.Accounting.ItemFees };
                parameters[28] = new SqlParameter("@BarrelDec", SqlDbType.Decimal) { Value = m.Gun.BarrelDec };
                parameters[29] = new SqlParameter("@ChamberDec", SqlDbType.Decimal) { Value = m.Gun.ChamberDec };
                parameters[30] = new SqlParameter("@OverallDec", SqlDbType.Decimal) { Value = m.Gun.OverallDec };
                parameters[31] = new SqlParameter("@WeightOz", SqlDbType.Decimal) { Value = m.Gun.WeightOz };
                parameters[32] = new SqlParameter("@ModelName", SqlDbType.VarChar) { Value = m.Gun.ModelName };
                parameters[33] = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = m.Gun.UpcCode };
                parameters[34] = new SqlParameter("@WebSearchUpc", SqlDbType.VarChar) { Value = m.Gun.WebSearchUpc };
                parameters[35] = new SqlParameter("@MfgPartNumber", SqlDbType.VarChar) { Value = m.Gun.MfgPartNumber };
                parameters[36] = new SqlParameter("@ItemDesc", SqlDbType.VarChar) { Value = m.Gun.Description };
                parameters[37] = new SqlParameter("@LongDesc", SqlDbType.VarChar) { Value = m.Gun.LongDescription };
                parameters[38] = new SqlParameter("@SerialNumber", SqlDbType.VarChar) { Value = m.Gun.SerialNumber };
                parameters[39] = new SqlParameter("@SvcCustName", SqlDbType.VarChar) { Value = m.Accounting.SvcCustName == "" ? (object)DBNull.Value : m.Accounting.SvcCustName };
                parameters[40] = new SqlParameter("@OldSku", SqlDbType.VarChar) { Value = m.Gun.OldSku == "" ? (object)DBNull.Value : m.Gun.OldSku };
                parameters[41] = new SqlParameter("@SellerCollTax", SqlDbType.Bit) { Value = m.Accounting.SellerCollectedTax };
                parameters[42] = new SqlParameter("@CurrentModel", SqlDbType.Bit) { Value = m.Gun.IsCurModel };
                parameters[43] = new SqlParameter("@Hide", SqlDbType.Bit) { Value = m.Gun.IsHidden };
                parameters[44] = new SqlParameter("@HideCA", SqlDbType.Bit) { Value = m.Compliance.CaHide };
                parameters[45] = new SqlParameter("@Active", SqlDbType.Bit) { Value = m.Gun.IsActive };
                parameters[46] = new SqlParameter("@Verified", SqlDbType.Bit) { Value = m.Gun.IsVerified };
                parameters[47] = new SqlParameter("@IsOnWeb", SqlDbType.Bit) { Value = m.Gun.IsOnWeb };
                parameters[48] = new SqlParameter("@IsUsed", SqlDbType.Bit) { Value = m.Gun.IsUsed };
                parameters[49] = new SqlParameter("@OrigBox", SqlDbType.Bit) { Value = m.Gun.OrigBox };
                parameters[50] = new SqlParameter("@OrigPapers", SqlDbType.Bit) { Value = m.Gun.OrigPaperwork };
                parameters[51] = new SqlParameter("@CaOkay", SqlDbType.Bit) { Value = m.Compliance.CaOkay };
                parameters[52] = new SqlParameter("@RostOK", SqlDbType.Bit) { Value = m.Compliance.CaRosterOk };
                parameters[53] = new SqlParameter("@SglAtnOK", SqlDbType.Bit) { Value = m.Compliance.CaSglActnOk };
                parameters[54] = new SqlParameter("@CurioOK", SqlDbType.Bit) { Value = m.Compliance.CaCurioOk };
                parameters[55] = new SqlParameter("@SglShtOK", SqlDbType.Bit) { Value = m.Compliance.CaSglShotOk };
                parameters[56] = new SqlParameter("@PptOK", SqlDbType.Bit) { Value = m.Compliance.CaPptOk };
                parameters[57] = new SqlParameter("@IsActualPPT", SqlDbType.Bit) { Value = m.Compliance.IsActualPpt };
                parameters[58] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = m.BoundBook.IsSale };
                parameters[59] = new SqlParameter("@IsOldSku", SqlDbType.Bit) { Value = m.Gun.IsOldSku };
                parameters[60] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = m.BoundBook.AcqDate == dt0 ? (object)DBNull.Value : m.BoundBook.AcqDate };

                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return tm;


                dr.Read();
                var x0 = 0;
                var b0 = false;
                double d0 = 0.00;


                var b1 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var i1 = Int32.TryParse(dr["InStockID"].ToString(), out x0) ? Convert.ToInt32(dr["InStockID"]) : 0;
                var i2 = Int32.TryParse(dr["TagCap"].ToString(), out x0) ? Convert.ToInt32(dr["TagCap"]) : 0;
                var i3 = Int32.TryParse(dr["ItemBasisID"].ToString(), out x0) ? Convert.ToInt32(dr["ItemBasisID"]) : 0;
                var d1 = Double.TryParse(dr["TagPrc"].ToString(), out d0) ? Convert.ToDouble(dr["TagPrc"]) : 0;
                var d2 = Double.TryParse(dr["TagBrl"].ToString(), out d0) ? Convert.ToDouble(dr["TagBrl"]) : 0;

                var v1 = dr["TagMFG"].ToString();
                var v2 = dr["TagTyp"].ToString();
                var v3 = dr["TagCal"].ToString();
                var v4 = dr["TagCnd"].ToString();
                var v5 = dr["TagSvc"].ToString();
                var v6 = dr["TagSku"].ToString();
                var v7 = dr["TagMdl"].ToString();
                var v8 = dr["TagMPN"].ToString();
                var v9 = dr["TagSer"].ToString();
                var v10 = dr["TagNam"].ToString();

                tm = new TagModel(i1, i3, i2, b1, d1, d2, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10); 
            }

            return tm;         
        }


        public GunModel GetGunSpecsWeb(int mst, int isi, bool onw)
        {
            var g = new GunModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetGunWebSpecs");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[3];
                parameters[0] = new SqlParameter("@MasterID", SqlDbType.Int) { Value = mst };
                parameters[1] = new SqlParameter("@InStockID", SqlDbType.Int) { Value = isi };
                parameters[2] = new SqlParameter("@OnWeb", SqlDbType.Bit) { Value = onw };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return g;

                dr.Read();
                
                double x0 = 0.00;
                var i0 = 0;
                var d0 = false;

                var mid = Int32.TryParse(dr["ManufID"].ToString(), out i0) ? Convert.ToInt32(dr["ManufID"]) : 0;
                var gtp = Int32.TryParse(dr["GunTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["GunTypeID"]) : 0;
                var cal = Int32.TryParse(dr["CaliberID"].ToString(), out i0) ? Convert.ToInt32(dr["CaliberID"]) : 0;
                var iDs = Int32.TryParse(dr["ImageDist"].ToString(), out i0) ? Convert.ToInt32(dr["ImageDist"]) : 0;
                var wlb =  Int32.TryParse(dr["WeightLb"].ToString(), out i0) ? Convert.ToInt32(dr["WeightLb"]) : 0;
                var fin =  Int32.TryParse(dr["FinishID"].ToString(), out i0) ? Convert.ToInt32(dr["FinishID"]) : 0;
                var cap =  Int32.TryParse(dr["CapacityInt"].ToString(), out i0) ? Convert.ToInt32(dr["CapacityInt"]) : 0;
                var cnd =  Int32.TryParse(dr["ConditionID"].ToString(), out i0) ? Convert.ToInt32(dr["ConditionID"]) : 0;
                var atn = Int32.TryParse(dr["ActionID"].ToString(), out i0) ? Convert.ToInt32(dr["ActionID"]) : 0;
                var pid = Int32.TryParse(dr["PicRowID"].ToString(), out i0) ? Convert.ToInt32(dr["PicRowID"]) : 0;

                var brl = Double.TryParse(dr["BarrelDec"].ToString(), out x0) ? Convert.ToDouble(dr["BarrelDec"]) : 0.000;
                var chm = Double.TryParse(dr["ChamberDec"].ToString(), out x0) ? Convert.ToDouble(dr["ChamberDec"]) : 0.000;
                var ovl = Double.TryParse(dr["OverallDec"].ToString(), out x0) ? Convert.ToDouble(dr["OverallDec"]) : 0.000;
                var woz = Double.TryParse(dr["WeightOz"].ToString(), out x0) ? Convert.ToDouble(dr["WeightOz"]) : 0.000;

                var brlIn = Convert.ToInt32(Math.Truncate(brl));
                var chmIn = Convert.ToInt32(Math.Truncate(chm));
                var ovlIn = Convert.ToInt32(Math.Truncate(ovl));

                //double brlDec = brl - brlIn;
                //double chmDec = chm - chmIn;
                //double ovlDec = ovl - ovlIn;

                var ipd = Boolean.TryParse(dr["InProduction"].ToString(), out d0) ? Convert.ToBoolean(dr["InProduction"]) : d0;
                var obx = Boolean.TryParse(dr["OriginalBox"].ToString(), out d0) ? Convert.ToBoolean(dr["OriginalBox"]) : d0;
                var opw = Boolean.TryParse(dr["OriginalPapers"].ToString(), out d0) ? Convert.ToBoolean(dr["OriginalPapers"]) : d0;
                var usd = Boolean.TryParse(dr["IsUsed"].ToString(), out d0) ? Convert.ToBoolean(dr["IsUsed"]) : d0;
                var hid = Boolean.TryParse(dr["Hide"].ToString(), out d0) ? Convert.ToBoolean(dr["Hide"]) : d0;
                var act = Boolean.TryParse(dr["Active"].ToString(), out d0) ? Convert.ToBoolean(dr["Active"]) : d0;
                var ver = Boolean.TryParse(dr["Verified"].ToString(), out d0) ? Convert.ToBoolean(dr["Verified"]) : d0;
                var iwb = Boolean.TryParse(dr["IsWebBased"].ToString(), out d0) ? Convert.ToBoolean(dr["IsWebBased"]) : d0;

                var caHd = Boolean.TryParse(dr["HideCA"].ToString(), out d0) ? Convert.ToBoolean(dr["HideCA"]) : d0;
                var caOk = Boolean.TryParse(dr["CaOkay"].ToString(), out d0) ? Convert.ToBoolean(dr["CaOkay"]) : d0;
                var caRt = Boolean.TryParse(dr["CaRosterOk"].ToString(), out d0) ? Convert.ToBoolean(dr["CaRosterOk"]) : d0;
                var caPt = Boolean.TryParse(dr["CaPptOk"].ToString(), out d0) ? Convert.ToBoolean(dr["CaPptOk"]) : d0;
                var caCr = Boolean.TryParse(dr["CaCurioOk"].ToString(), out d0) ? Convert.ToBoolean(dr["CaCurioOk"]) : d0;
                var caSa = Boolean.TryParse(dr["CaSglActnOk"].ToString(), out d0) ? Convert.ToBoolean(dr["CaSglActnOk"]) : d0;
                var caSs = Boolean.TryParse(dr["CaSglShotOk"].ToString(), out d0) ? Convert.ToBoolean(dr["CaSglShotOk"]) : d0;

                var model = dr["ModelName"].ToString();
                var upc = dr["UpcCode"].ToString();
                var spc = dr["SearchUpc"].ToString();
                var desc = dr["Description"].ToString();
                var mpn = dr["MfgPartNumber"].ToString();
                var longDesc = dr["LongDescription"].ToString();
                var inm = dr["ImageName"].ToString();
                var img = dr["ImageURL"].ToString();
                var dst = dr["DistCode"].ToString();



                var imgUrl = string.Empty;
                var dir = ConfigurationHelper.GetPropertyValue("application", "m10");
                //if (img.Length > 0) { imgUrl = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(dir), img); } //SINGLE WEB PIC

 
                var img1 = string.Empty;
                var ig1 = dr["Image1"].ToString();
                var t = DateTime.Now.Ticks;

                switch (iDs)
                {
                    case 25:
                        ig1 = dr["Image1"].ToString();
                        img1 = ig1.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthGn), ig1, t) : String.Empty;
                        break;
                    case 99:
                        ig1 = inm;
                        img1 = inm.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthGn), inm, t) : String.Empty;
                        break;
                    default:
                        ig1 = inm;
                        img1 = img.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(dir), img, t) : String.Empty;
                        break;
                }


                var ig2 = dr["Image2"].ToString();
                var ig3 = dr["Image3"].ToString();
                var ig4 = dr["Image4"].ToString();
                var ig5 = dr["Image5"].ToString();
                var ig6 = dr["Image6"].ToString();


                //var img1 = ig1.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthMd), ig1, t) : String.Empty;
                var img2 = ig2.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthMd), ig2, t) : String.Empty;
                var img3 = ig3.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthMd), ig3, t) : String.Empty;
                var img4 = ig4.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthMd), ig4, t) : String.Empty;
                var img5 = ig5.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthMd), ig5, t) : String.Empty;
                var img6 = ig6.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthMd), ig6, t) : String.Empty;

                var im = new ImageModel(pid, img1, img2, img3, img4, img5, img6, ig1, ig2, ig3, ig4, ig5, ig6, dst);
                var ca = new CaRestrictModel(caHd, caOk, caRt, caPt, caCr, caSa, caSs);
                g = new GunModel(model, upc, spc, desc, mpn, longDesc, imgUrl, isi, mid, gtp, cal, iDs, wlb, atn, fin, cap, cnd, brlIn, chmIn, ovlIn, brl, chm, ovl, woz, ipd, obx, opw, usd, hid, act, ver, iwb,
                     im, ca);


            }

            return g;
        }

        //public AmmoModel GetInStockGunById(int id)
        //{
        //    var am = new AmmoModel();

        //    using (var conn = new SqlConnection(AdminSqlConnection))
        //    {
        //        var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAmmoGetItem");
        //        var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
        //        conn.Open();

        //        var param = new SqlParameter("@CBID", SqlDbType.Int) { Value = id };
        //        cmd.Parameters.Add(param);

        //        var dr = cmd.ExecuteReader();
        //        if (!dr.HasRows) return am;
        //        var d0 = 0.00;
        //        var i0 = 0;
        //        var b0 = false;
        //        var dt0 = DateTime.MinValue;

        //        dr.Read();
        //        var dir = ConfigurationHelper.GetPropertyValue("application", "m10");

        //        var i1 = Int32.TryParse(dr["CaliberID"].ToString(), out i0) ? Convert.ToInt32(dr["CaliberID"]) : i0;
        //        var i2 = Int32.TryParse(dr["ManufID"].ToString(), out i0) ? Convert.ToInt32(dr["ManufID"]) : i0;
        //        var i3 = Int32.TryParse(dr["BulletTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["BulletTypeID"]) : i0;
        //        var i4 = Int32.TryParse(dr["GrainWeight"].ToString(), out i0) ? Convert.ToInt32(dr["GrainWeight"]) : i0;
        //        var i5 = Int32.TryParse(dr["RoundsPerBox"].ToString(), out i0) ? Convert.ToInt32(dr["RoundsPerBox"]) : i0;
        //        var i6 = Int32.TryParse(dr["SubCategoryID"].ToString(), out i0) ? Convert.ToInt32(dr["SubCategoryID"]) : i0;
        //        var i7 = Int32.TryParse(dr["ConditionID"].ToString(), out i0) ? Convert.ToInt32(dr["ConditionID"]) : i0;
        //        var i8 = Int32.TryParse(dr["UnitsCAL"].ToString(), out i0) ? Convert.ToInt32(dr["UnitsCAL"]) : i0;
        //        var i9 = Int32.TryParse(dr["TransTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["TransTypeID"]) : i0;
        //        var i10 = Int32.TryParse(dr["UnitsWYO"].ToString(), out i0) ? Convert.ToInt32(dr["UnitsWYO"]) : i0;


        //        var d1 = Double.TryParse(dr["AskingPrice"].ToString(), out d0) ? Convert.ToDouble(dr["AskingPrice"]) : d0;
        //        var d2 = Double.TryParse(dr["Chamber"].ToString(), out d0) ? Convert.ToDouble(dr["Chamber"]) : d0;
        //        var d3 = Double.TryParse(dr["ShotSizeSlugWt"].ToString(), out d0) ? Convert.ToDouble(dr["ShotSizeSlugWt"]) : d0;
        //        var d4 = Double.TryParse(dr["SellerTaxCollAmt"].ToString(), out d0) ? Convert.ToDouble(dr["SellerTaxCollAmt"]) : d0;
        //        var d5 = Double.TryParse(dr["UnitCost"].ToString(), out d0) ? Convert.ToDouble(dr["UnitCost"]) : d0;
        //        var d6 = Double.TryParse(dr["Freight"].ToString(), out d0) ? Convert.ToDouble(dr["Freight"]) : d0;
        //        var d7 = Double.TryParse(dr["Fees"].ToString(), out d0) ? Convert.ToDouble(dr["Fees"]) : d0;

        //        var b1 = Boolean.TryParse(dr["IsSlug"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSlug"]) : b0;
        //        var b2 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;
        //        var b3 = Boolean.TryParse(dr["SellerCollectedTax"].ToString(), out b0) ? Convert.ToBoolean(dr["SellerCollectedTax"]) : b0;
        //        var b4 = Boolean.TryParse(dr["IsDisposed"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDisposed"]) : b0;

        //        var v1 = dr["UpcCode"].ToString();
        //        var v2 = dr["ImageUrl"].ToString();
        //        var v3 = dr["ItemDesc"].ToString();
        //        var v4 = dr["MfgPartNumber"].ToString();
        //        var v5 = dr["TransID"].ToString();
        //        var v16 = dr["SearchUpc"].ToString();

        //        var v6 = dr["AcqCustName"].ToString();
        //        var v7 = dr["AcqCustType"].ToString();
        //        var v8 = dr["AcqFFLNumber"].ToString();
        //        var v9 = dr["AcqCaAvNumber"].ToString();
        //        var v10 = dr["AcqCustAddress"].ToString();
        //        var v11 = dr["DispCustName"].ToString();
        //        var v12 = dr["DispCustType"].ToString();
        //        var v13 = dr["DispFFLNumber"].ToString();
        //        var v14 = dr["DispCaAvNumber"].ToString();
        //        var v15 = dr["DispCustAddress"].ToString();


        //        var dt1 = DateTime.TryParse(dr["AcqDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["AcqDate"].ToString()) : dt0;
        //        var dt2 = DateTime.TryParse(dr["DispDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["DispDate"]) : dt0;
        //        var aqd = dt1 == dt0 ? string.Empty : dt1.ToShortDateString();
        //        var dpd = dt2 == dt0 ? string.Empty : dt2.ToShortDateString();

        //        var t = DateTime.Now.Ticks;
        //        var imgUrl = string.Empty;
        //        if (v2.Length > 0) { imgUrl = string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthAm), v2, t); }


        //        var atm = new AcctModel(d5, d6, d7, d4, b3, d1, i8, i10);

        //        var bm = new BoundBookModel(i9, v5, DecryptIt(v6), DecryptIt(v7), DecryptIt(v8), DecryptIt(v9), DecryptIt(v10), DecryptIt(v11), DecryptIt(v12), DecryptIt(v13), DecryptIt(v14), DecryptIt(v15), aqd, dpd, b4);

        //        am = new AmmoModel(id, i1, i2, i3, i4, i5, i6, i7, b1, b2, v1, v16, imgUrl, v3, v4, v5, d2, d3, atm, bm);

        //    }

        //    return am;
        //}


        public List<GunModel> CheckForExistingGun(GunModel g)
        {
            var l = new List<GunModel>();
            var b0 = false;

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCheckForExistingGun");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[6];
                parameters[0] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = g.ManufId };
                parameters[1] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = g.GunTypeId };
                parameters[2] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = g.CaliberId };
                parameters[3] = new SqlParameter("@ActionID", SqlDbType.Int) { Value = g.ActionId };
                parameters[4] = new SqlParameter("@CaOkay", SqlDbType.Int) { Value = g.CaOkId };
                parameters[5] = new SqlParameter("@SearchTxt", SqlDbType.VarChar) { Value = g.SearchText };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return l;

                var h = GetHostUrl();
                var dir = ConfigurationHelper.GetPropertyValue("application", "m10");
                var ddr = DecryptIt(dir);
                var dbp = DecryptIt(BPathDir);

                while (dr.Read())
                {
                    var x0 = 0;
                    var isi = Int32.TryParse(dr["InStockID"].ToString(), out x0) ? Convert.ToInt32(dr["InStockID"]) : 0;
                    var id = Int32.TryParse(dr["MasterID"].ToString(), out x0) ? Convert.ToInt32(dr["MasterID"]) : 0;
                    var img = dr["ImageName"].ToString();
                    var gunDesc = dr["GunDesc"].ToString();
                    var manuf = Int32.TryParse(dr["ManufID"].ToString(), out x0) ? Convert.ToInt32(dr["ManufID"]) : 0;
                    var model = dr["ModelName"].ToString();
                    var caliber = Int32.TryParse(dr["CaliberID"].ToString(), out x0) ? Convert.ToInt32(dr["CaliberID"]) : 0;
                    var gunType = Int32.TryParse(dr["GunTypeID"].ToString(), out x0) ? Convert.ToInt32(dr["GunTypeID"]) : 0;
                    var imgDist = dr["DistributorCode"].ToString();
                    var b1 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;


                    var imgUrl = string.Empty;
                    if (img.Length > 0)
                    {

                        if (imgDist == "STK") //PULL FROM OFFLINE IN-STOCK INVENTORY
                        {
                            var t = DateTime.Now.Ticks;
                            imgUrl = string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthGn), img, t);
                        }
                        else //PULL FROM WEB INVENTORY
                        {
                            imgUrl = string.Format("{0}/{1}/{2}/{3}/L/{4}", h, dbp, ddr, imgDist, img);
                        }

                        
                    }

                    var gun = new GunModel(id, isi, imgUrl, gunDesc, manuf, model, caliber, gunType, b1);
                    l.Add(gun);
                }
            }

            return l;
        }

        public List<GunModel> GetSvcGunsFromInquiry(Int64 inqId)
        {
            var l = new List<GunModel>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var bp = ConfigurationHelper.GetPropertyValue("application", "PathAddGuns");
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetSvcGunsByInquiry");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@InquiryID", SqlDbType.BigInt) { Value = inqId};
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return l;

                var h = GetHostUrl();
                var dir = DecryptIt(BInqDir);

                while (dr.Read())
                {
                    var x0 = 0;
                    var d0 = 0.00;
                    var xB = false;

                    var id = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                    var gunDesc = dr["GunDesc"].ToString();
                    var img = dr["Thumb"].ToString();
                    //var baseImg = thumb.Length > 0 ? String.Format("{0}/SvcGunPics/{1}", GetHostUrl(), thumb) : string.Empty;
                    var model = dr["ModelName"].ToString();
                    var serial = dr["SerialNumber"].ToString();

 
                    var mfgId = Int32.TryParse(dr["ManufID"].ToString(), out x0) ? Convert.ToInt32(dr["ManufID"]) : 0;
                    var calId = Int32.TryParse(dr["CaliberID"].ToString(), out x0) ? Convert.ToInt32(dr["CaliberID"]) : 0;
                    var finId = Int32.TryParse(dr["FinishID"].ToString(), out x0) ? Convert.ToInt32(dr["FinishID"]) : 0;
                    var condId = Int32.TryParse(dr["ConditionID"].ToString(), out x0) ? Convert.ToInt32(dr["ConditionID"]) : 0;
                    var gunTypeId = Int32.TryParse(dr["GunTypeID"].ToString(), out x0) ? Convert.ToInt32(dr["GunTypeID"]) : 0;
                    var barIn = Int32.TryParse(dr["BarrelIn"].ToString(), out x0) ? Convert.ToInt32(dr["BarrelIn"]) : 0;

                    var origBx = Boolean.TryParse(dr["OriginalBox"].ToString(), out xB) ? Convert.ToBoolean(dr["OriginalBox"]) : xB;
                    var origPw = Boolean.TryParse(dr["OriginalPapers"].ToString(), out xB) ? Convert.ToBoolean(dr["OriginalPapers"]) : xB;

                    var barDec = Double.TryParse(dr["BarrelDec"].ToString(), out d0) ? Convert.ToDouble(dr["BarrelDec"]) : 0;

                    var imgUrl = string.Empty;
                    if (img.Length > 0)
                    {
                        imgUrl = string.Format("{0}/{1}/L/{2}.jpg", h, dir, img); 
                    }

                    var gun = new GunModel(id, mfgId, calId, finId, condId, gunTypeId, barIn, origBx, origPw, barDec, gunDesc, imgUrl, model, serial);

                    l.Add(gun);
                }



            }

            return l;
        }

        public BookEntryModel  GetBookDataFromService(Int64 inqId, int gunId)
        {
            var bem = new BookEntryModel();
            var cbm = new CustomerBaseModel();
            var flm = new FflLicenseModel();
            var gunMod = new GunModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetBookDataFromSvc");
                var bp = ConfigurationHelper.GetPropertyValue("application", "PathAddGuns");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@InquiryNumber", SqlDbType.BigInt) { Value = inqId };
                parameters[1] = new SqlParameter("@GunID", SqlDbType.Int) { Value = gunId };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows){ return bem; }

                dr.Read();

                var xB = false;
                var x0 = 0;
                double d0 = 0.00;

                var model = dr["ModelName"].ToString();
                var serial = dr["SerialNumber"].ToString();
                var mfgId = Int32.TryParse(dr["ManufID"].ToString(), out x0) ? Convert.ToInt32(dr["ManufID"]) : 0;
                var gunTypeId = Int32.TryParse(dr["GunTypeID"].ToString(), out x0) ? Convert.ToInt32(dr["GunTypeID"]) : 0;
                var calId = Int32.TryParse(dr["CaliberID"].ToString(), out x0) ? Convert.ToInt32(dr["CaliberID"]) : 0;
                var finId = Int32.TryParse(dr["FinishID"].ToString(), out x0) ? Convert.ToInt32(dr["FinishID"]) : 0;
                var condId = Int32.TryParse(dr["ConditionID"].ToString(), out x0) ? Convert.ToInt32(dr["ConditionID"]) : 0;
                var barIn = Int32.TryParse(dr["BarrelIn"].ToString(), out x0) ? Convert.ToInt32(dr["BarrelIn"]) : 0;
                var origBx = Boolean.TryParse(dr["OriginalBox"].ToString(), out xB) ? Convert.ToBoolean(dr["OriginalBox"]) : xB;
                var origPw = Boolean.TryParse(dr["OriginalPapers"].ToString(), out xB) ? Convert.ToBoolean(dr["OriginalPapers"]) : xB;
                var barDec = Double.TryParse(dr["BarrelDec"].ToString(), out d0) ? Convert.ToDouble(dr["BarrelDec"]) : 0.000;


                var ig1 = string.Empty;
                var ig2 = string.Empty;
                var ig3 = string.Empty;
                var ig4 = string.Empty;
                var ig5 = string.Empty;
                var ig6 = string.Empty;

                var v1 = dr["Img1"].ToString();
                var v2 = dr["Img2"].ToString();
                var v3 = dr["Img3"].ToString();
                var v4 = dr["Img4"].ToString();
                var v5 = dr["Img5"].ToString();
                var v6 = dr["Img6"].ToString();

                if (v1.Length > 0) { ig1 = string.Format("{0}/{1}/L/{2}.jpg", GetHostUrl(), DecryptIt(BInqDir), v1); }
                if (v2.Length > 0) { ig2 = string.Format("{0}/{1}/L/{2}.jpg", GetHostUrl(), DecryptIt(BInqDir), v2); }
                if (v3.Length > 0) { ig3 = string.Format("{0}/{1}/L/{2}.jpg", GetHostUrl(), DecryptIt(BInqDir), v3); }
                if (v4.Length > 0) { ig4 = string.Format("{0}/{1}/L/{2}.jpg", GetHostUrl(), DecryptIt(BInqDir), v4); }
                if (v5.Length > 0) { ig5 = string.Format("{0}/{1}/L/{2}.jpg", GetHostUrl(), DecryptIt(BInqDir), v5); }
                if (v6.Length > 0) { ig6 = string.Format("{0}/{1}/L/{2}.jpg", GetHostUrl(), DecryptIt(BInqDir), v6); }

                gunMod = new GunModel(model, serial, mfgId, gunTypeId, calId, finId, condId, barIn, origBx, origPw, barDec, ig1, ig2, ig3, ig4, ig5, ig6);


                dr.NextResult();

                if (dr.HasRows)
                {
                    dr.Read();

                    var serviceTypeId = Int32.TryParse(dr["ServiceTypeId"].ToString(), out x0) ? Convert.ToInt32(dr["ServiceTypeId"]) : 0;
                    var sellerTypeId = Int32.TryParse(dr["SellerTypeId"].ToString(), out x0) ? Convert.ToInt32(dr["SellerTypeId"]) : 0;
                    var pickUpLocId = Int32.TryParse(dr["PickupLocID"].ToString(), out x0) ? Convert.ToInt32(dr["PickupLocID"]) : 0;
                    var bookName = dr["CustName"].ToString();

                    bem.BookName = bookName;
                    bem.SellerTypeId = sellerTypeId;
                    bem.PickupLocId = pickUpLocId;


                    var cn = String.Empty;
                    var fn = String.Empty;
                    var ln = String.Empty;
                    var ad = String.Empty;
                    var ct = String.Empty;
                    var st = 0;
                    var zp = 0;
                    var ze = 0;

                    if (serviceTypeId == 2) // FFL Transfer
                    {
                        dr.NextResult();
                        if (dr.HasRows)
                        {
                            dr.Read();

                            bem.MenuSellerId = Int32.TryParse(dr["MenuSellerID"].ToString(), out x0) ? Convert.ToInt32(dr["MenuSellerID"]) : 0;

                            switch (sellerTypeId)
                            {
                                case 1:
                                    st = Int32.TryParse(dr["FFLStateID"].ToString(), out x0) ? Convert.ToInt32(dr["FFLStateID"]) : 0;
                                    fn = dr["FFLDesc"].ToString();
                                    var tradeName = dr["TradeName"].ToString();
                                    var fullLicNum = dr["FullLicNumber"].ToString();
                                    var licValid = Boolean.TryParse(dr["LicValid"].ToString(), out xB) ? Convert.ToBoolean(dr["LicValid"]) : false;
                                    var licOnFile = Boolean.TryParse(dr["LicOnFile"].ToString(), out xB) ? Convert.ToBoolean(dr["LicOnFile"]) : false;
                                    var licExpDate = String.Format("{0:MM/dd/yyyy}", dr["LicExpires"]);
                                    flm = new FflLicenseModel(st, fn, tradeName, fullLicNum, licValid, licOnFile, licExpDate);
                                    break;
                                case 2:
                                    cn = dr["CompanyName"].ToString();
                                    fn = dr["FirstName"].ToString();
                                    ln = dr["LastName"].ToString();
                                    ad = dr["UserAddress"].ToString();
                                    ct = dr["City"].ToString();
                                    st = Int32.TryParse(dr["StateID"].ToString(), out x0) ? Convert.ToInt32(dr["StateID"]) : 0;
                                    zp = Int32.TryParse(dr["ZipCode"].ToString(), out x0) ? Convert.ToInt32(dr["ZipCode"]) : 0;
                                    ze = Int32.TryParse(dr["ZipExt"].ToString(), out x0) ? Convert.ToInt32(dr["ZipExt"]) : 0; 

                                    cbm = new CustomerBaseModel(cn, fn, ln, ad, ct, st, zp, ze);
                                    break;
                                case 3: 
                                    fn = dr["FirstName"].ToString();
                                    ln = dr["LastName"].ToString();
                                    ad = dr["UserAddress"].ToString();
                                    ct = dr["City"].ToString();
                                    st = Int32.TryParse(dr["StateID"].ToString(), out x0) ? Convert.ToInt32(dr["StateID"]) : 0;
                                    zp = Int32.TryParse(dr["ZipCode"].ToString(), out x0) ? Convert.ToInt32(dr["ZipCode"]) : 0;
                                    ze = Int32.TryParse(dr["ZipExt"].ToString(), out x0) ? Convert.ToInt32(dr["ZipExt"]) : 0; 

                                    cbm = new CustomerBaseModel(fn, ln, ad, ct, st, zp, ze);
                                    break;
                            }
                        }

                    }
                    else
                    {
                        dr.NextResult();
                        if (dr.HasRows)
                        {
                            dr.Read();

                            bem.MenuSellerId = Int32.TryParse(dr["MenuSellerID"].ToString(), out x0) ? Convert.ToInt32(dr["MenuSellerID"]) : 0;

                            fn = dr["FirstName"].ToString();
                            ln = dr["LastName"].ToString();
                            ad = dr["UserAddress"].ToString();
                            ct = dr["City"].ToString();
                            st = Int32.TryParse(dr["StateID"].ToString(), out x0) ? Convert.ToInt32(dr["StateID"]) : 0;
                            zp = Int32.TryParse(dr["ZipCode"].ToString(), out x0) ? Convert.ToInt32(dr["ZipCode"]) : 0;
                            ze = Int32.TryParse(dr["ZipExt"].ToString(), out x0) ? Convert.ToInt32(dr["ZipExt"]) : 0; 

                            cbm = new CustomerBaseModel(fn, ln, ad, ct, st, zp, ze);
                        }
                    }
                }

                bem.Customer = cbm;
                bem.Gun = gunMod;
                bem.Ffl = flm;
            }

            return bem;
        }

        public AddMenuItemModel AddGunLockMfg(string mfgName)
        {
            var mi = new AddMenuItemModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAddGunLockManuf");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@NewMfg", SqlDbType.VarChar) { Value = mfgName };
                cmd.Parameters.Add(param); 

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return mi;

                dr.Read();

                var x0 = 0;
                var ct = Int32.TryParse(dr["Count"].ToString(), out x0) ? Convert.ToInt32(dr["Count"]) : 0;
                var res = ct > 0;

                mi.IsDuplicate = res;

                if (!res)
                {
                    dr.NextResult();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        var selId = Int32.TryParse(dr["NewID"].ToString(), out x0) ? Convert.ToInt32(dr["NewID"]) : 0;
                        mi.SelectedId = selId;

                        dr.NextResult();

                        mi.GunLockMfg = new List<GunLockMenu>();

                        while (dr.Read())
                        {
                            var mfgId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                            var mfg = dr["LockManuf"].ToString();

                            var gm = new GunLockMenu();
                            gm.LockManufId = mfgId;
                            gm.LockManuf = mfg;
                            mi.GunLockMfg.Add(gm);
                        }
                    }
                }
            }

            return mi;
        }

        public AddMenuItemModel AddGunLockModel(int lockMfgId, string lockModel)
        {
            var mi = new AddMenuItemModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAddGunLockModel");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@LockMfgID", SqlDbType.Int) { Value = lockMfgId };
                parameters[1] = new SqlParameter("@LockModel", SqlDbType.VarChar) { Value = lockModel };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return mi;

                dr.Read();

                var x0 = 0;
                var ct = Int32.TryParse(dr["Count"].ToString(), out x0) ? Convert.ToInt32(dr["Count"]) : 0;
                var res = ct > 0;

                mi.IsDuplicate = res;

                if (!res)
                {
                    dr.NextResult();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        var selId = Int32.TryParse(dr["NewID"].ToString(), out x0) ? Convert.ToInt32(dr["NewID"]) : 0;
                        mi.SelectedId = selId;

                        dr.NextResult();

                        mi.GunLockModel = new List<GunLockMenu>();

                        while (dr.Read())
                        {
                            var modelId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                            var model = dr["LockModel"].ToString();

                            var gm = new GunLockMenu();
                            gm.LockModelId = modelId;
                            gm.LockModel = model;
                            mi.GunLockModel.Add(gm);
                        }
                    }
                }
            }

            return mi;
        }

        //public List<SelectListItem> GetGunsInStockMenu(int gunId)
        //{

        //    var list = new List<SelectListItem>();

        //    using (var conn = new SqlConnection(AdminSqlConnection))
        //    {
        //        var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMenuInStockGuns");
        //        var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
        //        conn.Open();

        //        var param = new SqlParameter("@InStockID", SqlDbType.Int) { Value = gunId };
        //        cmd.Parameters.Add(param);

        //        var dr = cmd.ExecuteReader();
        //        if (!dr.HasRows) return list;
        //        var d0 = 0.00;
        //        var i0 = 0;
        //        var b0 = false;
        //        var dt0 = new DateTime(1900, 1, 1);

        //        while (dr.Read())
        //        {
        //            var i1 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
        //            var i2 = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;
        //            var v1 = dr["TransID"].ToString();
        //            var dt1 = DateTime.TryParse(dr["CreateDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["CreateDate"]) : dt0;

        //            var mt = string.Format("{0} {1} Units: {2}", v1, dt1.ToShortDateString(), i2);
        //            list.Add(new SelectListItem { Value = i1.ToString(), Text = mt });
        //        }
        //    }

        //    return list;
        //}

        //public List<SelectListItem> GetMerchInStockMenu(int merchId)
        //{

        //    var list = new List<SelectListItem>();

        //    using (var conn = new SqlConnection(AdminSqlConnection))
        //    {
        //        var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMenuInStockMerch");
        //        var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
        //        conn.Open();

        //        var param = new SqlParameter("@MerchID", SqlDbType.Int) { Value = merchId };
        //        cmd.Parameters.Add(param);

        //        var dr = cmd.ExecuteReader();
        //        if (!dr.HasRows) return list;
        //        var d0 = 0.00;
        //        var i0 = 0;
        //        var b0 = false;
        //        var dt0 = new DateTime(1900, 1, 1);

        //        while (dr.Read())
        //        {
        //            var i1 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
        //            var i2 = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;
        //            var v1 = dr["TransID"].ToString();
        //            var dt1 = DateTime.TryParse(dr["CreateDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["CreateDate"]) : dt0;

        //            var mt = string.Format("{0} {1} Units: {2}", v1, dt1.ToShortDateString(), i2);
        //            list.Add(new SelectListItem { Value = i1.ToString(), Text = mt });
        //        }
        //    }

        //    return list;
        //}


        //public List<SelectListItem> GetAmmoInStockMenu(int ammoId)
        //{

        //    var list = new List<SelectListItem>();

        //    using (var conn = new SqlConnection(AdminSqlConnection))
        //    {
        //        var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMenuInStockAmmo");
        //        var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
        //        conn.Open();

        //        var param = new SqlParameter("@InStockID", SqlDbType.Int) { Value = ammoId };
        //        cmd.Parameters.Add(param);

        //        var dr = cmd.ExecuteReader();
        //        if (!dr.HasRows) return list;
        //        var d0 = 0.00;
        //        var i0 = 0;
        //        var b0 = false;
        //        var dt0 = new DateTime(1900, 1, 1);

        //        while (dr.Read())
        //        {
        //            var i1 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
        //            var i2 = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;
        //            var v1 = dr["TransID"].ToString();
        //            var dt1 = DateTime.TryParse(dr["CreateDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["CreateDate"]) : dt0;

        //            var mt = string.Format("{0} {1} Units: {2}", v1, dt1.ToShortDateString(), i2);
        //            list.Add(new SelectListItem { Value = i1.ToString(), Text = mt });
        //        }
        //    }

        //    return list;
        //}

        public List<SelectListItem> GetInStockMenu(int Id, int catId)
        {

            var list = new List<SelectListItem>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMenuInStock");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@InStockID", SqlDbType.Int) { Value = Id };
                parameters[1] = new SqlParameter("@CatID", SqlDbType.VarChar) { Value = catId };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return list;
                var i0 = 0;
                var dt0 = new DateTime(1900, 1, 1);

                while (dr.Read())
                {
                    var i1 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                    var i2 = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;
                    var v1 = dr["TransID"].ToString();
                    var dt1 = DateTime.TryParse(dr["CreateDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["CreateDate"]) : dt0;

                    var mt = string.Format("{0} {1} Units: {2}", v1, dt1.ToShortDateString(), i2);
                    list.Add(new SelectListItem { Value = i1.ToString(), Text = mt });
                }
            }

            return list;
        }

        public AddToBookModel GetGunItem(int id, bool isSale)
        {
            var a = new AddToBookModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGunsGetItem");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CBID", SqlDbType.Int) { Value = id };
                parameters[1] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = isSale };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return a;
                var dt0 = DateTime.MinValue;
                var d0 = 0.00;
                var i0 = 0;
                var b0 = false;

                dr.Read();

                var i1 = Int32.TryParse(dr["HCC"].ToString(), out i0) ? Convert.ToInt32(dr["HCC"]) : i0;
                var i2 = Int32.TryParse(dr["ActionID"].ToString(), out i0) ? Convert.ToInt32(dr["ActionID"]) : i0;
                var i3 = Int32.TryParse(dr["FinishID"].ToString(), out i0) ? Convert.ToInt32(dr["FinishID"]) : i0;
                var i4 = Int32.TryParse(dr["WeightLb"].ToString(), out i0) ? Convert.ToInt32(dr["WeightLb"]) : i0;
                var i5 = Int32.TryParse(dr["Capacity"].ToString(), out i0) ? Convert.ToInt32(dr["Capacity"]) : i0;
                var i6 = Int32.TryParse(dr["ConditionID"].ToString(), out i0) ? Convert.ToInt32(dr["ConditionID"]) : i0;
                var i7 = Int32.TryParse(dr["CapacityInt"].ToString(), out i0) ? Convert.ToInt32(dr["CapacityInt"]) : i0;
                var i8 = Int32.TryParse(dr["LockMakeId"].ToString(), out i0) ? Convert.ToInt32(dr["LockMakeId"]) : i0;
                var i9 = Int32.TryParse(dr["LockModelId"].ToString(), out i0) ? Convert.ToInt32(dr["LockModelId"]) : i0;
                var i10 = Int32.TryParse(dr["PicRowID"].ToString(), out i0) ? Convert.ToInt32(dr["PicRowID"]) : i0;
                var i11 = Int32.TryParse(dr["LocationID"].ToString(), out i0) ? Convert.ToInt32(dr["LocationID"]) : i0;
                var i12 = Int32.TryParse(dr["TransTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["TransTypeID"]) : i0;
                var i13 = Int32.TryParse(dr["GunTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["GunTypeID"]) : i0;
                var i14 = Int32.TryParse(dr["InStockID"].ToString(), out i0) ? Convert.ToInt32(dr["InStockID"]) : i0;
                var i15 = Int32.TryParse(dr["CustomerID"].ToString(), out i0) ? Convert.ToInt32(dr["CustomerID"]) : i0;
                var i16 = Int32.TryParse(dr["SellerID"].ToString(), out i0) ? Convert.ToInt32(dr["SellerID"]) : i0;
                var i17 = Int32.TryParse(dr["CategoryID"].ToString(), out i0) ? Convert.ToInt32(dr["CategoryID"]) : i0;


                var d1 = Double.TryParse(dr["Fees"].ToString(), out d0) ? Convert.ToDouble(dr["Fees"]) : d0;
                var d2 = Double.TryParse(dr["MSRP"].ToString(), out d0) ? Convert.ToDouble(dr["MSRP"]) : d0;
                var d3 = Double.TryParse(dr["Freight"].ToString(), out d0) ? Convert.ToDouble(dr["Freight"]) : d0;
                var d4 = Double.TryParse(dr["UnitCost"].ToString(), out d0) ? Convert.ToDouble(dr["UnitCost"]) : d0;
                var d5 = Double.TryParse(dr["WeightOz"].ToString(), out d0) ? Convert.ToDouble(dr["WeightOz"]) : d0;
                var d6 = Double.TryParse(dr["SalePrice"].ToString(), out d0) ? Convert.ToDouble(dr["SalePrice"]) : d0;
                var d7 = Double.TryParse(dr["BarrelDec"].ToString(), out d0) ? Convert.ToDouble(dr["BarrelDec"]) : d0;
                var d8 = Double.TryParse(dr["ChamberDec"].ToString(), out d0) ? Convert.ToDouble(dr["ChamberDec"]) : d0;
                var d9 = Double.TryParse(dr["OverallDec"].ToString(), out d0) ? Convert.ToDouble(dr["OverallDec"]) : d0;
                var d10 = Double.TryParse(dr["AskingPrice"].ToString(), out d0) ? Convert.ToDouble(dr["AskingPrice"]) : d0;
                var d11 = Double.TryParse(dr["SellerTaxCollected"].ToString(), out d0) ? Convert.ToDouble(dr["SellerTaxCollected"]) : d0;
                var d12 = Double.TryParse(dr["BuyerPricePaid"].ToString(), out d0) ? Convert.ToDouble(dr["BuyerPricePaid"]) : d0;

                var b1 = Boolean.TryParse(dr["Hide"].ToString(), out b0) ? Convert.ToBoolean(dr["Hide"]) : b0;
                var b2 = Boolean.TryParse(dr["HideCA"].ToString(), out b0) ? Convert.ToBoolean(dr["HideCA"]) : b0;
                var b3 = Boolean.TryParse(dr["Active"].ToString(), out b0) ? Convert.ToBoolean(dr["Active"]) : b0;
                var b4 = Boolean.TryParse(dr["IsUsed"].ToString(), out b0) ? Convert.ToBoolean(dr["IsUsed"]) : b0;
                var b5 = Boolean.TryParse(dr["CaOkay"].ToString(), out b0) ? Convert.ToBoolean(dr["CaOkay"]) : b0;
                var b6 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;
                var b7 = Boolean.TryParse(dr["HoldGun"].ToString(), out b0) ? Convert.ToBoolean(dr["HoldGun"]) : b0;
                var b9 = Boolean.TryParse(dr["IsPPT"].ToString(), out b0) ? Convert.ToBoolean(dr["IsPPT"]) : b0;
                var b10 = Boolean.TryParse(dr["Verified"].ToString(), out b0) ? Convert.ToBoolean(dr["Verified"]) : b0;
                var b11 = Boolean.TryParse(dr["IsCurioRel"].ToString(), out b0) ? Convert.ToBoolean(dr["IsCurioRel"]) : b0;
                var b12 = Boolean.TryParse(dr["IsCaRoster"].ToString(), out b0) ? Convert.ToBoolean(dr["IsCaRoster"]) : b0;
                var b13 = Boolean.TryParse(dr["OriginalBox"].ToString(), out b0) ? Convert.ToBoolean(dr["OriginalBox"]) : b0;
                //var b14 = Boolean.TryParse(dr["IsTaxExempt"].ToString(), out b0) ? Convert.ToBoolean(dr["IsTaxExempt"]) : b0;
                var b15 = Boolean.TryParse(dr["IsCaSglAtn"].ToString(), out b0) ? Convert.ToBoolean(dr["IsCaSglAtn"]) : b0;
                var b16 = Boolean.TryParse(dr["IsCaSglSht"].ToString(), out b0) ? Convert.ToBoolean(dr["IsCaSglSht"]) : b0;
                var b17 = Boolean.TryParse(dr["CurrentModel"].ToString(), out b0) ? Convert.ToBoolean(dr["CurrentModel"]) : b0;
                var b18 = Boolean.TryParse(dr["OriginalPapers"].ToString(), out b0) ? Convert.ToBoolean(dr["OriginalPapers"]) : b0;
                var b19 = Boolean.TryParse(dr["SellerTookTax"].ToString(), out b0) ? Convert.ToBoolean(dr["SellerTookTax"]) : b0;
                var b20 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var b21 = Boolean.TryParse(dr["IsWebBased"].ToString(), out b0) ? Convert.ToBoolean(dr["IsWebBased"]) : b0;
                var b22 = Boolean.TryParse(dr["IsOldSku"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOldSku"]) : b0;
                var b23 = Boolean.TryParse(dr["IsActualPPT"].ToString(), out b0) ? Convert.ToBoolean(dr["IsActualPPT"]) : b0;

                var v1 = dr["CFLC"].ToString();
                var v2 = dr["UpcCode"].ToString();
                var v3 = dr["ItemDesc"].ToString();
                var v4 = dr["SearchUpc"].ToString();
                var v5 = dr["ModelName"].ToString();
                var v6 = dr["MfgPartNumber"].ToString();
                var v7 = dr["LongDescription"].ToString();
                var v8 = dr["TransID"].ToString();
                var v9 = dr["Manufacturer"].ToString();
                var v10 = dr["CaliberGauge"].ToString();
                var v11 = dr["SerialNumber"].ToString();
                var v12 = dr["GunType"].ToString();
                var v13 = dr["ServiceName"].ToString();
                var v14 = dr["DistCode"].ToString();
                var v15 = dr["OldSku"].ToString();
                var v16 = dr["SellerName"].ToString();

                var dt1 = DateTime.TryParse(dr["HoldExpires"].ToString(), out dt0) ? Convert.ToDateTime(dr["HoldExpires"].ToString()) : DateTime.MinValue;
                var xdt = dt1 == DateTime.MinValue ? string.Empty : dt1.ToShortDateString();

                var ig1 = dr["Image1"].ToString();
                var ig2 = dr["Image2"].ToString();
                var ig3 = dr["Image3"].ToString();
                var ig4 = dr["Image4"].ToString();
                var ig5 = dr["Image5"].ToString();
                var ig6 = dr["Image6"].ToString();

                var t = DateTime.Now.Ticks;

                var ttp = Enum.GetName(typeof(PicFolders), i12);
                var cat = Enum.GetName(typeof(ItemCategories), i17);

                var img1 = ig1.Length > 0 ? string.Format("{0}/{1}/{2}/{3}/{4}?{5}", GetHostUrl(), DecryptIt(BPathDir), ttp, cat, ig1, t) : String.Empty;
                var img2 = ig2.Length > 0 ? string.Format("{0}/{1}/{2}/{3}/{4}?{5}", GetHostUrl(), DecryptIt(BPathDir), ttp, cat, ig2, t) : String.Empty;
                var img3 = ig3.Length > 0 ? string.Format("{0}/{1}/{2}/{3}/{4}?{5}", GetHostUrl(), DecryptIt(BPathDir), ttp, cat, ig3, t) : String.Empty;
                var img4 = ig4.Length > 0 ? string.Format("{0}/{1}/{2}/{3}/{4}?{5}", GetHostUrl(), DecryptIt(BPathDir), ttp, cat, ig4, t) : String.Empty;
                var img5 = ig5.Length > 0 ? string.Format("{0}/{1}/{2}/{3}/{4}?{5}", GetHostUrl(), DecryptIt(BPathDir), ttp, cat, ig5, t) : String.Empty;
                var img6 = ig6.Length > 0 ? string.Format("{0}/{1}/{2}/{3}/{4}?{5}", GetHostUrl(), DecryptIt(BPathDir), ttp, cat, ig6, t) : String.Empty;

                //var img1 = ig1.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthGn), ig1, t) : String.Empty;
                //var img2 = ig2.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthGn), ig2, t) : String.Empty;
                //var img3 = ig3.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthGn), ig3, t) : String.Empty;
                //var img4 = ig4.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthGn), ig4, t) : String.Empty;
                //var img5 = ig5.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthGn), ig5, t) : String.Empty;
                //var img6 = ig6.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthGn), ig6, t) : String.Empty;

                var im = new ImageModel(i12, img1, img2, img3, img4, img5, img6, ig1, ig2, ig3, ig4, ig5, ig6, v14);

                var am = new AcctModel(i11, i12, b20, b19, d4, d3, d1, d11, d10, d2, d6, d12, v13, v16);
                var ca = new CaRestrictModel(i8, i9, i1, i5, b5, b2, b7, b9, b11, b12, b15, b16, b23, v1, xdt);
                var gm = new GunModel(i14, i2, i3, i6, i7, i4, i13, d5, d7, d8, d9, b22, b1, b3, b4, b6, b10, b13, b17, b18, b21, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v15);

                a.Images = im;
                a.Accounting = am;
                a.Compliance = ca;
                a.Gun = gm;
                a.CustomerId = i15;
                a.SellerId = i16;
            }

            return a;
        }

        public TagModel UpdateGunCtx(AddToBookModel m)
        {

            var tm = new TagModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGunUpdate");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var dt0 = DateTime.MinValue;

                var parameters = new IDataParameter[53];
                parameters[0] = new SqlParameter("@CBID", SqlDbType.Int) { Value = m.Gun.CostBasisId };
                parameters[1] = new SqlParameter("@ActionID", SqlDbType.Int) { Value = m.Gun.ActionId };
                parameters[2] = new SqlParameter("@FinishID", SqlDbType.Int) { Value = m.Gun.FinishId };
                parameters[3] = new SqlParameter("@WeightLb", SqlDbType.Int) { Value = m.Gun.WeightLb };
                parameters[4] = new SqlParameter("@Capacity", SqlDbType.Int) { Value = m.Gun.CapacityInt };
                parameters[5] = new SqlParameter("@LockMakeID", SqlDbType.Int) { Value = m.Compliance.LockMakeId };
                parameters[6] = new SqlParameter("@LockModelID", SqlDbType.Int) { Value = m.Compliance.LockModelId };
                parameters[7] = new SqlParameter("@ConditionID", SqlDbType.Int) { Value = m.Gun.ConditionId };
                parameters[8] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = m.Accounting.TransTypeId };
                parameters[9] = new SqlParameter("@CaHiCapCt", SqlDbType.Int) { Value = m.Compliance.HiCapMagCount };
                parameters[10] = new SqlParameter("@CaHiCapCap", SqlDbType.Int) { Value = m.Compliance.HiCapCapacity};
                parameters[11] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = m.CustomerId };
                parameters[12] = new SqlParameter("@SellerID", SqlDbType.Int) { Value = m.SellerId };
                parameters[13] = new SqlParameter("@BarrelDec", SqlDbType.Decimal) { Value = m.Gun.BarrelDec };
                parameters[14] = new SqlParameter("@ChamberDec", SqlDbType.Decimal) { Value = m.Gun.ChamberDec };
                parameters[15] = new SqlParameter("@OverallDec", SqlDbType.Decimal) { Value = m.Gun.OverallDec };
                parameters[16] = new SqlParameter("@WeightOz", SqlDbType.Decimal) { Value = m.Gun.WeightOz };
                parameters[17] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = m.Accounting.ItemCost };
                parameters[18] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = m.Accounting.ItemFees };
                parameters[19] = new SqlParameter("@MSRP", SqlDbType.Decimal) { Value = m.Accounting.Msrp };
                parameters[20] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = m.Accounting.FreightCost };
                parameters[21] = new SqlParameter("@AskPrice", SqlDbType.Decimal) { Value = m.Accounting.AskingPrice };
                parameters[22] = new SqlParameter("@SalePrice", SqlDbType.Decimal) { Value = m.Accounting.SalePrice };
                parameters[23] = new SqlParameter("@TaxAmount", SqlDbType.Decimal) { Value = m.Accounting.SellerTaxAmount };
                parameters[24] = new SqlParameter("@BuyerPricePaid", SqlDbType.Decimal) { Value = m.Accounting.CustPricePaid };
                parameters[25] = new SqlParameter("@CFLC", SqlDbType.VarChar) { Value = m.Compliance.CflcInbound };
                parameters[26] = new SqlParameter("@MfgPartNumber", SqlDbType.VarChar) { Value = m.Gun.MfgPartNumber };
                parameters[27] = new SqlParameter("@ItemDesc", SqlDbType.VarChar) { Value = m.Gun.Description };
                parameters[28] = new SqlParameter("@LongDesc", SqlDbType.VarChar) { Value = m.Gun.LongDescription };
                parameters[29] = new SqlParameter("@WebSearchUpc", SqlDbType.VarChar) { Value = m.Gun.WebSearchUpc };
                parameters[30] = new SqlParameter("@Model", SqlDbType.VarChar) { Value = m.Gun.ModelName };
                parameters[31] = new SqlParameter("@SvcCustName", SqlDbType.VarChar) { Value = m.Accounting.SvcCustName == "" ? (object)DBNull.Value : m.Accounting.SvcCustName };
                parameters[32] = new SqlParameter("@OldSku", SqlDbType.VarChar) { Value = m.Gun.OldSku };
                parameters[33] = new SqlParameter("@OrigBx", SqlDbType.Bit) { Value = m.Gun.OrigBox };
                parameters[34] = new SqlParameter("@OrigPw", SqlDbType.Bit) { Value = m.Gun.OrigPaperwork };
                parameters[35] = new SqlParameter("@CaOkay", SqlDbType.Bit) { Value = m.Compliance.CaOkay };
                parameters[36] = new SqlParameter("@CaRost", SqlDbType.Bit) { Value = m.Compliance.CaRosterOk };
                parameters[37] = new SqlParameter("@CaSgAt", SqlDbType.Bit) { Value = m.Compliance.CaSglActnOk };
                parameters[38] = new SqlParameter("@CaSsPt", SqlDbType.Bit) { Value = m.Compliance.CaSglShotOk };
                parameters[39] = new SqlParameter("@CaCuri", SqlDbType.Bit) { Value = m.Compliance.CaCurioOk };
                parameters[40] = new SqlParameter("@CaPPTe", SqlDbType.Bit) { Value = m.Compliance.CaPptOk };
                parameters[41] = new SqlParameter("@CaHold", SqlDbType.Bit) { Value = m.Compliance.HoldGun };
                parameters[42] = new SqlParameter("@GotTax", SqlDbType.Bit) { Value = m.Accounting.SellerCollectedTax };
                parameters[43] = new SqlParameter("@IsOnWeb", SqlDbType.Bit) { Value = m.Gun.IsOnWeb };
                parameters[44] = new SqlParameter("@IsUsed", SqlDbType.Bit) { Value = m.Gun.IsUsed };
                parameters[45] = new SqlParameter("@IsHide", SqlDbType.Bit) { Value = m.Gun.IsHidden };
                parameters[46] = new SqlParameter("@IsHideCA", SqlDbType.Bit) { Value = m.Compliance.CaHide };
                parameters[47] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = m.Gun.IsActive };
                parameters[48] = new SqlParameter("@IsVerified", SqlDbType.Bit) { Value = m.Gun.IsVerified };
                parameters[49] = new SqlParameter("@IsCurModel", SqlDbType.Bit) { Value = m.Gun.IsCurModel };
                parameters[50] = new SqlParameter("@IsOldSku", SqlDbType.Bit) { Value = m.Gun.IsOldSku };
                parameters[51] = new SqlParameter("@IsActualPPT", SqlDbType.Bit) { Value = m.Compliance.IsActualPpt };
                parameters[52] = new SqlParameter("@HoldGunExp", SqlDbType.DateTime) { Value = m.Compliance.HoldGunExpires == dt0 ? (object)DBNull.Value : m.Compliance.HoldGunExpires };


                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return tm;

                dr.Read();
                var x0 = 0;
                var b0 = false;
                double d0 = 0.00;

                var b1 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var i1 = Int32.TryParse(dr["InStockID"].ToString(), out x0) ? Convert.ToInt32(dr["InStockID"]) : 0;
                var i2 = Int32.TryParse(dr["TagCap"].ToString(), out x0) ? Convert.ToInt32(dr["TagCap"]) : 0;
                var d1 = Double.TryParse(dr["TagPrc"].ToString(), out d0) ? Convert.ToDouble(dr["TagPrc"]) : 0;
                var d2 = Double.TryParse(dr["TagBrl"].ToString(), out d0) ? Convert.ToDouble(dr["TagBrl"]) : 0;

                var v1 = dr["TagMFG"].ToString();
                var v2 = dr["TagTyp"].ToString();
                var v3 = dr["TagCal"].ToString();
                var v4 = dr["TagCnd"].ToString();
                var v5 = dr["TagSvc"].ToString();
                var v6 = dr["TagSku"].ToString();
                var v7 = dr["TagMdl"].ToString();
                var v8 = dr["TagMPN"].ToString();
                var v9 = dr["TagSer"].ToString();
                var v10 = dr["TagNam"].ToString();

                tm = new TagModel(i1, 0, i2, b1, d1, d2, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10); 

            }

            return tm;   
        }

        public TagModel RestockGun(AddToBookModel a)
        {
            var tm = new TagModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGunRestock");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[35];
                parameters[0] = new SqlParameter("@InStockID", SqlDbType.Int) { Value = a.Gun.Id };
                parameters[1] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = a.Accounting.TransTypeId };
                parameters[2] = new SqlParameter("@AcqSourceID", SqlDbType.Int) { Value = a.BoundBook.AcqTypeId };
                parameters[3] = new SqlParameter("@LockMakeID", SqlDbType.Int) { Value = a.Compliance.LockMakeId };
                parameters[4] = new SqlParameter("@LockModelID", SqlDbType.Int) { Value = a.Compliance.LockModelId };
                parameters[5] = new SqlParameter("@HiCapMagCt", SqlDbType.Int) { Value = a.Compliance.HiCapMagCount };
                parameters[6] = new SqlParameter("@HiCapCap", SqlDbType.Int) { Value = a.Compliance.HiCapCapacity };
                parameters[7] = new SqlParameter("@AcqFFLSrc", SqlDbType.Int) { Value = a.BoundBook.AcqFflSrc };
                parameters[8] = new SqlParameter("@AcqFFLCode", SqlDbType.Int) { Value = a.BoundBook.AcqFflCode };
                parameters[9] = new SqlParameter("@UnitsCAL", SqlDbType.Int) { Value = a.Accounting.UnitsCal };
                parameters[10] = new SqlParameter("@UnitsWYO", SqlDbType.Int) { Value = a.Accounting.UnitsWyo };
                parameters[11] = new SqlParameter("@NotForSaleCAL", SqlDbType.Int) { Value = a.Accounting.NotForSaleCal };
                parameters[12] = new SqlParameter("@NotForSaleWYO", SqlDbType.Int) { Value = a.Accounting.NotForSaleWyo };
                parameters[13] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = a.Accounting.CustomerId };
                parameters[14] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = a.Accounting.SupplierId };
                parameters[15] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = a.BoundBook.LocationId };
                parameters[16] = new SqlParameter("@SellerTaxAmount", SqlDbType.Decimal) { Value = a.Accounting.SellerTaxAmount };
                parameters[17] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = a.Accounting.FreightCost };
                parameters[18] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = a.Accounting.ItemCost };
                parameters[19] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = a.Accounting.ItemFees  };
                parameters[20] = new SqlParameter("@ConsignmentPrice", SqlDbType.Decimal) { Value = a.Accounting.AskingPrice };
                parameters[21] = new SqlParameter("@BuyerPricePaid", SqlDbType.Decimal) { Value = a.Accounting.CustPricePaid };
                parameters[22] = new SqlParameter("@SerialNumber", SqlDbType.VarChar) { Value = a.BoundBook.GunSerial };
                parameters[23] = new SqlParameter("@CFLCInbound", SqlDbType.VarChar) { Value = a.Compliance.CflcInbound == "" ? (object)DBNull.Value : a.Compliance.CflcInbound };
                parameters[24] = new SqlParameter("@SvcCustName", SqlDbType.VarChar) { Value = a.Accounting.SvcCustName == "" ? (object)DBNull.Value : a.Accounting.SvcCustName };
                parameters[25] = new SqlParameter("@OldSku", SqlDbType.VarChar) { Value = a.Gun.OldSku };
                parameters[26] = new SqlParameter("@AcqEmail", SqlDbType.VarChar) { Value = a.BoundBook.AcqEmail == "" ? (object)DBNull.Value : a.BoundBook.AcqEmail };
                parameters[27] = new SqlParameter("@SellerCollTax", SqlDbType.Bit) { Value = a.Accounting.SellerCollectedTax };
                parameters[28] = new SqlParameter("@IsHold30", SqlDbType.Bit) { Value = a.Compliance.HoldGun };
                parameters[29] = new SqlParameter("@IsOnWeb", SqlDbType.Bit) { Value = a.Gun.IsOnWeb };
                parameters[30] = new SqlParameter("@IsOldSku", SqlDbType.Bit) { Value = a.Gun.IsOldSku };
                parameters[31] = new SqlParameter("@PptOK", SqlDbType.Bit) { Value = a.Compliance.CaPptOk };
                parameters[32] = new SqlParameter("@IsActualPPT", SqlDbType.Bit) { Value = a.Compliance.IsActualPpt };
                parameters[33] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = a.BoundBook.AcqDate == DateTime.MinValue ? (object)DBNull.Value : a.BoundBook.AcqDate };
                parameters[34] = new SqlParameter("@HoldExp", SqlDbType.DateTime) { Value = a.Compliance.HoldGunExpires == DateTime.MinValue ? (object)DBNull.Value : a.Compliance.HoldGunExpires };	
 	
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return tm;

                dr.Read();
                var x0 = 0;
                var b0 = false;
                double d0 = 0.00;

                var b1 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var i1 = Int32.TryParse(dr["InStockID"].ToString(), out x0) ? Convert.ToInt32(dr["InStockID"]) : 0;
                var i2 = Int32.TryParse(dr["TagCap"].ToString(), out x0) ? Convert.ToInt32(dr["TagCap"]) : 0;
                var d1 = Double.TryParse(dr["TagPrc"].ToString(), out d0) ? Convert.ToDouble(dr["TagPrc"]) : 0;
                var d2 = Double.TryParse(dr["TagBrl"].ToString(), out d0) ? Convert.ToDouble(dr["TagBrl"]) : 0;

                var v1 = dr["TagMFG"].ToString();
                var v2 = dr["TagTyp"].ToString();
                var v3 = dr["TagCal"].ToString();
                var v4 = dr["TagCnd"].ToString();
                var v5 = dr["TagSvc"].ToString();
                var v6 = dr["TagSku"].ToString();
                var v7 = dr["TagMdl"].ToString();
                var v8 = dr["TagMPN"].ToString();
                var v9 = dr["TagSer"].ToString();
                var v10 = dr["TagNam"].ToString();

                tm = new TagModel(i1, 0, i2, b1, d1, d2, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10);

            }

            return tm;
        }



        #region Ammunition

        public TagModel RestockAmmoCtx(AmmoModel a)
        {

            var tm = new TagModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAmmoRestock");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[23];
                parameters[0] = new SqlParameter("@InStockID", SqlDbType.Int) { Value = a.InStockId };
                parameters[1] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = a.BookModel.TransTypeId };
                parameters[2] = new SqlParameter("@UnitsCAL", SqlDbType.Int) { Value = a.AcctModel.UnitsCal };
                parameters[3] = new SqlParameter("@UnitsWYO", SqlDbType.Int) { Value = a.AcctModel.UnitsWyo };
                parameters[4] = new SqlParameter("@NotForSaleCAL", SqlDbType.Int) { Value = a.AcctModel.NotForSaleCal };
                parameters[5] = new SqlParameter("@NotForSaleWYO", SqlDbType.Int) { Value = a.AcctModel.NotForSaleWyo };
                parameters[6] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = a.AcctModel.CustomerId };
                parameters[7] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = a.AcctModel.SupplierId };
                parameters[8] = new SqlParameter("@FFLCode", SqlDbType.Int) { Value = a.BookModel.AcqFflCode };
                parameters[9] = new SqlParameter("@AcqTypeID", SqlDbType.Int) { Value = a.BookModel.AcqTypeId };
                parameters[10] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = a.BookModel.LocationId };
                parameters[11] = new SqlParameter("@SellerTaxAmount", SqlDbType.Decimal) { Value = a.AcctModel.SellerTaxAmount };
                parameters[12] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = a.AcctModel.FreightCost };
                parameters[13] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = a.AcctModel.ItemCost };
                parameters[14] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = a.AcctModel.ItemFees };
                parameters[15] = new SqlParameter("@BuyerPricePaid", SqlDbType.Decimal) { Value = a.AcctModel.CustPricePaid };
                parameters[16] = new SqlParameter("@SvcCustName", SqlDbType.VarChar) { Value = a.AcctModel.SvcCustName.Length > 0 ? a.AcctModel.SvcCustName : (object)DBNull.Value };
                parameters[17] = new SqlParameter("@AcqEmail", SqlDbType.VarChar) { Value = a.BookModel.AcqEmail.Length > 0 ? a.BookModel.AcqEmail : (object)DBNull.Value };
                parameters[18] = new SqlParameter("@SellerCollTax", SqlDbType.Bit) { Value = a.AcctModel.SellerCollectedTax };
                parameters[19] = new SqlParameter("@IsOnWeb", SqlDbType.Bit) { Value = a.IsOnWeb };
                parameters[20] = new SqlParameter("@IsActualPPT", SqlDbType.Bit) { Value = a.IsActualPpt };
                parameters[21] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = a.BookModel.IsSale };
                parameters[22] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = a.BookModel.AcqDate == DateTime.MinValue ? (object)DBNull.Value : a.BookModel.AcqDate };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return tm;

                dr.Read();
                var x0 = 0;
                var b0 = false;
                double d0 = 0.00;
                var b1 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var d1 = Double.TryParse(dr["TagPrc"].ToString(), out d0) ? Convert.ToDouble(dr["TagPrc"]) : 0;
                var i1 = Int32.TryParse(dr["AmmoID"].ToString(), out x0) ? Convert.ToInt32(dr["AmmoID"]) : 0;
                var i2 = Int32.TryParse(dr["TagRPB"].ToString(), out x0) ? Convert.ToInt32(dr["TagRPB"]) : 0;
                var v1 = dr["TagSku"].ToString();
                var v2 = dr["TagMfg"].ToString();
                var v3 = dr["TagCat"].ToString();
                var v4 = dr["TagCal"].ToString();
                var v5 = dr["TagBul"].ToString();
                var v6 = dr["TagMpn"].ToString();
                var v7 = dr["TagSvc"].ToString();
                var v8 = dr["TagNam"].ToString();

                tm = new TagModel(i1, i2, b1, d1, v1, v2, v3, v4, v5, v6, v7, v8);

            }

            return tm;
        }

        public TagModel AddAmmoItem(AmmoModel a)
        {
            var tm = new TagModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAmmoCreate");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[38];
                parameters[0] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = a.BookModel.TransTypeId };
                parameters[1] = new SqlParameter("@SubCatId", SqlDbType.Int) { Value = a.SubCategoryId };
                parameters[2] = new SqlParameter("@RoundsPerBox", SqlDbType.Int) { Value = a.RoundsPerBox };
                parameters[3] = new SqlParameter("@BulletTypeID", SqlDbType.Int) { Value = a.BulletTypeId };
                parameters[4] = new SqlParameter("@ConditionID", SqlDbType.Int) { Value = a.ConditionId };
                parameters[5] = new SqlParameter("@GrainWeight", SqlDbType.Int) { Value = a.GrainWeight };
                parameters[6] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = a.CaliberId };
                parameters[7] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = a.AmmoManufId };
                parameters[8] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = a.BookModel.LocationId };
                parameters[9] = new SqlParameter("@UnitsCAL", SqlDbType.Int) { Value = a.AcctModel.UnitsCal };
                parameters[10] = new SqlParameter("@UnitsWYO", SqlDbType.Int) { Value = a.AcctModel.UnitsWyo };
                parameters[11] = new SqlParameter("@NotForSaleCAL", SqlDbType.Int) { Value = a.AcctModel.NotForSaleCal };
                parameters[12] = new SqlParameter("@NotForSaleWYO", SqlDbType.Int) { Value = a.AcctModel.NotForSaleWyo };
                parameters[13] = new SqlParameter("@AcqSourceID", SqlDbType.Int) { Value = a.BookModel.AcqTypeId };
                parameters[14] = new SqlParameter("@AcqFFLCode", SqlDbType.Int) { Value = a.BookModel.AcqFflCode };
                parameters[15] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = a.AcctModel.CustomerId };
                parameters[16] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = a.AcctModel.SupplierId };
                parameters[17] = new SqlParameter("@BuyerPricePaid", SqlDbType.Decimal) { Value = a.AcctModel.CustPricePaid };
                parameters[18] = new SqlParameter("@SellerCollTaxAmt", SqlDbType.Decimal) { Value = a.AcctModel.SellerTaxAmount };
                parameters[19] = new SqlParameter("@ShotSizeSlugWt", SqlDbType.Decimal) { Value = a.ShotSizeWeight };
                parameters[20] = new SqlParameter("@AskingPrice", SqlDbType.Decimal) { Value = a.AcctModel.AskingPrice };
                parameters[21] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = a.AcctModel.FreightCost };
                parameters[22] = new SqlParameter("@Chamber", SqlDbType.Decimal) { Value = a.Chamber };
                parameters[23] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = a.AcctModel.ItemCost };
                parameters[24] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = a.AcctModel.ItemFees };
                parameters[25] = new SqlParameter("@ItemDesc", SqlDbType.VarChar) { Value = a.ItemDesc };
                parameters[26] = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = a.UpcCode };
                parameters[27] = new SqlParameter("@WebSearchUpc", SqlDbType.VarChar) { Value = a.WebSearchUpc };
                parameters[28] = new SqlParameter("@MfgPartNumber", SqlDbType.VarChar) { Value = a.MfgPartNumber };
                parameters[29] = new SqlParameter("@SvcCustName", SqlDbType.VarChar) { Value = a.AcctModel.SvcCustName=="" ? (object)DBNull.Value : a.AcctModel.SvcCustName };
                parameters[30] = new SqlParameter("@AcqEmail", SqlDbType.VarChar) { Value = a.BookModel.AcqEmail == "" ? (object)DBNull.Value : a.BookModel.AcqEmail };
                parameters[31] = new SqlParameter("@SellerCollTax", SqlDbType.Bit) { Value = a.AcctModel.SellerCollectedTax };
                parameters[32] = new SqlParameter("@IsSlug", SqlDbType.Bit) { Value = a.IsSlug };
                parameters[33] = new SqlParameter("@IsOnWeb", SqlDbType.Bit) { Value = a.IsOnWeb };
                parameters[34] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = a.BookModel.IsSale };
                parameters[35] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = a.IsActive };
                parameters[36] = new SqlParameter("@IsActualPPT", SqlDbType.Bit) { Value = a.IsActualPpt };
                parameters[37] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = a.BookModel.AcqDate == DateTime.MinValue ? (object)DBNull.Value : a.BookModel.AcqDate };

                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return tm;

                dr.Read();
                var x0 = 0;
                var b0 = false;
                double d0 = 0.00;
                var b1 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var d1 = Double.TryParse(dr["TagPrc"].ToString(), out d0) ? Convert.ToDouble(dr["TagPrc"]) : 0;
                var i1 = Int32.TryParse(dr["InStockId"].ToString(), out x0) ? Convert.ToInt32(dr["InStockId"]) : 0;
                var i2 = Int32.TryParse(dr["TagRPB"].ToString(), out x0) ? Convert.ToInt32(dr["TagRPB"]) : 0;
                var v1 = dr["TagSku"].ToString();
                var v2 = dr["TagMfg"].ToString();
                var v3 = dr["TagCat"].ToString();
                var v4 = dr["TagCal"].ToString();
                var v5 = dr["TagBul"].ToString();
                var v6 = dr["TagMpn"].ToString();
                var v7 = dr["TagSvc"].ToString();
                var v8 = dr["TagNam"].ToString();

                tm = new TagModel(i1, i2, b1, d1, v1, v2, v3, v4, v5, v6, v7, v8);
            }

            return tm;

        }

        public TagModel UpdateAmmoItem(AmmoModel a)
        {

            var tm = new TagModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAmmoUpdate");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[40];

                parameters[0] = new SqlParameter("@CBID", SqlDbType.Int) { Value = a.CostBasisId };
                parameters[1] = new SqlParameter("@SubCatId", SqlDbType.Int) { Value = a.SubCategoryId };
                parameters[2] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = a.AmmoManufId };
                parameters[3] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = a.CaliberId };
                parameters[4] = new SqlParameter("@BulletTypeID", SqlDbType.Int) { Value = a.BulletTypeId };
                parameters[5] = new SqlParameter("@GrainWeight", SqlDbType.Int) { Value = a.GrainWeight };
                parameters[6] = new SqlParameter("@RoundsPerBox", SqlDbType.Int) { Value = a.RoundsPerBox };
                parameters[7] = new SqlParameter("@ConditionID", SqlDbType.Int) { Value = a.ConditionId };
                parameters[8] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = a.BookModel.TransTypeId };
                parameters[9] = new SqlParameter("@UnitsCAL", SqlDbType.Int) { Value = a.AcctModel.UnitsCal };
                parameters[10] = new SqlParameter("@UnitsWYO", SqlDbType.Int) { Value = a.AcctModel.UnitsWyo };
                parameters[11] = new SqlParameter("@NotForSaleCAL", SqlDbType.Int) { Value = a.AcctModel.NotForSaleCal };
                parameters[12] = new SqlParameter("@NotForSaleWYO", SqlDbType.Int) { Value = a.AcctModel.NotForSaleWyo };
                parameters[13] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = a.BookModel.LocationId };
                parameters[14] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = a.AcctModel.CustomerId };
                parameters[15] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = a.AcctModel.SupplierId };
                parameters[16] = new SqlParameter("@FFLCode", SqlDbType.Int) { Value = a.BookModel.AcqFflCode };
                parameters[17] = new SqlParameter("@AcqTypeID", SqlDbType.Int) { Value = a.BookModel.AcqTypeId };
                parameters[18] = new SqlParameter("@ShotSizeSlugWt", SqlDbType.Decimal  ) { Value = a.ShotSizeWeight };
                parameters[19] = new SqlParameter("@AskingPrice", SqlDbType.Decimal) { Value = a.AcctModel.AskingPrice };
                parameters[20] = new SqlParameter("@UnitCost", SqlDbType.Decimal) { Value = a.AcctModel.ItemCost };
                parameters[21] = new SqlParameter("@Chamber", SqlDbType.Decimal) { Value = a.Chamber };
                parameters[22] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = a.AcctModel.FreightCost };
                parameters[23] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = a.AcctModel.ItemFees };
                parameters[24] = new SqlParameter("@Msrp", SqlDbType.Decimal) { Value = a.AcctModel.Msrp };
                parameters[25] = new SqlParameter("@TaxAmount", SqlDbType.Decimal) { Value = a.AcctModel.SellerTaxAmount };
                parameters[26] = new SqlParameter("@BuyerPricePaid", SqlDbType.Decimal) { Value = a.AcctModel.CustPricePaid };
                parameters[27] = new SqlParameter("@IsOnWeb", SqlDbType.Bit) { Value = a.IsOnWeb };
                parameters[28] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = a.BookModel.IsSale };
                parameters[29] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = a.IsActive };
                parameters[30] = new SqlParameter("@IsSlug", SqlDbType.Bit) { Value = a.IsSlug };
                parameters[31] = new SqlParameter("@IsActualPPT", SqlDbType.Bit) { Value = a.IsActualPpt };
                parameters[32] = new SqlParameter("@SellerTookTax", SqlDbType.Bit) { Value = a.AcctModel.SellerCollectedTax };
                parameters[33] = new SqlParameter("@MfgPartNumber", SqlDbType.VarChar) { Value = a.MfgPartNumber };
                parameters[34] = new SqlParameter("@ItemDesc", SqlDbType.VarChar) { Value = a.ItemDesc };
                parameters[35] = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = a.UpcCode };
                parameters[36] = new SqlParameter("@WebSearchUpc", SqlDbType.VarChar) { Value = a.WebSearchUpc };
                parameters[37] = new SqlParameter("@SvcCustName", SqlDbType.VarChar) { Value = a.AcctModel.SvcCustName == "" ? (object)DBNull.Value : a.AcctModel.SvcCustName };
                parameters[38] = new SqlParameter("@AcqEmail", SqlDbType.VarChar) { Value = a.BookModel.AcqEmail == "" ? (object)DBNull.Value : a.BookModel.AcqEmail };
                parameters[39] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = a.BookModel.AcqDate == DateTime.MinValue ? (object)DBNull.Value : a.BookModel.AcqDate };

                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return tm;

                dr.Read();
                var x0 = 0;
                var b0 = false;
                double d0 = 0.00;
                var b1 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var d1 = Double.TryParse(dr["TagPrc"].ToString(), out d0) ? Convert.ToDouble(dr["TagPrc"]) : 0;
                var i1 = Int32.TryParse(dr["AmmoID"].ToString(), out x0) ? Convert.ToInt32(dr["AmmoID"]) : 0;
                var i2 = Int32.TryParse(dr["TagRPB"].ToString(), out x0) ? Convert.ToInt32(dr["TagRPB"]) : 0;
                var v1 = dr["TagSku"].ToString();
                var v2 = dr["TagMfg"].ToString();
                var v3 = dr["TagCat"].ToString();
                var v4 = dr["TagCal"].ToString();
                var v5 = dr["TagBul"].ToString();
                var v6 = dr["TagMpn"].ToString();
                var v7 = dr["TagSvc"].ToString();
                var v8 = dr["TagNam"].ToString();

                tm = new TagModel(i1, i2, b1, d1, v1, v2, v3, v4, v5, v6, v7, v8);

            }

            return tm;
        }

        public void DeleteAmmoItem(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAmmoDeleteItem");

            var param = new SqlParameter("@ID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(AdminSqlConnection, proc, param);
        }

        public AmmoModel GetAmmoItem(int id, bool isSale)
        {
            var am = new AmmoModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAmmoGetItem");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CBID", SqlDbType.Int) { Value = id };
                parameters[1] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = isSale };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return am;
                var d0 = 0.00;
                var i0 = 0;
                var b0 = false;
                var dt0 = DateTime.MinValue;

                dr.Read();
                var dir = ConfigurationHelper.GetPropertyValue("application", "m10");

                var i1 = Int32.TryParse(dr["CaliberID"].ToString(), out i0) ? Convert.ToInt32(dr["CaliberID"]) : i0;
                var i2 = Int32.TryParse(dr["ManufID"].ToString(), out i0) ? Convert.ToInt32(dr["ManufID"]) : i0;
                var i3 = Int32.TryParse(dr["BulletTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["BulletTypeID"]) : i0;
                var i4 = Int32.TryParse(dr["GrainWeight"].ToString(), out i0) ? Convert.ToInt32(dr["GrainWeight"]) : i0;
                var i5 = Int32.TryParse(dr["RoundsPerBox"].ToString(), out i0) ? Convert.ToInt32(dr["RoundsPerBox"]) : i0;
                var i6 = Int32.TryParse(dr["SubCategoryID"].ToString(), out i0) ? Convert.ToInt32(dr["SubCategoryID"]) : i0;
                var i7 = Int32.TryParse(dr["ConditionID"].ToString(), out i0) ? Convert.ToInt32(dr["ConditionID"]) : i0;
                var i8 = Int32.TryParse(dr["UnitsCAL"].ToString(), out i0) ? Convert.ToInt32(dr["UnitsCAL"]) : i0;
                var i9 = Int32.TryParse(dr["TransTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["TransTypeID"]) : i0;
                var i10 = Int32.TryParse(dr["UnitsWYO"].ToString(), out i0) ? Convert.ToInt32(dr["UnitsWYO"]) : i0;
                var i11 = Int32.TryParse(dr["PicRowID"].ToString(), out i0) ? Convert.ToInt32(dr["PicRowID"]) : i0;
                var i12 = Int32.TryParse(dr["InStockID"].ToString(), out i0) ? Convert.ToInt32(dr["InStockID"]) : i0;
                var i13 = Int32.TryParse(dr["LocationID"].ToString(), out i0) ? Convert.ToInt32(dr["LocationID"]) : i0;
                var i14 = Int32.TryParse(dr["CustomerID"].ToString(), out i0) ? Convert.ToInt32(dr["CustomerID"]) : i0;
                var i15 = Int32.TryParse(dr["SupplierID"].ToString(), out i0) ? Convert.ToInt32(dr["SupplierID"]) : i0;

                var i16 = Int32.TryParse(dr["SupplierTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["SupplierTypeID"]) : i0;
                var i17 = Int32.TryParse(dr["FFLCode"].ToString(), out i0) ? Convert.ToInt32(dr["FFLCode"]) : i0;
                var i18 = Int32.TryParse(dr["StateID"].ToString(), out i0) ? Convert.ToInt32(dr["StateID"]) : i0;
 

                var d1 = Double.TryParse(dr["AskingPrice"].ToString(), out d0) ? Convert.ToDouble(dr["AskingPrice"]) : d0;
                var d2 = Double.TryParse(dr["ChamberDec"].ToString(), out d0) ? Convert.ToDouble(dr["ChamberDec"]) : d0;
                var d3 = Double.TryParse(dr["ShotSizeSlugWt"].ToString(), out d0) ? Convert.ToDouble(dr["ShotSizeSlugWt"]) : d0;
                var d4 = Double.TryParse(dr["SellerTaxCollected"].ToString(), out d0) ? Convert.ToDouble(dr["SellerTaxCollected"]) : d0;
                var d5 = Double.TryParse(dr["UnitCost"].ToString(), out d0) ? Convert.ToDouble(dr["UnitCost"]) : d0;
                var d6 = Double.TryParse(dr["Freight"].ToString(), out d0) ? Convert.ToDouble(dr["Freight"]) : d0;
                var d7 = Double.TryParse(dr["Fees"].ToString(), out d0) ? Convert.ToDouble(dr["Fees"]) : d0;
                var d8 = Double.TryParse(dr["BuyerPricePaid"].ToString(), out d0) ? Convert.ToDouble(dr["BuyerPricePaid"]) : d0;
                var d9 = Double.TryParse(dr["MSRP"].ToString(), out d0) ? Convert.ToDouble(dr["MSRP"]) : d0;

                var b1 = Boolean.TryParse(dr["IsSlug"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSlug"]) : b0;
                var b2 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;
                var b3 = Boolean.TryParse(dr["SellerTookTax"].ToString(), out b0) ? Convert.ToBoolean(dr["SellerTookTax"]) : b0;
                var b5 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var b6 = Boolean.TryParse(dr["IsActive"].ToString(), out b0) ? Convert.ToBoolean(dr["IsActive"]) : b0;
                var b7 = Boolean.TryParse(dr["IsActualPPT"].ToString(), out b0) ? Convert.ToBoolean(dr["IsActualPPT"]) : b0;

                var v1 = dr["UpcCode"].ToString();
                var v3 = dr["ItemDesc"].ToString();
                var v4 = dr["MfgPartNumber"].ToString();
                var v5 = dr["TransID"].ToString();
                var v16 = dr["SearchUpc"].ToString();

                var v6 = dr["FirstName"].ToString();
                var v7 = dr["LastName"].ToString();
                var v8 = dr["OrgName"].ToString();
                var v9 = dr["SupAddress"].ToString();
                var v10 = dr["SupCity"].ToString();
                var v11 = dr["SupState"].ToString();
                var v12 = dr["SupZipCode"].ToString();
                var v13 = dr["SupZipExt"].ToString();
                var v14 = dr["SupEmail"].ToString();
                var v15 = dr["FFLNumber"].ToString();
                var v20 = dr["CustomerName"].ToString();
                var v21 = dr["SupEmail"].ToString();

                var v17 = string.Empty;
                var v18 = v13.Length > 0 ? v12 + "-" + v13 : v12;
                var v19 = string.Empty;

                if (i17 > 0) //FFL NUMBER
                {
                    var ss = i17.ToString();
                    var a = ss.Substring(0, 1);
                    var b = ss.Substring(1, 2);
                    var c = ss.Substring(3, 5);

                    v19 = string.Format("{0}-{1}-{2}", a, b, c);
                }


                switch (i16)
                {
                    case 1: // FFL
                        v17 = string.Format("{0} {1} {2}, {3} {4} {5}", v8, v9, v10, v11, v18, v19);
                        break;
                    case 2: // C&R FFL
                        v17 = string.Format("{0} {1} : {2}", v6, v7, v15);
                        break;
                    case 3: // PVT PARTY - OWNER COLLECTION
                    case 6:
                        v17 = string.Format("{0} {1} : {2} {3}, {4} {5}", v6, v7, v9, v10, v11, v18);
                        break;
                    case 4: // POLICE - ORG
                    case 5:
                        v17 = string.Format("{0} : {1} {2}, {3} {4}", v8, v9, v10, v11, v18);
                        break;
 
                }

                var dt1 = DateTime.TryParse(dr["AcqDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["AcqDate"].ToString()) : dt0;
                var aqd = dt1 == DateTime.MinValue ? string.Empty : dt1.ToShortDateString();
 
                    var ig1 = dr["Image1"].ToString();
                    var ig2 = dr["Image2"].ToString();
                    var ig3 = dr["Image3"].ToString();
                    var ig4 = dr["Image4"].ToString();
                    var ig5 = dr["Image5"].ToString();
                    var ig6 = dr["Image6"].ToString();

                var t = DateTime.Now.Ticks;

                var img1 = ig1.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthAm), ig1, t) : String.Empty;
                var img2 = ig2.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthAm), ig2, t) : String.Empty;
                var img3 = ig3.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthAm), ig3, t) : String.Empty;
                var img4 = ig4.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthAm), ig4, t) : String.Empty;
                var img5 = ig5.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthAm), ig5, t) : String.Empty;
                var img6 = ig6.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthAm), ig6, t) : String.Empty;

                var im = new ImageModel(i11, img1, img2, img3, img4, img5, img6, ig1, ig2, ig3, ig4, ig5, ig6);
                var ct = new AcctModel(d5, d6, d7, d9, d4, b3, d1, d8, i8, i10, i14, i13);
                var bm = new BoundBookModel(b5, i15, i16, i9, i17, i18, v20, v17, v21, aqd);

                am = new AmmoModel(i12, i1, i2, i3, i4, i5, i6, i7, b1, b2, b6, b7, v1, v16, v3, v4, v5, d2, d3, im, ct, bm);
            }

            return am;
        }


        public AmmoModel GetAmmoInventoryItem(int id)
        {
            var am = new AmmoModel();

            using (var conn = new SqlConnection(GunDbSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAmmoGetInventoryItem");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return am;
                var d0 = 0.00;
                var i0 = 0;
                var b0 = false;
                var dt0 = new DateTime(1900, 1, 1);

                dr.Read();

                var dt1 = DateTime.TryParse(dr["AcqDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["AcqDate"]) : dt0;

                var i1 = Int32.TryParse(dr["TransTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["TransTypeID"]) : i0;
                var i2 = Int32.TryParse(dr["ConditionID"].ToString(), out i0) ? Convert.ToInt32(dr["ConditionID"]) : i0;
                var i3 = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;

                var ix = Int32.TryParse(dr["AmmoGroupID"].ToString(), out i0) ? Convert.ToInt32(dr["AmmoGroupID"]) : i0;

                var d1 = Double.TryParse(dr["UnitCost"].ToString(), out d0) ? Convert.ToDouble(dr["UnitCost"]) : d0;
                var d2 = Double.TryParse(dr["Freight"].ToString(), out d0) ? Convert.ToDouble(dr["Freight"]) : d0;
                var d3 = Double.TryParse(dr["Fees"].ToString(), out d0) ? Convert.ToDouble(dr["Fees"]) : d0;
                var d4 = Double.TryParse(dr["SellerTaxCollAmt"].ToString(), out d0) ? Convert.ToDouble(dr["SellerTaxCollAmt"]) : d0;

                var b1 = Boolean.TryParse(dr["SellerCollectedTax"].ToString(), out b0) ? Convert.ToBoolean(dr["SellerCollectedTax"]) : b0;
                var b2 = ix > 0;

                var v1 = dr["AcqCustName"].ToString();
                var v2 = dr["AcqCustType"].ToString();
                var v3 = dr["AcqFFLNumber"].ToString();
                var v4 = dr["AcqCaAvNumber"].ToString();
                var v5 = dr["AcqCustAddress"].ToString();


                var bb = new BoundBookModel(i1, v1, v2, v3, v4, v5, dt1.ToShortDateString());

                var at = new AcctModel(d1, d2, d3, d4, b1);
                am = new AmmoModel(b2, i2, string.Empty, at, bb);
            }

            return am;
        }

        public void ClearAmmoPic(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAmmoClearPic");

            var param = new SqlParameter("@MasterID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(GunDbSqlConnection, proc, param);
        }




        public TagModel GetAmmoTagData(int ammoId)
        {
            var tm = new TagModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAmmoGetRestockTag");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@InStockID", SqlDbType.Int) { Value = ammoId };
                cmd.Parameters.Add(param); 

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return tm;
                var d0 = 0.00;
                var i0 = 0;
                var b0 = false;

                dr.Read();

                var d1 = Double.TryParse(dr["AskPrice"].ToString(), out d0) ? Convert.ToDouble(dr["AskPrice"]) : d0;
                var d2 = Double.TryParse(dr["ChamberDec"].ToString(), out d0) ? Convert.ToDouble(dr["ChamberDec"]) : d0;
                var d3 = Double.TryParse(dr["ShotSizeSlugWt"].ToString(), out d0) ? Convert.ToDouble(dr["ShotSizeSlugWt"]) : d0;

                var i1 = Int32.TryParse(dr["RoundsPerBox"].ToString(), out i0) ? Convert.ToInt32(dr["RoundsPerBox"]) : i0;
                var i2 = Int32.TryParse(dr["GrainWeight"].ToString(), out i0) ? Convert.ToInt32(dr["GrainWeight"]) : i0;

                var b1 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var b2 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;
                var b3 = Boolean.TryParse(dr["IsShotgun"].ToString(), out b0) ? Convert.ToBoolean(dr["IsShotgun"]) : b0;
                var b4 = Boolean.TryParse(dr["IsSlug"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSlug"]) : b0;

                var v1 = dr["CatName"].ToString();
                var v2 = dr["CaliberName"].ToString();
                var v3 = dr["MfgPartNumber"].ToString();
                var v4 = dr["BulletAbbrev"].ToString();
                var v5 = dr["ManufName"].ToString();
                var v6 = dr["ItemDesc"].ToString();
                var v7 = dr["BulletType"].ToString();

                tm = new TagModel(d1, d2, d3, i1, i2, b1, b2, b3, b4, v1, v2, v3, v4, v5, v6, v7);

            }

            return tm;
        }


        public TagModel GetGunTagData(int isi, int loc, bool isSale)
        {
            var tm = new TagModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGunGetRestockTag");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[3];
                parameters[0] = new SqlParameter("@InStockID", SqlDbType.Int) { Value = isi };
                parameters[1] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = loc };
                parameters[2] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = isSale };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return tm;
                var dt0 = DateTime.MinValue;
                var d0 = 0.00;
                var i0 = 0;
                var b0 = false;

                dr.Read();  

                var i1 = Int32.TryParse(dr["CapacityInt"].ToString(), out i0) ? Convert.ToInt32(dr["CapacityInt"]) : i0;
                var i2 = Int32.TryParse(dr["HiCapCapacity"].ToString(), out i0) ? Convert.ToInt32(dr["HiCapCapacity"]) : i0;
                var i3 = Int32.TryParse(dr["HiCapCount"].ToString(), out i0) ? Convert.ToInt32(dr["HiCapCount"]) : i0;
                var i4 = Int32.TryParse(dr["LocationID"].ToString(), out i0) ? Convert.ToInt32(dr["LocationID"]) : i0;

                var d1 = Double.TryParse(dr["AskPrice"].ToString(), out d0) ? Convert.ToDouble(dr["AskPrice"]) : d0;
                var d2 = Double.TryParse(dr["MSRP"].ToString(), out d0) ? Convert.ToDouble(dr["MSRP"]) : d0;
                var d3 = Double.TryParse(dr["SalePrice"].ToString(), out d0) ? Convert.ToDouble(dr["SalePrice"]) : d0;

                var b1 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;
                var b2 = Boolean.TryParse(dr["Active"].ToString(), out b0) ? Convert.ToBoolean(dr["Active"]) : b0;
                var b3 = Boolean.TryParse(dr["Verified"].ToString(), out b0) ? Convert.ToBoolean(dr["Verified"]) : b0;
                var b4 = Boolean.TryParse(dr["Hide"].ToString(), out b0) ? Convert.ToBoolean(dr["Hide"]) : b0;
                var b5 = Boolean.TryParse(dr["HideCA"].ToString(), out b0) ? Convert.ToBoolean(dr["HideCA"]) : b0;
                var b6 = Boolean.TryParse(dr["IsUsed"].ToString(), out b0) ? Convert.ToBoolean(dr["IsUsed"]) : b0;
                var b7 = Boolean.TryParse(dr["CurrentModel"].ToString(), out b0) ? Convert.ToBoolean(dr["CurrentModel"]) : b0;
                var b8 = Boolean.TryParse(dr["CaOkay"].ToString(), out b0) ? Convert.ToBoolean(dr["CaOkay"]) : b0;
                var b9 = Boolean.TryParse(dr["CaRosterOk"].ToString(), out b0) ? Convert.ToBoolean(dr["CaRosterOk"]) : b0;
                var b10 = Boolean.TryParse(dr["CaSglActnOk"].ToString(), out b0) ? Convert.ToBoolean(dr["CaSglActnOk"]) : b0;
                var b11 = Boolean.TryParse(dr["CaCurioOk"].ToString(), out b0) ? Convert.ToBoolean(dr["CaCurioOk"]) : b0;
                var b12 = Boolean.TryParse(dr["CaSglShotOk"].ToString(), out b0) ? Convert.ToBoolean(dr["CaSglShotOk"]) : b0;
                var b13 = Boolean.TryParse(dr["CaPptOk"].ToString(), out b0) ? Convert.ToBoolean(dr["CaPptOk"]) : b0;

                var v1 = dr["ModelName"].ToString();
                var v2 = dr["FinishName"].ToString();
                var v3 = dr["ConditionName"].ToString();
                var v4 = dr["ActionName"].ToString();
                var v5 = dr["BookMfg"].ToString();
                var v6 = dr["Importer"].ToString();
                var v7 = dr["BookModel"].ToString();
                var v8 = dr["Caliber"].ToString();
                var v9 = dr["GunType"].ToString();


                tm = new TagModel(i1, i2, i3, i4, d1, d2, d3, b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13, v1, v2, v3, v4, v5, v6, v7, v8, v9);

            }

            return tm;
        }                



        #endregion

        #region Merchandise

        //public MerchandiseModel GetMerchAccounting(int id)
        //{
        //    var mm = new MerchandiseModel();

        //    using (var conn = new SqlConnection(GunDbSqlConnection))
        //    {
        //        var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMerchandiseGetAccounting");
        //        var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
        //        conn.Open();

        //        var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
        //        cmd.Parameters.Add(param);

        //        var dr = cmd.ExecuteReader();
        //        if (!dr.HasRows) return mm;
        //        var d0 = 0.00;
        //        var i0 = 0;
        //        var dt0 = new DateTime(1900, 1, 1);

        //        dr.Read();

        //        var i1 = Int32.TryParse(dr["TransTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["TransTypeID"]) : i0;
        //        var i2 = Int32.TryParse(dr["ConditionID"].ToString(), out i0) ? Convert.ToInt32(dr["ConditionID"]) : i0;
        //        var i3 = Int32.TryParse(dr["Units"].ToString(), out i0) ? Convert.ToInt32(dr["Units"]) : i0;

        //        var d1 = Double.TryParse(dr["UnitCost"].ToString(), out d0) ? Convert.ToDouble(dr["UnitCost"]) : d0;
        //        var d2 = Double.TryParse(dr["Freight"].ToString(), out d0) ? Convert.ToDouble(dr["Freight"]) : d0;
        //        var d3 = Double.TryParse(dr["Fees"].ToString(), out d0) ? Convert.ToDouble(dr["Fees"]) : d0;

        //        var at = new AcctModel(d1, d2, d3);
        //        mm = new MerchandiseModel(i1, i2, i3, at);
        //    }

        //    return mm;
        //}




        public MerchandiseModel GetMerchItem(int id, bool isSale)
        {
            var mm = new MerchandiseModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMerchandiseGetItem");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@CBID", SqlDbType.Int) { Value = id };
                parameters[1] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = isSale };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return mm;
                var d0 = 0.00;
                var i0 = 0;
                var b0 = false;
                var dt0 = DateTime.MinValue;

                dr.Read();

                var i1 = Int32.TryParse(dr["TransTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["TransTypeID"]) : i0;
                var i2 = Int32.TryParse(dr["SubCategoryID"].ToString(), out i0) ? Convert.ToInt32(dr["SubCategoryID"]) : i0;
                var i3 = Int32.TryParse(dr["ManufID"].ToString(), out i0) ? Convert.ToInt32(dr["ManufID"]) : i0;
                var i4 = Int32.TryParse(dr["ConditionID"].ToString(), out i0) ? Convert.ToInt32(dr["ConditionID"]) : i0;
                var i5 = Int32.TryParse(dr["UnitsCAL"].ToString(), out i0) ? Convert.ToInt32(dr["UnitsCAL"]) : i0;
                var i6 = Int32.TryParse(dr["UnitsWYO"].ToString(), out i0) ? Convert.ToInt32(dr["UnitsWYO"]) : i0;
                var i7 = Int32.TryParse(dr["ColorID"].ToString(), out i0) ? Convert.ToInt32(dr["ColorID"]) : i0;
                var i8 = Int32.TryParse(dr["ShipSizeID"].ToString(), out i0) ? Convert.ToInt32(dr["ShipSizeID"]) : i0;
                var i9 = Int32.TryParse(dr["UnitsPerBox"].ToString(), out i0) ? Convert.ToInt32(dr["UnitsPerBox"]) : i0;
                var i10 = Int32.TryParse(dr["WeightLb"].ToString(), out i0) ? Convert.ToInt32(dr["WeightLb"]) : i0;
                var i11 = Int32.TryParse(dr["PicRowID"].ToString(), out i0) ? Convert.ToInt32(dr["PicRowID"]) : i0;
                var i12 = Int32.TryParse(dr["InStockID"].ToString(), out i0) ? Convert.ToInt32(dr["InStockID"]) : i0;
                var i13 = Int32.TryParse(dr["CustomerID"].ToString(), out i0) ? Convert.ToInt32(dr["CustomerID"]) : i0;
                var i14 = Int32.TryParse(dr["LocationID"].ToString(), out i0) ? Convert.ToInt32(dr["LocationID"]) : i0;
                var i15 = Int32.TryParse(dr["SupplierID"].ToString(), out i0) ? Convert.ToInt32(dr["SupplierID"]) : i0;

                var i16 = Int32.TryParse(dr["SupplierTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["SupplierTypeID"]) : i0;
                var i17 = Int32.TryParse(dr["FFLCode"].ToString(), out i0) ? Convert.ToInt32(dr["FFLCode"]) : i0;
                var i18 = Int32.TryParse(dr["StateID"].ToString(), out i0) ? Convert.ToInt32(dr["StateID"]) : i0;

                var d1 = Double.TryParse(dr["UnitCost"].ToString(), out d0) ? Convert.ToDouble(dr["UnitCost"]) : d0;
                var d2 = Double.TryParse(dr["Freight"].ToString(), out d0) ? Convert.ToDouble(dr["Freight"]) : d0;
                var d3 = Double.TryParse(dr["Fees"].ToString(), out d0) ? Convert.ToDouble(dr["Fees"]) : d0;
                var d4 = Double.TryParse(dr["SellerTaxAmount"].ToString(), out d0) ? Convert.ToDouble(dr["SellerTaxAmount"]) : d0;
                var d5 = Double.TryParse(dr["AskingPrice"].ToString(), out d0) ? Convert.ToDouble(dr["AskingPrice"]) : d0;
                var d6 = Double.TryParse(dr["WeightOz"].ToString(), out d0) ? Convert.ToDouble(dr["WeightOz"]) : d0;
                var d7 = Double.TryParse(dr["BuyerPricePaid"].ToString(), out d0) ? Convert.ToDouble(dr["BuyerPricePaid"]) : d0;
                var d8 = Double.TryParse(dr["MSRP"].ToString(), out d0) ? Convert.ToDouble(dr["MSRP"]) : d0;

                var b1 = Boolean.TryParse(dr["CaOkay"].ToString(), out b0) ? Convert.ToBoolean(dr["CaOkay"]) : b0;
                var b2 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;
                var b3 = Boolean.TryParse(dr["SellerCollectedTax"].ToString(), out b0) ? Convert.ToBoolean(dr["SellerCollectedTax"]) : b0;
                var b4 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var b5 = Boolean.TryParse(dr["IsActive"].ToString(), out b0) ? Convert.ToBoolean(dr["IsActive"]) : b0;

                var v1 = dr["MfgPartNumber"].ToString();
                var v2 = dr["UpcCode"].ToString();
                var v3 = dr["SearchUpc"].ToString();
                var v4 = dr["ItemDesc"].ToString();
                var v5 = dr["LongDescription"].ToString();
                var v6 = dr["ModelName"].ToString();
                var v7 = dr["TransID"].ToString();

                var n1 = dr["FirstName"].ToString();
                var n2 = dr["LastName"].ToString();
                var n3 = dr["OrgName"].ToString();
                var n4 = dr["SupAddress"].ToString();
                var n5 = dr["SupCity"].ToString();
                var n6 = dr["SupState"].ToString();
                var n7 = dr["SupZipCode"].ToString();
                var n8 = dr["SupZipExt"].ToString();
                var n9 = dr["SupEmail"].ToString();
                var n10 = dr["FFLNumber"].ToString();
                var n11 = dr["CustomerName"].ToString();


                var n12 = string.Empty;
                var n13 = n8.Length > 0 ? n7 + "-" + n8 : n7;
                var n14 = string.Empty;

                if (i17 > 0) //FFL NUMBER
                {
                    var ss = i17.ToString();
                    var a = ss.Substring(0, 1);
                    var b = ss.Substring(1, 2);
                    var c = ss.Substring(3, 5);

                    n14 = string.Format("{0}-{1}-{2}", a, b, c);
                }


                switch (i16)
                {
                    case 1: // FFL
                        n12 = string.Format("{0} {1} {2}, {3} {4} {5}", n3, n4, n5, n6, n13, n14);
                        break;
                    case 2: // C&R FFL
                        n12 = string.Format("{0} {1} : {2}", n1, n2, n10);
                        break;
                    case 3: // PVT PARTY - OWNER COLLECTION
                    case 6:
                        n12 = string.Format("{0} {1} : {2} {3}, {4} {5}", n1, n2, n4, n5, n6, n13);
                        break;
                    case 4: // POLICE - ORG
                    case 5:
                        n12 = string.Format("{0} : {1} {2}, {3} {4}", n3, n4, n5, n6, n13);
                        break;

                }

                var dt1 = DateTime.TryParse(dr["AcqDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["AcqDate"].ToString()) : dt0;
                var aqd = dt1 == DateTime.MinValue ? string.Empty : dt1.ToShortDateString();


                var ig1 = dr["Image1"].ToString();
                var ig2 = dr["Image2"].ToString();
                var ig3 = dr["Image3"].ToString();
                var ig4 = dr["Image4"].ToString();
                var ig5 = dr["Image5"].ToString();
                var ig6 = dr["Image6"].ToString();

                var t = DateTime.Now.Ticks;

                var img1 = ig1.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthMd), ig1, t) : String.Empty;
                var img2 = ig2.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthMd), ig2, t) : String.Empty;
                var img3 = ig3.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthMd), ig3, t) : String.Empty;
                var img4 = ig4.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthMd), ig4, t) : String.Empty;
                var img5 = ig5.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthMd), ig5, t) : String.Empty;
                var img6 = ig6.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(PthMd), ig6, t) : String.Empty;


                var im = new ImageModel(i11, img1, img2, img3, img4, img5, img6, ig1, ig2, ig3, ig4, ig5, ig6);
                var am = new AcctModel(d1, d2, d3, d8, d4, d5, d7, i5, i6, i13, i15, b3);
                var bm = new BoundBookModel(i14, i1, i17, i16, i18, n12, n9, n11, aqd, b4);

                mm = new MerchandiseModel(i12, i1, i2, i3, i4, i7, i8, i9, i10, v1, v2, v3, v4, v5, v6, v7, d6, b1, b2, b5, im, am, bm);
            }

            return mm;
        }

        public TagModel AddMerchandiseItem(MerchandiseModel m)
        {
            var tm = new TagModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMerchandiseCreate");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[39];
                parameters[0] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = m.BookMdl.TransTypeId };
                parameters[1] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.ManufId };
                parameters[2] = new SqlParameter("@SubCatId", SqlDbType.Int) { Value = m.SubCategoryId };
                parameters[3] = new SqlParameter("@ColorID", SqlDbType.Int) { Value = m.ColorId };
                parameters[4] = new SqlParameter("@ConditionID", SqlDbType.Int) { Value = m.ConditionId };
                parameters[5] = new SqlParameter("@ShippingSizeID", SqlDbType.Int) { Value = m.ShippingBoxId };
                parameters[6] = new SqlParameter("@UnitsPerBox", SqlDbType.Int) { Value = m.ItemsPerBox };
                parameters[7] = new SqlParameter("@ShippingLb", SqlDbType.Int) { Value = m.ShippingLbs };
                parameters[8] = new SqlParameter("@AcqSourceID", SqlDbType.Int) { Value = m.BookMdl.AcqTypeId };
                parameters[9] = new SqlParameter("@AcqFFLCode", SqlDbType.Int) { Value = m.BookMdl.AcqFflCode };
                parameters[10] = new SqlParameter("@UnitsCAL", SqlDbType.Int) { Value = m.AcctMdl.UnitsCal };
                parameters[11] = new SqlParameter("@UnitsWYO", SqlDbType.Int) { Value = m.AcctMdl.UnitsWyo };
                parameters[12] = new SqlParameter("@NotForSaleCAL", SqlDbType.Int) { Value = m.AcctMdl.NotForSaleCal };
                parameters[13] = new SqlParameter("@NotForSaleWYO", SqlDbType.Int) { Value = m.AcctMdl.NotForSaleWyo };
                parameters[14] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = m.BookMdl.LocationId };
                parameters[15] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = m.AcctMdl.CustomerId };
                parameters[16] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = m.AcctMdl.SupplierId };
                parameters[17] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = m.AcctMdl.ItemCost };
                parameters[18] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = m.AcctMdl.FreightCost };
                parameters[19] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = m.AcctMdl.ItemFees };
                parameters[20] = new SqlParameter("@AskingPrice", SqlDbType.Decimal) { Value = m.AcctMdl.AskingPrice };
                parameters[21] = new SqlParameter("@Msrp", SqlDbType.Decimal) { Value = m.AcctMdl.Msrp };
                parameters[22] = new SqlParameter("@SellerTaxAmount", SqlDbType.Decimal) { Value = m.AcctMdl.SellerTaxAmount };
                parameters[23] = new SqlParameter("@BuyerPricePaid", SqlDbType.Decimal) { Value = m.AcctMdl.CustPricePaid };
                parameters[24] = new SqlParameter("@ShippingOz", SqlDbType.Decimal) { Value = m.ShippingOzs };
                parameters[25] = new SqlParameter("@ModelName", SqlDbType.VarChar) { Value = m.ModelName };
                parameters[26] = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = m.UpcCode };
                parameters[27] = new SqlParameter("@WebSearchUpc", SqlDbType.VarChar) { Value = m.WebSearchUpc };
                parameters[28] = new SqlParameter("@MfgPartNumber", SqlDbType.VarChar) { Value = m.MfgPartNumber };
                parameters[29] = new SqlParameter("@LongDesc", SqlDbType.VarChar) { Value = m.LongDesc };
                parameters[30] = new SqlParameter("@ItemDesc", SqlDbType.VarChar) { Value = m.ItemDesc };
                parameters[31] = new SqlParameter("@SvcCustName", SqlDbType.VarChar) { Value = m.AcctMdl.SvcCustName == "" ? (object)DBNull.Value : m.AcctMdl.SvcCustName };
                parameters[32] = new SqlParameter("@AcqEmail", SqlDbType.VarChar) { Value = m.BookMdl.AcqEmail == "" ? (object)DBNull.Value : m.BookMdl.AcqEmail };
                parameters[33] = new SqlParameter("@IsOnWeb", SqlDbType.Bit) { Value = m.IsOnWeb };
                parameters[34] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = m.IsActive };
                parameters[35] = new SqlParameter("@CaOkay", SqlDbType.Bit) { Value = m.CaOkay };
                parameters[36] = new SqlParameter("@SellerCollTax", SqlDbType.Bit) { Value = m.AcctMdl.SellerCollectedTax };
                parameters[37] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = m.BookMdl.IsSale };
                parameters[38] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = m.BookMdl.AcqDate == DateTime.MinValue ? (object)DBNull.Value : m.BookMdl.AcqDate };

                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return tm;

                dr.Read();
                var x0 = 0;
                var b0 = false;
                double d0 = 0.00;
                var i1 = Int32.TryParse(dr["MerchID"].ToString(), out x0) ? Convert.ToInt32(dr["MerchID"]) : 0;
                var b1 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var d1 = Double.TryParse(dr["TagPrc"].ToString(), out d0) ? Convert.ToDouble(dr["TagPrc"]) : 0;

                var v1 = dr["TagSku"].ToString();
                var v2 = dr["TagMfg"].ToString();
                var v3 = dr["TagCat"].ToString();
                var v4 = dr["TagCnd"].ToString();
                var v5 = dr["TagMpn"].ToString();
                var v6 = dr["TagDsc"].ToString();
                var v7 = dr["TagSvc"].ToString();
                var v8 = dr["TagNam"].ToString();

                tm = new TagModel(i1, b1, d1, v1, v2, v3, v4, v5, v6, v7, v8);
            }

            return tm;

        }



        //public string MakeMerchSku(int tTypeId)
        //{
        //    var retStr = String.Empty;

        //    using (var conn = new SqlConnection(AdminSqlConnection))
        //    {
        //        var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMakeMerchandiseSku");
        //        var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
        //        conn.Open();

        //        var param = new SqlParameter("@TransTypeId", SqlDbType.Int) { Value = tTypeId };
        //        cmd.Parameters.Add(param);


        //        var dr = cmd.ExecuteReader();
        //        if (!dr.HasRows) return retStr;

        //        dr.Read();

        //        retStr = dr["MerchSku"].ToString();
        //    }

        //    return retStr;
        //}

        public void UpdateInStockImg(int id, int idx, string imgName)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateInStockImage");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@MasterID", DbType.Int32) { Value = id };
            param[1] = new SqlParameter("@Index", DbType.Int32) { Value = idx };
            param[2] = new SqlParameter("@ImageName", DbType.String) { Value = imgName };
            DataProcs.ProcParams(GunDbSqlConnection, proc, param);
        }




        //public void UpdateBookImg(string tid, int idx, string imgName)
        //{
        //    var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateBookImage");

        //    var param = new IDataParameter[3];
        //    param[0] = new SqlParameter("@TransID", DbType.String) { Value = tid };
        //    param[1] = new SqlParameter("@Index", DbType.Int32) { Value = idx };
        //    param[2] = new SqlParameter("@ImageName", DbType.String) { Value = imgName };
        //    DataProcs.ProcParams(GunDbSqlConnection, proc, param);
        //}

        public void UpdateGunImg(int mid, int idx, string imgName)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGunsUpdateImage");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@InStockID", DbType.Int32) { Value = mid };
            param[1] = new SqlParameter("@Index", DbType.Int32) { Value = idx };
            param[2] = new SqlParameter("@ImageName", DbType.String) { Value = imgName };
            DataProcs.ProcParams(AdminSqlConnection, proc, param);
        }

        public void UpdateGunImg(int mid, string imgName)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGunsUpdateInStockImage");

            var param = new IDataParameter[2];
            param[0] = new SqlParameter("@InStockID", DbType.Int32) { Value = mid };
            param[1] = new SqlParameter("@ImageName", DbType.String) { Value = imgName };
            DataProcs.ProcParams(AdminSqlConnection, proc, param);
        }


        public void UpdateInvImg(int id, int idx, string imgName)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateInvImage");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@InStockID", DbType.Int32) { Value = id };
            param[1] = new SqlParameter("@Index", DbType.Int32) { Value = idx };
            param[2] = new SqlParameter("@ImageName", DbType.String) { Value = imgName };
            DataProcs.ProcParams(AdminSqlConnection, proc, param);
        }


        //public void UpdateMerchImg(int id, int idx, string imgName)
        //{
        //    var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateMerchImage");

        //    var param = new IDataParameter[3];
        //    param[0] = new SqlParameter("@InStockID", DbType.Int32) { Value = id };
        //    param[1] = new SqlParameter("@Index", DbType.Int32) { Value = idx };
        //    param[2] = new SqlParameter("@ImageName", DbType.String) { Value = imgName };
        //    DataProcs.ProcParams(AdminSqlConnection, proc, param);
        //}

        public void UpdateAmmoImg(int aid, string imgName)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateAmmoImage");

            var param = new IDataParameter[2];
            param[0] = new SqlParameter("@InStockID", DbType.Int32) { Value = aid };
            param[1] = new SqlParameter("@ImageName", DbType.String) { Value = imgName };
            DataProcs.ProcParams(AdminSqlConnection, proc, param);
        }


        public TagModel UpdateMerchandiseItem(MerchandiseModel m)
        {

            var tm = new TagModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMerchandiseUpdate");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();
                
                var parameters = new IDataParameter[40];
                parameters[0] = new SqlParameter("@CBID", SqlDbType.Int) { Value = m.CostBasisId };
                parameters[1] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = m.BookMdl.TransTypeId };
                parameters[2] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = m.ManufId };
                parameters[3] = new SqlParameter("@SubCatId", SqlDbType.Int) { Value = m.SubCategoryId };
                parameters[4] = new SqlParameter("@ColorID", SqlDbType.Int) { Value = m.ColorId };
                parameters[5] = new SqlParameter("@ConditionID", SqlDbType.Int) { Value = m.ConditionId };
                parameters[6] = new SqlParameter("@ShippingSizeID", SqlDbType.Int) { Value = m.ShippingBoxId };
                parameters[7] = new SqlParameter("@UnitsPerBox", SqlDbType.Int) { Value = m.ItemsPerBox };
                parameters[8] = new SqlParameter("@ShippingLb", SqlDbType.Int) { Value = m.ShippingLbs };
                parameters[9] = new SqlParameter("@ShippingOz", SqlDbType.Decimal) { Value = m.ShippingOzs };
                parameters[10] = new SqlParameter("@UnitsCAL", SqlDbType.Int) { Value = m.AcctMdl.UnitsCal };
                parameters[11] = new SqlParameter("@UnitsWYO", SqlDbType.Int) { Value = m.AcctMdl.UnitsWyo };
                parameters[12] = new SqlParameter("@NotForSaleCAL", SqlDbType.Int) { Value = m.AcctMdl.NotForSaleCal };
                parameters[13] = new SqlParameter("@NotForSaleWYO", SqlDbType.Int) { Value = m.AcctMdl.NotForSaleWyo };
                parameters[14] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = m.BookMdl.LocationId };
                parameters[15] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = m.AcctMdl.CustomerId };
                parameters[16] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = m.AcctMdl.SupplierId };
                parameters[17] = new SqlParameter("@FFLCode", SqlDbType.Int) { Value = m.BookMdl.AcqFflCode };
                parameters[18] = new SqlParameter("@AcqTypeID", SqlDbType.Int) { Value = m.BookMdl.AcqTypeId };
                parameters[19] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = m.AcctMdl.ItemCost };
                parameters[20] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = m.AcctMdl.FreightCost };
                parameters[21] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = m.AcctMdl.ItemFees };
                parameters[22] = new SqlParameter("@AskingPrice", SqlDbType.Decimal) { Value = m.AcctMdl.AskingPrice };
                parameters[23] = new SqlParameter("@SellerTaxAmount", SqlDbType.Decimal) { Value = m.AcctMdl.SellerTaxAmount };
                parameters[24] = new SqlParameter("@BuyerPricePaid", SqlDbType.Decimal) { Value = m.AcctMdl.CustPricePaid };
                parameters[25] = new SqlParameter("@Msrp", SqlDbType.Decimal) { Value = m.AcctMdl.Msrp };
                parameters[26] = new SqlParameter("@ModelName", SqlDbType.VarChar) { Value = m.ModelName };
                parameters[27] = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = m.UpcCode };
                parameters[28] = new SqlParameter("@WebSearchUpc", SqlDbType.VarChar) { Value = m.WebSearchUpc };
                parameters[29] = new SqlParameter("@MfgPartNumber", SqlDbType.VarChar) { Value = m.MfgPartNumber };
                parameters[30] = new SqlParameter("@LongDesc", SqlDbType.VarChar) { Value = m.LongDesc };
                parameters[31] = new SqlParameter("@ItemDesc", SqlDbType.VarChar) { Value = m.ItemDesc };
                parameters[32] = new SqlParameter("@SvcCustName", SqlDbType.VarChar) { Value = m.AcctMdl.SvcCustName == "" ? (object)DBNull.Value : m.AcctMdl.SvcCustName };
                parameters[33] = new SqlParameter("@AcqEmail", SqlDbType.VarChar) { Value = m.BookMdl.AcqEmail == "" ? (object)DBNull.Value : m.BookMdl.AcqEmail };
                parameters[34] = new SqlParameter("@IsOnWeb", SqlDbType.Bit) { Value = m.IsOnWeb };
                parameters[35] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = m.BookMdl.IsSale };
                parameters[36] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = m.IsActive };
                parameters[37] = new SqlParameter("@CaOkay", SqlDbType.Bit) { Value = m.CaOkay };
                parameters[38] = new SqlParameter("@SellerCollTax", SqlDbType.Int) { Value = m.AcctMdl.SellerCollectedTax };
                parameters[39] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = m.BookMdl.AcqDate == DateTime.MinValue ? (object)DBNull.Value : m.BookMdl.AcqDate };

                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }
                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return tm;

                dr.Read();
                var x0 = 0;
                var b0 = false;
                double d0 = 0.00;
                var i1 = Int32.TryParse(dr["InStockID"].ToString(), out x0) ? Convert.ToInt32(dr["InStockID"]) : 0;
                var b1 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var d1 = Double.TryParse(dr["TagPrc"].ToString(), out d0) ? Convert.ToDouble(dr["TagPrc"]) : 0;

                var v1 = dr["TagSku"].ToString();
                var v2 = dr["TagMfg"].ToString();
                var v3 = dr["TagCat"].ToString();
                var v4 = dr["TagCnd"].ToString();
                var v5 = dr["TagMpn"].ToString();
                var v6 = dr["TagDsc"].ToString();
                var v7 = dr["TagSvc"].ToString();
                var v8 = dr["TagNam"].ToString();

                tm = new TagModel(i1, b1, d1, v1, v2, v3, v4, v5, v6, v7, v8);

            }

            return tm;
        }

        public void DeleteMerchandiseItem(int mstId)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMerchandiseDeleteItem");

            var param = new SqlParameter("@MasterID", DbType.Int32) { Value = mstId };
            DataProcs.ProcOneParam(GunDbSqlConnection, proc, param);
        }

        public TagModel RestockMerchandise(MerchandiseModel m)
        {

            var tm = new TagModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMerchandiseRestock");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();
                
                var parameters = new IDataParameter[22];
                parameters[0] = new SqlParameter("@InStockID", SqlDbType.Int) { Value = m.InStockId };
                parameters[1] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = m.TransTypeId };
                parameters[2] = new SqlParameter("@UnitsCAL", SqlDbType.Int) { Value = m.AcctMdl.UnitsCal };
                parameters[3] = new SqlParameter("@UnitsWYO", SqlDbType.Int) { Value = m.AcctMdl.UnitsWyo };
                parameters[4] = new SqlParameter("@NotForSaleCAL", SqlDbType.Int) { Value = m.AcctMdl.NotForSaleCal };
                parameters[5] = new SqlParameter("@NotForSaleWYO", SqlDbType.Int) { Value = m.AcctMdl.NotForSaleWyo };
                parameters[6] = new SqlParameter("@LocationID", SqlDbType.Int) { Value = m.LocationId };
                parameters[7] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = m.AcctMdl.CustomerId };
                parameters[8] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = m.AcctMdl.SupplierId };
                parameters[9] = new SqlParameter("@FFLCode", SqlDbType.Int) { Value = m.BookMdl.AcqFflCode };
                parameters[10] = new SqlParameter("@AcqTypeID", SqlDbType.Int) { Value = m.BookMdl.AcqTypeId };
                parameters[11] = new SqlParameter("@Cost", SqlDbType.Decimal) { Value = m.AcctMdl.ItemCost };
                parameters[12] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = m.AcctMdl.FreightCost };
                parameters[13] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = m.AcctMdl.ItemFees };
                parameters[14] = new SqlParameter("@SellerTaxAmount", SqlDbType.Decimal) { Value = m.AcctMdl.SellerTaxAmount };
                parameters[15] = new SqlParameter("@BuyerPricePaid", SqlDbType.Decimal) { Value = m.AcctMdl.CustPricePaid };
                parameters[16] = new SqlParameter("@SellerCollTax", SqlDbType.Bit) { Value = m.AcctMdl.SellerCollectedTax };
                parameters[17] = new SqlParameter("@IsOnWeb", SqlDbType.Bit) { Value = m.IsOnWeb };
                parameters[18] = new SqlParameter("@IsSale", SqlDbType.Bit) { Value = m.BookMdl.IsSale };
                parameters[19] = new SqlParameter("@SvcCustName", SqlDbType.VarChar) { Value = m.AcctMdl.SvcCustName == "" ? (object)DBNull.Value : m.AcctMdl.SvcCustName };
                parameters[20] = new SqlParameter("@AcqEmail", SqlDbType.VarChar) { Value = m.BookMdl.AcqEmail == "" ? (object)DBNull.Value : m.BookMdl.AcqEmail };
                parameters[21] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = m.BookMdl.AcqDate == DateTime.MinValue ? (object)DBNull.Value : m.BookMdl.AcqDate };


                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return tm;

                dr.Read();
                var x0 = 0;
                var b0 = false;
                double d0 = 0.00;
                var i1 = Int32.TryParse(dr["MerchID"].ToString(), out x0) ? Convert.ToInt32(dr["MerchID"]) : 0;
                var d1 = Double.TryParse(dr["TagPrc"].ToString(), out d0) ? Convert.ToDouble(dr["TagPrc"]) : 0;
                var b1 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var v1 = dr["TagMfg"].ToString();
                var v2 = dr["TagCat"].ToString();
                var v3 = dr["TagDsc"].ToString();
                var v4 = dr["TagCnd"].ToString();
                var v5 = dr["TagMpn"].ToString();
                var v6 = dr["TagSku"].ToString();
                var v7 = dr["TagSvc"].ToString();
                var v8 = dr["TagNam"].ToString();

                tm = new TagModel(i1, d1, b1, v1, v2, v3, v4, v5, v6, v7, v8);
            }

            return tm;
        }

        //public void ClearPic(int id, int imgId)
        //{
        //    var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMerchandiseClearPic");

        //    var param = new IDataParameter[2];
        //    param[0] = new SqlParameter("@ID", DbType.Int32) { Value = id };
        //    param[1] = new SqlParameter("@ImageID", DbType.Int32) { Value = imgId };
        //    DataProcs.ProcParams(GunDbSqlConnection, proc, param);
        //}

        public ImageModel ClearPic(int id, int idx, SiteImageSections s)
        {
            var im = new ImageModel();

            using (var conn = new SqlConnection(GunDbSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDeleteInStockPic");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@InStockID", DbType.Int32) { Value = id };
                parameters[1] = new SqlParameter("@Index", DbType.Int32) { Value = idx };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return im;

                dr.Read();
                var ig1 = dr["Image1"].ToString();
                var ig2 = dr["Image2"].ToString();
                var ig3 = dr["Image3"].ToString();
                var ig4 = dr["Image4"].ToString();
                var ig5 = dr["Image5"].ToString();
                var ig6 = dr["Image6"].ToString();

                var t = DateTime.Now.Ticks;
                var dir = string.Empty;

                switch (s)
                {
                    case SiteImageSections.Guns:
                        dir = PthGn;
                        break;
                    case SiteImageSections.Ammunition:
                        dir = PthAm;
                        break;
                    case SiteImageSections.Merchandise:
                        dir = PthMd;
                        break;

                }

                var img1 = ig1.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(dir), ig1, t) : String.Empty;
                var img2 = ig2.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(dir), ig2, t) : String.Empty;
                var img3 = ig3.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(dir), ig3, t) : String.Empty;
                var img4 = ig4.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(dir), ig4, t) : String.Empty;
                var img5 = ig5.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(dir), ig5, t) : String.Empty;
                var img6 = ig6.Length > 0 ? string.Format("{0}/{1}/{2}/{3}?{4}", GetHostUrl(), DecryptIt(BPathDir), DecryptIt(dir), ig6, t) : String.Empty;

                im = new ImageModel(id, img1, img2, img3, img4, img5, img6, ig1, ig2, ig3, ig4, ig5, ig6); 
            }

            return im;
        }


        public TagModel GetMerchTagData(int merchId)
        {
            var tm = new TagModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMerchandiseGetRestockTag");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@InStockID", SqlDbType.Int) { Value = merchId };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return tm;
                var d0 = 0.00;
                var b0 = false;

                dr.Read();


                var d1 = Double.TryParse(dr["AskPrice"].ToString(), out d0) ? Convert.ToDouble(dr["AskPrice"]) : d0;
                var b1 = Boolean.TryParse(dr["IsSale"].ToString(), out b0) ? Convert.ToBoolean(dr["IsSale"]) : b0;
                var b2 = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;

                var v1 = dr["ManufName"].ToString();
                var v2 = dr["Category"].ToString();
                var v3 = dr["ItemDesc"].ToString();
                var v4 = dr["Condition"].ToString();
                var v5 = dr["MPN"].ToString();
                var v6 = dr["UpcCode"].ToString();
                var v7 = dr["LongDescription"].ToString();

                tm = new TagModel(d1, b1, b2, v1, v2, v3, v4, v5, v6, v7);
            }

            return tm;
        }        


        #endregion


        public AddMenuItemModel AddOtherManufacturer(string mfgName, int sectionId)
        {
            var mi = new AddMenuItemModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAddOtherManufacturer");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@NewMfg", SqlDbType.VarChar) { Value = mfgName };
                parameters[1] = new SqlParameter("@SectionID", SqlDbType.Int) { Value = sectionId };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return mi;

                dr.Read();

                var x0 = 0;
                var ct = Int32.TryParse(dr["Count"].ToString(), out x0) ? Convert.ToInt32(dr["Count"]) : 0;
                var res = ct > 0;

                mi.IsDuplicate = res;

                if (!res)
                {
                    dr.NextResult();
                    if (dr.HasRows)
                    {
                        mi.Manuf = new List<GunManufMenu>();

                        while (dr.Read())
                        {
                            var newId = Int32.TryParse(dr["NewID"].ToString(), out x0) ? Convert.ToInt32(dr["NewID"]) : 0;
                            var mfgId = Int32.TryParse(dr["ManufID"].ToString(), out x0) ? Convert.ToInt32(dr["ManufID"]) : 0;
                            var mfg = dr["ManufName"].ToString();


                            var gm = new GunManufMenu();
                            gm.SelectedManufId = newId;
                            gm.ManufId = mfgId;
                            gm.ManufName = mfg;
                            mi.Manuf.Add(gm);
                        }
                    }
                }
            }

            return mi;
        }

        public AddMenuItemModel AddGunManufacturer(string mfgName, bool isImport)
        {
            var mi = new AddMenuItemModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAddManufacturer");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@NewMfg", SqlDbType.VarChar) { Value = mfgName };
                parameters[1] = new SqlParameter("@IsImporter", SqlDbType.Bit) { Value = isImport };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return mi;

                dr.Read();

                var x0 = 0;
                var ct = Int32.TryParse(dr["Count"].ToString(), out x0) ? Convert.ToInt32(dr["Count"]) : 0;
                var res = ct > 0;

                mi.IsDuplicate = res;

                if (!res)
                {
                    dr.NextResult();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        var selId = Int32.TryParse(dr["NewID"].ToString(), out x0) ? Convert.ToInt32(dr["NewID"]) : 0;
                        mi.SelectedId = selId;

                        dr.NextResult();

                        mi.Manuf = new List<GunManufMenu>();

                        while (dr.Read())
                        {
                            var mfgId = Int32.TryParse(dr["ManufID"].ToString(), out x0) ? Convert.ToInt32(dr["ManufID"]) : 0;
                            var mfg = dr["ManufName"].ToString();

                            var gm = new GunManufMenu();
                            gm.ManufId = mfgId;
                            gm.ManufName = mfg;
                            mi.Manuf.Add(gm);
                        }
                    }
                }
            }

            return mi;
        }

        public AddMenuItemModel AddCaliber(string calName, int standId)
        {
            

            var mi = new AddMenuItemModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAddCaliber");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@NewCal", SqlDbType.VarChar) { Value = calName };
                parameters[1] = new SqlParameter("@SortCat", SqlDbType.Int) { Value = standId };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return mi;

                dr.Read();

                var x0 = 0;
                var ct = Int32.TryParse(dr["Count"].ToString(), out x0) ? Convert.ToInt32(dr["Count"]) : 0;
                var res = ct > 0;

                mi.IsDuplicate = res;

                if (!res)
                {
                    dr.NextResult();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        var selId = Int32.TryParse(dr["NewID"].ToString(), out x0) ? Convert.ToInt32(dr["NewID"]) : 0;
                        mi.SelectedId = selId;

                        dr.NextResult();

                        mi.Caliber = new List<GunCaliberMenu>();

                        while (dr.Read())
                        {
                            var calId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                            var cal = dr["CaliberName"].ToString();

                            var gm = new GunCaliberMenu();
                            gm.CaliberId = calId;
                            gm.CaliberName = cal;
                            mi.Caliber.Add(gm);
                        }
                    }
                }
            }

            return mi;

        }

        public AddMenuItemModel AddColor(string color)
        {


            var mi = new AddMenuItemModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAddColor");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[1];
                parameters[0] = new SqlParameter("@ColorName", SqlDbType.VarChar) { Value = color };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return mi;

                dr.Read();

                var x0 = 0;
                var ct = Int32.TryParse(dr["Count"].ToString(), out x0) ? Convert.ToInt32(dr["Count"]) : 0;
                var res = ct > 0;
                mi.IsDuplicate = res;

                if (!res)
                {
                    dr.NextResult();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        var selId = Int32.TryParse(dr["NewID"].ToString(), out x0) ? Convert.ToInt32(dr["NewID"]) : 0;
                        mi.SelectedId = selId;

                        dr.NextResult();

                        mi.Color = new List<ColorMenu>();

                        while (dr.Read())
                        {
                            var id = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                            var clrName = dr["ColorName"].ToString();

                            var cm = new ColorMenu();
                            cm.ColorId = id;
                            cm.ColorName = clrName;
                            mi.Color.Add(cm);
                        }
                    }
                }
            }

            return mi;

        }

        public AddMenuItemModel AddBulletType(string bulletType, string code)
        {

            var mi = new AddMenuItemModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAddBulletType");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@BulletType", SqlDbType.VarChar) { Value = bulletType };
                parameters[1] = new SqlParameter("@ShortCode", SqlDbType.VarChar) { Value = code };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return mi;

                dr.Read();

                var x0 = 0;
                var ct = Int32.TryParse(dr["Count"].ToString(), out x0) ? Convert.ToInt32(dr["Count"]) : 0;
                var res = ct > 0;

                mi.IsDuplicate = res;

                if (!res)
                {
                    dr.NextResult();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        var selId = Int32.TryParse(dr["NewID"].ToString(), out x0) ? Convert.ToInt32(dr["NewID"]) : 0;
                        mi.SelectedId = selId;

                        dr.NextResult();

                        mi.Bullet = new List<BulletTypeMenu>();

                        while (dr.Read())
                        {
                            var bId = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                            var bName = dr["Bullet"].ToString();

                            var b = new BulletTypeMenu();
                            b.BulletTypeId = bId;
                            b.BulletFullName = bName;
                            mi.Bullet.Add(b);
                        }
                    }
                }
            }

            return mi;

        }


        public string MakeOtherSku()
        {
            var retStr = String.Empty;

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMakeOtherSku");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return retStr;

                dr.Read();

                retStr = dr["OtherSku"].ToString();
            }

            return retStr;
        }


        public string MakeNewTransCode(int gunTypeId, int transId)
        {
            var retStr = String.Empty;

            using (var conn = new SqlConnection(ComplSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetNewTransCode");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = gunTypeId };
                parameters[1] = new SqlParameter("@TransTypeID", SqlDbType.VarChar) { Value = transId };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return retStr;

                dr.Read();

                retStr = dr["BookCode"].ToString();
            }

            return retStr;
        }

        //public string AddBookEntry(AddToBookModel a)
        //{
        //    var sku = string.Empty;

        //    using (var conn = new SqlConnection(ComplSqlConnection))
        //    {
        //        var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGunBookIn");
        //        var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
        //        conn.Open();

        //        cmd = AddParameters(cmd, GunBasicParams(a.Gun));
        //        cmd = AddParameters(cmd, AccountingParams(a.Accounting));
        //        cmd = AddParameters(cmd, ComplianceParams(a.Compliance));
        //        cmd = AddParameters(cmd, BoundBookParams(a.BoundBook));

        //        var dr = cmd.ExecuteReader();
        //        if (!dr.HasRows) { return sku; }

        //        dr.Read();

        //        sku = dr["SKU"].ToString();
        //    }

        //    return sku;
        //}

        public string CookUpcCode()
        {
            var upc = string.Empty;

            using (var conn = new SqlConnection(GunDbSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGenerateUpcCode");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) { return upc; }

                dr.Read();
                upc = dr["NewUpcCode"].ToString();
            }

            return upc;
        }

        public bool CheckUpcCode(string upcCode)
        {
            var duplicate = false;

            using (var conn = new SqlConnection(GunDbSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCheckUpcCode");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var p = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = upcCode };
                cmd.Parameters.Add(p);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) { return duplicate; }

                dr.Read();
                var x0 = 0;
                var upcCount = Int32.TryParse(dr["ItemCount"].ToString(), out x0) ? Convert.ToInt32(dr["ItemCount"]) : 0;
                duplicate = upcCount > 0;
            }

            return duplicate;
        }

        public void DeleteInStock(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDeleteInStock");

            var param = new SqlParameter("@ID", DbType.Int32) { Value = id };
            DataProcs.ProcOneParam(AdminSqlConnection, proc, param);
        }



        public SqlCommand AddParameters(SqlCommand cmd, IDataParameter[] p)
        {
            foreach (var parameter in p)
            {
                try { cmd.Parameters.Add(parameter); }
                catch (Exception e) { throw new Exception(e.ToString()); }
            }

            return cmd;
        }
 
 
        private IDataParameter[] GunBasicParams(GunModel gm)
        {
            var param = new IDataParameter[21];
            param[0] = new SqlParameter("@InquiryGunID", SqlDbType.Int) { Value = gm.InqGunId };
            param[1] = new SqlParameter("@ActionID", SqlDbType.Int) { Value = gm.ActionId };
            param[2] = new SqlParameter("@FinishID", SqlDbType.Int) { Value = gm.FinishId };
            param[3] = new SqlParameter("@CapacityInt", SqlDbType.Int) { Value = gm.CapacityInt };
            param[4] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = gm.ManufId };
            param[5] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = gm.CaliberId };
            param[6] = new SqlParameter("@ConditionID", SqlDbType.Int) { Value = gm.ConditionId };
            param[7] = new SqlParameter("@WeightLb", SqlDbType.Int) { Value = gm.WeightLb };
            param[8] = new SqlParameter("@BarrelDec", SqlDbType.Decimal) { Value = gm.BarrelDec };
            param[9] = new SqlParameter("@WeightOz", SqlDbType.Decimal) { Value = gm.WeightOz };
            param[10] = new SqlParameter("@ChamberDec", SqlDbType.Decimal) { Value = gm.ChamberDec };
            param[11] = new SqlParameter("@OverallDec", SqlDbType.Decimal) { Value = gm.OverallDec };
            param[12] = new SqlParameter("@IsUsed", SqlDbType.Bit) { Value = gm.IsUsed };
            param[13] = new SqlParameter("@OriginalBox", SqlDbType.Bit) { Value = gm.OrigBox };
            param[14] = new SqlParameter("@OriginalPapers", SqlDbType.Bit) { Value = gm.OrigPaperwork };
            param[15] = new SqlParameter("@IsOnWeb", SqlDbType.Bit) { Value = gm.IsOnWeb };
            param[16] = new SqlParameter("@MfgPartNumber", SqlDbType.VarChar) { Value = gm.MfgPartNumber };
            param[17] = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = gm.UpcCode };
            param[18] = new SqlParameter("@ModelName", SqlDbType.VarChar) { Value = gm.ModelName };
            param[19] = new SqlParameter("@Description", SqlDbType.VarChar) { Value = gm.Description };
            param[20] = new SqlParameter("@LongDesc", SqlDbType.VarChar) { Value = gm.LongDescription };

            return param;
        }


        private IDataParameter[] AccountingParams(AcctModel am)
        {
            var param = new IDataParameter[6];
            param[0] = new SqlParameter("@CaTaxEmpt", SqlDbType.Bit) { Value = am.CaTaxExempt };
            param[1] = new SqlParameter("@SellerCollTax", SqlDbType.Bit) { Value = am.SellerCollectedTax };
            param[2] = new SqlParameter("@ItemCost", SqlDbType.Decimal) { Value = am.ItemCost };
            param[3] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = am.FreightCost };
            param[4] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = am.ItemFees };
            param[5] = new SqlParameter("@SellerCollTaxAmt", SqlDbType.Decimal) { Value = am.SellerTaxAmount };

            return param;
        }

        private IDataParameter[] ComplianceParams(CaRestrictModel rm)
        {
            var param = new IDataParameter[8];
            param[0] = new SqlParameter("@Cflc", SqlDbType.VarChar) { Value = rm.CflcInbound };
            param[1] = new SqlParameter("@IsPPT", SqlDbType.Bit) { Value = rm.CaPptOk };
            param[2] = new SqlParameter("@HoldGun", SqlDbType.Bit) { Value = rm.HoldGun };
            param[3] = new SqlParameter("@HoldGunExp", SqlDbType.DateTime) { Value = rm.HoldGunExpires == DateTime.MinValue ? (object)DBNull.Value : rm.HoldGunExpires };
            param[4] = new SqlParameter("@HiCapMagCt", SqlDbType.Int) { Value = rm.HiCapMagCount };
            param[5] = new SqlParameter("@HiCapCapacity", SqlDbType.Int) { Value = rm.HiCapCapacity };
            param[6] = new SqlParameter("@LockMakeID", SqlDbType.Int) { Value = rm.LockMakeId };
            param[7] = new SqlParameter("@LockModelID", SqlDbType.Int) { Value = rm.LockModelId };


            return param;
        }

        private IDataParameter[] BoundBookParams(BoundBookModel bb)
        {
            var param = new IDataParameter[22];
            param[0] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = bb.TransTypeId };
            param[1] = new SqlParameter("@AcqTypeID", SqlDbType.Int) { Value = bb.AcqTypeId };
            param[2] = new SqlParameter("@FFLID", SqlDbType.Int) { Value = bb.AcqFflId };
            param[3] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = bb.GunTypeId };
            param[4] = new SqlParameter("@FFLName", SqlDbType.VarChar) { Value = bb.AcqFflName };
            param[5] = new SqlParameter("@FFLNumber", SqlDbType.VarChar) { Value = bb.AcqFflNumber };
            param[6] = new SqlParameter("@CurioFFLExp", SqlDbType.VarChar) { Value = bb.CurioExpDate };
            param[7] = new SqlParameter("@OrgName", SqlDbType.VarChar) { Value = bb.AcqOrgName };
            param[8] = new SqlParameter("@FirstName", SqlDbType.VarChar) { Value = bb.AcqFirstName };
            param[9] = new SqlParameter("@LastName", SqlDbType.VarChar) { Value = bb.AcqLastName };
            param[10] = new SqlParameter("@Address", SqlDbType.VarChar) { Value = bb.AcqAddress };
            param[11] = new SqlParameter("@City", SqlDbType.VarChar) { Value = bb.AcqCity };
            param[12] = new SqlParameter("@State", SqlDbType.VarChar) { Value = bb.AcqState };
            param[13] = new SqlParameter("@ZipCode", SqlDbType.VarChar) { Value = bb.AcqZipCode };
            param[14] = new SqlParameter("@ZipExt", SqlDbType.VarChar) { Value = bb.AcqZipExt };
            param[15] = new SqlParameter("@GunMfg", SqlDbType.VarChar) { Value = bb.GunMfg };
            param[16] = new SqlParameter("@GunImpt", SqlDbType.VarChar) { Value = bb.GunImpt };
            param[17] = new SqlParameter("@GunModel", SqlDbType.VarChar) { Value = bb.GunModelName };
            param[18] = new SqlParameter("@GunSerial", SqlDbType.VarChar) { Value = bb.GunSerial };
            param[19] = new SqlParameter("@GunType", SqlDbType.VarChar) { Value = bb.GunType };
            param[20] = new SqlParameter("@GunCaliber", SqlDbType.VarChar) { Value = bb.GunCaliber };
            param[21] = new SqlParameter("@AcqDate", SqlDbType.DateTime) { Value = bb.AcqDate == DateTime.MinValue ? (object)DBNull.Value : bb.AcqDate };

            return param;
        }

        private IDataParameter[] GunParams(AddToBookModel ab)
        {
            var g = ab.Gun;
            var bm = ab.BoundBook;
            var cm = ab.Compliance;

            var param = new IDataParameter[33];
            param[0] = new SqlParameter("@TransSku", SqlDbType.VarChar) { Value = ab.BoundBook.TagSku };
            param[1] = new SqlParameter("@UpcCode", SqlDbType.VarChar) { Value = g.UpcCode };
            param[2] = new SqlParameter("@MfgNumber", SqlDbType.VarChar) { Value = g.MfgPartNumber };
            param[3] = new SqlParameter("@Desc", SqlDbType.VarChar) { Value = g.Description };
            param[4] = new SqlParameter("@LongDesc", SqlDbType.VarChar) { Value = g.LongDescription };
            param[5] = new SqlParameter("@Model", SqlDbType.VarChar) { Value = g.ModelName };
            param[6] = new SqlParameter("@GunTypeID", SqlDbType.Int) { Value = g.GunTypeId };
            param[7] = new SqlParameter("@ManufID", SqlDbType.Int) { Value = g.ManufId };
            param[8] = new SqlParameter("@CaliberID", SqlDbType.Int) { Value = g.CaliberId };
            param[9] = new SqlParameter("@ActionID", SqlDbType.Int) { Value = g.ActionId };
            param[10] = new SqlParameter("@FinishID", SqlDbType.Int) { Value = g.FinishId };
            param[11] = new SqlParameter("@CondID", SqlDbType.Int) { Value = g.ConditionId };
            param[12] = new SqlParameter("@WeightLb", SqlDbType.Int) { Value = g.WeightLb };
            param[13] = new SqlParameter("@CapacityInt", SqlDbType.Int) { Value = g.CapacityInt };
            param[14] = new SqlParameter("@BarrelDec", SqlDbType.Decimal) { Value = g.BarrelDec };
            param[15] = new SqlParameter("@OverallDec", SqlDbType.Decimal) { Value = g.OverallDec };
            param[16] = new SqlParameter("@ChamberDec", SqlDbType.Decimal) { Value = g.ChamberDec };
            param[17] = new SqlParameter("@WeightOz", SqlDbType.Decimal) { Value = g.WeightOz };
            //param[18] = new SqlParameter("@PriceAsk", SqlDbType.Decimal) { Value = g.AskingPrice };
            //param[19] = new SqlParameter("@PriceMsrp", SqlDbType.Decimal) { Value = g.Msrp };
            //param[20] = new SqlParameter("@PriceSale", SqlDbType.Decimal) { Value = g.SalePrice };
            //param[21] = new SqlParameter("@UnitCost", SqlDbType.Decimal) { Value = g.UnitCost };
            //param[22] = new SqlParameter("@Freight", SqlDbType.Decimal) { Value = g.Freight };
            //param[23] = new SqlParameter("@Fees", SqlDbType.Decimal) { Value = g.Fees };
            param[18] = new SqlParameter("@InProd", SqlDbType.Bit) { Value = g.InProduction };
            param[19] = new SqlParameter("@IsUsed", SqlDbType.Bit) { Value = g.IsUsed };
            param[20] = new SqlParameter("@IsHidden", SqlDbType.Bit) { Value = g.IsHidden };
            param[21] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = g.IsActive };
            param[22] = new SqlParameter("@IsVerified", SqlDbType.Bit) { Value = g.IsVerified };
            param[23] = new SqlParameter("@OrigBox", SqlDbType.Bit) { Value = g.OrigBox };
            param[24] = new SqlParameter("@HasPpw", SqlDbType.Bit) { Value = g.OrigPaperwork };
            param[25] = new SqlParameter("@CaHide", SqlDbType.Bit) { Value = cm.CaHide };
            param[26] = new SqlParameter("@CaLegal", SqlDbType.Bit) { Value = cm.CaOkay };
            param[27] = new SqlParameter("@CaRoster", SqlDbType.Bit) { Value = cm.CaRosterOk };
            param[28] = new SqlParameter("@CaCurio", SqlDbType.Bit) { Value = cm.CaCurioOk };
            param[29] = new SqlParameter("@CaSaRev", SqlDbType.Bit) { Value = cm.CaSglActnOk };
            param[30] = new SqlParameter("@CaSsPst", SqlDbType.Bit) { Value = cm.CaSglShotOk };
            param[31] = new SqlParameter("@CaPpt", SqlDbType.Bit) { Value = cm.CaPptOk };
            param[32] = new SqlParameter("@TransTypeID", SqlDbType.Int) { Value = bm.TransTypeId };


            return param;
        }


    }
}