using System.Collections.Generic;

namespace MovieUserManagerService.Dtos
{
    public class AuthenticationResultFailed
    {
        public IEnumerable<string> errors { get; set; }
    }
}