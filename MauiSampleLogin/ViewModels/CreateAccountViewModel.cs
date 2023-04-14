using CommunityToolkit.Maui.Alerts;
using MauiSampleLogin.Contracts.CreateAccount;
using MauiSampleLogin.Models.CreateAccount;
using MauiSampleLogin.Services;
using System.Text;

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
            if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                var request = new CreateAccountRequest
                {
                    Name = name,
                    Email = email,
                    Password = password,
                };

                var validator = new CreateAccountContract(request);
                if (!validator.IsValid)
                {

                    var messages =
                        validator
                        .Notifications
                        .Select(x => x.Message);

                    var sb = new StringBuilder();
                    foreach (var message in messages)
                        sb.Append($"{message}\n");

                    await Shell.Current.DisplayAlert("Atenção",sb.ToString(),"OK");

                    return;
                }

                var result = await _loginService.CreateAccount(request);

                if (result)
                {
                    await Toast.Make("Usuário cadastrado com sucesso", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                    await Shell.Current.GoToAsync("..");
                }
            }
            else
                await Toast.Make("Sem conexão com a internet", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
        }
    }
}
