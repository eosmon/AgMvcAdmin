using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class SearchItem
    {
        public int Id { get; set; }
        public int InStockId { get; set; }
        public int MasterId { get; set; }
        public int ManufId { get; set; }
        public string ImageUrl { get; set; }
        public string ItemDesc { get; set; }
        public string StrPrice { get; set; }
        public bool IsOnWeb { get; set; }
        public bool IsHse { get; set; }
        public bool IsWyo { get; set; }
        public double AskingPrice { get; set; }
        public List<SourceItem> Sources { get; set; }


        public SearchItem() { }

        public SearchItem(int id, string desc, string img, string prc, bool iow, List<SourceItem> src) {
            Id = id;
            ItemDesc = desc;
            ImageUrl = img;
            StrPrice = prc;
            IsOnWeb = iow;
            Sources = src;
        }

        public SearchItem(int isi, int mid, string desc, string img, string prc, bool iow, List<SourceItem> src)
        {
            InStockId = isi;
            MasterId = mid;
            ItemDesc = desc;
            ImageUrl = img;
            StrPrice = prc;
            IsOnWeb = iow;
            Sources = src;
        }

    }
}