using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class CountModel
    {
        public int MsAll { get; set; }
        public int MsDuplicates { get; set; }
        public int MsManuf { get; set; }
        public int MsGunType { get; set; }
        public int MsCaliber { get; set; }
        public int MsCapacity { get; set; }
        public int MsAction { get; set; }
        public int MsFinish { get; set; }
        public int MsModel { get; set; }
        public int MsDesc { get; set; }
        public int MsLgDesc { get; set; }
        public int MsBarrel { get; set; }
        public int MsOverall { get; set; }
        public int MsWeight { get; set; }
        public int MsImage { get; set; }
        public int CaLegal { get; set; }
        public int CaRoster { get; set; }
        public int CaSaRev { get; set; }
        public int CaSsPst { get; set; }
        public int CaCurio { get; set; }
        public int CaPvtPt { get; set; }

        public int ResultCount { get; set; }
    }
}