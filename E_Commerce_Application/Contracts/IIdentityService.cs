using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_Commerce_Application.Contracts
{
    public interface IIdentityService
    {
        //Login Register
        Task<Result<IdentityUserResult>> FindByEmailAsync(string Email, CancellationToken ct = default);
        Task<Result<bool>> CheckPasswordAsync(string Email, string Password, CancellationToken ct = default);
        Task<Result<IdentityUserResult>> CreatUser(RegisterDto registerDto, CancellationToken ct = default);

        Task<Result<IEnumerable<string>>> GetRoleAsyncs(string email);
    }
}
