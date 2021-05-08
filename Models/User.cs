using System.ComponentModel.DataAnnotations;

namespace MovieUserManagerService.Models
{
    public class User
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
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string role { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string image { get; set; }
    }
}