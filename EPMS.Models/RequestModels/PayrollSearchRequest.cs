﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class PayrollSearchRequest : GetPagedListRequest
    {
        public Guid UserId { get; set; }
        public long EmployeeId { get; set; }
        public long JobTitleId { get; set; }
        public PayrollByColumn EmployeeByColumn
        {
            get
            {
                return (PayrollByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
