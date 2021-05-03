using System.ComponentModel.DataAnnotations;

namespace MovieUserManagerService.Read
{
    public class UserLoginDto
    {
        [Key]
        [DataType(DataType.Text)]
        public string username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}