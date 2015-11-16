namespace EPMS.Models.ResponseModels.ReportsResponseModels
{
    public class WarehouseReportDetails
    {
        public string WarehouseName { get; set; }
        public long? AvailabelItems { get; set; }
        public long? DefectiveItems { get; set; }
        public bool IsFull { get; set; }
    }
}
