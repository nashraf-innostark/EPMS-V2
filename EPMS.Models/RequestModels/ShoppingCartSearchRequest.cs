namespace EPMS.Models.RequestModels
{
    public class ShoppingCartSearchRequest
    {
        public long ProductId { get; set; }
        public string From { get; set; }
        public string UserCartId { get; set; }
    }
}