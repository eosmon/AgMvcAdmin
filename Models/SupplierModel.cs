using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class SupplierModel
    {
        public int Id { get; set; }
        public int SupplerTypeId { get; set; }
        public int FflCode { get; set; }
        public int StateId { get; set; }
        public int IdType { get; set; }
        public int IdState { get; set; }
        public int PptCtYtd { get; set; }
        public int PptCtLocal { get; set; }
        public int PptCtOther { get; set; }
        public string IdNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrgName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CurFfl { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipExt { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LineName { get; set; }
        public string LineAddress { get; set; }
        public string LineCurFfl { get; set; }
        public DateTime CurExp { get; set; }
        public DateTime IdDob { get; set; }
        public DateTime IdExpires { get; set; }
        public string LineCurExp { get; set; }
        public string LineIdDob { get; set; }
        public string LineIdExp { get; set; }

        public SupplierModel() { }

        public SupplierModel(int id, int stp, int ppl, int ppo, string nam, string adr, string cur, string eml, string phn, string exp) {
            Id = id;
            SupplerTypeId = stp;
            PptCtLocal = ppl;
            PptCtOther = ppo;
            LineName = nam;
            LineAddress = adr;
            LineCurFfl = cur;
            Email = eml;
            Phone = phn;
            LineCurExp = exp;
        }

        public SupplierModel(int id, int stp, string fnm, string lnm, string eml, string phn, string adr, string cty, string sta, string zip, string ext)
        {
            Id = id;
            SupplerTypeId = stp;
            FirstName = fnm;
            LastName = lnm;
            Email = eml;
            Phone = phn;
            Address = adr;
            City = cty;
            State = sta;
            ZipCode = zip;
            ZipExt = ext;
        }

        public SupplierModel(int sup, int sta, int idt, int ids, string fnm, string lnm, string org, string adr, string cty, string zip, string ext, string phn, string eml, string cur, string idn, DateTime exp, DateTime dob, DateTime dxp)
        {
            SupplerTypeId = sup;
            StateId = sta;
            IdType = idt;
            IdState = ids;
            FirstName = fnm;
            LastName = lnm;
            OrgName = org;
            Address = adr;
            City = cty;
            ZipCode = zip;
            ZipExt = ext;
            Phone = phn;
            Email = eml;
            CurFfl = cur;
            IdNumber = idn;
            CurExp = exp;
            IdDob = dob;
            IdExpires = dxp;
        }

        public SupplierModel(DateTime exp, DateTime dob, DateTime dxp, int sid, int sta, int idt, int ids, string fnm, string lnm, string org, string adr, string cty, string zip, string ext, string phn, string eml, string cur, string idn)
        {
            CurExp = exp;
            IdDob = dob;
            IdExpires = dxp;
            Id = sid;
            StateId = sta;
            IdType = idt;
            IdState = ids;
            FirstName = fnm;
            LastName = lnm;
            OrgName = org;
            Address = adr;
            City = cty;
            ZipCode = zip;
            ZipExt = ext;
            Phone = phn;
            Email = eml;
            CurFfl = cur;
            IdNumber = idn;
        }



        public SupplierModel(int sid, int stp, int sta, int idt, int ids, int clc, int cot, int cpt, string fnm, string lnm, string org, string adr, string cty, string sst, string zip, string ext, string phn, string eml, string cur, string idn, string exp, string dob, string dxp)
        {
            Id = sid;
            SupplerTypeId = stp;
            StateId = sta;
            IdType = idt;
            IdState = ids;
            PptCtLocal = clc;
            PptCtOther = cot;
            PptCtYtd = cpt;
            FirstName = fnm;
            LastName = lnm;
            OrgName = org;
            Address = adr;
            City = cty;
            State = sst;
            ZipCode = zip;
            ZipExt = ext;
            Phone = phn;
            Email = eml;
            CurFfl = cur;
            IdNumber = idn;
            LineCurExp = exp;
            LineIdDob = dob;
            LineIdExp = dxp;
        }

        public SupplierModel(int sid, int stp, int idt, int ids, int pcl, int pco, int pcy, string fnm, string lnm, string org, string adr, string cty, string sta, string zip, string ext, string phn, string eml, 
            string idn, string ffl, string dob, string exp, string cxp)
        {
            StateId = sid;
            SupplerTypeId = stp;
            IdType = idt;
            IdState = ids;
            PptCtLocal = pcl;
            PptCtOther = pco;
            PptCtYtd = pcy;
            FirstName = fnm;
            LastName = lnm;
            OrgName = org;
            Address = adr;
            City = cty;
            State = sta;
            ZipCode = zip;
            ZipExt = ext;
            Phone = phn;
            Email = eml;
            IdNumber = idn;
            CurFfl = ffl;
            LineIdDob = dob;
            LineIdExp = exp;
            LineCurExp = cxp;
        }




    }


}