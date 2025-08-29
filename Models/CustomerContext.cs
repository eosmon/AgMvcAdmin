using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using AgBase;
using AgMvcAdmin.Common;
using AgMvcAdmin.Controllers;
using AgMvcAdmin.Models.Common;
using AgMvcAdmin.Models.Menus;
using WebBase.Configuration;

namespace AgMvcAdmin.Models
{
    public class CustomerContext : BaseModel
    {

        public CustomerModel AddBaseCustomer(CustomerModel c)
        {

            var pr = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerAddBase");
            var bp = SetBaseParams(c);

            bp.Add(new SqlParameter("@ID", SqlDbType.Int) { Direction = ParameterDirection.Output });
            bp.Add(new SqlParameter("@DE", SqlDbType.Bit) { Direction = ParameterDirection.Output });
            bp.Add(new SqlParameter("@UK", SqlDbType.UniqueIdentifier) { Direction = ParameterDirection.Output });

            try
            {
                var gp = new Calls.GroupParams();
                gp = Calls.ProcParamsGroup(AdminSqlConnection, pr, bp);
                c.CustomerId = gp.RetInt;
                c.IsDupErr = gp.RetBool;
                c.SecurityBase.UserKey = gp.RetGuid;
                c.SecurityBase.StrUserKey = gp.RetGuid.ToString();
                c.SecurityBase.EncryUk = EncryptIt(c.SecurityBase.StrUserKey);
            }
            catch (Exception exc) { throw exc; }

            return c;
        }


        public CustomerModel UpdateBaseCustomer(CustomerModel c)
        {

            var pr = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerUpdateBase");
            var bp = SetBaseParams(c);

            bp.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = c.CustomerId });

            try { AgBase.Calls.ProcParams(AdminSqlConnection, pr, bp); }
            catch (Exception exc) { throw exc; }

            return c;
        }



        public CustomerModel AddCustomer(CustomerModel c)
        {
            c = AddBaseCustomer(c);
            var bp = new List<SqlParameter>();
            var proc = String.Empty;

            switch (c.CustomerType)
            {
                case CustomerTypes.PrivateParty:
                case CustomerTypes.OtherBiz:
                    bp = SetPvtPartyParams(c);
                    proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerAddPvtParty");
                    break;
                case CustomerTypes.CommercialFFL:
                    bp = SetFFLParams(c);
                    proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerAddFFL");
                    break;
                case CustomerTypes.CurioRelic:
                    bp = SetCurioRelicParams(c);
                    proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerAddCurioRelic");
                    break;
                case CustomerTypes.LawEnforcement:
                    bp = SetLEOParams(c);
                    proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerAddLEO");
                    break;
                default:
                    proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerAddBase");
                    break;


            }

            try  { Calls.ProcParams(AdminSqlConnection, proc, bp); }
            catch (Exception exc) { throw exc; }

            return c;
        }

        public int AddNewSupplier(SupplierModel s)
        {
            var sid = 0;
            var x0 = 0;

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSupplierAdd");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[18];
                parameters[0] = new SqlParameter("@SupplierTypeID", SqlDbType.Int) { Value = s.SupplerTypeId };
                parameters[1] = new SqlParameter("@StateID", SqlDbType.Int) { Value = s.StateId };
                parameters[2] = new SqlParameter("@IdType", SqlDbType.Int) { Value = s.IdType };
                parameters[3] = new SqlParameter("@IdState", SqlDbType.Int) { Value = s.IdState };
                parameters[4] = new SqlParameter("@FirstName", SqlDbType.VarChar) { Value = s.FirstName };
                parameters[5] = new SqlParameter("@LastName", SqlDbType.VarChar) { Value = s.LastName };
                parameters[6] = new SqlParameter("@OrgName", SqlDbType.VarChar) { Value = (object)s.OrgName ?? DBNull.Value };
                parameters[7] = new SqlParameter("@Address", SqlDbType.VarChar) { Value = s.Address };
                parameters[8] = new SqlParameter("@City", SqlDbType.VarChar) { Value = s.City };
                parameters[9] = new SqlParameter("@ZipCode", SqlDbType.VarChar) { Value = s.ZipCode };
                parameters[10] = new SqlParameter("@ZipExt", SqlDbType.VarChar) { Value = s.ZipExt };
                parameters[11] = new SqlParameter("@Phone", SqlDbType.VarChar) { Value = s.Phone };
                parameters[12] = new SqlParameter("@Email", SqlDbType.VarChar) { Value = (object)s.Email ?? DBNull.Value };
                parameters[13] = new SqlParameter("@IdNumber", SqlDbType.VarChar) { Value = (object)s.IdNumber ?? DBNull.Value };
                parameters[14] = new SqlParameter("@CurFFL", SqlDbType.VarChar) { Value = (object)s.CurFfl ?? DBNull.Value };
                parameters[15] = new SqlParameter("@CurExp", SqlDbType.DateTime) { Value = s.CurExp == DateTime.MinValue ? (object)DBNull.Value : s.CurExp };
                parameters[16] = new SqlParameter("@IdDOB", SqlDbType.DateTime) { Value = s.IdDob == DateTime.MinValue ? (object)DBNull.Value : s.IdDob };
                parameters[17] = new SqlParameter("@IdExp", SqlDbType.DateTime) { Value = s.IdExpires == DateTime.MinValue ? (object)DBNull.Value : s.IdExpires };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return sid;

                dr.Read();
                sid = Int32.TryParse(dr["SupplierID"].ToString(), out x0) ? Convert.ToInt32(dr["SupplierID"]) : 0;
            }

            return sid;
        }


        public void UpdateSupplier(SupplierModel s)
        {
            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSupplierUpdate");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[18];
                parameters[0] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = s.Id };
                parameters[1] = new SqlParameter("@StateID", SqlDbType.Int) { Value = s.StateId };
                parameters[2] = new SqlParameter("@IdType", SqlDbType.Int) { Value = s.IdType };
                parameters[3] = new SqlParameter("@IdState", SqlDbType.Int) { Value = s.IdState };
                parameters[4] = new SqlParameter("@FirstName", SqlDbType.VarChar) { Value = s.FirstName };
                parameters[5] = new SqlParameter("@LastName", SqlDbType.VarChar) { Value = s.LastName };
                parameters[6] = new SqlParameter("@OrgName", SqlDbType.VarChar) { Value = (object)s.OrgName ?? DBNull.Value };
                parameters[7] = new SqlParameter("@Address", SqlDbType.VarChar) { Value = s.Address };
                parameters[8] = new SqlParameter("@City", SqlDbType.VarChar) { Value = s.City };
                parameters[9] = new SqlParameter("@ZipCode", SqlDbType.VarChar) { Value = s.ZipCode };
                parameters[10] = new SqlParameter("@ZipExt", SqlDbType.VarChar) { Value = s.ZipExt };
                parameters[11] = new SqlParameter("@Phone", SqlDbType.VarChar) { Value = s.Phone };
                parameters[12] = new SqlParameter("@Email", SqlDbType.VarChar) { Value = (object)s.Email ?? DBNull.Value };
                parameters[13] = new SqlParameter("@IdNumber", SqlDbType.VarChar) { Value = (object)s.IdNumber ?? DBNull.Value };
                parameters[14] = new SqlParameter("@CurFFL", SqlDbType.VarChar) { Value = (object)s.CurFfl ?? DBNull.Value };
                parameters[15] = new SqlParameter("@CurExp", SqlDbType.DateTime) { Value = s.CurExp == DateTime.MinValue ? (object)DBNull.Value : s.CurExp };
                parameters[16] = new SqlParameter("@IdDOB", SqlDbType.DateTime) { Value = s.IdDob == DateTime.MinValue ? (object)DBNull.Value : s.IdDob };
                parameters[17] = new SqlParameter("@IdExp", SqlDbType.DateTime) { Value = s.IdExpires == DateTime.MinValue ? (object)DBNull.Value : s.IdExpires };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader(); 
            }
        }



        //public CustomerModel AddCustomer(CustomerModel c, CustomerTypes ct)
        //{

        //    var proc = String.Empty;
        //    var bp = SetBaseParams(c);

        //    switch (ct)
        //    {
        //        case CustomerTypes.PrivateParty:
        //        case CustomerTypes.OtherBiz:
        //            bp = SetPvtPartyParams(bp, c);
        //            proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerAddPvtParty");
        //            break;
        //        case CustomerTypes.CommercialFFL:
        //            bp = SetFFLParams(bp, c);
        //            proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerAddFFL");
        //            break;
        //        case CustomerTypes.CurioRelic:
        //            bp = SetCurioRelicParams(bp, c);
        //            proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerAddCurioRelic");
        //            break;
        //        case CustomerTypes.LawEnforcement:
        //            bp = SetLEOParams(bp, c);
        //            proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerAddLEO");
        //            break;
        //        default:
        //            proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerAddBase");
        //            break;


        //    }

        //    bp.Add(new SqlParameter("@ID", SqlDbType.Int) { Direction = ParameterDirection.Output });
        //    bp.Add(new SqlParameter("@DE", SqlDbType.Bit) { Direction = ParameterDirection.Output });
        //    bp.Add(new SqlParameter("@UK", SqlDbType.UniqueIdentifier) { Direction = ParameterDirection.Output });


        //    try
        //    {
        //        var gp = new Calls.GroupParams();
        //        gp = Calls.ProcParamsGroup(AdminSqlConnection, proc, bp);
        //        c.CustomerId = gp.RetInt;
        //        c.IsDupErr = gp.RetBool;
        //        c.SecurityBase.StrUserKey = gp.RetGuid.ToString();
        //        c.SecurityBase.EncryUk = EncryptIt(c.SecurityBase.StrUserKey);
        //    }
        //    catch (Exception exc) { throw exc; }

        //    return c;
        //}

        //public void UpdateCustReg(CustomerModel c, CustomerTypes ct)
        //{
        //    var proc = String.Empty;
        //    var bp = SetBaseParams(c);
        //    bp.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = c.CustomerId });


        //    switch (ct)
        //    {
        //        case CustomerTypes.PrivateParty:
        //        case CustomerTypes.OtherBiz:
        //            bp = SetPvtPartyParams(bp, c);
        //            proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerUpdatePvtParty"); 
        //            break;
        //        case CustomerTypes.CommercialFFL:
        //            //bp = SetFFLParams(bp, c);
        //            proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerUpdateFFL");
        //            break;
        //        case CustomerTypes.CurioRelic:
        //            bp = SetCurioRelicParams(bp, c);
        //            proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerUpdateCurioRelic");
        //            break;
        //        case CustomerTypes.LawEnforcement:
        //            bp = SetLEOParams(bp, c);
        //            proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerUpdateLEO");
        //            break;
        //        default:
        //            proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerUpdateBase");
        //            break;

        //    }

        //    try { AgBase.Calls.ProcParams(AdminSqlConnection, proc, bp); }
        //    catch (Exception exc) { throw exc; }
        //}


        public void UpdateCustomer(CustomerModel c)
        {
            c = UpdateBaseCustomer(c);

            var proc = String.Empty;
            var bp = new List<SqlParameter>();

            switch (c.CustomerType)
            {
                case CustomerTypes.PrivateParty:
                case CustomerTypes.OtherBiz:
                    bp = SetPvtPartyParams(c);
                    proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerUpdatePvtParty");
                    break;
                case CustomerTypes.CommercialFFL:
                    bp = SetFFLParams(c);
                    proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerUpdateFFL");
                    break;
                case CustomerTypes.CurioRelic:
                    bp = SetCurioRelicParams(c);
                    proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerUpdateCurioRelic");
                    break;
                case CustomerTypes.LawEnforcement:
                    bp = SetLEOParams(c);
                    proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerUpdateLEO");
                    break;
                default:
                    proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerUpdateBase");
                    break;

            }

            try { AgBase.Calls.ProcParams(AdminSqlConnection, proc, bp); }
            catch (Exception exc) { throw exc; }
        }


        private List<SqlParameter> SetBaseParams(CustomerModel c)
        {
            var parameters = new List<SqlParameter>();

            var phn = c.CustomerBase.Phone != null ? PhoneToInt(c.CustomerBase.Phone) : 0;
            var fax = c.Fax != null ? PhoneToInt(c.Fax) : 0;

            parameters.Add(new SqlParameter("@CustomerTypeID", SqlDbType.Int) { Value = c.CustomerTypeId });
            parameters.Add(new SqlParameter("@CustAddressID", SqlDbType.Int) { Value = (object)c.CustomerBase.CustAddressID ?? DBNull.Value });
            parameters.Add(new SqlParameter("@SecAnswer1", SqlDbType.VarChar) { Value = (object)EncryptIt(c.SecurityBase.SecurityAnswer1) ?? DBNull.Value });
            parameters.Add(new SqlParameter("@SecAnswer2", SqlDbType.VarChar) { Value = (object)EncryptIt(c.SecurityBase.SecurityAnswer2) ?? DBNull.Value });
            parameters.Add(new SqlParameter("@SecAnswer3", SqlDbType.VarChar) { Value = (object)EncryptIt(c.SecurityBase.SecurityAnswer3) ?? DBNull.Value });
            parameters.Add(new SqlParameter("@StateID", SqlDbType.Int) { Value = c.CustomerBase.StateId > 0 ? c.CustomerBase.StateId : SqlInt32.Null });
            parameters.Add(new SqlParameter("@ZipCode", SqlDbType.Int) { Value = (object)c.CustomerBase.ZipCode ?? DBNull.Value });
            parameters.Add(new SqlParameter("@ZipExt", SqlDbType.Int) { Value = (object)c.CustomerBase.ZipExt ?? DBNull.Value });
            parameters.Add(new SqlParameter("@SourceID", SqlDbType.Int) { Value = c.SourceId });
            parameters.Add(new SqlParameter("@IndustryID", SqlDbType.Int) { Value = c.IndustryId });
            parameters.Add(new SqlParameter("@ProfessionID", SqlDbType.Int) { Value = c.ProfessionId });
            parameters.Add(new SqlParameter("@Phone", SqlDbType.BigInt) { Value = phn > 0 ? phn : SqlInt64.Null });
            parameters.Add(new SqlParameter("@Fax", SqlDbType.BigInt) { Value = fax > 0 ? fax : SqlInt64.Null });
            parameters.Add(new SqlParameter("@EmailAddress", SqlDbType.VarChar) { Value = c.CustomerBase.EmailAddress });
            parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar) { Value = EncryptIt(c.SecurityBase.UserName) });
            parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar) { Value = EncryptIt(c.SecurityBase.Password) });
            parameters.Add(new SqlParameter("@CustomerNotes", SqlDbType.VarChar) { Value = c.CustomerNotes });
            parameters.Add(new SqlParameter("@SecQuest1", SqlDbType.Int) { Value = c.SecurityBase.SecurityQuestion1 });
            parameters.Add(new SqlParameter("@SecQuest2", SqlDbType.Int) { Value = c.SecurityBase.SecurityQuestion2 });
            parameters.Add(new SqlParameter("@SecQuest3", SqlDbType.Int) { Value = c.SecurityBase.SecurityQuestion3 });
            parameters.Add(new SqlParameter("@CompanyName", SqlDbType.VarChar) { Value = (object)c.CompanyName ?? DBNull.Value });
            parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar) { Value = (object)c.CustomerBase.FirstName ?? DBNull.Value });
            parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar) { Value = (object)c.CustomerBase.LastName ?? DBNull.Value });
            parameters.Add(new SqlParameter("@Address", SqlDbType.VarChar) { Value = (object)c.CustomerBase.Address ?? DBNull.Value });
            parameters.Add(new SqlParameter("@Suite", SqlDbType.VarChar) { Value = (object)c.CustomerBase.Suite ?? DBNull.Value });
            parameters.Add(new SqlParameter("@City", SqlDbType.VarChar) { Value = (object)c.CustomerBase.City ?? DBNull.Value });
            parameters.Add(new SqlParameter("@IsOnWeb", SqlDbType.Bit) { Value = c.IsOnWeb });
            parameters.Add(new SqlParameter("@BuyForResale", SqlDbType.Bit) { Value = c.BuyForResale });
            parameters.Add(new SqlParameter("@ResetPassword", SqlDbType.Bit) { Value = c.SecurityBase.ResetPassword });
            parameters.Add(new SqlParameter("@IsVIP", SqlDbType.Bit) { Value = c.IsVip });

            return parameters;
        }

        private List<SqlParameter> SetPvtPartyParams(CustomerModel c)
        {
            var l = new List<SqlParameter>();

            l.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = c.CustomerId });
            l.Add(new SqlParameter("@CaFscStatus", SqlDbType.Int) { Value = c.CaFscStatus > 0 ? c.CaFscStatus : SqlInt32.Null });
            l.Add(new SqlParameter("@IsUsCitizen", SqlDbType.Bit) { Value = c.IsCitizen });
            l.Add(new SqlParameter("@IsPermResident", SqlDbType.Bit) { Value = c.IsPermResident });
            l.Add(new SqlParameter("@IsAge21", SqlDbType.Bit) { Value = c.IsAge21 });
            l.Add(new SqlParameter("@CaHasGunSafe", SqlDbType.Bit) { Value = c.CaHasGunSafe });
            l.Add(new SqlParameter("@Dob", SqlDbType.VarChar) { Value = (object)EncryptIt(c.StrDob) ?? DBNull.Value });

            return l;
        }

        private List<SqlParameter> SetFFLParams(CustomerModel c)
        {
            var l = new List<SqlParameter>();

            l.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = c.CustomerId });
            l.Add(new SqlParameter("@FFLCode", SqlDbType.Int) { Value = c.FedFireLicBase.FflCode > 0 ? c.FedFireLicBase.FflCode : SqlInt32.Null });
            l.Add(new SqlParameter("@CaCfdNumber", SqlDbType.Int) { Value = c.FedFireLicBase.CaCfdNumber > 0 ? c.FedFireLicBase.CaCfdNumber : SqlInt32.Null });
            l.Add(new SqlParameter("@CaHiCapYn", SqlDbType.Bit) { Value = c.FedFireLicBase.CaHasHiCap });

            return l;
        }

        private List<SqlParameter> SetCurioRelicParams(CustomerModel c)
        {
            var l = new List<SqlParameter>();

            var d = c.FedFireLicBase.FflExpDay;
            var m = c.FedFireLicBase.FflExpMo;
            var y = c.FedFireLicBase.FflExpYear;
            var dt0 = new DateTime(2000,1,1);
            var expDate = DateTime.TryParse(string.Format("{0}/{1}/{2}", m, d, y), out dt0) ? Convert.ToDateTime(string.Format("{0}/{1}/{2}", m, d, y)) : dt0;

            l.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = c.CustomerId });
            l.Add(new SqlParameter("@FFLStateID", SqlDbType.Int) { Value = c.FedFireLicBase.FflStateId > 0 ? c.FedFireLicBase.FflStateId : SqlInt32.Null });
            l.Add(new SqlParameter("@FFLZipCode", SqlDbType.Int) { Value = c.FedFireLicBase.FflZipCode > 0 ? c.FedFireLicBase.FflZipCode : SqlInt32.Null });
            l.Add(new SqlParameter("@LicRegion", SqlDbType.Int) { Value = c.FedFireLicBase.LicRegion > 0 ? c.FedFireLicBase.LicRegion : SqlInt32.Null });
            l.Add(new SqlParameter("@LicDistrict", SqlDbType.Int) { Value = c.FedFireLicBase.LicDistrict > 0 ? c.FedFireLicBase.LicDistrict : SqlInt32.Null });
            l.Add(new SqlParameter("@FFLLicName", SqlDbType.VarChar) { Value = (object)c.FedFireLicBase.LicName ?? DBNull.Value });
            l.Add(new SqlParameter("@FFLAddress", SqlDbType.VarChar) { Value = (object)c.FedFireLicBase.FflAddress ?? DBNull.Value });
            l.Add(new SqlParameter("@FFLCity", SqlDbType.VarChar) { Value = (object)c.FedFireLicBase.FflCity ?? DBNull.Value });
            l.Add(new SqlParameter("@LicCounty", SqlDbType.VarChar) { Value = (object)c.FedFireLicBase.LicCounty ?? DBNull.Value });
            l.Add(new SqlParameter("@LicType", SqlDbType.VarChar) { Value = (object)c.FedFireLicBase.LicType ?? DBNull.Value });
            l.Add(new SqlParameter("@LicExpCode", SqlDbType.VarChar) { Value = (object)c.FedFireLicBase.LicExpCode ?? DBNull.Value });
            l.Add(new SqlParameter("@LicSequence", SqlDbType.VarChar) { Value = (object)c.FedFireLicBase.LicSequence ?? DBNull.Value });
            l.Add(new SqlParameter("@FFLExpDate", SqlDbType.DateTime) { Value = expDate });
            return l;
        }

        private List<SqlParameter> SetLEOParams(CustomerModel c)
        {
            var l = new List<SqlParameter>();

            l.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = c.CustomerId });
            l.Add(new SqlParameter("@JurisdictionID", SqlDbType.Int) { Value = c.LeoBase.JurisdictionId > 0 ? c.LeoBase.JurisdictionId : SqlInt32.Null });
            l.Add(new SqlParameter("@DivisionID", SqlDbType.Int) { Value = c.LeoBase.DivisionId > 0 ? c.LeoBase.DivisionId : SqlInt32.Null });
            l.Add(new SqlParameter("@Region", SqlDbType.VarChar) { Value = (object)c.LeoBase.RegionName ?? DBNull.Value });
            l.Add(new SqlParameter("@OfficerNumber", SqlDbType.VarChar) { Value = (object)c.LeoBase.OfficerNumber ?? DBNull.Value });
            return l;
        }

        public AddMenuItemModel AddIndustry(string industry)
        {
            var mi = new AddMenuItemModel();
            var l = new List<SelectListItem>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAddIndustry");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param =  new SqlParameter("@IndustryName", SqlDbType.VarChar) { Value = industry };
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

                        while (dr.Read())
                        {
                            var id = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                            var iName = dr["IndustryName"].ToString();

                            var li = new SelectListItem();
                            li.Value = id.ToString();
                            li.Text = iName;
                            l.Add(li);
                        }
                    }
                }

                mi.CustIndustry = l;
            }

            return mi;

        }

        public AddMenuItemModel AddProfession(int indId, string prof)
        {
            var mi = new AddMenuItemModel();
            var l = new List<SelectListItem>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAddProfession");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@IndustryID", SqlDbType.VarChar) { Value = indId };
                parameters[1] = new SqlParameter("@ProfessionName", SqlDbType.VarChar) { Value = prof };
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

                        while (dr.Read())
                        {
                            var id = Int32.TryParse(dr["ID"].ToString(), out x0) ? Convert.ToInt32(dr["ID"]) : 0;
                            var iName = dr["ProfessionName"].ToString();

                            var li = new SelectListItem();
                            li.Value = id.ToString();
                            li.Text = iName;
                            l.Add(li);
                        }
                    }
                }

                mi.CustProfession = l;
            }

            return mi;

        }

        public CustomerDoc AddCustomerDocument(CustomerDoc c)
        {
            var cd = new CustomerDoc();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcAddCustomerDoc");
                var cmd = new SqlCommand(proc, conn) {CommandType = CommandType.StoredProcedure};
                conn.Open();

                var parameters = new IDataParameter[9];
                parameters[0] = new SqlParameter("@UserID", SqlDbType.Int) { Value = c.UserId };
                parameters[1] = new SqlParameter("@DocCatID", SqlDbType.Int) { Value = c.DocCatId };
                parameters[2] = new SqlParameter("@DocTypeID", SqlDbType.Int) { Value = c.DocTypeId };
                parameters[3] = new SqlParameter("@StateID", SqlDbType.Int) { Value = c.StateId };
                parameters[4] = new SqlParameter("@DocNumber", SqlDbType.VarChar) { Value = EncryptIt(c.DocNumber) };
                parameters[5] = new SqlParameter("@ExpDate", SqlDbType.DateTime) { Value = c.ExpDate == DateTime.MinValue ? (object)DBNull.Value : c.ExpDate };
                parameters[6] = new SqlParameter("@Dob", SqlDbType.DateTime) { Value = c.Dob == DateTime.MinValue ? (object)DBNull.Value : c.Dob };
                parameters[7] = new SqlParameter("@IsCurAddr", SqlDbType.Bit) { Value = c.IsAddrCurrent };
                parameters[8] = new SqlParameter("@RealID", SqlDbType.Bit) { Value = c.IsRealId };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cd;

                dr.Read();

                var x0 = 0;
                var docId = Int32.TryParse(dr["DocID"].ToString(), out x0) ? Convert.ToInt32(dr["DocID"]) : x0;
                var docCr = Int32.TryParse(dr["CurDocCt"].ToString(), out x0) ? Convert.ToInt32(dr["CurDocCt"]) : x0;
                var docAr = Int32.TryParse(dr["ArcDocCt"].ToString(), out x0) ? Convert.ToInt32(dr["ArcDocCt"]) : x0;
                var docAl = Int32.TryParse(dr["AllDocCt"].ToString(), out x0) ? Convert.ToInt32(dr["AllDocCt"]) : x0;
                var subFo = dr["DocumentFolder"].ToString();

                var cuMod = new CustomerModel(docCr, docAr, docAl);

                cd = new CustomerDoc(docId, subFo, cuMod);
            }

            return cd;
        }

        public CustomerDoc UpdateDocument(CustomerDoc c)
        {
            var cd = new CustomerDoc();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateCustomerDoc");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[9];
                parameters[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = c.Id };
                parameters[1] = new SqlParameter("@DocCatID", SqlDbType.Int) { Value = c.DocCatId };
                parameters[2] = new SqlParameter("@DocTypeID", SqlDbType.Int) { Value = c.DocTypeId };
                parameters[3] = new SqlParameter("@StateID", SqlDbType.Int) { Value = c.StateId };
                parameters[4] = new SqlParameter("@DocNumber", SqlDbType.VarChar) { Value = c.DocNumber };
                parameters[5] = new SqlParameter("@ExpDate", SqlDbType.DateTime) { Value = c.ExpDate == DateTime.MinValue ? (object)DBNull.Value : c.ExpDate };
                parameters[6] = new SqlParameter("@Dob", SqlDbType.DateTime) { Value = c.Dob == DateTime.MinValue ? (object)DBNull.Value : c.Dob };
                parameters[7] = new SqlParameter("@IsCurAddr", SqlDbType.Bit) { Value = c.IsAddrCurrent };
                parameters[8] = new SqlParameter("@RealID", SqlDbType.Bit) { Value = c.IsRealId };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cd;

                dr.Read();

                var x0 = 0;
                var docId = Int32.TryParse(dr["DocID"].ToString(), out x0) ? Convert.ToInt32(dr["DocID"]) : x0;
                var subFo = dr["DocumentFolder"].ToString();

                cd = new CustomerDoc(docId, subFo);

            }

            return cd;
        }



        public SecurityModel SetTempLogin()
        {
            var sm = new SecurityModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCookTempLogin");
                var cmd = new SqlCommand(proc, conn) {CommandType = CommandType.StoredProcedure};
                conn.Open();

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return sm;

                dr.Read();

                var user = dr["TempUser"].ToString();
                var pswd = dr["TempPswd"].ToString();

                sm = new SecurityModel(user, pswd);
            }

            return sm;
        }

        public void UpdateProfilePic(int id, string imgName)
        {
            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateProfilePic");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                parameters[1] = new SqlParameter("@ImgName", SqlDbType.VarChar) { Value = imgName };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                cmd.ExecuteNonQuery();
            }            
        }


        public void UpdateDocImage(int id, string imgName)
        {
            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateDocImage");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                parameters[1] = new SqlParameter("@DocImg", SqlDbType.VarChar) { Value = imgName };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                cmd.ExecuteNonQuery();
            }            
        }

        public void SetPptOtherCount(int sup, int ct)
        {
            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetSupplierOtherPptCt");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@SupplierID", SqlDbType.Int) { Value = sup };
                parameters[1] = new SqlParameter("@Count", SqlDbType.VarChar) { Value = ct };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                cmd.ExecuteNonQuery();
            }
        }


        


        public List<CustomerDoc> GetCustomerDocs(int id, bool isCurrent)
        {
            var cd = new List<CustomerDoc>();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetCustomerDocs");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@UserID", SqlDbType.Int) { Value = id };
                parameters[1] = new SqlParameter("@IsCurrent", SqlDbType.Bit) { Value = isCurrent };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cd;

                var i0 = 0;
                var dt0 = DateTime.MinValue;
                var bc = new BaseController();

                while (dr.Read())
                {
                    var did = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                    var typ = Int32.TryParse(dr["DocTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["DocTypeID"]) : i0;
                    var version = Int32.TryParse(dr["VersionID"].ToString(), out i0) ? Convert.ToInt32(dr["VersionID"]) : i0;
                    var expDate = DateTime.TryParse(dr["ExpDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["ExpDate"]) : dt0;
                    var dtOfBth = DateTime.TryParse(dr["Dob"].ToString(), out dt0) ? Convert.ToDateTime(dr["Dob"]) : dt0;
                    var docImg = dr["ImgName"].ToString();
                    var docGrp = dr["DocGroup"].ToString();
                    var docTyp = dr["DocumentType"].ToString();
                    var docNum = dr["DocNumber"].ToString();
                    var docSub = dr["DocumentFolder"].ToString();

                    var dDoc = DecryptIt(docNum);

                    var exp = expDate == DateTime.MinValue ? "None" : expDate.ToShortDateString();
                    var dob = dtOfBth == DateTime.MinValue ? "" : dtOfBth.ToShortDateString();

                    docGrp = isCurrent ? docGrp : docGrp + " (Archived)";

                    var imgUrl = string.Empty;
                    if (docImg.Length > 0)
                    {
                        var p = ConfigurationHelper.GetPropertyValue("application", "p2");
                        imgUrl = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(p), docSub, docImg);
                        imgUrl += "?" + DateTime.Now.Ticks;
                    }

                    var m = new CustomerDoc(did, version, exp, dob, imgUrl, docGrp, docTyp, dDoc);
                    cd.Add(m);
                } 
            }

            return cd;
        }

        public CustomerDoc GetMenuSubCats(int id)
        {
            var cd = new CustomerDoc();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcMenuDocGetFields");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.VarChar) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cd;

                dr.Read();

                var b0 = false;

                var b1 = Boolean.TryParse(dr["IsIDNumber"].ToString(), out b0) ? Convert.ToBoolean(dr["IsIDNumber"]) : b0;
                var b2 = Boolean.TryParse(dr["IsState"].ToString(), out b0) ? Convert.ToBoolean(dr["IsState"]) : b0;
                var b3 = Boolean.TryParse(dr["IsExpDate"].ToString(), out b0) ? Convert.ToBoolean(dr["IsExpDate"]) : b0;
                var b4 = Boolean.TryParse(dr["IsDOB"].ToString(), out b0) ? Convert.ToBoolean(dr["IsDOB"]) : b0;
                var b5 = Boolean.TryParse(dr["IsAddressCurrent"].ToString(), out b0) ? Convert.ToBoolean(dr["IsAddressCurrent"]) : b0;
                var b6 = Boolean.TryParse(dr["IsRealID"].ToString(), out b0) ? Convert.ToBoolean(dr["IsRealID"]) : b0;

                cd = new CustomerDoc(id, b1, b2, b3, b4, b5, b6);
            }

            return cd;
        }



        public CustomerDoc GetCustomerDocById(int id)
        {
            var cd = new CustomerDoc();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetCustomerDocByID");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cd;

                var i0 = 0;
                var b0 = false;
                var dt0 = DateTime.MinValue;
                var bc = new BaseController();

                dr.Read();
                
                var did = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                var cat = Int32.TryParse(dr["DocCatID"].ToString(), out i0) ? Convert.ToInt32(dr["DocCatID"]) : i0;
                var typ = Int32.TryParse(dr["DocTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["DocTypeID"]) : i0;
                var sta = Int32.TryParse(dr["StateID"].ToString(), out i0) ? Convert.ToInt32(dr["StateID"]) : i0;
                var cur = Boolean.TryParse(dr["IsCurrent"].ToString(), out b0) ? Convert.ToBoolean(dr["IsCurrent"]) : b0;
                var adr = Boolean.TryParse(dr["IsAddressCurrent"].ToString(), out b0) ? Convert.ToBoolean(dr["IsAddressCurrent"]) : b0;
                var rid = Boolean.TryParse(dr["IsRealID"].ToString(), out b0) ? Convert.ToBoolean(dr["IsRealID"]) : b0;

                var expDate = DateTime.TryParse(dr["ExpDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["ExpDate"]) : dt0;
                var dtOfBth = DateTime.TryParse(dr["Dob"].ToString(), out dt0) ? Convert.ToDateTime(dr["Dob"]) : dt0;
                var docNum = dr["DocNumber"].ToString();
                var docImg = dr["ImgName"].ToString();
                var docSub = dr["DocumentFolder"].ToString();

                var dDoc = DecryptIt(docNum);

                //var f = bc.GetDocFolderCode(typ);

                var imgUrl = string.Empty;
                if (docImg.Length > 0)
                {
                    var p = ConfigurationHelper.GetPropertyValue("application", "p2");
                    imgUrl = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(p), docSub, docImg);
                    //imgUrl = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(p), DecryptIt(f), docImg);
                    imgUrl += "?" + DateTime.Now.Ticks;
                }

                cd = new CustomerDoc(did, cat, typ, sta, cur, adr, rid, expDate.ToShortDateString(), dtOfBth.ToShortDateString(), dDoc, imgUrl);
            }

            return cd;
        }


        public List<SupplierModel> SearchSuppliers(int x, string txt)
        {
            var cm = new List<SupplierModel>();
            var p = ConfigurationHelper.GetPropertyValue("application", "p2");
            var pp = ConfigurationHelper.GetPropertyValue("application", "m9");

            var srch = txt;

            //if search is by phone, Go numbers only:
            var rxEm = new Regex(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
            var ph = rxEm.Match(txt);
            if (ph.Success) { srch = Regex.Replace(txt, @"[^\d]", String.Empty); }

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSearchSuppliers");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@SupTypeID", SqlDbType.Int) { Value = x };
                parameters[1] = new SqlParameter("@SearchTxt", SqlDbType.VarChar) { Value = txt };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cm;

                var i0 = 0;
                var dt0 = DateTime.MinValue;

                while (dr.Read())
                {

                    var i1 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                    var i2 = Int32.TryParse(dr["SupplierTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["SupplierTypeID"]) : i0;
                    var i3 = Int32.TryParse(dr["PptCtLocal"].ToString(), out i0) ? Convert.ToInt32(dr["PptCtLocal"]) : i0;
                    var i4 = Int32.TryParse(dr["PptCtOther"].ToString(), out i0) ? Convert.ToInt32(dr["PptCtOther"]) : i0;

                    var v1 = dr["FirstName"].ToString();
                    var v2 = dr["LastName"].ToString();
                    var v3 = dr["OrgName"].ToString();
                    var v4 = dr["SupAddress"].ToString();
                    var v5 = dr["SupCity"].ToString();
                    var v6 = dr["SupState"].ToString();
                    var v7 = dr["SupZipCode"].ToString();
                    var v8 = dr["SupEmail"].ToString();
                    var v9 = dr["SupPhone"].ToString();
                    var v10 = dr["FFLNumber"].ToString();

                    var dt1 = DateTime.TryParse(dr["FFLExpires"].ToString(), out dt0) ? Convert.ToDateTime(dr["FFLExpires"]) : dt0;
                    var v11 = dt1.ToShortDateString();

                    var nl = string.Empty;
                    var al = string.Empty;
                    var cl = string.Empty;

                    switch (i2)
                    {
                        case 2:
                            nl = string.Format("{0} {1}", v1, v2);
                            al = string.Format("{0} {1} {2}, {3}", v4, v5, v6, v7);
                            cl = string.Format("C&R FFL: {0}", v10);
                            break;
                        case 3:
                            nl = string.Format("{0} {1}", v1, v2);
                            al = string.Format("{0} {1} {2}, {3}", v4, v5, v6, v7);
                            break;
                        case 4:
                        case 5:
                            nl = v3;
                            al = string.Format("{0} {1} {2}, {3}", v4, v5, v6, v7);
                            break;
                        case 6:
                            nl = string.Format("{0} {1}", v1, v2);
                            al = "FROM OWNER'S PERSONAL COLLECTION";
                            break;
                    }

                    var sm = new SupplierModel(i1, i2, i3, i4, nl, al, cl, v8, v9, v11);
                    cm.Add(sm);
                }
            }

            return cm;

        }

        //MAY BE OBSOLETE
        public SupplierModel SetPptSupplier(int tid, int sup)
        {
            var sm = new SupplierModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSupplierPptSet");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var parameters = new IDataParameter[2];
                parameters[0] = new SqlParameter("@TransID", SqlDbType.Int) { Value = tid };
                parameters[1] = new SqlParameter("@SupplierID", SqlDbType.VarChar) { Value = sup };
                foreach (var parameter in parameters) { cmd.Parameters.Add(parameter); }

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return sm;

                var i0 = 0;
                var dt0 = DateTime.MinValue;
                dr.Read();

                var i1 = Int32.TryParse(dr["SupplierTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["SupplierTypeID"]) : i0;
                var i2 = Int32.TryParse(dr["StateID"].ToString(), out i0) ? Convert.ToInt32(dr["StateID"]) : i0;
                var i3 = Int32.TryParse(dr["IDType"].ToString(), out i0) ? Convert.ToInt32(dr["IDType"]) : i0;
                var i4 = Int32.TryParse(dr["IDState"].ToString(), out i0) ? Convert.ToInt32(dr["IDState"]) : i0;

                var i5 = Int32.TryParse(dr["PptCtLocal"].ToString(), out i0) ? Convert.ToInt32(dr["PptCtLocal"]) : i0;
                var i6 = Int32.TryParse(dr["PptCtOther"].ToString(), out i0) ? Convert.ToInt32(dr["PptCtOther"]) : i0;
                var i7 = Int32.TryParse(dr["PptCtYtd"].ToString(), out i0) ? Convert.ToInt32(dr["PptCtYtd"]) : i0;

                var v1 = dr["FirstName"].ToString();
                var v2 = dr["LastName"].ToString();
                var v3 = dr["OrgName"].ToString();
                var v4 = dr["SupAddress"].ToString();
                var v5 = dr["SupCity"].ToString();
                var v6 = dr["SupZipCode"].ToString();
                var v7 = dr["SupZipExt"].ToString();
                var v8 = dr["SupPhone"].ToString();
                var v9 = dr["SupEmail"].ToString();
                var v10 = dr["FFLNumber"].ToString();
                var v11 = dr["IDNumber"].ToString();
                var v15 = dr["SupState"].ToString();

                var dt1 = DateTime.TryParse(dr["FFLExpires"].ToString(), out dt0) ? Convert.ToDateTime(dr["FFLExpires"]) : dt0;
                var dt2 = DateTime.TryParse(dr["IDDob"].ToString(), out dt0) ? Convert.ToDateTime(dr["IDDob"]) : dt0;
                var dt3 = DateTime.TryParse(dr["IDExpires"].ToString(), out dt0) ? Convert.ToDateTime(dr["IDExpires"]) : dt0;

                var v12 = dt1 == DateTime.MinValue ? string.Empty : dt1.ToShortDateString();
                var v13 = dt2 == DateTime.MinValue ? string.Empty : dt2.ToShortDateString();
                var v14 = dt3 == DateTime.MinValue ? string.Empty : dt3.ToShortDateString();

                sm = new SupplierModel(sup, i1, i2, i3, i4, i5, i6, i7, v1, v2, v3, v4, v5, v15, v6, v7, v8, v9, v10, v11, v12, v13, v14);
 
            }

            return sm;
        }

        public SupplierModel PptSellerInfoGet(int isi)
        {
            var sm = new SupplierModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSupplierPptGet");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@SvcBasisID", SqlDbType.Int) { Value = isi };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return sm;

                var i0 = 0;
                var dt0 = DateTime.MinValue;
                dr.Read();

                var i1 = Int32.TryParse(dr["SupplierTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["SupplierTypeID"]) : i0;
                var i2 = Int32.TryParse(dr["StateID"].ToString(), out i0) ? Convert.ToInt32(dr["StateID"]) : i0;
                var i3 = Int32.TryParse(dr["IDType"].ToString(), out i0) ? Convert.ToInt32(dr["IDType"]) : i0;
                var i4 = Int32.TryParse(dr["IDState"].ToString(), out i0) ? Convert.ToInt32(dr["IDState"]) : i0;
                var i5 = Int32.TryParse(dr["PptCtLocal"].ToString(), out i0) ? Convert.ToInt32(dr["PptCtLocal"]) : i0;
                var i6 = Int32.TryParse(dr["PptCtOther"].ToString(), out i0) ? Convert.ToInt32(dr["PptCtOther"]) : i0;
                var i7 = Int32.TryParse(dr["PptCtYtd"].ToString(), out i0) ? Convert.ToInt32(dr["PptCtYtd"]) : i0;

                var i8 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;

                var v1 = dr["FirstName"].ToString();
                var v2 = dr["LastName"].ToString();
                var v3 = dr["OrgName"].ToString();
                var v4 = dr["SupAddress"].ToString();
                var v5 = dr["SupCity"].ToString();
                var v6 = dr["SupZipCode"].ToString();
                var v7 = dr["SupZipExt"].ToString();
                var v8 = dr["SupPhone"].ToString();
                var v9 = dr["SupEmail"].ToString();
                var v10 = dr["FFLNumber"].ToString();
                var v11 = dr["IDNumber"].ToString();
                var v15 = dr["SupState"].ToString();

                var dt1 = DateTime.TryParse(dr["FFLExpires"].ToString(), out dt0) ? Convert.ToDateTime(dr["FFLExpires"]) : dt0;
                var dt2 = DateTime.TryParse(dr["IDDob"].ToString(), out dt0) ? Convert.ToDateTime(dr["IDDob"]) : dt0;
                var dt3 = DateTime.TryParse(dr["IDExpires"].ToString(), out dt0) ? Convert.ToDateTime(dr["IDExpires"]) : dt0;

                var v12 = dt1 == DateTime.MinValue ? string.Empty : dt1.ToShortDateString();
                var v13 = dt2 == DateTime.MinValue ? string.Empty : dt2.ToShortDateString();
                var v14 = dt3 == DateTime.MinValue ? string.Empty : dt3.ToShortDateString();

                sm = new SupplierModel(i8, i1, i2, i3, i4, i5, i6, i7, v1, v2, v3, v4, v5, v15, v6, v7, v8, v9, v10, v11, v12, v13, v14);

            }

            return sm;
        }


        public SupplierModel GetSupplier(int id)
        {
            var s = new SupplierModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSupplierGet");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.VarChar) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return s;

                dr.Read();

                var i0 = 0;
                var dt0 = DateTime.MinValue;

                var i1 = Int32.TryParse(dr["StateID"].ToString(), out i0) ? Convert.ToInt32(dr["StateID"]) : i0;
                var i2 = Int32.TryParse(dr["SupplierTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["SupplierTypeID"]) : i0;
                var i3 = Int32.TryParse(dr["IDType"].ToString(), out i0) ? Convert.ToInt32(dr["IDType"]) : i0;
                var i4 = Int32.TryParse(dr["IDState"].ToString(), out i0) ? Convert.ToInt32(dr["IDState"]) : i0;
                var i5 = Int32.TryParse(dr["PptCtLocal"].ToString(), out i0) ? Convert.ToInt32(dr["PptCtLocal"]) : i0;
                var i6 = Int32.TryParse(dr["PptCtOther"].ToString(), out i0) ? Convert.ToInt32(dr["PptCtOther"]) : i0;
                var i7 = Int32.TryParse(dr["PptCtYtd"].ToString(), out i0) ? Convert.ToInt32(dr["PptCtYtd"]) : i0;

                var v1 = dr["FirstName"].ToString();
                var v2 = dr["LastName"].ToString();
                var v3 = dr["OrgName"].ToString();
                var v4 = dr["SupAddress"].ToString();
                var v5 = dr["SupCity"].ToString();
                var v6 = dr["SupState"].ToString();
                var v7 = dr["SupZipCode"].ToString();
                var v8 = dr["SupZipExt"].ToString();
                var v9 = dr["SupPhone"].ToString();
                var v10 = dr["SupEmail"].ToString();
                var v11 = dr["IDNumber"].ToString();
                var v12 = dr["FFLNumber"].ToString();

                var dt1 = DateTime.TryParse(dr["IDDob"].ToString(), out dt0) ? Convert.ToDateTime(dr["IDDob"]) : dt0;
                var dt2 = DateTime.TryParse(dr["IDExpires"].ToString(), out dt0) ? Convert.ToDateTime(dr["IDExpires"]) : dt0;
                var dt3 = DateTime.TryParse(dr["FFLExpires"].ToString(), out dt0) ? Convert.ToDateTime(dr["FFLExpires"]) : dt0;

                var sd1 = dt1.ToShortDateString();
                var sd2 = dt2.ToShortDateString();
                var sd3 = dt3.ToShortDateString();

                s = new SupplierModel(i1, i2, i3, i4, i5, i6, i7, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, sd1, sd2, sd3);
            }

            return s;
        }


        public List<CustomerModel> SearchCustomers(string txt)
        {
            var cm = new List<CustomerModel>();
            var p = ConfigurationHelper.GetPropertyValue("application", "a1"); 
            var pp = ConfigurationHelper.GetPropertyValue("application", "m9"); 
            
            var srch = txt;

            //if search is by phone, Go numbers only:
            var rxEm = new Regex(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
            var ph = rxEm.Match(txt);
            if (ph.Success) { srch = Regex.Replace(txt, @"[^\d]", String.Empty); }


            //if search is by email, Encrypt first:
            var rx = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var em = rx.Match(txt);
            if (em.Success) { srch = EncryptIt(txt); }

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSearchCustomers");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@SearchTxt", SqlDbType.VarChar) { Value = txt };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cm;

                var i0 = 0;
                var b0 = false;
                Int64 bi0 = 0;
                var dt0 = DateTime.MinValue;

                while (dr.Read())
                {
                    var id = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                    var sid = Int32.TryParse(dr["StateID"].ToString(), out i0) ? Convert.ToInt32(dr["StateID"]) : i0;
                    var zip = Int32.TryParse(dr["ZipCode"].ToString(), out i0) ? Convert.ToInt32(dr["ZipCode"]) : i0;
                    var ext = Int32.TryParse(dr["ZipExt"].ToString(), out i0) ? Convert.ToInt32(dr["ZipExt"]) : i0;
                    var phn = Int64.TryParse(dr["Phone"].ToString(), out bi0) ? Convert.ToInt64(dr["Phone"]) : bi0;
                    var reg = Boolean.TryParse(dr["IsRegistered"].ToString(), out b0) ? Convert.ToBoolean(dr["IsRegistered"]) : b0;
                    var ctp = dr["CustomerType"].ToString();
                    var fNm = dr["FirstName"].ToString();
                    var lNm = dr["LastName"].ToString();
                    var eml = dr["EmailAddress"].ToString();
                    var img = dr["ImgProfile"].ToString();
                    var com = dr["CompanyName"].ToString();
                    var adr = dr["Address"].ToString();
                    var cty = dr["City"].ToString();
                    var abv = dr["StateAbbrev"].ToString();
 
                    var dem = DecryptIt(eml);

                    var cName = string.Format("{0} {1} {2}", fNm, lNm, com.Length > 0 ? "- " + com : string.Empty);
                    var phnEml = string.Format("{0}  {1}", phn > 0 ? "PH: " + PhoneToString(phn) : string.Empty, dem.Length > 0 ? "E: " + dem : string.Empty);

                    var imgUrl = string.Empty;
                    if (img.Length > 0) { imgUrl = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(p), DecryptIt(pp), img); }


                    var fa = string.Format("{0} {1}, {2} {3}", adr, cty, abv, zip);
                    var cb = new CustomerBaseModel(fNm, lNm, adr, cty, sid, zip, ext, abv);               
                    var m = new CustomerModel(id, reg, cName, fa, phnEml, ctp, imgUrl, cb);
                    cm.Add(m);
                }
            }

            return cm;

        }

        public CustomerModel GetCustomerById(int id)
        {
            var cm = new CustomerModel();
            var cd = new List<CustomerDoc>();
            var pd = new List<CustomerDoc>();
            var ca = new List<SelectListItem>();
            var p = ConfigurationHelper.GetPropertyValue("application", "a1");
            var dc = ConfigurationHelper.GetPropertyValue("application", "p2");
            var pp = ConfigurationHelper.GetPropertyValue("application", "m9");

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
               
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetCustomerByID");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cm;

                var i0 = 0;
                var b0 = false;
                var g0 = new Guid();
                Int64 bi0 = 0;
                var dt0 = DateTime.MinValue;
                var dbm = 0;
                var dbd = 0;
                var dby = 0;

                dr.Read();

                /* BASIC INFO */
                var sq1 = Int32.TryParse(dr["SecQuestion1"].ToString(), out i0) ? Convert.ToInt32(dr["SecQuestion1"]) : i0;
                var sq2 = Int32.TryParse(dr["SecQuestion2"].ToString(), out i0) ? Convert.ToInt32(dr["SecQuestion2"]) : i0;
                var sq3 = Int32.TryParse(dr["SecQuestion3"].ToString(), out i0) ? Convert.ToInt32(dr["SecQuestion3"]) : i0;
                var cid = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                var src = Int32.TryParse(dr["SourceID"].ToString(), out i0) ? Convert.ToInt32(dr["SourceID"]) : i0;
                var ctp = Int32.TryParse(dr["CustomerTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["CustomerTypeID"]) : i0;
                var ind = Int32.TryParse(dr["IndustryID"].ToString(), out i0) ? Convert.ToInt32(dr["IndustryID"]) : i0;
                var pro = Int32.TryParse(dr["ProfessionID"].ToString(), out i0) ? Convert.ToInt32(dr["ProfessionID"]) : i0;
                var sta = Int32.TryParse(dr["StateID"].ToString(), out i0) ? Convert.ToInt32(dr["StateID"]) : i0;
                var zip = Int32.TryParse(dr["ZipCode"].ToString(), out i0) ? Convert.ToInt32(dr["ZipCode"]) : i0;
                var ext = Int32.TryParse(dr["ZipExt"].ToString(), out i0) ? Convert.ToInt32(dr["ZipExt"]) : i0;
                var adc = Int32.TryParse(dr["AllDocCt"].ToString(), out i0) ? Convert.ToInt32(dr["AllDocCt"]) : i0;
                var cdc = Int32.TryParse(dr["CurDocCt"].ToString(), out i0) ? Convert.ToInt32(dr["CurDocCt"]) : i0;
                var arc = Int32.TryParse(dr["ArcDocCt"].ToString(), out i0) ? Convert.ToInt32(dr["ArcDocCt"]) : i0;

                var phn = Int64.TryParse(dr["Phone"].ToString(), out bi0) ? Convert.ToInt64(dr["Phone"]) : bi0;
                var fax = Int64.TryParse(dr["Fax"].ToString(), out bi0) ? Convert.ToInt64(dr["Fax"]) : bi0;
                var uky = Guid.TryParse(dr["UserKey"].ToString(), out g0) ? Guid.Parse(dr["UserKey"].ToString()) : g0;

                var vip = Boolean.TryParse(dr["IsVIP"].ToString(), out b0) ? Convert.ToBoolean(dr["IsVIP"]) : b0;
                var web = Boolean.TryParse(dr["IsOnWeb"].ToString(), out b0) ? Convert.ToBoolean(dr["IsOnWeb"]) : b0;
                var rsp = Boolean.TryParse(dr["ResetPassword"].ToString(), out b0) ? Convert.ToBoolean(dr["ResetPassword"]) : b0;

                var usr = dr["UserName"].ToString();
                var psw = dr["Password"].ToString();
                var fir = dr["FirstName"].ToString();
                var las = dr["LastName"].ToString();
                var eml = dr["EmailAddress"].ToString();
                var cmp = dr["CompanyName"].ToString();
                var adr = dr["Address"].ToString();
                var ste = dr["Suite"].ToString();
                var cty = dr["City"].ToString();
                var not = dr["CustomerNotes"].ToString();
                var img = dr["ImgProfile"].ToString();
                var sa1 = dr["SecAnswer1"].ToString();
                var sa2 = dr["SecAnswer2"].ToString();
                var sa3 = dr["SecAnswer3"].ToString();
                var edb = dr["Dob"].ToString();

                var sdb = DateTime.TryParse(DecryptIt(edb), out dt0) ? Convert.ToDateTime(DecryptIt(edb)) : dt0;
                if (sdb > DateTime.MinValue)
                {
                    dbm = sdb.Month;
                    dbd = sdb.Day;
                    dby = sdb.Year;

                }

                var imgUrl = string.Empty;
                if (img.Length > 0) { imgUrl = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(p), DecryptIt(pp), img); }

                cm = new CustomerModel(cid, src, ind, pro, dbm, dbd, dby, adc, cdc, arc, PhoneToString(fax), not, imgUrl, vip, web);
                cm.CustomerTypeId = ctp;

                var sm = new SecurityModel(uky, DecryptIt(usr), DecryptIt(psw), rsp, sq1, sq2, sq3, DecryptIt(sa1), DecryptIt(sa2), DecryptIt(sa3));
                cm.SecurityBase = sm;

                var bm = new CustomerBaseModel(DecryptIt(eml), fir, las, adr, ste, cty, PhoneToString(phn), sta, zip, ext);
                cm.CustomerBase = bm;


                /* ADDRESS LIST */
                dr.NextResult();

                if (!dr.HasRows) return cm;
                while (dr.Read())
                {
                    var i1 = dr["ID"].ToString();
                    var v1 = dr["CustAddress"].ToString();
                    var sl = new SelectListItem();
                    sl.Text = v1;
                    sl.Value = i1;
                    ca.Add(sl);
                }

                cm.CustAddresses = ca;

                /* CUSTOMER TYPE */
                if (ctp > 0)
                {
                    dr.NextResult();

                    if (!dr.HasRows) return cm;
                    dr.Read();

                        
                    switch ((CustomerTypes)ctp)
                    {
                        case CustomerTypes.PrivateParty:

                            var pStId = Int32.TryParse(dr["CaFscStatusID"].ToString(), out i0) ? Convert.ToInt32(dr["CaFscStatusID"]) : i0;
                            var pgnSf = Boolean.TryParse(dr["CaHasGunSafe"].ToString(), out b0) ? Convert.ToBoolean(dr["CaHasGunSafe"]) : b0;
                            var pIsCz = Boolean.TryParse(dr["IsUsCitizen"].ToString(), out b0) ? Convert.ToBoolean(dr["IsUsCitizen"]) : b0;
                            var ipr = Boolean.TryParse(dr["IsPermResident"].ToString(), out b0) ? Convert.ToBoolean(dr["IsPermResident"]) : b0;

                            cm.CaFscStatus = pStId;
                            cm.CaHasGunSafe = pgnSf;
                            cm.IsCitizen = pIsCz;
                            cm.IsPermResident = ipr;
                            break;

                        case CustomerTypes.CommercialFFL:

                            var fExpDt = DateTime.TryParse(dr["LicExpires"].ToString(), out dt0) ? Convert.ToDateTime(dr["LicExpires"]) : dt0;
                            var fByRs = Boolean.TryParse(dr["BuyingForResale"].ToString(), out b0) ? Convert.ToBoolean(dr["BuyingForResale"]) : b0;
                            var fHiCp = Boolean.TryParse(dr["CaHiCapYn"].ToString(), out b0) ? Convert.ToBoolean(dr["CaHiCapYn"]) : b0;

                            var fcd = Int32.TryParse(dr["FFLCode"].ToString(), out i0) ? Convert.ToInt32(dr["FFLCode"]) : i0;
                            var fZip = Int32.TryParse(dr["ZipCode"].ToString(), out i0) ? Convert.ToInt32(dr["ZipCode"]) : i0;
                            var fLcRg = Int32.TryParse(dr["LicRegion"].ToString(), out i0) ? Convert.ToInt32(dr["LicRegion"]) : i0;
                            var fLcDs = Int32.TryParse(dr["LicDistrict"].ToString(), out i0) ? Convert.ToInt32(dr["LicDistrict"]) : i0;
                            var fStId = Int32.TryParse(dr["StateID"].ToString(), out i0) ? Convert.ToInt32(dr["StateID"]) : i0;
                            var fCaCf = Int32.TryParse(dr["CaCfdNumber"].ToString(), out i0) ? Convert.ToInt32(dr["CaCfdNumber"]) : i0;

                            var fLsc = dr["LicCounty"].ToString();
                            var fLex = dr["LicExpCode"].ToString();
                            var fSeq = dr["LicSequence"].ToString();
                            var fLtp = dr["LicType"].ToString();
                            var fLnm = dr["LicName"].ToString();
                            var fTrd = dr["TradeName"].ToString();
                            var fAdr = dr["Address"].ToString();
                            var fCty = dr["City"].ToString();
                            var fPhn = dr["Phone"].ToString();
                            var iExp = fExpDt < DateTime.Now;

                            var fl = new FflLicenseModel(fcd, fExpDt.Day, fExpDt.Month, fExpDt.Year, fHiCp, iExp, fZip, fLcRg, fLcDs, fStId, fCaCf, fCty, fLsc, fLex, fSeq, fLtp, fLnm, fTrd, fAdr, string.Format(fPhn, "(###) ###-####"));
                            cm.FedFireLicBase = fl;

                            cm.BuyForResale = fByRs;



                            break;
                        case CustomerTypes.CurioRelic:

                            var rExpDt = DateTime.TryParse(dr["LicExpires"].ToString(), out dt0) ? Convert.ToDateTime(dr["LicExpires"]) : dt0;
                            var rZip = Int32.TryParse(dr["ZipCode"].ToString(), out i0) ? Convert.ToInt32(dr["ZipCode"]) : i0;
                            var rLcRg = Int32.TryParse(dr["LicRegion"].ToString(), out i0) ? Convert.ToInt32(dr["LicRegion"]) : i0;
                            var rLcDs = Int32.TryParse(dr["LicDistrict"].ToString(), out i0) ? Convert.ToInt32(dr["LicDistrict"]) : i0;
                            var rStId = Int32.TryParse(dr["StateID"].ToString(), out i0) ? Convert.ToInt32(dr["StateID"]) : i0;

                            var rLsc = dr["LicCounty"].ToString();
                            var rLex = dr["LicExpCode"].ToString();
                            var rSeq = dr["LicSequence"].ToString();
                            var rLtp = dr["LicType"].ToString();
                            var rLnm = dr["LicName"].ToString();
                            var rAdr = dr["Address"].ToString();
                            var rCty = dr["City"].ToString();

                            var cr = new FflLicenseModel(rExpDt.Day, rExpDt.Month, rExpDt.Year, rZip, rLcRg, rLcDs, rStId, rCty, rLsc, rLex, rSeq, rLtp, rLnm, rAdr);
                            cm.FedFireLicBase = cr;

                            break;
                        case CustomerTypes.LawEnforcement:

                            var lJurs = Int32.TryParse(dr["JurisdictionID"].ToString(), out i0) ? Convert.ToInt32(dr["JurisdictionID"]) : i0;
                            var lDivn = Int32.TryParse(dr["DivisionID"].ToString(), out i0) ? Convert.ToInt32(dr["DivisionID"]) : i0;
                            var lRegn = dr["Region"].ToString();

                            var leo = new LeoModel(lJurs, lDivn, lRegn);
                            cm.LeoBase = leo;

                            break;
                        case CustomerTypes.OtherBiz:

                            var bStId = Int32.TryParse(dr["CaFscStatusID"].ToString(), out i0) ? Convert.ToInt32(dr["CaFscStatusID"]) : i0;
                            var bgnSf = Boolean.TryParse(dr["CaHasGunSafe"].ToString(), out b0) ? Convert.ToBoolean(dr["CaHasGunSafe"]) : b0;
                            var bIsCz = Boolean.TryParse(dr["IsUsCitizen"].ToString(), out b0) ? Convert.ToBoolean(dr["IsUsCitizen"]) : b0;
                            var bResa = Boolean.TryParse(dr["BuyingForResale"].ToString(), out b0) ? Convert.ToBoolean(dr["BuyingForResale"]) : b0;

                            cm.CompanyName = cmp;
                            cm.CaFscStatus = bStId;
                            cm.CaHasGunSafe = bgnSf;
                            cm.IsCitizen = bIsCz;
                            cm.BuyForResale = bResa;

                            break;
                    }

                }

                /* DOCUMENTS */
                if (cdc > 0)
                {
                    /*  PRESENTATION GRID */
                    dr.NextResult();
                    if (!dr.HasRows) return cm;

                    while (dr.Read())
                    {
                        var i1 = Int32.TryParse(dr["VersionID"].ToString(), out i0) ? Convert.ToInt32(dr["VersionID"]) : i0;
                        var b1 = Boolean.TryParse(dr["IsCurrent"].ToString(), out b0) ? Convert.ToBoolean(dr["IsCurrent"]) : b0;
                        var b2 = Boolean.TryParse(dr["IsExpired"].ToString(), out b0) ? Convert.ToBoolean(dr["IsExpired"]) : b0;
                        var d1 = DateTime.TryParse(dr["ExpDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["ExpDate"]) : dt0;
                        var v1 = dr["StateAbbrev"].ToString();
                        var v2 = dr["DocGroup"].ToString();
                        var v3 = dr["DocumentType"].ToString();
                        var v4 = dr["DocNumber"].ToString();
                        var v5 = dr["ImgName"].ToString();
                        var v7 = dr["DocumentFolder"].ToString();

                        var dn = DecryptIt(v4);

                        var v8 = d1 == DateTime.MinValue ? "None" : d1.ToShortDateString();
                        var v9 = b2 ? "Expired" : "Active";
                        var v10 = string.Empty;

                        if (v5.Length > 0)
                        {
                            v10 = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(dc), v7, v5);
                            v10 += "?" + DateTime.Now.Ticks;
                        }

                        var s = new CustomerDoc(i1, b1, b2, v8, v1, v2, v3, dn, v9, v10);
                        pd.Add(s);  
                    }



                    /*  EDITABLE DOCUMENTS  */
                    dr.NextResult();

                    if (!dr.HasRows) return cm;

                    var bc = new BaseController();

                    while (dr.Read())
                    {
                        var did = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                        var typ = Int32.TryParse(dr["DocTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["DocTypeID"]) : i0;
                        var version = Int32.TryParse(dr["VersionID"].ToString(), out i0) ? Convert.ToInt32(dr["VersionID"]) : i0;
                        var expDate = DateTime.TryParse(dr["ExpDate"].ToString(), out dt0) ? Convert.ToDateTime(dr["ExpDate"]) : dt0;
                        var dtOfBth = DateTime.TryParse(dr["Dob"].ToString(), out dt0) ? Convert.ToDateTime(dr["Dob"]) : dt0;
                        var docImg = dr["ImgName"].ToString();
                        var docGrp = dr["DocGroup"].ToString();
                        var docTyp = dr["DocumentType"].ToString();
                        var docNum = dr["DocNumber"].ToString();
                        var docSub = dr["DocumentFolder"].ToString();

                        var dDoc = DecryptIt(docNum);

                        var exp = expDate == DateTime.MinValue ? "None" : expDate.ToShortDateString();
                        var dob = dtOfBth == DateTime.MinValue ? "" : dtOfBth.ToShortDateString();

                        var iUrl = string.Empty;

                        //var f = bc.GetDocFolderCode(typ);
  
                        if (docImg.Length > 0)
                        {
                            //iUrl = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(p), DecryptIt(f), docImg);
                            iUrl = string.Format("{0}/{1}/{2}/{3}", GetHostUrl(), DecryptIt(dc), docSub, docImg);
                            iUrl += "?" + DateTime.Now.Ticks;
                        }

                        var m = new CustomerDoc(did, version, exp, dob, iUrl, docGrp, docTyp, dDoc);
                        cd.Add(m);
                    }                         
                }

                cm.CustomerDocs = cd;
                cm.PresentationDocs = pd;

            }

            return cm;

        }

        public CustomerModel SetDocCurrent(int id)
        {
            var cm = new CustomerModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcSetDocCurrent");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cm;

                var i0 = 0;

                dr.Read();

                var cdc = Int32.TryParse(dr["CurDocCt"].ToString(), out i0) ? Convert.ToInt32(dr["CurDocCt"]) : i0;
                var arc = Int32.TryParse(dr["ArcDocCt"].ToString(), out i0) ? Convert.ToInt32(dr["ArcDocCt"]) : i0;

                cm = new CustomerModel(cdc, arc);
            }

            return cm;
        }


        public CustomerModel ArchiveDoc(int id)
        {
            var cm = new CustomerModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcArchiveDoc");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cm;

                var i0 = 0;

                dr.Read();

                var cdc = Int32.TryParse(dr["CurDocCt"].ToString(), out i0) ? Convert.ToInt32(dr["CurDocCt"]) : i0;
                var arc = Int32.TryParse(dr["ArcDocCt"].ToString(), out i0) ? Convert.ToInt32(dr["ArcDocCt"]) : i0;

                cm = new CustomerModel(cdc, arc);
            }

            return cm;
        }



        public CustomerModel GetOrderCustomer(int id)
        {
            var cm = new CustomerModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcOrderCustomerInfo");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return cm;

                var i0 = 0;
                Int64 bi = 0;
 
                dr.Read();

                var cid = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                var ctp = Int32.TryParse(dr["CustomerTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["CustomerTypeID"]) : i0;
                var zip = Int32.TryParse(dr["ZipCode"].ToString(), out i0) ? Convert.ToInt32(dr["ZipCode"]) : i0;
                var ext = Int32.TryParse(dr["ZipExt"].ToString(), out i0) ? Convert.ToInt32(dr["ZipExt"]) : i0;
                var phn = Int64.TryParse(dr["Phone"].ToString(), out bi) ? Convert.ToInt64(dr["Phone"]) : bi;

                var fnm = dr["FirstName"].ToString();
                var lnm = dr["LastName"].ToString();
                var eml = dr["EmailAddress"].ToString();
                var adr = dr["Address"].ToString();
                var ste = dr["Suite"].ToString();
                var cty = dr["City"].ToString(); 
                var sta = dr["State"].ToString();

                var cbm = new CustomerBaseModel();
                cbm.FirstName = fnm;
                cbm.LastName=lnm;
                cbm.EmailAddress = DecryptIt(eml);
                cbm.Address=adr;
                cbm.Suite=ste;
                cbm.City=cty;
                cbm.StateName=sta;
                cbm.Phone = PhoneToString(phn);
                cbm.ZipCode=zip;
                cbm.ZipExt=ext;

                cm = new CustomerModel(cid, ctp, cbm);

            }

            return cm;

        }

        public SupplierModel GetSupplierAddress(int id)
        {
            var sm = new SupplierModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcGetSupplierAddress");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return sm;

                var i0 = 0;
                dr.Read();

                var i1 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                var i2 = Int32.TryParse(dr["SupplierTypeID"].ToString(), out i0) ? Convert.ToInt32(dr["SupplierTypeID"]) : i0;
 
                var v1 = dr["FirstName"].ToString();
                var v2 = dr["LastName"].ToString();
                var v3 = dr["SupEmail"].ToString();
                var v4 = dr["SupPhone"].ToString();
                var v5 = dr["SupAddress"].ToString();
                var v6 = dr["SupCity"].ToString();
                var v7 = dr["SupState"].ToString();
                var v8 = dr["SupZipCode"].ToString();
                var v9 = dr["SupZipExt"].ToString();

                sm = new SupplierModel(i1, i2, v1, v2, v3, v4, v5, v6, v7, v8, v9);
            }

            return sm;

        }

        public CustomerBaseModel GetCustAddress(int id)
        {
            var b = new CustomerBaseModel();

            using (var conn = new SqlConnection(AdminSqlConnection))
            {
                var proc = ConfigurationHelper.GetPropertyValue("application", "ProcCustomerGetAddrById");
                var cmd = new SqlCommand(proc, conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();

                var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();
                if (!dr.HasRows) return b;

                var i0 = 0;
                dr.Read();

                var i1 = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
                var i2 = Int32.TryParse(dr["StateID"].ToString(), out i0) ? Convert.ToInt32(dr["StateID"]) : i0;
                var i3 = Int32.TryParse(dr["ZipCode"].ToString(), out i0) ? Convert.ToInt32(dr["ZipCode"]) : i0;
                var i4 = Int32.TryParse(dr["ZipExt"].ToString(), out i0) ? Convert.ToInt32(dr["ZipExt"]) : i0;

                var v1 = dr["Address"].ToString();
                var v2 = dr["Suite"].ToString();
                var v3 = dr["City"].ToString();

                b = new CustomerBaseModel(i1, i2, i3, i4, v1, v2, v3);
            }

            return b;
        }


        

    }
}