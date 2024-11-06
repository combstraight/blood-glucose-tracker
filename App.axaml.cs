using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BloodGlucoseTracker.ViewModels;

namespace BloodGlucoseTracker;

public partial class App : Application
{
    public static MainViewModel MainViewModel { get; private set; } = new MainViewModel();
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = MainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}