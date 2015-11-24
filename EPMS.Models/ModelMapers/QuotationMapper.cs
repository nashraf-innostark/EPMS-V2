using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class QuotationMapper
    {
        public static Quotation UpdateDbDataFromClient(this Quotation destination, Quotation source)
        {
            destination.QuotationId = source.QuotationId;
            destination.CustomerId = source.CustomerId;
            destination.RFQId = source.RFQId;
            destination.GreetingsEn = source.GreetingsEn;
            destination.GreetingsAr = source.GreetingsAr;
            destination.QuotationDiscount = source.QuotationDiscount;
            destination.FirstInstallement = source.FirstInstallement;
            destination.FirstInsDueAtCompletion = source.FirstInsDueAtCompletion;
            destination.SecondInstallment = source.SecondInstallment;
            destination.SecondInsDueAtCompletion = source.SecondInsDueAtCompletion;
            destination.ThirdInstallment = source.ThirdInstallment;
            destination.ThirdInsDueAtCompletion = source.ThirdInsDueAtCompletion;
            destination.FourthInstallment = source.FourthInstallment;
            destination.FourthInsDueAtCompletion = source.FourthInsDueAtCompletion;
            destination.NotesEn = source.NotesEn;
            destination.NotesAr = source.NotesAr;
            destination.RecLastUpdatedBy = source.RecLastUpdatedBy;
            destination.RecLastUpdatedDate = source.RecLastUpdatedDate;
            return destination;
        }

        public static QuotationItemDetail UpdateDbDataFromClient(this QuotationItemDetail destination, QuotationItemDetail source)
        {
            destination.ItemId = source.ItemId;
            destination.QuotationId = source.QuotationId;
            destination.ItemVariationId = source.ItemVariationId;
            destination.IsItemSKU = source.IsItemSKU;
            destination.IsItemDescription = source.IsItemDescription;
            destination.ItemDetails = source.ItemDetails;
            destination.ItemQuantity = source.ItemQuantity;
            destination.UnitPrice = source.UnitPrice;
            destination.TotalPrice = source.TotalPrice;
            destination.RecLastUpdatedBy = source.RecLastUpdatedBy;
            destination.RecLastUpdatedDate = source.RecLastUpdatedDate;
            return destination;
        }
    }
}
