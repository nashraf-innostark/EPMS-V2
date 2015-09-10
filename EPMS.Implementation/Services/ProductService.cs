using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
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

        #endregion

        #region Constructor

        public ProductService(IProductRepository productRepository, IProductSectionRepository productSectionRepository, IProductImageRepository productImageRepository, IInventoryDepartmentRepository inventoryDepartmentRepository, IItemVariationRepository itemVariationRepository, IProductSectionService productSectionService)
        {
            this.productRepository = productRepository;
            this.productSectionRepository = productSectionRepository;
            this.productImageRepository = productImageRepository;
            this.inventoryDepartmentRepository = inventoryDepartmentRepository;
            this.itemVariationRepository = itemVariationRepository;
            this.productSectionService = productSectionService;
        }

        #endregion

        #region Public

        public IEnumerable<Product> GetAll()
        {
            return productRepository.GetAll();
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

        public bool Delete(long id)
        {
            Product productToDelete = productRepository.Find(id);
            productRepository.Delete(productToDelete);
            productRepository.SaveChanges();
            return true;
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
            return new ProductResponse();
        }

        public bool SaveProducts(IList<Product> products)
        {
            try
            {
                foreach (var product in products)
                {
                    productRepository.Add(product);
                    productRepository.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void AddProductImages(ProductRequest productToSave)
        {
            if (productToSave.ProductImages !=null)
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

            if (clientList != null && clientList.Any())
            {
                //Add New Items
                foreach (ProductImage productImage in clientList)
                {
                    if (productImage.ImageId > 0)
                    {
                        productImageRepository.Update(productImage);
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
                    }
                }
                //Delete Items that were removed from ClientList
                foreach (ProductImage productImage in dbList)
                {
                    if (clientList.All(x => x.ImageId != productImage.ImageId))
                    {
                        productImageRepository.Delete(productImage);
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
                }
            }
            productImageRepository.SaveChanges();
        }

        public ProductsListResponse GetProductsList(ProductSearchRequest request)
        {
            ProductsListResponse response = new ProductsListResponse
            {
                Products = new List<Product>(),
                AllProducts = productRepository.GetAll().ToList(),
                ProductSections = productSectionService.GetAll()
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
                    var secProducts  = productRepository.GetByItemVariationId(null, request, request.Id);
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
                Product = new Product(),
                ProductSections = productSectionRepository.GetAll().ToList()
            };
            response.Product = productRepository.Find(id);
            return response;
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
