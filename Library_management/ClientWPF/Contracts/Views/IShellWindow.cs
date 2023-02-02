using System.Windows.Controls;

using ClientWPF.Behaviors;

using MahApps.Metro.Controls;

namespace ClientWPF.Contracts.Views;

public interface IShellWindow
{
    Frame GetNavigationFrame();

    void ShowWindow();

    void CloseWindow();

    Frame GetRightPaneFrame();

    SplitView GetSplitView();

    RibbonTabsBehavior GetRibbonTabsBehavior();
}
