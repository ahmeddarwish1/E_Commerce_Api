using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Identity;

namespace E_Commerce_Application.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }
        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var userResult = await _identityService.FindByEmailAsync(loginDto.Email);

            if (!userResult.IsSuccess)
            {
                return Result<UserDto>.Fail(userResult.Errors);
            }
            var checkPasswordResult = await _identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password);
            if (!checkPasswordResult.IsSuccess)
            {
                return Result<UserDto>.Fail(Error.Unauthorized("Invalid username or password"));
            }

            var rolesresult = await _identityService.GetRoleAsyncs(loginDto.Email);
            var token = _tokenService.createToken(userResult.data.Id, userResult.data.Email, userResult.data.UserName, rolesresult.data);
            return Result<UserDto>.Ok(new UserDto()
            {
                Email = userResult.data.Email,
                DisplayName = userResult.data.DisplayName,
                Token = token
            });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var result = await _identityService.CreatUser(registerDto);
            if (!result.IsSuccess || result.data is null)
                return Result<UserDto>.Fail(result.Errors);
            var rolesresult = await _identityService.GetRoleAsyncs(registerDto.Email);
            var token = _tokenService.createToken(result.data.Id, result.data.Email, result.data.UserName, rolesresult.data);
            return Result<UserDto>.Ok(new UserDto()
                {
                    Email = result.data.Email,
                    DisplayName = result.data.DisplayName,
                    Token = token
                });
        }
    }
}
