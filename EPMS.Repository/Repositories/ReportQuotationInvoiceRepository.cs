using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ReportQuotationInvoiceRepository : BaseRepository<ReportQuotationInvoice>, IReportQuotaionInvoiceRepository
    {
        public ReportQuotationInvoiceRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<ReportQuotationInvoice> DbSet
        {
            get { return db.ReportQuotationInvoices; }
        }
    }
}
