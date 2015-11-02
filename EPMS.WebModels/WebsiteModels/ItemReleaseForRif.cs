using System.Collections.Generic;

namespace EPMS.WebModels.WebsiteModels
{
    public class ItemReleaseForRif
    {
        public ItemReleaseForRif()
        {
            ItemReleaseDetails = new List<ItemReleaseDetail>();
        }
        public long ItemReleaseId { get; set; }
        public string FormNumber { get; set; }
        public IList<ItemReleaseDetail> ItemReleaseDetails { get; set; }
    }
}