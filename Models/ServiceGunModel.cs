using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgMvcAdmin.Common;

namespace AgMvcAdmin.Models
{
    public class ServiceGunModel
    {
        public int Id { get; set; }
        public string UserKey { get; set; }
        public long InquiryNumber { get; set; }
        public int ServiceTypeId { get; set; }
        public int ManufId { get; set; }
        public int GunTypeId { get; set; }
        public int CaliberId { get; set; }
        public int FinishId { get; set; }
        public int ConditionId { get; set; }
        public int BarrelIn { get; set; }
        public int CustomerId { get; set; }
        public bool IsService { get; set; }
        public bool IsCurio { get; set; }
        public bool OriginalBox { get; set; }
        public bool OriginalPaperwork { get; set; }
        public double BarrelDec { get; set; }
        public string TxtManuf { get; set; }
        public string TxtCaliber { get; set; }
        public string TxtFinish { get; set; }
        public string TxtCondition { get; set; }
        public string TxtGunType { get; set; }
        public string TxtDescription { get; set; }
        public string TxtBarrel { get; set; }
        public string ModelName { get; set; }
        public string SerialNumber { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string Image6 { get; set; }
        public PhotoUpload UploadType { get; set; }
        public HttpFileCollectionBase Files { get; set; }
        public string[] GroupIdArr { get; set; }

        public ServiceGunModel() { }

        public ServiceGunModel(string manuf, string model, string barrelLen, string caliber, string gunType, string finish, string cond, string serial, int barrelIn)
        {
            TxtManuf = manuf;
            ModelName = model;
            TxtBarrel = barrelLen;
            TxtCaliber = caliber;
            TxtGunType = gunType;
            TxtFinish = finish;
            TxtCondition = cond;
            SerialNumber = serial;
            BarrelIn = barrelIn;
        }

        public ServiceGunModel(int id, PhotoUpload uType, string uk, string imgName)
        {
            Id = id;
            UploadType = uType;
            UserKey = uk;
            Image1 = imgName;
        }


        public ServiceGunModel(int id, PhotoUpload uType, long inqNo, string uk, string imgName)
        {
            Id = id;
            UploadType = uType;
            InquiryNumber = inqNo;
            UserKey = uk;
            Image1 = imgName;
        }

        public ServiceGunModel(int id, long inqNo, string uk)
        {
            Id = id;
            InquiryNumber = inqNo;
            UserKey = uk;
        }

        public ServiceGunModel(HttpFileCollectionBase f, int id, string uk, long inqNo)
        {
            Files = f;
            Id = id;
            UserKey = uk;
            InquiryNumber = inqNo;
        }

        public ServiceGunModel(HttpFileCollectionBase f, int id, PhotoUpload uType, string uk, string imgName)
        {
            Files = f;
            Id = id;
            UploadType = uType;
            UserKey = uk;
            Image1 = imgName;
        }

        public ServiceGunModel(HttpFileCollectionBase f, int id, PhotoUpload uType, string uk, long inqNo)
        {
            Files = f;
            Id = id;
            UploadType = uType;
            UserKey = uk;
            InquiryNumber = inqNo;
        }

        public ServiceGunModel(HttpFileCollectionBase f, int id, string uk, long inqNo, string[] groupArr, bool isSvc)
        {
            Files = f;
            Id = id;
            UserKey = uk;
            InquiryNumber = inqNo;
            GroupIdArr = groupArr;
            IsService = isSvc;
        }

        public ServiceGunModel(HttpFileCollectionBase f, int id, string uk, bool isCurio, string[] groupArr, bool isSvc)
        {
            Files = f;
            Id = id;
            UserKey = uk;
            IsCurio = isCurio;
            GroupIdArr = groupArr;
            IsService = isSvc;
        }

        public ServiceGunModel(HttpFileCollectionBase f, int id, PhotoUpload uType, string uk, string imgName, string[] groupArr)
        {
            Files = f;
            Id = id;
            UploadType = uType;
            UserKey = uk;
            Image1 = imgName;
            GroupIdArr = groupArr;
        }
    }
}