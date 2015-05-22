using System.Collections.Generic;

namespace EPMS.Web.ViewModels.InventoryItem
{
    public class InventoryItemViewModel
    {
        public InventoryItemViewModel()
        {
            InventoryItem = new Models.InventoryItem();
        }
        public Models.InventoryItem InventoryItem { get; set; }
        public IEnumerable<Models.InventoryItem> InventoryItems { get; set; }

    }
}