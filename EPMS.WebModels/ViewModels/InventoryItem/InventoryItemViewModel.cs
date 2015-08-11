using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.InventoryItem
{
    public class InventoryItemViewModel
    {
        public InventoryItemViewModel()
        {
            InventoryItem = new WebsiteModels.InventoryItem();
            ItemVariations = new List<WebsiteModels.ItemVariation>();
        }
        public WebsiteModels.InventoryItem InventoryItem { get; set; }
        public IEnumerable<WebsiteModels.InventoryItem> InventoryItems { get; set; }
        public IList<WebsiteModels.ItemVariation> ItemVariations { get; set; }

    }
}