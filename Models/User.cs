using System.ComponentModel.DataAnnotations;

namespace MovieUserManagerService.Models
{
    public class User
    {
        [Key]
        public string username { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string firstname { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
    }
}