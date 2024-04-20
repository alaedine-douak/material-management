using System.Windows;

namespace GM;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = new MainWindow();

        MainWindow.Show();

        base.OnStartup(e);
    }
}

