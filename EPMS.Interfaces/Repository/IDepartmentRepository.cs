﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> LoadAll(); 
        DepartmentResponse GetAllDepartment(DepartmentSearchRequest departmentSearchRequest);

        Department FindDepartmentById(int? id);
    }
}
