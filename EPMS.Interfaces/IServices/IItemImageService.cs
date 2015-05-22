using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface  IItemImageService
    {
        IEnumerable<ItemImage> GetAll();
        ItemImage FindItemImageById(long id);
        bool AddItemImage(ItemImage itemImage);
        bool UpdateItemImage(ItemImage itemImage);
        void DeleteItemImage(ItemImage itemImage);
    }
}
