
using System.ComponentModel.DataAnnotations;
using Resources;

namespace SomeProducts.PresentationServices.Models.Account
{
    public class LogInUserModel
    {
        [Display(Name = "Name", ResourceType = typeof(LocalResource))]
        [Required(ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "RequiredField")]
        public string Name { get; set; }

        [Display(Name = "Password", ResourceType = typeof(LocalResource))]
        [Required(ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "RequiredField")]
        public string Password { get; set; }
    }
}
