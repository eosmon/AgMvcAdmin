using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AgMvcAdmin.Common;
using WebBase.Configuration;

namespace AgMvcAdmin.Models
{
    public class ServicesContext : BaseModel
    {
        public void DeleteImageDocs(int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcDeleteCustomerImgDocs");
            var param = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
            AgBase.Calls.ProcOneParam(WebSqlConnection, proc, param);
        }

        public void AddSvcDocImage(PhotoUpload p, string userKey, string imgName, int id)
        {
            var proc = ConfigurationHelper.GetPropertyValue("application", "ProcInsertServiceDocImg");
            var catId = (int)p;
            var gd = new Guid();
            var uk = Guid.TryParse(userKey, out gd) ? Guid.Parse(userKey) : Guid.NewGuid();

            var param = new IDataParameter[4];
            param[0] = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = id };
            param[1] = new SqlParameter("@UserKey", SqlDbType.UniqueIdentifier) { Value = uk };
            param[2] = new SqlParameter("@CatID", SqlDbType.Int) { Value = catId };
            param[3] = new SqlParameter("@ImgName", SqlDbType.VarChar) { Value = imgName };
            AgBase.Calls.ProcParams(WebSqlConnection, proc, param);
        }

        public void UpdateImage(ServiceGunModel m)
        {
            var catId = (int)m.UploadType;
            var pn = ConfigurationHelper.GetPropertyValue("application", "ProcUpdateCustomerDocImg");

            var param = new IDataParameter[3];
            param[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = m.Id };
            param[1] = new SqlParameter("@CatID", SqlDbType.Int) { Value = catId };
            param[2] = new SqlParameter("@ImgName", SqlDbType.VarChar) { Value = m.Image1 };
            AgBase.Calls.ProcParams(WebSqlConnection, pn, param);
        }
    }
}