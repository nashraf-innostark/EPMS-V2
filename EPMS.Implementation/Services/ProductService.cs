using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Implementation.Services
{
    public class ProductService : IProductService
    {

        #region Private

        private readonly IProductRepository productRepository;
        private readonly IProductSectionRepository productSectionRepository;
        private readonly IProductImageRepository productImageRepository;
        private readonly IInventoryDepartmentRepository inventoryDepartmentRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IProductSectionService productSectionService;
        private readonly IWebsiteHomePageRepository homePageRepository;

        #endregion

        #region Constructor

        public ProductService(IProductRepository productRepository, IProductSectionRepository productSectionRepository, IProductImageRepository productImageRepository, IInventoryDepartmentRepository inventoryDepartmentRepository, IItemVariationRepository itemVariationRepository, IProductSectionService productSectionService, IWebsiteHomePageRepository homePageRepository)
        {
            this.productRepository = productRepository;
            this.productSectionRepository = productSectionRepository;
            this.productImageRepository = productImageRepository;
            this.inventoryDepartmentRepository = inventoryDepartmentRepository;
            this.itemVariationRepository = itemVariationRepository;
            this.productSectionService = productSectionService;
            this.homePageRepository = homePageRepository;
        }

        #endregion

        #region Public

        public IEnumerable<Product> GetAll()
        {
            return productRepository.GetAll();
        }

        public IEnumerable<Product> GetAllSortedProducts()
        {
            return productRepository.GetAllSortedProducts();
        }

        public int GetProductsCount()
        {
            return productRepository.GetProductsCount();
        }

        public Product GetProductForCatalog(int pageNo)
        {
            var product = productRepository.GetProductForCatalog(pageNo);
            return product;
        }

        public Product FindProductById(long id)
        {
            return productRepository.Find(id);
        }

        public bool AddProduct(Product product)
        {
            product.RecCreatedBy = ClaimsPrincipal.Current.Identity.GetUserId(); ;
            product.RecCreatedDt = DateTime.Now;
            product.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId(); ;
            product.RecLastUpdatedDt = DateTime.Now;
            productRepository.Add(product);
            return true;
        }

        public bool UpdateProduct(Product product)
        {
            product.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId(); ;
            product.RecLastUpdatedDt = DateTime.Now;
            productRepository.Update(product);
            return true;
        }

        public void DeleteProduct(Product product)
        {
            productRepository.Delete(product);
            productRepository.SaveChanges();
        }

        public string DeleteProduct(long id)
        {
            Product productToDelete = productRepository.Find(id);
            try
            {
                if (productToDelete != null)
                {
                    if (productToDelete.ProductImages.Any())
                    {
                        foreach (var productImage in productToDelete.ProductImages.ToList())
                        {
                            productImageRepository.Delete(productImage);
                            productImageRepository.SaveChanges();
                        }
                    }
                    productRepository.Delete(productToDelete);
                    productRepository.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception)
            {
                return "Associated";
            }
            return "Error";
        }

        public ProductResponse ProductResponse(long id)
        {
            ProductResponse productResponse = new ProductResponse
            {
                Product = id > 0 ? productRepository.Find(id) : new Product(),
                ProductSections = productSectionRepository.GetAll().ToList()
            };
            return productResponse;
        }

        public ProductResponse SaveProduct(ProductRequest productToSave)
        {
            try
            {
                //Update Product Case
                if (productToSave.Product.ProductId > 0)
                {
                    Product productFromDatabase = productRepository.Find(productToSave.Product.ProductId);
                    UpdateProduct(productToSave.Product);
                    UpdateProductImages(productToSave, productFromDatabase);
                }
                //Add Product Case
                else
                {
                    AddProduct(productToSave.Product);
                    AddProductImages(productToSave);
                }
                productRepository.SaveChanges();
                return new ProductResponse { Status = true };
            }
            catch (Exception)
            {
                return new ProductResponse { Status = false };
            }
        }

        public bool SaveProducts(IList<Product> products)
        {
            try
            {
                IList<ItemVariation> items = new List<ItemVariation>();
                string userId = "";
                foreach (var product in products)
                {
                    productRepository.Add(product);
                    userId = product.RecCreatedBy;
                    productRepository.SaveChanges();
                    var item = itemVariationRepository.Find((long)product.ItemVariationId);
                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
                AddProductSections(items, userId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        void AddProductSections(IEnumerable<ItemVariation> items, string userId)
        {
            IList<InventoryDepartment> productSections = new List<InventoryDepartment>();
            foreach (var itemVariation in items)
            {
                var department = itemVariation.InventoryItem.InventoryDepartment;
                while (department.ParentDepartment != null)
                {
                    department = department.ParentDepartment;
                }
                if (!productSections.Contains(department))
                {
                    productSections.Add(department);
                }
            }
            foreach (var productSection in productSections)
            {
                ProductSection sectionToAdd = new ProductSection
                {
                    SectionNameEn = productSection.DepartmentNameEn,
                    SectionNameAr = productSection.DepartmentNameAr,
                    InventoyDepartmentId = productSection.DepartmentId,
                    ShowToPublic = true,
                    RecCreatedBy = userId,
                    RecCreatedDt = DateTime.Now,
                    RecLastUpdatedBy = userId,
                    RecLastUpdatedDt = DateTime.Now
                };
                var section = productSectionRepository.FindByDepartmentId(productSection.DepartmentId);
                if (section == null)
                {
                    productSectionRepository.Add(sectionToAdd);
                    productSectionRepository.SaveChanges();
                }
            }
        }

        public IList<long> RemoveDuplication(string[] itemVariationIds)
        {
            //foreach (var variationId in itemVariationIds)
            //{
            //    variationId = variationId.Split('_')[0];
            //}
            IList<long> noDuplication = new List<long>();
            if (itemVariationIds != null)
            {
                foreach (string itemVariationId in itemVariationIds)
                {
                    if (itemVariationId.Contains("Item"))
                    {
                        var id = Convert.ToInt64(itemVariationId.Split('_')[0]);
                        var product = productRepository.FindByVariationId(id);
                        if (product == null)
                        {
                            noDuplication.Add(id);
                        }
                    }
                }
            }
            return noDuplication;
        }

        private void AddProductImages(ProductRequest productToSave)
        {
            if (productToSave.ProductImages != null)
            {
                foreach (ProductImage productImage in productToSave.ProductImages)
                {
                    ProductImage image = new ProductImage
                    {
                        ProductId = productToSave.Product.ProductId,
                        ImageOrder = productImage.ImageOrder,
                        ProductImagePath = productImage.ProductImagePath,
                        ShowImage = productImage.ShowImage
                    };
                    productImageRepository.Add(image);
                }
                productImageRepository.SaveChanges();
            }
        }

        private void UpdateProductImages(ProductRequest productToSave, Product productFromDatabase)
        {
            IEnumerable<ProductImage> dbList = productFromDatabase.ProductImages.ToList();
            IEnumerable<ProductImage> clientList = productToSave.ProductImages.ToList();

            if (clientList.Any())
            {
                //Add New Items
                foreach (ProductImage productImage in clientList)
                {
                    if (productImage.ImageId > 0)
                    {
                        productImageRepository.Update(productImage);
                        productImageRepository.SaveChanges();
                    }
                    else
                    {
                        ProductImage image = new ProductImage
                        {
                            ProductId = productToSave.Product.ProductId,
                            ImageOrder = productImage.ImageOrder,
                            ProductImagePath = productImage.ProductImagePath,
                            ShowImage = productImage.ShowImage
                        };
                        productImageRepository.Add(image);
                        productImageRepository.SaveChanges();
                    }
                }
                //Delete Items that were removed from ClientList
                foreach (ProductImage productImage in dbList)
                {
                    if (clientList.All(x => x.ImageId != productImage.ImageId))
                    {
                        productImageRepository.Delete(productImage);
                        productImageRepository.SaveChanges();
                        //var directory = ConfigurationManager.AppSettings["ProductImage"];
                        //var path = "~" + directory + productImage.ProductImagePath;
                        //Utility.DeleteFile(path);
                    }
                }
            }
            else
            {
                //Delete All Images if ClientList is Null
                foreach (ProductImage productImage in dbList)
                {
                    productImageRepository.Delete(productImage);
                    productImageRepository.SaveChanges();
                }
            }
        }

        public ProductsListResponse GetProductsList(ProductSearchRequest request)
        {
            ProductsListResponse response = new ProductsListResponse
            {
                Products = new List<Product>(),
                AllProducts = productRepository.GetAll().OrderByDescending(x => x.RecCreatedDt).Take(100).ToList(),
                ProductSections = productSectionService.GetAll().ToList(),
                ShowProductPrice = homePageRepository.GetHomePageResponse().ShowProductPrice
            };
            switch (request.From)
            {
                case "Inventory":
                    var department = inventoryDepartmentRepository.Find(request.Id);
                    IEnumerable<InventoryDepartment> departmentsForProduct = AllChildDepartments(department);
                    IEnumerable<long> itemVariationIds = GetAllItemVariationIds(departmentsForProduct);
                    var invProducts = productRepository.GetByItemVariationId(itemVariationIds, request, 0);

                    response.Products = invProducts.Products;
                    response.TotalCount = invProducts.TotalCount;
                    break;
                case "Sections":
                    var secProducts = productRepository.GetByItemVariationId(null, request, request.Id);
                    response.Products = secProducts.Products;
                    response.TotalCount = secProducts.TotalCount;
                    break;
            }
            return response;
        }

        public ProductDetailResponse GetProductDetails(long id, string from)
        {
            ProductDetailResponse response = new ProductDetailResponse
            {
                ProductSections = productSectionRepository.GetAll().ToList(),
                ShowProductPrice = homePageRepository.GetHomePageResponse().ShowProductPrice
            };
            response.Product = productRepository.Find(id);
            // size id for add to cart on changing product size
            if (response.Product != null && response.Product.ItemVariationId != null)
            {
                foreach (var variation in response.Product.ItemVariation.InventoryItem.ItemVariations)
                {
                    if (variation.Sizes.FirstOrDefault() != null)
                    {
                        Product firstOrDefault = variation.Products.FirstOrDefault();
                        if (firstOrDefault != null)
                            response.ProductSizes.Add(new ProductSize
                            {
                                //ProductId = response.Product.ProductId,
                                ProductId = firstOrDefault.ProductId,
                                VariationId = variation.ItemVariationId,
                                SizeId = variation.Sizes.FirstOrDefault().SizeId
                            });
                    }
                }
            }
            return response;
        }

        public ProductListViewResponse GetAllProducts(ProductSearchRequest searchRequest)
        {
            return productRepository.GetAllProducts(searchRequest);
        }

        private IEnumerable<InventoryDepartment> AllChildDepartments(InventoryDepartment department)
        {
            IList<InventoryDepartment> childDepartments = new List<InventoryDepartment>();
            childDepartments.Add(department);
            if (department.InventoryDepartments.Any())
            {
                foreach (var inventoryDepartment1 in department.InventoryDepartments)
                {
                    childDepartments.Add(inventoryDepartment1);
                    if (inventoryDepartment1.InventoryDepartments.Any())
                    {
                        foreach (var inventoryDepartment2 in inventoryDepartment1.InventoryDepartments)
                        {
                            childDepartments.Add(inventoryDepartment2);
                        }
                    }
                }
            }
            return childDepartments;
        }

        private IEnumerable<long> GetAllItemVariationIds(IEnumerable<InventoryDepartment> departments)
        {
            IList<long> itemVariationIds = new List<long>();
            foreach (var inventoryDepartment in departments)
            {
                if (inventoryDepartment.InventoryItems.Any())
                {
                    foreach (var inventoryItem in inventoryDepartment.InventoryItems)
                    {
                        if (inventoryItem.ItemVariations.Any())
                        {
                            foreach (var itemVariation in inventoryItem.ItemVariations)
                            {
                                itemVariationIds.Add(itemVariation.ItemVariationId);
                            }
                        }
                    }
                }
            }
            return itemVariationIds;
        }

        #endregion
    }
}
