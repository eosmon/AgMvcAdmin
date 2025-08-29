using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgMvcAdmin.Common;
using WebBase.Configuration;

namespace AgMvcAdmin.Models
{
    public class CommonModel : BaseModel
    {
        public string GetBaseImgPath(PhotoUpload imgPath)
        {
            var n = "Path" + Enum.GetName(typeof(PhotoUpload), (int)imgPath);
            var p = ConfigurationHelper.GetPropertyValue("application", n);
            var s = DecryptIt(p);

            return s;
        }

        public string CookFileName(PhotoUpload p, string userId)
        {
            var str = String.Empty;
            var n = Enum.GetName(typeof(PhotoUpload), (int)p) + "File";
            var file = ConfigurationHelper.GetPropertyValue("application", n);
            var f = DecryptIt(file);
            str = userId + f;
            return str;
        }

        public string CookFileName(string inqNo, int id, int i)
        {
            var str = String.Empty;
            var pre = CookFileName(PhotoUpload.Svc9, inqNo);
            str = String.Format("{0}{1}_{2}", pre, id, i);
            return str;
        }
    }
}