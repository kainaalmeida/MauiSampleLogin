using MauiSampleLogin.Services;

namespace MauiSampleLogin.ViewModels;

[ObservableObject]
public partial class MainViewModel
{
    private readonly ILoginService _loginService;
    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    public MainViewModel(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [RelayCommand]
    public async Task Login()
    {
        var result = await _loginService.LoginAsync(new Models.LoginRequest
        {
            Email = email,
            Password = password
        });

        if (!string.IsNullOrEmpty(result.Token))
        {
        }
    }
}
