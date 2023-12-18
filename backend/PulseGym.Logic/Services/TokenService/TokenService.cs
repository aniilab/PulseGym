using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using PulseGym.Entities.Exceptions;
using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private readonly UserManager<User> _userManager;

        private readonly ITokenRepository _tokenRepository;

        public TokenService(IConfiguration configuration, UserManager<User> userManager, ITokenRepository tokenRepository)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        public async Task<TokensDTO> GenerateTokensAsync(User user)
        {
            var accessToken = await GenerateAccessTokenAsync(user);
            var refreshToken = GenerateRefreshToken();

            await _userManager.SetAuthenticationTokenAsync(
                        user,
                        _configuration.GetSection("ApplicationName").Value,
                        "RefreshToken",
                        refreshToken);

            return new TokensDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<TokensDTO> RefreshAsync(string refreshToken)
        {
            if (!ValidateRefreshToken(refreshToken))
            {
                throw new UnauthorizedException("Invalid refresh token.");
            }

            var user = await _tokenRepository.GetUserByTokenAsync(refreshToken);

            await _tokenRepository.DeleteByUserIdAsync(user.Id);

            var tokens = await GenerateTokensAsync(user);

            return tokens;
        }

        public async Task DeleteTokens(Guid userId)
        {
            await _tokenRepository.DeleteByUserIdAsync(userId);
        }

        private async Task<string> GenerateAccessTokenAsync(User user)
        {
            var role = (await _userManager.GetRolesAsync(user)).Single();
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var secret = Encoding.UTF8.GetBytes(_configuration.GetSection("Authentication:AccessTokenSecret").Value);

            var key = new SymmetricSecurityKey(secret);

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration.GetSection("Authentication:AccessTokenExpirationMinutes").Value)),
                issuer: _configuration.GetSection("Authentication:Issuer").Value,
                audience: _configuration.GetSection("Authentication:Audience").Value,
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private string GenerateRefreshToken()
        {
            var secret = Encoding.UTF8.GetBytes(_configuration.GetSection("Authentication:RefreshTokenSecret").Value);

            var key = new SymmetricSecurityKey(secret);

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration.GetSection("Authentication:RefreshTokenExpirationMinutes").Value)),
                issuer: _configuration.GetSection("Authentication:Issuer").Value,
                audience: _configuration.GetSection("Authentication:Audience").Value,
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool ValidateRefreshToken(string refreshToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateActor = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration.GetSection("Authentication:Issuer").Value,
                ValidAudience = _configuration.GetSection("Authentication:Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Authentication:RefreshTokenSecret").Value))
            };

            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
