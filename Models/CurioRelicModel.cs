using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class CurioRelicModel
    {
        public string CurioName { get; set; }
        public string CurioAddress { get; set; }
        public string CurioCity { get; set; }
        public int CurioStateId { get; set; }
        public int CurioZipCode { get; set; }
    }
}