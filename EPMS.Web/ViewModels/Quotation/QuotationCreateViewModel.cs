﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Quotation
{
    public class QuotationCreateViewModel
    {
        public QuotationCreateViewModel()
        {
            QuotationItemDetails = new List<QuotationItemDetail>();
        }
        public long QuotationId { get; set; }
        public string ClientName { get; set; }
        [Required(ErrorMessage = "Order Number is required.")]
        public long OrderId { get; set; }
        public long CreatedByEmployee { get; set; }
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
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecUpdatedDt { get; set; }
        public string RecUpdatedBy { get; set; }
        [Required(ErrorMessage = "Client Name is required.")]
        public long CustomerId { get; set; }
        public string CreatedByName { get; set; }
        public int OldItemDetailsCount { get; set; }

        public IList<QuotationItemDetail> QuotationItemDetails { get; set; }
        public string BtnText { get; set; }
        public string PageTitle { get; set; }
    }
}