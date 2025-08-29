using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class FulfillModel
    {

        public int TransactionId { get; set; }
        public int FulfillSrcId { get; set; }
        public int RecipSrcId { get; set; }
        public int FflSrcId { get; set; }
        public int FflStateId { get; set; }
        public int StateId { get; set; }
        public int PuStateId { get; set; }
        public int FflCode { get; set; }
        public int FflId { get; set; }
        public int RcSupplierId { get; set; }
        public int RcAcqSrcId { get; set; }
        public int RcFflCode { get; set; }
        public int PuSupplierId { get; set; }
        public int PuAcqSrcId { get; set; }
        public int PuFflCode { get; set; }
        public int RecoveryObjId { get; set; }
        public string AttyName { get; set; }
        public string AttyPhone { get; set; }
        public string AttyEmail { get; set; }
        public string CaseOfcName { get; set; }
        public string CaseOfcPhone { get; set; }
        public string CaseOfcEmail { get; set; }
        public string StateName { get; set; }
        public string OrgName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public string ZipExt { get; set; }
        public string PuOrgName { get; set; }
        public string PuFirstName { get; set; }
        public string PuLastName { get; set; }
        public string PuAddress { get; set; }
        public string PuCity { get; set; }
        public string PuPhone { get; set; }
        public string PuEmail { get; set; }
        public string PuZipCode { get; set; }
        public string PuZipExt { get; set; }
        public string RcEmail { get; set; }
        public string CurioName { get; set; }
        public string CurioNumber { get; set; }
        public string CaDojCflc { get; set; }
        public string Notes { get; set; }
        public DateTime CurioExp { get; set; }

        public FulfillModel() { }


        public FulfillModel(int tid, int fid, int rid, int fsc, int fst, int sid, int pst, int ffl, string org, string fnm, string lnm, string adr, string cty, string zip, string ext, string phn, string eml,
            string por, string pfn, string pln, string pad, string pct, string pzp, string pxt, string ppn, string pem, string cnm, string cnu, string nts, DateTime exp)
        {
            TransactionId = tid;
            FulfillSrcId = fid;
            RecipSrcId = rid;
            FflSrcId = fsc;
            FflStateId = fst;
            StateId = sid;
            PuStateId = pst;
            FflCode = ffl;
            OrgName = org;
            FirstName = fnm;
            LastName = lnm;
            Address = adr;
            City = cty;
            ZipCode = zip;
            ZipExt = ext;
            Phone = phn;
            Email = eml;

            PuOrgName = por;
            PuFirstName = pfn;
            PuLastName = pln;
            PuAddress = pad;
            PuCity = pct;
            PuZipCode = pzp;
            PuZipExt = pxt;
            PuPhone = ppn;
            PuEmail = pem;

            CurioName = cnm;
            CurioNumber = cnu;
            Notes = nts;
            CurioExp = exp;
        }


        /* CUSTOM GUN READ */
        public FulfillModel(int rid, int fsc, int fst, int sid, int fcd, string org, string fnm, string lnm, string adr, string cty, string zip, string ext, string phn, string eml, string cfl, string cnm)
        {
            RecipSrcId = rid;
            FflSrcId = fsc;
            FflStateId = fst;
            StateId = sid;
            FflCode = fcd;
            OrgName = org;
            FirstName = fnm;
            LastName = lnm;
            Address = adr;
            City = cty;
            ZipCode = zip;
            ZipExt = ext;
            Phone = phn;
            Email = eml;
            CurioNumber = cfl;
            CurioName = cnm;
        }

        /* CUSTOM GUN READ */
        public FulfillModel(string org, string fnm, string lnm, string adr, string cty, string zip, string ext, string phn, string eml, string cfl, string cnm)
        {
            OrgName = org;
            FirstName = fnm;
            LastName = lnm;
            Address = adr;
            City = cty;
            ZipCode = zip;
            ZipExt = ext;
            Phone = phn;
            Email = eml;
            CurioNumber = cfl;
            CurioName = cnm;
        }

        //public FulfillModel(int tid, int ful, int sup, int raq, int rfc, int psu, int paq, int pfc, string rem, string pem, string not)
        //{
        //    TransactionId = tid;
        //    FulfillSrcId = ful;
        //    RcSupplierId = sup;
        //    RcAcqSrcId = raq;
        //    RcFflCode = rfc;
        //    PuSupplierId = psu;
        //    PuAcqSrcId = paq;
        //    PuFflCode = pfc;
        //    RcEmail = rem;
        //    PuEmail = pem;
        //    Notes = not;
        //}

        public FulfillModel(int tid, int ful, int sup, int raq, int rfc, int psu, int paq, int pfc, int rec, string rem, string pem, string atn, string atp, string ate, string con, string cop, string coe, string not)
        {
            TransactionId = tid;
            FulfillSrcId = ful;
            RcSupplierId = sup;
            RcAcqSrcId = raq;
            RcFflCode = rfc;
            PuSupplierId = psu;
            PuAcqSrcId = paq;
            PuFflCode = pfc;
            RecoveryObjId = rec;
            RcEmail = rem;
            PuEmail = pem;
            AttyName = atn;
            AttyPhone = atp;
            AttyEmail = ate;
            CaseOfcName = con;
            CaseOfcPhone = cop;
            CaseOfcEmail = coe;
            Notes = not;
        }


        public FulfillModel(int tid, int ful, int rec, string atn, string atp, string ate, string con, string cop, string coe, string not)
        {
            TransactionId = tid;
            FulfillSrcId = ful;
            RecoveryObjId = rec;
            AttyName = atn;
            AttyPhone = atp;
            AttyEmail = ate;
            CaseOfcName = con;
            CaseOfcPhone = cop;
            CaseOfcEmail = coe;
            Notes = not;
        }










    }
}