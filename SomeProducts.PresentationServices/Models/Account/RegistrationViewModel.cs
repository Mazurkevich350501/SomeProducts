
using Resources;
using System.ComponentModel.DataAnnotations;

namespace SomeProducts.PresentationServices.Models.Account
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Name", ResourceType = typeof(LocalResource))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "RequiredField")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(LocalResource))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(LocalResource))]
        [Compare(nameof(Password),
            ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "PasswordDontConfirm")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
