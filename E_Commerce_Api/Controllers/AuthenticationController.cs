using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Api.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto, CancellationToken ct = default)
        {
            return ToActionResult(await authenticationService.LoginAsync(loginDto, ct));
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto, CancellationToken ct = default)
        {
            return ToActionResult(await authenticationService.RegisterAsync(registerDto, ct));
        }








    }
}
