﻿using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.DIF
{
    public class DifListViewModel
    {
        //Rfi's Search Request data
        public DifSearchRequest SearchRequest { get; set; }
        /// <summary>
        /// Requests List
        /// </summary>
        public IEnumerable<WebsiteModels.DIF> aaData { get; set; }
        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;
        public int sLimit;
        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;
        public string sEcho;
    }
}