using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int CostBasisId { get; set; }
        public int ServiceBasisId { get; set; }
        public int SupplierId { get; set; }
        public int InvSrcId { get; set; } //forgot what this is??
        public int Units { get; set; }
        public int LockMakeId { get; set; }
        public int LockModelId { get; set; }
        public string ItemMake { get; set; }
        public string ItemModel { get; set; }
        public string ItemDesc { get; set; }
        public string SerialNumber { get; set; }
        public string FscNumber { get; set; }
        public double Price { get; set; }
        public double Extension { get; set; }
        public DateTime FscExpires { get; set; }
        public bool IsTaxable { get; set; }
        public List<SelectListItem> GunLockModel { get; set; }
        public List<SelectListItem> GunLockMake { get; set; }
        public List<SelectListItem> CaFsdOption { get; set; }
        public List<SelectListItem> InvSrcSales { get; set; }
        public List<SelectListItem> InvSrcService { get; set; }
    }
}