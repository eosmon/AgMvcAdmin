using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class FflLicenseModel
    {
        public int FflId { get; set; }
        public int FflCode { get; set; }
        public int FflStateId { get; set; }
        public int LicTypeId { get; set; }
        public int LicRegion { get; set; }
        public int LicDistrict { get; set; }
        public int FflZipCode { get; set; }
        public int FflExpDay { get; set; }
        public int FflExpMo { get; set; }
        public int FflExpYear { get; set; }
        public int CaCfdNumber { get; set; }
        public string LicCounty { get; set; }
        public string LicType { get; set; }
        public string LicExpCode { get; set; }
        public string LicSequence { get; set; }
        public string LicName { get; set; }
        public string TradeName { get; set; }
        public string FflAddress { get; set; }
        public string FflCity { get; set; }
        public string FflState { get; set; }
        public string FflPhone { get; set; }
        public string FflEmail { get; set; }
        public string FflFax { get; set; }
        public string FflNumber { get; set; }
        public string FflFullLic { get; set; }
        public string FflCityStZip { get; set; }
        public string FflFullTxt { get; set; }
        public string FflValStr { get; set; }
        public string FflExpires { get; set; }
        public string MapString { get; set; }
        public bool CaHasHiCap { get; set; }
        public bool HasImgFfl { get; set; }
        public bool FflExists { get; set; }
        public bool FflOnFile { get; set; }
        public bool FflIsValid { get; set; }
        public bool IsExpired { get; set; }
        public bool DoTransfers { get; set; }
        public double Distance { get; set; }
        public DateTime ExpDate { get; set; }

        public FflLicenseModel() { }

        public FflLicenseModel(int id, string fullTxt, string trade, string fullLicNum, bool licValid, bool licOnFile, string licExpDate)
        {
            FflId = id;
            FflFullTxt = fullTxt;
            TradeName = trade;
            FflFullLic = fullLicNum;
            FflIsValid = licValid;
            FflOnFile = licOnFile;
            FflExpires = licExpDate;
        }

        public FflLicenseModel(string txt, string val)
        {
            FflFullTxt = txt;
            FflValStr = val;
        }

        public FflLicenseModel(int id, bool doTransfers, string tradeName, string address, string phone)
        {
            FflId = id;
            DoTransfers = doTransfers;
            TradeName = tradeName;
            FflAddress = address;
            FflPhone = phone;
        }

        public FflLicenseModel(int fflId, string trade, string addr, string cityStZip, string phone, string licNum)
        {
            FflId = fflId;
            TradeName = trade;
            FflAddress = addr;
            FflCityStZip = cityStZip;
            FflPhone = phone;
            FflNumber = licNum;
        }


        public FflLicenseModel(int fflId, bool doTrn, string trade, string addr, string cityStZip, string phone, string mapStr)
        {
            FflId = fflId;
            DoTransfers = doTrn;
            TradeName = trade;
            FflAddress = addr;
            FflCityStZip = cityStZip;
            FflPhone = phone;
            MapString = mapStr;
        }


        public FflLicenseModel(int fflId, bool doTrn, string trade, string addr, string cityStZip, string phone, string mapStr, double distance, int zipCode)
        {
            FflId = fflId;
            DoTransfers = doTrn;
            TradeName = trade;
            FflAddress = addr;
            FflCityStZip = cityStZip;
            FflPhone = phone;
            MapString = mapStr;
            Distance = distance;
            FflZipCode = zipCode;
        }


        public FflLicenseModel(int id, bool doTransfers, double distance, string tradeName, string address, string phone)
        {
            FflId = id;
            DoTransfers = doTransfers;
            Distance = distance;
            //FflZipCode = zipCode;
            TradeName = tradeName;
            FflAddress = address;
            FflPhone = phone;
        }

        public FflLicenseModel(bool doTransfers, string tradeName, string address, string city, string state, int zipCode, string phone, string fflNum)
        {
            DoTransfers = doTransfers;
            TradeName = tradeName;
            FflAddress = address;
            FflCity = city;
            FflState = state;
            FflZipCode = zipCode;
            FflPhone = phone;
            FflNumber = fflNum;
        }

        public FflLicenseModel(int fflId, int fflCode, string trade, string addr, string cityStZip, string phone, string licNum, string fullLicNum, bool licValid, bool licOnFile, string licExpDate, string email)
        {
            FflId = fflId;
            FflCode = fflCode;
            TradeName = trade;
            FflAddress = addr;
            FflCityStZip = cityStZip;
            FflPhone = phone;
            FflNumber = licNum;
            FflFullLic = fullLicNum;
            FflIsValid = licValid;
            FflOnFile = licOnFile;
            FflExpires = licExpDate;
            FflEmail = email;
        }


        public FflLicenseModel(string trade, string fullLic, bool isValid, bool onFile, string expDate, string email)
        {
            TradeName = trade;
            FflFullLic = fullLic;
            FflIsValid = isValid;
            FflOnFile = onFile;
            FflExpires = expDate;
            FflEmail = email;
        }

        // CURIO & RELIC
        public FflLicenseModel(int expDy, int expMo, int expYr,  int zip, int reg, int dist, int state, string city, string cnty, string expCd, string seq, string licTyp, string licName, string addr)
        {
 

                FflExpDay = expDy;
                FflExpMo = expMo;
                FflExpYear = expYr;
                FflZipCode = zip;
                LicRegion = reg;
                LicDistrict = dist;
                FflStateId = state;
                FflCity = city;
                LicCounty = cnty;
                LicExpCode = expCd;
                LicSequence = seq;
                LicType = licTyp;
                LicName = licName;
                FflAddress = addr;
        }

        // FFL
        public FflLicenseModel(int fcd, int expDy, int expMo, int expYr, bool hiCap, bool isExp, int zip, int reg, int dist, int state, int caCfd, string city, string cnty, string expCd, string seq, string licTyp, string licName, string trade, string addr, string phn)
        {
            FflCode = fcd;
            FflExpDay = expDy;
            FflExpMo = expMo;
            FflExpYear = expYr;
            CaHasHiCap = hiCap;
            IsExpired = isExp;
            FflZipCode = zip;
            LicRegion = reg;
            LicDistrict = dist;
            FflStateId = state;
            CaCfdNumber = caCfd;
            FflCity = city;
            LicCounty = cnty;
            LicExpCode = expCd;
            LicSequence = seq;
            LicType = licTyp;
            LicName = licName;
            TradeName = trade;
            FflAddress = addr;
            FflPhone = phn;
        }
    }
}