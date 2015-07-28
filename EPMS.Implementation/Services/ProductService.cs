using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class ProductService : IProductService
    {

        #region Private

        private readonly IProductRepository productRepository;

        #endregion

        #region Constructor

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
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
            productRepository.Add(product);
            productRepository.SaveChanges();
            return true;
        }

        public bool UpdateProduct(Product product)
        {
            productRepository.Update(product);
            productRepository.SaveChanges();
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

        #endregion
    }
}
