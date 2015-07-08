using EPMS.Models.DomainModels;
using EPMS.Web.Models;

namespace EPMS.Web.ModelMappers
{
    public static class PhysicalCountMapper
    {
        public static PhysicalCountListModel CreateListFromServerToClient(this PhysicalCount source)
        {
            return new PhysicalCountListModel
            {
                PCId = source.PCId
            };
        }
    }
}