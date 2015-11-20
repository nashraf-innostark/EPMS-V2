﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class Quotation
    {
        public long QuotationId { get; set; }
        public long? RFQId { get; set; }
        public string GreetingsEn { get; set; }
        public string GreetingsAr { get; set; }
        [Range(0, 100, ErrorMessage = "The total % of discount should not exceed 100")]
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
        public DateTime RecCreatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        [Required(ErrorMessage = "Client Name is required.")]
        public long CustomerId { get; set; }
        public string ClientNameEn { get; set; }
        public string ClientNameAr { get; set; }

        public IEnumerable<QuotationItemDetail> QuotationItemDetails { get; set; }
        public Customer Customers { get; set; }
    }
}