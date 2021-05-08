namespace MovieUserManagerService.Utils
{
    public static class ErrorMessages
    {
        public const string passwordMissmatch = "Passwords do not match!";
        public const string emailFormat = "Invalid email address!";
        public const string userExists = "Username is already used!";
        internal static string invalidCredentials =  "Invalid username or password!";
        internal static string userNotLoggedIn = "User not logged in!";
    }
}