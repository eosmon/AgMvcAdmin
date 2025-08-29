using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models
{
    public class ContentModel
    {
        public int Id { get; set; }
        public int StaticId { get; set; }
        public string ToolTipDesc { get; set; }
        public string ToolTipText { get; set; }

        public string PageName { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoOgType { get; set; }
        public string SeoDesc { get; set; }

        public string HeaderTitle { get; set; }
        public string HeaderTxt{ get; set; }
        public string HeaderImg { get; set; }

        public string StaticTxt { get; set; }

        public List<ContentToolTip> ToolTips { get; set; }
        public List<ContentBanner> Banners { get; set; }
        public List<CampaignModel> Campaigns { get; set; }
        public List<HomeModelConfig> Home { get; set; }

        public List<SelectListItem> AdOptions { get; set; }
        public List<SelectListItem> BgColors { get; set; }
        public List<SelectListItem> FtrSizes { get; set; }

        public bool HasHeader { get; set; }
        public bool HasStatic { get; set; }
        public bool IsHomePage { get; set; }
 


        public ContentModel(){}


        public ContentModel(int id, string ttd)
        {
            Id = id;
            ToolTipDesc = ttd;
        }

        public ContentModel(int id, string htl, string htx)
        {
            Id = id;
            HeaderTitle = htl;
            HeaderTxt = htx;
        }

        public ContentModel(int id, int stId, bool isHdr, bool isStat, string tPgNm, string seoTtl, string tSeoKw, string tSeoOg, string tSeoDesc, string tTitle, string tTxt, string tImg, string tStTxt)
        {
            Id = id;
            StaticId = stId;
            HasHeader = isHdr;
            HasStatic = isStat;
            PageName = tPgNm;
            SeoTitle = seoTtl;
            SeoKeywords = tSeoKw;
            SeoOgType = tSeoOg;
            SeoDesc = tSeoDesc;
            HeaderTitle = tTitle;
            HeaderTxt = tTxt;
            HeaderImg = tImg;
            StaticTxt = tStTxt;
        }


        public ContentModel(int id, string name, string ttl, string kw, string og, string dsc)
        {
            Id = id;
            PageName = name;
            SeoTitle = ttl;
            SeoKeywords = kw;
            SeoOgType = og;
            SeoDesc = dsc;
        }



    }
}