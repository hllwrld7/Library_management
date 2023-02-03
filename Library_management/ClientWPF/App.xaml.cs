using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

using ClientWPF.Contracts.Services;
using ClientWPF.Contracts.Views;
using ClientWPF.Core.Contracts.Services;
using ClientWPF.Core.Services;
using ClientWPF.Models;
using ClientWPF.Services;
using ClientWPF.Views;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ClientWPF;

public partial class App : Application
{
    private IHost _host;

    public T GetService<T>()
        where T : class
        => _host.Services.GetService(typeof(T)) as T;

    public App()
    {
    }

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        _host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureAppConfiguration(c =>
                {
                    c.SetBasePath(appLocation);
                })
                .ConfigureServices(ConfigureServices)
                .Build();
        await _host.StartAsync();
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {

        // App Host
        services.AddHostedService<ApplicationHostService>();

        // Activation Handlers

        // Core Services
        services.AddSingleton<ISqliteService, SqliteService>();

        // Services
        services.AddSingleton<IWindowManagerService, WindowManagerService>();
        services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();
        services.AddSingleton<ISystemService, SystemService>();
        services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();
        services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
        services.AddSingleton<ILibraryManagementService, LibraryManagementService>();
        services.AddSingleton<IRightPaneService, RightPaneService>();
        services.AddSingleton<INavigationService, NavigationService>();

        // Views
        services.AddTransient<IShellWindow, ShellWindow>();

        services.AddTransient<MainPage>();

        services.AddTransient<DataGridPage>();

        services.AddTransient<SettingsPage>();

        services.AddTransient<ShellDialogWindow>();

        // Configuration
        services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        _host = null;
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        
    }
}
