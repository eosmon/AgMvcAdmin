using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using HtmlAgilityPack;
using System.IO;
using System.Diagnostics;
using WebBase.Configuration;



namespace AgMvcAdmin.Models
{
    public class ConvertPdf : BaseModel
    {
        private readonly string invPth = ConfigurationHelper.GetPropertyValue("application", "a18");
        private readonly string invUrl = ConfigurationHelper.GetPropertyValue("application", "a19");

        public string MakeThisWork(string html, string fileName)
        {
            var newUrl = string.Empty;


            var fileDir = DecryptIt(invPth); //@"F:\GunBiz\NewInvoices\";
            string oldPath = Path.Combine(fileDir, fileName);
            File.WriteAllText(oldPath, html);

            var wp = DecryptIt(invUrl); ////http://localhost:8003/Invoices/";
            newUrl = wp + fileName;

            return newUrl;

        }






    }
}