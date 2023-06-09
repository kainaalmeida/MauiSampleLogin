﻿using MauiSampleLogin.Services;
using MauiSampleLogin.Services.Restaurants;
using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using UraniumUI;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

namespace MauiSampleLogin;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .UseMauiCommunityToolkit()
            .UseSentry(options =>
            {
                options.Dsn = "https://126ce51bb5dc45199ac85d3319982da6@o4505264216408064.ingest.sentry.io/4505264219029504";
                options.Debug = true;
                options.TracesSampleRate = 1.0;
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddMaterialIconFonts();
            });

        builder.Services.AddScoped<ILoginService, LoginService>();

        builder.Services.AddSingleton<CreateAccountViewModel>();
        builder.Services.AddSingleton<CreateAccountPage>();

        builder.Services.AddSingleton<ProductsPage>();

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddScoped<IRestaurantService, RestaurantService>();
        builder.Services.AddSingleton<RestaurantsViewModel>();
        builder.Services.AddSingleton<RestaurantsPage>();

        builder.Services.AddSingleton(typeof(IFingerprint), CrossFingerprint.Current);

        AppCenter.Start("android=82dcc988-a946-4c31-9beb-82b3b9ff00f0", typeof(Analytics), typeof(Crashes), typeof(Distribute));

        return builder.Build();
    }
}
