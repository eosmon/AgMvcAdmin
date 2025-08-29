using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class OrderCount
    {
        public int Transactions { get; set; }
        public int Cart { get; set; }
        public int Address { get; set; }
        public int LockModel { get; set; }
        public int DistributorUnits { get; set; }

        public OrderCount() { }

        public OrderCount(int tid, int cid, int aid, int lmd, int dst)
        {
            Transactions = tid;
            Cart = cid;
            Address = aid;
            LockModel = lmd;
            DistributorUnits = dst;
        }
    }
}