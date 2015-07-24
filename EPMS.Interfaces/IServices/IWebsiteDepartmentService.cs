using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IWebsiteDepartmentService
    {
        IEnumerable<WebsiteDepartment> GetAll();
        WebsiteDepartment FindDepartmentById(long id);
        bool AddDepartment(WebsiteDepartment websiteDepartment);
        bool UpdateDepartment(WebsiteDepartment websiteDepartment);
        void DeleteDepartment(long id);
    }
}
