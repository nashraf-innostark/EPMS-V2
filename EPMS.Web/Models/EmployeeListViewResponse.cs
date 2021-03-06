﻿using System.Collections.Generic;

namespace EPMS.Web.Models
{
    /// <summary>
    /// Employee List View Response
    /// </summary>
    public class EmployeeListViewResponse
    {
        /// <summary>
        /// Job Titles 
        /// </summary>
        public IEnumerable<JobTitle> JobTitles { get; set; }

        /// <summary>
        /// Departments List
        /// </summary>
        public IEnumerable<Department> Departments { get; set; }
    }
}