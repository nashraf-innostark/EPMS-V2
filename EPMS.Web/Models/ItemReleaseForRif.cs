using System.Collections.Generic;

namespace EPMS.Web.Models
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