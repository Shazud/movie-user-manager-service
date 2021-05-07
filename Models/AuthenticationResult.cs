namespace MovieUserManagerService.Models
{
    public class AuthenticationResult
    {
        public string token { get; set; }
        public bool success { get; set; }
        public string errorMessage { get; set; }
    }
}