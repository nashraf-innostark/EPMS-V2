using System;
using System.Collections.Generic;
using System.Globalization;
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
        #region Private
        private readonly IAspNetUserRepository aspNetUserRepository;
        private readonly INotificationRepository notificationRepository;
        private readonly INotificationService notificationService;
        private readonly IEmployeeRepository repository;
        private readonly IAllowanceRepository allowanceRepository;
        private readonly IEmployeeJobHistoryRepository employeeJobHistoryRepository;
        private readonly IEmployeeRequestRepository requestRepository;
        private readonly IJobTitleHistoryRepository JobTitleHistoryRepository;
        private readonly IJobTitleService JobTitleService;
        private readonly IEmployeeRequestService EmployeeRequestService;
        private readonly ITaskEmployeeService TaskEmployeeService;
        private readonly IAllowanceService allowanceService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aspNetUserRepository"></param>
        /// <param name="notificationRepository"></param>
        /// <param name="notificationService"></param>
        /// <param name="xRepository"></param>
        public EmployeeService(IAspNetUserRepository aspNetUserRepository, INotificationRepository notificationRepository, INotificationService notificationService, IEmployeeRepository xRepository, IEmployeeRequestRepository requestRepository, IAllowanceRepository allowanceRepository, IEmployeeJobHistoryRepository employeeJobHistoryRepository, IJobTitleHistoryRepository jobTitleHistoryRepository, IJobTitleService jobTitleService, IEmployeeRequestService employeeRequestService, ITaskEmployeeService taskEmployeeService, IAllowanceService allowanceService)
        {
            this.aspNetUserRepository = aspNetUserRepository;
            this.notificationRepository = notificationRepository;
            this.notificationService = notificationService;
            repository = xRepository;
            this.requestRepository = requestRepository;
            this.allowanceRepository = allowanceRepository;
            this.employeeJobHistoryRepository = employeeJobHistoryRepository;
            JobTitleHistoryRepository = jobTitleHistoryRepository;
            JobTitleService = jobTitleService;
            EmployeeRequestService = employeeRequestService;
            TaskEmployeeService = taskEmployeeService;
            this.allowanceService = allowanceService;
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
                return repository.Find((long)id);
            }
            return null;
        }

        public EmployeeResponse GetEmployeeAlongJobHistory(long? employeeId)
        {
            EmployeeResponse response = new EmployeeResponse();
            if (employeeId != null)
            {
                response.Employee = repository.Find((long)employeeId);
                response.Allowance = allowanceService.FindByEmpIdDate(response.Employee.EmployeeId, DateTime.Now);
                response.JobTitleList = JobTitleService.GetAll();
                response.EmployeeMonetaryRequests = EmployeeRequestService.LoadAllMonetaryRequests(DateTime.Now, (long)employeeId);
                response.EmployeeRequests = EmployeeRequestService.LoadAllRequestsForEmployee((long)employeeId);
                response.EmployeeTasks = TaskEmployeeService.GetTaskEmployeeByEmployeeId((long)employeeId);
                var jobHistory = employeeJobHistoryRepository.GetJobHistoryByEmployeeId((long)employeeId).ToList();
                if (jobHistory.Any())
                {
                    for (int i = 0; i < jobHistory.Count; i++)
                    {
                        //response.JobHistory.JobTitleHistory.Add(JobTitleHistoryRepository.GetJobTitleHistoryByJobTitleId(employeeJobHistories[i].JobTitleId));
                        JobHistory jobs = new JobHistory();
                        jobs.JobTitle = jobHistory[i].JobTitle.JobTitleNameE;
                        DateTime from;
                        DateTime to;
                        if (i == 0)
                        {
                            from = response.Employee.RecCreatedDt ?? DateTime.Now;
                        }
                        else
                        {
                            from = jobHistory[i - 1].RecCreatedDate;
                        }
                        if (i == jobHistory.Count)
                        {
                            to = DateTime.Now;
                        }
                        else
                        {
                            to = jobHistory[i].RecCreatedDate;
                        }
                        jobs.BasicSalary = jobHistory[i].JobTitle.BasicSalary ?? 0;
                        var allowance = allowanceRepository.FindAllownceFromTo((long)employeeId, from, to).ToList();
                        double totalLastAllowances = 0;
                        if (allowance.Any())
                        {
                            var lastAllowances = allowance.OrderByDescending(x => x.AllowanceDate).FirstOrDefault();
                            if (lastAllowances != null)
                            {
                                totalLastAllowances = lastAllowances.Allowance1 + lastAllowances.Allowance2 + lastAllowances.Allowance3 + lastAllowances.Allowance4 + lastAllowances.Allowance5 ?? 0;
                            }
                            jobs.SalaryWithAllowances = jobs.BasicSalary + totalLastAllowances;
                        }
                        else
                        {
                            var lastAllowance = allowanceRepository.FindLastAllownce((long)employeeId);
                            if (lastAllowance != null)
                            {
                                totalLastAllowances = lastAllowance.Allowance1 + lastAllowance.Allowance2 + lastAllowance.Allowance3 + lastAllowance.Allowance4 + lastAllowance.Allowance5 ?? 0;
                            }
                            jobs.SalaryWithAllowances = jobs.BasicSalary + totalLastAllowances;
                        }
                        double sumOfAllAllowances = allowance.Sum(allowance1 => allowance1.Allowance1 + allowance1.Allowance2 + allowance1.Allowance3 + allowance1.Allowance4 + allowance1.Allowance5 ?? 0);
                        int noOfMonths = to.Month - from.Month;
                        jobs.TotalSalaryReceived = (noOfMonths * jobs.BasicSalary) + sumOfAllAllowances;
                        jobs.From = Convert.ToDateTime(from)
                            .ToString("dd/MM/yyyy", new CultureInfo("en"));
                        jobs.To = Convert.ToDateTime(to)
                            .ToString("dd/MM/yyyy", new CultureInfo("en"));
                        response.JobHistories.Add(jobs);
                    }
                }
                return response;
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
                var jobTitleHistory = response.Employee.JobTitle.JobTitleHistories.OrderByDescending(x => x.RecCreatedDate).FirstOrDefault(x => empJobTitleHistory != null && ((x.JobTitleId == empJobTitleHistory.JobTitleId) && (x.RecCreatedDate <= currTime)));
                if (jobTitleHistory != null)
                {
                    response.Employee.JobTitle.JobTitleNameE = jobTitleHistory.JobTitle.JobTitleNameE;
                    response.Employee.JobTitle.BasicSalary = jobTitleHistory.BasicSalary;
                    response.IsError = false;
                }
                response.Allowance = allowanceRepository.FindAllownce((long)id, currTime);
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

            if (employee.EmployeeIqamaExpiryDt != null)
            {
                notificationService.CreateNotification("iqama", employee.EmployeeId, employee.EmployeeIqamaExpiryDt);
            }
            if (employee.EmployeePassportExpiryDt != null)
            {
                notificationService.CreateNotification("passport", employee.EmployeeId, employee.EmployeePassportExpiryDt);
            }
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
                        JobTitleId = Convert.ToInt64(tempEmployee.JobTitleId),
                        RecCreatedDate = DateTime.Now,
                        RecCreatedBy = employee.RecLastUpdatedBy
                    };
                    employeeJobHistoryRepository.Add(jobHistory);
                    employeeJobHistoryRepository.SaveChanges();
                }
                repository.Update(employee);
                repository.SaveChanges();

                if (employee.EmployeeIqamaExpiryDt != null)
                {
                    notificationService.CreateNotification("iqama", employee.EmployeeId, employee.EmployeeIqamaExpiryDt);
                }
                if (employee.EmployeePassportExpiryDt != null)
                {
                    notificationService.CreateNotification("passport", employee.EmployeeId, employee.EmployeePassportExpiryDt);
                }
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

    }
}
