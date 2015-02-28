﻿using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Implementation.Services
{
    public class EmployeeRequestService:IEmployeeRequestService
    {
        private readonly INotificationService notificationService;
        private readonly IEmployeeRequestRepository repository;
        private readonly IEmployeeRequestDetailRepository repositoryRequestDetail;
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notificationService"></param>
        /// <param name="xRepository"></param>
        public EmployeeRequestService(INotificationService notificationService,IEmployeeRequestRepository xRepository, IEmployeeRequestDetailRepository xRepositoryRequestDetail)
        {
            this.notificationService = notificationService;
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
                if (requestDetail.IsReplied)
                {
                    SendNotification(requestDetail);
                }
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
        public IEnumerable<EmployeeRequest> LoadRequestsForDashboard(string requester)
        {
            return repository.GetRequestsForDashboard(requester);
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
        public IEnumerable<EmployeeRequest> LoadAllRequestsForEmployee(long requester)
        {
            return repository.GetAllRequests(Convert.ToInt64(requester));
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


        public void SendNotification(RequestDetail requestDetail)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();

            #region Request Approved
            requestDetail.EmployeeRequest = repository.Find(requestDetail.RequestId);

            string status = requestDetail.IsApproved ? " Approved" : " Rejected";
            notificationViewModel.NotificationResponse.TitleE = requestDetail.EmployeeRequest.RequestTopic + status;
            notificationViewModel.NotificationResponse.TitleA = requestDetail.EmployeeRequest.RequestTopic + status;

            notificationViewModel.NotificationResponse.CategoryId = 3; //Employees
            notificationViewModel.NotificationResponse.SubCategoryId = 1;

            notificationViewModel.NotificationResponse.AlertBefore = 3; //1 Day
            notificationViewModel.NotificationResponse.AlertDate =
                Convert.ToDateTime(DateTime.Now).ToShortDateString();
            notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
            notificationViewModel.NotificationResponse.SystemGenerated = false;//False, because it is not for admins, it has its specific recipient
            
            notificationViewModel.NotificationResponse.UserId = requestDetail.EmployeeRequest.Employee.AspNetUsers.FirstOrDefault().Id;
            notificationViewModel.NotificationResponse.EmployeeId = requestDetail.EmployeeRequest.Employee.EmployeeId;
            notificationService.AddUpdateNotification(notificationViewModel);

            #endregion
        }
    }
}
