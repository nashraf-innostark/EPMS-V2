﻿using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class PhysicalCountModel
    {
        public long PCId { get; set; }
        public long RequesterEmpId { get; set; }
        public string RequesterName { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public string NotesEn { get; set; }
        public string NotesAr { get; set; }

        public long WarehouseId { get; set; }
    }
}