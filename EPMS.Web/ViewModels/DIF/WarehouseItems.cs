using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.Web.ViewModels.DIF
{
    public class WarehouseItems
    {
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownListItems { get; set; }
        public string InventoryDepartments { get; set; }
    }
}