using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IWarehouseDetailService
    {
        /// <summary>
        /// Get All WarehouseDetail
        /// </summary>
        IEnumerable<WarehouseDetail> GetAll();
        /// <summary>
        /// Find WarehouseDetail By Id
        /// </summary>
        WarehouseDetail FindWarehouseDetailById(long id);
        /// <summary>
        /// Add WarehouseDetail
        /// </summary>
        bool AddWarehouseDetail(WarehouseDetail warehouseDetail);
        /// <summary>
        /// Update WarehouseDetail
        /// </summary>
        bool UpdateWarehouseDetail(WarehouseDetail warehouseDetail);
    }
}
