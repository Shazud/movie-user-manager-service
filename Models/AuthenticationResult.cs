using System.Collections.Generic;

namespace MovieUserManagerService.Models
{
    public class AuthenticationResult
    {
        public string token { get; set; }
        public bool success { get; set; }
        public IEnumerable<string> errors { get; set; }
    }
}