using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgMvcAdmin.FedEx;
using AgMvcAdmin.Common;

namespace AgMvcAdmin.Models.Common
{
    public class ShippingItem
    {
        public int ItemNumber { get; set; }
        public int BoxCount { get; set; }
        public int Units { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string InsuranceFee { get; set; }
        public string TotalFee { get; set; }
        public bool IsSignatureRequired { get; set; }
        public bool IsInsured { get; set; }
        public bool IsOrdnance { get; set; }
        public bool IsGround { get; set; }
        public bool IsOneRate { get; set; }
        public decimal Weight { get; set; }
        public decimal InsuredAmount { get; set; }
        public SignatureOptionType SignatureType { get; set; }
        public FedExPkgTypes BoxSize { get; set; }



        public ShippingItem() { }

        // QUOTE BOTH
        public ShippingItem(int itemId, int boxCt, int units, SignatureOptionType sigTyp, FedExPkgTypes box, decimal wgt, decimal insAmt, string len, string width, string height, bool oneRate, bool insured, bool ammo)
        {
            ItemNumber = itemId;
            BoxCount = boxCt;
            Units = units;
            SignatureType = sigTyp;
            BoxSize = box;
            Weight = wgt;
            InsuredAmount = insAmt;
            Length = len;
            Width = width;
            Height = height;
            IsOneRate = oneRate;
            IsInsured = insured;
            IsOrdnance = ammo;
        }

        //// QUOTE AMMO
        //public ShippingItem(int itemId, int units, SignatureOptionType sigTyp, FedExPkgTypes box, decimal wgt, decimal insAmt, string len, string width, string height, bool oneRate, bool insured, bool ammo)
        //{
        //    ItemNumber = itemId;
        //    PackageCount = units;
        //    SignatureType = sigTyp;
        //    BoxSize = box;
        //    Weight = wgt;
        //    InsuredAmount = insAmt;
        //    Length = len;
        //    Width = width;
        //    Height = height;
        //    IsOneRate = oneRate;
        //    IsInsured = insured;
        //    IsOrdnance = ammo;
        //}

        // GUNS & MERCHANDISE
        public ShippingItem(int itemId, int units, SignatureOptionType sigTyp, decimal wgt, decimal insAmt, string len, string width, string height, bool insured)
        {
            ItemNumber = itemId;
            Units = units;
            SignatureType = sigTyp;
            Weight = wgt;
            InsuredAmount = insAmt;
            Length = len;
            Width = width;
            Height = height;
            IsInsured = insured;
        }


        //SHIPPING QUOTE
        public ShippingItem(int cnt, bool one, bool ins, bool ord, decimal amt, string len, string wid, string hgt, SignatureOptionType stp, FedExPkgTypes box)
        {
            BoxCount = cnt;
            IsOneRate = one;
            IsInsured = ins;
            IsOrdnance = ord;
            InsuredAmount = amt;
            Length = len;
            Width = wid;
            Height = hgt;
            SignatureType = stp;
            BoxSize = box;
        }

    }



}