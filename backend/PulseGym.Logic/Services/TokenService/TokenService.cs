using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using PulseGym.DAL.Models;

namespace PulseGym.Logic.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public TokenService(IConfiguration configuration, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> GenerateAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(await GetClaimsBasedOnRoleAsync(user));

            var secret = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value);

            var key = new SymmetricSecurityKey(secret);

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private async Task<ICollection<Claim>> GetClaimsBasedOnRoleAsync(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var result = new List<Claim>();

            foreach (var role in userRoles)
            {
                var roleIdentity = _roleManager.Roles.FirstOrDefault(r => r.Name == role);
                if (roleIdentity != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(roleIdentity);
                    result.AddRange(roleClaims);
                }
            }

            return result;
        }
    }
}
