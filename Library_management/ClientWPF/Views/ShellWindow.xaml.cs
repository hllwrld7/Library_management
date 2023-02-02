using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

using ClientWPF.Behaviors;
using ClientWPF.Contracts.Services;
using ClientWPF.Contracts.Views;
using ClientWPF.Core.Contracts.Services;
using ClientWPF.Core.Services;
using ClientWPF.Services;
using Fluent;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ClientWPF.Views;

public partial class ShellWindow : MetroWindow, IShellWindow, IRibbonWindow, INotifyPropertyChanged
{
    private readonly IRightPaneService _rightPaneService;
    private readonly INavigationService _navigationService;
    private readonly ILibraryManagementService _libraryManagementService;
    private IWindowManagerService _windowManager;

    private int _chosenBooksCount = 0;

    public RibbonTitleBar TitleBar
    {
        get => (RibbonTitleBar)GetValue(TitleBarProperty);
        private set => SetValue(TitleBarPropertyKey, value);
    }

    private static readonly DependencyPropertyKey TitleBarPropertyKey = DependencyProperty.RegisterReadOnly(nameof(TitleBar), typeof(RibbonTitleBar), typeof(ShellWindow), new PropertyMetadata());

    public static readonly DependencyProperty TitleBarProperty = TitleBarPropertyKey.DependencyProperty;

    public ShellWindow(IServiceProvider serviceProvider, IRightPaneService rightPaneService, IWindowManagerService windowManager)
    {
        System.Diagnostics.Debugger.Launch();
        _rightPaneService = rightPaneService;
        InitializeComponent();
        navigationBehavior.Initialize(serviceProvider);
        _navigationService = (INavigationService)serviceProvider.GetService(typeof(INavigationService));
        _libraryManagementService = (ILibraryManagementService)serviceProvider.GetService(typeof(ILibraryManagementService));
        DataContext = this;
        _windowManager = windowManager;
    }

    public Frame GetNavigationFrame()
        => shellFrame;

    public RibbonTabsBehavior GetRibbonTabsBehavior()
        => tabsBehavior;

    public Frame GetRightPaneFrame()
        => rightPaneFrame;

    public SplitView GetSplitView()
        => splitView;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var window = sender as MetroWindow;
        TitleBar = window.FindChild<RibbonTitleBar>("RibbonTitleBar");
        TitleBar.InvalidateArrange();
        TitleBar.UpdateLayout();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _libraryManagementService.DisposeOfSqliteService();
        tabsBehavior.Unsubscribe();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;
        }

        storage = value;
        OnPropertyChanged(propertyName);
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void Dictionaries_Click(object sender, RoutedEventArgs e)
    {
        var dataGridPage = new DataGridPage(_libraryManagementService);
        
        _navigationService.NavigateTo(dataGridPage.GetType(), BookCategory.Dictionary);
        dataGridPage.BookChosen += OnBookChosen;
    }

    private void OnBookChosen(object sender, EventArgs e)
    {
        btCheckout.Content = $"{_chosenBooksCount + 1} books chosen";
    }

    private void Encyclopedias_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo(typeof(DataGridPage), BookCategory.Encyclopedia);
    }

    private void Fiction_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo(typeof(DataGridPage), BookCategory.Fiction);
    }

    private void Other_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo(typeof(DataGridPage), BookCategory.None);
    }

    private void BorrowBook_Click(object sender, RoutedEventArgs e)
    {
         

    }

    private void FillLibrary_Click(object sender, RoutedEventArgs e)
    {
        _libraryManagementService.FillLibrary();
    }
}
