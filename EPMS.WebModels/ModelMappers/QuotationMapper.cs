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
                FirstInstallmentStatus = source.FirstInstallmentStatus,
                SecondInstallmentStatus = source.SecondInstallmentStatus,
                ThirdInstallmentStatus = source.ThirdInstallmentStatus,
                FourthInstallmentStatus = source.FourthInstallmentStatus,
                NotesEn = source.NotesEn,
                NotesAr = source.NotesAr,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
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
                FirstInstallmentStatus = source.FirstInstallmentStatus,
                SecondInstallmentStatus = source.SecondInstallmentStatus,
                ThirdInstallmentStatus = source.ThirdInstallmentStatus,
                FourthInstallmentStatus = source.FourthInstallmentStatus,
                NotesEn = source.NotesEn,
                NotesAr = source.NotesAr,
                Status = source.Status,
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
                IsFirstInstallmentStatus = source.FirstInstallmentStatus,
                IsSecondInstallmentStatus = source.SecondInstallmentStatus,
                IsThirdInstallmentStatus = source.ThirdInstallmentStatus,
                IsFourthInstallmentStatus = source.FourthInstallmentStatus,
                NotesEn = source.NotesEn,
                NotesAr = source.NotesAr,
                Status = source.Status,
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

        public static WebsiteModels.QuotationItemDetail CreateForPayment(this QuotationItemDetail source)
        {
            var direction = Resources.Shared.Common.TextDirection;
            return new WebsiteModels.QuotationItemDetail
            {
                ItemQuantity = source.ItemQuantity,
                UnitPrice = source.UnitPrice,
                TotalPrice = source.TotalPrice,
                ItemDetails = source.ItemVariationId != null
                    ? direction == "ltr"
                        ? source.ItemVariation.InventoryItem.ItemNameEn
                        : source.ItemVariation.InventoryItem.ItemNameAr
                    : source.ItemDetails
            };
        }

        public static WebsiteModels.Quotation CreateForPayment(this Quotation source, string ins)
        {
            var quot = new WebsiteModels.Quotation
            {
                QuotationId = source.QuotationId,
                GrandTotal = source.QuotationItemDetails.Sum(x=>x.TotalPrice),
                //QuotationItemDetails = source.QuotationItemDetails.Select(x=>x.CreateForPayment()),
            };
            double amount = Convert.ToDouble(quot.GrandTotal);
            double disc = (Convert.ToDouble(source.QuotationDiscount)/100)*amount;
            quot.GrandTotal = Convert.ToDecimal(amount - disc);
            switch (ins)
            {
                case "1":
                    quot.GrandTotal = (source.FirstInstallement/100) * quot.GrandTotal;
                    break;
                case "2":
                    quot.GrandTotal = source.SecondInstallment != 0 ? (Convert.ToDecimal(source.SecondInstallment) / 100) * quot.GrandTotal : 0;
                    break;
                case "3":
                    quot.GrandTotal = source.ThirdInstallment != 0 ? (Convert.ToDecimal(source.ThirdInstallment) / 100) * quot.GrandTotal : 0;
                    break;
                case "4":
                    quot.GrandTotal = source.FourthInstallment != 0 ? (Convert.ToDecimal(source.FourthInstallment) / 100) * quot.GrandTotal : 0;
                    break;
            }
            return quot;
        }

        public static WebsiteModels.Quotation CreateForInvoice(this Quotation source)
        {
            decimal totalAmount = source.QuotationItemDetails.Sum(x => x.TotalPrice);
            decimal discountpercentage = (decimal) (source.QuotationDiscount / Convert.ToDecimal(100));
            decimal discountAmount = discountpercentage * source.QuotationItemDetails.Sum(x => x.TotalPrice);
            decimal grandTotal = source.QuotationItemDetails.Sum(x => x.TotalPrice) - discountAmount;
            var quot = new WebsiteModels.Quotation
            {
                QuotationId = source.QuotationId,
                CustomerId = source.CustomerId,
                RFQId = source.RFQId,
                SerialNumber = source.SerialNumber,
                GreetingsEn = source.GreetingsEn,
                GreetingsAr = source.GreetingsAr,
                QuotationDiscount = source.QuotationDiscount,
                FirstInstallement = grandTotal * (source.FirstInstallement / Convert.ToDecimal(100)),
                FirstInsDueAtCompletion = source.FirstInsDueAtCompletion,
                SecondInstallment = grandTotal * (source.SecondInstallment / Convert.ToDecimal(100)),
                SecondInsDueAtCompletion = source.SecondInsDueAtCompletion,
                ThirdInstallment = grandTotal * (source.ThirdInstallment / Convert.ToDecimal(100)),
                ThirdInsDueAtCompletion = source.ThirdInsDueAtCompletion,
                FourthInstallment = grandTotal * (source.FourthInstallment / Convert.ToDecimal(100)),
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
                GrandTotal = grandTotal,

                FirstInstallmentStatus = source.FirstInstallmentStatus ? "Paid" : "Unpaid",
                SecondInstallmentStatus = source.SecondInstallmentStatus ? "Paid" : "Unpaid",
                ThirdInstallmentStatus = source.ThirdInstallmentStatus ? "Paid" : "Unpaid",
                FourthInstallmentStatus = source.FourthInstallmentStatus ? "Paid" : "Unpaid",

                IsFirstInstallmentStatus = source.FirstInstallmentStatus,
                IsSecondInstallmentStatus = source.SecondInstallmentStatus,
                IsThirdInstallmentStatus = source.ThirdInstallmentStatus,
                IsFourthInstallmentStatus = source.FourthInstallmentStatus
            };
            quot.FirstInstallement = Math.Round(quot.FirstInstallement, 2, MidpointRounding.AwayFromZero);
            quot.SecondInstallment = Math.Round((decimal) quot.SecondInstallment, 2, MidpointRounding.AwayFromZero);
            quot.ThirdInstallment = Math.Round((decimal)quot.ThirdInstallment, 2, MidpointRounding.AwayFromZero);
            quot.FourthInstallment = Math.Round((decimal)quot.FourthInstallment, 2, MidpointRounding.AwayFromZero);
            return quot;
        }

    }
}