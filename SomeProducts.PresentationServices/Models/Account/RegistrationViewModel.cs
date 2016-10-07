
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SomeProducts.PresentationServices.Models.Account
{
    public class RegistrationViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "Password don't confirm")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
