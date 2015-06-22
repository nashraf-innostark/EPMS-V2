using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class ItemReleaseQuantityHistory
    {
        public long ItemReleaseQuantityId { get; set; }
        public long ItemReleaseId { get; set; }
        public long ItemVariationId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }

        public virtual ItemReleaseHistory ItemReleaseHistory { get; set; }
        public virtual ItemVariation ItemVariation { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
