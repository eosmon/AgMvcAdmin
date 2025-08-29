using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models
{
    public class HomeModelConfig
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public int OptionId { get; set; }
        public int FeatureId { get; set; }
        public int FeatureSizeId { get; set; }
        public int PromoColorId { get; set; }
        public int GroupId { get; set; }
        public bool IsPromo { get; set; }
        public bool IsActive { get; set; }
        public bool IsHeader { get; set; }
        public string PromoText { get; set; }
        public string PromoBgColor { get; set; }

        public HomeModelConfig(){}

        /* FEATURE ADD & UPDATE */
        public HomeModelConfig(int grpId, int ftrId, int szId, int colId, bool isPromo, string prTxt)
        {
            GroupId = grpId;
            FeatureId = ftrId;
            FeatureSizeId = szId;
            PromoColorId = colId;
            IsPromo = isPromo;
            PromoText = prTxt;
        }

        /* HOME ADD */
        public HomeModelConfig(int posId, int optId, int ftrId, bool isAtv)
        {
            PositionId = posId;
            OptionId = optId;
            FeatureId = ftrId;
            IsActive = isAtv;
        }


        /* HOME UPDATE */
        public HomeModelConfig(int id, int posId, int optId, int ftrId, bool isAtv)
        {
            Id = id;
            PositionId = posId;
            OptionId = optId;
            FeatureId = ftrId;
            IsActive = isAtv;
        }

        /* UPDATE */
        public HomeModelConfig(int id, int posId, int optId, int ftrId, int szId, int colId, int grpId, bool isHeader, bool isPromo, bool isAtv, string prTxt)
        {
            Id = id;
            PositionId = posId;
            OptionId = optId;
            FeatureId = ftrId;
            FeatureSizeId = szId;
            PromoColorId = colId;
            GroupId = grpId;
            IsHeader = isHeader;
            IsPromo = isPromo;
            IsActive = isAtv;
            PromoText = prTxt;
        }
    }
}