using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository iRepository;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xRepository"></param>
        public DepartmentService(IDepartmentRepository xRepository)
        {
            iRepository = xRepository;
        }

        #endregion


        public IEnumerable<Department> LoadAll()
        {
            return iRepository.LoadAll();
        }
        public DepartmentResponse GetAllDepartment(DepartmentSearchRequest departmentSearchRequest)
        {
            return iRepository.GetAllDepartment(departmentSearchRequest);
        }

        public Department FindDepartmentById(int? id)
        {
            return iRepository.FindDepartmentById(id);
        }
    }
}
