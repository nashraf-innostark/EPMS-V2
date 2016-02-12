using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IAboutUsRepository : IBaseRepository<AboutUs, long>
    {
        AboutUs GetAboutUs();
        AboutUs GetAboutUsForWebsite();
        AboutUs SearchAboutUs(string search);
    }
}
