﻿namespace EPMS.Models.DomainModels
{
    public class TransferItemStatus
    {
        public long Id { get; set; }
        public string NotesEn { get; set; }
        public string NotesAr { get; set; }
        public int Status { get; set; }
        public string ManagerId { get; set; }
    }
}