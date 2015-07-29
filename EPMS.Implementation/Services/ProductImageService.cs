using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class ProductImageService : IProductImageService
    {
        #region Private

        private readonly IProductImageRepository productImageRepository;

        #endregion

        #region Constructor

        public ProductImageService(IProductImageRepository productImageRepository)
        {
            this.productImageRepository = productImageRepository;
        }

        #endregion

        #region Public
        public IEnumerable<ProductImage> GetAll()
        {
            return productImageRepository.GetAll();
        }

        public ProductImage FindProductImageById(long id)
        {
            return productImageRepository.Find(id);
        }

        public bool AddProductImage(ProductImage productImage)
        {
            productImageRepository.Add(productImage);
            productImageRepository.SaveChanges();
            return true;
        }

        public bool UpdateProductImage(ProductImage productImage)
        {
            productImageRepository.Update(productImage);
            productImageRepository.SaveChanges();
            return true;
        }
        #endregion
    }
}
