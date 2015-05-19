﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IInventoryDepartmentService
    {
        IEnumerable<InventoryDepartment> GetAll();
        InventoryDepartment FindInventoryDepartmentById(long id);
        bool AddDepartment(InventoryDepartment department);
        bool UpdateDepartment(InventoryDepartment department);
        void DeleteDepartment(InventoryDepartment department);
        SaveInventoryDepartmentResponse SaveDepartment(InventoryDepartmentRequest departmentToSave);

    }
}
