using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class WebsiteCustomer
    {
        public long CustomerId { get; set; }
        [Required(ErrorMessage = "Your Name is required")]
        public string CustomerNameEn { get; set; }
        public string CustomerNameAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime? RecUpdatedDate { get; set; }
    }
}
