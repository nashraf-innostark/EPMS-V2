using System;
using System.Globalization;
using System.Linq;

namespace EPMS.WebModels.ModelMappers
{
    public static class ReceiptMapper
    {
        public static WebsiteModels.Receipt CreateFromServerToClient(this Models.DomainModels.Receipt source)
        {
            decimal discountpercentage = (decimal) (source.Invoice.Quotation.QuotationDiscount/Convert.ToDecimal(100));
            decimal discountAmount = discountpercentage*
                                     source.Invoice.Quotation.QuotationItemDetails.Sum(x => x.TotalPrice);
            decimal grandTotal = source.Invoice.Quotation.QuotationItemDetails.Sum(x => x.TotalPrice) - discountAmount;
            var totalAmount = source.Invoice.Quotation.QuotationItemDetails.Sum(x => x.TotalPrice);
            var amountPaidTillNow =
                source.Invoice.Receipts.Where(x => x.InvoiceId == source.InvoiceId).Sum(x => x.AmountPaid);
            var receipt = new WebsiteModels.Receipt
            {
                ReceiptId = source.ReceiptId,
                ReceiptNumber = source.ReceiptNumber,
                InvoiceId = source.InvoiceId,
                AmountPaid = source.AmountPaid,
                InstallmentNumber = source.InstallmentNumber,
                OrderNumber = source.Invoice.Quotation.Orders.FirstOrDefault().OrderNo,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt.ToString("dd/MM/yyyy", new CultureInfo("en")),
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
            return receipt;
        }

        public static Models.DomainModels.Receipt CreateFromClientToServer(this WebsiteModels.Receipt source)
        {
            var receipt = new Models.DomainModels.Receipt();
            receipt.ReceiptId = source.ReceiptId;
            receipt.InvoiceId = source.InvoiceId;
            receipt.AmountPaid = source.AmountPaid;
            receipt.InstallmentNumber = source.InstallmentNumber;
            //receipt.RecCreatedBy = source.RecCreatedBy;
            //receipt.RecCreatedDt = DateTime.ParseExact(source.RecCreatedDt, "dd/MM/yyyy", new CultureInfo("en"));
            //receipt.RecLastUpdatedBy = source.RecLastUpdatedBy;
            //receipt.RecLastUpdatedDt = source.RecLastUpdatedDt;
            receipt.PaymentType = source.PaymentType;
            receipt.IsPaid = source.IsPaid;
            receipt.PaypalId = source.PaypalId;
            return receipt;
        }
    }
}