using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.InventoryItem
{
    public class InventoryItemViewModel
    {
        public InventoryItemViewModel()
        {
            InventoryItem = new WebsiteModels.InventoryItem();
            ItemVariations = new List<WebsiteModels.ItemVariation>();
        }

        public InventoryItemSearchRequest SearchRequest { get; set; }
        public WebsiteModels.InventoryItem InventoryItem { get; set; }
        public IEnumerable<WebsiteModels.InventoryItemForListView> aaData { get; set; }
        public IList<WebsiteModels.ItemVariation> ItemVariations { get; set; }
        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;
        public string sEcho;

    }
    public class InventoryItemListViewModel
    {
        public InventoryItemListViewModel()
        {
            aaData = new List<WebsiteModels.InventoryItemForListView>();
        }

        public InventoryItemSearchRequest SearchRequest { get; set; }
        public List<WebsiteModels.InventoryItemForListView> aaData { get; set; }
        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;
        public string sEcho;

    }
}