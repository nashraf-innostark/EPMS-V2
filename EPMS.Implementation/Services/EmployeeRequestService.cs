using System;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class EmployeeRequestService:IEmployeeRequestService
    {
        private readonly IEmployeeRequestRepository repository;
        private readonly IEmployeeRequestDetailRepository repositoryRequestDetail;
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xRepository"></param>
        public EmployeeRequestService(IEmployeeRequestRepository xRepository, IEmployeeRequestDetailRepository xRepositoryRequestDetail)
        {
            repository = xRepository;
            repositoryRequestDetail = xRepositoryRequestDetail;
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
                repositoryRequestDetail.Add(requestDetail);
                repositoryRequestDetail.SaveChanges();
                return requestDetail.RequestId;
            }
            catch (Exception exception)
            {
                return 0;
            }
        }

        public EmployeeRequest Find(long id)
        {
            return repository.Find(id);
        }

        public RequestDetail GetRequestDetailByRequestId(long requestId)
        {
            return repositoryRequestDetail.LoadRequestDetailByRequestId(requestId);
        }
    }
}
