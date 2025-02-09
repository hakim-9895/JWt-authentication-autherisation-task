using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Service;

namespace WebApplication7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Authenticationservice _authenticationservice;

        public  AuthenticationController(Authenticationservice authservice)
        {
            _authenticationservice = authservice;
        }
        [HttpPost("authentication")]
        public  IActionResult Get( [FromBody] LoginModal login)
        {
            var token =_authenticationservice.Authenticate(login.Name, login.Password);
            if (token == null)
            
                return Unauthorized();

                return Ok(new{ Token = token});
            
        }

    }
   public class LoginModal{
        public string Name { get; set; }
        public string Password { get; set; }

    }
}
