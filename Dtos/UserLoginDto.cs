using System.ComponentModel.DataAnnotations;

namespace MovieUserManagerService.Dtos
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