namespace EPMS.WebModels.ModelMappers
{
    public static class InvoiceMapper
    {
        public static WebsiteModels.Invoice CreateFromServerToClient(this Models.DomainModels.Invoice source)
        {
            return new WebsiteModels.Invoice
            {
                InvoiceId = source.InvoiceId,
                InvoiceNumber = source.InvoiceNumber,
                Notes = source.Notes,
                NotesA = source.NotesA,
                QuotationId = source.QuotationId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                ClientNameE = source.Quotation.Customer.CustomerNameE,
                ClientNameA = source.Quotation.Customer.CustomerNameA,
                CustomerId = source.Quotation.CustomerId
            };
        }

        public static Models.DomainModels.Invoice CreateFromClientToServer(this WebsiteModels.Invoice source)
        {
            return new Models.DomainModels.Invoice
            {
                InvoiceId = source.InvoiceId,
                InvoiceNumber = source.InvoiceNumber,
                Notes = source.Notes,
                NotesA = source.NotesA,
                QuotationId = source.QuotationId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

    }
}
