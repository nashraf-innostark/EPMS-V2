using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface ITIRItemService
    {
        bool AddTIRItem(TIRItem tir);
        bool UpdateTIRItem(TIRItem tir);
        void DeleteTIRItem(TIRItem tir);
    }
}
