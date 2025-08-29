using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class OrderSupplier
    {
        public int Id { get; set; }
        public int InStockId { get; set; }
        public int LocationId { get; set; }
        public int ItemBasisId { get; set; }
        public int TransactionId { get; set; }
        public int UnitsCal { get; set; }
        public int UnitsWyo { get; set; }
        public int CartId { get; set; }
        public int DistributorId { get; set; }
        public string TransSku { get; set; }
        public string Distributor { get; set; }
        public string SerialNumber { get; set; }
        public bool IsGun { get; set; }

        public OrderSupplier() { }

        public OrderSupplier(int id, int tid, int cid, int did, string dis)
        {
            Id = id;
            TransactionId = tid;
            CartId = cid;
            DistributorId = did;
            Distributor = dis;
        }

        public OrderSupplier(int ibi, int isi, int cid, int tid, int lid, int uca, int uwy, string sku, string ser, bool ign)
        {
            ItemBasisId = ibi;
            InStockId = isi;
            CartId = cid;
            TransactionId = tid;
            LocationId = lid;
            UnitsCal = uca;
            UnitsWyo = uwy;
            TransSku = sku;
            SerialNumber = ser;
            IsGun = ign;
        }
    }
}