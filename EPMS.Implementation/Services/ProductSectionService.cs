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

        public IList<long> RemoveDuplication(string[] departmentIds)
        {
            IList<long> noDuplication = new List<long>();
            if (departmentIds != null)
            {
                foreach (string departmentId in departmentIds)
                {
                    if (departmentId.Contains("department"))
                    {
                        var id = Convert.ToInt64(departmentId.Split('_')[0]);
                        var productSection = productSectionRepository.FindByDepartmentId(id);
                        if (productSection == null)
                        {
                            noDuplication.Add(id);
                        }
                    }
                }
            }
            return noDuplication;
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

        public string DeleteProductSection(long id)
        {
            ProductSection productSectionToDelete = productSectionRepository.Find(id);
            try
            {
                if (productSectionToDelete != null)
                {
                    productSectionRepository.Delete(productSectionToDelete);
                    productSectionRepository.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception)
            {
                return "Associated";
            }
            return "Error";
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
