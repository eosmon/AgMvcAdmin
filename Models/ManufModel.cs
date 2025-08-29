using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class ManufModel
    {
        public int ManufId { get; set; }
        public int ParentId { get; set; }
        public int DistId { get; set; }

        public bool IsOnSsi { get; set; }
        public bool IsOnWss { get; set; }
        public bool IsOnLip { get; set; }
        public bool IsOnDav { get; set; }
        public bool IsOnRsr { get; set; }
        public bool IsOnBhc { get; set; }
        public bool IsOnGrn { get; set; }
        public bool IsOnZan { get; set; }
        public bool IsOnMge { get; set; }

        public bool IsOnFeed { get; set; }
        public bool IsActive { get; set; }
        public bool IsScrollActive { get; set; }
        public bool IsFeedOnly { get; set; }
        public bool IsParentOnly { get; set; }
        public bool UpdateImg { get; set; }

        public string ScrollImgUrl { get; set; }
        public string ScrollBgColor { get; set; }
        public string ManufName { get; set; }
        public string ParentName { get; set; }
        public string ManufUrl { get; set; }
        public string MapCode { get; set; }

        public CountModel ItemCount { get; set; }



        public ManufModel() { }

        public ManufModel(CountModel ic)
        {
            ItemCount = ic;
        }


        public ManufModel(int mfgId, int parId, bool isSsi, bool isWss, bool isLip, bool isDav, bool isRsr,
            bool isBhc, bool isGrn, bool isZan, bool isMge, bool isOnFeed, bool isActive,
            string mfgName, string parName, string mfgUrl)
        {
            ManufId = mfgId;
            ParentId = parId;
            IsOnSsi = isSsi;
            IsOnWss = isWss;
            IsOnLip = isLip;
            IsOnDav = isDav;
            IsOnRsr = isRsr;
            IsOnBhc = isBhc;
            IsOnGrn = isGrn;
            IsOnZan = isZan;
            IsOnMge = isMge;
            IsOnFeed = isOnFeed;
            IsActive = isActive;
            ManufName = mfgName;
            ParentName = parName;
            ManufUrl = mfgUrl;
        }


        public ManufModel(int mfgId, bool active, string mfg, string scrollUrl, string bgColor, string mfgUrl)
        {
            ManufId = mfgId;
            IsScrollActive = active;
            ManufName = mfg;
            ScrollImgUrl = scrollUrl;
            ScrollBgColor = bgColor;
            ManufUrl = mfgUrl;
        }

        public ManufModel(int mfgId, bool active, bool updImg, string bgColor, string mfgUrl, string imgName)
        {
            ManufId = mfgId;
            IsActive = active;
            UpdateImg = updImg;
            ScrollBgColor = bgColor;
            ManufUrl = mfgUrl; 
            ScrollImgUrl = imgName;
           
        }

    }


}