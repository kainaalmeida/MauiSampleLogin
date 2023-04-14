namespace MauiSampleLogin;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override async void OnStart()
    {

        if (!string.IsNullOrEmpty(Preferences.Default.Get("token", string.Empty)))
            await Shell.Current.GoToAsync($"//{nameof(RestaurantsPage)}");

        base.OnStart();
    }
}
