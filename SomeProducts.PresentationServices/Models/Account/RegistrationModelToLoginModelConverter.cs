namespace SomeProducts.PresentationServices.Models.Account
{
    public static class RegistrationModelToLoginModelConverter
    {
        public static LogInUserModel ToLogInUserModel(this RegistrationViewModel model)
        {
            return new LogInUserModel()
            {
                Name = model.Name,
                Password = model.Password
            };
        }
    }
}