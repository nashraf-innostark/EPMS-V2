using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IContactUsRepository : IBaseRepository<ContactUs, long>
    {
        ContactUs GetContactUs();
        ContactUs SearchContactUs(string search);
    }
}
