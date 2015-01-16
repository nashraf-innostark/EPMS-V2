using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Quotation
    {
        public long QuotationId { get; set; }
        public string ClientName { get; set; }
        public string OrderNumber { get; set; }
        public long CreatedByEmployee { get; set; }
        public string GreetingsEn { get; set; }
        public string GreetingsAr { get; set; }
        public short? QuotationDiscount { get; set; }
        public decimal FirstInstallement { get; set; }
        public short FirstInsDueAtCompletion { get; set; }
        public decimal? SecondInstallment { get; set; }
        public short? SecondInsDueAtCompletion { get; set; }
        public decimal? ThirdInstallment { get; set; }
        public short? ThirdInsDueAtCompletion { get; set; }
        public decimal? FourthInstallment { get; set; }
        public short? FourthInsDueAtCompletion { get; set; }
        public string NotesEn { get; set; }
        public string NotesAr { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecUpdatedDt { get; set; }
        public string RecUpdatedBy { get; set; }
        public long CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<QuotationItemDetail> QuotationItemDetails { get; set; }
    }
}
