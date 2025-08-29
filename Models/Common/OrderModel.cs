using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class OrderModel
    {
        public int OrderMasterId { get; set; }
        public int OrderId { get; set; }
        public int UserID { get; set; }
        public int LocationId { get; set; }
        public int SalesRepId { get; set; }
        public int TransTypeId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsQuote { get; set; }
        public bool IsPpt { get; set; }
        public string OrderNumber { get; set; }

        public OrderModel() { }

        public OrderModel(int id, string nb)
        {
            OrderId = id;
            OrderNumber = nb;
        }

        public OrderModel(int id, int tt)
        {
            OrderMasterId = id;
            TransTypeId = tt;
        }

        public OrderModel(bool ppt, int id, int tt)
        {
            IsPpt = ppt;
            OrderMasterId = id;
            TransTypeId = tt;
        }

        public OrderModel(int id, int tt, bool iq)
        {
            OrderId = id;
            TransTypeId = tt;
            IsQuote = iq;
        }

        public OrderModel(int uid, int lid, int sid, bool iqt, DateTime odt) {

            UserID = uid;
            LocationId = lid;
            SalesRepId = sid;
            OrderDate = odt;
            IsQuote = iqt;
        }

        public OrderModel(int oid, int uid, int lid, int sid, bool iqt, DateTime odt)
        {
            OrderMasterId = oid;
            UserID = uid;
            LocationId = lid;
            SalesRepId = sid;
            IsQuote = iqt;
            OrderDate = odt;
        }





    }
}