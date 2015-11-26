﻿using System.Linq;

namespace EPMS.WebModels.ModelMappers
{
    public static class ReceiptMapper
    {
        public static WebsiteModels.Receipt CreateFromServerToClient(this Models.DomainModels.Receipt source)
        {
            var totalAmount = source.Invoice.Quotation.QuotationItemDetails.Sum(x => x.TotalPrice);
            var amountPaidTillNow =
                source.Invoice.Receipts.Where(x => x.InvoiceId == source.InvoiceId).Sum(x => x.AmountPaid);
            return new WebsiteModels.Receipt
            {
                ReceiptId = source.ReceiptId,
                ReceiptNumber = source.ReceiptNumber,
                InvoiceId = source.InvoiceId,
                AmountPaid = source.AmountPaid,
                InstallmentNumber = source.InstallmentNumber,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                InvoiceNumber = source.Invoice.InvoiceNumber,
                CustomerNameE = source.Invoice.Quotation.Customer.CustomerNameE,
                CustomerNameA = source.Invoice.Quotation.Customer.CustomerNameA,
                CustomerId = source.Invoice.Quotation.CustomerId,
                AmountPaidTillNow = amountPaidTillNow,
                AmountLeft = totalAmount - amountPaidTillNow,
            };
        }

        public static Models.DomainModels.Receipt CreateFromClientToServer(this WebsiteModels.Receipt source)
        {
            return new Models.DomainModels.Receipt
            {
                ReceiptId = source.ReceiptId,
                InvoiceId = source.InvoiceId,
                AmountPaid = source.AmountPaid,
                InstallmentNumber = source.InstallmentNumber,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}
