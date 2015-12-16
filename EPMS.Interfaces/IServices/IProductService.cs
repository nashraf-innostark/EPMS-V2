﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAllSortedProducts();
        int GetProductsCount();
        Product GetProductForCatalog(int pageNo);
        Product FindProductById(long id);
        bool AddProduct(Product product);
        bool UpdateProduct(Product product);
        void DeleteProduct(Product product);
        string DeleteProduct(long id);
        ProductResponse ProductResponse(long id);
        ProductResponse SaveProduct(ProductRequest productToSave);
        bool SaveProducts(IList<Product> products);
        IList<long> RemoveDuplication(string[] itemVariationIds);
        ProductsListResponse GetProductsList(ProductSearchRequest request);
        ProductDetailResponse GetProductDetails(long id, string from);
        ProductListViewResponse GetAllProducts(ProductSearchRequest searchRequest);
    }
}
