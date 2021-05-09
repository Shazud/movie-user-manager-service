using Microsoft.AspNetCore.Mvc;
using MovieUserManagerService.Models;
using MovieUserManagerService.Services;

namespace MovieUserManagerService.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenValidationController : ControllerBase
    {
        private readonly IUserAuthenticationService _auth;

        public TokenValidationController(IUserAuthenticationService auth)
        {
            _auth = auth;
        }

        [HttpPost]
        public ActionResult ValidateToken(Token token)
        {
            return _auth.ValidateToken(token.token) != string.Empty 
                    ? Ok(new {ok = true})
                    : Unauthorized(new {ok = false});
        }
    }
}