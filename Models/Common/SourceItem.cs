using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class SourceItem
    {
        public string DistCode { get; set; }
        public string Margin { get; set; }
        public string StrCost { get; set; }
        public string StrGross { get; set; }
        public double Cost { get; set; }
        public int FflCode { get; set; }
        public int DistId { get; set; }
        public int Units { get; set; }
        public int SupplierId { get; set; }

        public SourceItem() { }

        public SourceItem(string dc, string mg, int su, int ut, string cs, string gs)
        {

            DistCode = dc;
            Margin = mg;
            SupplierId = su;
            Units = ut;
            StrCost = cs;
            StrGross = gs;
        }

    }


}