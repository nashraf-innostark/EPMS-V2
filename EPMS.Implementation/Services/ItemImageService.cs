using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    class ItemImageService : IItemImageService
    {
        private readonly IItemImageRepository imageRepository;

        public ItemImageService(IItemImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public IEnumerable<ItemImage> GetAll()
        {
            return imageRepository.GetAll();
        }

        public ItemImage FindItemImageById(long id)
        {
            return imageRepository.Find(id);
        }

        public bool AddItemImage(ItemImage itemImage)
        {
            imageRepository.Add(itemImage);
            imageRepository.SaveChanges();
            return true;
        }

        public bool UpdateItemImage(ItemImage itemImage)
        {
            imageRepository.Update(itemImage);
            imageRepository.SaveChanges();
            return true;
        }

        public void DeleteItemImage(ItemImage itemImage)
        {
            imageRepository.Delete(itemImage);
            imageRepository.SaveChanges();
        }
    }
}
