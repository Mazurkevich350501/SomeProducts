
using System.ComponentModel.DataAnnotations;

namespace SomeProducts.PresentationServices.Models.Account
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resource))]
        [Compare(nameof(Password),
            ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "PasswordDontConfirm")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
