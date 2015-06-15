using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IItemBarcodeService
    {
        long AddItemBarcode(ItemBarcode itemBarcode);
        long UpdateItemBarcode(ItemBarcode itemBarcode);
        ItemBarcode FindItemBarcode(long itemBarcodeId);
    }
}
