using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Receipt
{
    public class ReceiptListViewModel
    {
        public ReceiptListViewModel()
        {
            Receipts = new List<WebsiteModels.Receipt>();
        }
        public IEnumerable<WebsiteModels.Receipt> Receipts { get; set; } 
    }
}
