using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class Aisle
    {
        public long AisleId { get; set; }
        public string AisleName { get; set; }
        public long WarehouseId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
