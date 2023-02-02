using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using ClientWPF.Contracts.Views;
using ClientWPF.Core.Contracts.Services;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace ClientWPF.Views;

public partial class ShellDialogWindow : Page, INotifyPropertyChanged, INavigationAware
{
    ILibraryManagementService _libraryManagementService;
    public ShellDialogWindow(ILibraryManagementService sampleDataService)
    {
        InitializeComponent();
        DataContext = this;
        _libraryManagementService = sampleDataService;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnNavigatedFrom()
    {
        
    }

    public void OnNavigatedTo(object parameter)
    {
        
    }

    private void AddBookClick(object sender, RoutedEventArgs e)
    {
        var title = tbTitle.Text;
        _libraryManagementService.AddBook(title, "2022-2-22", "Drama");
    }
}
