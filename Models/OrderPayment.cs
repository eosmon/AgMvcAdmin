using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class OrderPayment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int PaymentTypeId { get; set; }
        public int CardLastFour { get; set; }
        public double BeginBalance { get; set; }
        public double EndingBalance { get; set; }
        public double AmountPaid { get; set; }
        public string AuthCode { get; set; }
        public string CheckNumber { get; set; }
        public string StrDate { get; set; }
        public string PaymentDesc { get; set; }
        public DateTime PaymentDate { get; set; }

        public OrderPayment() { }

        // Insert Payment
        public OrderPayment(int oid, int cid, int clf, int ptp, string ath, string chk, double bbl, double apd, DateTime pdt)
        {
            OrderId = oid;
            CustomerId = cid;
            CardLastFour = clf;
            PaymentTypeId = ptp;
            AuthCode = ath;
            CheckNumber = chk;
            BeginBalance = bbl;
            AmountPaid = apd;
            PaymentDate = pdt;
        }

        // View Payments
        public OrderPayment(int id, double bbl, double apd, double ebl, string des, string dat)
        {
            Id = id;
            BeginBalance = bbl;
            AmountPaid = apd;
            EndingBalance = ebl;
            PaymentDesc = des;
            StrDate = dat;
        }
    }
}