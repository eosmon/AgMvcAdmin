using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class CampaignModel
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public int CampaignId { get; set; }
        public int ShowDelay { get; set; }
        public int PositionId { get; set; }
        public int BannerCount { get; set; }

        public string CampaignName { get; set; }

        public bool HasCampaign { get; set; }
        public bool IsAvailAll { get; set; }
        public bool IsAvailCurrent { get; set; }

        public List<ContentBanner> AllBanners { get; set; }
        public List<ContentBanner> CurrentBanners { get; set; }


        public CampaignModel(){}

        public CampaignModel(int pgId, int cpId, int psId)
        {
            PageId = pgId;
            CampaignId = cpId;
            PositionId = psId;
        }

        public CampaignModel(int cmpId, int delay, string name)
        {
            CampaignId = cmpId;
            ShowDelay = delay;
            CampaignName = name;
        }

        public CampaignModel(bool hasCmp, int id, int posId, int ct, int delay, string name)
        {
            HasCampaign = hasCmp;
            Id = id;
            PositionId = posId;
            BannerCount = ct;
            ShowDelay = delay;
            CampaignName = name;
        }
    }
}