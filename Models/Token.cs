using System.ComponentModel.DataAnnotations;

namespace MovieUserManagerService.Models
{
    public class Token
    {
        [Required]
        public string token { get; set; }
    }
}