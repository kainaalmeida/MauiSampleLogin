using CommunityToolkit.Maui.Alerts;
using MauiSampleLogin.Contracts.Login;
using MauiSampleLogin.Services;
using Plugin.Fingerprint.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace MauiSampleLogin.ViewModels;

[ObservableObject]
public partial class MainViewModel
{
    private readonly ILoginService _loginService;
    private readonly IFingerprint _fingerprint;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    public MainViewModel(ILoginService loginService, IFingerprint fingerprint)
    {
        _loginService = loginService;
        _fingerprint = fingerprint;
    }

    [RelayCommand]
    public async Task Login()
    {
        if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Unknown ||
            Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.None)
        {
            await Toast.Make("Sem conexão com a internet", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            return;
        }

        var loginRequest = new Models.LoginRequest
        {
            Email = email,
            Password = password
        };

        var validator = new LoginContract(loginRequest);
        if (!validator.IsValid)
        {
            var messages = validator.Notifications.Select(x => x.Message);
            var sb = new StringBuilder();

            foreach (var message in messages)
                sb.Append($"{message}\n");

            await Shell.Current.DisplayAlert("Atenção", sb.ToString(), "OK");
            return;
        }

        var result = await _loginService.LoginAsync(loginRequest);

        if (string.IsNullOrEmpty(result.Access_Token))
        {
            await Toast.Make("A requisação falhou, tente novamente.", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            return;
        }

        Preferences.Default.Set("token", result.Access_Token);
        Preferences.Default.Set("refreshToken", result.Refresh_Token);

        await Shell.Current.GoToAsync($"//{nameof(RestaurantsPage)}");

    }

    [RelayCommand]
    public async Task CreateAccount()
    {
        await Shell.Current.GoToAsync(nameof(CreateAccountPage));
    }

    [RelayCommand]
    public async Task ValidateBiometric()
    {
        var isValid = await _fingerprint.IsAvailableAsync();

        if (!isValid)
        {
            await Shell.Current.DisplayAlert("Atenção!", "Dispositivo não contém sensor biométrica", "OK");
            return;
        }

        if (!string.IsNullOrEmpty(Preferences.Default.Get("token", string.Empty)))
        {
            var request = new AuthenticationRequestConfiguration("Validação biométrica", "Precisamos validar sua biometria para prosseguir");
            var result = await _fingerprint.AuthenticateAsync(request);
            if (result.Authenticated)
            {
                await Shell.Current.DisplayAlert("Autenticado!", "Acesso garantido", "OK");
                await Shell.Current.GoToAsync($"//{nameof(RestaurantsPage)}");
            }
            else
                await Shell.Current.DisplayAlert("Não autenticado", "Acesso negado", "OK");
        }

    }
}
