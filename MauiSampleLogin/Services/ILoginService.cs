using MauiSampleLogin.Models;

namespace MauiSampleLogin.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}
