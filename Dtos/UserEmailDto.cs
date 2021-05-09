namespace MovieUserManagerService.Dtos
{
    public class UserEmailDto
    {
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public bool newsletter { get; set; }
    }
}