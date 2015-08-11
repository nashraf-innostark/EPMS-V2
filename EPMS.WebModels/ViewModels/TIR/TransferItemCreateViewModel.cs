using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.WebModels.ViewModels.TIR
{
    public class TransferItemCreateViewModel
    {
        public TransferItemCreateViewModel()
        {
            Tir = new WebModels.WebsiteModels.TIR();
            TirItems = new List<WebModels.WebsiteModels.TIRItem>();
        }
        public WebModels.WebsiteModels.TIR Tir { get; set; }
        public IList<WebModels.WebsiteModels.TIRItem> TirItems { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}