using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class ComplaintService: IComplaintService
    {
        private readonly IComplaintRepository complaintRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ComplaintService(IComplaintRepository complaintRepository)
        {
            this.complaintRepository = complaintRepository;
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
    }
}
