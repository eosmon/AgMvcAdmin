using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class AddToBookModel
    {
        public GunModel GunBasic { get; set; }
        public GunModel Gun { get; set; }
        public AcctModel Accounting { get; set; }
        public CaRestrictModel Compliance { get; set; }
        public BoundBookModel BoundBook { get; set; }
        public ImageModel Images { get; set; }
        public int CustomerId { get; set; }
        public int SellerId { get; set; } /* DEPRICATED */
        public int SupplierId { get; set; }

        public AddToBookModel() { }

        public AddToBookModel(GunModel gm, AcctModel ac, BoundBookModel bb, CaRestrictModel cm, int cu, int su)
        {
            Gun = gm;
            Accounting = ac;
            BoundBook = bb;
            Compliance = cm;
            CustomerId = cu;
            SupplierId = su;
        }

        public AddToBookModel(AcctModel ac, CaRestrictModel cm, GunModel gm, int cu, int sl)
        {
            Accounting = ac;
            Compliance = cm;
            Gun = gm;
            CustomerId = cu;
            SellerId = sl;
        }

    }


}