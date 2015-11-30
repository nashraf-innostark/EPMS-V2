using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.RequestModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels;
using EPMS.Repository.BaseRepository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class QuotationRepository : BaseRepository<Quotation>, IQuotationRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public QuotationRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Quotation> DbSet
        {
            get { return db.Quotations; }
        }
        #endregion

        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<QuotationByColumn, Func<Quotation, object>> quotationClause =

            new Dictionary<QuotationByColumn, Func<Quotation, object>>
                    {
                        { QuotationByColumn.SerialNumber,  c => c.SerialNumber},
                        { QuotationByColumn.ClientName,  c => c.Customer.CustomerNameE},
                        //{ QuotationByColumn.OrderId, c => c.OrderId}
                    };
        #endregion

        #region Public
        public QuotationResponse GetAllQuotation(QuotationSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayStart + searchRequest.iDisplayLength;

            long rfqNo = Convert.ToInt64(searchRequest.SearchString);

            Expression<Func<Quotation, bool>> query;
            if (!searchRequest.AllowedAll)
            {
                if (searchRequest.RoleName == "Customer")
                {
                    query =
                    s =>
                        s.CustomerId == searchRequest.CustomerId && !s.FromOrder && (string.IsNullOrEmpty(searchRequest.SearchString) ||
                        (s.Customer.CustomerNameE.Contains(searchRequest.SearchString) || s.Customer.CustomerNameA.Contains(searchRequest.SearchString)));
                }
                else
                {
                    query =
                    s =>
                        s.RecCreatedBy.Equals(searchRequest.UserId) && !s.FromOrder && (string.IsNullOrEmpty(searchRequest.SearchString) ||
                        (s.Customer.CustomerNameE.Contains(searchRequest.SearchString) || s.Customer.CustomerNameA.Contains(searchRequest.SearchString)));
                }

            }
            else
            {
                query =
                    s =>
                        !s.FromOrder && (string.IsNullOrEmpty(searchRequest.SearchString) ||
                        (s.Customer.CustomerNameE.Contains(searchRequest.SearchString) || s.Customer.CustomerNameA.Contains(searchRequest.SearchString)));
            }

            IEnumerable<Quotation> quotations = searchRequest.sSortDir_0 == "asc" ?
                DbSet
                .Where(query).OrderBy(quotationClause[searchRequest.QuotationByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(quotationClause[searchRequest.QuotationByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new QuotationResponse { Quotations = quotations, TotalCount = DbSet.Count(query) };
        }

        public Quotation FindQuotationByOrderId(long orderId)
        {
            //return DbSet.FirstOrDefault(x => x.OrderId == orderId);
            return null;
        }

        public IEnumerable<Quotation> GetAllQuotationByCustomerId(long customerId)
        {
            return DbSet.Where(quot => quot.CustomerId == customerId && quot.Projects.Count == 0).ToList();
        }

        public long GetAllQuotationsCountByCustomerId(QOReportCreateOrDetailsRequest request)
        {
            return DbSet.Count(quot => quot.CustomerId == request.CustomerId && quot.RecCreatedDate >= request.From && quot.RecCreatedDate <= request.To);
        }

        public IEnumerable<Quotation> FindQuotationByIdForProjectDetail(long id)
        {
            return DbSet.Where(quot => quot.QuotationId == id);
        }

        #endregion
    }
}
