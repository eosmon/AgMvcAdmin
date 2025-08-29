using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class CustomerDoc
    {
        public Guid UserKey { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DocCatId { get; set; }
        public int DocTypeId { get; set; }
        public int StateId { get; set; }
        public int Version { get; set; }
        public string DocGroup { get; set; }
        public string DocNumber { get; set; }
        public string DocStatus { get; set; }
        public string DocType { get; set; }
        public string DocImg { get; set; }
        public string UsState { get; set; }
        public string StrExp { get; set; }
        public string StrDob { get; set; }
        public string FolderCode { get; set; }
        public string SubFolder { get; set; }
        public DateTime ExpDate { get; set; }
        public DateTime Dob { get; set; }
        public bool IsCurrent { get; set; }

        public bool IsIdField { get; set; }
        public bool IsStateField { get; set; }
        public bool IsExpField { get; set; }
        public bool IsExpired { get; set; }
        public bool IsDobField { get; set; }
        public bool IsAddrCurrent { get; set; }
        public bool IsRealId { get; set; }
        public CustomerModel CustModel { get; set; }


        public CustomerDoc(){}

        public CustomerDoc(int id, string sub)
        {
            Id = id;
            SubFolder = sub;
        }

        public CustomerDoc(int id, string sub, CustomerModel cmd)
        {
            Id = id;
            SubFolder = sub;
            CustModel = cmd;
        }

        public CustomerDoc(int id, bool isId, bool isSt, bool isExp, bool isDob, bool isAdc, bool isRid)
        {
            Id = id;
            IsIdField = isId;
            IsStateField = isSt;
            IsExpField = isExp;
            IsDobField = isDob;
            IsAddrCurrent = isAdc;
            IsRealId = isRid;
        }

        public CustomerDoc(int uid, int docCatId, int docTypeId, int stId, DateTime expDate, DateTime dob, Guid userKey, string docNumber, bool isAdc, bool isRid)
        {
            UserId = uid;
            DocCatId = docCatId;
            DocTypeId = docTypeId;
            StateId = stId;
            ExpDate = expDate;
            Dob = dob;
            UserKey = userKey;
            DocNumber = docNumber;
            IsAddrCurrent = isAdc;
            IsRealId = isRid;
        }

        public CustomerDoc(int id, int version, string exp, string dob, string docImg, string docGrp, string docTyp, string docNum)
        {
            Id = id;
            Version = version;
            StrExp = exp;
            StrDob = dob;
            DocImg = docImg;
            DocGroup = docGrp;
            DocType = docTyp;
            DocNumber = docNum;
        }

        public CustomerDoc(int id, int catId, int typId, int stateId, bool isCur, bool isAdc, bool isRid, string exp, string dob, string docNum, string imgName)
        {
            Id = id;
            DocCatId = catId;
            DocTypeId = typId;
            StateId = stateId;
            IsCurrent = isCur;
            IsAddrCurrent = isAdc;
            IsRealId = isRid;
            StrExp = exp;
            StrDob = dob;
            DocNumber = docNum;
            DocImg = imgName;
        }

        public CustomerDoc(int id, int docCatId, int docTypeId, int stId, DateTime expDate, DateTime dob, string docNumber, bool isAdc, bool isRid)
        {
            Id = id;
            DocCatId = docCatId;
            DocTypeId = docTypeId;
            StateId = stId;
            ExpDate = expDate;
            Dob = dob;
            DocNumber = docNumber;
            IsAddrCurrent = isAdc;
            IsRealId = isRid;
        }

        public CustomerDoc(int vid, bool cur, bool ixp, string exp, string sta, string grp, string typ, string num, string sts, string img)
        {
            Version = vid;
            IsCurrent = cur;
            IsExpired = ixp;
            StrExp = exp;
            UsState = sta;
            DocGroup = grp;
            DocType = typ;
            DocNumber = num;
            DocStatus = sts;
            DocImg = img;
        }
    }
}