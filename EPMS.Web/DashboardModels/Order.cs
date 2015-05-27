namespace EPMS.Web.DashboardModels
{
    public class Order
    {
        public long OrderId { get; set; }
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public int? OrderStatus { get; set; }
        public long CustomerId { get; set; }
    }
}