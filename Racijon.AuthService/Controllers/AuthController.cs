using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Racijon.AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        //private readonly Mapper _mapper = mapper;
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            //var user = _userRepository.GetUserByEmailOrUsername(request.Identifier);
            Tuple<string, string, Guid, string> user = new("test@example.com", "test", Guid.NewGuid(), "password");
            //if (user != null && VerifyPassword(request.Password, user.PasswordHash))
            //{
            //    var token = _jwtService.GenerateToken(_mapper.Map<User, TokenModel>(user));
            //    return Ok(new { Token = token });
            //}
            if (/*user != null && VerifyPassword(request.Password, user.PasswordHash)*/
                (request.Identifier == user.Item1 || request.Identifier == user.Item2) && request.Password == user.Item4)
            {
                var token = _jwtService.GenerateToken(new Tuple<string, string, Guid>(user.Item1, user.Item2, user.Item3));
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        public string Identifier { get; set; } = string.Empty; // Email or username
        public string Password { get; set; } = string.Empty;
    }
}
