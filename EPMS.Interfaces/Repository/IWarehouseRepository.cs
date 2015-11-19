using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels.ReportsResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface  IWarehouseRepository : IBaseRepository<Warehouse, long>
    {
        bool WarehouseExists(Warehouse warehouse);
        IEnumerable<WarehouseReportDetails> GetWarehouseReportDetails(WarehouseReportCreateOrDetailsRequest request);
    }
}
