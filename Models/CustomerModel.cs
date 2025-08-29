using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgMvcAdmin.Models.Common;
using AgMvcAdmin.Common;

namespace AgMvcAdmin.Models
{
    public class CustomerModel
    {
        public CustomerTypes CustomerType { get; set; }
        public SecurityModel SecurityBase { get; set; }
        public CustomerBaseModel CustomerBase { get; set; }
        public FflLicenseModel FedFireLicBase { get; set; }
        public CurioRelicModel CurioRelicBase { get; set; }
        public LeoModel LeoBase { get; set; }
        public List<CustomerDoc> CustomerDocs { get; set; }
        public List<CustomerDoc> PresentationDocs { get; set; }
        public List<SelectListItem> CustAddresses { get; set; }
        public string FName { get; set; }
        public string CompanyName { get; set; }
        public string Fax { get; set; }
        public string CaFscNumber { get; set; }
        public string CaFscExpMo { get; set; }
        public string ResaleNumber { get; set; }
        public string CustomerNotes { get; set; }
        public string StrFullName { get; set; }
        public string StrFullAddr { get; set; }
        public string StrEmailPhn { get; set; }
        public string StrCustType { get; set; }
        public string StrDob { get; set; }
        public string ProfilePic { get; set; }
        public bool AddWebAccesss { get; set; }
        public bool EmailMatch { get; set; }
        public bool IsCitizen { get; set; }
        public bool IsPermResident { get; set; }
        public bool CaHasGunSafe { get; set; }
        public bool BuyForResale { get; set; }
        public bool GotStateId { get; set; }
        public bool AgreeSigned { get; set; }
        public bool IsOnWeb { get; set; }
        public bool IsVip { get; set; }
        public bool IsReg { get; set; }
        public bool IsAge21 { get; set; }
        public bool IsDupErr { get; set; }
        public int CustomerId { get; set; }
        public int CustomerTypeId { get; set; }
        public int StateIdType { get; set; }
        public int StateIssued { get; set; }
        public int DobDay { get; set; }
        public int DobMonth { get; set; }
        public int DobYear { get; set; }
        public int CaFscStatus { get; set; }
        public int CaFscExpDay { get; set; }
        public int CaFscExpYear { get; set; }
        public int SourceId { get; set; }
        public int IndustryId { get; set; }
        public int ProfessionId { get; set; }
        public int DocCtAll { get; set; }
        public int DocCtCurrent { get; set; }
        public int DocCtArchived { get; set; }

        public CustomerModel() { }

        public CustomerModel(int curCt, int arcCt)
        {
            DocCtCurrent = curCt;
            DocCtArchived = arcCt;
        }

        public CustomerModel(int curCt, int arcCt, int allCt)
        {
            DocCtCurrent = curCt;
            DocCtArchived = arcCt;
            DocCtAll = allCt;
        }

        public CustomerModel(bool emailMatch)
        {
            EmailMatch = emailMatch;
        }

        public CustomerModel(int id, int cTypeId, CustomerBaseModel cbm)
        {
            CustomerId = id;
            CustomerTypeId = cTypeId;
            CustomerBase = cbm;
        }


        public CustomerModel(bool emailMatch, SecurityModel model, string firstName)
        {
            EmailMatch = emailMatch;
            SecurityBase = model;
            FName = firstName;
        }

        public CustomerModel(int id, bool isReg, string name, string addr, string phnEml, string custType, string pic, CustomerBaseModel cb)
        {
            CustomerId = id;
            IsReg = isReg;
            StrFullName = name;
            StrFullAddr = addr;
            StrEmailPhn = phnEml;
            StrCustType = custType;
            ProfilePic = pic;
            CustomerBase = cb;
        }

        public CustomerModel(int id, int src, int ind, int prof, int mo, int day, int yr, int dca, int dcc, int arc, string fax, string notes, string img, bool vip, bool iow)
        {
            CustomerId = id;
            SourceId = src;
            IndustryId = ind;
            ProfessionId = prof;
            DobMonth = mo;
            DobDay = day;
            DobYear = yr;
            DocCtAll = dca;
            DocCtCurrent = dcc;
            DocCtArchived = arc;
            Fax = fax;
            CustomerNotes = notes;
            ProfilePic = img;
            IsVip = vip;
            IsOnWeb = iow;
        }
    }
}