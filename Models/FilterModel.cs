using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class FilterModel
    {
        public bool IsMissingAll { get; set; }
        public bool IsMissingGunType { get; set; }
        public bool IsMissingCaliber { get; set; }
        public bool IsMissingCapacity { get; set; }
        public bool IsMissingAction { get; set; }
        public bool IsMissingFinish { get; set; }
        public bool IsMissingModel { get; set; }
        public bool IsMissingDesc { get; set; }
        public bool IsMissingImage { get; set; }
        public bool IsMissingLongDesc { get; set; }
        public bool IsMissingBrlLen { get; set; }
        public bool IsMissingOvrLen { get; set; }
        public bool IsMissingWeight { get; set; }
        public bool IsCaAwRestricted { get; set; }
        public bool IsHidden { get; set; }
        public bool IsCurrentModel { get; set; }
        public bool IsMissingOnly { get; set; }
        public bool IsOnDataFeed{ get; set; }
        public bool IsLeo { get; set; }

        public bool IsSsi { get; set; }
        public bool IsWss { get; set; }
        public bool IsLip { get; set; }
        public bool IsDav { get; set; }
        public bool IsRsr { get; set; }
        public bool IsBhc { get; set; }
        public bool IsGrn { get; set; }
        public bool IsZan { get; set; }
        public bool IsMge { get; set; }

        public int DaysBackToSearch { get; set; }
        public int PagingStartRow { get; set; }
        public int PagingMaxRows { get; set; }
        public int TotalRowCount { get; set; }
        public int PagingRowCount { get; set; }

        public DateTime DateAdded { get; set; }
    }
}