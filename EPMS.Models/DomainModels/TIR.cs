using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class TIR
    {
        public long Id { get; set; }
        public string FormNumber { get; set; }
        public string DefectivenessE { get; set; }
        public string DefectivenessA { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public System.DateTime RecUpdatedDate { get; set; }
        public string NotesA { get; set; }
        public string NotesE { get; set; }
        public int Status { get; set; }
        public string ManagerId { get; set; }
        public long FromWarehouseId { get; set; }
        public long ToWarehouseId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser Manager { get; set; }
        public virtual ICollection<TIRItem> TIRItems { get; set; }
        public virtual Warehouse FromWarehouse { get; set; }
        public virtual Warehouse ToWarehouse { get; set; }
    }
}
