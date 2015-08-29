namespace EPMS.Models.IdentityModels.ViewModels
{
    public class WebCustomerIdentityViewModel
    {
        public WebCustomerIdentityViewModel()
        {
            SignUp = new CustomerSignUpViewModel();
            Login = new CustomerLoginViewModel();
        }
        public CustomerSignUpViewModel SignUp { get; set; }
        public CustomerLoginViewModel Login { get; set; }
    }
}
