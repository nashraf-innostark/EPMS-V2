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

        #endregion

        #region Constructor

        public ProductService(IProductRepository productRepository, IProductSectionRepository productSectionRepository, IProductImageRepository productImageRepository)
        {
            this.productRepository = productRepository;
            this.productSectionRepository = productSectionRepository;
            this.productImageRepository = productImageRepository;
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

        #endregion
    }
}
