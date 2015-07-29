using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class ProductSectionService : IProductSectionService
    {

        #region Private

        private readonly IProductSectionRepository productSectionRepository;

        #endregion

        #region Constructor

        public ProductSectionService(IProductSectionRepository productSectionRepository)
        {
            this.productSectionRepository = productSectionRepository;
        }

        #endregion

        #region Public

        public IEnumerable<ProductSection> GetAll()
        {
            return productSectionRepository.GetAll();
        }

        public ProductSection FindProductSectionById(long id)
        {
            return productSectionRepository.Find(id);
        }

        public bool AddProductSection(ProductSection productSection)
        {
            productSectionRepository.Add(productSection);
            productSectionRepository.SaveChanges();
            return true;
        }

        public bool UpdateProductSection(ProductSection productSection)
        {
            productSectionRepository.Update(productSection);
            productSectionRepository.SaveChanges();
            return true;
        }

        public void DeleteProductSection(ProductSection productSection)
        {
            productSectionRepository.Delete(productSection);
            productSectionRepository.SaveChanges();
        }

        public bool Delete(long id)
        {
            ProductSection productSectionToDelete = productSectionRepository.Find(id);
            productSectionRepository.Delete(productSectionToDelete);
            productSectionRepository.SaveChanges();
            return true;
        }

        public bool SaveProductSections(IList<ProductSection> productSections)
        {
            try
            {
                foreach (var productSection in productSections)
                {
                    productSectionRepository.Add(productSection);
                    productSectionRepository.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
