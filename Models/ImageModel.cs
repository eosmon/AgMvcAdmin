using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class ImageModel
    {
        public int PicId { get; set; }
        public int MasterId { get; set; }
        public int ImageDist { get; set; }
        public int ManufId { get; set; }
        public int GunTypeId { get; set; }
        public int CaliberId { get; set; }
        public string ManufName { get; set; }
        public string UpcCode { get; set; }
        public string MfgPartNumber { get; set; }
        public string ImgName { get; set; }
        public string ItemDesc { get; set; }
        public string ImgSsi { get; set; }
        public string ImgWss { get; set; }
        public string ImgLip { get; set; }
        public string ImgDav { get; set; }
        public string ImgRsr { get; set; }
        public string ImgBhc { get; set; }
        public string ImgGrn { get; set; }
        public string ImgZan { get; set; }
        public string ImgMge { get; set; }
        public string ImgHse { get; set; }
        public string ImgAlt { get; set; }
        public string ImgCur { get; set; }
        public string ImgHse1 { get; set; }
        public string ImgHse2 { get; set; }
        public string ImgHse3 { get; set; }
        public string ImgHse4 { get; set; }
        public string ImgHse5 { get; set; }
        public string ImgHse6 { get; set; }
        public string Io1 { get; set; }
        public string Io2 { get; set; }
        public string Io3 { get; set; }
        public string Io4 { get; set; }
        public string Io5 { get; set; }
        public string Io6 { get; set; }
        public string DistCode { get; set; }
        public bool IsSsi { get; set; }
        public bool IsWss { get; set; }
        public bool IsLip { get; set; }
        public bool IsDav { get; set; }
        public bool IsRsr { get; set; }
        public bool IsBhc { get; set; }
        public bool IsGrn { get; set; }
        public bool IsZan { get; set; }
        public bool IsMge { get; set; }
        public bool IsHse { get; set; }
        public bool IsAlt { get; set; }
        public bool IsCur { get; set; }
        public bool IsHsePath { get; set; }
        public FilterModel Filters { get; set; }


        public ImageModel() {}

        public ImageModel(string img1)
        {
            ImgHse1 = img1;
        }

        public ImageModel(int id, string img1, string img2, string img3, string img4, string img5, string img6)
        {
            PicId = id;
            ImgHse1 = img1;
            ImgHse2 = img2;
            ImgHse3 = img3;
            ImgHse4 = img4;
            ImgHse5 = img5;
            ImgHse6 = img6;
        }


        public ImageModel(int id, string img1, string img2, string img3, string img4, string img5, string img6, string io1, string io2, string io3, string io4, string io5, string io6)
        {
            PicId = id;
            ImgHse1 = img1;
            ImgHse2 = img2;
            ImgHse3 = img3;
            ImgHse4 = img4;
            ImgHse5 = img5;
            ImgHse6 = img6;
            Io1 = io1;
            Io2 = io2;
            Io3 = io3;
            Io4 = io4;
            Io5 = io5;
            Io6 = io6;
        }

        public ImageModel(int id, string img1, string img2, string img3, string img4, string img5, string img6, string io1, string io2, string io3, string io4, string io5, string io6, string dc)
        {
            PicId = id;
            ImgHse1 = img1;
            ImgHse2 = img2;
            ImgHse3 = img3;
            ImgHse4 = img4;
            ImgHse5 = img5;
            ImgHse6 = img6;
            Io1 = io1;
            Io2 = io2;
            Io3 = io3;
            Io4 = io4;
            Io5 = io5;
            Io6 = io6;
            DistCode = dc;
        }



        

        public ImageModel(int mstId, int imgDs, string mfgName, string upc, string mpn, string desc, string imgSsi, string imgWss, string imgLip, string imgDav, string imgRsr, string imgBhc, 
                          string imgZan, string imgMge, string imgHse, string imgAlt, string imgCur, bool isSsi, bool isWss, bool isLip,
                          bool isDav, bool isRsr, bool isBhc, bool isZan, bool isMge, bool isHse, bool isAlt, bool isCur, bool isHp, FilterModel fil)
        {
            MasterId = mstId;
            ImageDist = imgDs;
            ManufName = mfgName;
            UpcCode = upc;
            MfgPartNumber = mpn;
            ItemDesc = desc;
            ImgSsi = imgSsi;
            ImgWss = imgWss;
            ImgLip = imgLip;
            ImgDav = imgDav;
            ImgRsr = imgRsr;
            ImgBhc = imgBhc;
            ImgZan = imgZan;
            ImgMge = imgMge;
            ImgHse = imgHse;
            ImgAlt = imgAlt;
            ImgCur = imgCur;
            IsSsi = isSsi;
            IsWss = isWss;
            IsLip = isLip;
            IsDav = isDav;
            IsRsr = isRsr;
            IsBhc = isBhc;
            IsZan = isZan;
            IsMge = isMge;
            IsHse = isHse;
            IsAlt = isAlt;
            IsCur = isCur;
            IsHsePath = isHp;
            Filters = fil;
        }
 
    }


}