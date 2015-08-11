namespace EPMS.WebModels.ViewModels.Orders
{
    public class OrdersCreateViewModel
    {
        public OrdersCreateViewModel()
        {
            Orders = new WebsiteModels.Order();
        }
        public WebsiteModels.Order Orders { get; set; }
        public string PageTitle { get; set; }
        public string BtnText { get; set; }
        public string RoleName { get; set; }

    }
}