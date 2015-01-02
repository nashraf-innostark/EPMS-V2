using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

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

        public bool DeleteRequest(long requestId)
        {
            try
            {
                EmployeeRequest request = Find(requestId);
                if (request != null)
                {
                    if (request.RequestDetails.Any())
                    {
                        int count = request.RequestDetails.Count();
                        for (int i = 0; i < count; i++)
                        {
                            repositoryRequestDetail.Delete(request.RequestDetails.FirstOrDefault(x => x.RowVersion == i));
                            repositoryRequestDetail.SaveChanges();
                        }
                    }
                    repository.Delete(request);
                    repository.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public EmployeeRequestResponse LoadAllRequests(EmployeeRequestSearchRequest searchRequset)
        {
            return repository.GetAllRequests(searchRequset);
        }
        public IEnumerable<EmployeeRequest> LoadAllMonetaryRequests(DateTime currentMonth, long id)
        {
            return repository.GetAllMonetaryRequests(currentMonth,id);
        }

        public IEnumerable<EmployeeRequest> LoadAllRequests(string requester)
        {
            return requester == "Admin" ? repository.GetAll() : repository.GetAllRequests(Convert.ToInt64(requester));
        }

        public RequestDetail LoadRequestDetailByRequestId(long requestId)
        {
            return repositoryRequestDetail.LoadRequestDetailByRequestId(requestId);
        }

        public bool UpdateRequest(EmployeeRequest request)
        {
            try
            {
                repository.Update(request);
                repository.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool UpdateRequestDetail(RequestDetail requestDetail)
        {
            try
            {
                repositoryRequestDetail.Update(requestDetail);
                repositoryRequestDetail.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}
