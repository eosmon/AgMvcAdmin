using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using AgMvcAdmin.FedEx;
using AgMvcAdmin.Common;
using WebBase.Configuration;
using System.Web.Services.Protocols;

namespace AgMvcAdmin.Models.Common
{
    public class ShipFedEx
    {

        public Shipment SetFxAuth(int lid)
        {
            var b0 = false;
            var ip = ConfigurationHelper.GetFeatureFlagValue("application", "FedExProd").ToString();
            var isProd = Boolean.TryParse(ip, out b0) ? Convert.ToBoolean(ip) : b0;

            var eFxKey = string.Empty;
            var eFxMeter = string.Empty;
            var eFxPswd = string.Empty;
            var eFxAccount = string.Empty;

            var oStreet = string.Empty;
            var oCity = string.Empty;
            var oState = string.Empty;
            var oZip = string.Empty;
            var oCountry = string.Empty;

            if (isProd)
            {
                eFxKey = ConfigurationHelper.GetPropertyValue("application", "FxKeyProd");
                eFxMeter = ConfigurationHelper.GetPropertyValue("application", "FxMeterProd");
                eFxPswd = ConfigurationHelper.GetPropertyValue("application", "FxPswdProd");
                eFxAccount = ConfigurationHelper.GetPropertyValue("application", "FxAcctProd");
            }
            else
            {
                eFxKey = ConfigurationHelper.GetPropertyValue("application", "FxKeyTest");
                eFxMeter = ConfigurationHelper.GetPropertyValue("application", "FxMeterTest");
                eFxPswd = ConfigurationHelper.GetPropertyValue("application", "FxPswdTest");
                eFxAccount = ConfigurationHelper.GetPropertyValue("application", "FxAcctTest");
            }

            if (lid == 1)
            {
                oStreet = ConfigurationHelper.GetPropertyValue("application", "CaStreet");
                oCity = ConfigurationHelper.GetPropertyValue("application", "CaCity");
                oState = ConfigurationHelper.GetPropertyValue("application", "CaState");
                oZip = ConfigurationHelper.GetPropertyValue("application", "CaZip");
            }
            else
            {
                oStreet = ConfigurationHelper.GetPropertyValue("application", "WyStreet");
                oCity = ConfigurationHelper.GetPropertyValue("application", "WyCity");
                oState = ConfigurationHelper.GetPropertyValue("application", "WyState");
                oZip = ConfigurationHelper.GetPropertyValue("application", "WyZip");
            }

            oCountry = ConfigurationHelper.GetPropertyValue("application", "Country");


            var s = new Shipment();
            s.FxKey = eFxKey;
            s.FxMeter = eFxMeter;
            s.FxPswd = eFxPswd;
            s.FxAccount = eFxAccount;
            s.ShipStreet = oStreet;
            s.ShipCity = oCity;
            s.ShipState = oState;
            s.ShipZipCode = oZip;
            s.ShipCountry = oCountry;
            s.DestCountry = oCountry; // DEFAULT USA. NO EXPORTING

            return s;

        }


        public Shipment QuoteShipping(Shipment s)
        {
            var l = new List<ShippingQuote>();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = CreateRequest(s);
            RateService service = new RateService();

            try
            {
                RateReply reply = service.getRates(request);

                switch (reply.HighestSeverity)
                {
                    case NotificationSeverityType.SUCCESS:
                    case NotificationSeverityType.NOTE:
                    case NotificationSeverityType.WARNING:
                        s = ShowRateReply(s, reply);
                        break;
                    case NotificationSeverityType.ERROR:
                    case NotificationSeverityType.FAILURE:
                        s = SetError(s, reply);
                        s.IsError = true;
                        break;
                }
            }
            catch (SoapException e)
            {
                Console.WriteLine(e.Detail.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return s;
        }

        private static RateRequest CreateRequest(Shipment s)
        {
            RateRequest r = new RateRequest();
            var rad = new WebAuthenticationDetail();
            rad.UserCredential = new WebAuthenticationCredential();
            rad.UserCredential.Key = s.FxKey;
            rad.UserCredential.Password = s.FxPswd;
            r.WebAuthenticationDetail = rad;
            r.ClientDetail = new ClientDetail { AccountNumber = s.FxAccount, MeterNumber = s.FxMeter };
            r.TransactionDetail = new TransactionDetail();
            r.TransactionDetail.CustomerTransactionId = s.TransId;
            r.Version = new VersionId();
            r.ReturnTransitAndCommit = true;
            r.ReturnTransitAndCommitSpecified = true;

            r = SetDetails(r, s);
            return r;
        }

        private static RateRequest SetDetails(RateRequest r, Shipment s)
        {
            var bs = FedExPkgTypes.Unknown;

            switch (s.QuoteType)
            {
                default:
                    bs = FedExPkgTypes.YOUR_PACKAGING;
                    break;
                case ShipQuoteTypes.OneRate:
                    bs = FedExPkgTypes.FEDEX_LARGE_BOX;
                    break;
            }

            var rs = new RequestedShipment();
 

            rs.Shipper = new Party();
            rs.Shipper.Address = new Address();
            rs.Shipper.Address.StreetLines = new string[1] { s.ShipStreet };
            rs.Shipper.Address.City = s.ShipCity;
            rs.Shipper.Address.StateOrProvinceCode = s.ShipState;
            rs.Shipper.Address.PostalCode = s.ShipZipCode;
            rs.Shipper.Address.CountryCode = s.ShipCountry;

            rs.Recipient = new Party();
            rs.Recipient.Address = new Address();
            rs.Recipient.Address.StateOrProvinceCode = s.DestState;
            rs.Recipient.Address.PostalCode = s.DestZipCode;
            rs.Recipient.Address.CountryCode = s.DestCountry;

            if (s.QuoteType == ShipQuoteTypes.OneRate)
            {
                rs.SpecialServicesRequested = new ShipmentSpecialServicesRequested();
                rs.SpecialServicesRequested.SpecialServiceTypes = new string[1];
                rs.SpecialServicesRequested.SpecialServiceTypes[0] = "FEDEX_ONE_RATE";
            }

            var ct = 0;
            var pkgCt = 0;
            var nl = new List<ShippingItem>();

            switch (s.QuoteType)
            {
                case ShipQuoteTypes.Items:
                    nl = s.Items;
                    pkgCt = s.ItemBoxCount;
                    break;
                case ShipQuoteTypes.Ammo:
                    nl = s.ClsAmmo.Ammo;
                    pkgCt = s.ClsAmmo.GroupBoxCount;
                    break;
                case ShipQuoteTypes.OneRate:
                    nl = s.ClsOneRate.OneRate;
                    pkgCt = s.ClsOneRate.GroupBoxCount;
                    break;
            }

            ct = nl.Count;
            rs.RequestedPackageLineItems = new RequestedPackageLineItem[ct];
            var rli = new RequestedPackageLineItem[ct];
            var x = 0;

            foreach (var item in nl)
            {
                rli[x] = new RequestedPackageLineItem();
                rli[x].SequenceNumber = item.ItemNumber.ToString();  //Box sequence number
                rli[x].GroupPackageCount = item.BoxCount.ToString(); //AMMO 1 case per tracking number

                rli[x].Weight = new Weight(); // package weight
                rli[x].Weight.Units = WeightUnits.LB;
                rli[x].Weight.UnitsSpecified = true;
                rli[x].Weight.Value = item.Weight;
                rli[x].Weight.ValueSpecified = true;

                // package dimensions
                rli[x].Dimensions = new Dimensions();
                rli[x].Dimensions.Length = item.Length;
                rli[x].Dimensions.Width = item.Width;
                rli[x].Dimensions.Height = item.Height;
                rli[x].Dimensions.Units = LinearUnits.IN;
                rli[x].Dimensions.UnitsSpecified = true;

                rli[x].SpecialServicesRequested = new PackageSpecialServicesRequested();
                rli[x].SpecialServicesRequested.SignatureOptionDetail = new SignatureOptionDetail();
                rli[x].SpecialServicesRequested.SignatureOptionDetail.OptionType = item.SignatureType;
                rli[x].SpecialServicesRequested.SignatureOptionDetail.OptionTypeSpecified = true;

                rli[x].InsuredValue = new Money();
                rli[x].InsuredValue.AmountSpecified = item.IsInsured;
                rli[x].InsuredValue.Amount = Math.Round(item.InsuredAmount);
                rli[x].InsuredValue.Currency = "USD";

                if (s.QuoteType == ShipQuoteTypes.Ammo)
                {
                    rli[x].SpecialServicesRequested.DangerousGoodsDetail = new DangerousGoodsDetail();
                    rli[x].SpecialServicesRequested.DangerousGoodsDetail.Regulation = HazardousCommodityRegulationType.ORMD;
                    rli[x].SpecialServicesRequested.DangerousGoodsDetail.RegulationSpecified = true;
                }

                rs.RequestedPackageLineItems[x] = rli[x];
                x++;
            }

            rs.DropoffTypeSpecified = true;
            rs.DropoffType = DropoffType.BUSINESS_SERVICE_CENTER;
            rs.PackagingType = Enum.GetName(typeof(FedExPkgTypes), bs);
            if (s.QuoteType == ShipQuoteTypes.Ammo) { rs.ServiceType = "FEDEX_GROUND"; }
            rs.PackageCount = pkgCt.ToString();


            r.RequestedShipment = rs;

            return r;
        }

        private static Shipment SetError(Shipment s, RateReply reply)
        {
            if (reply.Notifications.Length == 0) { return s; }
            var e = reply.Notifications[0];
            var msg = string.Format("{0}: {1}", e.Severity, e.LocalizedMessage);

            var err = e.LocalizedMessage;
            var q = new List<ShippingQuote>();
            var qa = s.QuoteType == ShipQuoteTypes.Ammo ? true : false;
            var qo = s.QuoteType == ShipQuoteTypes.OneRate ? true : false;

            var sq = new ShippingQuote(FreightCarriers.FedEx, s.TransactionId, true, qa, qo, err);
            q.Add(sq);

            switch (s.QuoteType)
            {
                case ShipQuoteTypes.Items:
                    s.Quote = q;
                    break;
                case ShipQuoteTypes.Ammo:
                    s.ClsAmmo.QuoteAmmo = q;
                    break;
                case ShipQuoteTypes.OneRate:
                    s.ClsOneRate.QuoteOneRate = q;
                    break;

            }

            //
            //var sq = new ShippingQuote(-3, "** FEDEX QUOTING UNAVAILABLE - SHIPPING T.B.D. **");
            //ql.Add(sq);
            //s.Quote = ql;
            return s;
        }

        private static Shipment ShowRateReply(Shipment s, RateReply reply)
        {
            var ql = new List<ShippingQuote>();

            Console.WriteLine("RateReply details:");
            foreach (RateReplyDetail rateReplyDetail in reply.RateReplyDetails)
            {
                var sq = new ShippingQuote();

                var st = rateReplyDetail.ServiceType;
                var pt = rateReplyDetail.PackagingType;

                if (st == "FIRST_OVERNIGHT" || st == "PRIORITY_OVERNIGHT" || st == "FEDEX_2_DAY_AM") { continue; }
                 
                switch (st)
                {
                    case "STANDARD_OVERNIGHT":
                        sq.ShipText = "OVERNIGHT";
                        sq.ShipOption = ShipTimes.Overnight;
                        break;
                    case "FEDEX_2_DAY":
                        sq.ShipText = "2ND DAY AIR";
                        sq.ShipOption = ShipTimes.TwoDayAir;
                        break;
                    case "FEDEX_EXPRESS_SAVER":
                        sq.ShipText = "2-3 DAY PRIORITY";
                        sq.ShipOption = ShipTimes.ExpressSaver;
                        break;
                    case "FEDEX_GROUND":
                        sq.ShipText = "GROUND";
                        sq.ShipOption = ShipTimes.Ground;
                        break;
                }

                double rate = 0.00;
                double disc = 0.00;
                double ins = 0.00;
                double sig = 0.00;
                double del = 0.00;
                double ful = 0.00;

                foreach (RatedShipmentDetail shipmentDetail in rateReplyDetail.RatedShipmentDetails)
                {
                    if (shipmentDetail == null) return s;
                    if (shipmentDetail.ShipmentRateDetail == null) return s;
                    ShipmentRateDetail rateDetail = shipmentDetail.ShipmentRateDetail;

                    if (rateDetail.FreightDiscounts != null) //No discount on ONE RATE
                    {
                        foreach (RateDiscount d in rateDetail.FreightDiscounts)
                        {
                            var rd = d.Amount.Amount;
                            if (rd > 0) { disc += (double)rd; }
                        }
                    }

                    foreach (Surcharge sc in rateDetail.Surcharges)
                    {
                        var scTp = sc.SurchargeType;
                        var scAm = sc.Amount.Amount; // 2 digit decimal

                        // BASE CHARGES
                        if (scTp == SurchargeType.DELIVERY_AREA) { del += (double)sc.Amount.Amount; }
                        if (scTp == SurchargeType.FUEL) { ful += (double)sc.Amount.Amount; }

                        if (scTp == SurchargeType.INSURED_VALUE)  { ins += (double)sc.Amount.Amount; }
                        if (scTp == SurchargeType.SIGNATURE_OPTION) { sig += (double)sc.Amount.Amount; }


                    }

                    rate += (double)rateDetail.TotalBaseCharge.Amount + del + ful;
                }

                var sf = new ShippingFees(rate, ins, sig, disc);


                switch (sq.ShipOption)
                {
                    case ShipTimes.Overnight:
                        sq.FeesOvernight = sf;
                        break;
                    case ShipTimes.TwoDayAir:
                        sq.Fees2ndDay = sf;
                        break;
                    case ShipTimes.ExpressSaver:
                        sq.FeesExpressSaver = sf;
                        break;
                    case ShipTimes.Ground:
                        sq.FeesGround = sf;
                        break;
                }


                if (rateReplyDetail.DeliveryTimestampSpecified) { var dt = rateReplyDetail.DeliveryTimestamp.ToShortDateString(); }

                var tt = 0;

                if (rateReplyDetail.TransitTimeSpecified)
                {
                    var t = Enum.GetName(typeof(TransitTimeType), rateReplyDetail.TransitTime);
                    tt = GetDaysFromTransit(t);
                }
                else
                {
                    switch (sq.ShipOption)
                    {
                        case ShipTimes.Overnight:
                            tt = 1;
                            break;
                        case ShipTimes.TwoDayAir:
                            tt = 2;
                            break;
                        case ShipTimes.ExpressSaver:
                            tt = 3;
                            break;
                        default:
                            tt = 4;
                            break;
                    }

                }

                sq.TransitDays = tt;
                sq.SelectValue = (int)sq.ShipOption;

                ql.Add(sq);
            }


            var nt = string.Empty;

            foreach (var note in reply.Notifications) { nt = Enum.GetName(typeof(NotificationSeverityType), note.Severity); }

            switch (s.QuoteType)
            {
                case ShipQuoteTypes.Items:
                    s.Quote = ql;
                    s.QuoteResult = nt;
                    break;
                case ShipQuoteTypes.Ammo:
                    s.ClsAmmo.QuoteAmmo = ql;
                    s.ClsAmmo.QuoteResult = nt;
                    break;
                case ShipQuoteTypes.OneRate:
                    s.ClsOneRate.QuoteOneRate = ql;
                    s.ClsOneRate.QuoteResult = nt;
                    break;
            }

            return s;
        }

        //public ShippingCollection GetShippingList(int lid, int tid, string zipCode)
        //{
        //    //var spc = EstimateShipping(lid, tid);

        //    //var sc = new ShippingContext();
        //    //sc.SetInsuranceCost(tid, spc.InsureCost);

        //    //List<ShippingQuote> ol = spc.QuoteMenu.OrderBy(o => o.SelectValue).ToList();
        //    //spc.QuoteMenu = ol;
        //    //return spc;
        //}


        public void SetShippingQuote(int lid, int tid)
        {
            WriteShippingQuotes(lid, tid);

            //var sc = new ShippingContext();
            //sc.SetInsuranceCost(tid, spc.InsureCost);

        }


        public void WriteShippingQuotes(int lid, int tid)
        {
            var spc = new ShippingCollection();

            var l = new List<ShippingQuote>();
            var s = SetFxAuth(lid);
            var ams = string.Empty;

            var sc = new ShippingContext();
            s = sc.GetShippingBase(tid, s);

            var n1 = new List<ShippingQuote>();
            var n2 = new List<ShippingQuote>();
            var n3 = new List<ShippingQuote>();

            if (s.HasItems)
            {
                s.QuoteType = ShipQuoteTypes.Items;
                s = QuoteShipping(s);
                if (s.IsError)
                {
                    //l = s.Quote;
                    //spc.QuoteMenu = l;
                    //return spc;
                }
                n1 = SetShippingOptions(s);
            }

            if (s.HasAmmo)
            {
                s.QuoteType = ShipQuoteTypes.Ammo;
                s = QuoteShipping(s);
                if (s.IsError)
                {
                    //l = s.Quote;
                    //spc.QuoteMenu = l;
                    //return spc;
                }
                n2 = SetShippingOptions(s);
            }

            if (s.HasOneRate)
            {
                s.QuoteType = ShipQuoteTypes.OneRate;
                s = QuoteShipping(s);
                if (s.IsError)
                {
                    //l = s.Quote;
                    //spc.QuoteMenu = l;
                    //return spc;
                }
                n3 = SetShippingOptions(s);
            }

            var allRows = n1.Concat(n2).Concat(n3).ToList();
            WriteShippingRates(s.TransactionId, allRows);

        }


        public List<ShippingQuote> SetShippingOptions(Shipment s)
        {
            var sq = new List<ShippingQuote>(); //list holding list params
            var amo = false;
            var one = false;
            var tid = s.TransactionId;

            switch (s.QuoteType)
            {
                case ShipQuoteTypes.Items:
                    sq = s.Quote;
                    amo = false;
                    one = false;
                    break;
                case ShipQuoteTypes.Ammo:
                    sq = s.ClsAmmo.QuoteAmmo;
                    amo = true;
                    one = false;
                    break;
                case ShipQuoteTypes.OneRate:
                    sq = s.ClsOneRate.QuoteOneRate;
                    amo = false;
                    one = true;
                    break;
            }

            var nl = new List<ShippingQuote>(); //list to add new quote

            foreach (var item in   sq)
            {

                double bas = 0.00;
                double ins = 0.00;
                double sig = 0.00;
                double dis = 0.00;

                switch (item.ShipOption)
                {
                    case ShipTimes.Overnight:
                        bas = item.FeesOvernight.BaseAmount; 
                        ins = item.FeesOvernight.Insurance;
                        sig = item.FeesOvernight.Signature;
                        dis = item.FeesOvernight.RateDiscount;
                        break;
                    case ShipTimes.TwoDayAir:
                        bas = item.Fees2ndDay.BaseAmount;
                        ins = item.Fees2ndDay.Insurance;
                        sig = item.Fees2ndDay.Signature;
                        dis = item.Fees2ndDay.RateDiscount;
                        break;
                    case ShipTimes.ExpressSaver:
                        bas = item.FeesExpressSaver.BaseAmount;
                        ins = item.FeesExpressSaver.Insurance;
                        sig = item.FeesExpressSaver.Signature;
                        dis = item.FeesExpressSaver.RateDiscount;
                        break;
                    case ShipTimes.Ground:
                        bas = item.FeesGround.BaseAmount;
                        ins = item.FeesGround.Insurance;
                        sig = item.FeesGround.Signature;
                        dis = item.FeesGround.RateDiscount;
                        break;
                }

                var txt = string.Empty;
                var opt = 0;
                var day = 0;
                var err = false;


                opt = item.SelectValue;
                day = item.TransitDays;
                txt = item.ShipText;
                err = item.IsError;


                var q = new ShippingQuote(FreightCarriers.FedEx, tid, opt, day, bas, ins, sig, dis, amo, one, err, txt);
                nl.Add(q);

            }

            return nl;

        }


        public void WriteShippingRates(int tid, List<ShippingQuote> items)
        {
            var sc = new ShippingContext();

            sc.ClearShippingRates(tid);

            foreach (var a in items)
            {
                sc.AddShippingRates(a);
            }
        }

        public static int GetDaysFromTransit(string t)
        {
            var x = 0;

            switch (t) {
                case "TWENTY_DAYS":
                    x = 20;
                    break;
                case "NINETEEN_DAYS":
                    x = 19;
                    break;
                case "EIGHTEEN_DAYS":
                    x = 18;
                    break;
                case "SEVENTEEN_DAYS":
                    x = 17;
                    break;
                case "SIXTEEN_DAYS":
                    x = 16;
                    break;
                case "FIFTEEN_DAYS":
                    x = 15;
                    break;
                case "FOURTEEN_DAYS":
                    x = 14;
                    break;
                case "THIRTEEN_DAYS":
                    x = 13;
                    break;
                case "TWELVE_DAYS":
                    x = 12;
                    break;
                case "ELEVEN_DAYS":
                    x = 11;
                    break;
                case "TEN_DAYS":
                    x = 10;
                    break;
                case "NINE_DAYS":
                    x = 9;
                    break;
                case "EIGHT_DAYS":
                    x = 8;
                    break;
                case "SEVEN_DAYS":
                    x = 7;
                    break;
                case "SIX_DAYS":
                    x = 6;
                    break;
                case "FIVE_DAYS":
                    x = 5;
                    break;
                case "FOUR_DAYS":
                    x = 4;
                    break;
                case "THREE_DAYS":
                    x = 3;
                    break;
                case "TWO_DAYS":
                    x = 2;
                    break;
                case "ONE_DAY":
                    x = 1;
                    break;
                case "UNKNOWN":
                    x = 0;
                    break;

            }

            return x;
        }
    }
}