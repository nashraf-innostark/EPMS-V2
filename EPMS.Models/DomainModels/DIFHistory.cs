using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class DIFHistory
    {
        public long Id { get; set; }
        public string DefectivenessE { get; set; }
        public string DefectivenessA { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
        public string NotesA { get; set; }
        public string NotesE { get; set; }
        public int Status { get; set; }
        public string ManagerId { get; set; }
        public long ParentId { get; set; }
        public string FormNumber { get; set; }
        public long WarehouseId { get; set; }

        public virtual AspNetUser CreatedBy { get; set; }
        public virtual AspNetUser Manager { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<DIFItemHistory> DIFItemHistories { get; set; }
    }
}
