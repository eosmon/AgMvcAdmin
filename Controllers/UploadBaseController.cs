using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AgMvcAdmin.Common;
using AgMvcAdmin.Models;
using WebBase.Configuration;

namespace AgMvcAdmin.Controllers
{
    public class UploadBaseController : BaseController
    {
        public string UploadCustRegDoc(ServiceGunModel m)
        {
            var docs = String.Empty;

            try
            {
                var photoId = string.Empty;

                photoId = m.IsService ? m.InquiryNumber.ToString() : m.Id.ToString();

                CommonModel sm = new CommonModel();
                for (int i = 0; i < m.Files.Count; i++)
                {
                    var file = m.Files[i];

                    string fname;
                    string imgName = string.Empty;


                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" ||
                        Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    var path = String.Format(":\\{0}", fname);
                    var ext = Path.GetExtension(path);

                    m.UploadType = (PhotoUpload)Enum.Parse(typeof(PhotoUpload), m.GroupIdArr[i]);

                    var bp = sm.GetBaseImgPath(m.UploadType);

                    imgName = sm.CookFileName(m.UploadType, photoId);
                    m.Image1 = imgName + ext;

                    //// Get the complete folder path and store the file inside it.  
                    ////fname = Path.Combine(Server.MapPath("~/Docs/"), fext);
                    fname = Path.Combine(bp, m.Image1);
                    file.SaveAs(fname);

                    if (System.IO.File.Exists(fname))
                    {
                        docs += docs.Length > 0 ? ", " + AddDocStr(m.UploadType) : AddDocStr(m.UploadType);
                        ServicesContext sc = new ServicesContext();
                        if (m.IsService)
                        {
                            sc.AddSvcDocImage(m.UploadType, m.UserKey, m.Image1, m.Id);
                        }
                        else
                        {
                            sc.UpdateImage(m);
                        }

                    }

                }
                return docs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void WipeImageDocs(int id)
        {
            var cm = new CommonModel();
            var f = id + "_*";
            var path = cm.DecryptIt(ConfigurationHelper.GetPropertyValue("application", "PathSvcB"));

            var files = Directory.GetFiles(path, f, SearchOption.AllDirectories);

            foreach (var fi in files)
            {
                System.IO.File.Delete(fi);
            }

            ServicesContext sc = new ServicesContext();
            sc.DeleteImageDocs(id);
        }
    }
}