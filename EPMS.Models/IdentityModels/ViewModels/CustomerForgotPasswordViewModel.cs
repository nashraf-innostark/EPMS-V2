using System.ComponentModel.DataAnnotations;

namespace EPMS.Models.IdentityModels.ViewModels
{
    public class CustomerForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}
