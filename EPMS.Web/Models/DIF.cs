using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class DIF
    {
        public long Id { get; set; }
        [Required]
        [Display(Name = "Returning Reason")]
        public string DefectivenessE { get; set; }
        public string DefectivenessA { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecCreatedDateString { get; set; }
        public string RecUpdatedBy { get; set; }
        public System.DateTime RecUpdatedDate { get; set; }
        public string NotesA { get; set; }
        public string NotesE { get; set; }
        public int Status { get; set; }
        public string ManagerId { get; set; }
        public string RequesterName { get; set; }
        public string ManagerName { get; set; }
    }
}