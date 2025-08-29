using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class SaleModel
    {
        public int CategoryId { get; set; }
        public int CaliberId { get; set; }
        public int SubCatId { get; set; }
        public int ManufId { get; set; }
        public int IsUsed { get; set; }
        public int OnSale { get; set; }
        public List<SaleItem> Item { get; set; }

        public SaleModel(){}

        public SaleModel(int catId, int subCatId, int mfgId, int calId, int isUsed, int onSale)
        {
            CategoryId = catId;
            SubCatId = subCatId;
            ManufId = mfgId;
            CaliberId = calId;
            IsUsed = isUsed;
            OnSale = onSale;
        }
    }
}