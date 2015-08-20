﻿using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class Status
    {
        public long StatusId { get; set; }
        public string StatusNameEn { get; set; }
        public string StatusNameAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
    }
}