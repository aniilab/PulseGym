﻿using PulseGym.DAL.Models;
using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Services
{
    public interface IAuthService
    {
        Task<TokensDTO?> LoginUserAsync(UserLoginRequestDTO user);

        Task<User> RegisterUserAsync(UserRegisterDTO userRegister, string role);

        Task LogoutAsync(Guid userId);

        Task<UserLoginResponseDTO> GetUserAsync(Guid userId);
    }
}
