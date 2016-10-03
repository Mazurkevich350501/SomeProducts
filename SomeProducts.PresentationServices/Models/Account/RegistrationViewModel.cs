
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
        [Compare(nameof(Password), ErrorMessage = "Passwords dont confirm")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
