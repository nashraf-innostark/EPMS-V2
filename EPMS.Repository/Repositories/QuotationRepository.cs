using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.RequestModels;
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
                        { QuotationByColumn.ClientName,  c => c.ClientName},
                        { QuotationByColumn.OrderNo, c => c.OrderNumber}
                    };
        #endregion

        #region Public
        public QuotationResponse GetAllQuotation(QuotationSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayStart + searchRequest.iDisplayLength;

            Expression<Func<Quotation, bool>> query =
                s => ((string.IsNullOrEmpty(searchRequest.SearchString)) || (s.ClientName.Contains(searchRequest.SearchString)) ||
                    (s.OrderNumber.Contains(searchRequest.SearchString)));

            IEnumerable<Quotation> quotations = searchRequest.sSortDir_0 == "asc" ?
                DbSet
                .Where(query).OrderBy(quotationClause[searchRequest.QuotationByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(quotationClause[searchRequest.QuotationByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new QuotationResponse { Quotations = quotations, TotalCount = DbSet.Count(query) };
        }

        public Quotation FindQuotationByOrderNo(string orderNo)
        {
            return DbSet.FirstOrDefault(x => x.OrderNumber == orderNo);
        }

        #endregion
    }
}
