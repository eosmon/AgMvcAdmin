using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgMvcAdmin.Models.Common;

namespace AgMvcAdmin.Models
{
    public class ServiceModel
    {
        public MerchandiseModel Merch { get; set; }
        public AmmoModel Ammo { get; set; }
        public GunModel Gun { get; set; }
        public CartModel Cart { get; set; }
        public int ValuationId { get; set; }
        public string Notes { get; set; }
        public double CommRate { get; set; }
        public double FlatAmount { get; set; }
        public double PartsCost { get; set; }
        public double LaborCost { get; set; }
        public double OfferPrice { get; set; }
        public double Insurance { get; set; }
        public double MonthlyRent { get; set; }

        public ServiceModel() { }

        public ServiceModel(GunModel gun, CartModel crt, int val, string not, double lbr, double pts, double flt, double com, double ins, double fee, double ofr)
        {
            Gun = gun;
            Cart = crt;
            ValuationId = val;
            Notes = not;
            LaborCost = lbr;
            PartsCost = pts;
            FlatAmount = flt;
            CommRate = com;
            Insurance = ins;
            MonthlyRent = fee;
            OfferPrice = ofr;
        }

        public ServiceModel(GunModel gun, CartModel crt, int val, string not, double lbr, double pts, double flt, double com) {
            Gun = gun;
            Cart = crt;
            ValuationId = val;
            Notes = not;
            LaborCost = lbr;
            PartsCost = pts;
            FlatAmount = flt;
            CommRate = com;
        }

        public ServiceModel(AmmoModel amo, CartModel crt, int val, string not, double flt, double com)
        {
            Ammo = amo;
            Cart = crt;
            ValuationId = val;
            Notes = not;
            FlatAmount = flt;
            CommRate = com;
        }

        public ServiceModel(AmmoModel amo, CartModel crt, int val, string not, double flt, double com, double ofr, double ins, double rnt)
        {
            Ammo = amo;
            Cart = crt;
            ValuationId = val;
            Notes = not;
            FlatAmount = flt;
            CommRate = com;
            OfferPrice = ofr;
            Insurance = ins;
            MonthlyRent = rnt;
        }

        public ServiceModel(MerchandiseModel mdm, CartModel crt, int val, string not, double flt, double com)
        {
            Merch = mdm;
            Cart = crt;
            ValuationId = val;
            Notes = not;
            FlatAmount = flt;
            CommRate = com;
        }

        // ACQUISITION GUNS
        public ServiceModel(GunModel gun, CartModel crt, string not, double ofr)
        {
            Gun = gun;
            Cart = crt;
            Notes = not;
            OfferPrice = ofr;
        }

        // ACQUISITION AMMO
        public ServiceModel(AmmoModel amo, CartModel crt, string not, double ofr)
        {
            Ammo = amo;
            Cart = crt;
            Notes = not;
            OfferPrice = ofr;
        }

        // ACQUISITION MERCH
        public ServiceModel(MerchandiseModel mdm, CartModel crt, string not, double ofr)
        {
            Merch = mdm;
            Cart = crt;
            Notes = not;
            OfferPrice = ofr;
        }

        // ACQUISITION MERCH
        public ServiceModel(MerchandiseModel mdm, CartModel crt, double ins, double rnt, double ofr)
        {
            Merch = mdm;
            Cart = crt;
            Insurance = ins;
            MonthlyRent = rnt;
            OfferPrice = ofr;

        }
    }
}