using Microsoft.AspNetCore.Identity.Data;
using Shortly.Contract.Services.V1.Identity;
using LoginRequest = Shortly.Contract.Services.V1.Identity.LoginRequest;
using RegisterRequest = Shortly.Contract.Services.V1.Identity.RegisterRequest;

namespace Shortly.Application.Abstractions
{
    public interface IUserServices
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<TokenRefreshResponse> RefreshTokenAsync(TokenRefreshRequest request);
        Task<(RegisterResponse?, string?)> RegisterAsync(RegisterRequest request);
    }
}
