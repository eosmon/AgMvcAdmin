using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class ContentToolTip
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public int ContentCategoryId { get; set; }

        public string TipDesc { get; set; }
        public string ToolTipTxt { get; set; }

        public bool HasToolTip { get; set; }


        public ContentToolTip(){}

        public ContentToolTip(bool hasTt, int id, string desc, string txt)
        {
            HasToolTip = hasTt;
            Id = id;
            TipDesc = desc;
            ToolTipTxt = txt;
        }
    }
}