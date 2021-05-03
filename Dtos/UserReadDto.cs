namespace MovieUserManagerService.Read
{
    public class UserReadDto
    {
        public string username { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public bool isAdmin { get; set; }
    }
}