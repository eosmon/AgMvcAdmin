using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AgMvcAdmin.Common;
using AgMvcAdmin.Models;
using AgMvcAdmin.Models.Common;
using AgMvcAdmin.Models.Menus;

namespace AgMvcAdmin.Controllers
{
    public class CustomerController : UploadBaseController
    {
        //
        // GET: /Customer/
        public ActionResult Register()
        {
            var cc = new CustomerContext();
            var l = cc.SetTempLogin();

            var pg = new PageModel(Pages.CustomerAdd);
            pg.Login = l;
            return View(pg);
        }

        [HttpPost]
        public ActionResult GetFfl()
        {
            var f1 = Request["ffl1"];
            var f2 = Request["ffl2"];
            var f3 = Request["ffl3"];
            var f4 = Request["ffl4"];
            var f5 = Request["ffl5"];
            var f6 = Request["ffl6"];

            var nf1 = 0;
            var nf2 = 0;

            var f = new FflLicenseModel();
            f.LicRegion = int.TryParse(f1, out nf1) ? Convert.ToInt32(f1) : 0;
            f.LicDistrict = int.TryParse(f2, out nf2) ? Convert.ToInt32(f2) : 0;
            f.LicCounty = f3;
            f.LicType = f4;
            f.LicExpCode = f5;
            f.LicSequence = f6;

            var fc = new FflContext();
            var ffl = fc.GetFflByLicense(f);

            return Json(new { fflData = ffl }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Create()
        {
            var cc = new CustomerContext();
            var bm = new CustomerBaseModel();
            var sm = new SecurityModel();
            var cm = new CustomerModel();
            var fm = new FflLicenseModel();
            var cr = new CurioRelicModel();
            var le = new LeoModel();

            var l = new List<Type>();
            l.Add(bm.GetType());
            l.Add(sm.GetType());
            l.Add(cm.GetType());
            l.Add(fm.GetType());
            l.Add(cr.GetType());
            l.Add(le.GetType());

            foreach (string key in Request.Form)
            {

                object val;
                var xInt = 0;
                var xBit = false;
                var v = SetPropTypes(l, key);
                if (v.ClassName == null) { continue; }

                if (v.PropertyType == "Int32") { val = Int32.TryParse(v.ValueName, out xInt) ? Convert.ToInt32(v.ValueName) : 0; }
                else if (v.PropertyType == "Boolean") { val = Boolean.TryParse(v.ValueName, out xBit) ? Convert.ToBoolean(v.ValueName) : false; }
                else { val = v.ValueName; }

                if (bm.GetType() == Type.GetType(v.ClassName)) { v.PropInfo.SetValue(bm, val); }
                else if (sm.GetType() == Type.GetType(v.ClassName)) { v.PropInfo.SetValue(sm, val); }
                else if (cm.GetType() == Type.GetType(v.ClassName)) { v.PropInfo.SetValue(cm, val); }
                else if (fm.GetType() == Type.GetType(v.ClassName)) { v.PropInfo.SetValue(fm, val); }
                else if (cr.GetType() == Type.GetType(v.ClassName)) { v.PropInfo.SetValue(cr, val); }
                else if (le.GetType() == Type.GetType(v.ClassName)) { v.PropInfo.SetValue(le, val); }
            }

            cm.CustomerBase = bm;
            cm.SecurityBase = sm;

            if (sm.UserName == string.Empty) { sm.UserName = string.Format("{0}_{1}", bm.FirstName, CookRandomStr(6)).ToUpper(); }
            if (sm.Password == string.Empty) { sm.Password = CookVeryRandomStr(12, true); }

            var cType = (CustomerTypes)cm.CustomerTypeId;
            var isCurio = false;

            switch (cType)
            {
                case CustomerTypes.PrivateParty:
                case CustomerTypes.OtherBiz:
                    break;
                case CustomerTypes.CommercialFFL:
                    cm.FedFireLicBase = fm;
                    break;
                case CustomerTypes.CurioRelic:
                    cm.FedFireLicBase = fm;
                    cm.CurioRelicBase = cr;
                    isCurio = true;
                    break;
                case CustomerTypes.LawEnforcement:
                    cm.LeoBase = le;

                    break;
            }

            var dob = string.Empty;
            if (cm.DobMonth > 0 && cm.DobDay > 0 && cm.DobYear > 0)
            {
                dob = string.Format("{0}/{1}/{2}", cm.DobMonth, cm.DobDay, cm.DobYear);
                cm.StrDob = dob;

                // check if customer is under 21
                var tod = DateTime.Today;
                var age = tod.Year - cm.DobYear;
                var i21 = age >= 21;
                cm.IsAge21 = i21;
            }

            // CLEANUP CITIZENSHIP
            if (cm.IsCitizen) { cm.IsPermResident = false; }


            cm.CustomerType = cType;

            var atn = Request["Action"];

            // HERE IS THE ADD/EDIT
            if (atn == "Create") { cm = cc.AddCustomer(cm); } else { cc.UpdateCustomer(cm); }
 
            if (Request.Files.Count > 0)
            {
                var r = new Random();
                var fName = string.Format("{0}-{1}-{2}.jpg", bm.FirstName, bm.LastName, r.Next());

                HttpFileCollectionBase files = Request.Files;
                SetProfilePic(files[0], cm.CustomerId, fName);
            }

            return Json(new { iCm = cm, iBm = bm, iSm = cm.SecurityBase }, JsonRequestBehavior.AllowGet);
        }






        private ValueProps SetPropTypes(List<Type> l, string key)
        {
            var v = new ValueProps();

            foreach (var p in l)
            {
                var k = p.GetProperty(key);

                if (k != null)
                {
                    v.PropInfo = k;
                    v.ClassName = p.FullName;
                    v.PropertyName = key;
                    v.PropertyType = k.PropertyType.Name;
                    v.ValueName = Request[key];
                    break;
                }
            }
            return v;
        }




        [HttpPost]
        public JsonResult TempLogin(string industry)
        {
            var cc = new CustomerContext();
            var l = cc.SetTempLogin();

            return Json(l, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddIndustry(string industry)
        {
            var cc = new CustomerContext();
            var am = cc.AddIndustry(industry);

            return Json(am, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddProfession(string indId, string prof)
        {
            var i0 = 0;
            var cc = new CustomerContext();
            var v1 = Int32.TryParse(indId, out i0) ? Convert.ToInt32(indId) : i0;
            var am = cc.AddProfession(v1, prof);

            return Json(am, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetProfessions(string indId)
        {
            var i0 = 0;
            var mm = new MenuModel();
            var v1 = Int32.TryParse(indId, out i0) ? Convert.ToInt32(indId) : i0;
            var sl = mm.GetProfessions(v1);

            return Json(sl, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetDocumentTypes(string catId)
        {
            var i0 = 0;
            var mm = new MenuModel();
            var v1 = Int32.TryParse(catId, out i0) ? Convert.ToInt32(catId) : i0;
            var sl = mm.GetDocTypes(v1);

            return Json(sl, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSubMenus(string docId)
        {
            var i0 = 0;
            var mm = new CustomerContext();
            var v1 = Int32.TryParse(docId, out i0) ? Convert.ToInt32(docId) : i0;
            var sl = mm.GetMenuSubCats(v1);

            return Json(sl, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult NewSupplier()
        {
            var i0 = 0;
            var dt0 = DateTime.MinValue;
            var i1 = Int32.TryParse(Request["Stp"], out i0) ? Convert.ToInt32(Request["Stp"]) : i0;
            var i2 = Int32.TryParse(Request["Sta"], out i0) ? Convert.ToInt32(Request["Sta"]) : i0;
            var i3 = Int32.TryParse(Request["Idt"], out i0) ? Convert.ToInt32(Request["Idt"]) : i0;
            var i4 = Int32.TryParse(Request["Ids"], out i0) ? Convert.ToInt32(Request["Ids"]) : i0;

            var v1 = Request["Fir"];
            var v2 = Request["Las"];
            var v3 = Request["Org"];
            var v4 = Request["Adr"];
            var v5 = Request["Cty"];
            var v6 = Request["Zip"];
            var v7 = Request["Ext"];
            var v8 = Request["Phn"];
            var v9 = Request["Eml"];
            var v10 = Request["Cfl"];
            var v11 = Request["Idn"];

            var dt1 = DateTime.TryParse(Request["Cxp"], out dt0) ? Convert.ToDateTime(Request["Cxp"]) : dt0;
            var dt2 = DateTime.TryParse(Request["Dob"], out dt0) ? Convert.ToDateTime(Request["Dob"]) : dt0;
            var dt3 = DateTime.TryParse(Request["Exp"], out dt0) ? Convert.ToDateTime(Request["Exp"]) : dt0;

            var sm = new SupplierModel(i1, i2, i3, i4, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, dt1, dt2, dt3);
            var cc = new CustomerContext();
            var id = cc.AddNewSupplier(sm);
            return Json(id, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ModSupplier()
        {
            var i0 = 0;
            var dt0 = DateTime.MinValue;
            var i1 = Int32.TryParse(Request["Sid"], out i0) ? Convert.ToInt32(Request["Sid"]) : i0;
            var i2 = Int32.TryParse(Request["Sta"], out i0) ? Convert.ToInt32(Request["Sta"]) : i0;
            var i3 = Int32.TryParse(Request["Idt"], out i0) ? Convert.ToInt32(Request["Idt"]) : i0;
            var i4 = Int32.TryParse(Request["Ids"], out i0) ? Convert.ToInt32(Request["Ids"]) : i0;

            var v1 = Request["Fir"];
            var v2 = Request["Las"];
            var v3 = Request["Org"];
            var v4 = Request["Adr"];
            var v5 = Request["Cty"];
            var v6 = Request["Zip"];
            var v7 = Request["Ext"];
            var v8 = Request["Phn"];
            var v9 = Request["Eml"];
            var v10 = Request["Cfl"];
            var v11 = Request["Idn"];

            var dt1 = DateTime.TryParse(Request["Cxp"], out dt0) ? Convert.ToDateTime(Request["Cxp"]) : dt0;
            var dt2 = DateTime.TryParse(Request["Dob"], out dt0) ? Convert.ToDateTime(Request["Dob"]) : dt0;
            var dt3 = DateTime.TryParse(Request["Exp"], out dt0) ? Convert.ToDateTime(Request["Exp"]) : dt0;

            var sm = new SupplierModel(dt1, dt2, dt3, i1, i2, i3, i4, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11);
            var cc = new CustomerContext();
            cc.UpdateSupplier(sm);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }



        [HttpPost] 
        public JsonResult AddDocument()
        {
            var i0 = 0;
            var g0 = new Guid();
            var dt0 = DateTime.MinValue;
            var b0 = false;

            var v1 = Int32.TryParse(Request["Uid"], out i0) ? Convert.ToInt32(Request["Uid"]) : i0;
            var v2 = Int32.TryParse(Request["Cat"], out i0) ? Convert.ToInt32(Request["Cat"]) : i0;
            var v3 = Int32.TryParse(Request["Typ"], out i0) ? Convert.ToInt32(Request["Typ"]) : i0;
            var v4 = Int32.TryParse(Request["Sta"], out i0) ? Convert.ToInt32(Request["Sta"]) : i0;
            var v5 = DateTime.TryParse(Request["Exp"], out dt0) ? Convert.ToDateTime(Request["Exp"]) : dt0;
            var v6 = DateTime.TryParse(Request["Dob"], out dt0) ? Convert.ToDateTime(Request["Dob"]) : dt0;
            var v7 = Guid.TryParse(Request["Uky"], out g0) ? Guid.Parse(Request["Uky"]) : g0;
            var v8 = Request["Did"];

            var b1 = Boolean.TryParse(Request["Adr"], out b0) ? Convert.ToBoolean(Request["Adr"]) : true;
            var b2 = Boolean.TryParse(Request["Rid"], out b0) ? Convert.ToBoolean(Request["Rid"]) : true;

            var cd = new CustomerDoc(v1, v2, v3, v4, v5, v6, v7, v8, b1, b2);
            var cc = new CustomerContext();

            var d = cc.AddCustomerDocument(cd);

            if (Request.Files.Count > 0)
            {
                var dt = Enum.GetName(typeof(DocTypes), v3);
                var fName = string.Format("{0}-{1}-{2}.jpg", v1, d.Id, dt);
                var fPath = string.Format("{0}\\{1}", d.SubFolder, fName);

                HttpFileCollectionBase files = Request.Files;
                CookImgDoc(files[0], d.Id, fPath);
            }


            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EditDocument()
        {
            var i0 = 0;
            var dt0 = DateTime.MinValue;
            var b0 = false;

            var v1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0;
            var v8 = Int32.TryParse(Request["Uid"], out i0) ? Convert.ToInt32(Request["Uid"]) : i0;
            var v2 = Int32.TryParse(Request["Cat"], out i0) ? Convert.ToInt32(Request["Cat"]) : i0;
            var v3 = Int32.TryParse(Request["Typ"], out i0) ? Convert.ToInt32(Request["Typ"]) : i0;
            var v4 = Int32.TryParse(Request["Sta"], out i0) ? Convert.ToInt32(Request["Sta"]) : i0;
            var v5 = DateTime.TryParse(Request["Exp"], out dt0) ? Convert.ToDateTime(Request["Exp"]) : dt0;
            var v6 = DateTime.TryParse(Request["Dob"], out dt0) ? Convert.ToDateTime(Request["Dob"]) : dt0;
            var v7 = Request["Num"];

            var b1 = Boolean.TryParse(Request["Adr"], out b0) ? Convert.ToBoolean(Request["Adr"]) : true;
            var b2 = Boolean.TryParse(Request["Rid"], out b0) ? Convert.ToBoolean(Request["Rid"]) : true;

            var cd = new CustomerDoc(v1, v2, v3, v4, v5, v6, v7, b1, b2);
            var cc = new CustomerContext();

            var d = cc.UpdateDocument(cd);

            if (Request.Files.Count > 0)
            {
                var dt = Enum.GetName(typeof(DocTypes), v3);
                var fName = string.Format("{0}-{1}-{2}.jpg", v1, d.Id, dt);
                var fPath = string.Format("{0}\\{1}", d.SubFolder, fName);

                HttpFileCollectionBase files = Request.Files;
                CookImgDoc(files[0], d.Id, fPath);
            }


            return Json("", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SetDocCur()
        {
            var i0 = 0;
            var v1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0;
            var cc = new CustomerContext();
            var d = cc.SetDocCurrent(v1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetAllDocs()
        {
            var i0 = 0;
            var b0 = false;
            var v1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0;
            var v2 = Boolean.TryParse(Request["Cur"], out b0) ? Convert.ToBoolean(Request["Cur"]) : b0;

            var cc = new CustomerContext();

            var d = cc.GetCustomerDocs(v1, v2);

            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ArchiveDoc()
        {
            var i0 = 0;
            var v1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0;

            var cc = new CustomerContext();
            var d = cc.ArchiveDoc(v1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetDocById(string id)
        {
            var i0 = 0;
            var v1 = Int32.TryParse(id, out i0) ? Convert.ToInt32(id) : i0;

            var cc = new CustomerContext();
            var d = cc.GetCustomerDocById(v1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetCustomer(string id)
        {
            var i0 = 0;
            var v1 = Int32.TryParse(id, out i0) ? Convert.ToInt32(id) : i0;
            var cc = new CustomerContext();

            var d = cc.GetCustomerById(v1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPptSellerInfo(string isi)
        {
            var i0 = 0;
            var i1 = Int32.TryParse(isi, out i0) ? Convert.ToInt32(isi) : i0;
            var cc = new CustomerContext();

            var d = cc.PptSellerInfoGet(i1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetSupPptOtherCt(string id, string ct)
        {
            var i0 = 0;
            var i1 = Int32.TryParse(id, out i0) ? Convert.ToInt32(id) : i0;
            var i2 = Int32.TryParse(ct, out i0) ? Convert.ToInt32(ct) : i0;
            var cc = new CustomerContext();

            cc.SetPptOtherCount(i1, i2);

            return Json("success", JsonRequestBehavior.AllowGet);
        }


        


        [HttpPost]
        public JsonResult GetCustAddress(string id)
        {
            var i0 = 0;
            var v1 = Int32.TryParse(id, out i0) ? Convert.ToInt32(id) : i0;
            var cc = new CustomerContext();

            var d = cc.GetCustAddress(v1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetOrderCustomer(string id)
        {
            var i0 = 0;
            var v1 = Int32.TryParse(id, out i0) ? Convert.ToInt32(id) : i0;
            var cc = new CustomerContext();

            var d = cc.GetOrderCustomer(v1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetSupplierById(string id)
        {
            var i0 = 0;
            var v1 = Int32.TryParse(id, out i0) ? Convert.ToInt32(id) : i0;
            var cc = new CustomerContext();

            var d = cc.GetSupplierAddress(v1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetItemSupplier(string Id)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Id, out x0) ? Convert.ToInt32(Id) : 0;

            var cc = new CustomerContext();
            var su = cc.GetSupplier(v1);

            return Json(su, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult FindCustomers(string search)
        {
            var cc = new CustomerContext();
            var g = cc.SearchCustomers(search);

            return Json(g, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult FindSuppliers(string sid, string search)
        {
            var i0 = 0;
            var i1 = Int32.TryParse(sid, out i0) ? Convert.ToInt32(sid) : i0;

            var cc = new CustomerContext();
            var g = cc.SearchSuppliers(i1, search);

            return Json(g, JsonRequestBehavior.AllowGet);
        }




        public struct ValueProps
        {
            public PropertyInfo PropInfo { get; set; }
            public string ClassName { get; set; }
            public string PropertyName { get; set; }
            public string PropertyType { get; set; }
            public string ValueName { get; set; }
        };
	}
}