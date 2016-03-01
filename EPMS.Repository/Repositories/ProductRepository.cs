using System;
using System.Collections;
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
        private readonly Dictionary<ProductByOption, Expression<Func<Product, object>>> inventoryClause =

            new Dictionary<ProductByOption, Expression<Func<Product, object>>>
                    {
                        { ProductByOption.ProductNameEn,  c => c.ItemVariation.InventoryItem.ItemNameEn},
                        { ProductByOption.ProductNameAr,  c => c.ItemVariation.InventoryItem.ItemNameAr},
                        { ProductByOption.ProductPrice, c => c.ItemVariation.UnitPrice},
                    };

        private readonly Dictionary<ProductByOption, Expression<Func<Product, object>>> sectionClause =

            new Dictionary<ProductByOption, Expression<Func<Product, object>>>
                    {
                        { ProductByOption.ProductNameEn,  c => c.ProductNameEn},
                        { ProductByOption.ProductNameAr,  c => c.ProductNameAr},
                        { ProductByOption.ProductPrice, c => c.ProductPrice},
                    };

        private readonly Dictionary<ProductByColumn, Func<Product, object>> productClause =

            new Dictionary<ProductByColumn, Func<Product, object>>
            {
                {ProductByColumn.ProductName, c => c.ProductNameEn},
                {ProductByColumn.ProductPrice, c => c.ProductPrice},
                {ProductByColumn.DiscountedPrice, c => c.DiscountedPrice},
                {ProductByColumn.ProductDescription, c => c.ProductDescEn},
                {ProductByColumn.ProductSpecification, c => c.ProductSpecificationEn},
            };


        #endregion

        public ProductResponse GetByItemVariationId(IEnumerable<long> itemVariationIds, ProductSearchRequest request, long productSectionId)
        {
            int fromRow = request.iDisplayStart;
            int toRow = request.iDisplayLength;

            bool searchSpecified = !string.IsNullOrEmpty(request.SearchString);
            ProductResponse response = new ProductResponse
            {
                Products = new List<Product>()
            };
            switch (request.From)
            {
                case "Inventory":
                    Expression<Func<Product, bool>> query =
                        x => itemVariationIds.Contains(x.ItemVariationId.Value) && ((searchSpecified && (x.ItemVariationId.HasValue &&
                    x.ItemVariation.InventoryItem.ItemNameEn.Contains(request.SearchString) || x.ItemVariation.InventoryItem.ItemNameAr.Contains(request.SearchString) ||
                    x.ProductNameEn.Contains(request.SearchString) || x.ProductNameAr.Contains(request.SearchString)))
                    || !searchSpecified);

                    var products = DbSet.Where(query).GroupBy(x=>x.ItemVariation.InventoryItem);
                    foreach (var prod in products)
                    {
                        response.Products.Add(prod.FirstOrDefault());
                    }
                    response.Products = request.SortDirection == "asc" ? 
                        response.Products.AsQueryable().OrderBy(inventoryClause[request.ProductByOption]).Skip(fromRow).Take(toRow).ToList() : 
                        response.Products.AsQueryable().OrderByDescending(inventoryClause[request.ProductByOption]).Skip(fromRow).Take(toRow).ToList();
                    response.TotalCount = products.Count();
                    
                    break;
                case "Sections":
                    Expression<Func<Product, bool>> queery =
                        x => x.ProductSectionId == productSectionId  && ((searchSpecified && (x.ItemVariationId.HasValue &&
                    x.ItemVariation.InventoryItem.ItemNameEn.Contains(request.SearchString) || x.ItemVariation.InventoryItem.ItemNameAr.Contains(request.SearchString) ||
                    x.ProductNameEn.Contains(request.SearchString) || x.ProductNameAr.Contains(request.SearchString)))
                    || !searchSpecified);
                    
                    response.Products = request.SortDirection == "asc" ?
                        DbSet.Where(queery).OrderBy(sectionClause[request.ProductByOption]).Skip(fromRow).Take(toRow).ToList() :
                        DbSet.Where(queery).OrderByDescending(sectionClause[request.ProductByOption]).Skip(fromRow).Take(toRow).ToList();
                    response.TotalCount = DbSet.Count(queery);
                    break;
            }
            return response;
        }
        public IEnumerable<Product> GetByProductSectionId(long productSectionId)
        {
            return DbSet.Where(x => x.ProductSectionId == productSectionId);
        }

        public IEnumerable<Product> SearchInProducts(string search)
        {
            return
                DbSet.Where(
                    x =>
                        (x.ProductNameEn.Contains(search) || x.ProductNameAr.Contains(search) ||
                         x.ProductDescEn.Contains(search) || x.ProductDescAr.Contains(search) ||
                         x.ProductSpecificationEn.Contains(search) || x.ProductSpecificationAr.Contains(search)
                        )
                       );
        }

        public ProductResponse SearchInProducts(ProductSearchRequest productSearchRequest, string search)
        {
            int fromRow = productSearchRequest.iDisplayStart;
            int toRow = productSearchRequest.iDisplayLength;

            ProductResponse response = new ProductResponse
            {
                TotalCount = DbSet.Count(x => (x.ItemVariationId != null && (x.ItemVariation.DescriptionEn.Contains(search) || x.ItemVariation.DescriptionAr.Contains(search) ||
                        x.ItemVariation.AdditionalInfoEn.Contains(search) || x.ItemVariation.AdditionalInfoAr.Contains(search) ||
                        x.ItemVariation.InventoryItem.ItemNameEn.Contains(search) || x.ItemVariation.InventoryItem.ItemNameAr.Contains(search) ||
                        x.ItemVariation.SKUDescriptionEn.Contains(search) || x.ItemVariation.SKUDescriptionAr.Contains(search))
                        ) ||
                        (x.ProductNameEn.Contains(search) || x.ProductNameAr.Contains(search) ||
                        x.ProductDescEn.Contains(search) || x.ProductDescAr.Contains(search) ||
                        x.ProductSpecificationEn.Contains(search) || x.ProductSpecificationAr.Contains(search)
                    )),
                    Products = DbSet.Where(
                    x =>
                        (x.ItemVariationId != null && (x.ItemVariation.DescriptionEn.Contains(search) || x.ItemVariation.DescriptionAr.Contains(search) ||
                        x.ItemVariation.AdditionalInfoEn.Contains(search) || x.ItemVariation.AdditionalInfoAr.Contains(search) ||
                        x.ItemVariation.InventoryItem.ItemNameEn.Contains(search) || x.ItemVariation.InventoryItem.ItemNameAr.Contains(search) ||
                        x.ItemVariation.SKUDescriptionEn.Contains(search) || x.ItemVariation.SKUDescriptionAr.Contains(search))
                        ) || 
                        (x.ProductNameEn.Contains(search) || x.ProductNameAr.Contains(search) ||
                         x.ProductDescEn.Contains(search) || x.ProductDescAr.Contains(search) ||
                         x.ProductSpecificationEn.Contains(search) || x.ProductSpecificationAr.Contains(search)
                        )
                       ).OrderBy(x => x.ProductId).Skip(fromRow).Take(toRow).ToList(),
            };
            return response;
        }

        public Product FindByVariationId(long variationId)
        {
            return DbSet.FirstOrDefault(x => x.ItemVariationId == variationId);
        }

        public Product GetProductForCatalog(int pageNo)
        {
            int fromRow = pageNo-1;
            int toRow = pageNo;

            var dbset = DbSet.GroupBy(x=>x.ItemVariationId).OrderBy(x=>x.FirstOrDefault().ProductNameEn ?? x.FirstOrDefault().ItemVariation.InventoryItem.ItemNameEn).Skip(fromRow).Take(toRow);
            return dbset.FirstOrDefault().FirstOrDefault();
        }

        public int GetProductsCount()
        {
            return DbSet.Count();
        }

        public IEnumerable<Product> GetAllSortedProducts()
        {
            return DbSet.OrderBy(x => x.ProductNameEn ?? x.ItemVariation.InventoryItem.ItemNameEn);
        }


        public ProductListViewResponse GetAllProducts(ProductSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayLength;

            Expression<Func<Product, bool>> query =
                s =>
                    ((string.IsNullOrEmpty(searchRequest.SearchString)) ||
                     (s.ProductNameEn.Contains(searchRequest.SearchString) || s.ProductNameAr.Contains(searchRequest.SearchString) ||
                     s.ItemVariation.SKUDescriptionEn.Contains(searchRequest.SearchString) ||
                     s.ItemVariation.SKUDescriptionAr.Contains(searchRequest.SearchString) ||
                     s.ProductPrice.Contains(searchRequest.SearchString) ||
                     s.ProductDescEn.Contains(searchRequest.SearchString) || s.ProductDescAr.Contains(searchRequest.SearchString) ||
                     s.ProductSpecificationEn.Contains(searchRequest.SearchString) || s.ProductSpecificationAr.Contains(searchRequest.SearchString)));
            IEnumerable<Product> products = searchRequest.sSortDir_0 == "asc"
                ? DbSet
                    .Where(query)
                    .OrderBy(productClause[searchRequest.ProductByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet
                    .Where(query)
                    .OrderByDescending(productClause[searchRequest.ProductByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

            return new ProductListViewResponse {Products = products, TotalCount = DbSet.Count(query)};
        }

        //IList<long> RemoveDuplication()
        //{
            
        //}
    }
}