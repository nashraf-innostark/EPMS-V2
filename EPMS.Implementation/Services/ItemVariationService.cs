using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    class ItemVariationService : IItemVariationService
    {
        private readonly IItemVariationRepository variationRepository;

        public ItemVariationService(IItemVariationRepository variationRepository)
        {
            this.variationRepository = variationRepository;
        }

        public IEnumerable<ItemVariation> GetAll()
        {
            return variationRepository.GetAll();
        }

        public ItemVariation FindVariationById(long id)
        {
            return variationRepository.Find(id);
        }

        public bool AddVariation(ItemVariation itemVariation)
        {
            variationRepository.Add(itemVariation);
            variationRepository.SaveChanges();
            return true;
        }

        public bool UpdateVariation(ItemVariation itemVariation)
        {
            variationRepository.Update(itemVariation);
            variationRepository.SaveChanges();
            return true;
        }

        public void DeleteVartiation(ItemVariation itemVariation)
        {
            variationRepository.Delete(itemVariation);
            variationRepository.SaveChanges();
        }
    }
}
