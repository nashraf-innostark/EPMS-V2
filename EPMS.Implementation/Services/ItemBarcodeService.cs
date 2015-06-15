using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class ItemBarcodeService:IItemBarcodeService
    {
        private readonly IItemBarcodeRepository itemBarcodeRepository;

        public ItemBarcodeService(IItemBarcodeRepository itemBarcodeRepository)
        {
            this.itemBarcodeRepository = itemBarcodeRepository;
        }

        public long AddItemBarcode(ItemBarcode itemBarcode)
        {
            itemBarcodeRepository.Add(itemBarcode);
            itemBarcodeRepository.SaveChanges();
            return itemBarcode.Id;
        }

        public long UpdateItemBarcode(ItemBarcode itemBarcode)
        {
            itemBarcodeRepository.Update(itemBarcode);
            itemBarcodeRepository.SaveChanges();
            return itemBarcode.Id;
        }

        public ItemBarcode FindItemBarcode(long itemBarcodeId)
        {
            return itemBarcodeRepository.Find(itemBarcodeId);
        }
    }
}
