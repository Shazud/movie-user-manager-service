using System.ComponentModel.DataAnnotations;
using MovieUserManagerService.Utils;

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
        [Compare(nameof(password), ErrorMessage = ErrorMessages.passwordMissmatch)]
        public string passwordConfirmation { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(RegularExpressions.email, ErrorMessage = ErrorMessages.emailFormat)]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string role { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string image { get; set; }
    }
}