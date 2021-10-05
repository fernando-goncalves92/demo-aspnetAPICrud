namespace App.Api.ViewModels.Authentication
{
    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel User { get; set; }
    }
}
