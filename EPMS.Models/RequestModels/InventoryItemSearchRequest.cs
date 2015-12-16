using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class InventoryItemSearchRequest : GetPagedListRequest
    {
        public InventoryItemByColumn InventoryItemByColumn
        {
            get
            {
                return (InventoryItemByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
