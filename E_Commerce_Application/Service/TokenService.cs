using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce_Application.Service
{
    public class TokenService(IOptions<JWTSettings> options) : ITokenService
    {
        private readonly JWTSettings settings = options.Value;
        public string createToken(string userid, string email, string username, IEnumerable<string> roles)
        {
            //private Claims == > user
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userid),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, username)
            };

            Claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
            var cerdinals = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature); //header
            var token = new JwtSecurityToken(
                                            issuer: settings.Issuer,
                                            audience: settings.Audience,
                                            claims: Claims,
                                            expires: DateTime.UtcNow.AddMinutes(settings.ExpirationMinutes),
                                            signingCredentials: cerdinals
                                            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
    public class JWTSettings
    {
        public string SecretKey { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public int ExpirationMinutes { get; set; }
    }
}
