using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ItemReleaseRepository : BaseRepository<ItemRelease>, IItemReleaseRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ItemReleaseRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemRelease> DbSet
        {
            get { return db.ItemReleases; }
        }

        #endregion

        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<ItemReleaseByColumn, Func<ItemRelease, object>> itemReleaseClause =

            new Dictionary<ItemReleaseByColumn, Func<ItemRelease, object>>
                    {
                        { ItemReleaseByColumn.ItemReleaseFormNumber,  c => c.FormNumber},
                        { ItemReleaseByColumn.Quantity, c => c.QuantityReleased},
                        { ItemReleaseByColumn.EmployeeName, c => c.CreatedBy},
                        { ItemReleaseByColumn.ShipmentDetails, c => c.DeliveryInfo},
                        { ItemReleaseByColumn.Status, c => c.Status}
                    };
        #endregion

        #region Public

        public ItemReleaseResponse GetAllItemRelease(ItemReleaseSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayStart + searchRequest.iDisplayLength;
            if (searchRequest.iSortCol_0 == 0)
            {
                searchRequest.iSortCol_0 = 1;
            }
            if (!searchRequest.CompleteAccess)
            {
                if (searchRequest.iSortCol_0 > 2)
                {
                    searchRequest.iSortCol_0 += 1;
                }
            }
            Expression<Func<ItemRelease, bool>> queery;
            int status;
            if (int.TryParse(searchRequest.SearchString, out status))
            {
                if (searchRequest.Requester == "Admin")
                {
                    queery =
                s => ((string.IsNullOrEmpty(searchRequest.SearchString)) || (s.FormNumber.Contains(searchRequest.SearchString) ||
                    s.CreatedBy.Contains(searchRequest.SearchString) || (s.DeliveryInfo.Contains(searchRequest.SearchString) ||
                    s.DeliveryInfoArabic.Contains(searchRequest.SearchString)) || s.Status == status));
                }
                else
                {
                    queery =
                s => ((string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Contains(searchRequest.SearchString) ||
                    (s.DeliveryInfo.Contains(searchRequest.SearchString) || s.DeliveryInfoArabic.Contains(searchRequest.SearchString)) || s.Status == status))
                    && s.RecCreatedBy.Equals(searchRequest.Requester));
                }
            }
            else
            {
                if (searchRequest.Requester == "Admin")
                {
                    queery =
                s => ((string.IsNullOrEmpty(searchRequest.SearchString)) || (s.FormNumber.Contains(searchRequest.SearchString) ||
                    s.CreatedBy.Contains(searchRequest.SearchString) || (s.DeliveryInfo.Contains(searchRequest.SearchString) ||
                    s.DeliveryInfoArabic.Contains(searchRequest.SearchString))));
                }
                else
                {
                    queery =
                s => ((string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Contains(searchRequest.SearchString) ||
                    (s.DeliveryInfo.Contains(searchRequest.SearchString) || s.DeliveryInfoArabic.Contains(searchRequest.SearchString))))
                    && s.RecCreatedBy.Equals(searchRequest.Requester));
                }
            }

            IEnumerable<ItemRelease> releases = searchRequest.sSortDir_0 == "asc" ?
            DbSet
            .Where(queery).OrderBy(itemReleaseClause[searchRequest.ItemReleaseByColumn]).Skip(fromRow).Take(toRow).ToList()
                                       :
                                       DbSet
                                       .Where(queery).OrderByDescending(itemReleaseClause[searchRequest.ItemReleaseByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new ItemReleaseResponse { ItemReleases = releases, TotalDisplayRecords = DbSet.Count(queery), TotalRecords = DbSet.Count(queery) };
        }

        public IEnumerable<ItemRelease> GetRecentIRFs(int status, string requester, DateTime date)
        {
            DateTime newDataTime = new DateTime();
            if (requester == "Admin")
            {
                requester = "";
                return status > 0 ? DbSet.Where(x => x.Status == status && (string.IsNullOrEmpty(requester) || x.RecCreatedBy == requester) && (date == newDataTime || DbFunctions.TruncateTime(x.RecCreatedDate) == date.Date)).OrderByDescending(x => x.RecCreatedDate).Take(5) :
                    DbSet.Where(x => (string.IsNullOrEmpty(requester) || x.RecCreatedBy == requester) && (date == newDataTime || DbFunctions.TruncateTime(x.RecCreatedDate) == date.Date)).OrderByDescending(x => x.RecCreatedDate).Take(5);
            }
            return status > 0 ? DbSet.Where(x => x.RecCreatedBy == requester && x.Status == status && (date == newDataTime || DbFunctions.TruncateTime(x.RecCreatedDate) == date.Date)).OrderByDescending(x => x.RecCreatedDate).Take(5) :
                DbSet.Where(x => x.RecCreatedBy == requester && (date == newDataTime || DbFunctions.TruncateTime(x.RecCreatedDate) == date.Date)).OrderByDescending(x => x.RecCreatedDate).Take(5);
        }

        public string GetLastFormNumber()
        {
            ItemRelease itemRelease = DbSet.OrderByDescending(x => x.RecCreatedDate).FirstOrDefault();
            if (itemRelease != null)
                return itemRelease.FormNumber;
            return "IR00000001";
        }

        #endregion
    }
}
