using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgMvcAdmin.Common;
using AgMvcAdmin.Models;
using AgMvcAdmin.Models.Menus;
using AgMvcAdmin.Models.Common;
using AppBase;

namespace AgMvcAdmin.Controllers
{
    public class OrdersController : BaseController
    {
        //
        // GET: /Orders/
        //public ActionResult NewOrder()
        //{
        //    var pg = new PageModel(Pages.OrdersCreate);
        //    return View(pg);
        //}

        public ActionResult CookOrder()
        {

            //var cc = new CustomerContext();
            var l = new SecurityModel("", "");

            var pg = new PageModel(Pages.OrdersCreate);
            pg.Login = l;
            return View(pg);
        }

        public ActionResult TestOrder()
        {
            var l = new SecurityModel("", "");

            var pg = new PageModel(Pages.OrdersCreate);
            pg.Login = l;
            return View(pg);
        }

        public ActionResult OrderList()
        {
            var l = new SecurityModel("", "");

            var pg = new PageModel(Pages.OrdersExisting);
            pg.Login = l;
            return View(pg);
        }


        [HttpPost]
        public JsonResult GetOrder()
        {
            var i0 = 0;
            var dt0 = DateTime.MinValue;

            var i1 = Int32.TryParse(Request["oid"], out i0) ? Convert.ToInt32(Request["oid"]) : i0;

            var oc = new OrderContext();
            var iv = oc.GetFullInvoice(i1);

            return Json(iv, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult AllOrders()
        {
            var oc = new OrderContext();
            var iv = oc.GetOrdersList();

            return Json(iv, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public JsonResult RenderPrintInvoice()
        //{
        //    var i0 = 0;
        //    var dt0 = DateTime.MinValue;

        //    var i1 = Int32.TryParse(Request["oid"], out i0) ? Convert.ToInt32(Request["oid"]) : i0;

        //    var oc = new OrderContext();
        //    var iv = oc.SetPrintInvoice(i1);

        //    return Json(iv, JsonRequestBehavior.AllowGet);

        //}

        [HttpPost]
        public JsonResult SaveInvoice()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["oid"], out i0) ? Convert.ToInt32(Request["oid"]) : i0;

            var oc = new OrderContext();
            var d = oc.SetPrintInvoice(i1);

            var sub = String.Format("{0:C}", d.SubTotal);
            var tax = String.Format("{0:C}", d.SalesTax);
            var ttl = String.Format("{0:C}", d.OrderTotal);
            var bpd = String.Format("{0:C}", d.BalancePaid);
            var due = String.Format("{0:C}", d.BalanceDue);
            var ocd = d.OrderCode;
            var hdr = d.Header;
            var rep = d.SalesRep;
            var pay = d.PayMethods;
            var ful = d.FulfillTypes;
            var ffl = d.FflCode;
            var tcd = d.TermsCond;
            var lib = d.LiabilityTxt;
            var dat = d.StrOrderDate;
            var nts = d.Notes;
            var hnt = nts.Length > 0;

            var con = d.CustAddress.OrgName;
            var cfn = d.CustAddress.FirstName;
            var cln = d.CustAddress.LastName;
            var cad = d.CustAddress.Address;
            var cty = d.CustAddress.City;
            var cst = d.CustAddress.StateName;
            var czp = d.CustAddress.ZipCode;
            var cph = d.CustAddress.Phone;
            var cem = d.CustAddress.EmailAddress;

            var son = d.ShopAddress.OrgName;
            var sad = d.ShopAddress.Address;
            var sty = d.ShopAddress.City;
            var sst = d.ShopAddress.StateName;
            var szp = d.ShopAddress.ZipCode;
            var sph = d.ShopAddress.Phone;
            var sem = d.ShopAddress.EmailAddress;

            var nsa = sty + ", " + sst + " " + szp;
            var cxn = cfn + " " + cln;
            var ccz = cty + ", " + cst + " " + czp;
            //var bgc = "#FFFFFF";

            var t = d.OrderTransactions[0].OrderCartItems;

            var a = "";

            a += "<!DOCTYPE html><html><head><link href=\"https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css\" rel=\"stylesheet\"><link href=\"/Content/bootstrap.css\" rel=\"stylesheet\" /><link href=\"/Content/site.css\" rel=\"stylesheet\" /><link href=\"/Content/main.css\" rel=\"stylesheet\" /><link href=\"/Content/bootstrap-select.css\" rel=\"stylesheet\" /><link href=\"/Content/jasney-bootstrap.min.css\" rel=\"stylesheet\" /><link href=\"/Content/lobibox.css\" rel=\"stylesheet\" /><link href=\"/Content/orders.css\" rel=\"stylesheet\"><link href=\"/Content/customer.css\" rel=\"stylesheet\"></head><body>";

            a += "<div id=\"dvPrint\" class=\"ord-print-main\" style=\"border:solid 1px black; padding-bottom:200px\">";
            a += "<div style=\"width:100%; border:solid 1px black\">";
            a += "<div class=\"ord-top\" style=\"background-color:black;\">";
            a += "<div class=\"ord-top-logo\"><img src=\"../Common/Images/Print_Logo.png\" style=\"width:155px; height:auto;\" /></div>";
            a += "<div class=\"ord-top-biz-info\">";
            a += "<div id=\"dvBzAddr\" class=\"ord-print-addr\">" + sad + "</div>";
            a += "<div id=\"dvBzCszp\" class=\"ord-print-addr\">" + nsa + "</div>";
            a += "<div class=\"ord-print-addr\">Phone: <span id=\"spBzPhon\">" + sph + "</span></div>";

            a += "<div class=\"ord-tag-promo\">";
            a += "<div style=\"display:inline-block; width:45%; text-align:right; padding-right:8px;\" id=\"dvWeb\">ARGENT ECM</div>";
            a += "<div style=\"display:inline-block; width:45%; text-align:left; padding-left:10px;\" id=\"dvFfl\">FFL# <span id=\"spBzFfl\">" + ffl + "</span></div>";
            a += "</div>";
            a += "<div class=\"ord-print-used\">** WE BUY USED GUNS **</div>";
            a += "<div id=\"dvBzEmal\" style=\"padding-top:3px; font-size:.8em;\">" + sem + "</div>";
            a += "</div>";
            a += "<div class=\"ord-print-ord-num\">Order #: <span id=\"spOrdNum\" style=\"font-weight:bold;\">" + ocd + "</span></div>";
            a += "<div class=\"ord-print-heading\" id=\"dvInvTtl\">" + hdr + "</div>";
            a += "<div class=\"ord-top-cust\">";
            a += "<div style=\"width:100%\">";
            a += "<div style=\"width:40%; text-align:left; display:inline-block;\">Customer:</div>";
            a += "<div class=\"ord-print-date\">Date:<span style=\"font-weight:bold; padding-left:5px;\" id=\"spOrdDat\">" + dat + "</span></div>";
            a += "</div>";
            a += "<div class=\"ord-print-customer\">";
            a += "<div id=\"dvCsNam\">" + cxn + "</div>";
            a += "<div id=\"dvCsAdr\">" + cad + "</div>";
            a += "<div id=\"dvCsCsz\">" + ccz + "</div>";
            a += "<div><span id=\"spCsPhn\">" + cph + "</span> <span id=\"spCsEml\" style=\"padding-left:10px;\">" + cem + "</span></div>";
            a += "</div>";
            a += "</div>";
            a += "<div style=\"background-color:white;\"></div>";
            a += "</div>";
            a += "</div>";
            a += "<div style=\"padding-top:15px; padding-bottom:15px;\">";
            a += "<div class=\"ord-print-tdt\">";
            a += "<div class=\"ord-print-ttl\" style=\"border-left: 0\">Sold By:</div>";
            a += "<div class=\"ord-print-txt\" id=\"dvSalesRp\">" + rep + "</div>";
            a += "<div class=\"ord-print-ttl\">Fulfillment:</div>";
            a += "<div class=\"ord-print-txt\" id=\"dvFulfill\">" + ful + "</div>";
            a += "<div class=\"ord-print-ttl\">Payment Type:</div>";
            a += "<div class=\"ord-print-txt\" id=\"dvPayMthd\">" + pay + "</div>";
            a += "</div>";
            a += "</div>";
            a += "<div class=\"ord-print-top\">";
            a += "<div id=\"dvPrintGrid\">";
            a += "<div class=\"ord-print-hdr\" id=\"dvPrtGrdHdr\">";
            a += "<div class=\"ord-print\">Item</div>";
            a += "<div class=\"ord-print\">Category</div>";
            a += "<div class=\"ord-print\">Inventory #</div>";
            a += "<div class=\"ord-print\" style=\"text-align:left; padding-left:10px;\">Description</div>";
            a += "<div class=\"ord-print\">Units</div>";
            a += "<div class=\"ord-print\">Price</div>";
            a += "<div class=\"ord-print\">Amount</div>";
            a += "<div class=\"ord-print\" style=\"border-right:solid 1px black;\">Tax</div>";
            a += "</div>";
            a += "<div id=\"dvPrintRows\">";

            foreach (var item in t)
            {

                var cTrw = item.IsTaxRow;
                var cRid = item.RowId;
                var cCag = item.CategoryId;
                var cTtp = item.TransTypeId;
                var cUnt = cTrw ? string.Empty : item.Units.ToString();
                var cPrc = cTrw ? string.Empty : String.Format("{0:C}", item.Price);
                var cExt = cTrw ? string.Empty : String.Format("{0:C}", item.Extension);
                var cCat = item.Category;
                var cSid = item.SrcInvDesc;
                var cIds = item.ItemDesc;
                var cTax = item.Taxable;
                var cIsr = item.IsSellerRow;
                var cIsh = item.IsShipRow;
                var cIpu = item.IsPickupRow;
                var cIdl = item.IsDeliverRow;
                var cFid = item.FeeID;
                var cFee = item.IsFee;

                var cAsh = item.AddressShipping;
                var cAdl = item.AddressDelivery;
                var cApu = item.AddressPickup;
                var cAsl = item.AddressSeller;


                a += "<div class=\"ord-print-row\">";
                a += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cRid + ".</div>";
                a += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cCat + "</div>";
                a += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cSid + "</div>";
                a += "<div class=\"ord-row\" style=\"border-left:solid 1px black; text-align:left; padding-left:10px;\">";
                a += cIds;
                if (cFee)
                {
                    switch (cFid)
                    {
                        case 11:
                            a += setAddress(cAsh);
                            break;
                    }
                }
                if (cCag > 0 && !cFee && cTtp == 103) // transfers only: no fees, category required
                {
                    a += setAddress(cAsl);
                }
                a += "</div>";


                a += "<div class=\"ord-row\" style=\"border-left:solid 1px black;\">" + cUnt + "</div>";
                a += "<div class=\"ord-row\" style=\"border-left:solid 1px black; text-align:right; padding-right:10px;\">" + cPrc + "</div>";
                a += "<div class=\"ord-row\" style=\"border-left:solid 1px black; text-align:right; padding-right:10px;\">" + cExt + "</div>";
                a += "<div class=\"ord-row\" style=\"border-left:solid 1px black; border-right:solid 1px black;\">" + cTax + "</div>";
                a += "</div>";

                //bgc = bgc == "#E3E5E7" ? "#FFFFFF" : "#E3E5E7";

            };

            a += "</div>";
            if (hnt)
            {
                a += "<div class=\"ord-print-bass\">";
                a += "<div class=\"ord-print-notes\"><span style=\"font-weight:bold;\">Notes: </span>" + nts + "</div>";
                a += "<div class=\"ord-print-bdr-lb\"></div>";
                a += "<div class=\"ord-print-bdr-lb\"></div>";
                a += "<div class=\"ord-print-bdr-lb\"></div>";
                a += "<div class=\"ord-print-bdr-lb\" style=\"border-right:solid 1px black;\"></div>";
                a += "</div>";
            }

            a += "<div id=\"dvPrintWrap\" style=\"page-break-inside: avoid; page-break-before: auto;\">";
            a += "<div class=\"ord-print-bass\">";
            a += "<div class=\"ord-print-tc-ttl\">CUSTOMER TERMS & CONDITIONS</div>";
            a += "<div class=\"ord-print-bdr-lb\"></div>";
            a += "<div class=\"ord-print-bdr-lb\"></div>";
            a += "<div class=\"ord-print-bdr-lb\"></div>";
            a += "<div class=\"ord-print-bdr-lb\" style=\"border-right:solid 1px black;\"></div>";
            a += "</div>";
            a += "<div class=\"ord-print-bass\">";
            a += "<div class=\"ord-print-tc-txt\" id=\"dvTrmCond\">" + tcd + "</div>";
            a += "<div style=\"grid-column-start: 5; grid-column-end: 7; border-left:solid 1px black;\">";
            a += "<div class=\"ord-print-totals-txt\">Sub-Total: </div>";
            a += "<div class=\"ord-print-totals-txt\">Sales Tax: </div>";
            a += "<div class=\"ord-print-totals-txt\">Order Total: </div>";
            a += "<div class=\"ord-print-totals-txt\">Amount Paid: </div>";
            a += "<div class=\"ord-print-totals-txt\">Balance Due: </div>";
            a += "</div>";
            a += "<div style=\"border-left:solid 1px black;\">";
            a += "<div class=\"ord-print-totals-amt\" id=\"dvOrdSub\">" + sub + "</div>";
            a += "<div class=\"ord-print-totals-amt\" id=\"dvOrdTax\">" + tax + "</div>";
            a += "<div class=\"ord-print-totals-amt\" id=\"dvOrdTtl\">" + ttl + "</div>";
            a += "<div class=\"ord-print-totals-amt\" id=\"dvOrdApd\">" + bpd + "</div>";
            a += "<div class=\"ord-print-totals-amt\" id=\"dvOrdAdu\">" + due + "</div>";
            a += "</div>";
            a += "<div style=\"border-right:solid 1px black;\">";
            a += "<div class=\"ord-print-ttl-end\">&nbsp;</div>";
            a += "<div class=\"ord-print-ttl-end\">&nbsp;</div>";
            a += "<div class=\"ord-print-ttl-end\">&nbsp;</div>";
            a += "<div class=\"ord-print-ttl-end\">&nbsp;</div>";
            a += "<div class=\"ord-print-ttl-end\">&nbsp;</div>";
            a += "</div>";
            a += "</div>";
            a += "</div>";
            a += "<div style=\"width:100%\">";
            a += "<div style=\"padding-left:5px; padding-top:10px; font-size:13px;text-align:left;\"><span style=\"font-weight:bold;\">LIABILITY: </span> <span id=\"spLiabTxt\">" + lib + "</span></div>";
            a += "</div>";
            a += "<div style=\"width:100%; text-align:left\">";
            a += "<div class=\"ord-sig-line\" style=\"padding-top:25px; padding-left:5px; text-align:left;\">";
            a += "<div style=\"height:25px;\">I have read and agree to the these conditions:</div>";
            a += "<div style=\"border-bottom:solid 1px black; border-right:solid 1px black\">&nbsp;</div>";
            a += "<div style=\"border-bottom:solid 1px black\">&nbsp;</div>";
            a += "</div>";
            a += "<div class=\"ord-sig-line\" style=\"padding-top:3px; padding-left:5px;\">";
            a += "<div style=\"height:25px;\"></div>";
            a += "<div style=\"font-weight:bold;\">Customer Signature</div>";
            a += "<div style=\"font-weight:bold;\">Date</div>";
            a += "</div>";
            a += "</div>";
            //a += "</div>";
            a += "</div>";
            a += "</div>";
            a += "</div>";
            a += "</body></html>";

            var xcn = cfn + '_' + cln;
            var xcb = con.Length > 2 ? xcn + "_" + con : xcn;
            var xcd = DateTime.Now.ToString("MMddyy");
            var nfn = xcb.Replace(" ","") + "_" + xcd + ".htm";

            var cp = new ConvertPdf();
            var u = cp.MakeThisWork(a, nfn);
            d.InvoiceUrl = u;

            return Json(d, JsonRequestBehavior.AllowGet);

        }

        public string setAddress(OrderAddress o)
        {
            var a = "";

            var dsc = o.ItemDesc;
            var ful = o.FullName;
            var org = o.OrgName;
            var fnm = o.FirstName;
            var lnm = o.LastName;
            var adr = o.Address;
            var cty = o.City;
            var sta = o.StateName;
            var zip = o.ZipCode;
            var ext = o.ZipExt;
            var eml = o.EmailAddress;
            var phn = o.Phone;
            var ffl = o.StrFFL;

            var fzp = ext.Length > 0 ? zip + "-" + ext : zip;
            var csz = cty + ", " + sta + " " + fzp;
            var nem = eml.Length > 0 ? " E. " + eml : "";

            a += "<div style=\"font-weight:bold; font size:12px; padding-top:10px;\">" + dsc + "</div>";
            a += "<div style=\"font-size: .9em; padding-bottom:10px;\">";
            a += "<div>" + fnm + " " + lnm + "</div>";
            a += "<div>" + org + "</div>";
            a += "<div>" + adr + "</div>";
            a += "<div>" + csz + "</div>";
            a += "<div>P. " + phn + nem + "</div>";
            if (ffl.Length > 0)
            {
                a += "<div>FFL: " + ffl + "</div>";
            }
            a += "</div>";

            return a;
        }

        [HttpPost]
        public JsonResult NixOrder()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["oid"], out i0) ? Convert.ToInt32(Request["oid"]) : i0;

            var oc = new OrderContext();
            oc.DeleteOrder(i1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NixTransaction()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0;

            var oc = new OrderContext();
            oc.DeleteTransaction(i1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NixInvoiceTrans()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["oid"], out i0) ? Convert.ToInt32(Request["oid"]) : i0;
            var i2 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0;

            var oc = new OrderContext();
            var o = oc.DeleteInvoiceTransaction(i1, i2);

            return Json(o, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult NixTransactionCart()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0;

            var oc = new OrderContext();
            oc.DeleteTransactionCart(i1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ResetTransType()
        {
            var i0 = 0;
            var b0 = false;
            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0;
            var b1 = Boolean.TryParse(Request["ppt"], out b0) ? Convert.ToBoolean(Request["ppt"]) : b0;

            var oc = new OrderContext();
            oc.ResetFflTransType(i1, b1);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetLocationById(string Id)
        {
            var i0 = 0;
            var oc = new OrderContext();
            var v1 = Int32.TryParse(Id, out i0) ? Convert.ToInt32(Id) : i0;
            var sl = oc.GetLocation(v1);

            return Json(sl, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchOrderGuns(string mfg, string typ, string cal, string cok, string str)
        {
            if (str.Length < 2)
            {
                return Json("string too short", JsonRequestBehavior.AllowGet);
            }

            var x0 = 0;
            var v1 = Int32.TryParse(mfg, out x0) ? Convert.ToInt32(mfg) : 0;
            var v2 = Int32.TryParse(typ, out x0) ? Convert.ToInt32(typ) : 0;
            var v3 = Int32.TryParse(cal, out x0) ? Convert.ToInt32(cal) : 0;
            var v4 = Int32.TryParse(cok, out x0) ? Convert.ToInt32(cok) : -1;

            var gm = new GunModel(v1, v2, v3, v4, str);
            var oc = new OrderContext();
            var g = oc.SearchGuns(gm);

            return Json(g, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchOrderAmmo(string mfg, string cal, string atp, string btp, string str)
        {
            if (str.Length < 2)
            {
                return Json("string too short", JsonRequestBehavior.AllowGet);
            }

            var x0 = 0;
            var v1 = Int32.TryParse(mfg, out x0) ? Convert.ToInt32(mfg) : 0;
            var v2 = Int32.TryParse(cal, out x0) ? Convert.ToInt32(cal) : 0;
            var v3 = Int32.TryParse(atp, out x0) ? Convert.ToInt32(atp) : 0;
            var v4 = Int32.TryParse(btp, out x0) ? Convert.ToInt32(btp) : 0;

            var am = new AmmoModel(v1, v2, v3, v4, str);
            var oc = new OrderContext();
            var a = oc.SearchAmmo(am);

            return Json(a, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchOrderMerchandise(string mfg, string cat, string str)
        {
            if (str.Length < 2)
            {
                return Json("string too short", JsonRequestBehavior.AllowGet);
            }

            var x0 = 0;
            var v1 = Int32.TryParse(mfg, out x0) ? Convert.ToInt32(mfg) : 0;
            var v2 = Int32.TryParse(cat, out x0) ? Convert.ToInt32(cat) : 0;


            var mm = new MerchandiseModel(v1, v2, str);
            var oc = new OrderContext();
            var a = oc.SearchMerch(mm);

            return Json(a, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult StartOrder()
        {
            var i0 = 0;
            var dt0 = DateTime.MinValue;

            var i1 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;
            var i2 = Int32.TryParse(Request["lid"], out i0) ? Convert.ToInt32(Request["lid"]) : i0;
            var i3 = Int32.TryParse(Request["sid"], out i0) ? Convert.ToInt32(Request["sid"]) : i0;
            var i4 = Int32.TryParse(Request["otp"], out i0) ? Convert.ToInt32(Request["otp"]) : i0;

            var dt1 = DateTime.TryParse(Request["odt"], out dt0) ? Convert.ToDateTime(Request["odt"]) : DateTime.MinValue;

            var b1 = i4 == 2 ? true : false;

            var oc = new OrderContext();
            var om = new OrderModel(i1, i2, i3, b1, dt1);
            var id = oc.StartOrder(om);

            return Json(id, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EditBaseOrder()
        {
            var i0 = 0;
            var dt0 = DateTime.MinValue;

            var i1 = Int32.TryParse(Request["oid"], out i0) ? Convert.ToInt32(Request["oid"]) : i0;
            var i2 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;
            var i3 = Int32.TryParse(Request["lid"], out i0) ? Convert.ToInt32(Request["lid"]) : i0;
            var i4 = Int32.TryParse(Request["sid"], out i0) ? Convert.ToInt32(Request["sid"]) : i0;
            var i5 = Int32.TryParse(Request["otp"], out i0) ? Convert.ToInt32(Request["otp"]) : i0;

            var dt1 = DateTime.TryParse(Request["odt"], out dt0) ? Convert.ToDateTime(Request["odt"]) : DateTime.MinValue;

            var b1 = i5 == 2 ? true : false;

            var oc = new OrderContext();
            var om = new OrderModel(i1, i2, i3, i4, b1, dt1);
            oc.UpdateBaseOrder(om);

            return Json("success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult StartOrderTrans()
        {
            var i0 = 0;
 
            var i1 = Int32.TryParse(Request["oid"], out i0) ? Convert.ToInt32(Request["oid"]) : i0;
            var i2 = Int32.TryParse(Request["ttp"], out i0) ? Convert.ToInt32(Request["ttp"]) : i0;

            var oc = new OrderContext();
            var om = new OrderModel(i1, i2);
            var id = oc.RunOrderTrans(om);

            return Json(id, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult AddCartSaleItem()
        {
            var i0 = 0;
            double d0 = 0.00;

            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0; // transactionId
            var i2 = Int32.TryParse(Request["sup"], out i0) ? Convert.ToInt32(Request["sup"]) : i0; // supplier id
            var i3 = Int32.TryParse(Request["isi"], out i0) ? Convert.ToInt32(Request["isi"]) : i0; // inStockId
            var i4 = Int32.TryParse(Request["mid"], out i0) ? Convert.ToInt32(Request["mid"]) : i0; // masterId
            var i5 = Int32.TryParse(Request["unt"], out i0) ? Convert.ToInt32(Request["unt"]) : i0; // units
            var i6 = Int32.TryParse(Request["ttp"], out i0) ? Convert.ToInt32(Request["ttp"]) : i0; // trans type
            var i7 = Int32.TryParse(Request["did"], out i0) ? Convert.ToInt32(Request["did"]) : i0; // distributor id

            var d1 = Double.TryParse(Request["ask"], out d0) ? Convert.ToDouble(Request["ask"]) : d0; // asking price

            

            var oc = new OrderContext();
            var cm = new CartModel(i1, i2, i3, i4, i5, d1, i7);

            var d = oc.AddSaleToCart(cm);

            if (i6 == 101) // store menus for sales only
            {
                var s1 = Request["mnu"].Replace("^^", "^").Replace("$", ""); //supplier options
                var sup = s1.Split('^').ToList();
                var l = SetInvSuppliers(i1, d.CartId, sup);
                oc.AddSaleCartMenu(l);
            }



            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult PostOrderPayment()
        {
            var i0 = 0;
            var d0 = 0.00;
            var dt0 = DateTime.MinValue;

            var i1 = Int32.TryParse(Request["oid"], out i0) ? Convert.ToInt32(Request["oid"]) : i0;
            var i2 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;
            var i3 = Int32.TryParse(Request["clf"], out i0) ? Convert.ToInt32(Request["clf"]) : i0;
            var i4 = Int32.TryParse(Request["ptp"], out i0) ? Convert.ToInt32(Request["ptp"]) : i0;

            var v1 = Request["ath"];
            var v2 = Request["chk"];

            var d1 = Double.TryParse(Request["bbl"], out d0) ? Convert.ToDouble(Request["bbl"]) : d0;
            var d2 = Double.TryParse(Request["apd"], out d0) ? Convert.ToDouble(Request["apd"]) : d0;

            var dt1 = DateTime.TryParse(Request["pdt"], out dt0) ? Convert.ToDateTime(Request["pdt"]) : DateTime.MinValue;

            var oc = new OrderContext();
            var op = new OrderPayment(i1, i2, i3, i4, v1, v2, d1, d2, dt1);
            var d = oc.PostOrderPayment(op);

            return Json(d, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult AddFee()
        {
            var i0 = 0;
            var d0 = 0.00;
            var dt0 = DateTime.MinValue;

            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0;
            var i2 = Int32.TryParse(Request["fid"], out i0) ? Convert.ToInt32(Request["fid"]) : i0;
            var i3 = Int32.TryParse(Request["unt"], out i0) ? Convert.ToInt32(Request["unt"]) : i0;

            var d1 = Double.TryParse(Request["cst"], out d0) ? Convert.ToDouble(Request["cst"]) : d0;

            var v1 = Request["des"];

            var oc = new OrderContext();
            var i = new OrderCartItem(i1, i2, i3, d1, v1);
            var d = oc.AddSaleCartFee(i);

            return Json(d, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult NixFeeRow()
        {
            var i0 = 0;
            var d0 = 0.00;
            var dt0 = DateTime.MinValue;

            var i1 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;

            var oc = new OrderContext();
            var d = oc.DeleteCartFee(i1);

            return Json(d, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult ViewPayments()
        {
            var i0 = 0;

            var i1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0; //order id

            var oc = new OrderContext();
            var sl = oc.GetOrderPayments(i1);

            return Json(sl, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult NixPayment()
        {
            var i0 = 0;

            var i1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0; //pymt id
            var i2 = Int32.TryParse(Request["oid"], out i0) ? Convert.ToInt32(Request["oid"]) : i0; //order id

            var oc = new OrderContext();
            var sl = oc.DeletePayment(i1, i2);

            return Json(sl, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetMenuPrice(int Id)
        {
            var oc = new OrderContext();
            var sl = oc.GetFeeMenuCost(Id);

            return Json(sl, JsonRequestBehavior.AllowGet);
        }


        public List<CartModel> SetInvSuppliers(int tid, int cid, List<string> mnu)
        {
            var l = new List<CartModel>();
            var i0 = 0;
            double d0 = 0.00;

            foreach (var i in mnu)
            {
                if (i.Length == 0) { continue; }

                var x = i.Split('|').ToList();

                var sup = Int32.TryParse(x[0], out i0) ? Convert.ToInt32(x[0]) : i0; 
                var stk = Int32.TryParse(x[2], out i0) ? Convert.ToInt32(x[2]) : i0; 
                var mst = Int32.TryParse(x[3], out i0) ? Convert.ToInt32(x[3]) : i0; 
                var unt = Int32.TryParse(x[4], out i0) ? Convert.ToInt32(x[4]) : i0; 
                var cst = Double.TryParse(x[1], out d0) ? Convert.ToDouble(x[1]) : d0;

                var cm = new CartModel(tid, cid, sup, stk, mst, unt, cst);
                l.Add(cm);
            }

            return l;
        }

        [HttpPost]
        public JsonResult AddItemTransfer()
        {
            var i0 = 0;
            double d0 = 0.00;

            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0; // transactionId
            var i2 = Int32.TryParse(Request["bid"], out i0) ? Convert.ToInt32(Request["bid"]) : i0; // cost basis id

            var oc = new OrderContext();
            var d = oc.AddCartTransfer(i1, i2);

            return Json(d, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult ShowCart()
        {
            var i0 = 0;

            var i1 = Int32.TryParse(Request["Id"], out i0) ? Convert.ToInt32(Request["Id"]) : i0; //transactionId

            var oc = new OrderContext();
            var sl = oc.ViewCart(i1);

            return Json(sl, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AdjustCartQuantity()
        {
            var i0 = 0;

            var i1 = Int32.TryParse(Request["CartId"], out i0) ? Convert.ToInt32(Request["CartId"]) : i0;
            var b1 = Boolean.Parse(Request["AddItem"]) && Convert.ToBoolean(Request["AddItem"]);
 
            var oc = new OrderContext();
            var d = oc.IncrementCartCount(i1, b1);
 
            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult NixCartItem()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["CartId"], out i0) ? Convert.ToInt32(Request["CartId"]) : i0;

            var oc = new OrderContext();
            var d = oc.DeleteCartItem(i1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetTaxMenuItem()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;
            var i2 = Int32.TryParse(Request["tsi"], out i0) ? Convert.ToInt32(Request["tsi"]) : i0;

            var oc = new OrderContext();
            var o = oc.SetTaxStatus(i1, i2);

            return Json(o, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetFsdOption()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;
            var i2 = Int32.TryParse(Request["fsd"], out i0) ? Convert.ToInt32(Request["fsd"]) : i0;

            var oc = new OrderContext();
            var o = oc.SetCartFsd(i1, i2);

            return Json(o, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetLockMake()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;
            var i2 = Int32.TryParse(Request["lmk"], out i0) ? Convert.ToInt32(Request["lmk"]) : i0;

            var oc = new OrderContext();
            var o = oc.SetCartLockMake(i1, i2);

            return Json(o, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetLockModel()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;
            var i2 = Int32.TryParse(Request["lmd"], out i0) ? Convert.ToInt32(Request["lmd"]) : i0;

            var oc = new OrderContext();
            var o = oc.SetCartLockModel(i1, i2);

            return Json(o, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetCartSupplier()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;
            var i2 = Int32.TryParse(Request["sid"], out i0) ? Convert.ToInt32(Request["sid"]) : i0;

            var oc = new OrderContext();
            var o = oc.SetItemSupplier(i1, i2);

            return Json(o, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetInventoryMenu()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;
            var i2 = Int32.TryParse(Request["mid"], out i0) ? Convert.ToInt32(Request["mid"]) : i0;

            var oc = new OrderContext();
            var o = oc.SetInventoryMenuItem(i1, i2);

            return Json(o, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SetCartUnits()
        {
            var i0 = 0;
            var i1 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;
            var i2 = Int32.TryParse(Request["unt"], out i0) ? Convert.ToInt32(Request["unt"]) : i0;

            var oc = new OrderContext();
            var o = oc.SetItemUnits(i1, i2);

            return Json(o, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetCartPrice()
        {
            var i0 = 0;
            var d0 = 0.00;
            var i1 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;
            var d1 = Double.TryParse(Request["prc"], out d0) ? Convert.ToDouble(Request["prc"]) : d0;

            var oc = new OrderContext();
            var o = oc.SetItemPrice(i1, d1);

            return Json(o, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SetTaxRate()
        {
            var i0 = 0;
            var d0 = 0.00;
            var i1 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0;
            var d1 = Double.TryParse(Request["rat"], out d0) ? Convert.ToDouble(Request["rat"]) : d0;

            var oc = new OrderContext();
            var o = oc.SetItemTaxRate(i1, d1);

            return Json(o, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult AddFulfillment()
        {
            var i0 = 0;
            var dt0 = DateTime.MinValue;
 
            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0; // transactionId
            var i2 = Int32.TryParse(Request["ful"], out i0) ? Convert.ToInt32(Request["ful"]) : i0; // fulFillSrcId
            var i3 = Int32.TryParse(Request["sup"], out i0) ? Convert.ToInt32(Request["sup"]) : i0; // supplier id
            var i4 = Int32.TryParse(Request["daq"], out i0) ? Convert.ToInt32(Request["daq"]) : i0; // dest acq src
            var i5 = Int32.TryParse(Request["fcd"], out i0) ? Convert.ToInt32(Request["fcd"]) : i0; // dest fflCode
            var i6 = Int32.TryParse(Request["pup"], out i0) ? Convert.ToInt32(Request["pup"]) : i0; // pickup supplier id
            var i7 = Int32.TryParse(Request["paq"], out i0) ? Convert.ToInt32(Request["paq"]) : i0; // pickup acq src
            var i8 = Int32.TryParse(Request["pfc"], out i0) ? Convert.ToInt32(Request["pfc"]) : i0; // pickup fflCode
            var i9 = Int32.TryParse(Request["loc"], out i0) ? Convert.ToInt32(Request["loc"]) : i0; // location id (for shipping)
            var i10 = Int32.TryParse(Request["rec"], out i0) ? Convert.ToInt32(Request["rec"]) : i0; //recovery objective id

            var v1 = Request["dem"]; // dest email 
            var v2 = Request["pem"]; // pickup email 
            var v3 = Request["aty"];
            var v4 = Request["aph"];
            var v5 = Request["aem"];
            var v6 = Request["ofc"];
            var v7 = Request["oph"];
            var v8 = Request["oem"];
            var v9 = Request["not"]; // notes 



            var dt1 = DateTime.TryParse(Request["cxp"], out dt0) ? Convert.ToDateTime(Request["cxp"]) : dt0;


            var f = new FulfillModel(i1, i2, i3, i4, i5, i6, i7, i8, i10, v1, v2, v3, v4, v5, v6, v7, v8, v9);
            var oc = new OrderContext();
            oc.AddFulfillmentSale(f);

            //set for shipping & delivery only
            if (i2 == 2 || i2 == 3)
                {
                    var fx = new ShipFedEx();
                    fx.SetShippingQuote(i9, i1);      //TODO: UPDATE FEDEX SERVICES
                }

            var o = oc.SetBaseInvoiceFees(i1);

            return Json(o, JsonRequestBehavior.AllowGet);
        }


        public JsonResult AddF2F()
        {
            var i0 = 0;

            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0; //transactionId
            var i2 = Int32.TryParse(Request["fsc"], out i0) ? Convert.ToInt32(Request["fsc"]) : i0; //fulFillSrcId
            var i3 = Int32.TryParse(Request["rec"], out i0) ? Convert.ToInt32(Request["rec"]) : i0; //recoveryObjId

            var v1 = Request["aty"];
            var v2 = Request["aph"];
            var v3 = Request["aem"];
            var v4 = Request["ofc"];
            var v5 = Request["oph"];
            var v6 = Request["oem"];
            var v7 = Request["nts"];

            var f = new FulfillModel(i1, i2, i3, v1, v2, v3, v4, v5, v6, v7);

            var sc = new ShippingContext();
            sc.SetF2FTransfer(f);

            var oc = new OrderContext();
            var o = oc.SetBaseInvoiceFees(i1);

            return Json(o, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OtherSvcAddGun()
        {
            var i0 = 0;
            double d0 = 0.00;
            var dt0 = DateTime.MinValue;
            var bb = new BaseModel();
            var b0 = false;

            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0; //transactionId
            var i2 = Int32.TryParse(Request["mid"], out i0) ? Convert.ToInt32(Request["mid"]) : i0; //mfgId
            var i3 = Int32.TryParse(Request["gtp"], out i0) ? Convert.ToInt32(Request["gtp"]) : i0; //gunType
            var i4 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0; //caliberId
            var i5 = Int32.TryParse(Request["aid"], out i0) ? Convert.ToInt32(Request["aid"]) : i0; //actionId
            var i6 = Int32.TryParse(Request["fid"], out i0) ? Convert.ToInt32(Request["fid"]) : i0; //finishId
            var i7 = Int32.TryParse(Request["cap"], out i0) ? Convert.ToInt32(Request["cap"]) : i0; //capacity
            var i8 = Int32.TryParse(Request["cnd"], out i0) ? Convert.ToInt32(Request["cnd"]) : i0; //conditionId
            var i9 = Int32.TryParse(Request["lmk"], out i0) ? Convert.ToInt32(Request["lmk"]) : i0; //lock make
            var i10 = Int32.TryParse(Request["lmd"], out i0) ? Convert.ToInt32(Request["lmd"]) : i0; //lock model
            var i11 = Int32.TryParse(Request["val"], out i0) ? Convert.ToInt32(Request["val"]) : i0; //valuation id
            var i12 = Int32.TryParse(Request["mst"], out i0) ? Convert.ToInt32(Request["mst"]) : i0; //masterId
            var i13 = Int32.TryParse(Request["ttp"], out i0) ? Convert.ToInt32(Request["ttp"]) : i0; //transTypeId
            var i14 = Int32.TryParse(Request["wlb"], out i0) ? Convert.ToInt32(Request["wlb"]) : i0; //pounds

            var b1 = Boolean.TryParse(Request["box"], out b0) ? Convert.ToBoolean(Request["box"]) : b0;
            var b2 = Boolean.TryParse(Request["ppw"], out b0) ? Convert.ToBoolean(Request["ppw"]) : b0;
            var b3 = i10 > 0; //has lock

            var v1 = Request["mdl"];
            var v2 = Request["mpn"];
            var v3 = Request["upc"];
            var v4 = Request["ser"];
            var v5 = Request["not"];

            var d1 = Double.TryParse(Request["rep"], out d0) ? Convert.ToDouble(Request["rep"]) : d0;
            var d2 = Double.TryParse(Request["pts"], out d0) ? Convert.ToDouble(Request["pts"]) : d0;
            var d3 = Double.TryParse(Request["flt"], out d0) ? Convert.ToDouble(Request["flt"]) : d0;
            var d4 = Double.TryParse(Request["com"], out d0) ? Convert.ToDouble(Request["com"]) : d0;
            var d5 = Double.TryParse(Request["brl"], out d0) ? Convert.ToDouble(Request["brl"]) : d0;
            var d6 = Double.TryParse(Request["ins"], out d0) ? Convert.ToDouble(Request["ins"]) : d0; // insurance
            var d7 = Double.TryParse(Request["fee"], out d0) ? Convert.ToDouble(Request["fee"]) : d0; // rent
            var d8 = Double.TryParse(Request["ofr"], out d0) ? Convert.ToDouble(Request["ofr"]) : d0; // offer
            var d9 = Double.TryParse(Request["woz"], out d0) ? Convert.ToDouble(Request["woz"]) : d0; // ounces

            var c = new CartModel(i1, 1);
            var g = new GunModel(i12, i2, i3, i4, i5, i6, i7, i8, i9, i10, i14, b1, b2, b3, v1, v2, v3, v4, d5, d9);
            var s = new ServiceModel(g, c, i11, v5, d1, d2, d3, d4, d6, d7, d8);

            var oc = new OrderContext();
            var cid = oc.AddOtherSvcGun(s);

            if (Request.Files.Count > 0)
            {
                var imgCat = Enum.GetName(typeof(PicFolders), i13);

                var rf = Request.Files;
                var gid = Request["GroupId"];
                var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Guns);
                //var imgName = string.Format("{0}{1}-{2}.jpg", imgCat, folder, CookRandomStr(14));
                var imgName = string.Format("{0}.jpg", CookRandomStr(14));
                UpdateSvcPic(rf[0], cid, imgName, ImgSections.Guns, (PicFolders)i13);
            }

            var d = oc.ViewCart(i1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OtherSvcAddAmmo()
        {
            var i0 = 0;
            double d0 = 0.00;
            var bb = new BaseModel();
            var b0 = false;

            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0; //transactionId
            var i2 = Int32.TryParse(Request["mid"], out i0) ? Convert.ToInt32(Request["mid"]) : i0; //mfgId
            var i3 = Int32.TryParse(Request["atp"], out i0) ? Convert.ToInt32(Request["atp"]) : i0; //ammoTypeId
            var i4 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0; //caliberId
            var i5 = Int32.TryParse(Request["btp"], out i0) ? Convert.ToInt32(Request["btp"]) : i0; //bulletTypeId
            var i6 = Int32.TryParse(Request["cnd"], out i0) ? Convert.ToInt32(Request["cnd"]) : i0; //condId
            var i7 = Int32.TryParse(Request["rpb"], out i0) ? Convert.ToInt32(Request["rpb"]) : i0; //roundsPerBox
            var i8 = Int32.TryParse(Request["bwt"], out i0) ? Convert.ToInt32(Request["bwt"]) : i0; //bulletWeight
            var i9 = Int32.TryParse(Request["val"], out i0) ? Convert.ToInt32(Request["val"]) : i0; //valuation id
            var i10 = Int32.TryParse(Request["unt"], out i0) ? Convert.ToInt32(Request["unt"]) : i0; //units
            var i11 = Int32.TryParse(Request["ttp"], out i0) ? Convert.ToInt32(Request["ttp"]) : i0; // transTypeId

            var v1 = Request["mdl"];
            var v2 = Request["mpn"];
            var v3 = Request["upc"];
            var v4 = Request["not"];

            var d1 = Double.TryParse(Request["flt"], out d0) ? Convert.ToDouble(Request["flt"]) : d0;
            var d2 = Double.TryParse(Request["com"], out d0) ? Convert.ToDouble(Request["com"]) : d0;
            var d3 = Double.TryParse(Request["ofr"], out d0) ? Convert.ToDouble(Request["ofr"]) : d0;
            var d4 = Double.TryParse(Request["ins"], out d0) ? Convert.ToDouble(Request["ins"]) : d0;
            var d5 = Double.TryParse(Request["fee"], out d0) ? Convert.ToDouble(Request["fee"]) : d0;

            var c = new CartModel(i1, i10);
            var a = new AmmoModel(i2, i3, i4, i5, i6, i7, i8, v1, v2, v3);
            var s = new ServiceModel(a, c, i9, v4, d1, d2, d3, d4, d5);

            var oc = new OrderContext();
            var cid = oc.AddOtherSvcAmmo(s);

            if (Request.Files.Count > 0)
            {
                var imgCat = Enum.GetName(typeof(PicFolders), i11);

                var rf = Request.Files;
                var gid = Request["GroupId"];
                var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Ammo);
                //var imgName = string.Format("{0}{1}-{2}.jpg", imgCat, folder, CookRandomStr(8));
                var imgName = string.Format("{0}.jpg", CookRandomStr(14));
                UpdateSvcPic(rf[0], cid, imgName, ImgSections.Ammo, (PicFolders)i11);
            }

            var d = oc.ViewCart(i1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult OtherSvcAddMerch()
        {
            var i0 = 0;
            double d0 = 0.00;
            var bb = new BaseModel();
 
            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0; // transactionId
            var i2 = Int32.TryParse(Request["mid"], out i0) ? Convert.ToInt32(Request["mid"]) : i0; // mfgId
            var i3 = Int32.TryParse(Request["sct"], out i0) ? Convert.ToInt32(Request["sct"]) : i0; // subCat
            var i4 = Int32.TryParse(Request["cnd"], out i0) ? Convert.ToInt32(Request["cnd"]) : i0; // condId
            var i5 = Int32.TryParse(Request["ssz"], out i0) ? Convert.ToInt32(Request["ssz"]) : i0; // shipSizeId
            var i6 = Int32.TryParse(Request["slb"], out i0) ? Convert.ToInt32(Request["slb"]) : i0; // shipLbs
            var i7 = Int32.TryParse(Request["ipb"], out i0) ? Convert.ToInt32(Request["ipb"]) : i0; // itemsPerBox
            var i8 = Int32.TryParse(Request["unt"], out i0) ? Convert.ToInt32(Request["unt"]) : i0; // units
            var i9 = Int32.TryParse(Request["ttp"], out i0) ? Convert.ToInt32(Request["ttp"]) : i0; // transTypeId 

            var v1 = Request["not"];
            var v2 = Request["mdl"];
            var v3 = Request["des"];
            var v4 = Request["mpn"];

            var d1 = Double.TryParse(Request["soz"], out d0) ? Convert.ToDouble(Request["soz"]) : d0;
            var d2 = Double.TryParse(Request["ins"], out d0) ? Convert.ToDouble(Request["ins"]) : d0; // insValue
            var d3 = Double.TryParse(Request["fee"], out d0) ? Convert.ToDouble(Request["fee"]) : d0; // rent
            var d4 = Double.TryParse(Request["ofr"], out d0) ? Convert.ToDouble(Request["ofr"]) : d0; // offer


            var c = new CartModel(i1, i8, v1);
            var m = new MerchandiseModel(i2, i3, i4, i5, i6, i7, v2, v3, v4, d1);
            var s = new ServiceModel(m, c, d2, d3, d4);

            var oc = new OrderContext();
            var cid = oc.AddOtherSvcMerch(s);

            if (Request.Files.Count > 0)
            {

                var imgCat = Enum.GetName(typeof(PicFolders), i9);

                var rf = Request.Files;
                var gid = Request["GroupId"];
                var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Merchandise);
                //var imgName = string.Format("{0}{1}-{2}.jpg", imgCat, folder, CookRandomStr(8));
                var imgName = string.Format("{0}.jpg", CookRandomStr(14));
                UpdateSvcPic(rf[0], cid, imgName, ImgSections.Merchandise, (PicFolders)i9);
            }

            var d = oc.ViewCart(i1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CustomAddGun()
        {
            var i0 = 0;
            double d0 = 0.00;
            var dt0 = DateTime.MinValue;
            var bb = new BaseModel();
            var b0 = false;

            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0; //transactionId
            var i2 = Int32.TryParse(Request["mid"], out i0) ? Convert.ToInt32(Request["mid"]) : i0; //mfgId
            var i3 = Int32.TryParse(Request["sct"], out i0) ? Convert.ToInt32(Request["sct"]) : i0; //gunType
            var i4 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0; //caliberId
            var i5 = Int32.TryParse(Request["aid"], out i0) ? Convert.ToInt32(Request["aid"]) : i0; //actionId
            var i6 = Int32.TryParse(Request["fid"], out i0) ? Convert.ToInt32(Request["fid"]) : i0; //finishId
            var i7 = Int32.TryParse(Request["cap"], out i0) ? Convert.ToInt32(Request["cap"]) : i0; //capacity
            var i8 = Int32.TryParse(Request["cnd"], out i0) ? Convert.ToInt32(Request["cnd"]) : i0; //conditionId
            var i9 = Int32.TryParse(Request["sup"], out i0) ? Convert.ToInt32(Request["sup"]) : i0; //supplier id
            var i10 = Int32.TryParse(Request["fcd"], out i0) ? Convert.ToInt32(Request["fcd"]) : i0; //fflCode
            var i11 = Int32.TryParse(Request["acq"], out i0) ? Convert.ToInt32(Request["acq"]) : i0; //acqTypeID
            var i12 = Int32.TryParse(Request["ipb"], out i0) ? Convert.ToInt32(Request["ipb"]) : i0; //itemsPerBox
            var i13 = Int32.TryParse(Request["wlb"], out i0) ? Convert.ToInt32(Request["wlb"]) : i0; //shipPounds
            var i14 = Int32.TryParse(Request["ssz"], out i0) ? Convert.ToInt32(Request["ssz"]) : i0; //shipSizeId
            var i15 = Int32.TryParse(Request["qty"], out i0) ? Convert.ToInt32(Request["qty"]) : i0; //units

            var b1 = Boolean.TryParse(Request["box"], out b0) ? Convert.ToBoolean(Request["box"]) : b0;
            var b2 = Boolean.TryParse(Request["ppw"], out b0) ? Convert.ToBoolean(Request["ppw"]) : b0;

            var v1 = Request["eml"];
            var v2 = Request["mdl"];
            var v3 = Request["mpn"];
            var v4 = Request["upc"];

            var d1 = Double.TryParse(Request["prc"], out d0) ? Convert.ToDouble(Request["prc"]) : d0;
            var d2 = Double.TryParse(Request["cst"], out d0) ? Convert.ToDouble(Request["cst"]) : d0;
            var d3 = Double.TryParse(Request["frt"], out d0) ? Convert.ToDouble(Request["frt"]) : d0;
            var d4 = Double.TryParse(Request["fee"], out d0) ? Convert.ToDouble(Request["fee"]) : d0;
            var d5 = Double.TryParse(Request["brl"], out d0) ? Convert.ToDouble(Request["brl"]) : d0;
            var d6 = Double.TryParse(Request["woz"], out d0) ? Convert.ToDouble(Request["woz"]) : d0;

            var dt1 = DateTime.TryParse(Request["adt"], out dt0) ? Convert.ToDateTime(Request["adt"]) : DateTime.MinValue;

            var b = new BoundBookModel(v1, i9, i10, i11, dt1);
            var g = new GunModel(i5, i6, i8, i2, i3, i4, i7, i12, i13, i14, d5, d6, b1, b2, v2, v3, v4);
            var c = new CartModel(i1, i15, d1, d2, d3, d4);

            var cm = new CustomModel(c, g, b);

            var oc = new OrderContext();
            var cid = oc.AddCustomGun(cm);


            var l = new List<CartModel>(); 
            var lm = new CartModel();

            lm.TransactionId = i1;
            lm.CartId = cid;
            lm.SupplierId = -1;
            lm.InStockId = -1;
            lm.MasterId = -1;
            lm.Units = i15;
            lm.Cost = d2;
            l.Add(lm);

            oc.AddSaleCartMenu(l);

            if (Request.Files.Count > 0)
            {
                var rf = Request.Files;
                var gid = Request["GroupId"];
                var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Guns);
                var imgName = string.Format("{0}.jpg", CookRandomStr(14));
                UpdateSvcPic(rf[0], cid, imgName, ImgSections.Guns, PicFolders.Custom);
            }

            var d = oc.ViewCart(i1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CustomAddAmmo()
        {
            var i0 = 0;
            double d0 = 0.00;
            var dt0 = DateTime.MinValue;
            var bb = new BaseModel();
            var b0 = false;

            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0; // transactionId
            var i2 = Int32.TryParse(Request["atp"], out i0) ? Convert.ToInt32(Request["atp"]) : i0; // ammoType
            var i3 = Int32.TryParse(Request["mid"], out i0) ? Convert.ToInt32(Request["mid"]) : i0; // mfgId
            var i4 = Int32.TryParse(Request["cid"], out i0) ? Convert.ToInt32(Request["cid"]) : i0; // caliberId
            var i5 = Int32.TryParse(Request["bid"], out i0) ? Convert.ToInt32(Request["bid"]) : i0; // bulletTypeId
            var i6 = Int32.TryParse(Request["gwt"], out i0) ? Convert.ToInt32(Request["gwt"]) : i0; // bulletWgt
            var i7 = Int32.TryParse(Request["rpb"], out i0) ? Convert.ToInt32(Request["rpb"]) : i0; // roundPerBox

            var i9 = Int32.TryParse(Request["cnd"], out i0) ? Convert.ToInt32(Request["cnd"]) : i0;  // conditionId
            var i10 = Int32.TryParse(Request["sup"], out i0) ? Convert.ToInt32(Request["sup"]) : i0; // sup id
            var i11 = Int32.TryParse(Request["fcd"], out i0) ? Convert.ToInt32(Request["fcd"]) : i0; // fflCode
            var i12 = Int32.TryParse(Request["acq"], out i0) ? Convert.ToInt32(Request["acq"]) : i0; // acqTypID
            var i13 = Int32.TryParse(Request["qty"], out i0) ? Convert.ToInt32(Request["qty"]) : i0; // units

            var b1 = Boolean.TryParse(Request["slg"], out b0) ? Convert.ToBoolean(Request["slg"]) : b0; // units

            var v1 = Request["eml"];
            var v2 = Request["mpn"];
            var v3 = Request["upc"];
            var v4 = Request["mdl"];

            var d1 = Double.TryParse(Request["prc"], out d0) ? Convert.ToDouble(Request["prc"]) : d0;
            var d2 = Double.TryParse(Request["cst"], out d0) ? Convert.ToDouble(Request["cst"]) : d0;
            var d3 = Double.TryParse(Request["frt"], out d0) ? Convert.ToDouble(Request["frt"]) : d0;
            var d4 = Double.TryParse(Request["fee"], out d0) ? Convert.ToDouble(Request["fee"]) : d0;
            var d5 = Double.TryParse(Request["chb"], out d0) ? Convert.ToDouble(Request["chb"]) : d0;
            var d6 = Double.TryParse(Request["ssz"], out d0) ? Convert.ToDouble(Request["ssz"]) : d0;
             
            var dt1 = DateTime.TryParse(Request["adt"], out dt0) ? Convert.ToDateTime(Request["adt"]) : DateTime.MinValue;

            var b = new BoundBookModel(v1, i10, i11, i12, dt1);
            var a = new AmmoModel(i2, i3, i4, i5, i6, i7, i9, v2, v4, v3, b1, d5, d6);
            var c = new CartModel(i1, i13, d1, d2, d3, d4);
            var cm = new CustomModel(c, a, b);  

            var oc = new OrderContext();
            var cid = oc.AddCustomAmmo(cm);

            if (Request.Files.Count > 0)
            {
                var rf = Request.Files;
                var gid = Request["GroupId"];
                var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Ammo);
                //var imgName = string.Format("Custom{0}-{1}.jpg", folder, CookRandomStr(6));
                var imgName = string.Format("{0}.jpg", CookRandomStr(14));
                UpdateSvcPic(rf[0], cid, imgName, ImgSections.Ammo, PicFolders.Custom);
            }

            var d = oc.ViewCart(i1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CustomAddMerch()
        {
            var i0 = 0;
            double d0 = 0.00;
            var dt0 = DateTime.MinValue;
            var bb = new BaseModel();

            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0; // transactionId
            var i3 = Int32.TryParse(Request["mid"], out i0) ? Convert.ToInt32(Request["mid"]) : i0; // mfgId
            var i4 = Int32.TryParse(Request["sct"], out i0) ? Convert.ToInt32(Request["sct"]) : i0; // subCategory
            var i5 = Int32.TryParse(Request["ssz"], out i0) ? Convert.ToInt32(Request["ssz"]) : i0; // shipSizeId
            var i6 = Int32.TryParse(Request["ipb"], out i0) ? Convert.ToInt32(Request["ipb"]) : i0; // itemsPerBox
            var i7 = Int32.TryParse(Request["wlb"], out i0) ? Convert.ToInt32(Request["wlb"]) : i0; // shipPounds
            var i8 = Int32.TryParse(Request["qty"], out i0) ? Convert.ToInt32(Request["qty"]) : i0; // units
            var i9 = Int32.TryParse(Request["cnd"], out i0) ? Convert.ToInt32(Request["cnd"]) : i0; // conditionId
            var i10 = Int32.TryParse(Request["sup"], out i0) ? Convert.ToInt32(Request["sup"]) : i0; // sup id
            var i11 = Int32.TryParse(Request["fcd"], out i0) ? Convert.ToInt32(Request["fcd"]) : i0; // fflCode
            var i12 = Int32.TryParse(Request["acq"], out i0) ? Convert.ToInt32(Request["acq"]) : i0; // acqTypID

            var d1 = Double.TryParse(Request["prc"], out d0) ? Convert.ToDouble(Request["prc"]) : d0;
            var d2 = Double.TryParse(Request["cst"], out d0) ? Convert.ToDouble(Request["cst"]) : d0;
            var d3 = Double.TryParse(Request["frt"], out d0) ? Convert.ToDouble(Request["frt"]) : d0;
            var d4 = Double.TryParse(Request["fee"], out d0) ? Convert.ToDouble(Request["fee"]) : d0;
            var d5 = Double.TryParse(Request["woz"], out d0) ? Convert.ToDouble(Request["woz"]) : d0;

            var v1 = Request["eml"];
            var v2 = Request["mdl"];
            var v3 = Request["mpn"];
            var v4 = Request["upc"];
            var v5 = Request["dsc"];

            var dt1 = DateTime.TryParse(Request["adt"], out dt0) ? Convert.ToDateTime(Request["adt"]) : DateTime.MinValue;
             
            var b = new BoundBookModel(v1, i10, i11, i12, dt1);
            var m = new MerchandiseModel(i3, i4, i9, i5, i6, i7, d5, v2, v3, v4, v5);
            var c = new CartModel(i1, i8, d1, d2, d3, d4);

            var cm = new CustomModel(c, m, b);

            var oc = new OrderContext();
            var cid = oc.AddCustomItem(cm);

            if (Request.Files.Count > 0)
            {
                var rf = Request.Files;
                var gid = Request["GroupId"];
                var folder = Enum.GetName(typeof(ImgSections), (int)ImgSections.Merchandise);
                var imgName = string.Format("{0}.jpg", CookRandomStr(14));
                UpdateSvcPic(rf[0], cid, imgName, ImgSections.Merchandise, PicFolders.Custom);
            }

            var d = oc.ViewCart(i1);

            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GenericAddItem()
        {
            var i0 = 0;
            double d0 = 0.00;
            var b0 = false;
            var bb = new BaseModel();

            var i1 = Int32.TryParse(Request["tid"], out i0) ? Convert.ToInt32(Request["tid"]) : i0; // transactionId
            var b1 = Boolean.TryParse(Request["tax"], out b0) ? Convert.ToBoolean(Request["tax"]) : b0;
            var d1 = Double.TryParse(Request["prc"], out d0) ? Convert.ToDouble(Request["prc"]) : d0;
            var v1 = Request["des"];

            var c = new CartModel(i1, b1, d1, v1);

            var oc = new OrderContext();
            oc.AddGenericItem(c);

            return Json("success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetSalesReps(string locId)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(locId, out x0) ? Convert.ToInt32(locId) : 0;

            var mm = new MenuModel();
            var lm = mm.GetSalesRepsByLocation(v1);

            return Json(lm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTransferItems(string cat, string cid, string ppt)
        {
            var x0 = 0;
            var b0 = false;

            var i1 = Int32.TryParse(cat, out x0) ? Convert.ToInt32(cat) : 0;
            var i2 = Int32.TryParse(cid, out x0) ? Convert.ToInt32(cid) : 0;
            var b1 = Boolean.TryParse(ppt, out b0) ? Convert.ToBoolean(ppt) : b0;

            var oc = new OrderContext();
            var lm = oc.GetCustFflTransfers(i1, i2, b1);

            return Json(lm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPptItems(string cat, string cid, string sup)
        {
            var x0 = 0;
            var i1 = Int32.TryParse(cat, out x0) ? Convert.ToInt32(cat) : 0;
            var i2 = Int32.TryParse(cid, out x0) ? Convert.ToInt32(cid) : 0;
            var i3 = Int32.TryParse(sup, out x0) ? Convert.ToInt32(sup) : 0;

            var oc = new OrderContext();
            var lm = oc.GetCustPptItems(i1, i2, i3);

            return Json(lm, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetCaPptSellers(string cat, string cid)
        {
            var x0 = 0;
            var i1 = Int32.TryParse(cat, out x0) ? Convert.ToInt32(cat) : 0;
            var i2 = Int32.TryParse(cid, out x0) ? Convert.ToInt32(cid) : 0;

            var oc = new OrderContext();
            var lm = oc.GetPptSuppliers(i1, i2);

            return Json(lm, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetFfl(string Id)
        {
            var x0 = 0;
            var v1 = Int32.TryParse(Id, out x0) ? Convert.ToInt32(Id) : 0;

            var bc = new BookContext();
            var wd = bc.GetFflByCode(v1);

            return Json(wd, JsonRequestBehavior.AllowGet);
        }


    }
}