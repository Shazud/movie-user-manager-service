using System.Collections.Generic;

namespace MovieUserManagerService.Dtos
{
    public class AuthenticationResultFailedDto
    {
        public IEnumerable<string> errors { get; set; }
    }
}