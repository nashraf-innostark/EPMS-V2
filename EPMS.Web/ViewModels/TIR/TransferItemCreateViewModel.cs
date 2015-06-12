using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.Web.ViewModels.TIR
{
    public class TransferItemCreateViewModel
    {
        public TransferItemCreateViewModel()
        {
            Tir = new Models.TIR();
            TirItems = new List<Models.TIRItem>();
        }
        public Models.TIR Tir { get; set; }
        public IList<Models.TIRItem> TirItems { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}