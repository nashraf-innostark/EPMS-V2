using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class Customer
    {
        public long CustomerId { get; set; }
        [Required(ErrorMessage = "Customer Name is required.")]
        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters.")]
        public string CustomerNameE { get; set; }
        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters.")]
        public string CustomerNameA { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string CustomerAddress { get; set; }
        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters.")]
        public string CustomerMobile { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public long OrdersCount { get; set; }
        public string LatestComplaint { get; set; }
        public string LatestOrder { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string UserId { get; set; }
        
        //For All Customers Report
        public long ComplaintsCount { get; set; }

        //public IEnumerable<Order> Orders { get; set; }
    }
}