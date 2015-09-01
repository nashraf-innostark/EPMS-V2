namespace EPMS.Models.IdentityModels.ViewModels
{
    public class WebCustomerIdentityViewModel
    {
        public WebCustomerIdentityViewModel()
        {
            SignUp = new CustomerSignUpViewModel();
            Login = new CustomerLoginViewModel();
            ForgotPassword = new CustomerForgotPasswordViewModel();
            ResetPassword = new CustoemrResetPasswordViewModel();
        }
        public CustomerSignUpViewModel SignUp { get; set; }
        public CustomerLoginViewModel Login { get; set; }
        public CustomerForgotPasswordViewModel ForgotPassword { get; set; }
        public CustoemrResetPasswordViewModel ResetPassword { get; set; }
    }
}
