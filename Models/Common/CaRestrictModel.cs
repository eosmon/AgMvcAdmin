using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class CaRestrictModel
    {
        public string CflcInbound{ get; set; }
        public string StrHoldExp { get; set; }
        public int LockMakeId { get; set; }
        public int LockModelId { get; set; }
        public int HiCapMagCount { get; set; }
        public int HiCapCapacity { get; set; }
        public int HiCapMagCaliberId { get; set; }

        public DateTime HoldGunExpires { get; set; }
        public DateTime HiCapAcqDate { get; set; }

        public bool HoldGun { get; set; }
        public bool GunHasLock { get; set; }
        public bool CaHide { get; set; }
        public bool CaOkay { get; set; }
        public bool CaRosterOk { get; set; }
        public bool CaPptOk { get; set; }
        public bool CaCurioOk { get; set; }
        public bool CaSglActnOk { get; set; }
        public bool CaSglShotOk { get; set; }
        public bool IsActualPpt { get; set; }


        public CaRestrictModel()
        {
        }

        public CaRestrictModel(bool ok, bool rost, bool ppt, bool cur, bool atn, bool sht)
        {
            CaOkay = ok;
            CaRosterOk = rost;
            CaPptOk = ppt;
            CaCurioOk = cur;
            CaSglActnOk = atn;
            CaSglShotOk = sht;
        }

        public CaRestrictModel(bool hdc, bool ok, bool rost, bool ppt, bool cur, bool atn, bool sht)
        {
            CaHide = hdc;
            CaOkay = ok;
            CaRosterOk = rost;
            CaPptOk = ppt;
            CaCurioOk = cur;
            CaSglActnOk = atn;
            CaSglShotOk = sht;
        }


        public CaRestrictModel(bool hide, bool ok, bool rost, bool ppt, bool cur, bool atn, bool sht, bool ipt)
        {
            CaHide = hide;
            CaOkay = ok;
            CaRosterOk = rost;
            CaPptOk = ppt;
            CaCurioOk = cur;
            CaSglActnOk = atn;
            CaSglShotOk = sht;
            IsActualPpt = ipt;
        }


        public CaRestrictModel(bool holdGun, int capacity, int hiCapCt, DateTime holdExp)
        {
            HoldGun = holdGun;
            HiCapCapacity = capacity;
            HiCapMagCount = hiCapCt;
            HoldGunExpires = holdExp;
        }


        public CaRestrictModel(string cflc, bool holdGun, DateTime holdGunExp, int lockMakeId, int lockModelId,
            int capacity, int hiCapCt, bool hasLock, bool isPpt)
        {
            CflcInbound = cflc;
            HoldGun = holdGun;
            HoldGunExpires = holdGunExp;
            LockMakeId = lockMakeId;
            LockModelId = lockModelId;
            HiCapCapacity = capacity;
            HiCapMagCount = hiCapCt;
            GunHasLock = hasLock;
            CaPptOk = isPpt;
        }


        public CaRestrictModel(int lMake, int lMdl, int magCt, int magCap, string cflc, string exp, bool hold, bool hide, bool ok, bool rst, bool ppt, bool cur, bool sAtn, bool sSht)
        {
            LockMakeId = lMake;
            LockModelId = lMdl;
            HiCapMagCount = magCt;
            HiCapCapacity = magCap;
            CflcInbound = cflc;
            StrHoldExp = exp;
            HoldGun = hold;
            CaHide = hide;
            CaOkay = ok;
            CaRosterOk = rst;
            CaPptOk = ppt;
            CaCurioOk = cur;
            CaSglActnOk = sAtn;
            CaSglShotOk = sSht;

        }

        // NON SALE GUN READ
        public CaRestrictModel(int lMake, int lMdl, int magCt, int magCap, bool hold, string cflc, string holdExp)
        {
            LockMakeId = lMake;
            LockModelId = lMdl;
            HiCapMagCount = magCt;
            HiCapCapacity = magCap;
            HoldGun = hold;
            CflcInbound = cflc;
            StrHoldExp = holdExp;
        }


        public CaRestrictModel(int lMake, int lMdl, int magCt, int magCap, bool cok, bool hide, bool hold, bool isPpt, bool cur, bool rst, bool sae, bool sse, bool ipt, string cflc, string holdExp)
        {
            LockMakeId = lMake;
            LockModelId = lMdl;
            HiCapMagCount = magCt;
            HiCapCapacity = magCap;
            CaOkay = cok;
            CaHide = hide;
            HoldGun = hold;
            CaPptOk = isPpt;
            CaCurioOk = cur;
            CaRosterOk = rst;
            CaSglActnOk = sae;
            CaSglShotOk = sse;
            IsActualPpt = ipt;
            CflcInbound = cflc;
            StrHoldExp = holdExp;
        }

        public CaRestrictModel(int lMake, int lMdl, int magCt, int magCap, bool cok, bool hide, bool hold, bool isPpt, bool cur, bool rst, bool sae, bool sse, bool ipt, string cflc, DateTime holdExp)
        {
            LockMakeId = lMake;
            LockModelId = lMdl;
            HiCapMagCount = magCt;
            HiCapCapacity = magCap;
            CaOkay = cok;
            CaHide = hide;
            HoldGun = hold;
            CaPptOk = isPpt;
            CaCurioOk = cur;
            CaRosterOk = rst;
            CaSglActnOk = sae;
            CaSglShotOk = sse;
            IsActualPpt = ipt;
            CflcInbound = cflc;
            HoldGunExpires = holdExp;
        }

        public CaRestrictModel(int lMake, int lMdl, int magCt, int magCap, string cflc, DateTime hldExp, DateTime acqDate, bool holdGun, bool ppt, bool ipt)
        {
            LockMakeId = lMake;
            LockModelId = lMdl;
            HiCapMagCount = magCt;
            HiCapCapacity = magCap;
            CflcInbound = cflc;
            HoldGunExpires = hldExp;
            HiCapAcqDate = acqDate;
            HoldGun = holdGun;
            CaPptOk = ppt;
            IsActualPpt = ipt;
        }

 




    }

}