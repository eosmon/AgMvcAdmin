using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tesseract;
using WebBase.Configuration;
using System.IO.Ports;

namespace AgMvcAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public void RunOcr()
        {

            var tt = Request["txt"];

 


            /************* OCR BELOW ***************/

            //var f = Request.Files;
            //if (f.Count == 0) { return; }
            //var fn = f[0].FileName;
            //var aPath = ConfigurationHelper.GetPropertyValue("application", "PathDocs");

            var iPath = @"F:\Development\AllGuns\Website\AgMvcAdmin\Common\Images\StateID.jpg";
            var tPath = @"F:\Development\AllGuns\Website\AgMvcAdmin\tessdata";

            //var nPath = string.Format("{0}StateID\\{1}", aPath, fn);
            //f[0].SaveAs(nPath);

            //var bmp = Image.FromStream(f[0].InputStream, true, true);

            var img = new Bitmap(iPath);
            TesseractEngine eg = new TesseractEngine(tPath, "eng", EngineMode.Default);
            //Page pg = eg.Process(img, PageSegMode.AutoOsd);
            Page pg = eg.Process(img, PageSegMode.Auto);
            var tx = pg.GetText();


 

        }
    }
}