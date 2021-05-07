using System.ComponentModel.DataAnnotations;

namespace MovieUserManagerService.Dtos
{
    public class UserUpdateDto
    {
        [Key]
        [DataType(DataType.Text)]
        public string username { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string lastname { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string firstname { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string passwordConfirmation { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required]
        public bool isAdmin { get; set; }
    }
}