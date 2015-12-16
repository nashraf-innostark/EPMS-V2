using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Web.Razor.Generator;
using EPMS.Models.Common;
using EPMS.Models.RequestModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels;
using EPMS.Repository.BaseRepository;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class InventoryItemRepository : BaseRepository<InventoryItem>, IInventoryItemRepository
    {

        private readonly Dictionary<InventoryItemByColumn, Func<InventoryItem, object>> itemReqeustClause =

            new Dictionary<InventoryItemByColumn, Func<InventoryItem, object>>
            {
                {InventoryItemByColumn.ItemCode, c => c.ItemCode},
                {InventoryItemByColumn.ItemNameEn, c => c.ItemNameEn},
                {InventoryItemByColumn.ItemDescriptionEn, c => c.ItemDescriptionEn},
            };

        public InventoryItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<InventoryItem> DbSet
        {
            get { return db.InventoryItems; }
        }

        public bool ItemExists(InventoryItem item)
        {
            if (item.ItemId > 0) //Already in the System
            {
                return DbSet.Any(
                    it => item.ItemId != it.ItemId &&
                          (it.ItemNameEn == item.ItemNameEn || it.ItemNameAr == item.ItemNameAr));
            }
            return DbSet.Any(
                it =>
                    it.ItemNameEn == item.ItemNameEn || it.ItemNameAr == item.ItemNameAr);
        }

        public IEnumerable<InventoryItem> GetInventoryItemReportDetails(
            InventoryItemReportCreateOrDetailsRequest request)
        {
            return DbSet.Include(x => x.ItemVariations)
                .Include("ItemVariations.ItemManufacturers")
                .Include("ItemVariations.ItemReleaseQuantities")
                .Include("ItemVariations.DIFItems")
                .Where(x => (request.ItemId == 0 || x.ItemId == request.ItemId));
        }

        public InventoryItemResponse GetAllInventoryItems(InventoryItemSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayLength;

            Expression<Func<InventoryItem, bool>> query =
                s =>
                    ((string.IsNullOrEmpty(searchRequest.SearchString)) ||
                     (s.ItemCode.Contains(searchRequest.SearchString)) ||
                     (s.ItemNameEn.Contains(searchRequest.SearchString)) ||
                     (s.DescriptionForQuotationEn.Contains(searchRequest.SearchString)));
            IEnumerable<InventoryItem> inventoryItems = searchRequest.sSortDir_0 == "asc"
                ? DbSet
                    .Where(query)
                    .OrderBy(itemReqeustClause[searchRequest.InventoryItemByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet
                    .Where(query)
                    .OrderByDescending(itemReqeustClause[searchRequest.InventoryItemByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new InventoryItemResponse { InventoryItems = inventoryItems, TotalCount = DbSet.Count(query) };
        }
    }
}