using Microsoft.AspNetCore.Mvc;
using StudySphere.Models;
using StudySphere.Services;

namespace StudySphere.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ICreateUserService _createUserService;
        private readonly ILoginService _loginService;
        private readonly IResetPasswordService _resetPasswordService;


        public UserController(ICreateUserService createUserService, ILoginService loginService, IResetPasswordService resetPasswordService)
        {
            _createUserService = createUserService;
            _loginService = loginService;
            _resetPasswordService = resetPasswordService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser user, CancellationToken cancellationToken)
        {
            return Ok(await _createUserService.CreateUser(user, cancellationToken));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginFields user, CancellationToken cancellationToken)
        {
            return await _loginService.Login(user.Username, user.Password, cancellationToken);
        }

        [HttpPost]
        [Route("resetPasswordLink")]
        public async Task<IActionResult> ResetPasswordLink([FromBody] ResetAccount user, CancellationToken cancellationToken)
        {
            return Ok(await _resetPasswordService.ResetPasswordLink(user.Email, cancellationToken));
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword request, CancellationToken cancellationToken)
        {
            return Ok(await _resetPasswordService.ResetPassword(request, cancellationToken));
        }
    }
}