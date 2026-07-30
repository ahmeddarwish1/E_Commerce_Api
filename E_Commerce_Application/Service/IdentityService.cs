using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce_Application.Service
{
    public class IdentityService : IIdentityService


    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<bool>> CheckPasswordAsync(string Email, string Password, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null)
                return Result<bool>.Fail(Error.NotFound("User not Found"));

            var isValid = await _userManager.CheckPasswordAsync(user, Password);
            return Result<bool>.Ok(isValid);
        }

        public async Task<Result<IdentityUserResult>> CreatUser(RegisterDto registerDto, CancellationToken ct = default)
        {
            var user = new ApplicationUser()
            {

                Email = registerDto.Email,
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {

                var errors = result.Errors.Select(e => new Error(e.Code, e.Description, ErrorType.Unauthorized)).ToList();
                return Result<IdentityUserResult>.Fail(errors);
            }
            return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.Email, user.UserName, user.DisplayName));
        }

        public async Task<Result<IdentityUserResult>> FindByEmailAsync(string Email, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null)

                return Result<IdentityUserResult>.Fail(Error.NotFound("User not Found"));

            else

                return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.Email, user.UserName, user.DisplayName));
        }

        public async Task<Result<IEnumerable<string>>> GetRoleAsyncs(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return Result<IEnumerable<string>>.Fail(Error.NotFound("User not Found"));
            }
            var roles = await _userManager.GetRolesAsync(user);
            return Result<IEnumerable<string>>.Ok(roles);
        }
    }
}
