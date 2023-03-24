namespace MauiSampleLogin;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(CreateAccountPage), typeof(CreateAccountPage));
    }
}
