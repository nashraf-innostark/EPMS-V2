using System;

namespace EPMS.Models.DomainModels
{
    public class ItemReleaseStatus
    {
        public long ItemReleaseId { get; set; }
        public string Notes { get; set; }
        public string NotesAr { get; set; }
        public short Status { get; set; }
        public string ManagerId { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
    }
}
