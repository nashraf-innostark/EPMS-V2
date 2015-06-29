namespace EPMS.Models.RequestModels
{
    public class PurchaseOrderStatus
    {
        public long PurchaseOrderId { get; set; }
        public string Notes { get; set; }
        public string NotesAr { get; set; }
        public int Status { get; set; }
        public string ManagerId { get; set; }
    }
}
