using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class ProductSearchRequest : GetPagedListRequest
    {
        public long Id { get; set; }
        public string From { get; set; }
        public int NoOfItems { get; set; }
        public int SortBy { get; set; }
        public string SortDirection { get; set; }


        public ProductByOption ProductByOption
        {
            get
            {
                return (ProductByOption)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
