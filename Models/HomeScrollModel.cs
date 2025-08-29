using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class HomeScrollModel
    {
        public bool IsScrollActive { get; set; }
        public List<ManufModel> Manuf { get; set; }
    }
}