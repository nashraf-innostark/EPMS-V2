using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IPartnerService
    {
        IEnumerable<Partner> GetAll();
        Partner FindPartnerById(long id);
        bool AddPartner(Partner partner);
        bool UpdatePartner(Partner partner);
        void DeletePartner(long id);
    }
}
