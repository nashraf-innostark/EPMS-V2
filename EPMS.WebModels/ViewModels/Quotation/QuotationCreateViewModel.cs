using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPMS.Models.ResponseModels;

namespace EPMS.WebModels.ViewModels.Quotation
{
    public class QuotationCreateViewModel
    {
        public QuotationCreateViewModel()
        {
            QuotationItemDetails = new List<WebsiteModels.QuotationItemDetail>();
        }
        public long QuotationId { get; set; }
        public long? RFQId { get; set; }
        public string GreetingsEn { get; set; }
        public string GreetingsAr { get; set; }
        [Range(0, 100, ErrorMessageResourceType = typeof(Resources.CMS.Quotation), ErrorMessageResourceName = "DiscountRangeError")]
        public short? QuotationDiscount { get; set; }
        [Required(ErrorMessageResourceType = typeof (Resources.CMS.Quotation), ErrorMessageResourceName = "FirstInstallmentAmountError")]
        public decimal FirstInstallement { get; set; }
        [Required(ErrorMessageResourceType = typeof (Resources.CMS.Quotation), ErrorMessageResourceName = "FirstInsDueAtCompletionError")]
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
        public int OldItemDetailsCount { get; set; }
        public string CreatedByName { get; set; }
        public short Status { get; set; }
        public bool FirstInstallmentStatus { get; set; }
        public bool SecondInstallmentStatus { get; set; }
        public bool ThirdInstallmentStatus { get; set; }
        public bool FourthInstallmentStatus { get; set; }

        public IList<WebsiteModels.QuotationItemDetail> QuotationItemDetails { get; set; }
        // items for inventory pop up
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}