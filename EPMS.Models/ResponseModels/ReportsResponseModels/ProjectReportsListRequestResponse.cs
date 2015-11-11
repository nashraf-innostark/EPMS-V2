﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels.ReportsResponseModels
{
    public class ProjectReportsListRequestResponse
    {
        public ProjectReportsListRequestResponse()
        {
            Projects = new List<Report>();
        }
        public IEnumerable<Report> Projects { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
    }
}