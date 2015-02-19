using System;
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
    public class EmployeeService : IEmployeeService
    {
        private readonly INotificationService notificationService;
        private readonly IEmployeeRepository repository;
        private readonly IAllowanceRepository allowanceRepository;
        private readonly IEmployeeJobHistoryRepository employeeJobHistoryRepository;
        private readonly IEmployeeRequestRepository requestRepository;


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notificationService"></param>
        /// <param name="xRepository"></param>
        public EmployeeService(INotificationService notificationService,IEmployeeRepository xRepository, IEmployeeRequestRepository requestRepository, IAllowanceRepository allowanceRepository, IEmployeeJobHistoryRepository employeeJobHistoryRepository)
        {
            this.notificationService = notificationService;
            repository = xRepository;
            this.requestRepository = requestRepository;
            this.allowanceRepository = allowanceRepository;
            this.employeeJobHistoryRepository = employeeJobHistoryRepository;
        }

        #endregion

        public EmployeeResponse GetAllEmployees(EmployeeSearchRequset employeeSearchRequset)
        {
            return repository.GetAllEmployees(employeeSearchRequset);
        }

        public Employee FindEmployeeById(long? id)
        {
            if (id != null)
            {
                return repository.Find((long) id);
            }
            return null;
        }
        public PayrollResponse FindEmployeeForPayroll(long? id, DateTime currTime)
        {
            PayrollResponse response = new PayrollResponse();
            if (id != null)
            {
                response.IsError = true;
                response.Employee = repository.Find((long)id);
                var empJobTitleHistory = response.Employee.EmployeeJobHistories.FirstOrDefault(x => x.RecCreatedDate <= currTime);
                var jobTitleHistory = response.Employee.JobTitle.JobTitleHistories.OrderByDescending(x=>x.RecCreatedDate).FirstOrDefault(x => empJobTitleHistory != null && ((x.JobTitleId == empJobTitleHistory.JobTitleId) && (x.RecCreatedDate <= currTime)));
                if (jobTitleHistory != null)
                {
                    response.Employee.JobTitle.JobTitleNameE = jobTitleHistory.JobTitle.JobTitleNameE;
                    response.Employee.JobTitle.BasicSalary = jobTitleHistory.BasicSalary;
                    response.IsError = false;
                }
                response.Allowance = allowanceRepository.FindForAllownce((long) id, currTime);
                response.Requests = requestRepository.GetAllMonetaryRequests(currTime, (long)id);
                return response;
            }
            return null;
        }

        public IEnumerable<Employee> GetRecentEmployees(string requester)
        {
            return repository.GetRecentEmployees(requester);
        }

        public IEnumerable<Employee> GetAll()
        {
            return repository.GetAll();
        }

        /// <summary>
        /// Add Employee to DB
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>EmployeeId</returns>
        public long AddEmployee(Employee employee)
        {
            repository.Add(employee);
            repository.SaveChanges();

            //SendNotification(employee);
            return employee.EmployeeId;
        }
        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="employee">Employee Model</param>
        /// <returns></returns>
        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                var tempEmployee = repository.Find(employee.EmployeeId);
                if (employee.JobTitleId != tempEmployee.JobTitleId)
                {
                    var jobHistory = new EmployeeJobHistory
                    {
                        EmployeeId = employee.EmployeeId,
                        JobTitleId = Convert.ToInt64(employee.JobTitleId),
                        RecCreatedDate = DateTime.Now,
                        RecCreatedBy = employee.RecLastUpdatedBy
                    };
                    employeeJobHistoryRepository.Add(jobHistory);
                    employeeJobHistoryRepository.SaveChanges();
                }
                repository.Update(employee);
                repository.SaveChanges();

                //SendNotification(employee);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete Employee from DB
        /// </summary>
        /// <param name="employee">Employee to be Deleted</param>
        public void DeleteEmployee(Employee employee)
        {
            try
            {
                repository.Delete(employee);
                repository.SaveChanges();
            }
            catch (Exception)
            {   
                throw;
            }
        }

        public IEnumerable<string> FindEmployeeEmailByIds(List<long> employeeIds)
        {
            IEnumerable<string> empIds = repository.FindEmployeeEmailById(employeeIds);
            return empIds;
        }

        public void SendNotification(Employee employee)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();

            #region Iqama Expiry Date

            if (Utility.IsDate(employee.EmployeeIqamaExpiryDt))
            {
                notificationViewModel.NotificationResponse.TitleE = "Employee Iqama";
                notificationViewModel.NotificationResponse.TitleA = "Employee Iqama";

                notificationViewModel.NotificationResponse.CategoryId = 3; //Employees
                notificationViewModel.NotificationResponse.AlertBefore = 1; //Month
                notificationViewModel.NotificationResponse.AlertDate =
                    Convert.ToDateTime(employee.EmployeeIqamaExpiryDt).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 0; //Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = true;
                //notificationViewModel.NotificationResponse.EmployeeId = employee.EmployeeId;

                notificationService.AddUpdateNotification(notificationViewModel);
            }

            #endregion

            #region Passport Expiry Date

            if (Utility.IsDate(employee.EmployeePassportExpiryDt))
            {
                notificationViewModel.NotificationResponse.TitleE = "Employee Passport";
                notificationViewModel.NotificationResponse.TitleA = "Employee Passport";

                notificationViewModel.NotificationResponse.CategoryId = 3; //Employees
                notificationViewModel.NotificationResponse.AlertBefore = 1; //Month
                notificationViewModel.NotificationResponse.AlertDate =
                    Convert.ToDateTime(employee.EmployeePassportExpiryDt).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 0; //Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = true;
                //notificationViewModel.NotificationResponse.EmployeeId = employee.EmployeeId;

                notificationService.AddUpdateNotification(notificationViewModel);
            }

            #endregion
        }
    }
}
