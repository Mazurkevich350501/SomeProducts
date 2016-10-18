
using System.ComponentModel.DataAnnotations;

namespace SomeProducts.PresentationServices.Models.Account
{
    public class LogInUserModel
    {
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }
    }
}
