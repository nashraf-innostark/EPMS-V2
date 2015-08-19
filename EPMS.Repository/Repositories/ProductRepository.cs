using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        #region Constructor
        public ProductRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<Product> DbSet
        {
            get { return db.Products; }
        }
        #endregion

        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<ProductByOption, Func<Product, object>> inventoryClause =

            new Dictionary<ProductByOption, Func<Product, object>>
                    {
                        { ProductByOption.ProductNameEn,  c => c.ItemVariation.InventoryItem.ItemNameEn},
                        { ProductByOption.ProductNameAr,  c => c.ItemVariation.InventoryItem.ItemNameAr},
                        { ProductByOption.ProductPrice, c => c.ItemVariation.UnitPrice},
                    };

        private readonly Dictionary<ProductByOption, Func<Product, object>> sectionClause =

            new Dictionary<ProductByOption, Func<Product, object>>
                    {
                        { ProductByOption.ProductNameEn,  c => c.ProductNameEn},
                        { ProductByOption.ProductNameAr,  c => c.ProductNameAr},
                        { ProductByOption.ProductPrice, c => c.ProductPrice},
                    };
        #endregion

        public IList<Product> GetByItemVariationId(IEnumerable<long> itemVariationIds, ProductSearchRequest request)
        {
            bool searchSpecified = !string.IsNullOrEmpty(request.SearchString);
            IList<Product> result = new List<Product>();
            switch (request.From)
            {
                case "Inventory":
                    Expression<Func<Product, bool>> query =
                        x => itemVariationIds.Contains(x.ItemVariationId.Value) && ((searchSpecified && (x.ItemVariationId.HasValue &&
                    x.ItemVariation.InventoryItem.ItemNameEn.Contains(request.SearchString) || x.ItemVariation.InventoryItem.ItemNameAr.Contains(request.SearchString) ||
                    x.ProductNameEn.Contains(request.SearchString) || x.ProductNameAr.Contains(request.SearchString)))
                    || !searchSpecified);

                    result = request.SortDirection == "asc" ?
                        DbSet.Where(query).OrderBy(inventoryClause[request.ProductByOption]).Take(request.NoOfItems).ToList() :
                        DbSet.Where(query).OrderByDescending(inventoryClause[request.ProductByOption]).Take(request.NoOfItems).ToList();
                    break;
                case "Sections":
                    Expression<Func<Product, bool>> queery =
                        x => itemVariationIds.Contains(x.ItemVariationId.Value) && searchSpecified && (x.ItemVariationId.HasValue &&
                    x.ItemVariation.InventoryItem.ItemNameEn.Contains(request.SearchString) || x.ItemVariation.InventoryItem.ItemNameAr.Contains(request.SearchString) ||
                    x.ProductNameEn.Contains(request.SearchString) || x.ProductNameAr.Contains(request.SearchString))
                    || !searchSpecified;
                    
                    result = request.SortDirection == "asc" ?
                        DbSet.Where(queery).OrderBy(sectionClause[request.ProductByOption]).Take(request.NoOfItems).ToList() :
                        DbSet.Where(queery).OrderByDescending(sectionClause[request.ProductByOption]).Take(request.NoOfItems).ToList();
                    break;
            }
            return result;
        }
        public IEnumerable<Product> GetByProductSectionId(long productSectionId)
        {
            return DbSet.Where(x => x.ProductSectionId == productSectionId);
        }
    }
}
