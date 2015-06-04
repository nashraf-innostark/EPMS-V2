using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace EPMS.Web.ViewModels.InventoryItem
{
    public class InventoryItemViewModel
    {
        public InventoryItemViewModel()
        {
            InventoryItem = new Models.InventoryItem();
            ItemVariations = new List<Models.ItemVariation>();
        }
        public Models.InventoryItem InventoryItem { get; set; }
        public IEnumerable<Models.InventoryItem> InventoryItems { get; set; }
        public IList<Models.ItemVariation> ItemVariations { get; set; }

    }
}