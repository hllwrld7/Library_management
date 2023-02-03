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

public partial class ShellWindow : MetroWindow, IShellWindow, IRibbonWindow
{
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

    public ShellWindow(IServiceProvider serviceProvider, IWindowManagerService windowManager)
    {
        InitializeComponent();
        navigationBehavior.Initialize(serviceProvider);
        _navigationService = (INavigationService)serviceProvider.GetService(typeof(INavigationService));
        _libraryManagementService = (ILibraryManagementService)serviceProvider.GetService(typeof(ILibraryManagementService));
        DataContext = this;
        _windowManager = windowManager;
        _libraryManagementService.OrderBookNumberChanged += OnOrderBookNumberChanged;
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
        _libraryManagementService.RewriteDbTables();
        _libraryManagementService.DisposeOfSqliteService();
        _libraryManagementService.OrderBookNumberChanged -= OnOrderBookNumberChanged;
        tabsBehavior.Unsubscribe();
    }

    private void Dictionaries_Click(object sender, RoutedEventArgs e)
    {
        var dataGridPage = new DataGridPage(_libraryManagementService);
        
        _navigationService.NavigateTo(dataGridPage.GetType(), BookCategory.Dictionary);
    }

    private void OnOrderBookNumberChanged(object sender, int e)
    {
        _chosenBooksCount = e;
        btCheckout.Content = $"{_chosenBooksCount} books chosen";
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
        _navigationService.NavigateTo(typeof(ShellDialogWindow));
    }

    private void FillLibrary_Click(object sender, RoutedEventArgs e)
    {
        _libraryManagementService.FillLibrary();
        

    }
}
