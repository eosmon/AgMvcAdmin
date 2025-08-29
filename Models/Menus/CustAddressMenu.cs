using System.Collections.Generic;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class CustAddressMenu
    {
        public List<SelectListItem> CustAddresses { get; set; }
        public int? CustAddrId { get; set; }
        public string CustAddrName { get; set; }
 
    }
}