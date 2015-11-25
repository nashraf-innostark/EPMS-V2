using System;
using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.WebModels.ViewModels.Quotation;

namespace EPMS.WebModels.ModelMappers
{
    public static class QuotationMapper
    {
        public static Quotation CreateFromClientToServer(this QuotationCreateViewModel source)
        {
            var caseType = new Quotation
            {
                QuotationId = source.QuotationId,
                CustomerId = source.CustomerId,
                RFQId = source.RFQId,
                SerialNumber = source.SerialNumber,
                GreetingsEn = source.GreetingsEn,
                GreetingsAr = source.GreetingsAr,
                QuotationDiscount = source.QuotationDiscount,
                FirstInstallement = source.FirstInstallement,
                FirstInsDueAtCompletion = source.FirstInsDueAtCompletion,
                SecondInstallment = source.SecondInstallment,
                SecondInsDueAtCompletion = source.SecondInsDueAtCompletion,
                ThirdInstallment = source.ThirdInstallment,
                ThirdInsDueAtCompletion = source.ThirdInsDueAtCompletion,
                FourthInstallment = source.FourthInstallment,
                FourthInsDueAtCompletion = source.FourthInsDueAtCompletion,
                NotesEn = source.NotesEn,
                NotesAr = source.NotesAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
            if (source.QuotationItemDetails.Any())
            {
                caseType.QuotationItemDetails =
                    source.QuotationItemDetails.Select(x => x.CreateFromClientToServer()).ToList();
            }
            return caseType;
        }

        public static QuotationCreateViewModel CreateFromServerToClient(this Quotation source)
        {
            return new QuotationCreateViewModel
            {
                QuotationId = source.QuotationId,
                CustomerId = source.CustomerId,
                RFQId = source.RFQId,
                SerialNumber = source.SerialNumber,
                GreetingsEn = source.GreetingsEn,
                GreetingsAr = source.GreetingsAr,
                QuotationDiscount = source.QuotationDiscount,
                FirstInstallement = source.FirstInstallement,
                FirstInsDueAtCompletion = source.FirstInsDueAtCompletion,
                SecondInstallment = source.SecondInstallment,
                SecondInsDueAtCompletion = source.SecondInsDueAtCompletion,
                ThirdInstallment = source.ThirdInstallment,
                ThirdInsDueAtCompletion = source.ThirdInsDueAtCompletion,
                FourthInstallment = source.FourthInstallment,
                FourthInsDueAtCompletion = source.FourthInsDueAtCompletion,
                NotesEn = source.NotesEn,
                NotesAr = source.NotesAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                QuotationItemDetails = source.QuotationItemDetails.Select(x => x.CreateFromServerToClient()).ToList(),
            };
        }

        public static WebsiteModels.Quotation CreateFromServerToClientDdl(this Quotation source)
        {
            return new WebsiteModels.Quotation
            {
                QuotationId = source.QuotationId,
                CustomerId = source.CustomerId,
                Customers = source.Customer.CreateFromServerToClient(),
                GreetingsEn = source.GreetingsEn,
                GreetingsAr = source.GreetingsAr,
            };
        }

        public static WebsiteModels.Quotation CreateFromServerToClientLv(this Quotation source)
        {
            return new WebsiteModels.Quotation
            {
                QuotationId = source.QuotationId,
                CustomerId = source.CustomerId,
                RFQId = source.RFQId,
                SerialNumber = source.SerialNumber,
                Customers = source.Customer.CreateFromServerToClient(),
                RecCreatedDate = source.RecCreatedDate,
                GreetingsEn = source.GreetingsEn,
                GreetingsAr = source.GreetingsAr,
                FirstInstallement = source.FirstInstallement,
                FirstInsDueAtCompletion = source.FirstInsDueAtCompletion,
                SecondInstallment = source.SecondInstallment,
                SecondInsDueAtCompletion = source.SecondInsDueAtCompletion,
                ThirdInstallment = source.ThirdInstallment,
                ThirdInsDueAtCompletion = source.ThirdInsDueAtCompletion,
                FourthInstallment = source.FourthInstallment,
                FourthInsDueAtCompletion = source.FourthInsDueAtCompletion,
                NotesEn = source.NotesEn,
                NotesAr = source.NotesAr,
                QuotationItemDetails = source.QuotationItemDetails.Select(x => x.CreateFromServerToClient()),
                QuotationDiscount = source.QuotationDiscount,
                ClientNameEn = source.Customer != null ? source.Customer.CustomerNameE : "",
                ClientNameAr = source.Customer != null ? source.Customer.CustomerNameA : "",
            };
        }
        public static WebsiteModels.Quotation CreateFromServerToClientForProject(this Quotation source)
        {
            return new WebsiteModels.Quotation
            {
                QuotationId = source.QuotationId,
                ClientNameEn = source.Customer.CustomerNameE,
                ClientNameAr = source.Customer.CustomerNameA,
                CustomerId = source.CustomerId,
                RFQId = source.RFQId,
                SerialNumber = source.SerialNumber,
                GreetingsEn = source.GreetingsEn,
                GreetingsAr = source.GreetingsAr,
            };
        }

        public static QuotationItemDetail CreateFromClientToServer(this WebsiteModels.QuotationItemDetail source)
        {
            var caseType = new QuotationItemDetail
            {
                ItemId = source.ItemId,
                QuotationId = source.QuotationId,
                ItemVariationId = source.ItemVariationId,
                IsItemSKU = source.IsItemSKU,
                IsItemDescription = source.IsItemDescription,
                ItemDetails = source.ItemDetails,
                ItemQuantity = source.ItemQuantity,
                UnitPrice = source.UnitPrice,
                TotalPrice = source.TotalPrice,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
            return caseType;
        }

        public static WebsiteModels.QuotationItemDetail CreateFromServerToClient(this QuotationItemDetail source)
        {
            return new WebsiteModels.QuotationItemDetail
            {
                ItemId = source.ItemId,
                QuotationId = source.QuotationId,
                ItemVariationId = source.ItemVariationId,
                IsItemSKU = source.IsItemSKU,
                IsItemDescription = source.IsItemDescription,
                ItemDetails = source.ItemDetails,
                ItemQuantity = source.ItemQuantity,
                UnitPrice = source.UnitPrice,
                TotalPrice = source.TotalPrice,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
        }

        public static WebsiteModels.Quotation CreateForInvoice(this Quotation source)
        {
            decimal discountpercentage = (decimal) (source.QuotationDiscount / Convert.ToDecimal(100));
            decimal discountAmount = discountpercentage * source.QuotationItemDetails.Sum(x => x.TotalPrice);
            decimal discount = source.QuotationItemDetails.Sum(x => x.TotalPrice) - discountAmount;
            return new WebsiteModels.Quotation
            {
                QuotationId = source.QuotationId,
                CustomerId = source.CustomerId,
                RFQId = source.RFQId,
                SerialNumber = source.SerialNumber,
                GreetingsEn = source.GreetingsEn,
                GreetingsAr = source.GreetingsAr,
                QuotationDiscount = source.QuotationDiscount,
                FirstInstallement = source.FirstInstallement,
                FirstInsDueAtCompletion = source.FirstInsDueAtCompletion,
                SecondInstallment = source.SecondInstallment,
                SecondInsDueAtCompletion = source.SecondInsDueAtCompletion,
                ThirdInstallment = source.ThirdInstallment,
                ThirdInsDueAtCompletion = source.ThirdInsDueAtCompletion,
                FourthInstallment = source.FourthInstallment,
                FourthInsDueAtCompletion = source.FourthInsDueAtCompletion,
                NotesEn = source.NotesEn,
                NotesAr = source.NotesAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                QuotationItemDetails = source.QuotationItemDetails.Select(x => x.CreateFromServerToClient()).ToList(),
                ClientNameEn = source.Customer.CustomerNameE,
                ClientNameAr = source.Customer.CustomerNameA,
                CustomerAddress = source.Customer.CustomerAddress,
                CustomerPhone = source.Customer.CustomerMobile,
                CustomerEmail = source.Customer.AspNetUsers.First().Email,
                SubTotal = source.QuotationItemDetails.Sum(x=>x.TotalPrice),
                GrandTotal = discount,

                FirstInstallmentStatus = source.FirstInstallmentStatus ? "Paid" : "Unpaid",
                SecondInstallmentStatus = source.SecondInstallmentStatus ? "Paid" : "Unpaid",
                ThirdInstallmentStatus = source.ThirdInstallmentStatus ? "Paid" : "Unpaid",
                FourthInstallmentStatus = source.FourthInstallmentStatus ? "Paid" : "Unpaid",

                IsFirstInstallmentStatus = source.FirstInstallmentStatus,
                IsSecondInstallmentStatus = source.SecondInstallmentStatus,
                IsThirdInstallmentStatus = source.ThirdInstallmentStatus,
                IsFourthInstallmentStatus = source.FourthInstallmentStatus
            };
        }

    }
}