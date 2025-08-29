using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class LeoModel
    {
        public int JurisdictionId { get; set; }
        public int DivisionId { get; set; }
        public string RegionName { get; set; }
        public string OfficerNumber { get; set; }

        public LeoModel() { }

        public LeoModel(int jur, int div, string region)
        {
            JurisdictionId = jur;
            DivisionId = div;
            RegionName = region;
        }
    }
}