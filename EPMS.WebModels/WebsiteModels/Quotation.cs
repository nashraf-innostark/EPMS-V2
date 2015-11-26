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
        [Range(0, 100, ErrorMessageResourceType = typeof (Resources.CMS.Quotation), ErrorMessageResourceName = "DiscountRangeError")]
        public short? QuotationDiscount { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.CMS.Quotation), ErrorMessageResourceName = "FirstInstallmentAmountError")]
        public decimal FirstInstallement { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.CMS.Quotation), ErrorMessageResourceName = "FirstInsDueAtCompletionError")]
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
        public long CustomerId { get; set; }
        public string SerialNumber { get; set; }
        public string ClientNameEn { get; set; }
        public string ClientNameAr { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public double GrandAmount { get; set; }
        public string FirstInstallmentStatus { get; set; }
        public string SecondInstallmentStatus { get; set; }
        public string ThirdInstallmentStatus { get; set; }
        public string FourthInstallmentStatus { get; set; }
        public bool IsFirstInstallmentStatus { get; set; }
        public bool IsSecondInstallmentStatus { get; set; }
        public bool IsThirdInstallmentStatus { get; set; }
        public bool IsFourthInstallmentStatus { get; set; }
        public short Status { get; set; }

        public IEnumerable<QuotationItemDetail> QuotationItemDetails { get; set; }
        public Customer Customers { get; set; }
    }
}