using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class QuotationResponse
    {
        public IEnumerable<Quotation> Quotations { get; set; }
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
