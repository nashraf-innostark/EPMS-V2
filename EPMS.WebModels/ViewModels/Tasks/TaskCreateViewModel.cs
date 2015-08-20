﻿using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Tasks
{
    public class TaskCreateViewModel
    {
        public TaskCreateViewModel()
        {
            ProjectTask = new WebsiteModels.ProjectTask();
            RequisitTasks = new List<long>();
            AssignedEmployees = new List<long>();
            OldRequisitTasks = new List<long>();
            OldAssignedEmployees = new List<long>();
        }
        public WebsiteModels.ProjectTask ProjectTask { get; set; }
        public List<long> RequisitTasks { get; set; }
        public List<long> OldRequisitTasks { get; set; }
        public List<WebsiteModels.ProjectTask> PreRequisitTasks { get; set; }
        public List<long> AssignedEmployees { get; set; }
        public List<long> OldAssignedEmployees { get; set; }
        public IEnumerable<WebsiteModels.Project> Projects { get; set; }
        public IEnumerable<WebsiteModels.ProjectsForDDL> ProjectsForDdls { get; set; }
        public IEnumerable<WebsiteModels.ProjectTask> ProjectAllTasks { get; set; }
        public IEnumerable<WebsiteModels.ProjectTask> AllParentTasks { get; set; }
        public IEnumerable<WebsiteModels.Employee> AllEmployees { get; set; }
        public string PageTitle { get; set; }
        public string BtnText { get; set; }
        public string Header { get; set; }
    }
}