using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class CartModel
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public int CartId { get; set; }
        public int DistributorId { get; set; }
        public int TransId { get; set; }
        public int TransactionId { get; set; }
        public int TransTypeId { get; set; }
        public int UserID { get; set; }
        public int CategoryId { get; set; }
        public int GroupCatId { get; set; }
        public int SubCatId { get; set; }
        public int SupplierId { get; set; }
        public int MasterId{ get; set; }
        public int InStockId { get; set; }
        public int ImgDist { get; set; }
        public int SourceId { get; set; }
        public int Units { get; set; }
        public int FflCode { get; set; }
        public bool IsWebItem { get; set; }
        public bool IsTaxable { get; set; }
        public double Cost { get; set; }
        public double Freight { get; set; }
        public double Fees { get; set; }
        public double Insurance { get; set; }
        public double Rent { get; set; }
        public double Price { get; set; }
        public double Extension { get; set; }
        public double Parts { get; set; }
        public double Labor { get; set; }
        public double CartTotal { get; set; }
        public string CatName { get; set; }
        public string Manuf { get; set; }
        public string Mpn { get; set; }
        public string Model { get; set; }
        public string Cond { get; set; }
        public string ImgName { get; set; }
        public string ItemDesc { get; set; }
        public string Notes { get; set; }

        public CartModel() { }

        public CartModel(int tid, int sup, int isi, int mid, int unt, double prc, int did)
        {
            TransactionId = tid;
            SupplierId = sup;
            InStockId = isi;
            MasterId = mid;
            Units = unt;
            Price = prc;
            DistributorId = did;
        }

        public CartModel(int tid, int cid, int sup, int isi, int mid, int unt, double cst)
        {
            TransactionId = tid;
            CartId = cid;
            SupplierId = sup;
            InStockId = isi;
            MasterId = mid;
            Units = unt;
            Cost = cst;
        }

        public CartModel(int tid, int unt)
        {
            TransactionId = tid;
            Units = unt;
        }

        public CartModel(int tid, int unt, string not)
        {
            TransactionId = tid;
            Units = unt;
            Notes = not;
        }

        public CartModel(int tid, bool tax, double prc, string dsc)
        {
            TransactionId = tid;
            IsTaxable = tax;
            Price = prc;
            ItemDesc = dsc;
        }


        public CartModel(int rid, int id, int catId, int unt, int dst, int ttp, string cat, string mfg, string mod, string desc, string mpn, string cnd, string img, string not, double prc, double ext, double ctl, double ins, double fee, double pts, double lab) {
            RowId = rid;
            Id = id;
            CategoryId = catId;
            Units = unt;
            ImgDist = dst;
            TransTypeId = ttp;
            CatName = cat;
            Manuf = mfg;
            Model = mod;
            ItemDesc = desc;
            Mpn = mpn;
            Cond = cnd;
            ImgName = img;
            Notes = not;
            Price = prc;
            Extension = ext;
            CartTotal = ctl;
            Insurance = ins;
            Rent = fee;
            Parts = pts;
            Labor = lab;
        }

        public CartModel(int rid, int id, int catId, int unt, int dst, string cat, string mfg, string mod, string desc, string mpn, string cnd, string img, double prc, double ext, double ctl)
        {
            RowId = rid;
            Id = id;
            CategoryId = catId;
            Units = unt;
            ImgDist = dst;
            CatName = cat;
            Manuf = mfg;
            Model = mod;
            ItemDesc = desc;
            Mpn = mpn;
            Cond = cnd;
            ImgName = img;
            Price = prc;
            Extension = ext;
            CartTotal = ctl;
        }


        public CartModel(int tid, int sid, int qty, double prc, double cst, double frt, double fee)
        { 
            TransactionId = tid;
            SupplierId = sid;
            Units = qty;
            Price = prc;
            Cost = cst;
            Freight = frt;
            Fees = fee;
        }


        public CartModel(int tid, int qty, double prc, double cst, double frt, double fee)
        {
            TransactionId = tid;
            Units = qty;
            Price = prc;
            Cost = cst;
            Freight = frt;
            Fees = fee;
        }


    }
}