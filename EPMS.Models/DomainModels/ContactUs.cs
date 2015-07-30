﻿using System;

namespace EPMS.Models.DomainModels
{
    public class ContactUs
    {
        public long ContactUsId { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string Address { get; set; }
        public string AddressAr { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string POBox { get; set; }
        public string Website { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string FormEmail { get; set; }
        public string ContentEn { get; set; }
        public string ContentAr { get; set; }
        public bool ShowToPublic { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
    }
}
