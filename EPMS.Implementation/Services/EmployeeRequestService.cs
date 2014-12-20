using System;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class EmployeeRequestService:IEmployeeRequestService
    {
        private readonly IEmployeeRequestRepository repository;
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xRepository"></param>
        public EmployeeRequestService(IEmployeeRequestRepository xRepository)
        {
            repository = xRepository;
        }

        #endregion
        public long AddRequest(EmployeeRequest request)
        {
            try
            {
                repository.Add(request);
                repository.SaveChanges();
                return request.RequestId;
            }
            catch (Exception exception)
            {
                return 0;
            }
        }

        public long AddRequestDetail(RequestDetail requestDetail)
        {
            try
            {
                //repository.Add(requestDetail);
                //repository.SaveChanges();
                //return requestDetail.RequestId;
                return 1;
            }
            catch (Exception exception)
            {
                return 0;
            }
        }
    }
}
