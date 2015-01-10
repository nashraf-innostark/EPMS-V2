using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class Customer
    {
        public long CustomerId { get; set; }
        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public long OrdersCount { get; set; }

    }
}