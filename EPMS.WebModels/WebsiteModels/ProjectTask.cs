using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class ProjectTask
    {
        public ProjectTask()
        {
            RequisitTasks = new List<ProjectTask>();
            TaskEmployees = new List<TaskEmployee>();
            SubTasksWeight = new List<SubTaskWeight>();
        }
        public long TaskId { get; set; }
        public long? CustomerId { get; set; }
        [Required(ErrorMessage = "Please select Project.")]
        public long ProjectId { get; set; }

        public bool IsParent { get; set; }
        [Required(ErrorMessage = "Task Name (English) is required.")]
        public string TaskNameE { get; set; }
        [Required(ErrorMessage = "Task Name (Arabic) is required.")]
        public string TaskNameA { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionA { get; set; }
        //[Required(ErrorMessage = "Please select Task start date according to Project start/end date")]
        public string StartDate { get; set; }
        public string StartDateAr { get; set; }
        //[Required(ErrorMessage = "Please select Task end date according to Project start/end date")]
        public string EndDate { get; set; }
        public string EndDateAr { get; set; }
        public decimal TotalCost { get; set; }
        [Required(ErrorMessage = "Total Weight is required.")]
        [Range(0, 100, ErrorMessage = "Value for Task Wight must be between {1} and {2}.")]
        public string TotalWeight { get; set; }
        public string NotesE { get; set; }
        public string NotesA { get; set; }
        [Range(0, 100, ErrorMessage = "Value for TaskProgress must be between {1} and {2}.")]
        [Required(ErrorMessage = "Task Progress is required.")]
        //[Range(0, 100, ErrorMessage = "ABC")]
        public string TaskProgress { get; set; }
        public string TaskProgressText { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string ProjectNameE { get; set; }
        public string ProjectNameA { get; set; }
        public long? ParentTask { get; set; }
        //public int SubTasksPercentageCount { get; set; }
        public decimal PrevTasksWeightSum { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }

        public string PreReqTasks { get; set; }
        public List<ProjectTask> RequisitTasks { get; set; }
        public List<TaskEmployee> TaskEmployees { get; set; }
        public List<ProjectTask> SubTasks { get; set; }
        public List<SubTaskWeight> SubTasksWeight { get; set; }
        public string EmployeesAssigned { get; set; }
        public int NoOfSubTasks { get; set; }
    }
}