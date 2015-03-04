using System;

namespace EPMS.Models.RequestModels
{
    public class JobOfferedSearchRequest : GetPagedListRequest
    {
        public long JobOfferedId { get; set; }
        public long JobTitleId { get; set; }
        public string JobDescription { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
    }
}
