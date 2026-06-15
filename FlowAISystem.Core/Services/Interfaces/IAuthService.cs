using FlowAISystem.Data.DTOs.Auth;

namespace FlowAISystem.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task <TokenResponseDto> LoginAsync(LoginDto dto);
        Task LogoutAsync();
    }
}