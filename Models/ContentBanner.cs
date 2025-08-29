using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class ContentBanner
    {
        public int Id { get; set; }
        public int BannerId { get; set; }
        public int PageId { get; set; }
        public int CampaignId { get; set; }
        public int TimeDelay { get; set; }
        public int SortId { get; set; }

        public string ItemDesc { get; set; }
        public string ImageUrl { get; set; }
        public string NavToUrl { get; set; }

        public bool HasBanner { get; set; }
        public bool NewWindow { get; set; }

        public ContentBanner(){}

        public ContentBanner(int id, string des, string nav, bool newWin)
        {
            BannerId = id;
            ItemDesc = des;
            NavToUrl = nav;
            NewWindow = newWin;
        }

        public ContentBanner(int id, string des, string img, string nav)
        {
            BannerId = id;
            ItemDesc = des;
            ImageUrl = img;
            NavToUrl = nav;
        }


        public ContentBanner(int id, string des, string img, string nav, bool newWin)
        {
            BannerId = id;
            ItemDesc = des;
            ImageUrl = img;
            NavToUrl = nav;
            NewWindow = newWin;
        }


        public ContentBanner(int id, int brId, int sort, string des, string img, string nav)
        {
            Id = id;
            BannerId = brId;
            SortId = sort;
            ItemDesc = des;
            ImageUrl = img;
            NavToUrl = nav;
        }


        public ContentBanner(bool isBr, int id, int bSt, string des, string img, string nav)
        {
            HasBanner = isBr;
            BannerId = id;
            SortId = bSt;
            ItemDesc = des;
            ImageUrl = img;
            NavToUrl = nav;
        }

        public ContentBanner(int cpId, int delay, int pgId, int sort, string des, string nav)
        {
            CampaignId = cpId;
            TimeDelay = delay;
            PageId = pgId;
            SortId = sort;
            ItemDesc = des;
            NavToUrl = nav;
        }

    }
 
}