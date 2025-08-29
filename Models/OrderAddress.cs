using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class OrderAddress
    {
        public string ItemDesc { get; set; }
        public string FullName { get; set; }
        public string OrgName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }
        public string ZipExt { get; set; }
        public string StrFFL { get; set; }
        public int CustAddressID { get; set; }
        public int StateId { get; set; }
        public int TransactionId { get; set; }
        public int CartId { get; set; }
        public int FflCode { get; set; }
        public bool IsSellerAddr { get; set; }
        public bool IsShipAddr { get; set; }
        public bool IsPickupAddr { get; set; }
        public bool IsDeliveryAddr { get; set; }


        public OrderAddress() { }

        public OrderAddress(string onm, string oad, string oct, string ost, string ozc, string oph, string oem)
        {
            OrgName = onm;
            Address = oad;
            City = oct;
            StateName = ost;
            ZipCode = ozc;
            Phone = oph;
            EmailAddress = oem;
        }

        public OrderAddress(string onm, string ofn, string oln, string oad, string osu, string oct, string ost, string ozc, string oze, string oph, string oem)
        {
            OrgName = onm;
            FirstName = ofn;
            LastName = oln;
            Address = oad;
            Suite = osu;
            City = oct;
            StateName = ost;
            ZipCode = ozc;
            ZipExt = ozc;
            Phone = oph;
            EmailAddress = oem;
        }

        public OrderAddress(int tid, int cid, int ffl, bool sla, bool sha, bool pua, bool dad, string fnm, string lnm, string org, string adr, string cty, string sta, string zip, string ext, string phn, string eml, string dsc)
        {
            TransactionId = tid;
            CartId = cid;
            FflCode = ffl;
            IsSellerAddr = sla;
            IsShipAddr = sha;
            IsPickupAddr = pua;
            IsDeliveryAddr = dad;
            FirstName = fnm;
            LastName = lnm;
            OrgName = org;
            Address = adr;
            City = cty;
            StateName = sta;
            ZipCode = zip;
            ZipExt = ext;
            Phone = phn;
            EmailAddress = eml;
            ItemDesc = dsc;
        }

        public OrderAddress(string dsc, string ful, string org, string fnm, string lnm, string adr, string cty, string sta, string zip, string ext, string eml, string phn, string ffl)
        {
            ItemDesc = dsc;
            FullName = ful;
            OrgName = org;
            FirstName = fnm;
            LastName = lnm;
            Address = adr;
            City = cty;
            StateName = sta;
            ZipCode = zip;
            ZipExt = ext;
            EmailAddress = eml;
            Phone = phn;
            StrFFL = ffl;
        }

        public OrderAddress(string org, string fnm, string lnm, string adr, string cty, string sta, string zip, string phn, string eml)
        {
            OrgName = org; ;
            FirstName = fnm;
            LastName = lnm;
            Address = adr;
            City = cty;
            StateName = sta;
            ZipCode = zip;
            Phone = phn;
            EmailAddress = eml;
        }


    }
}