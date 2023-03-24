using Flurl;
using Flurl.Http;
using MauiSampleLogin.Helper;
using MauiSampleLogin.Models;
using MauiSampleLogin.Models.CreateAccount;

namespace MauiSampleLogin.Services
{
    public class LoginService : ILoginService
    {
        public async Task<bool> CreateAccount(CreateAccountRequest createAccountRequest)
        {
            try
            {
                var response = await Constants.BASE_URL
                .AppendPathSegment("/users")
                .PostJsonAsync(createAccountRequest);

                return response.ResponseMessage.IsSuccessStatusCode;
            }
            catch (FlurlHttpException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }
    }
}
