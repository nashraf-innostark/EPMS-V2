using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.Web.ViewModels;
using EPMS.Web.ViewModels.Quotation;

namespace EPMS.Web.ModelMappers
{
    public static class QuotationMapper
    {
        public static Quotation CreateFromClientToServer(this QuotationCreateViewModel source)
        {
            var caseType = new Quotation
            {
                QuotationId = source.QuotationId,
                ClientName = source.ClientName,
                CustomerId = source.CustomerId,
                OrderId = source.OrderId,
                CreatedByEmployee = source.CreatedByEmployee,
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
                RecCreatedDt = source.RecCreatedDt,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDt = source.RecUpdatedDt,
            };
            return caseType;
        }

        public static QuotationCreateViewModel CreateFromServerToClient(this Quotation source)
        {
            return new QuotationCreateViewModel
            {
                QuotationId = source.QuotationId,
                ClientName = source.ClientName,
                CustomerId = source.CustomerId,
                OrderId = source.OrderId ?? 0,
                CreatedByEmployee = source.CreatedByEmployee,
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
                RecCreatedDt = source.RecCreatedDt,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDt = source.RecUpdatedDt,
                QuotationItemDetails = source.QuotationItemDetails.Select(x => x.CreateFromServerToClient()).ToList(),
            };
        }

        public static Models.Quotation CreateFromServerToClientDdl(this Quotation source)
        {
            return new Models.Quotation
            {
                QuotationId = source.QuotationId,
                ClientName = source.ClientName,
                CustomerId = source.CustomerId,
                Customers = source.Customer.CreateFromServerToClient(),
                GreetingsEn = source.GreetingsEn,
                GreetingsAr = source.GreetingsAr,
            };
        }

        public static Models.Quotation CreateFromServerToClientLv(this Quotation source)
        {
            return new Models.Quotation
            {
                QuotationId = source.QuotationId,
                ClientName = source.ClientName,
                CustomerId = source.CustomerId,
                OrderId = source.OrderId ?? 0,
                CreatedByEmployee = source.CreatedByEmployee,
                Customers = source.Customer.CreateFromServerToClient(),
                RecCreatedDt = source.RecCreatedDt,
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
            };
        }
        public static Models.Quotation CreateFromServerToClientForProject(this Quotation source)
        {
            return new Models.Quotation
            {
                QuotationId = source.QuotationId,
                ClientName = source.ClientName,
                CustomerId = source.CustomerId,
                OrderId = source.OrderId ?? 0,
                CreatedByEmployee = source.CreatedByEmployee,
                GreetingsEn = source.GreetingsEn,
                GreetingsAr = source.GreetingsAr,
            };
        }

        public static QuotationItemDetail CreateFromClientToServer(this Models.QuotationItemDetail source)
        {
            var caseType = new QuotationItemDetail
            {
                ItemId = source.ItemId,
                QuotationId = source.QuotationId,
                ItemDetails = source.ItemDetails,
                ItemQuantity = source.ItemQuantity,
                UnitPrice = source.UnitPrice,
                TotalPrice = source.TotalPrice,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDt = source.RecUpdatedDt,
            };
            return caseType;
        }

        public static Models.QuotationItemDetail CreateFromServerToClient(this QuotationItemDetail source)
        {
            return new Models.QuotationItemDetail
            {
                ItemId = source.ItemId,
                QuotationId = source.QuotationId,
                ItemDetails = source.ItemDetails,
                ItemQuantity = source.ItemQuantity,
                UnitPrice = source.UnitPrice,
                TotalPrice = source.TotalPrice,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDt = source.RecUpdatedDt,
            };
        }
    }
}