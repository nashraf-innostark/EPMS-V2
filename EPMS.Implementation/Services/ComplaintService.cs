using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository complaintRepository;
        private readonly IDepartmentService departmentService;
        private readonly IOrdersService ordersService;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ComplaintService(IComplaintRepository complaintRepository, IDepartmentService departmentService, IOrdersService ordersService)
        {
            this.complaintRepository = complaintRepository;
            this.departmentService = departmentService;
            this.ordersService = ordersService;
        }

        #endregion
        public Complaint FindComplaintById(long id)
        {
            return complaintRepository.Find(id);
        }

        public bool AddComplaint(Complaint complaint)
        {
            complaintRepository.Add(complaint);
            complaintRepository.SaveChanges();
            return true;
        }

        public bool UpdateComplaint(Complaint complaint)
        {
            complaintRepository.Update(complaint);
            complaintRepository.SaveChanges();
            return true;
        }

        public IEnumerable<Complaint> LoadAllComplaints()
        {
            return complaintRepository.GetAll();
        }

        public IEnumerable<Complaint> LoadAllComplaintsByCustomerId(long id)
        {
            return complaintRepository.GetAllComplaintsByCustomerId(id);
        }

        public IEnumerable<Complaint> LoadComplaintsForDashboard(string requester)
        {
            return complaintRepository.GetComplaintsForDashboard(requester);
        }

        public ComplaintResponse GetComplaintResponse(long complaintId, long customerId, string roleName)
        {
            ComplaintResponse response = new ComplaintResponse
            {
                Departments = departmentService.GetAll()
            };
            if (customerId == 0)
            {
                response.Complaint = FindComplaintById(complaintId);
                response.Orders = ordersService.GetOrdersByCustomerId(response.Complaint.CustomerId);
                return response;
            }
            //response.Complaint = FindComplaintById(complaintId);
            response.Orders = ordersService.GetOrdersByCustomerId(customerId);
            return response;
        }
    }
}
