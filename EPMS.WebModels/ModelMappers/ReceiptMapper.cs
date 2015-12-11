using System;
using System.Linq;

namespace EPMS.WebModels.ModelMappers
{
    public static class ReceiptMapper
    {
        public static WebsiteModels.Receipt CreateFromServerToClient(this Models.DomainModels.Receipt source)
        {
            decimal discountpercentage = (decimal)(source.Invoice.Quotation.QuotationDiscount / Convert.ToDecimal(100));
            decimal discountAmount = discountpercentage * source.Invoice.Quotation.QuotationItemDetails.Sum(x => x.TotalPrice);
            decimal grandTotal = source.Invoice.Quotation.QuotationItemDetails.Sum(x => x.TotalPrice) - discountAmount;
            var totalAmount = source.Invoice.Quotation.QuotationItemDetails.Sum(x => x.TotalPrice);
            var amountPaidTillNow =
                source.Invoice.Receipts.Where(x => x.InvoiceId == source.InvoiceId).Sum(x => x.AmountPaid);
            var receipt =  new WebsiteModels.Receipt
            {
                ReceiptId = source.ReceiptId,
                ReceiptNumber = source.ReceiptNumber,
                InvoiceId = source.InvoiceId,
                AmountPaid = source.AmountPaid,
                InstallmentNumber = source.InstallmentNumber,
                OrderNumber = source.Invoice.Quotation.Orders.FirstOrDefault().OrderNo,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                InvoiceNumber = source.Invoice.InvoiceNumber,
                CustomerNameE = source.Invoice.Quotation.Customer.CustomerNameE,
                CustomerNameA = source.Invoice.Quotation.Customer.CustomerNameA,
                CustomerId = source.Invoice.Quotation.CustomerId,
                AmountPaidTillNow = amountPaidTillNow,
                AmountLeft = Math.Round((grandTotal - amountPaidTillNow), 2, MidpointRounding.AwayFromZero),
                PaymentType = source.PaymentType,
                IsPaid = source.IsPaid,
                PaypalId = source.PaypalId
            };
            if (source.PaymentType == 2)
            {
                receipt.PaymentType = "Paypal";
            }
            else if (source.PaymentType == 3)
            {
                receipt.PaymentType = "Offline";
            }
            else if (source.PaymentType == 4)
            {
                receipt.PaymentType = "On Delivery";
                
            }
            return receipt;
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
                PaymentType = source.PaymentType,
                IsPaid = source.IsPaid,
                PaypalId = source.PaypalId
            };
        }
    }
}
