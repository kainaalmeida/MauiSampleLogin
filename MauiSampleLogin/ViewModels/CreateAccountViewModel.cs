using MauiSampleLogin.Models.CreateAccount;
using MauiSampleLogin.Services;

namespace MauiSampleLogin.ViewModels
{
    [ObservableObject]
    public partial class CreateAccountViewModel
    {
        [ObservableProperty]
        string name;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string email;

        private readonly ILoginService _loginService;
        public CreateAccountViewModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [RelayCommand]
        public async Task Save()
        {
            var request = new CreateAccountRequest
            {
                Name = name,
                Email = email,
                Password = password,
            };

            var result = await _loginService.CreateAccount(request);

            if (result)
                await Shell.Current.GoToAsync("..");
        }
    }
}
