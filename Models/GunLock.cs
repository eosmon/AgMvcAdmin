using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class GunLock
    {
        public int LockManufId { get; set; }
        public int LockModelId { get; set; }
        public string LockModel { get; set; }

        public GunLock() { }

        public GunLock(int mfg, int mid, string mod)
        {
            LockManufId = mfg;
            LockModelId = mid;
            LockModel = mod;
        }


    }



}