using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class ProjectTask
    {
        public ProjectTask()
        {
            RequisitTasks = new List<ProjectTask>();
            TaskEmployees = new List<TaskEmployee>();
        }
        public long TaskId { get; set; }
        [Required(ErrorMessage = "Please select Customer.")]
        public long CustomerId { get; set; }
        [Required(ErrorMessage = "Please select Project.")]
        public long ProjectId { get; set; }
        [Required(ErrorMessage = "Task Name (English) is required.")]
        public string TaskNameE { get; set; }
        [Required(ErrorMessage = "Task Name (Arabic) is required.")]
        public string TaskNameA { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionA { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "Total Cost is required.")]
        public decimal TotalCost { get; set; }
        [Required(ErrorMessage = "Total Weight is required.")]
        public string TotalWeight { get; set; }
        public string NotesE { get; set; }
        public string NotesA { get; set; }
        public int? TaskProgress { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string ProjectNameE { get; set; }
        public string ProjectNameA { get; set; }

        public string PreReqTasks { get; set; }
        public List<ProjectTask> RequisitTasks { get; set; }
        public List<TaskEmployee> TaskEmployees { get; set; }
        public string EmployeesAssigned { get; set; }
    }
}