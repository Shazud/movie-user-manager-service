using Microsoft.AspNetCore.Mvc;
using MovieUserManagerService.Models;
using MovieUserManagerService.Services;

namespace MovieUserManagerService.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TokenValidationController : ControllerBase
    {
        private readonly IUserAuthenticationService _auth;

        public TokenValidationController(IUserAuthenticationService auth)
        {
            _auth = auth;
        }

        [HttpPost("token")]
        public ActionResult ValidateToken(Token token)
        {
            return _auth.ValidateToken(token.token) != string.Empty 
                    ? Ok(new {ok = true})
                    : Unauthorized(new {ok = false});
        }

        [HttpPost("validate")]
        public ActionResult ValidateTokenAndUsername(Token token)
        {
            return _auth.ValidateToken(token.token) != string.Empty && _auth.GetTokenClaimValue(token.token, "id") == token.username
                    ? Ok(new {ok = true})
                    : Unauthorized(new {ok = false});
        }
    }
}